namespace Market.Models
{
    public class AuthProcess
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public bool CaptchaNeeded { get; set; }

        public string CaptchaGid { get; set; }

        public string CaptchaImageUrl { get; set; }

        public bool EmailAuthNeeded { get; set; }

        public string EmailId { get; set; }

        public bool TwoFactorNeeded { get; set; }

    }
}