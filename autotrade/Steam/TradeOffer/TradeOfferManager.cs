using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using autotrade.Steam.TradeOffer.Enums;
using autotrade.Steam.TradeOffer.Models;
using SteamKit2;

namespace autotrade.Steam.TradeOffer
{
    public class TradeOfferManager
    {
        public delegate void TradeOfferUpdatedHandler(TradeOffer offer);

        private readonly Dictionary<string, TradeOfferState> _knownTradeOffers =
            new Dictionary<string, TradeOfferState>();

        private readonly OfferSession _session;
        private readonly Queue<Offer> _unhandledTradeOfferUpdates;
        private readonly TradeOfferWebAPI _webApi;

        public TradeOfferManager(string apiKey, CookieContainer cookies, string sessionid)
        {
            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));

            LastTimeCheckedOffers = DateTime.MinValue;
            _webApi = new TradeOfferWebAPI(apiKey);
            _session = new OfferSession(_webApi, cookies, sessionid);
            _unhandledTradeOfferUpdates = new Queue<Offer>();
        }

        public DateTime LastTimeCheckedOffers { get; private set; }

        /// <summary>
        ///     Occurs when a new trade offer has been made by the other user
        /// </summary>
        public event TradeOfferUpdatedHandler OnTradeOfferUpdated;

        public void EnqueueUpdatedOffers()
        {
            var startTime = DateTime.Now;

            var offersResponse = LastTimeCheckedOffers == DateTime.MinValue
                ? _webApi.GetAllTradeOffers()
                : _webApi.GetAllTradeOffers(GetUnixTimeStamp(LastTimeCheckedOffers).ToString());
            AddTradeOffersToQueue(offersResponse);

            LastTimeCheckedOffers =
                startTime - TimeSpan
                    .FromMinutes(
                        5); //Lazy way to make sure we don't miss any trade offers due to slightly differing clocks
        }

        private void AddTradeOffersToQueue(OffersResponse offers)
        {
            if (offers?.AllOffers == null)
                return;

            lock (_unhandledTradeOfferUpdates)
            {
                foreach (var offer in offers.AllOffers) _unhandledTradeOfferUpdates.Enqueue(offer);
            }
        }

        public bool HandleNextPendingTradeOfferUpdate()
        {
            Offer nextOffer;
            lock (_unhandledTradeOfferUpdates)
            {
                if (!_unhandledTradeOfferUpdates.Any()) return false;
                nextOffer = _unhandledTradeOfferUpdates.Dequeue();
            }

            return HandleTradeOfferUpdate(nextOffer);
        }

        private bool HandleTradeOfferUpdate(Offer offer)
        {
            if (_knownTradeOffers.ContainsKey(offer.TradeOfferId) &&
                _knownTradeOffers[offer.TradeOfferId] == offer.TradeOfferState) return false;

            //make sure the api loaded correctly sometimes the items are missing
            if (IsOfferValid(offer))
            {
                SendOfferToHandler(offer);
            }
            else
            {
                var resp = _webApi.GetTradeOffer(offer.TradeOfferId);
                if (IsOfferValid(resp.Offer))
                {
                    SendOfferToHandler(resp.Offer);
                }
                else
                {
                    Debug.WriteLine("Offer returned from steam api is not valid : " + resp.Offer.TradeOfferId);
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
            _knownTradeOffers[offer.TradeOfferId] = offer.TradeOfferState;
            OnTradeOfferUpdated(new TradeOffer(_session, offer));
        }

        private uint GetUnixTimeStamp(DateTime dateTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (uint) (dateTime.ToUniversalTime() - epoch).TotalSeconds;
        }

        public TradeOffer NewOffer(SteamID other)
        {
            return new TradeOffer(_session, other);
        }

        public bool TryGetOffer(string offerId, out TradeOffer tradeOffer)
        {
            tradeOffer = null;
            var resp = _webApi.GetTradeOffer(offerId);
            if (resp != null)
            {
                if (IsOfferValid(resp.Offer))
                {
                    tradeOffer = new TradeOffer(_session, resp.Offer);
                    return true;
                }

                Debug.WriteLine("Offer returned from steam api is not valid : " + resp.Offer.TradeOfferId);
            }

            return false;
        }
    }
}