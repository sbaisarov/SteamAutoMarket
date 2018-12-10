using SteamAutoMarket.UI.Repository.Context;

namespace SteamAutoMarket.Steam.TradeOffer
{
    using System;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Net;

    using Newtonsoft.Json;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.Steam.Auth;
    using SteamAutoMarket.Steam.Market.Exceptions;
    using SteamAutoMarket.Steam.TradeOffer.Enums;
    using SteamAutoMarket.Steam.TradeOffer.Models;

    using SteamKit2;

    public class OfferSession
    {
        internal const string SendUrl = "https://steamcommunity.com/tradeoffer/new/send";

        private readonly CookieContainer _cookies;

        private readonly string _sessionId;

        private readonly TradeOfferWebApi _webApi;

        public OfferSession(TradeOfferWebApi webApi, CookieContainer cookies, string sessionId)
        {
            this._webApi = webApi;
            this._cookies = cookies;
            this._sessionId = sessionId;

            this.JsonSerializerSettings = new JsonSerializerSettings
                                              {
                                                  PreserveReferencesHandling = PreserveReferencesHandling.None,
                                                  Formatting = Formatting.None
                                              };
        }

        public string Error { get; private set; }

        internal JsonSerializerSettings JsonSerializerSettings { get; set; }

        public TradeOfferAcceptResponse Accept(string tradeOfferId)
        {
            var data = new NameValueCollection
                           {
                               { "sessionid", this._sessionId }, { "serverid", "1" }, { "tradeofferid", tradeOfferId }
                           };

            var url = $"https://steamcommunity.com/tradeoffer/{tradeOfferId}/accept";
            var referer = $"https://steamcommunity.com/tradeoffer/{tradeOfferId}/";

            var resp = SteamWeb.Request(url, "POST", data, this._cookies, referer: referer);

            if (!string.IsNullOrEmpty(resp))
                try
                {
                    var res = JsonConvert.DeserializeObject<TradeOfferAcceptResponse>(resp);

                    // steam can return 'null' response
                    if (res != null)
                    {
                        res.Accepted = string.IsNullOrEmpty(res.TradeError);
                        return res;
                    }
                }
                catch (JsonException)
                {
                    return new TradeOfferAcceptResponse { TradeError = "Error parsing server response: " + resp };
                }

            // if it didn't work as expected, check the state, maybe it was accepted after all
            var state = this._webApi.GetOfferState(tradeOfferId);
            return new TradeOfferAcceptResponse { Accepted = state == TradeOfferState.TradeOfferStateAccepted };
        }

        public bool Cancel(string tradeOfferId)
        {
            var data = new NameValueCollection
                           {
                               { "sessionid", this._sessionId }, { "tradeofferid", tradeOfferId }, { "serverid", "1" }
                           };
            var url = string.Format("https://steamcommunity.com/tradeoffer/{0}/cancel", tradeOfferId);

            // should be http://steamcommunity.com/{0}/{1}/tradeoffers/sent/ - id/profile persona/id64 ideally
            var referer = string.Format("https://steamcommunity.com/tradeoffer/{0}/", tradeOfferId);

            var resp = SteamWeb.Request(url, "POST", data, this._cookies, referer: referer);

            if (!string.IsNullOrEmpty(resp))
            {
                try
                {
                    var json = JsonConvert.DeserializeObject<NewTradeOfferResponse>(resp);
                    if (json.TradeOfferId != null && json.TradeOfferId == tradeOfferId) return true;
                }
                catch (JsonException ex)
                {
                    Debug.WriteLine("Error on cancel trade offer" + ex.Message);
                }
            }
            else
            {
                var state = this._webApi.GetOfferState(tradeOfferId);
                if (state == TradeOfferState.TradeOfferStateCanceled) return true;
            }

            return false;
        }

        /// <summary>
        ///     Creates a new counter offer
        /// </summary>
        /// <param name="message">A message to include with the trade offer</param>
        /// <param name="otherSteamId">The SteamID of the partner we are trading with</param>
        /// <param name="status">The list of items we and they are going to trade</param>
        /// <param name="newTradeOfferId">The trade offer Id that will be created if successful</param>
        /// <param name="tradeOfferId">The trade offer Id of the offer being countered</param>
        /// <returns></returns>
        //public bool CounterOffer(
        //    string message,
        //    SteamID otherSteamId,
        //    TradeStatus status,
        //    out string newTradeOfferId,
        //    string tradeOfferId)
        //{
        //    if (string.IsNullOrEmpty(tradeOfferId))
        //        throw new ArgumentNullException("tradeOfferId", @"Trade Offer Id must be set for counter offers.");

        //    var data = new NameValueCollection
        //                   {
        //                       { "sessionid", this._sessionId },
        //                       { "serverid", "1" },
        //                       { "partner", otherSteamId.ConvertToUInt64().ToString() },
        //                       { "tradeoffermessage", message },
        //                       { "json_tradeoffer", JsonConvert.SerializeObject(status, this.JsonSerializerSettings) },
        //                       { "tradeofferid_countered", tradeOfferId },
        //                       { "trade_offer_create_params", "{}" }
        //                   };

