namespace Steam.TradeOffer.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    public class InventoryRootModel
    {
        [JsonProperty("assets")]
        public List<RgInventory> Assets { get; set; }

        [JsonProperty("descriptions")]
        public List<RgDescription> Descriptions { get; set; }

        [JsonProperty("last_assetid")]
        public string LastAssetid { get; set; }

        [JsonProperty("more_items")]
        public int MoreItems { get; set; }

        [JsonProperty("rwgrsn")]
        public int Rwgrsn { get; set; }

        [JsonProperty("success")]
        public int Success { get; set; }

        [JsonProperty("total_inventory_count")]
        public int TotalInventoryCount { get; set; }

        public static RgDescription GetDescription(RgInventory asset, List<RgDescription> descriptions)
        {
            return descriptions.FirstOrDefault(
                item => asset.Instanceid == item.InstanceId && asset.Classid == item.Classid);
        }
    }
}