namespace SteamAutoMarket.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using Steam.TradeOffer.Models.Full;

    public class TradeHistoryModel
    {
        public TradeHistoryModel(FullHistoryTradeOffer offer)
        {
            this.Offer = offer;
            this.MyItems = offer.MyItems.GroupBy(i => i.Description.MarketHashName)
                .Select(g => new SteamTradeHistoryItemsModel(g.ToArray()));
            this.PartnerItems = offer.HisItems.GroupBy(i => i.Description.MarketHashName)
                .Select(g => new SteamTradeHistoryItemsModel(g.ToArray()));
            this.TradeParameters = this.GetTradeParameters(offer);
            this.TradeId = offer.TradeId;
            this.Sender = new SteamAccountHyperlinkModel(offer.SteamIdOther);
            this.State = offer.Status.ToString().Replace("TradeState", string.Empty);
        }

        public IEnumerable<SteamTradeHistoryItemsModel> MyItems { get; }

        public FullHistoryTradeOffer Offer { get; }

        public IEnumerable<SteamTradeHistoryItemsModel> PartnerItems { get; }

        public SteamAccountHyperlinkModel Sender { get; }

        public string State { get; }

        public string TradeId { get; }

        public IEnumerable<NameValueModel> TradeParameters { get; }

        private IEnumerable<NameValueModel> GetTradeParameters(FullHistoryTradeOffer offer)
        {
            return new[]
                       {
                           new NameValueModel("SteamIdOther", offer.SteamIdOther),
                           new NameValueModel("TradeId", offer.TradeId), new NameValueModel("TimeInit", offer.TimeInit),
                           new NameValueModel("TimeInitOriginal", offer.Offer.TimeInit),
                           new NameValueModel("TimeEscrowEnd", offer.TimeEscrowEnd),
                           new NameValueModel("Status", offer.Status.ToString().Replace("TradeState", string.Empty)),
                           new NameValueModel("My items count", offer.MyItems.Count),
                           new NameValueModel("Partner items count", offer.HisItems.Count)
                       };
        }
    }
}