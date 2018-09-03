using System;
using System.Diagnostics;
using System.Web;
using autotrade.Steam.TradeOffer.Enums;
using autotrade.Steam.TradeOffer.Models;
using autotrade.Utils;
using Newtonsoft.Json;
using SteamAuth;

namespace autotrade.Steam.TradeOffer
{
    public class TradeOfferWebApi
    {
        private const string BaseUrl = "http://api.steampowered.com/IEconService/{0}/{1}/{2}";
        private readonly string _apiKey;

        public TradeOfferWebApi(string apiKey)
        {
            _apiKey = apiKey;

            if (apiKey == null) throw new ArgumentNullException("apiKey");
        }

        public OfferResponse GetTradeOffer(string tradeofferid)
        {
            var options = string.Format("?key={0}&tradeofferid={1}&language={2}", _apiKey, tradeofferid, "en_us");
            var url = string.Format(BaseUrl, "GetTradeOffer", "v1", options);
            try
            {
                var response = SteamWeb.Request(url, "GET", data: null);
                var result = JsonConvert.DeserializeObject<ApiResponse<OfferResponse>>(response);
                return result.Response;
            }
            catch (Exception ex)
            {
                Logger.Warning("Error on getting trade offers", ex);
            }

            return new OfferResponse();
        }

        public TradeOfferState GetOfferState(string tradeofferid)
        {
            var resp = GetTradeOffer(tradeofferid);
            if (resp != null && resp.Offer != null) return resp.Offer.TradeOfferState;
            return TradeOfferState.TradeOfferStateUnknown;
        }

        public OffersResponse GetAllTradeOffers(string timeHistoricalCutoff = "1389106496", string language = "en_us")
        {
            return GetTradeOffers(true, true, false, true, false, timeHistoricalCutoff, language);
        }

        public OffersResponse GetActiveTradeOffers(bool getSentOffers, bool getReceivedOffers, bool getDescriptions,
            string language = "en_us")
        {
            return GetTradeOffers(getSentOffers, getReceivedOffers, getDescriptions, true, false, "1389106496",
                language);
        }

        public OffersResponse GetTradeOffers(bool getSentOffers, bool getReceivedOffers, bool getDescriptions,
            bool activeOnly, bool historicalOnly, string timeHistoricalCutoff = "1389106496", string language = "en_us")
        {
            if (!getSentOffers && !getReceivedOffers)
                throw new ArgumentException("getSentOffers and getReceivedOffers can't be both false");

            var options = string.Format(
                "?key={0}&get_sent_offers={1}&get_received_offers={2}&get_descriptions={3}&language={4}&active_only={5}&historical_only={6}",
                _apiKey, BoolConverter(getSentOffers), BoolConverter(getReceivedOffers), BoolConverter(getDescriptions),
                language, BoolConverter(activeOnly), BoolConverter(historicalOnly));

            if (timeHistoricalCutoff != "1389106496") options += $"&time_historical_cutoff={timeHistoricalCutoff}";


            var url = string.Format(BaseUrl, "GetTradeOffers", "v1", options);
            var response = SteamWeb.Request(url, "GET", data: null);
            try
            {
                var result = JsonConvert.DeserializeObject<ApiResponse<OffersResponse>>(response);
                return result.Response;
            }
            catch (Exception ex)
            {
                Logger.Error("Error on get rdade offers", ex);
            }

            return new OffersResponse();
        }

        public TradeHistoryResponse GetTradeHistory(int? maxTrades, long? startAfterTime, string startAfterTradeId,
            bool navigatingBack = false, bool getDescriptions = false, string lanugage = "en",
            bool includeFailed = false)
        {
            if (!string.IsNullOrEmpty(startAfterTradeId)) startAfterTime = null;

            var options = GetOptions(
                ("key", _apiKey),
                ("max_trades", maxTrades),
                ("start_after_tradeid", startAfterTradeId),
                ("get_descriptions", BoolConverter(getDescriptions)),
                ("language", lanugage),
                ("include_failed", BoolConverter(includeFailed)),
                ("start_after_time", startAfterTime),
                ("navigating_back", BoolConverter(navigatingBack))
            );

            var url = string.Format(BaseUrl, "GetTradeHistory", "v1", options);
            var response = SteamWeb.Request(url, "GET", data: null);
            try
            {
                var result = JsonConvert.DeserializeObject<ApiResponse<TradeHistoryResponse>>(response);
                return result.Response;
            }
            catch (Exception ex)
            {
                Logger.Error("Error on get trade history", ex);
            }

            return new TradeHistoryResponse();
        }

        private static string GetOptions(params (string, object)[] options)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            foreach (var param in options)
                if (param.Item2 != null && param.Item2.ToString() != "")
                    queryString[param.Item1] = param.Item2.ToString();
            return "?" + queryString;
        }

        private static string BoolConverter(bool value)
        {
            return value ? "1" : "0";
        }

        public TradeOffersSummary GetTradeOffersSummary(uint timeLastVisit)
        {
            var options = string.Format("?key={0}&time_last_visit={1}", _apiKey, timeLastVisit);
            var url = string.Format(BaseUrl, "GetTradeOffersSummary", "v1", options);

            try
            {
                var response = SteamWeb.Request(url, "GET", data: null);
                var resp = JsonConvert.DeserializeObject<ApiResponse<TradeOffersSummary>>(response);

                return resp.Response;
            }
            catch (Exception ex)
            {
                Logger.Warning("Error on getting trade offer summary", ex);
            }

            return new TradeOffersSummary();
        }

        public bool DeclineTradeOffer(ulong tradeofferid)
        {
            var options = $"?key={_apiKey}&tradeofferid={tradeofferid}";
            var url = string.Format(BaseUrl, "DeclineTradeOffer", "v1", options);
            
            var response = SteamWeb.Request(url, "POST", data: null);
            dynamic json = JsonConvert.DeserializeObject(response);

            if (json == null || json.success != "1") return false;
            return true;
        }

        public bool CancelTradeOffer(ulong tradeofferid)
        {
            var options = $"?key={_apiKey}&tradeofferid={tradeofferid}";
            var url = string.Format(BaseUrl, "CancelTradeOffer", "v1", options);
            
            var response = SteamWeb.Request(url, "POST", data: null);
            dynamic json = JsonConvert.DeserializeObject(response);

            if (json == null || json.success != "1") return false;
            return true;
        }
    }
}