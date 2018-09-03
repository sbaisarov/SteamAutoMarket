using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using autotrade.Steam.TradeOffer.Enums;
using autotrade.Steam.TradeOffer.Models;
using Newtonsoft.Json;
using SteamAuth;
using SteamKit2;

namespace autotrade.Steam.TradeOffer
{
    public class OfferSession
    {
        internal const string SendUrl = "https://steamcommunity.com/tradeoffer/new/send";
        private readonly CookieContainer _cookies;
        private readonly string _sessionId;
        private readonly TradeOfferWebAPI _webApi;

        public OfferSession(TradeOfferWebAPI webApi, CookieContainer cookies, string sessionId)
        {
            _webApi = webApi;
            _cookies = cookies;
            _sessionId = sessionId;

            JsonSerializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.None, Formatting = Formatting.None
            };
        }

        public string Error { get; private set; }

        internal JsonSerializerSettings JsonSerializerSettings { get; set; }

        public TradeOfferAcceptResponse Accept(string tradeOfferId)
        {
            var data = new NameValueCollection();
            data.Add("sessionid", _sessionId);
            data.Add("serverid", "1");
            data.Add("tradeofferid", tradeOfferId);

            var url = $"https://steamcommunity.com/tradeoffer/{tradeOfferId}/accept";
            var referer = $"https://steamcommunity.com/tradeoffer/{tradeOfferId}/";

            var resp = SteamWeb.Request(url, "POST", data, _cookies, referer: referer);

            if (!string.IsNullOrEmpty(resp))
                try
                {
                    var res = JsonConvert.DeserializeObject<TradeOfferAcceptResponse>(resp);
                    //steam can return 'null' response
                    if (res != null)
                    {
                        res.Accepted = string.IsNullOrEmpty(res.TradeError);
                        return res;
                    }
                }
                catch (JsonException)
                {
                    return new TradeOfferAcceptResponse {TradeError = "Error parsing server response: " + resp};
                }

            //if it didn't work as expected, check the state, maybe it was accepted after all
            var state = _webApi.GetOfferState(tradeOfferId);
            return new TradeOfferAcceptResponse {Accepted = state == TradeOfferState.TradeOfferStateAccepted};
        }

        public bool Decline(string tradeOfferId)
        {
            var data = new NameValueCollection();
            data.Add("sessionid", _sessionId);
            data.Add("serverid", "1");
            data.Add("tradeofferid", tradeOfferId);

            var url = string.Format("https://steamcommunity.com/tradeoffer/{0}/decline", tradeOfferId);
            //should be http://steamcommunity.com/{0}/{1}/tradeoffers - id/profile persona/id64 ideally
            var referer = string.Format("https://steamcommunity.com/tradeoffer/{0}/", tradeOfferId);

            var resp = SteamWeb.Request(url, "POST", data, _cookies, referer: referer);

            if (!string.IsNullOrEmpty(resp))
            {
                try
                {
                    var json = JsonConvert.DeserializeObject<NewTradeOfferResponse>(resp);
                    if (json.TradeOfferId != null && json.TradeOfferId == tradeOfferId) return true;
                }
                catch (JsonException jsex)
                {
                    Debug.WriteLine(jsex);
                }
            }
            else
            {
                var state = _webApi.GetOfferState(tradeOfferId);
                if (state == TradeOfferState.TradeOfferStateDeclined) return true;
            }

            return false;
        }

