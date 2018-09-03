using System.Collections.Generic;
using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class AssetDescription
    {
        [JsonProperty("appid")] public int AppId { get; set; }

        [JsonProperty("classid")] public string ClassId { get; set; }

        [JsonProperty("instanceid")] public string InstanceId { get; set; }

        [JsonProperty("currency")] public bool IsCurrency { get; set; }

        [JsonProperty("background_color")] public string BackgroundColor { get; set; }

        [JsonProperty("icon_url")] public string IconUrl { get; set; }

        [JsonProperty("icon_url_large")] public string IconUrlLarge { get; set; }

        [JsonProperty("descriptions")] public List<Description> Descriptions { get; set; }

        [JsonProperty("tradable")] public bool IsTradable { get; set; }

        [JsonProperty("owner_actions")] public List<OwnerAction> OwnerActions { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("name_color")] public string NameColor { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("market_name")] public string MarketName { get; set; }

        [JsonProperty("market_hash_name")] public string MarketHashName { get; set; }
    }
}