using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class AppData
    {
        [JsonProperty("limited")] public int Limited { get; set; }

        [JsonProperty("def_index")] public int DefIndex { get; set; }

        [JsonProperty("is_itemset_name")] public int? IsItemsetName { get; set; }
    }
}