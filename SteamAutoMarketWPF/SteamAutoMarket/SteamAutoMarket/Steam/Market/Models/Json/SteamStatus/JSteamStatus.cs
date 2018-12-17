namespace SteamAutoMarket.Steam.Market.Models.Json.SteamStatus
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class JSteamStatus
    {
        [JsonProperty("online")]
        public double Online { get; set; }

        [JsonProperty("online_info")]
        public string OnlineInfo { get; set; }

        [JsonProperty("services")]
        public JServices Services { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }
    }
}