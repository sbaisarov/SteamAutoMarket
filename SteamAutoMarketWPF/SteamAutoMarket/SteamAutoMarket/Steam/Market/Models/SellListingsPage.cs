namespace SteamAutoMarket.Steam.Market.Models
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class SellListingsPage
    {
        public List<MyListingsSalesItem> SellListings { get; set; }

        public int TotalCount { get; set; }
    }
}