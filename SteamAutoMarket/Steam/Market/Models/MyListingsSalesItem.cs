namespace SteamAutoMarket.Steam.Market.Models
{
    public class MyListingsSalesItem
    {
        public long SaleId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string HashName { get; set; }
        public string Date { get; set; }
        public int AppId { get; set; }
        public double Price { get; set; }
    }
}