namespace Steam.TradeOffer.Models.Full
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Steam.TradeOffer.Enums;

    using SteamKit2;

    public class FullHistoryTradeOffer
    {
        public FullHistoryTradeOffer(TradeHistoryItem historyItem, List<AssetDescription> assetDescriptions)
        {
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

        public List<FullHistoryTradeItem> HisItems { get; set; }

        public List<FullHistoryTradeItem> MyItems { get; set; }

        public TradeState Status { get; set; }

        public SteamID SteamIdOther { get; set; }

        public string TimeEscrowEnd { get; set; }

        public DateTime TimeInit { get; set; }

        public string TradeId { get; set; }

        private List<FullHistoryTradeItem> GetFullHistoryTradeItemsList(
            List<TradedAsset> tradedAssets,
            List<TradedCurrency> tradedCurrencies,
            List<AssetDescription> assetDescriptions)
        {
            var fullItems = new List<FullHistoryTradeItem>();
            if (tradedAssets == null) return fullItems;

            foreach (var asset in tradedAssets)
            {
                TradedCurrency currency = null;
                if (tradedCurrencies != null)
                    tradedCurrencies.FirstOrDefault(
                        curr => curr.ClassId == asset.ClassId && curr.ContextId == asset.ContextId);

                var description = assetDescriptions.FirstOrDefault(
                    descr => asset.ClassId == descr.ClassId && asset.InstanceId == descr.InstanceId);

                fullItems.Add(new FullHistoryTradeItem(asset, currency, description));
            }

            return fullItems;
        }
    }
}