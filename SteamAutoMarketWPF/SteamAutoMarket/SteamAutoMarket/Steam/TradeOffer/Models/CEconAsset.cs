namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class CEconAsset
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("appid")]
        public string AppId { get; set; }

        [JsonProperty("assetid")]
        public string AssetId { get; set; }

        [JsonProperty("classid")]
        public string ClassId { get; set; }

        [JsonProperty("contextid")]
        public string ContextId { get; set; }

        [JsonProperty("instanceid")]
        public string InstanceId { get; set; }

        [JsonProperty("missing")]
        public bool IsMissing { get; set; }
    }
}