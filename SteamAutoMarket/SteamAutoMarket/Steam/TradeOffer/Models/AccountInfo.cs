using Newtonsoft.Json;

namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    public class AccountInfo
    {
        [JsonProperty("steamid")] public ulong SteamId { get; set; }

        [JsonProperty("personaname")] public string PersonaName { get; set; }
    }
}