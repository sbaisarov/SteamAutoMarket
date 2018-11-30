namespace SteamAutoMarket.Steam.Market.Models.Json
{
    using Newtonsoft.Json;

    public class JLogin : JSuccess
    {
        [JsonProperty("bad_captcha")]
        public bool BadCaptcha { get; set; }

        [JsonProperty("captcha_gid")]
        public string CaptchaGid { get; set; }

        [JsonProperty("captcha_needed")]
        public bool CaptchaNeeded { get; set; }

        [JsonProperty("emailauth_needed")]
        public bool EmailAuthNeeded { get; set; }

        [JsonProperty("emailsteamid")]
        public string EmailId { get; set; }

        [JsonProperty("login_complete")]
        public bool LoginComplete { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("requires_twofactor")]
        public bool RequiresTwoFactor { get; set; }
    }
}