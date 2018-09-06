using Newtonsoft.Json;

namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    public class OwnerAction
    {
        [JsonProperty("link")] public string Link { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }
}