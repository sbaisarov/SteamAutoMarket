using System;
using System.Collections.Generic;
using System.Diagnostics;
using SteamAutoMarket.Steam.TradeOffer.Enums;
using SteamAutoMarket.Steam.TradeOffer.Models;
using SteamAutoMarket.Utils;
using SteamKit2;

namespace SteamAutoMarket.Steam.TradeOffer
{
    public class TradeOffer
    {
        public TradeOffer(OfferSession session, SteamID partnerSteamdId)
        {
            Items = new TradeStatus();
            IsOurOffer = true;
            OfferState = TradeOfferState.TradeOfferStateUnknown;
            Session = session;
            PartnerSteamId = partnerSteamdId;
        }

        public TradeOffer(OfferSession session, Offer offer)
        {
            var myAssets = new List<TradeAsset>();
            var myMissingAssets = new List<TradeAsset>();
            var theirAssets = new List<TradeAsset>();
            var theirMissingAssets = new List<TradeAsset>();
            if (offer.ItemsToGive != null)
                foreach (var asset in offer.ItemsToGive)
                {
                    var tradeAsset = new TradeAsset();
                    //todo: for currency items we need to check descriptions for currency bool and use the appropriate method
                    tradeAsset.CreateItemAsset(Convert.ToInt64(asset.AppId), Convert.ToInt64(asset.ContextId),
                        Convert.ToInt64(asset.AssetId), Convert.ToInt64(asset.Amount));
                    //todo: for missing assets we should store them somewhere else? if offer state is active we shouldn't be here though
                    if (!asset.IsMissing)
                        myAssets.Add(tradeAsset);
                    else
                        myMissingAssets.Add(tradeAsset);
                }

            if (offer.ItemsToReceive != null)
                foreach (var asset in offer.ItemsToReceive)
                {
                    var tradeAsset = new TradeAsset();
                    tradeAsset.CreateItemAsset(Convert.ToInt64(asset.AppId), Convert.ToInt64(asset.ContextId),
                        Convert.ToInt64(asset.AssetId), Convert.ToInt64(asset.Amount));
                    if (!asset.IsMissing)
                        theirAssets.Add(tradeAsset);
                    else
                        theirMissingAssets.Add(tradeAsset);
                }

            Session = session;
            //assume public individual
            PartnerSteamId = new SteamID(Convert.ToUInt32(offer.AccountIdOther), EUniverse.Public,
                EAccountType.Individual);
            TradeOfferId = offer.TradeOfferId;
            OfferState = offer.TradeOfferState;
            IsOurOffer = offer.IsOurOffer;
            ExpirationTime = offer.ExpirationTime;
            TimeCreated = offer.TimeCreated;
            TimeUpdated = offer.TimeUpdated;
            Message = offer.Message;
            Items = new TradeStatus(myAssets, theirAssets);
        }

        public OfferSession Session { get; set; }

        public SteamID PartnerSteamId { get; }

        public TradeStatus Items { get; set; }

        public string TradeOfferId { get; }

        public TradeOfferState OfferState { get; }

        public bool IsOurOffer { get; }

        public int TimeCreated { get; }

        public int ExpirationTime { get; }

        public int TimeUpdated { get; }

        public string Message { get; }

        public bool IsFirstOffer => TimeCreated == TimeUpdated;

        /// <summary>
        ///     Counter an existing offer with an updated offer
        /// </summary>
        /// <param name="newTradeOfferId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool CounterOffer(out string newTradeOfferId, string message = "")
        {
            newTradeOfferId = string.Empty;
            if (!string.IsNullOrEmpty(TradeOfferId) && !IsOurOffer &&
                OfferState == TradeOfferState.TradeOfferStateActive && Items.NewVersion)
                return Session.CounterOffer(message, PartnerSteamId, Items, out newTradeOfferId, TradeOfferId);
            Logger.Warning("Can't counter offer a trade that doesn't have an offerid, is ours or isn't active");
            return false;
        }

        /// <summary>
        ///     Send a new trade offer
        /// </summary>
        /// <param name="offerId">The trade offer id if successully created</param>
        /// <param name="message">Optional message to included with the trade offer</param>
        /// <returns>true if successfully sent, otherwise false</returns>
        public bool Send(out string offerId, string message = "")
        {
            offerId = string.Empty;
            if (TradeOfferId == null) return Session.SendTradeOffer(message, PartnerSteamId, Items, out offerId);
            Logger.Warning("Can't send a trade offer that already exists.");
            return false;
        }

        /// <summary>
        ///     Send a new trade offer using a token
        /// </summary>
        /// <param name="offerId">The trade offer id if successully created</param>
        /// <param name="token">The token of the partner</param>
        /// <param name="message">Optional message to included with the trade offer</param>
        /// <returns></returns>
        public bool SendWithToken(out string offerId, string token, string message = "")
        {
            offerId = string.Empty;
            if (TradeOfferId == null)
                return Session.SendTradeOfferWithToken(message, PartnerSteamId, Items, token, out offerId);
            Logger.Warning("Can't send a trade offer that already exists.");
            return false;
        }

        /// <summary>
        ///     Accepts the current offer
        /// </summary>
        /// <returns>TradeOfferAcceptResponse object containing accept result</returns>
        public TradeOfferAcceptResponse Accept()
        {
            if (TradeOfferId == null)
                return new TradeOfferAcceptResponse { TradeError = "Can't accept a trade without a tradeofferid" };
            if (!IsOurOffer && OfferState == TradeOfferState.TradeOfferStateActive) return Session.Accept(TradeOfferId);
            Logger.Warning("Can't accept a trade that is not active");
            return new TradeOfferAcceptResponse { TradeError = "Can't accept a trade that is not active" };
        }

        /// <summary>
        ///     Accepts the current offer. Old signature for compatibility
        /// </summary>
        /// <param name="tradeId">the tradeid if successful</param>
        /// <returns>true if successful, otherwise false</returns>
        [Obsolete("Use TradeOfferAcceptResponse Accept()")]
        public bool Accept(out string tradeId)
        {
            tradeId = string.Empty;
            if (TradeOfferId == null) throw new ArgumentException("TradeOfferId");

            var result = Accept();
            if (result.Accepted) tradeId = result.TradeId;
            return result.Accepted;
        }

        /// <summary>
        ///     Decline the current offer
        /// </summary>
        /// <returns>true if successful, otherwise false</returns>
        public bool Decline()
        {
            if (TradeOfferId == null)
            {
                Logger.Error("Can't decline a trade without a trade offer id");
                throw new ArgumentException("TradeOfferId");
            }

            if (!IsOurOffer && OfferState == TradeOfferState.TradeOfferStateActive)
                return Session.Decline(TradeOfferId);
            Logger.Warning("Can't decline a trade that is not active");
            return false;
        }

        /// <summary>
        ///     Cancel the current offer
        /// </summary>
        /// <returns>true if successful, otherwise false</returns>
        public bool Cancel()
        {
            if (TradeOfferId == null)
            {
                Logger.Error("Can't cancel a trade without a trade offer id");
                throw new ArgumentException("TradeOfferId");
            }

            if (IsOurOffer && OfferState == TradeOfferState.TradeOfferStateActive) return Session.Cancel(TradeOfferId);
            Logger.Warning("Can't cancel a trade that is not active and ours");
            return false;
        }
    }
}