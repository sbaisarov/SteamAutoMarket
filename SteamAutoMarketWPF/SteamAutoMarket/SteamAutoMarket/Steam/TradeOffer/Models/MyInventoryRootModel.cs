namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;
    
    public class MyInventoryRootModel
    {
        [JsonProperty("rgInventory")]
        public IDictionary<string, MyRgInventory> Assets { get; set; }

        [JsonProperty("rgDescriptions")]
        public IDictionary<string, RgDescription> Descriptions { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
        
        [JsonProperty("more")]
        public bool More { get; set; }
        
        [JsonProperty("more_start")]
        public bool MoreStart { get; set; }

        public static RgDescription GetDescription(RgInventory asset, List<RgDescription> descriptions)
        {
            return descriptions.FirstOrDefault(
                item => asset.Instanceid == item.InstanceId && asset.Classid == item.Classid);
        }
    }
}