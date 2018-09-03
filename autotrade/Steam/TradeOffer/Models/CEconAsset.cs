using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class CEconAsset
    {
        [JsonProperty("appid")] public string AppId { get; set; }

        [JsonProperty("contextid")] public string ContextId { get; set; }

        [JsonProperty("assetid")] public string AssetId { get; set; }

        [JsonProperty("classid")] public string ClassId { get; set; }

        [JsonProperty("instanceid")] public string InstanceId { get; set; }

        [JsonProperty("amount")] public string Amount { get; set; }

        [JsonProperty("missing")] public bool IsMissing { get; set; }
    }
}