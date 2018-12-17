namespace SteamAutoMarket.Steam.Market.Models
{
    using System;

    [Serializable]
    public class MarketItemInfo
    {
        public int NameId { get; set; }

        public int PublisherFeePercent { get; set; }
    }
}