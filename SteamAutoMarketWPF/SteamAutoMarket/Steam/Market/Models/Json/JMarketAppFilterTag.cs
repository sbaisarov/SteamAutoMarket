namespace Steam.Market.Models.Json
{
    using Newtonsoft.Json;

    public class JMarketAppFilterTag
    {
        [JsonProperty("localized_name")]
        public string LocalizedName { get; set; }
    }
}