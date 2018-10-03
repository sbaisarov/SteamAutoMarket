namespace SteamAutoMarket.Steam.Market.Models
{
    public class MyListingsOrdersItem
    {
        public long OrderId { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string HashName { get; set; }
        public string Date { get; set; }
        public int AppId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Game { get; set; }
    }
}