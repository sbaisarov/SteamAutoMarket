using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class Description
    {
        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("value")] public string Value { get; set; }
    }
}