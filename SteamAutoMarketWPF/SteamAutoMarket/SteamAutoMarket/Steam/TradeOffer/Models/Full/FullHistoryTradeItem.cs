namespace SteamAutoMarket.Steam.TradeOffer.Models.Full
{
    using System;

    [Serializable]
    public class FullHistoryTradeItem
    {
        public FullHistoryTradeItem(
            TradedAsset tradedAsset,
            TradedCurrency tradedCurrency,
            AssetDescription assetDescription)
        {
            this.Asset = tradedAsset;
            this.Currency = tradedCurrency;
            this.Description = assetDescription;
        }

        public TradedAsset Asset { get; set; }

        public TradedCurrency Currency { get; set; }

        public AssetDescription Description { get; set; }
    }
}