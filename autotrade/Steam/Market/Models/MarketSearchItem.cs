namespace SteamAutoMarket.Steam.Market.Models
{
    public class MarketSearchItem
    {
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Game { get; set; }
        public int Quantity { get; set; }
        public double MinPrice { get; set; }
        public string HashName { get; set; }
        public int AppId { get; set; }
    }
}