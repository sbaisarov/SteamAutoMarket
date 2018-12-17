namespace SteamAutoMarket.Steam.Market.Models
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class OrderGraph
    {
        public List<OrderGraphItem> Orders { get; set; }

        public int Total { get; set; }
    }
}