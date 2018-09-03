using System.Collections.Generic;

namespace autotrade.Steam.Market.Models
{
    public class MarketSearch
    {
        public MarketSearch()
        {
            Items = new List<MarketSearchItem>();
            TotalCount = 0;
        }

        public int TotalCount { get; set; }
        public List<MarketSearchItem> Items { get; set; }
    }
}