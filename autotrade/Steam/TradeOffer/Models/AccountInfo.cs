using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class AccountInfo
    {
        [JsonProperty("steamid")] public ulong SteamId { get; set; }

        [JsonProperty("personaname")] public string PersonaName { get; set; }
    }
}