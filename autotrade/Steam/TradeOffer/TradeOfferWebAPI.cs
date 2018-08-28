using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using SteamAuth;
using static autotrade.Steam.TradeOffer.Inventory;
using autotrade.Utils;
using Newtonsoft.Json.Serialization;
using System.Text;
using SteamKit2;
using autotrade.CustomElements.Utils;

namespace autotrade.Steam.TradeOffer {
    public class TradeOfferWebAPI {
        private readonly string apiKey;

        public TradeOfferWebAPI(string apiKey) {
            this.apiKey = apiKey;

            if (apiKey == null) {
                throw new ArgumentNullException("apiKey");
            }
        }

        private const string BaseUrl = "http://api.steampowered.com/IEconService/{0}/{1}/{2}";

        public OfferResponse GetTradeOffer(string tradeofferid) {
            string options = string.Format("?key={0}&tradeofferid={1}&language={2}", apiKey, tradeofferid, "en_us");
            string url = String.Format(BaseUrl, "GetTradeOffer", "v1", options);
            try {
                string response = SteamWeb.Request(url, "GET", data: null);
                var result = JsonConvert.DeserializeObject<ApiResponse<OfferResponse>>(response);
                return result.Response;
            } catch (Exception ex) {
                Utils.Logger.Warning("Error on getting trade offers", ex);
            }
            return new OfferResponse();
        }

        public TradeOfferState GetOfferState(string tradeofferid) {
            var resp = GetTradeOffer(tradeofferid);
            if (resp != null && resp.Offer != null) {
                return resp.Offer.TradeOfferState;
            }
            return TradeOfferState.TradeOfferStateUnknown;
        }

        public OffersResponse GetAllTradeOffers(string timeHistoricalCutoff = "1389106496", string language = "en_us") {
            return GetTradeOffers(true, true, false, true, false, timeHistoricalCutoff, language);
        }

        public OffersResponse GetActiveTradeOffers(bool getSentOffers, bool getReceivedOffers, bool getDescriptions, string language = "en_us") {
            return GetTradeOffers(getSentOffers, getReceivedOffers, getDescriptions, true, false, "1389106496", language);
        }

        public OffersResponse GetTradeOffers(bool getSentOffers, bool getReceivedOffers, bool getDescriptions, bool activeOnly, bool historicalOnly, string timeHistoricalCutoff = "1389106496", string language = "en_us") {
            if (!getSentOffers && !getReceivedOffers) {
                throw new ArgumentException("getSentOffers and getReceivedOffers can't be both false");
            }

            string options = string.Format("?key={0}&get_sent_offers={1}&get_received_offers={2}&get_descriptions={3}&language={4}&active_only={5}&historical_only={6}",
                apiKey, BoolConverter(getSentOffers), BoolConverter(getReceivedOffers), BoolConverter(getDescriptions), language, BoolConverter(activeOnly), BoolConverter(historicalOnly));

            if (timeHistoricalCutoff != "1389106496") options += $"&time_historical_cutoff={timeHistoricalCutoff}";


            string url = String.Format(BaseUrl, "GetTradeOffers", "v1", options);
            string response = SteamWeb.Request(url, "GET", data: null);
            try {
                var result = JsonConvert.DeserializeObject<ApiResponse<OffersResponse>>(response);
                return result.Response;
            } catch (Exception ex) {
                Logger.Error("Error on get rdade offers", ex);
            }
            return new OffersResponse();
        }

        public TradeHistoryResponse GetTradeHistory(int? maxTrades, long? startAfterTime, string startAfterTradeId,
            bool navigatingBack = false, bool getDescriptions = false, string lanugage = "en", bool includeFailed = false) {
            if (!string.IsNullOrEmpty(startAfterTradeId)) startAfterTime = null;

            string options = GetOptions(
                    ("key", apiKey),
                    ("max_trades", maxTrades),
                    ("start_after_tradeid", startAfterTradeId),
                    ("get_descriptions", BoolConverter(getDescriptions)),
                    ("language", lanugage),
                    ("include_failed", BoolConverter(includeFailed)),
                    ("start_after_time", startAfterTime),
                    ("navigating_back", BoolConverter(navigatingBack))
                 );

            string url = String.Format(BaseUrl, "GetTradeHistory", "v1", options);
            string response = SteamWeb.Request(url, "GET", data: null);
            try {
                var result = JsonConvert.DeserializeObject<ApiResponse<TradeHistoryResponse>>(response);
                return result.Response;
            } catch (Exception ex) {
                Debug.WriteLine(ex);
            }

            return new TradeHistoryResponse();
        }

        private static string GetOptions(params (string, object)[] options) {
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            foreach (var param in options) {
                if (param.Item2 != null && param.Item2.ToString() != "") {
                    queryString[param.Item1] = param.Item2.ToString();
                }
            }
            return "?" + queryString.ToString();
        }

