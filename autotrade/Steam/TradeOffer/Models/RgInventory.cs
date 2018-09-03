using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class RgInventory
    {
        [JsonProperty("appid")] public int Appid { get; set; }

        [JsonProperty("classid")] public string Classid { get; set; }

        [JsonProperty("instanceid")] public string Instanceid { get; set; }

        [JsonProperty("amount")] public string Amount { get; set; }

        [JsonProperty("contextid")] public string Contextid { get; set; }

        [JsonProperty("assetid")] public string Assetid { get; set; }
    }
}