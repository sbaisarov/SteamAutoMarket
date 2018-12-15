namespace SteamAutoMarket.Steam.Market.Models
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class MarketSearch
    {
        public MarketSearch()
        {
            this.Items = new List<MarketSearchItem>();
            this.TotalCount = 0;
        }

        public List<MarketSearchItem> Items { get; set; }

        public int TotalCount { get; set; }
    }
}