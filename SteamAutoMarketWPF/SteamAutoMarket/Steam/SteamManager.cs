namespace Steam
{
    using System;
    using System.Collections.Generic;
    using System.Net;
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
            string apiKey,
            bool forceSessionRefresh = false)
        {
            this.Login = login;
            this.Password = password;
            this.Guard = mafile;
            this.SteamId = new SteamID(this.Guard.Session.SteamID);

            this.SteamClient = new UserLogin(login, password) { TwoFactorCode = this.GenerateSteamGuardCode() };

            // this.Guard.DeviceID = this.GetDeviceId(); todo
            var isSessionRefreshed = this.Guard.RefreshSession();
            if (isSessionRefreshed == false || forceSessionRefresh)
            {
                this.UpdateSteamSession();
            }

            this.ApiKey = apiKey; // todo

            // this.UpdateTradeToken(); todo
            this.Guard.Session.AddCookies(this.Cookies);

            this.Inventory = new Inventory();
            this.TradeOfferWeb = new TradeOfferWebApi(apiKey);
            this.OfferSession = new OfferSession(this.TradeOfferWeb, this.Cookies, this.Guard.Session.SessionID);

            var market = new SteamMarketHandler(ELanguage.English, "user-agent");
            var auth = new Auth(market, this.Cookies) { IsAuthorized = true };
            market.Auth = auth;

            this.MarketClient = new MarketClient(market);
        }

        public string ApiKey { get; set; }

        public CookieContainer Cookies { get; set; } = new CookieContainer();

        public SteamGuardAccount Guard { get; set; }

        public Inventory Inventory { get; set; }

        public string Login { get; }

        public MarketClient MarketClient { get; set; }

        public OfferSession OfferSession { get; set; }

        public string Password { get; }

        public UserLogin SteamClient { get; set; }

        public SteamID SteamId { get; }

        public TradeOfferWebApi TradeOfferWeb { get; }

        protected bool IsSessionUpdated { get; set; }

        public virtual void AcceptOffers(IEnumerable<Offer> offers)
        {
            foreach (var offer in offers)
            {
                this.OfferSession.Accept(offer.TradeOfferId);
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

        // public bool SendTradeOffer(List<FullRgItem> items, string partnerId, string tradeToken, out string offerId)
        // {
        // var offer = new TradeOffer.TradeOffer.TradeOffer(this.OfferSession, new SteamID(ulong.Parse(partnerId)));
        // foreach (var item in items)
        // {
        // offer.Items.AddMyItem(
        // item.Asset.Appid,
        // long.Parse(item.Asset.Contextid),
        // long.Parse(item.Asset.Assetid),
        // long.Parse(item.Asset.Amount));
        // }

        // return offer.SendWithToken(out offerId, tradeToken);
        // }

        // public void ConfirmTradeTransactions(List<ulong> offerids)
        // {
        // try
        // {
        // var confirmations = this.Guard.FetchConfirmations();
        // var conf = confirmations.Where(
        // item => item.ConfType == Confirmation.ConfirmationType.Trade && offerids.Contains(item.Creator))
        // .ToArray()[0];
        // this.Guard.AcceptConfirmation(conf);
        // }
        // catch (SteamGuardAccount.WGTokenExpiredException)
        // {
        // Program.WorkingProcessForm.AppendWorkingProcessInfo("Session expired. Updating...");
        // this.Guard.RefreshSession();
        // this.ConfirmTradeTransactions(offerids);
        // }
        // }
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

        private string GenerateSteamGuardCode()
        {
            // todo add license check
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
            // todo add license check
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

        // public string UpdateTradeToken()
        // {
        // var response = SteamWeb.Request(
        // "https://steamcommunity.com/my/tradeoffers/privacy",
        // "GET",
        // string.Empty,
        // this.Cookies);

        // if (response == null)
        // {
        // return null;
        // }

        // var token = string.Empty;

        // try
        // {
        // token = Regex.Match(response, @"https://steamcommunity\.com/tradeoffer/new/\?partner=.+&token=(.+?)""")
        // .Groups[1].Value;
        // }
        // catch (Exception)
        // {
        // // ignored
        // }

        // if (token != string.Empty)
        // {
        // var acc = SavedSteamAccount.Get().FirstOrDefault(a => a.Login == this.Guard.AccountName);
        // if (acc != null)
        // {
        // acc.TradeToken = token;
        // SavedSteamAccount.UpdateAll(SavedSteamAccount.Get());
        // }
        // }

        // return token;
        // }

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
        // }
    }
}