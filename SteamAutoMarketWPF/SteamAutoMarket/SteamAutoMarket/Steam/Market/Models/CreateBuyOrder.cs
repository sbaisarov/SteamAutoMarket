namespace SteamAutoMarket.Steam.Market.Models
{
    using System;

    using SteamAutoMarket.Steam.Market.Enums;

    [Serializable]
    public class CreateBuyOrder
    {
        public long? OrderId { get; set; }

        public ECreateBuyOrderStatus Status { get; set; }
    }
}