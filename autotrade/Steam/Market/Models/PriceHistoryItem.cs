using System;

namespace autotrade.Steam.Market.Models
{
    public class PriceHistoryItem
    {
        public DateTime DateTime { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
}