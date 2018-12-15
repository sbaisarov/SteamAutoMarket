namespace SteamAutoMarket.Steam.Market.Models.Json
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class JSuccessInt
    {
        [JsonProperty("success")]
        public int Success { get; set; }
    }
}