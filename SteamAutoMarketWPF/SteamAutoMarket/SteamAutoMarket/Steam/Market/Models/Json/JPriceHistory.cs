namespace SteamAutoMarket.Steam.Market.Models.Json
{
    using Newtonsoft.Json;

    public class JPriceHistory : JSuccess
    {
        [JsonProperty("prices")]
        public dynamic Prices { get; set; }
    }
}