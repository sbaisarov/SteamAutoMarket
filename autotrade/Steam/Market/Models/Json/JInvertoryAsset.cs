using Newtonsoft.Json;

namespace autotrade.Steam.Market.Models.Json
{
    public class JInvertoryAsset
    {
        [JsonProperty("assetid")] public long AssetId { get; set; }

        [JsonProperty("appid")] public int AppId { get; set; }

        [JsonProperty("contextid")] public int ContextId { get; set; }

        [JsonProperty("classid")] public long ClassId { get; set; }

        [JsonProperty("instanceid")] public long InstanceId { get; set; }

        [JsonProperty("amount")] public int Amount { get; set; }
    }
}