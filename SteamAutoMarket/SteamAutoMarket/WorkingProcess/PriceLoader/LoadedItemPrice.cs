namespace SteamAutoMarket.WorkingProcess.PriceLoader
{
    using System;

    internal class LoadedItemPrice
    {
        public LoadedItemPrice(DateTime parseTime, double price)
        {
            this.ParseTime = parseTime;
            this.Price = price;
        }

        public DateTime ParseTime { get; set; }

        public double Price { get; set; }
    }
}