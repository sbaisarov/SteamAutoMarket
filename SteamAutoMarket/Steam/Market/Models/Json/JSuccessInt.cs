using Newtonsoft.Json;

namespace SteamAutoMarket.Steam.Market.Models.Json
{
    public class JSuccessInt
    {
        [JsonProperty("success")] public int Success { get; set; }
    }
}