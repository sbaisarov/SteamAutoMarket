namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;

    public class GemsBreakModel
    {
        public string ItemName { get; set; }
        public int? GemsCount { get; set; }
        public FullRgItem ItemModel { get; set; }
        public int Count { get; set; }
        public FullRgItem[] ItemsList { get; set; }
    }
}
