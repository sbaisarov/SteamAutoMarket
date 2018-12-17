namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class Description
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}