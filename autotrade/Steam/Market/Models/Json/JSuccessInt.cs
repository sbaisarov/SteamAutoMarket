using Newtonsoft.Json;

namespace autotrade.Steam.Market.Models.Json
{
    public class JSuccessInt
    {
        [JsonProperty("success")] public int Success { get; set; }
    }
}