        public bool Cancel(string tradeOfferId)
        {
            var data = new NameValueCollection();
            data.Add("sessionid", _sessionId);
            data.Add("tradeofferid", tradeOfferId);
            data.Add("serverid", "1");
            var url = string.Format("https://steamcommunity.com/tradeoffer/{0}/cancel", tradeOfferId);
            //should be http://steamcommunity.com/{0}/{1}/tradeoffers/sent/ - id/profile persona/id64 ideally
            var referer = string.Format("https://steamcommunity.com/tradeoffer/{0}/", tradeOfferId);

            var resp = SteamWeb.Request(url, "POST", data, _cookies, referer: referer);

            if (!string.IsNullOrEmpty(resp))
            {
                try
                {
                    var json = JsonConvert.DeserializeObject<NewTradeOfferResponse>(resp);
                    if (json.TradeOfferId != null && json.TradeOfferId == tradeOfferId) return true;
                }
                catch (JsonException jsex)
                {
                    Debug.WriteLine(jsex);
                }
            }
            else
            {
                var state = _webApi.GetOfferState(tradeOfferId);
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
        public bool CounterOffer(string message, SteamID otherSteamId, TradeOffer.TradeStatus status,
            out string newTradeOfferId, string tradeOfferId)
        {
            if (string.IsNullOrEmpty(tradeOfferId))
                throw new ArgumentNullException("tradeOfferId", "Trade Offer Id must be set for counter offers.");

            var data = new NameValueCollection();
            data.Add("sessionid", _sessionId);
            data.Add("serverid", "1");
            data.Add("partner", otherSteamId.ConvertToUInt64().ToString());
            data.Add("tradeoffermessage", message);
            data.Add("json_tradeoffer", JsonConvert.SerializeObject(status, JsonSerializerSettings));
            data.Add("tradeofferid_countered", tradeOfferId);
            data.Add("trade_offer_create_params", "{}");

            var referer = string.Format("https://steamcommunity.com/tradeoffer/{0}/", tradeOfferId);

            if (!Request(SendUrl, data, referer, tradeOfferId, out newTradeOfferId))
            {
                var state = _webApi.GetOfferState(tradeOfferId);
                if (state == TradeOfferState.TradeOfferStateCountered) return true;
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Creates a new trade offer
        /// </summary>
        /// <param name="message">A message to include with the trade offer</param>
        /// <param name="otherSteamId">The SteamID of the partner we are trading with</param>
        /// <param name="status">The list of items we and they are going to trade</param>
        /// <param name="newTradeOfferId">The trade offer Id that will be created if successful</param>
        /// <returns>True if successfully returns a newTradeOfferId, else false</returns>
        public bool SendTradeOffer(string message, SteamID otherSteamId, TradeOffer.TradeStatus status,
            out string newTradeOfferId)
        {
            var data = new NameValueCollection();
            data.Add("sessionid", _sessionId);
            data.Add("serverid", "1");
            data.Add("partner", otherSteamId.ConvertToUInt64().ToString());
            data.Add("tradeoffermessage", message);
            data.Add("json_tradeoffer", JsonConvert.SerializeObject(status, JsonSerializerSettings));
            data.Add("trade_offer_create_params", "{}");

            var referer = string.Format("https://steamcommunity.com/tradeoffer/new/?partner={0}",
                otherSteamId.AccountID);

            return Request(SendUrl, data, referer, null, out newTradeOfferId);
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
        public bool SendTradeOfferWithToken(string message, SteamID otherSteamId, TradeOffer.TradeStatus status,
            string token, out string newTradeOfferId)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException("token", "Partner trade offer token is missing");
            var offerToken = new OfferAccessToken {TradeOfferAccessToken = token};

            var data = new NameValueCollection();
            data.Add("sessionid", _sessionId);
            data.Add("serverid", "1");
            data.Add("partner", otherSteamId.ConvertToUInt64().ToString());
            data.Add("tradeoffermessage", message);
            data.Add("json_tradeoffer", JsonConvert.SerializeObject(status, JsonSerializerSettings));
            data.Add("trade_offer_create_params", JsonConvert.SerializeObject(offerToken, JsonSerializerSettings));

            var referer = string.Format("https://steamcommunity.com/tradeoffer/new/?partner={0}&token={1}",
                otherSteamId.AccountID, token);
            return Request(SendUrl, data, referer, null, out newTradeOfferId);
        }

        internal bool Request(string url, NameValueCollection data, string referer,
            string tradeOfferId, out string newTradeOfferId)
        {
            newTradeOfferId = "";

            var resp = SteamWeb.Request(url, "POST", data, referer: referer);
            if (!string.IsNullOrEmpty(resp))
                try
                {
                    var offerResponse = JsonConvert.DeserializeObject<NewTradeOfferResponse>(resp);
                    if (!string.IsNullOrEmpty(offerResponse.TradeOfferId))
                    {
                        newTradeOfferId = offerResponse.TradeOfferId;
                        return true;
                    }

                    Error = offerResponse.TradeError;
                    Debug.WriteLine(offerResponse.TradeError);
                }
                catch (JsonException jsex)
                {
                    Debug.WriteLine(jsex);
                }

            return false;
        }
    }
}