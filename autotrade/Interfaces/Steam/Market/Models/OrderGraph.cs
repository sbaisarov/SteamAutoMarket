using System.Collections.Generic;

namespace Market.Models
{
    public class OrderGraph
    {
        public int Total { get; set; }
        public List<OrderGraphItem> Orders { get; set; } 
    }
}