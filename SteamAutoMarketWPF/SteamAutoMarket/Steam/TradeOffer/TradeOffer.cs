namespace Steam.TradeOffer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Steam.TradeOffer.Enums;
    using Steam.TradeOffer.Models;

    using SteamKit2;

    public class TradeOffer
    {
        public TradeOffer(OfferSession session, SteamID partnerSteamdId)
        {
            this.Items = new TradeStatus();
            this.IsOurOffer = true;
            this.OfferState = TradeOfferState.TradeOfferStateUnknown;
            this.Session = session;
            this.PartnerSteamId = partnerSteamdId;
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

                    // todo: for currency items we need to check descriptions for currency bool and use the appropriate method
                    tradeAsset.CreateItemAsset(
                        Convert.ToInt64(asset.AppId),
                        Convert.ToInt64(asset.ContextId),
                        Convert.ToInt64(asset.AssetId),
                        Convert.ToInt64(asset.Amount));

                    // todo: for missing assets we should store them somewhere else? if offer state is active we shouldn't be here though
                    if (!asset.IsMissing)
                        myAssets.Add(tradeAsset);
                    else
                        myMissingAssets.Add(tradeAsset);
                }

            if (offer.ItemsToReceive != null)
                foreach (var asset in offer.ItemsToReceive)
                {
                    var tradeAsset = new TradeAsset();
                    tradeAsset.CreateItemAsset(
                        Convert.ToInt64(asset.AppId),
                        Convert.ToInt64(asset.ContextId),
                        Convert.ToInt64(asset.AssetId),
                        Convert.ToInt64(asset.Amount));
                    if (!asset.IsMissing)
                        theirAssets.Add(tradeAsset);
                    else
                        theirMissingAssets.Add(tradeAsset);
                }

            this.Session = session;

            // assume public individual
            this.PartnerSteamId = new SteamID(
                Convert.ToUInt32(offer.AccountIdOther),
                EUniverse.Public,
                EAccountType.Individual);
            this.TradeOfferId = offer.TradeOfferId;
            this.OfferState = offer.TradeOfferState;
            this.IsOurOffer = offer.IsOurOffer;
            this.ExpirationTime = offer.ExpirationTime;
            this.TimeCreated = offer.TimeCreated;
            this.TimeUpdated = offer.TimeUpdated;
            this.Message = offer.Message;
            this.Items = new TradeStatus(myAssets, theirAssets);
        }

        public int ExpirationTime { get; }

        public bool IsFirstOffer => this.TimeCreated == this.TimeUpdated;

        public bool IsOurOffer { get; }

        public TradeStatus Items { get; set; }

        public string Message { get; }

        public TradeOfferState OfferState { get; }

        public SteamID PartnerSteamId { get; }

        public OfferSession Session { get; set; }

        public int TimeCreated { get; }

        public int TimeUpdated { get; }

        public string TradeOfferId { get; }

        /// <summary>
        ///     Accepts the current offer
        /// </summary>
        /// <returns>TradeOfferAcceptResponse object containing accept result</returns>
        public TradeOfferAcceptResponse Accept()
        {
            if (this.TradeOfferId == null)
                return new TradeOfferAcceptResponse { TradeError = "Can't accept a trade without a tradeofferid" };
            if (!this.IsOurOffer && this.OfferState == TradeOfferState.TradeOfferStateActive)
                return this.Session.Accept(this.TradeOfferId);
            Debug.WriteLine("Can't accept a trade that is not active");
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
            if (this.TradeOfferId == null) throw new ArgumentException("TradeOfferId");

            var result = this.Accept();
            if (result.Accepted) tradeId = result.TradeId;
            return result.Accepted;
        }

        /// <summary>
        ///     Cancel the current offer
        /// </summary>
        /// <returns>true if successful, otherwise false</returns>
        public bool Cancel()
        {
            if (this.TradeOfferId == null)
            {
                Debug.WriteLine("Can't cancel a trade without a trade offer id");
                throw new ArgumentException("TradeOfferId");
            }

            if (this.IsOurOffer && this.OfferState == TradeOfferState.TradeOfferStateActive)
                return this.Session.Cancel(this.TradeOfferId);
            Debug.WriteLine("Can't cancel a trade that is not active and ours");
            return false;
        }

        /// <summary>
        ///     Counter an existing offer with an updated offer
        /// </summary>
        /// <param name="newTradeOfferId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        // public bool CounterOffer(out string newTradeOfferId, string message = "")
        // {
        // newTradeOfferId = string.Empty;
        // if (!string.IsNullOrEmpty(this.TradeOfferId) && !this.IsOurOffer
        // && this.OfferState == TradeOfferState.TradeOfferStateActive
        // && this.Items.NewVersion)
        // return this.Session.CounterOffer(
        // message,
        // this.PartnerSteamId,
        // this.Items,
        // out newTradeOfferId,
        // this.TradeOfferId);
        // Debug.WriteLine("Can't counter offer a trade that doesn't have an offerid, is ours or isn't active");
        // return false;
        // }

        /// <summary>
        ///     Decline the current offer
        /// </summary>
        /// <returns>true if successful, otherwise false</returns>
        public bool Decline()
        {
            if (this.TradeOfferId == null)
            {
                Debug.WriteLine("Can't decline a trade without a trade offer id");
                throw new ArgumentException("TradeOfferId");
            }

            if (!this.IsOurOffer && this.OfferState == TradeOfferState.TradeOfferStateActive)
                return this.Session.Decline(this.TradeOfferId);
            Debug.WriteLine("Can't decline a trade that is not active");
            return false;
        }

        /// <summary>
        ///     Send a new trade offer
        /// </summary>
        /// <param name="offerId">The trade offer id if successully created</param>
        /// <param name="message">Optional message to included with the trade offer</param>
        /// <returns>true if successfully sent, otherwise false</returns>
        // public string Send(out string offerId, string message = "")
        // {
        // offerId = string.Empty;
        // if (this.TradeOfferId == null)
        // return this.Session.SendTradeOffer(message, this.PartnerSteamId, this.Items);
        // Debug.WriteLine("Can't send a trade offer that already exists.");
        // return false;
        // }

        /// <summary>
        ///     Send a new trade offer using a token
        /// </summary>
        /// <param name="offerId">The trade offer id if successully created</param>
        /// <param name="token">The token of the partner</param>
        /// <param name="message">Optional message to included with the trade offer</param>
        /// <returns></returns>
        public string SendWithToken(string token, string message = "")
        {
            return this.Session.SendTradeOfferWithToken(message, this.PartnerSteamId, this.Items, token);
        }
    }
}