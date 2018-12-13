namespace SteamAutoMarket.Steam.TradeOffer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;

    using SteamAutoMarket.Steam.TradeOffer.Enums;
    using SteamAutoMarket.Steam.TradeOffer.Models;

    using SteamKit2;

    public class TradeOfferManager
    {
        private readonly Dictionary<string, TradeOfferState> _knownTradeOffers =
            new Dictionary<string, TradeOfferState>();

        private readonly OfferSession _session;

        private readonly Queue<Offer> _unhandledTradeOfferUpdates;

        private readonly TradeOfferWebApi _webApi;

        public TradeOfferManager(string apiKey, CookieContainer cookies, string sessionid)
        {
            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));

            this.LastTimeCheckedOffers = DateTime.MinValue;
            this._webApi = new TradeOfferWebApi(apiKey);
            this._session = new OfferSession(this._webApi, cookies, sessionid);
            this._unhandledTradeOfferUpdates = new Queue<Offer>();
        }

        public delegate void TradeOfferUpdatedHandler(TradeOffer offer);

        /// <summary>
        ///     Occurs when a new trade offer has been made by the other user
        /// </summary>
        public event TradeOfferUpdatedHandler OnTradeOfferUpdated;

        public DateTime LastTimeCheckedOffers { get; private set; }

        public void EnqueueUpdatedOffers()
        {
            var startTime = DateTime.Now;

            var offersResponse = this.LastTimeCheckedOffers == DateTime.MinValue
                                     ? this._webApi.GetAllTradeOffers()
                                     : this._webApi.GetAllTradeOffers(
                                         this.GetUnixTimeStamp(this.LastTimeCheckedOffers).ToString());
            this.AddTradeOffersToQueue(offersResponse);

            this.LastTimeCheckedOffers =
                startTime - TimeSpan.FromMinutes(
                    5); // Lazy way to make sure we don't miss any trade offers due to slightly differing clocks
        }

        public bool HandleNextPendingTradeOfferUpdate()
        {
            Offer nextOffer;
            lock (this._unhandledTradeOfferUpdates)
            {
                if (!this._unhandledTradeOfferUpdates.Any()) return false;
                nextOffer = this._unhandledTradeOfferUpdates.Dequeue();
            }

            return this.HandleTradeOfferUpdate(nextOffer);
        }

        public TradeOffer NewOffer(SteamID other)
        {
            return new TradeOffer(this._session, other);
        }

        public bool TryGetOffer(string offerId, out TradeOffer tradeOffer)
        {
            tradeOffer = null;
            var resp = this._webApi.GetTradeOffer(offerId);
            if (resp != null)
            {
                if (this.IsOfferValid(resp.Offer))
                {
                    tradeOffer = new TradeOffer(this._session, resp.Offer);
                    return true;
                }

                Debug.WriteLine($"Offer returned from steam api is not valid : {resp.Offer.TradeOfferId}");
            }

            return false;
        }

        private void AddTradeOffersToQueue(OffersResponse offers)
        {
            if (offers?.AllOffers == null)
                return;

            lock (this._unhandledTradeOfferUpdates)
            {
                foreach (var offer in offers.AllOffers) this._unhandledTradeOfferUpdates.Enqueue(offer);
            }
        }

        private uint GetUnixTimeStamp(DateTime dateTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (uint)(dateTime.ToUniversalTime() - epoch).TotalSeconds;
        }

        private bool HandleTradeOfferUpdate(Offer offer)
        {
            if (this._knownTradeOffers.ContainsKey(offer.TradeOfferId)
                && this._knownTradeOffers[offer.TradeOfferId] == offer.TradeOfferState) return false;

            // make sure the api loaded correctly sometimes the items are missing
            if (this.IsOfferValid(offer))
            {
                this.SendOfferToHandler(offer);
            }
            else
            {
                var resp = this._webApi.GetTradeOffer(offer.TradeOfferId);
                if (this.IsOfferValid(resp.Offer))
                {
                    this.SendOfferToHandler(resp.Offer);
                }
                else
                {
                    Debug.WriteLine($"Offer returned from steam api is not valid : {resp.Offer.TradeOfferId}");
                    return false;
                }
            }

            return true;
        }

        private bool IsOfferValid(Offer offer)
        {
            var hasItemsToGive = offer.ItemsToGive != null && offer.ItemsToGive.Count != 0;
            var hasItemsToReceive = offer.ItemsToReceive != null && offer.ItemsToReceive.Count != 0;
            return hasItemsToGive || hasItemsToReceive;
        }

        private void SendOfferToHandler(Offer offer)
        {
            this._knownTradeOffers[offer.TradeOfferId] = offer.TradeOfferState;
            this.OnTradeOfferUpdated?.Invoke(new TradeOffer(this._session, offer));
        }
    }
}