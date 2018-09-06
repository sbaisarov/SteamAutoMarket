using Newtonsoft.Json;

namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    public class TradedAsset
    {
        [JsonProperty("appid")] public int Appid { get; set; }

        [JsonProperty("contextid")] public string ContextId { get; set; }

        [JsonProperty("assetid")] public string AssetId { get; set; }

        [JsonProperty("classid")] public string ClassId { get; set; }

        [JsonProperty("instanceid")] public string InstanceId { get; set; }

        [JsonProperty("new_assetid")] public string NewAssetId { get; set; }

        [JsonProperty("new_contextid")] public string NewContextId { get; set; }

        [JsonProperty("rollback_new_assetid")] public string RollbackNewAssetId { get; set; }

        [JsonProperty("rollback_new_contextid")]
        public string RollbackNewContextId { get; set; }

        [JsonProperty("amount")] public string Amount { get; set; }
    }
}