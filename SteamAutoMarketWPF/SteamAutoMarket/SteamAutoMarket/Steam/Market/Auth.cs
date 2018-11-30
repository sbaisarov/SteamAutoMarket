namespace SteamAutoMarket.Steam.Market
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using RestSharp;

    using SteamAutoMarket.Steam.Market.Exceptions;
    using SteamAutoMarket.Steam.Market.Models;
    using SteamAutoMarket.Steam.Market.Models.Json;

    public class Auth
    {
        private readonly SteamMarketHandler _steam;

        public Auth(SteamMarketHandler steam, CookieContainer cookies = null)
        {
            this._steam = steam;
            this.CookieContainer = cookies;
        }

        public CookieContainer CookieContainer { get; private set; }

        public bool IsAuthorized { get; set; }

        public AuthProcess Do(
            string login,
            string pass,
            string captchaGid = null,
            string captchaText = null,
            string steamGuard = null,
            string emailId = null)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass))
                throw new ArgumentException("Login or password must not be empty");

            var auth = new AuthProcess();

            var rsa = this.GetRsa(login);

            if (!rsa.Success)
            {
                auth.Message = "Failed to get RSA";
                return auth;
            }

            var encPass = this.EncryptPassword(pass, rsa.Module, rsa.Exponent);

            if (string.IsNullOrEmpty(encPass)) throw new SteamException("Failed to get encrypt password");

            var @params = new Dictionary<string, string>
                              {
                                  { "password", encPass },
                                  { "username", login },
                                  { "rsatimestamp", rsa.TimeStamp },
                                  { "remember_login", true.ToString() },
                                  { "loginfriendlyname", string.Empty },
                                  { "l", "en" }
                              };

            if (!string.IsNullOrEmpty(captchaGid) && !string.IsNullOrEmpty(captchaText))
            {
                @params.Add("captchagid", captchaGid);
                @params.Add("captcha_text", captchaText);
            }
            else
            {
                @params.Add("captchagid", "-1");
                @params.Add("captcha_text", null);
            }

            if (!string.IsNullOrEmpty(steamGuard) && !string.IsNullOrEmpty(emailId))
            {
                @params.Add("emailsteamid", emailId);
                @params.Add("emailauth", steamGuard);
            }
            else if (!string.IsNullOrEmpty(steamGuard))
            {
                @params.Add("twofactorcode", steamGuard);
            }

            var resp = this._steam.Request(Urls.LoginDo, Method.POST, Urls.Login, @params);
            var jresp = JsonConvert.DeserializeObject<JLogin>(resp.Data.Content);

            auth.Message = jresp.Message;

            if (jresp.Success)
            {
                this.IsAuthorized = true;
                this.CookieContainer = resp.CookieContainer;
                auth.Success = true;
                return auth;
            }

            if (jresp.CaptchaNeeded)
            {
                auth.CaptchaNeeded = true;
                auth.CaptchaGid = jresp.CaptchaGid;
                auth.CaptchaImageUrl = Utils.GetCaptchaImageUrl(jresp.CaptchaGid);
            }

            if (jresp.EmailAuthNeeded)
            {
                auth.EmailAuthNeeded = true;
                auth.EmailId = jresp.EmailId;
            }
            else if (jresp.RequiresTwoFactor)
            {
                auth.TwoFactorNeeded = true;
            }

            return auth;
        }

        public bool Do(CookieContainer cookieContainer)
        {
            var resp = this._steam.Request(Urls.SteamCommunity, Method.GET, Urls.Login, null, false, cookieContainer);

            var cookies = resp.CookieContainer.GetCookies(new Uri(Urls.SteamCommunity));

            if (cookies["steamLogin"] != null && !cookies["steamLogin"].Value.Equals("deleted"))
            {
                this.IsAuthorized = true;
                this.CookieContainer = resp.CookieContainer;
                return true;
            }

            return false;
        }

        public Task<AuthProcess> DoAsync(
            string login,
            string pass,
            string captchaGid = null,
            string captchaText = null,
            string steamGuard = null,
            string emailId = null)
        {
            var result =
                new Task<AuthProcess>(() => this.Do(login, pass, captchaGid, captchaText, steamGuard, emailId));
            result.Start();
            return result;
        }

        public JRsa GetRsa(string login)
        {
            if (string.IsNullOrEmpty(login)) throw new ArgumentException("Login must not be empty");
            var data = new Dictionary<string, string> { { "username", login } };
            var resp = this._steam.Request(Urls.LoginRsa, Method.POST, Urls.SteamCommunity, data);
            return JsonConvert.DeserializeObject<JRsa>(resp.Data.Content);
        }

        public void Logout()
        {
            var sessionId = this.SessionId();
            var data = new Dictionary<string, string> { { "sessionid", sessionId } };

            this._steam.Request(Urls.Logout, Method.POST, Urls.Market, data, true);

            this.ResetAuth();
        }

        public void ResetAuth()
        {
            this.CookieContainer = new CookieContainer();
            this.IsAuthorized = false;
        }

        public string SessionId()
        {
            var cookies = this.CookieContainer.GetCookies(new Uri(Urls.SteamCommunity));

            var id = (from Cookie cook in cookies where cook.Name == "sessionid" select cook.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(id)) throw new SteamException("Cannot get session ID from cookies");

            return id;
        }

        private string EncryptPassword(string password, string modval, string expval)
        {
            var rsa = new RSACryptoServiceProvider();
            var rsaParams = new RSAParameters
                                {
                                    Modulus = Utils.StringToByteArray(modval),
                                    Exponent = Utils.StringToByteArray(expval)
                                };

            rsa.ImportParameters(rsaParams);
            var encodedPass = rsa.Encrypt(Encoding.ASCII.GetBytes(password), false);

            return Convert.ToBase64String(encodedPass);
        }
    }
}