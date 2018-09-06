using Newtonsoft.Json;

namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    public class Tag
    {
        [JsonProperty("internal_name")] public string InternalName { get; set; }

        [JsonProperty("category")] public string Category { get; set; }

        [JsonProperty("localized_tag_name")] public string LocalizedTagName { get; set; }

        [JsonProperty("localized_category_name")]
        public string LocalizedCategoryName { get; set; }
    }
}