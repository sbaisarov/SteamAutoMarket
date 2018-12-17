namespace SteamAutoMarket.UI.Repository.PriceCache
{
    using System;

    [Serializable]
    public class PriceCacheModel
    {
        public string MarketHashName { get; set; }

        public DateTime ParseTime { get; set; }

        public double Price { get; set; }
    }
}