        private static string BoolConverter(bool value) {
            return value ? "1" : "0";
        }

        public TradeOffersSummary GetTradeOffersSummary(UInt32 timeLastVisit) {
            string options = string.Format("?key={0}&time_last_visit={1}", apiKey, timeLastVisit);
            string url = String.Format(BaseUrl, "GetTradeOffersSummary", "v1", options);

            try {
                string response = SteamWeb.Request(url, "GET", data: null);
                var resp = JsonConvert.DeserializeObject<ApiResponse<TradeOffersSummary>>(response);

                return resp.Response;
            } catch (Exception ex) {
                Logger.Warning("Error on getting trade offer summary", ex);
            }
            return new TradeOffersSummary();
        }

        private bool DeclineTradeOffer(ulong tradeofferid) {
            string options = string.Format("?key={0}&tradeofferid={1}", apiKey, tradeofferid);
            string url = String.Format(BaseUrl, "DeclineTradeOffer", "v1", options);
            Debug.WriteLine(url);
            string response = SteamWeb.Request(url, "POST", data: null);
            dynamic json = JsonConvert.DeserializeObject(response);

            if (json == null || json.success != "1") {
                return false;
            }
            return true;
        }

        private bool CancelTradeOffer(ulong tradeofferid) {
            string options = string.Format("?key={0}&tradeofferid={1}", apiKey, tradeofferid);
            string url = String.Format(BaseUrl, "CancelTradeOffer", "v1", options);
            Debug.WriteLine(url);
            string response = SteamWeb.Request(url, "POST", data: null);
            dynamic json = JsonConvert.DeserializeObject(response);

            if (json == null || json.success != "1") {
                return false;
            }
            return true;
        }
    }

    public class TradeOffersSummary {
        [JsonProperty("pending_received_count")]
        public int PendingReceivedCount { get; set; }

        [JsonProperty("new_received_count")]
        public int NewReceivedCount { get; set; }

        [JsonProperty("updated_received_count")]
        public int UpdatedReceivedCount { get; set; }

        [JsonProperty("historical_received_count")]
        public int HistoricalReceivedCount { get; set; }

        [JsonProperty("pending_sent_count")]
        public int PendingSentCount { get; set; }

        [JsonProperty("newly_accepted_sent_count")]
        public int NewlyAcceptedSentCount { get; set; }

        [JsonProperty("updated_sent_count")]
        public int UpdatedSentCount { get; set; }

        [JsonProperty("historical_sent_count")]
        public int HistoricalSentCount { get; set; }
    }

    public class ApiResponse<T> {
        [JsonProperty("response")]
        public T Response { get; set; }
    }

    public class OfferResponse {
        [JsonProperty("offer")]
        public Offer Offer { get; set; }

        [JsonProperty("descriptions")]
        public List<AssetDescription> Descriptions { get; set; }
    }

    public class OffersResponse {
        [JsonProperty("trade_offers_sent")]
        public List<Offer> TradeOffersSent { get; set; }

        [JsonProperty("trade_offers_received")]
        public List<Offer> TradeOffersReceived { get; set; }

        [JsonProperty("descriptions")]
        public List<AssetDescription> Descriptions { get; set; }

        public IEnumerable<Offer> AllOffers {
            get {
                if (TradeOffersSent == null) {
                    return TradeOffersReceived;
                }

                return (TradeOffersReceived == null ? TradeOffersSent : TradeOffersSent.Union(TradeOffersReceived));
            }
        }
    }

    public class TradeHistoryResponse {
        [JsonProperty("total_trades")] public int TotalTrades { get; set; }

        [JsonProperty("more")] public bool More { get; set; }

        [JsonProperty("trades")] public List<TradeHistoryItem> Trades { get; set; }

        [JsonProperty("descriptions")] public List<AssetDescription> Descriptions { get; set; }
    }

    public class TradeHistoryItem {
        [JsonProperty("tradeid")] public string TradeId { get; set; }

        [JsonProperty("steamid_other")] public string SteamIdOther { get; set; }

        [JsonProperty("time_init")] public string TimeInit { get; set; }

        [JsonProperty("time_escrow_end")] public string TimeEscrowEnd { get; set; }

        [JsonProperty("status")] public TradeState Status { get; set; }

        [JsonProperty("assets_received")] public List<TradedAsset> AssetsReceived { get; set; }

        [JsonProperty("assets_given")] public List<TradedAsset> AssetsGiven { get; set; }

        [JsonProperty("currency_received")] public List<TradedCurrency> CurrencyReceived { get; set; }

        [JsonProperty("currency_given")] public List<TradedCurrency> CurrencyGiven { get; set; }
    }

