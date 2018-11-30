namespace SteamAutoMarket.Steam.Market.Models
{
    using SteamAutoMarket.Steam.Market.Enums;

    public class CreateBuyOrder
    {
        public long? OrderId { get; set; }

        public ECreateBuyOrderStatus Status { get; set; }
    }
}