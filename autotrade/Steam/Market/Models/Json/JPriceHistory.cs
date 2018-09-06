using Newtonsoft.Json;

namespace SteamAutoMarket.Steam.Market.Models.Json
{
    public class JPriceHistory : JSuccess
    {
        [JsonProperty("prices")] public dynamic Prices { get; set; }
    }
}