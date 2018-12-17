namespace SteamAutoMarket.Steam.TradeOffer.Models.Full
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SteamAutoMarket.Steam.TradeOffer.Enums;

    using SteamKit2;

    [Serializable]
    public class FullHistoryTradeOffer
    {
        public FullHistoryTradeOffer(TradeHistoryItem historyItem, List<AssetDescription> assetDescriptions)
        {
            this.Offer = historyItem;
            this.TradeId = historyItem.TradeId;
            this.SteamIdOther = new SteamID(ulong.Parse(historyItem.SteamIdOther));
            this.TimeInit = SteamUtils.ParseSteamUnixDate(int.Parse(historyItem.TimeInit));
            this.TimeEscrowEnd = historyItem.TimeEscrowEnd;
            this.Status = historyItem.Status;
            this.MyItems = this.GetFullHistoryTradeItemsList(
                historyItem.AssetsGiven,
                historyItem.CurrencyGiven,
                assetDescriptions);
            this.HisItems = this.GetFullHistoryTradeItemsList(
                historyItem.AssetsReceived,
                historyItem.CurrencyReceived,
                assetDescriptions);
        }

        public List<FullHistoryTradeItem> HisItems { get; }

        public List<FullHistoryTradeItem> MyItems { get; }

        public TradeHistoryItem Offer { get; }

        public TradeState Status { get; }

        public SteamID SteamIdOther { get; }

        public string TimeEscrowEnd { get; }

        public DateTime TimeInit { get; }

        public string TradeId { get; }

        private List<FullHistoryTradeItem> GetFullHistoryTradeItemsList(
            IReadOnlyCollection<TradedAsset> tradedAssets,
            IReadOnlyCollection<TradedCurrency> tradedCurrencies,
            IReadOnlyCollection<AssetDescription> assetDescriptions)
        {
            var fullItems = new List<FullHistoryTradeItem>();
            if (tradedAssets == null) return fullItems;

            foreach (var asset in tradedAssets)
            {
                var currency = tradedCurrencies?.FirstOrDefault(
                    curr => curr.ClassId == asset.ClassId && curr.ContextId == asset.ContextId);

                var description = assetDescriptions?.FirstOrDefault(
                    descr => asset.ClassId == descr.ClassId && asset.InstanceId == descr.InstanceId);

                fullItems.Add(new FullHistoryTradeItem(asset, currency, description));
            }

            return fullItems;
        }
    }
}