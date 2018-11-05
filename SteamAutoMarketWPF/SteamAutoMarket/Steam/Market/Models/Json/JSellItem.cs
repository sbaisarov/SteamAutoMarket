namespace Steam.Market.Models.Json
{
    using Newtonsoft.Json;

    public class JSellItem : JSuccess
    {
        [JsonProperty("email_domain")]
        public string EmailDomain { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Include)]
        public string Message { get; set; }

        [JsonProperty("needs_email_confirmation")]
        public bool NeedsEmailConfirmation { get; set; }

        [JsonProperty("needs_mobile_confirmation")]
        public bool NeedsMobileConfirmation { get; set; }

        [JsonProperty("requires_confirmation")]
        public int RequiresConfirmation { get; set; }
    }
}