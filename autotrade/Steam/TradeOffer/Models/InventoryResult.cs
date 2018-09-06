using Newtonsoft.Json;

namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    public class InventoryResult
    {
        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("num_backpack_slots")] public uint NumBackpackSlots { get; set; }

        [JsonProperty("items")] public Item[] Items { get; set; }
    }
}