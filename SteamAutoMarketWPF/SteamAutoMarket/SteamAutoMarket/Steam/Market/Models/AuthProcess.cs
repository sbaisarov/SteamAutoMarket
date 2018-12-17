namespace SteamAutoMarket.Steam.Market.Models
{
    using System;

    [Serializable]
    public class AuthProcess
    {
        public string CaptchaGid { get; set; }

        public string CaptchaImageUrl { get; set; }

        public bool CaptchaNeeded { get; set; }

        public bool EmailAuthNeeded { get; set; }

        public string EmailId { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }

        public bool TwoFactorNeeded { get; set; }
    }
}