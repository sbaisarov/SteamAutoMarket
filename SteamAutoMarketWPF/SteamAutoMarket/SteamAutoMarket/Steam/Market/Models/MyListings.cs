namespace SteamAutoMarket.Steam.Market.Models
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class MyListings
    {
        public List<MyListingsSalesItem> ConfirmationSales { get; set; } = new List<MyListingsSalesItem>();

        public int ItemsToBuy { get; set; }

        public int ItemsToConfirm { get; set; }

        public int ItemsToSell { get; set; }

        public List<MyListingsOrdersItem> Orders { get; set; } = new List<MyListingsOrdersItem>();

        public List<MyListingsSalesItem> Sales { get; set; } = new List<MyListingsSalesItem>();

        public double SumOrderPricesToBuy { get; set; }

        public int Total { get; set; }
    }
}