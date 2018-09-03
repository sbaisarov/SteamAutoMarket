using System.Collections.Generic;
using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class RgDescription
    {
        [JsonProperty("appid")] public int Appid { get; set; }

        [JsonProperty("classid")] public string Classid { get; set; }

        [JsonProperty("instanceid")] public string InstanceId { get; set; }

        [JsonProperty("icon_url")] public string IconUrl { get; set; }

        [JsonProperty("icon_url_large")] public string IconUrlLarge { get; set; }

        [JsonProperty("icon_drag_url")] public string IconDragUrl { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("market_hash_name")] public string MarketHashName { get; set; }

        [JsonProperty("market_name")] public string MarketName { get; set; }

        [JsonProperty("name_color")] public string NameColor { get; set; }

        [JsonProperty("background_color")] public string BackgroundColor { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("tradable")] public bool IsTradable { get; set; }

        [JsonProperty("commodity")] public bool IsCommodity { get; set; }

        [JsonProperty("marketable")] public bool IsMarketable { get; set; }

        [JsonProperty("market_tradable_restriction")]
        public int MarketTradableRestriction { get; set; }

        [JsonProperty("descriptions")] public List<Description> Descriptions { get; set; }

        [JsonProperty("tags")] public List<Tag> Tags { get; set; }
    }
}