using Market.Enums;

namespace Market.Models
{
    public class CreateBuyOrder
    {
        public ECreateBuyOrderStatus Status { get; set; }
        public long? OrderId { get; set; }
    }
}