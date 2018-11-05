namespace Steam.TradeOffer.Models
{
    using Newtonsoft.Json;

    public class AccountInfo
    {
        [JsonProperty("personaname")]
        public string PersonaName { get; set; }

        [JsonProperty("steamid")]
        public ulong SteamId { get; set; }
    }
}