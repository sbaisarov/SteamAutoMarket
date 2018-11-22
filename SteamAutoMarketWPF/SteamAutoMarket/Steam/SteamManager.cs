namespace Steam
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Threading;

    using Core;

    using Newtonsoft.Json;

    using Steam.Market;
    using Steam.Market.Enums;
    using Steam.Market.Exceptions;
    using Steam.Market.Interface;
    using Steam.Market.Models;
    using Steam.Market.Models.Json;
    using Steam.SteamAuth;
    using Steam.TradeOffer;
    using Steam.TradeOffer.Models;
    using Steam.TradeOffer.Models.Full;

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
            this.Login = login;
            this.Password = password;
            this.Guard = mafile;
            this.SteamId = new SteamID(this.Guard.Session.SteamID);

            this.SteamClient = new UserLogin(login, password) { TwoFactorCode = this.GenerateSteamGuardCode() };

            this.Guard.DeviceID = this.GetDeviceId();
            var isSessionRefreshed = this.Guard.RefreshSession();
            if (isSessionRefreshed == false || forceSessionRefresh)
            {
                this.UpdateSteamSession();
            }

            this.Guard.Session.AddCookies(this.Cookies);

            this.ApiKey = apiKey ?? this.FetchApiKey();
            this.TradeToken = tradeToken ?? this.FetchTradeToken();

            this.Inventory = new Inventory();
            this.TradeOfferWeb = new TradeOfferWebApi(this.ApiKey);
            this.OfferSession = new OfferSession(this.TradeOfferWeb, this.Cookies, this.Guard.Session.SessionID);

            var market = new SteamMarketHandler(ELanguage.English, userAgent);
            var auth = new Auth(market, this.Cookies) { IsAuthorized = true };
            market.Auth = auth;

            this.MarketClient = new MarketClient(market);

            this.Currency = currency ?? this.FetchCurrency();
            this.MarketClient.CurrentCurrency = this.Currency;
        }

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

        public virtual void AcceptOffers(IEnumerable<Offer> offers)
        {
            foreach (var offer in offers)
            {
                this.OfferSession.Accept(offer.TradeOfferId);
            }
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
                Logger.Log.Warn("Steam web session expired");
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
                    if (++attempts == 3)
                    {
                        Logger.Log.Warn($"Error on getting average price of {hashName}", ex);
                    }
                }
            }

            return price;
        }

        public virtual double? GetCurrentPrice(int appid, string hashName)
        {
            var itemPageInfo = MarketInfoCache.Get(appid, hashName);

            if (itemPageInfo == null)
            {
                itemPageInfo = this.MarketClient.ItemPage(appid, hashName);
                MarketInfoCache.Cache(appid, hashName, itemPageInfo);
            }

            var attempts = 0;
            double? price = null;
            while (attempts < 3)
            {
                try
                {
                    var histogram = this.MarketClient.ItemOrdersHistogramAsync(
                        itemPageInfo.NameId,
                        "RU",
                        ELanguage.Russian,
                        5).Result;

                    price = histogram.MinSellPrice;
                    break;
                }
                catch (Exception ex)
                {
                    if (++attempts == 3)
                    {
                        Logger.Log.Warn($"Error on getting current price of {hashName}", ex);
                    }
                }
            }

            return price;
        }

        public OffersResponse ReceiveTradeOffers()
        {
            return this.TradeOfferWeb.GetActiveTradeOffers(false, true, true);
        }

        public bool RemoveListing(long orderid)
        {
            var attempts = 0;
            while (attempts < 3)
            {
                var status = this.MarketClient.CancelSellOrder(orderid);
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
                price / 1.15);

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
        }

        protected InventoryRootModel LoadInventoryPage(
            SteamID steamId,
            int appId,
            int contextId,
            string startAssetId = null)
        {
            return this.Inventory.LoadInventoryPage(steamId, appId, contextId, startAssetId);
        }

        private double? CountAveragePrice(List<PriceHistoryDay> history, int daysCount)
        {
            // days are sorted from oldest to newest, we need the contrary
            history.Reverse();
            var days = history.GetRange(0, (daysCount < history.Count) ? daysCount : history.Count);
            var average = this.IterateHistory(days);
            if (average is null)
            {
                throw new SteamException($"No prices recorded during {daysCount} days");
            }

            average = this.IterateHistory(days, average);

            return average;
        }

        // public async Task ConfirmMarketTransactions()
        // {
        // Program.WorkingProcessForm.AppendWorkingProcessInfo("Fetching confirmations");
        // try
        // {
        // var confirmations = this.Guard.FetchConfirmations();
        // var marketConfirmations = confirmations
        // .Where(item => item.ConfType == Confirmation.ConfirmationType.MarketSellTransaction).ToArray();
        // Program.WorkingProcessForm.AppendWorkingProcessInfo("Accepting confirmations");
        // this.Guard.AcceptMultipleConfirmations(marketConfirmations);
        // }
        // catch (SteamGuardAccount.WGTokenExpiredException)
        // {
        // Program.WorkingProcessForm.AppendWorkingProcessInfo("Session expired. Updating...");
        // this.Guard.RefreshSession();
        // await this.ConfirmMarketTransactions();
        // }
        // }
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
                    this.Guard.SharedSecret + "," + TimeAligner.GetSteamTime());
                return JsonConvert.DeserializeObject<IDictionary<string, string>>(response)["result_0x23432"];
            }
        }

        private string GetDeviceId()
        {
            using (var wb = new WebClient())
            {
                var response = wb.UploadString(
                    "https://www.steambiz.store/api/gdevid",
                    this.SteamClient.SteamID.ToString());
                return JsonConvert.DeserializeObject<IDictionary<string, string>>(response)["result_0x23432"];
            }
        }

        private double? IterateHistory(IEnumerable<PriceHistoryDay> history, double? average = null)
        {
            double sum = 0;
            var count = 0;
            foreach (var item in history)
            {
                foreach (var data in item.History)
                {
                    if (!(average is null))
                    {
                        if (data.Price < average / 2 || data.Price > average * 2)
                        {
                            continue;
                        }
                    }

                    sum += data.Price * data.Count;
                    count += data.Count;
                }
            }

            double? result = sum / count;
            if (!double.IsNaN((double)result))
            {
                return result;
            }

            result = null;
            if (!(average is null))
            {
                result = average;
            }

            return result;
        }

        // public void CancelSellOrder(List<MyListingsSalesItem> itemsToCancel)
        // {
        // var index = 1;
        // const int ThreadsCount = 2;
        // var semaphore = new Semaphore(ThreadsCount, ThreadsCount);
        // var timeTrackCount = SavedSettings.Get().Settings2FaItemsToConfirm;
        // var timeTracker = new SellTimeTracker(timeTrackCount);

        // PriceLoader.StopAll();
        // PriceLoader.WaitForLoadFinish();

        // foreach (var item in itemsToCancel)
        // {
        // try
        // {
        // Program.WorkingProcessForm.AppendWorkingProcessInfo(
        // $"[{index++}/{itemsToCancel.Count}] Canceling - '{item.Name}'");

        // var realIndex = index;
        // semaphore.WaitOne();

        // Task.Run(
        // () =>
        // {
        // var response = this.MarketClient.CancelSellOrder(item.SaleId);

        // if (response == ECancelSellOrderStatus.Fail)
        // {
        // Program.WorkingProcessForm.AppendWorkingProcessInfo(
        // $"[{realIndex}/{itemsToCancel.Count}] Error on market cancel item");
        // }

        // if (realIndex % timeTrackCount == 0)
        // {
        // timeTracker.TrackTime(itemsToCancel.Count - realIndex);
        // }

        // semaphore.Release();
        // });
        // }
        // catch (Exception e)
        // {
        // Program.WorkingProcessForm.AppendWorkingProcessInfo(
        // $"[{index++}/{itemsToCancel.Count}] Error on cancel market item - {e.Message}");
        // }
        // }
    }
}