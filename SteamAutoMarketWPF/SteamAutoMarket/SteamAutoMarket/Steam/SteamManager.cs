namespace SteamAutoMarket.Steam
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Management;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Threading;

    using Newtonsoft.Json;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.Steam.Auth;
    using SteamAutoMarket.Steam.Market;
    using SteamAutoMarket.Steam.Market.Enums;
    using SteamAutoMarket.Steam.Market.Exceptions;
    using SteamAutoMarket.Steam.Market.Interface;
    using SteamAutoMarket.Steam.Market.Models;
    using SteamAutoMarket.Steam.Market.Models.Json;
    using SteamAutoMarket.Steam.TradeOffer;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;

    using SteamKit2;

    public class SteamManager
    {
        public SteamManager(
            string login,
            string password,
            SteamGuardAccount mafile,
            string apiKey = null,
            string tradeToken = null,
            int? currency = null,
            string userAgent = "",
            bool forceSessionRefresh = false)
        {
            if (!File.Exists("license.txt"))
            {
                throw new UnauthorizedAccessException("Cant get required info");
            }

            LicenseKey = File.ReadAllText("license.txt").Trim('\n', '\r', ' ');
            HwId = GetHwid();

            this.Login = login;
            this.Password = password;
            this.Guard = mafile;
            this.SteamId = new SteamID(this.Guard.Session.SteamID);

            Logger.Log.Debug("Fetching two factor token..");
            var twoFactorCode = this.GenerateSteamGuardCode();
            Logger.Log.Debug($"Two factor token is - {twoFactorCode}");

            this.SteamClient = new UserLogin(login, password) { TwoFactorCode = twoFactorCode };

            this.Guard.DeviceID = this.GetDeviceId();

            Logger.Log.Debug("Checking session status..");
            var isSessionRefreshed = this.Guard.RefreshSession();
            if (isSessionRefreshed == false || forceSessionRefresh)
            {
                this.UpdateSteamSession();
            }

            this.Guard.Session.AddCookies(this.Cookies);

            if (forceSessionRefresh)
            {
                this.ApiKey = this.FetchApiKey();
                this.TradeToken = this.FetchTradeToken();
            }
            else
            {
                this.ApiKey = apiKey ?? this.FetchApiKey();
                this.TradeToken = tradeToken ?? this.FetchTradeToken();
            }

            this.ApiKey = apiKey ?? this.FetchApiKey();
            this.TradeToken = tradeToken ?? this.FetchTradeToken();

            this.Inventory = new Inventory();

            Logger.Log.Debug("Initializing TradeOfferWebApi..");
            this.TradeOfferWeb = new TradeOfferWebApi(this.ApiKey);

            Logger.Log.Debug("Initializing OfferSession..");
            this.OfferSession = new OfferSession(this.TradeOfferWeb, this.Cookies, this.Guard.Session.SessionID);

            Logger.Log.Debug("Initializing SteamMarketHandler..");
            var market = new SteamMarketHandler(ELanguage.English, userAgent);
            var auth = new Market.Auth(market, this.Cookies) { IsAuthorized = true };
            market.Auth = auth;

            Logger.Log.Debug("Initializing SteamMarketHandler..");
            this.MarketClient = new MarketClient(market);

            if (forceSessionRefresh)
            {
                this.Currency = this.FetchCurrency();
            }
            else
            {
                this.Currency = currency ?? this.FetchCurrency();
            }

            this.MarketClient.CurrentCurrency = this.Currency;

            Logger.Log.Debug("SteamManager was successfully initialized");
        }

        public static string HwId { get; set; }

        public static string LicenseKey { get; set; }

        public string ApiKey { get; set; }

        public CookieContainer Cookies { get; set; } = new CookieContainer();

        public int Currency { get; set; }

        public SteamGuardAccount Guard { get; set; }

        public Inventory Inventory { get; set; }

        public string Login { get; }

        public MarketClient MarketClient { get; set; }

        public OfferSession OfferSession { get; set; }

        public string Password { get; }

        public UserLogin SteamClient { get; set; }

        public SteamID SteamId { get; }

        public TradeOfferWebApi TradeOfferWeb { get; }

        public string TradeToken { get; set; }

        protected bool IsSessionUpdated { get; set; }

        public void BuyOnMarket(int appid, string hashName)
        {
            var averagePrice = this.GetAveragePrice(appid, hashName, 7);
            var histogram = this.GetPrice(appid, hashName);
        }

        public void ConfirmTradeTransactions(ulong offerId)
        {
            try
            {
                var confirmations = this.Guard.FetchConfirmations();
                var conf = confirmations.FirstOrDefault(
                    item => item.ConfType == Confirmation.ConfirmationType.Trade && (item.Creator == offerId));
                if (conf == null)
                {
                    throw new SteamException($"Trade with {offerId} trade id not found");
                }

                this.Guard.AcceptConfirmation(conf);
            }
            catch (SteamGuardAccount.WGTokenExpiredException)
            {
                Logger.Log.Debug("Steam web session expired");
                this.UpdateSteamSession();
                this.ConfirmTradeTransactions(offerId);
            }
        }

        public string FetchTradeToken()
        {
            this.IsSessionUpdated = true;

            Logger.Log.Debug("Parsing trade token from - 'https://steamcommunity.com/my/tradeoffers/privacy'");

            try
            {
                var response = SteamWeb.Request(
                    "https://steamcommunity.com/my/tradeoffers/privacy",
                    "GET",
                    string.Empty,
                    this.Cookies);

                if (response == null)
                {
                    Logger.Log.Warn(
                        "Error on parsing trade token. Steam privacy page cant not be loaded. Try to scrap it manually from - 'https://steamcommunity.com/my/tradeoffers/privacy'");

                    return null;
                }

                var token = Regex.Match(
                    response,
                    @"https://steamcommunity\.com/tradeoffer/new/\?partner=.+&token=(.+?)""").Groups[1].Value;

                if (string.IsNullOrEmpty(token))
                {
                    Logger.Log.Warn(
                        "Error on parsing trade token. Steam privacy page cant not be loaded. Try to scrap it manually from - 'https://steamcommunity.com/my/tradeoffers/privacy'");

                    return null;
                }

                Logger.Log.Debug($"'{token}' trade token was successfully parsed");
                return token;
            }
            catch (Exception e)
            {
                Logger.Log.Warn(
                    $"Error on parsing trade token. {e.Message}. Try to scrap it manually from - 'https://steamcommunity.com/my/tradeoffers/privacy'");

                return null;
            }
        }

        public virtual double? GetAveragePrice(int appid, string hashName, int days)
        {
            double? price = null;
            var attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    var history = this.MarketClient.PriceHistory(appid, hashName);
                    price = this.CountAveragePrice(history, days);
                    if (price.HasValue)
                    {
                        price = Math.Round(price.Value, 2);
                    }

                    break;
                }
                catch (Exception ex)
                {
                    attempts++;
                    Logger.Log.Warn($"Error on getting average price of {hashName}", ex.InnerException ?? ex);
                }
            }

            return price;
        }

        public virtual double? GetCurrentPrice(int appid, string hashName)
        {
            var histogram = this.GetPrice(appid, hashName);
            if (histogram == null)
            {
                Logger.Log.Warn($"Error on getting current price of {hashName}");
                return null;
            }

            var price = histogram.MinSellPrice;
            return price;
        }

        public IEnumerable<FullTradeOffer> ReceiveTradeOffers(
            bool getSentOffers,
            bool getReceivedOffers,
            string language = "en_us")
        {
            var offersResponse = this.TradeOfferWeb.GetActiveTradeOffers(
                getSentOffers,
                getReceivedOffers,
                true,
                language);

            var fullOffersList = new List<FullTradeOffer>();

            if (offersResponse.AllOffers == null)
            {
                return fullOffersList;
            }

            fullOffersList.AddRange(
                offersResponse.AllOffers.Select(
                    trade => new FullTradeOffer
                                 {
                                     Offer = trade,
                                     MyItems = FullTradeItem.GetFullItemsList(
                                         trade.ItemsToGive,
                                         offersResponse.Descriptions),
                                     PartnerItems = FullTradeItem.GetFullItemsList(
                                         trade.ItemsToReceive,
                                         offersResponse.Descriptions)
                                 }));

            return fullOffersList;
        }

        public bool RemoveListing(long orderId)
        {
            var attempts = 0;
            while (attempts < 3)
            {
                var status = this.MarketClient.CancelSellOrder(orderId);
                if (status == ECancelSellOrderStatus.Canceled) return true;
                attempts++;
            }

            return false;
        }

        public virtual void SellOnMarket(FullRgItem item, double price)
        {
            var asset = item.Asset;
            var description = item.Description;

            JSellItem resp = this.MarketClient.SellItem(
                description.Appid,
                int.Parse(asset.Contextid),
                long.Parse(asset.Assetid),
                int.Parse(item.Asset.Amount),
                price);

            var message = resp.Message; // error message
            if (message != null)
            {
                throw new SteamException(message);
            }
        }

        public string SendTradeOffer(FullRgItem[] items, SteamID partnerId, string tradeToken)
        {
            var offer = new TradeOffer.TradeOffer(this.OfferSession, partnerId);
            foreach (var item in items)
            {
                offer.Items.AddMyItem(
                    item.Asset.Appid,
                    long.Parse(item.Asset.Contextid),
                    long.Parse(item.Asset.Assetid),
                    long.Parse(item.Asset.Amount));
            }

            return offer.SendWithToken(tradeToken);
        }

        public void UpdateSteamSession()
        {
            Logger.Log.Info($"Saved steam session for {this.Guard.AccountName} is expired. Refreshing session.");
            this.IsSessionUpdated = true;

            LoginResult loginResult;
            var tryCount = 0;
            do
            {
                loginResult = this.SteamClient.DoLogin();
                if (loginResult == LoginResult.LoginOkay)
                {
                    continue;
                }

                Logger.Log.Warn($"Login status is - {loginResult}");

                if (++tryCount == 3)
                {
                    throw new SteamException("Login failed after 3 attempts!");
                }

                Thread.Sleep(3000);
            }
            while (loginResult != LoginResult.LoginOkay);

            if (loginResult != LoginResult.LoginOkay)
            {
                throw new SteamException("Login failed after 3 attempts");
            }

            this.Guard.Session = this.SteamClient.Session;
            this.Guard.Session.AddCookies(this.Cookies);
        }

        protected InventoryRootModel LoadInventoryPage(
            SteamID steamId,
            int appId,
            int contextId,
            string startAssetId = null,
            CookieContainer cookies = null)
        {
            return this.Inventory.LoadInventoryPage(steamId, appId, contextId, startAssetId, cookies: cookies);
        }

        private static string GetHwid()
        {
            var mc = new ManagementClass("win32_processor");
            var moc = mc.GetInstances();
            var uid = moc.Cast<ManagementObject>().Select(x => x.Properties["processorID"]).FirstOrDefault()?.Value
                .ToString();

            try
            {
                var dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + "C" + @":""");
                dsk.Get();
                uid += dsk["VolumeSerialNumber"].ToString();
            }
            catch
            {
                throw new UnauthorizedAccessException("Cant get required info");
            }

            return uid;
        }

        private double? CountAveragePrice(List<PriceHistoryDay> history, int daysCount)
        {
            // days are sorted from oldest to newest, we need the contrary
            history.Reverse();
            var days = history.GetRange(0, (daysCount < history.Count) ? daysCount : history.Count);
            var pricesTotal = this.IterateHistory(days, null, 2);
            if (pricesTotal.Count == 0)
            {
                throw new SteamException($"No prices recorded during {daysCount} days");
            }

            var average = Math.Round(pricesTotal.Average(), 2);
            var prices = new List<double>();
            var rate = 2;

            // while less than 30% of amount of total prices
            while (prices.Count < pricesTotal.Count * 0.3)
            {
                prices = this.IterateHistory(days, average, rate);
                if (prices.Count > 0) average = Math.Round(prices.Average(), 2);
                rate *= 2;
            }

            return average;
        }

        private string FetchApiKey()
        {
            this.IsSessionUpdated = true;

            Logger.Log.Debug("Parsing steam api key from - 'https://steamcommunity.com/dev/apikey'");
            while (true)
            {
                var response = SteamWeb.Request(
                    "https://steamcommunity.com/dev/apikey",
                    "GET",
                    data: null,
                    cookies: this.Cookies);
                var keyParse = Regex.Match(response, @"Key: (.+)</p").Groups[1].Value.Trim();
                if (keyParse.Length != 0)
                {
                    Logger.Log.Debug($"{keyParse} api key was successfully parsed");
                    return keyParse;
                }

                Logger.Log.Debug("Seems like account do not have api key. Trying to regenerate it");
                var sessionid = this.SteamClient.Session.SessionID;
                var data = new NameValueCollection
                               {
                                   { "domain", "domain.com" },
                                   { "agreeToTerms", "agreed" },
                                   { "sessionid", sessionid },
                                   { "Submit", "Register" }
                               };

                response = SteamWeb.Request(
                    "https://steamcommunity.com/dev/registerkey",
                    "GET",
                    data: data,
                    cookies: this.Cookies);
            }
        }

        private int FetchCurrency()
        {
            Logger.Log.Debug("Parsing current currency");
            try
            {
                this.IsSessionUpdated = true;
                var result = this.MarketClient.WalletInfo().Currency;
                Logger.Log.Debug($"Current currency is '{result}'");
                return result;
            }
            catch (Exception e)
            {
                throw new SteamException($"Error on parsing current currency - {e.Message}", e);
            }
        }

        private string GenerateSteamGuardCode()
        {
            using (var wb = new WebClient())
            {
                var response = wb.UploadString(
                    "https://www.steambiz.store/api/gguardcode",
                    $"{this.Guard.SharedSecret},{TimeAligner.GetSteamTime()},{LicenseKey},{HwId}");
                return JsonConvert.DeserializeObject<IDictionary<string, string>>(response)["result_0x23432"];
            }
        }

        private string GetDeviceId()
        {
            using (var wb = new WebClient())
            {
                var response = wb.UploadString(
                    "https://www.steambiz.store/api/gdevid",
                    $"{this.SteamClient.SteamID.ToString()},{LicenseKey},{HwId}");
                return JsonConvert.DeserializeObject<IDictionary<string, string>>(response)["result_0x23432"];
            }
        }

        private ItemOrdersHistogram GetPrice(int appid, string hashName)
        {
            var itemPageInfo = MarketInfoCache.Get(appid, hashName);

            if (itemPageInfo == null)
            {
                itemPageInfo = this.MarketClient.ItemPage(appid, hashName);
                MarketInfoCache.Cache(appid, hashName, itemPageInfo);
            }

            var attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    var histogram = this.MarketClient.ItemOrdersHistogramAsync(itemPageInfo.NameId).Result;
                    return histogram;
                }
                catch (Exception ex)
                {
                    Logger.Log.Debug($"Error on getting price - {appid}-{hashName} - {ex.Message}", ex);
                    if (++attempts == 3)
                    {
                        break;
                    }
                }
            }

            return null;
        }

        private List<double> IterateHistory(IEnumerable<PriceHistoryDay> history, double? average, int rate)
        {
            var prices = new List<double>();

            foreach (var item in history)
            {
                foreach (var data in item.History)
                {
                    if (!(average is null))
                    {
                        var diff = (double)(average - data.Price);
                        if (data.Price < (diff / rate) || data.Price > (diff * rate))
                        {
                            if (diff > 0) continue;
                        }
                    }

                    prices.AddRange(Enumerable.Range(0, data.Count).Select(_ => data.Price));
                }
            }

            return prices;
        }
    }
}