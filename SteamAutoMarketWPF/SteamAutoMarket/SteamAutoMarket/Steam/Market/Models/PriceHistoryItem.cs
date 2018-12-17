namespace SteamAutoMarket.Steam.Market.Models
{
    using System;

    [Serializable]
    public class PriceHistoryItem
    {
        public int Count { get; set; }

        public DateTime DateTime { get; set; }

        public double Price { get; set; }
    }
}