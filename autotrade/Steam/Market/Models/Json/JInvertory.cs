using System.Collections.Generic;
using Newtonsoft.Json;

namespace autotrade.Steam.Market.Models.Json
{
    public class JInvertory : JSuccessInt
    {
        [JsonProperty("total_inventory_count")]
        public int TotalInventoryCount { get; set; }

        [JsonProperty("assets")] public List<JInvertoryAsset> Assets { get; set; }

        [JsonProperty("descriptions")] public List<JDescription> Descriptions { get; set; }
    }
}