namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class AccountInfo
    {
        [JsonProperty("personaname")]
        public string PersonaName { get; set; }

        [JsonProperty("steamid")]
        public ulong SteamId { get; set; }
    }
}