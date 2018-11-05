namespace Steam.Market.Models
{
    public class MyListingsOrdersItem
    {
        public int AppId { get; set; }

        public string Date { get; set; }

        public string Game { get; set; }

        public string HashName { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public long OrderId { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public string Url { get; set; }
    }
}