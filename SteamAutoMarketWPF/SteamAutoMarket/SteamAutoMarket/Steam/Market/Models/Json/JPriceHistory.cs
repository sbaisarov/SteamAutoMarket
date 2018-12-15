namespace SteamAutoMarket.Steam.Market.Models.Json
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class JPriceHistory : JSuccess
    {
        [JsonProperty("prices")]
        public dynamic Prices { get; set; }
    }
}