    public class TradedAsset {
        [JsonProperty("appid")] public int Appid { get; set; }

        [JsonProperty("contextid")] public string ContextId { get; set; }

        [JsonProperty("assetid")] public string AssetId { get; set; }

        [JsonProperty("classid")] public string ClassId { get; set; }

        [JsonProperty("instanceid")] public string InstanceId { get; set; }

        [JsonProperty("new_assetid")] public string NewAssetId { get; set; }

        [JsonProperty("new_contextid")] public string NewContextId { get; set; }

        [JsonProperty("rollback_new_assetid")] public string RollbackNewAssetId { get; set; }

        [JsonProperty("rollback_new_contextid")] public string RollbackNewContextId { get; set; }

        [JsonProperty("amount")] public string Amount { get; set; }
    }

    public class TradedCurrency {
        [JsonProperty("appid")] public int Appid { get; set; }

        [JsonProperty("contextid")] public string ContextId { get; set; }

        [JsonProperty("currencyid")] public string CurrenyId { get; set; }

        [JsonProperty("classid")] public string ClassId { get; set; }

        [JsonProperty("new_currencyid")] public string NewCurrencyId { get; set; }

        [JsonProperty("new_contextid")] public string NewContextId { get; set; }

        [JsonProperty("rollback_new_currencyid")] public string RollbackNewCurrencyId { get; set; }

        [JsonProperty("rollback_new_contextid")] public string RollbackNewContextId { get; set; }

        [JsonProperty("amount")] public string Amount { get; set; }
    }

    public class Offer {
        [JsonProperty("tradeofferid")]
        public string TradeOfferId { get; set; }

        [JsonProperty("accountid_other")]
        public int AccountIdOther { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("expiration_time")]
        public int ExpirationTime { get; set; }

        [JsonProperty("trade_offer_state")]
        public TradeOfferState TradeOfferState { get; set; }

        [JsonProperty("items_to_give")]
        public List<CEconAsset> ItemsToGive { get; set; }

        [JsonProperty("items_to_receive")]
        public List<CEconAsset> ItemsToReceive { get; set; }

        [JsonProperty("is_our_offer")]
        public bool IsOurOffer { get; set; }

        [JsonProperty("time_created")]
        public int TimeCreated { get; set; }

        [JsonProperty("time_updated")]
        public int TimeUpdated { get; set; }

        [JsonProperty("from_real_time_trade")]
        public bool FromRealTimeTrade { get; set; }

        [JsonProperty("escrow_end_date")]
        public int EscrowEndDate { get; set; }

        [JsonProperty("confirmation_method")]
        private int confirmationMethod { get; set; }
        public TradeOfferConfirmationMethod ConfirmationMethod { get { return (TradeOfferConfirmationMethod)confirmationMethod; } set { confirmationMethod = (int)value; } }
    }

    public enum TradeOfferState {
        TradeOfferStateInvalid = 1,
        TradeOfferStateActive = 2,
        TradeOfferStateAccepted = 3,
        TradeOfferStateCountered = 4,
        TradeOfferStateExpired = 5,
        TradeOfferStateCanceled = 6,
        TradeOfferStateDeclined = 7,
        TradeOfferStateInvalidItems = 8,
        TradeOfferStateNeedsConfirmation = 9,
        TradeOfferStateCanceledBySecondFactor = 10,
        TradeOfferStateInEscrow = 11,
        TradeOfferStateUnknown
    }

    public enum TradeState {
        TradeStateInit = 0,
        TradeStatePreCommited = 1,
        TradeStateCommited = 2,
        TradeStateComplete = 3,
        TradeStateFailed = 4,
        TradeStatePartialSupportRollback = 5,
        TradeStateFullSupportRollback = 6,
        TradeStateSupportRollbackSelective = 7,
        TradeStateRollbackFailed = 8,
        TradeStateRollbackAbandoned = 9,
        TradeStateInEscrow = 10,
        TradeStateEscrowRollback = 11,
        TradeStateUnknown
    }

    public enum TradeOfferConfirmationMethod {
        TradeOfferConfirmationMethodInvalid = 0,
        TradeOfferConfirmationMethodEmail = 1,
        TradeOfferConfirmationMethodMobileApp = 2
    }

    public class CEconAsset {
        [JsonProperty("appid")]
        public string AppId { get; set; }

        [JsonProperty("contextid")]
        public string ContextId { get; set; }

        [JsonProperty("assetid")]
        public string AssetId { get; set; }

        [JsonProperty("classid")]
        public string ClassId { get; set; }

        [JsonProperty("instanceid")]
        public string InstanceId { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("missing")]
        public bool IsMissing { get; set; }
    }

    public class AssetDescription {
        [JsonProperty("appid")]
        public int AppId { get; set; }

