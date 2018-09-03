using System;
using System.Collections.Generic;
using System.Linq;
using autotrade.CustomElements.Utils;
using autotrade.Steam.TradeOffer.Enums;
using SteamKit2;

namespace autotrade.Steam.TradeOffer.Models.Full
{
    public class FullHistoryTradeOffer
    {
        public FullHistoryTradeOffer(TradeHistoryItem historyItem, List<AssetDescription> assetDescriptions)
        {
            TradeId = historyItem.TradeId;
            SteamIdOther = new SteamID(ulong.Parse(historyItem.SteamIdOther));
            TimeInit = CommonUtils.ParseSteamUnixDate(int.Parse(historyItem.TimeInit));
            TimeEscrowEnd = historyItem.TimeEscrowEnd;
            Status = historyItem.Status;
            MyItems = GetFullHistoryTradeItemsList(historyItem.AssetsGiven, historyItem.CurrencyGiven,
                assetDescriptions);
            HisItems = GetFullHistoryTradeItemsList(historyItem.AssetsReceived, historyItem.CurrencyReceived,
                assetDescriptions);
        }

        public string TradeId { get; set; }
        public SteamID SteamIdOther { get; set; }
        public DateTime TimeInit { get; set; }
        public string TimeEscrowEnd { get; set; }
        public TradeState Status { get; set; }
        public List<FullHistoryTradeItem> MyItems { get; set; }
        public List<FullHistoryTradeItem> HisItems { get; set; }

        private List<FullHistoryTradeItem> GetFullHistoryTradeItemsList(List<TradedAsset> tradedAssets,
            List<TradedCurrency> tradedCurrencies, List<AssetDescription> assetDescriptions)
        {
            var fullItems = new List<FullHistoryTradeItem>();
            if (tradedAssets == null) return fullItems;

            foreach (var asset in tradedAssets)
            {
                TradedCurrency currency = null;
                if (tradedCurrencies != null)
                    tradedCurrencies.FirstOrDefault(curr =>
                        curr.ClassId == asset.ClassId && curr.ContextId == asset.ContextId);

                var description = assetDescriptions.FirstOrDefault(descr =>
                    asset.ClassId == descr.ClassId && asset.InstanceId == descr.InstanceId);

                fullItems.Add(new FullHistoryTradeItem(asset, currency, description));
            }

            return fullItems;
        }
    }
}