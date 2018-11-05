namespace Steam.Market.Models
{
    using System.Collections.Generic;

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