namespace SteamAutoMarket.Steam.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    using Newtonsoft.Json;

    using SteamAutoMarket.Steam.Market.Exceptions;

    [Serializable]
    public class SteamGuardAccount
    {
        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("device_id")]
        public string DeviceID { get; set; }

        /// <summary>
        /// Set to true if the authenticator has actually been applied to the account.
        /// </summary>
        [JsonProperty("fully_enrolled")]
        public bool FullyEnrolled { get; set; }

        [JsonProperty("identity_secret")]
        public string IdentitySecret { get; set; }

        [JsonIgnore]
        public WebProxy Proxy { get; set; }

        [JsonProperty("revocation_code")]
        public string RevocationCode { get; set; }

        [JsonProperty("secret_1")]
        public string Secret1 { get; set; }

        [JsonProperty("serial_number")]
        public string SerialNumber { get; set; }

        [JsonProperty("server_time")]
        public long ServerTime { get; set; }

        public SessionData Session { get; set; }

        [JsonProperty("shared_secret")]
        public string SharedSecret { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("token_gid")]
        public string TokenGID { get; set; }

        [JsonProperty("uri")]
        public string URI { get; set; }

        public bool AcceptConfirmation(Confirmation conf)
        {
            return this.SendConfirmationAjax(conf, "allow");
        }

        public bool AcceptMultipleConfirmations(Confirmation[] confs)
        {
            return this.SendMultiConfirmationAjax(confs, "allow");
        }

        public bool DenyConfirmation(Confirmation conf)
        {
            return this.SendConfirmationAjax(conf, "cancel");
        }

        public bool DenyMultipleConfirmations(Confirmation[] confs)
        {
            return this.SendMultiConfirmationAjax(confs, "cancel");
        }

        public Confirmation[] FetchConfirmations()
        {
            var url = this.GenerateConfirmationURL();

            var cookies = new CookieContainer();
            this.Session.AddCookies(cookies);

            var response = SteamWeb.Request(url, "GET", string.Empty, cookies, proxy: this.Proxy);

            var confRegex = new Regex(
                "<div class=\"mobileconf_list_entry\" id=\"conf[0-9]+\" data-confid=\"(\\d+)\" data-key=\"(\\d+)\" data-type=\"(\\d+)\" data-creator=\"(\\d+)\"");

            if (response == null || !confRegex.IsMatch(response))
            {
                if (response != null && response.Contains(
                        "It looks like your Steam Guard Mobile Authenticator is providing incorrect Steam Guard codes"))
                {
                    throw new SteamException("Invalid authenticator");
                }

                if (response == null || !response.Contains("<div>Nothing to confirm</div>"))
                {
                    throw new SteamException("Error on confirmation fetch! Steam response is empty");
                }

                return new Confirmation[0];
            }

            var confirmations = confRegex.Matches(response);

            var ret = new List<Confirmation>();
            foreach (Match confirmation in confirmations)
            {
                if (confirmation.Groups.Count != 5) continue;

                if (!ulong.TryParse(confirmation.Groups[1].Value, out var confID)
                    || !ulong.TryParse(confirmation.Groups[2].Value, out var confKey)
                    || !int.TryParse(confirmation.Groups[3].Value, out var confType) || !ulong.TryParse(
                        confirmation.Groups[4].Value,
                        out var confCreator))
                {
                    continue;
                }

                ret.Add(new Confirmation(confID, confKey, confType, confCreator));
            }

            return ret.ToArray();
        }

        public string GenerateConfirmationQueryParams(string tag)
        {
            if (string.IsNullOrEmpty(this.DeviceID))
                throw new ArgumentException("Device ID is not present");

            var queryParams = this.GenerateConfirmationQueryParamsAsNVC(tag);

            return "p=" + queryParams["p"] + "&a=" + queryParams["a"] + "&k=" + queryParams["k"] + "&t="
                   + queryParams["t"] + "&m=android&tag=" + queryParams["tag"];
        }

        public NameValueCollection GenerateConfirmationQueryParamsAsNVC(string tag)
        {
            if (string.IsNullOrEmpty(this.DeviceID))
                throw new ArgumentException("Device ID is not present");

            var time = TimeAligner.GetSteamTime();

            var ret = new NameValueCollection
                          {
                              { "p", this.DeviceID },
                              { "a", this.Session.SteamID.ToString() },
                              { "k", this.GenerateConfirmationHashForTime(time, tag, this.IdentitySecret) },
                              { "t", time.ToString() },
                              { "m", "android" },
                              { "tag", tag }
                          };

            return ret;
        }

        public string GenerateConfirmationURL(string tag = "conf")
        {
            var endpoint = ApiEndpoints.CommunityBase + "/mobileconf/conf?";
            var queryString = this.GenerateConfirmationQueryParams(tag);
            return endpoint + queryString;
        }

        /// <summary>
        /// Refreshes the Steam session. Necessary to perform confirmations if your session has expired or changed.
        /// </summary>
        /// <returns></returns>
        public bool RefreshSession()
        {
            var postData = new NameValueCollection { { "access_token", this.Session.OAuthToken } };

            string response;
            try
            {
                response = SteamWeb.Request(ApiEndpoints.MobileAuthGetWgToken, "POST", postData, proxy: this.Proxy);
            }
            catch (WebException)
            {
                return false;
            }

            if (response == null) return false;

            try
            {
                var refreshResponse = JsonConvert.DeserializeObject<RefreshSessionDataResponse>(response);
                if (refreshResponse?.Response == null || string.IsNullOrEmpty(refreshResponse.Response.Token))
                    return false;

                var token = this.Session.SteamID + "%7C%7C" + refreshResponse.Response.Token;
                var tokenSecure = this.Session.SteamID + "%7C%7C" + refreshResponse.Response.TokenSecure;

                this.Session.SteamLogin = token;
                this.Session.SteamLoginSecure = tokenSecure;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GenerateConfirmationHashForTime(long time, string tag, string identitySecret)
        {
            var decode = Convert.FromBase64String(identitySecret);
            var n2 = 8;
            if (tag != null)
            {
                if (tag.Length > 32)
                {
                    n2 = 8 + 32;
                }
                else
                {
                    n2 = 8 + tag.Length;
                }
            }

            var array = new byte[n2];
            var n3 = 8;
            while (true)
            {
                var n4 = n3 - 1;
                if (n3 <= 0)
                {
                    break;
                }

                array[n4] = (byte)time;
                time >>= 8;
                n3 = n4;
            }

            if (tag != null)
            {
                Array.Copy(Encoding.UTF8.GetBytes(tag), 0, array, 8, n2 - 8);
            }

            try
            {
                var hmacGenerator = new HMACSHA1 { Key = decode };
                var hashedData = hmacGenerator.ComputeHash(array);
                var encodedData = Convert.ToBase64String(hashedData, Base64FormattingOptions.None);
                var hash = WebUtility.UrlEncode(encodedData);
                return hash;
            }
            catch
            {
                return null;
            }
        }

        private ConfirmationDetailsResponse GetConfirmationDetails(Confirmation conf)
        {
            var url = ApiEndpoints.CommunityBase + "/mobileconf/details/" + conf.ID + "?";
            var queryString = this.GenerateConfirmationQueryParams("details");
            url += queryString;

            var cookies = new CookieContainer();
            this.Session.AddCookies(cookies);
            var referer = this.GenerateConfirmationURL();

            var response = SteamWeb.Request(url, "GET", string.Empty, cookies, null, proxy: this.Proxy, referer: referer);
            if (string.IsNullOrEmpty(response)) return null;

            var confResponse = JsonConvert.DeserializeObject<ConfirmationDetailsResponse>(response);
            return confResponse;
        }

        private bool SendConfirmationAjax(Confirmation conf, string op)
        {
            var url = ApiEndpoints.CommunityBase + "/mobileconf/ajaxop";
            var queryString = "?op=" + op + "&";
            queryString += this.GenerateConfirmationQueryParams(op);
            queryString += "&cid=" + conf.ID + "&ck=" + conf.Key;
            url += queryString;

            var cookies = new CookieContainer();
            this.Session.AddCookies(cookies);
            var referer = this.GenerateConfirmationURL();

            var response = SteamWeb.Request(url, "GET", string.Empty, cookies, null, referer, proxy: this.Proxy);
            if (response == null) return false;

            var confResponse = JsonConvert.DeserializeObject<SendConfirmationResponse>(response);
            return confResponse.Success;
        }

        private bool SendMultiConfirmationAjax(Confirmation[] confs, string op)
        {
            const string Url = ApiEndpoints.CommunityBase + "/mobileconf/multiajaxop";

            var query = "op=" + op + "&" + this.GenerateConfirmationQueryParams(op);
            foreach (var conf in confs)
            {
                query += "&cid[]=" + conf.ID + "&ck[]=" + conf.Key;
            }

            var cookies = new CookieContainer();
            this.Session.AddCookies(cookies);
            var referer = this.GenerateConfirmationURL();

            var response = SteamWeb.Request(Url, "POST", query, cookies, referer: referer, proxy: this.Proxy);
            if (response == null) return false;

            var confResponse = JsonConvert.DeserializeObject<SendConfirmationResponse>(response);
            return confResponse.Success;
        }

        public class WGTokenExpiredException : Exception
        {
        }

        private class ConfirmationDetailsResponse
        {
            [JsonProperty("html")]
            public string HTML { get; set; }

            [JsonProperty("success")]
            public bool Success { get; set; }
        }

        private class RefreshSessionDataResponse
        {
            [JsonProperty("response")]
            public RefreshSessionDataInternalResponse Response { get; set; }

            internal class RefreshSessionDataInternalResponse
            {
                [JsonProperty("token")]
                public string Token { get; set; }

                [JsonProperty("token_secure")]
                public string TokenSecure { get; set; }
            }
        }

        private class RemoveAuthenticatorResponse
        {
            [JsonProperty("response")]
            public RemoveAuthenticatorInternalResponse Response { get; set; }

            internal class RemoveAuthenticatorInternalResponse
            {
                [JsonProperty("success")]
                public bool Success { get; set; }
            }
        }

        private class SendConfirmationResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }
        }
    }
}