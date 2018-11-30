namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using Newtonsoft.Json;

    public class AppData
    {
        [JsonProperty("def_index")]
        public int DefIndex { get; set; }

        [JsonProperty("is_itemset_name")]
        public int? IsItemsetName { get; set; }

        [JsonProperty("limited")]
        public int Limited { get; set; }
    }
}