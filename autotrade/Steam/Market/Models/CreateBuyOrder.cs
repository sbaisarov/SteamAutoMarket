using autotrade.Steam.Market.Enums;

namespace autotrade.Steam.Market.Models
{
    public class CreateBuyOrder
    {
        public ECreateBuyOrderStatus Status { get; set; }
        public long? OrderId { get; set; }
    }
}