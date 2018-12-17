namespace SteamAutoMarket.Steam.Market.Models
{
    using System;

    [Serializable]
    public class MarketInfoCacheModel
    {
        public MarketInfoCacheModel()
        {
        }

        public MarketInfoCacheModel(string appIdHashName, MarketItemInfo info)
        {
            this.AppIdHashName = appIdHashName;
            this.NameId = info.NameId;
            this.PublisherFeePercent = info.PublisherFeePercent;
        }

        public string AppIdHashName { get; set; }

        public int NameId { get; set; }

        public int PublisherFeePercent { get; set; }
    }
}