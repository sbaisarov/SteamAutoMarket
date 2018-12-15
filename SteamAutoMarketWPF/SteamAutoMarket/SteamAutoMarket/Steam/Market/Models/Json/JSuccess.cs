namespace SteamAutoMarket.Steam.Market.Models.Json
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class JSuccess
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}