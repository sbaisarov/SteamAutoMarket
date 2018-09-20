using System.Collections.Generic;

namespace SteamAutoMarket.Steam.Market.Models
{
    public class MyListings
    {
        public int ItemsToBuy { get; set; }
        public int ItemsToSell { get; set; }
        public int ItemsToConfirm { get; set; }
        public double SumOrderPricesToBuy { get; set; }
        public List<MyListingsOrdersItem> Orders { get; set; }
        public List<MyListingsSalesItem> Sales { get; set; }
    }
}