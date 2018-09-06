using System;

namespace SteamAutoMarket.WorkingProcess.PriceLoader
{
    internal class LoadedItemPrice
    {
        public LoadedItemPrice(DateTime parseTime, double price)
        {
            ParseTime = parseTime;
            Price = price;
        }

        public DateTime ParseTime { get; set; }
        public double Price { get; set; }
    }
}