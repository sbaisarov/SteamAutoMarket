using Newtonsoft.Json;

namespace SteamAutoMarket.Steam.Market.Models.Json.SteamStatus
{
    public class JBaseService
    {
        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("time")] public long Time { get; set; }
    }
}