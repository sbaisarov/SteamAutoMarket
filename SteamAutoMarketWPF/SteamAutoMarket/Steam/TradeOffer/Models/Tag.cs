namespace Steam.TradeOffer.Models
{
    using Newtonsoft.Json;

    public class Tag
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("internal_name")]
        public string InternalName { get; set; }

        [JsonProperty("localized_category_name")]
        public string LocalizedCategoryName { get; set; }

        [JsonProperty("localized_tag_name")]
        public string LocalizedTagName { get; set; }
    }
}