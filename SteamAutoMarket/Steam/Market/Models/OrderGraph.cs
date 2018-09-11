using System.Collections.Generic;

namespace SteamAutoMarket.Steam.Market.Models
{
    public class OrderGraph
    {
        public int Total { get; set; }
        public List<OrderGraphItem> Orders { get; set; }
    }
}