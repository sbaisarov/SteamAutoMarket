using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class OwnerAction
    {
        [JsonProperty("link")] public string Link { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }
}