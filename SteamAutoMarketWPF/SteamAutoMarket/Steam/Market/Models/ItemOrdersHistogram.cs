namespace Steam.Market.Models
{
    public class ItemOrdersHistogram
    {
        public OrderGraph BuyOrderGraph { get; set; }

        public double? HighBuyOrder { get; set; }

        public double? MinSellPrice { get; set; }

        public OrderGraph SellOrderGraph { get; set; }
    }
}