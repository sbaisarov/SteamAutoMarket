namespace SteamAutoMarket.UI.Models
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using SteamAutoMarket.Steam;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;

    public class ActiveTradeModel
    {
        public ActiveTradeModel(FullTradeOffer offer)
        {
            this.Offer = offer;
            this.MyItems = offer.MyItems.GroupBy(i => i.Description.MarketHashName)
                .Select(g => new SteamTradeItemsModel(g.ToArray()));
            this.PartnerItems = offer.PartnerItems.GroupBy(i => i.Description.MarketHashName)
                .Select(g => new SteamTradeItemsModel(g.ToArray()));
            this.TradeParameters = GetTradeParameters(offer);
            this.TradeId = offer.Offer.TradeOfferId;
            this.Sender = new SteamAccountHyperlinkModel(offer.Offer.AccountIdOther);
            this.State = offer.Offer.IsOurOffer ? "Sent " : "Received ";
            this.State += offer.Offer.TradeOfferState.ToString().Replace("TradeOfferState", string.Empty);
        }

        public IEnumerable<SteamTradeItemsModel> MyItems { get; }

        public FullTradeOffer Offer { get; }

        public IEnumerable<SteamTradeItemsModel> PartnerItems { get; }

        public SteamAccountHyperlinkModel Sender { get; }

        public string State { get; }

        public string TradeId { get; }

        public IEnumerable<NameValueModel> TradeParameters { get; }

        private static IEnumerable<NameValueModel> GetTradeParameters(FullTradeOffer tradeOffer)
        {
            var expirationTime = SteamUtils.ParseSteamUnixDate(tradeOffer.Offer.ExpirationTime)
                .ToString(CultureInfo.InvariantCulture);
            var createdTime = SteamUtils.ParseSteamUnixDate(tradeOffer.Offer.TimeCreated)
                .ToString(CultureInfo.InvariantCulture);
            var updatedTime = SteamUtils.ParseSteamUnixDate(tradeOffer.Offer.TimeUpdated)
                .ToString(CultureInfo.InvariantCulture);

            return new[]
                       {
                           new NameValueModel("TradeOfferId", tradeOffer.Offer.TradeOfferId),
                           new NameValueModel(
                               "TradeOfferState",
                               tradeOffer.Offer.TradeOfferState.ToString().Replace("TradeOfferState", string.Empty)),
                           new NameValueModel("Message", tradeOffer.Offer.Message),
                           new NameValueModel("IsOurOffer", tradeOffer.Offer.IsOurOffer.ToString()),
                           new NameValueModel("AccountIdOther", tradeOffer.Offer.AccountIdOther.ToString()),
                           new NameValueModel("ExpirationTime", expirationTime),
                           new NameValueModel(
                               "ConfirmationMethod",
                               tradeOffer.Offer.EConfirmationMethod.ToString().Replace(
                                   "TradeOfferConfirmation",
                                   string.Empty)),
                           new NameValueModel("TimeCreated", createdTime),
                           new NameValueModel("TimeUpdated", updatedTime),
                           new NameValueModel("EscrowEndDate", tradeOffer.Offer.EscrowEndDate.ToString()),
                           new NameValueModel("FromRealTimeTrade", tradeOffer.Offer.FromRealTimeTrade.ToString())
                       };
        }
    }
}