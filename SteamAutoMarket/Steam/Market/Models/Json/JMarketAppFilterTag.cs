using Newtonsoft.Json;

namespace SteamAutoMarket.Steam.Market.Models.Json
{
    public class JMarketAppFilterTag
    {
        [JsonProperty("localized_name")] public string LocalizedName { get; set; }
    }
}