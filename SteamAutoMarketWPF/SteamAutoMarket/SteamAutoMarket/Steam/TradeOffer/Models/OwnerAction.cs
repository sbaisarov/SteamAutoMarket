namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class OwnerAction
    {
        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}