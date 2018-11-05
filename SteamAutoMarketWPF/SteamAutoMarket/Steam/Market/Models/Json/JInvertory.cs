namespace Steam.Market.Models.Json
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class JInvertory : JSuccessInt
    {
        [JsonProperty("assets")]
        public List<JInvertoryAsset> Assets { get; set; }

        [JsonProperty("descriptions")]
        public List<JDescription> Descriptions { get; set; }

        [JsonProperty("total_inventory_count")]
        public int TotalInventoryCount { get; set; }
    }
}