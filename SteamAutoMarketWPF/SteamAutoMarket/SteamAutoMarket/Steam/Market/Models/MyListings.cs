namespace SteamAutoMarket.Steam.Market.Models
{
    using System.Collections.Generic;

    public class MyListings
    {
        public List<MyListingsSalesItem> ConfirmationSales { get; set; }

        public int ItemsToBuy { get; set; }

        public int ItemsToConfirm { get; set; }

        public int ItemsToSell { get; set; }

        public List<MyListingsOrdersItem> Orders { get; set; }

        public List<MyListingsSalesItem> Sales { get; set; }

        public double SumOrderPricesToBuy { get; set; }

        public int Total { get; set; }
    }
}