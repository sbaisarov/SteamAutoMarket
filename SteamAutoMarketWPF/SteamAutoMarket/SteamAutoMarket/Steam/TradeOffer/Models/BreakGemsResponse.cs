namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using Newtonsoft.Json;

    public class BreakGemsResponse
    {
        [JsonProperty("success")]
        public int Success { get; set; }

        [JsonProperty("strHTML")]
        public string StrHtml { get; set; }

        [JsonProperty("goo_value_received ")]
        public string GooValueReceived { get; set; }

        [JsonProperty("goo_value_total")]
        public string GooValueTotal { get; set; }
    }
}
