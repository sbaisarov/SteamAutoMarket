
namespace Market.Models
{
    public class ItemOrdersHistogram
    {
        public double? MinSellPrice { get; set; }
        public double? HighBuyOrder { get; set; }

        public OrderGraph SellOrderGraph { get; set; }
        public OrderGraph BuyOrderGraph { get; set; }
    }
}