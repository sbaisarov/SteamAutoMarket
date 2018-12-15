namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
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