        [JsonProperty("classid")]
        public string ClassId { get; set; }

        [JsonProperty("instanceid")]
        public string InstanceId { get; set; }

        [JsonProperty("currency")]
        public bool IsCurrency { get; set; }

        [JsonProperty("background_color")]
        public string BackgroundColor { get; set; }

        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        [JsonProperty("icon_url_large")]
        public string IconUrlLarge { get; set; }

        [JsonProperty("descriptions")]
        public List<Description> Descriptions { get; set; }

        [JsonProperty("tradable")]
        public bool IsTradable { get; set; }

        [JsonProperty("owner_actions")]
        public List<OwnerAction> OwnerActions { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("name_color")]
        public string NameColor { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("market_name")]
        public string MarketName { get; set; }

        [JsonProperty("market_hash_name")]
        public string MarketHashName { get; set; }
    }

    public class Description {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class OwnerAction {
        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class FullTradeItem {
        public CEconAsset Asset { get; set; }
        public AssetDescription Description { get; set; }

        public static AssetDescription GetDescription(CEconAsset asset, List<AssetDescription> descriptions) {
            AssetDescription description = null;
            try {
                description = descriptions
                    .First(item => (asset.InstanceId == item.InstanceId && asset.ClassId == item.ClassId));

            } catch (Exception ex) when (ex is ArgumentNullException || ex is InvalidOperationException) {
                description = new AssetDescription
                {
                    MarketHashName = "[Info is missing]",
                    AppId = int.Parse(asset.AppId),
                    Name = "[Info is missing]",
                    Type = "[Info is missing]"
                };
            }
            return description;
        }

        public static List<FullTradeItem> GetFullItemsList(List<CEconAsset> assets, List<AssetDescription> descriptions) {
            var itemsList = new List<FullTradeItem>();
            if (assets == null || descriptions == null) return itemsList;

            foreach (var item in assets) {
                itemsList.Add(
                    new FullTradeItem
                    {
                        Asset = item,
                        Description = FullTradeItem.GetDescription(item, descriptions)
                    }
                );
            }
            return itemsList;
        }
    }

    public class FullTradeOffer {
        public Offer Offer { get; set; }
        public List<FullTradeItem> ItemsToGive { get; set; }
        public List<FullTradeItem> ItemsToRecieve { get; set; }
    }

    public class FullHistoryTradeOffer {
        public string TradeId { get; set; }
        public SteamID SteamIdOther { get; set; }
        public DateTime TimeInit { get; set; }
        public string TimeEscrowEnd { get; set; }
        public TradeState Status { get; set; }
        public List<FullHistoryTradeItem> MyItems { get; set; }
        public List<FullHistoryTradeItem> HisItems { get; set; }

        public FullHistoryTradeOffer(TradeHistoryItem historyItem, List<AssetDescription> assetDescriptions) {
            TradeId = historyItem.TradeId;
            SteamIdOther = new SteamID(ulong.Parse(historyItem.SteamIdOther));
            TimeInit = CommonUtils.ParseSteamUnixDate(int.Parse(historyItem.TimeInit));
            TimeEscrowEnd = historyItem.TimeEscrowEnd;
            Status = historyItem.Status;
            MyItems = GetFullHistoryTradeItemsList(historyItem.AssetsGiven, historyItem.CurrencyGiven, assetDescriptions);
            HisItems = GetFullHistoryTradeItemsList(historyItem.AssetsReceived, historyItem.CurrencyReceived, assetDescriptions);
        }

        private List<FullHistoryTradeItem> GetFullHistoryTradeItemsList(List<TradedAsset> tradedAssets, List<TradedCurrency> tradedCurrencies, List<AssetDescription> assetDescriptions) {
            var fullItems = new List<FullHistoryTradeItem>();
            if (tradedAssets == null) return fullItems;

            foreach (var asset in tradedAssets) {
                TradedCurrency currency = null;
                if (tradedCurrencies != null) tradedCurrencies.FirstOrDefault(curr => curr.ClassId == asset.ClassId && curr.ContextId == asset.ContextId);

                var description = assetDescriptions.FirstOrDefault(descr => asset.ClassId == descr.ClassId && asset.InstanceId == descr.InstanceId);

                fullItems.Add(new FullHistoryTradeItem(asset, currency, description));
            }

            return fullItems;
        }
    }

    public class FullHistoryTradeItem {
        public TradedAsset Asset { get; set; }
        public TradedCurrency Currency { get; set; }
        public AssetDescription Description { get; set; }

        public FullHistoryTradeItem(TradedAsset tradedAsset, TradedCurrency tradedCurrency, AssetDescription assetDescription) {
            Asset = tradedAsset;
            Currency = tradedCurrency;
            Description = assetDescription;
        }
    }
}