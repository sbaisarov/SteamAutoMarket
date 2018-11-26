namespace Steam.TradeOffer
{
    using System;
    using System.Diagnostics;
    using System.Web;

    using Newtonsoft.Json;

    using Steam.Auth;
    using Steam.TradeOffer.Enums;
    using Steam.TradeOffer.Models;

    public class TradeOfferWebApi
    {
        private const string BaseUrl = "http://api.steampowered.com/IEconService/{0}/{1}/{2}";

        private readonly string _apiKey;

        public TradeOfferWebApi(string apiKey)
        {
            this._apiKey = apiKey;

            if (apiKey == null) throw new ArgumentNullException("apiKey");
        }

        public bool CancelTradeOffer(ulong tradeofferid)
        {
            var options = $"?key={this._apiKey}&tradeofferid={tradeofferid}";
            var url = string.Format(BaseUrl, "CancelTradeOffer", "v1", options);

            var response = SteamWeb.Request(url, "POST", data: null);
            dynamic json = JsonConvert.DeserializeObject(response);

            if (json == null || json.success != "1") return false;
            return true;
        }

        public bool DeclineTradeOffer(ulong tradeofferid)
        {
            var options = $"?key={this._apiKey}&tradeofferid={tradeofferid}";
            var url = string.Format(BaseUrl, "DeclineTradeOffer", "v1", options);

            var response = SteamWeb.Request(url, "POST", data: null);
            dynamic json = JsonConvert.DeserializeObject(response);

            if (json == null || json.success != "1") return false;
            return true;
        }

        public OffersResponse GetActiveTradeOffers(
            bool getSentOffers,
            bool getReceivedOffers,
            bool getDescriptions,
            string language = "en_us")
        {
            return this.GetTradeOffers(
                getSentOffers,
                getReceivedOffers,
                getDescriptions,
                true,
                false,
                "1389106496",
                language);
        }

        public OffersResponse GetAllTradeOffers(string timeHistoricalCutoff = "1389106496", string language = "en_us")
        {
            return this.GetTradeOffers(true, true, false, true, false, timeHistoricalCutoff, language);
        }

        public TradeOfferState GetOfferState(string tradeofferid)
        {
            var resp = this.GetTradeOffer(tradeofferid);
            if (resp != null && resp.Offer != null) return resp.Offer.TradeOfferState;
            return TradeOfferState.TradeOfferStateUnknown;
        }

        public TradeHistoryResponse GetTradeHistory(
            int? maxTrades = 100,
            string startAfterTime = null,
            string startAfterTradeId = null,
            bool navigatingBack = false,
            bool getDescriptions = false,
            bool includeFailed = false,
            string lanugage = "en")
        {
            if (!string.IsNullOrEmpty(startAfterTradeId)) startAfterTime = null;

            var options = GetOptions(
                ("key", this._apiKey),
                ("max_trades", maxTrades),
                ("start_after_tradeid", startAfterTradeId),
                ("get_descriptions", BoolConverter(getDescriptions)),
                ("language", lanugage),
                ("include_failed", BoolConverter(includeFailed)),
                ("start_after_time", startAfterTime),
                ("navigating_back", BoolConverter(navigatingBack)));

            var url = string.Format(BaseUrl, "GetTradeHistory", "v1", options);
            var response = SteamWeb.Request(url, "GET", data: null);
            try
            {
                var result = JsonConvert.DeserializeObject<ApiResponse<TradeHistoryResponse>>(response);
                return result.Response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error on get trade history", ex);
            }

            return new TradeHistoryResponse();
        }

        public OfferResponse GetTradeOffer(string tradeofferid)
        {
            var options = string.Format("?key={0}&tradeofferid={1}&language={2}", this._apiKey, tradeofferid, "en_us");
            var url = string.Format(BaseUrl, "GetTradeOffer", "v1", options);
            try
            {
                var response = SteamWeb.Request(url, "GET", data: null);
                var result = JsonConvert.DeserializeObject<ApiResponse<OfferResponse>>(response);
                return result.Response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error on getting trade offers", ex);
            }

            return new OfferResponse();
        }

        public OffersResponse GetTradeOffers(
            bool getSentOffers,
            bool getReceivedOffers,
            bool getDescriptions,
            bool activeOnly,
            bool historicalOnly,
            string timeHistoricalCutoff = "1389106496",
            string language = "en_us")
        {
            if (!getSentOffers && !getReceivedOffers)
                throw new ArgumentException("getSentOffers and getReceivedOffers can't be both false");

            var options = string.Format(
                "?key={0}&get_sent_offers={1}&get_received_offers={2}&get_descriptions={3}&language={4}&active_only={5}&historical_only={6}",
                this._apiKey,
                BoolConverter(getSentOffers),
                BoolConverter(getReceivedOffers),
                BoolConverter(getDescriptions),
                language,
                BoolConverter(activeOnly),
                BoolConverter(historicalOnly));

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
                Debug.WriteLine("Error on get rdade offers", ex);
            }

            return new OffersResponse();
        }

        public TradeOffersSummary GetTradeOffersSummary(uint timeLastVisit)
        {
            var options = string.Format("?key={0}&time_last_visit={1}", this._apiKey, timeLastVisit);
            var url = string.Format(BaseUrl, "GetTradeOffersSummary", "v1", options);

            try
            {
                var response = SteamWeb.Request(url, "GET", data: null);
                var resp = JsonConvert.DeserializeObject<ApiResponse<TradeOffersSummary>>(response);

                return resp.Response;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error on getting trade offer summary", ex);
            }

            return new TradeOffersSummary();
        }

        private static string BoolConverter(bool value)
        {
            return value ? "1" : "0";
        }

        private static string GetOptions(params (string, object)[] options)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            foreach (var param in options)
                if (param.Item2 != null && param.Item2.ToString() != string.Empty)
                    queryString[param.Item1] = param.Item2.ToString();
            return "?" + queryString;
        }
    }
}