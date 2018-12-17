namespace SteamAutoMarket.Steam.Auth
{
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Security.Cryptography;
    using System.Threading;

    using Newtonsoft.Json;

    /// <summary>
    /// Handles the linking process for a new mobile authenticator.
    /// </summary>
    public class AuthenticatorLinker
    {
        /// <summary>
        /// True if the authenticator has been fully finalized.
        /// </summary>
        public bool Finalized = false;

        /// <summary>
        /// Set to register a new phone number when linking. If a phone number is not set on the account, this must be set. If a phone number is set on the account, this must be null.
        /// </summary>
        public string PhoneNumber = null;

        private readonly CookieContainer _cookies;

        private readonly SessionData _session;

        public AuthenticatorLinker(SessionData session)
        {
            this._session = session;
            this.DeviceID = GenerateDeviceID();

            this._cookies = new CookieContainer();
            session.AddCookies(this._cookies);
        }

        public enum FinalizeResult
        {
            BadSMSCode,

            UnableToGenerateCorrectCodes,

            Success,

            GeneralFailure
        }

        public enum LinkResult
        {
            MustProvidePhoneNumber, // No phone number on the account

            MustRemovePhoneNumber, // A phone number is already on the account

            AwaitingFinalization, // Must provide an SMS code

            GeneralFailure, // General failure (really now!)

            AuthenticatorPresent
        }

        /// <summary>
        /// Randomly-generated device ID. Should only be generated once per linker.
        /// </summary>
        public string DeviceID { get; }

        /// <summary>
        /// After the initial link step, if successful, this will be the SteamGuard data for the account. PLEASE save this somewhere after generating it; it's vital data.
        /// </summary>
        public SteamGuardAccount LinkedAccount { get; private set; }

        public static string GenerateDeviceID()
        {
            using (var sha1 = new SHA1Managed())
            {
                var secureRandom = new RNGCryptoServiceProvider();
                var randomBytes = new byte[8];
                secureRandom.GetBytes(randomBytes);

                var hashedBytes = sha1.ComputeHash(randomBytes);
                var random32 = BitConverter.ToString(hashedBytes).Replace("-", string.Empty).Substring(0, 32).ToLower();

                return "android:" + SplitOnRatios(random32, new[] { 8, 4, 4, 4, 12 }, "-");
            }
        }

        public LinkResult AddAuthenticator()
        {
            var hasPhone = this._hasPhoneAttached();
            if (hasPhone && this.PhoneNumber != null)
                return LinkResult.MustRemovePhoneNumber;
            if (!hasPhone && this.PhoneNumber == null)
                return LinkResult.MustProvidePhoneNumber;

            if (!hasPhone)
            {
                if (!this._addPhoneNumber())
                {
                    return LinkResult.GeneralFailure;
                }
            }

            var postData = new NameValueCollection
                               {
                                   { "access_token", this._session.OAuthToken },
                                   { "steamid", this._session.SteamID.ToString() },
                                   { "authenticator_type", "1" },
                                   { "device_identifier", this.DeviceID },
                                   { "sms_phone_id", "1" }
                               };

            var response = SteamWeb.MobileLoginRequest(
                APIEndpoints.STEAMAPI_BASE + "/ITwoFactorService/AddAuthenticator/v0001",
                "POST",
                postData);
            if (response == null) return LinkResult.GeneralFailure;

            var addAuthenticatorResponse = JsonConvert.DeserializeObject<AddAuthenticatorResponse>(response);
            if (addAuthenticatorResponse == null || addAuthenticatorResponse.Response == null)
            {
                return LinkResult.GeneralFailure;
            }

            if (addAuthenticatorResponse.Response.Status == 29)
            {
                return LinkResult.AuthenticatorPresent;
            }

            if (addAuthenticatorResponse.Response.Status != 1)
            {
                return LinkResult.GeneralFailure;
            }

            this.LinkedAccount = addAuthenticatorResponse.Response;
            this.LinkedAccount.Session = this._session;
            this.LinkedAccount.DeviceID = this.DeviceID;

            return LinkResult.AwaitingFinalization;
        }

        private static string SplitOnRatios(string str, int[] ratios, string intermediate)
        {
            var result = string.Empty;

            var pos = 0;
            for (var index = 0; index < ratios.Length; index++)
            {
                result += str.Substring(pos, ratios[index]);
                pos = ratios[index];

                if (index < ratios.Length - 1)
                    result += intermediate;
            }

            return result;
        }

        private bool _addPhoneNumber()
        {
            var postData = new NameValueCollection
                               {
                                   { "op", "add_phone_number" },
                                   { "arg", this.PhoneNumber },
                                   { "sessionid", this._session.SessionID }
                               };

            var response = SteamWeb.Request(
                APIEndpoints.COMMUNITY_BASE + "/steamguard/phoneajax",
                "POST",
                postData,
                this._cookies);
            if (response == null) return false;

            var addPhoneNumberResponse = JsonConvert.DeserializeObject<AddPhoneResponse>(response);
            return addPhoneNumberResponse.Success;
        }

        private bool _checkSMSCode(string smsCode)
        {
            var postData = new NameValueCollection
                               {
                                   { "op", "check_sms_code" },
                                   { "arg", smsCode },
                                   { "checkfortos", "0" },
                                   { "skipvoip", "1" },
                                   { "sessionid", this._session.SessionID }
                               };

            var response = SteamWeb.Request(
                APIEndpoints.COMMUNITY_BASE + "/steamguard/phoneajax",
                "POST",
                postData,
                this._cookies);
            if (response == null) return false;

            var addPhoneNumberResponse = JsonConvert.DeserializeObject<AddPhoneResponse>(response);

            if (!addPhoneNumberResponse.Success)
            {
                Thread.Sleep(
                    3500); // It seems that Steam needs a few seconds to finalize the phone number on the account.
                return this._hasPhoneAttached();
            }

            return true;
        }

        private bool _hasPhoneAttached()
        {
            var postData = new NameValueCollection
                               {
                                   { "op", "has_phone" }, { "arg", "null" }, { "sessionid", this._session.SessionID }
                               };

            var response = SteamWeb.Request(
                APIEndpoints.COMMUNITY_BASE + "/steamguard/phoneajax",
                "POST",
                postData,
                this._cookies);
            if (response == null) return false;

            var hasPhoneResponse = JsonConvert.DeserializeObject<HasPhoneResponse>(response);
            return hasPhoneResponse.HasPhone;
        }

        private class AddAuthenticatorResponse
        {
            [JsonProperty("response")]
            public SteamGuardAccount Response { get; set; }
        }

        private class AddPhoneResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }
        }

        private class FinalizeAuthenticatorResponse
        {
            [JsonProperty("response")]
            public FinalizeAuthenticatorInternalResponse Response { get; set; }

            internal class FinalizeAuthenticatorInternalResponse
            {
                [JsonProperty("server_time")]
                public long ServerTime { get; set; }

                [JsonProperty("status")]
                public int Status { get; set; }

                [JsonProperty("success")]
                public bool Success { get; set; }

                [JsonProperty("want_more")]
                public bool WantMore { get; set; }
            }
        }

        private class HasPhoneResponse
        {
            [JsonProperty("has_phone")]
            public bool HasPhone { get; set; }
        }
    }
}