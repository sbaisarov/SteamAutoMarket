namespace Steam.Market.Models
{
    using System.Collections.Generic;

    public class OrderGraph
    {
        public List<OrderGraphItem> Orders { get; set; }

        public int Total { get; set; }
    }
}