using Newtonsoft.Json;

namespace autotrade.Steam.Market.Models.Json
{
    public class JSuccess
    {
        [JsonProperty("success")] public bool Success { get; set; }
    }
}