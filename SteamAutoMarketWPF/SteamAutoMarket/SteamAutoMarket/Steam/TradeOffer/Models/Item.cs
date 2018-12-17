namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class Item
    {
        [JsonProperty("attributes")]
        public ItemAttribute[] Attributes { get; set; }

        [JsonProperty("contained_item")]
        public Item ContainedItem { get; set; }

        [JsonProperty("custom_desc")]
        public string CustomDescription { get; set; }

        [JsonProperty("custom_name")]
        public string CustomName { get; set; }

        [JsonProperty("defindex")]
        public ushort DefIndex { get; set; }

        [JsonProperty("id")]
        public ulong Id { get; set; }

        [JsonProperty("flag_cannot_craft")]
        public bool IsNotCraftable { get; set; }

        [JsonProperty("flag_cannot_trade")]
        public bool IsNotTradeable { get; set; }

        [JsonProperty("level")]
        public byte Level { get; set; }

        [JsonProperty("origin")]
        public int Origin { get; set; }

        [JsonProperty("original_id")]
        public ulong OriginalId { get; set; }

        [JsonProperty("quality")]
        public int Quality { get; set; }

        [JsonProperty("quantity")]
        public int RemainingUses { get; set; }
    }
}