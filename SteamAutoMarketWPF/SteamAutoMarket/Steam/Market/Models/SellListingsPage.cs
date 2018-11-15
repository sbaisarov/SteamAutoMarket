namespace Steam.Market.Models
{
    using System.Collections.Generic;

    public class SellListingsPage
    {
        public List<MyListingsSalesItem> SellListings { get; set; }

        public int TotalCount { get; set; }
    }
}