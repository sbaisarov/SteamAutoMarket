using Newtonsoft.Json;

namespace autotrade.Steam.Market.Models.Json
{
    public class JMarketAppFilterTag
    {
        [JsonProperty("localized_name")] public string LocalizedName { get; set; }
    }
}