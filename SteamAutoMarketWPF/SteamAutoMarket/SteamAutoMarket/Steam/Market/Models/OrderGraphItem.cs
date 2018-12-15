namespace SteamAutoMarket.Steam.Market.Models
{
    using System;

    [Serializable]
    public class OrderGraphItem
    {
        public int Count { get; set; }

        public double Price { get; set; }

        public string Title { get; set; }
    }
}