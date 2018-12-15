namespace SteamAutoMarket.Steam.Market.Models.Json
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class JInvertoryAsset
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("appid")]
        public int AppId { get; set; }

        [JsonProperty("assetid")]
        public long AssetId { get; set; }

        [JsonProperty("classid")]
        public long ClassId { get; set; }

        [JsonProperty("contextid")]
        public int ContextId { get; set; }

        [JsonProperty("instanceid")]
        public long InstanceId { get; set; }
    }
}