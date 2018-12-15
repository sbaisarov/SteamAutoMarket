namespace SteamAutoMarket.Steam.Market.Models
{
    using System;

    [Serializable]
    public class ItemOrdersHistogram
    {
        public OrderGraph BuyOrderGraph { get; set; }

        public double? HighBuyOrder { get; set; }

        public double? MinSellPrice { get; set; }

        public OrderGraph SellOrderGraph { get; set; }
    }
}