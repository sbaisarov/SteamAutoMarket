using Newtonsoft.Json;

namespace SteamAutoMarket.Steam.Market.Models.Json
{
    public class JSuccess
    {
        [JsonProperty("success")] public bool Success { get; set; }
    }
}