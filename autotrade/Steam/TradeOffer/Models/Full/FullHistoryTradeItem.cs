namespace autotrade.Steam.TradeOffer.Models.Full
{
    public class FullHistoryTradeItem
    {
        public FullHistoryTradeItem(TradedAsset tradedAsset, TradedCurrency tradedCurrency,
            AssetDescription assetDescription)
        {
            Asset = tradedAsset;
            Currency = tradedCurrency;
            Description = assetDescription;
        }

        public TradedAsset Asset { get; set; }
        public TradedCurrency Currency { get; set; }
        public AssetDescription Description { get; set; }
    }
}