        //    var referer = string.Format("https://steamcommunity.com/tradeoffer/{0}/", tradeOfferId);

        //    if (!this.Request(SendUrl, data, referer))
        //    {
        //        var state = this._webApi.GetOfferState(tradeOfferId);
        //        if (state == TradeOfferState.TradeOfferStateCountered) return true;
        //        return false;
        //    }

        //    return true;
        //}
        public bool Decline(string tradeOfferId)
        {
            var data = new NameValueCollection
                           {
                               { "sessionid", this._sessionId }, { "serverid", "1" }, { "tradeofferid", tradeOfferId }
                           };

            var url = string.Format("https://steamcommunity.com/tradeoffer/{0}/decline", tradeOfferId);

            // should be http://steamcommunity.com/{0}/{1}/tradeoffers - id/profile persona/id64 ideally
            var referer = string.Format("https://steamcommunity.com/tradeoffer/{0}/", tradeOfferId);

            var resp = SteamWeb.Request(url, "POST", data, this._cookies, referer: referer);

            if (!string.IsNullOrEmpty(resp))
            {
                try
                {
                    var json = JsonConvert.DeserializeObject<NewTradeOfferResponse>(resp);
                    if (json.TradeOfferId != null && json.TradeOfferId == tradeOfferId) return true;
                }
                catch (JsonException ex)
                {
                    Debug.WriteLine("Error on decline trade offer" + ex.Message);
                }
            }
            else
            {
                var state = this._webApi.GetOfferState(tradeOfferId);
                if (state == TradeOfferState.TradeOfferStateDeclined) return true;
            }

            return false;
        }

        /// <summary>
        ///     Creates a new trade offer
        /// </summary>
        /// <param name="message">A message to include with the trade offer</param>
        /// <param name="otherSteamId">The SteamID of the partner we are trading with</param>
        /// <param name="status">The list of items we and they are going to trade</param>
        /// <param name="newTradeOfferId">The trade offer Id that will be created if successful</param>
        /// <returns>True if successfully returns a newTradeOfferId, else false</returns>
        public string SendTradeOffer(string message, SteamID otherSteamId, TradeStatus status)
        {
            var data = new NameValueCollection
                           {
                               { "sessionid", this._sessionId },
                               { "serverid", "1" },
                               { "partner", otherSteamId.ConvertToUInt64().ToString() },
                               { "tradeoffermessage", message },
                               { "json_tradeoffer", JsonConvert.SerializeObject(status, this.JsonSerializerSettings) },
                               { "trade_offer_create_params", "{}" }
                           };

            var referer = string.Format(
                "https://steamcommunity.com/tradeoffer/new/?partner={0}",
                otherSteamId.AccountID);

            return this.Request(SendUrl, data, referer);
        }

        /// <summary>
        ///     Creates a new trade offer with a token
        /// </summary>
        /// <param name="message">A message to include with the trade offer</param>
        /// <param name="otherSteamId">The SteamID of the partner we are trading with</param>
        /// <param name="status">The list of items we and they are going to trade</param>
        /// <param name="token">The token of the partner we are trading with</param>
        /// <param name="newTradeOfferId">The trade offer Id that will be created if successful</param>
        /// <returns>True if successfully returns a newTradeOfferId, else false</returns>
        public string SendTradeOfferWithToken(string message, SteamID otherSteamId, TradeStatus status, string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token), @"Partner trade offer token is missing");
            var offerToken = new OfferAccessToken { TradeOfferAccessToken = token };

            var data = new NameValueCollection
                           {
                               { "sessionid", this._sessionId },
                               { "captcha", "" },
                               { "serverid", "1" },
                               { "partner", otherSteamId.ConvertToUInt64().ToString() },
                               { "tradeoffermessage", message },
                               { "json_tradeoffer", JsonConvert.SerializeObject(status, this.JsonSerializerSettings) },
                               {
                                   "trade_offer_create_params",
                                   JsonConvert.SerializeObject(offerToken, this.JsonSerializerSettings)
                               }
                           };

            var referer = string.Format(
                "https://steamcommunity.com/tradeoffer/new/?partner={0}&token={1}",
                otherSteamId.AccountID,
                token);

            return this.Request(SendUrl, data, referer);
        }

        internal string Request(string url, NameValueCollection data, string referer)
        {
            var resp = SteamWeb.Request(url, "POST", data, this._cookies, referer: referer);

            if (string.IsNullOrEmpty(resp)) throw new SteamException("Steam response is empty. No extra info provided");

            try
            {
                var offerResponse = JsonConvert.DeserializeObject<NewTradeOfferResponse>(resp);
                if (!string.IsNullOrEmpty(offerResponse.TradeOfferId))
                {
                    return offerResponse.TradeOfferId;
                }

                throw new SteamException(offerResponse.TradeError);
            }
            catch (JsonException ex)
            {
                Logger.Log.Debug(resp);
                throw new SteamException($"Unknown steam response - {ex.Message}");
            }
        }
    }
}