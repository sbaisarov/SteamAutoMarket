namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using Newtonsoft.Json;

    public class InventoryResult
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }

        [JsonProperty("num_backpack_slots")]
        public uint NumBackpackSlots { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}