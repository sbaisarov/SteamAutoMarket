namespace SteamAutoMarket.Models
{
    using System;

    public class CachedPriceModel
    {
        public CachedPriceModel(DateTime parseTime, double price)
        {
            this.ParseTime = parseTime;
            this.Price = price;
        }

        public DateTime ParseTime { get; set; }

        public double Price { get; set; }
    }
}