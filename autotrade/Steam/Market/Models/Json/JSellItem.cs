using Newtonsoft.Json;

namespace SteamAutoMarket.Steam.Market.Models.Json
{
    public class JSellItem : JSuccess
    {
        [JsonProperty("needs_email_confirmation")]
        public bool NeedsEmailConfirmation { get; set; }

        [JsonProperty("needs_mobile_confirmation")]
        public bool NeedsMobileConfirmation { get; set; }

        [JsonProperty("requires_confirmation")]
        public int RequiresConfirmation { get; set; }

        [JsonProperty("email_domain")] public string EmailDomain { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Include)]
        public string Message { get; set; }
    }
}