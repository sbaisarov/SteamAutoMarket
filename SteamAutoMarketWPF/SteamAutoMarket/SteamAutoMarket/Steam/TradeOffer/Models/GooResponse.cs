namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using Newtonsoft.Json;

    public class GooResponse
    {
        [JsonProperty("success")]
        public int Success { get; set; }

        [JsonProperty("goo_value")]
        public string GooValue { get; set; }
    }
}
