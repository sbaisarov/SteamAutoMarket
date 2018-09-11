using SteamAutoMarket.Steam.Market.Enums;

namespace SteamAutoMarket.Steam.Market.Models
{
    public class CreateBuyOrder
    {
        public ECreateBuyOrderStatus Status { get; set; }
        public long? OrderId { get; set; }
    }
}