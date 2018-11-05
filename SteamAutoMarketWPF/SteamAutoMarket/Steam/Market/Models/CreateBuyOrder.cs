namespace Steam.Market.Models
{
    using Steam.Market.Enums;

    public class CreateBuyOrder
    {
        public long? OrderId { get; set; }

        public ECreateBuyOrderStatus Status { get; set; }
    }
}