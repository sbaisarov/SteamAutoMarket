namespace SteamAutoMarket.Steam
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;

    using SteamAuth;

    using SteamAutoMarket.Steam.Market;
    using SteamAutoMarket.Steam.Market.Enums;
    using SteamAutoMarket.Steam.Market.Exceptions;
    using SteamAutoMarket.Steam.Market.Interface;
    using SteamAutoMarket.Steam.Market.Models;
    using SteamAutoMarket.Steam.Market.Models.Json;
    using SteamAutoMarket.Steam.TradeOffer;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.Utils;
    using SteamAutoMarket.WorkingProcess;
    using SteamAutoMarket.WorkingProcess.MarketPriceFormation;
    using SteamAutoMarket.WorkingProcess.PriceLoader;
    using SteamAutoMarket.WorkingProcess.Settings;

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
            this.Guard = mafile;
            this.SteamClient = new UserLogin(login, password) { TwoFactorCode = this.Guard.GenerateSteamGuardCode() };

            var isSessionRefreshed = this.Guard.RefreshSession();
            if (isSessionRefreshed == false || forceSessionRefresh)
            {
                Logger.Debug($"Saved steam session for {login} is expired. Refreshing session.");
                LoginResult loginResult;
                var tryCount = 0;
                do
                {
                    loginResult = this.SteamClient.DoLogin();
                    if (loginResult == LoginResult.LoginOkay)
                    {
                        continue;
                    }

                    Logger.Warning($"Login status is - {loginResult}");

                    if (++tryCount == 3)
                    {
                        throw new WebException("Login failed after 3 attempts!");
                    }

                    Thread.Sleep(3000);
                }
                while (loginResult != LoginResult.LoginOkay);

                this.Guard.Session = this.SteamClient.Session;
                this.SaveAccount(this.Guard);
            }

            this.ApiKey = apiKey;

            this.TradeOfferWeb = new TradeOfferWebApi(apiKey);
            this.OfferSession = new OfferSession(this.TradeOfferWeb, this.Cookies, this.Guard.Session.SessionID);
            this.Guard.Session.AddCookies(this.Cookies);
            var market = new SteamMarketHandler(ELanguage.English, "user-agent");
            var auth = new Auth(market, this.Cookies) { IsAuthorized = true };

            this.FetchTradeToken();
            market.Auth = auth;
            this.MarketClient = new MarketClient(market);
            this.Inventory = new Inventory();
        }

        public string ApiKey { get; set; }

        public OfferSession OfferSession { get; set; }

        public TradeOfferWebApi TradeOfferWeb { get; }

        public Inventory Inventory { get; set; }

        public UserLogin SteamClient { get; set; }

        public MarketClient MarketClient { get; set; }

        public SteamGuardAccount Guard { get; set; }

        public CookieContainer Cookies { get; set; } = new CookieContainer();

        public List<FullRgItem> LoadInventory(string steamid, string appid, string contextid, bool withLogs = false)
        {
            if (withLogs)
            {
                return this.Inventory.GetInventoryWithLogs(
                    new SteamID(ulong.Parse(steamid)),
                    int.Parse(appid),
                    int.Parse(contextid));
            }

            return this.Inventory.GetInventory(
                new SteamID(ulong.Parse(steamid)),
                int.Parse(appid),
                int.Parse(contextid));
        }

        public void SellOnMarket(ToSaleObject items)
        {
            PriceLoader.WaitForLoadFinish();

            var maxErrorsCount = SavedSettings.Get().ErrorsOnSellToSkip;
            var currentItemIndex = 1;
            var itemsToConfirmCount = SavedSettings.Get().Settings2FaItemsToConfirm;
            var totalItemsCount = items.ItemsForSaleList.Sum(x => x.Items.Count());
            var timeTracker = new SellTimeTracker(itemsToConfirmCount);

            foreach (var package in items.ItemsForSaleList)
            {
                var itemName = package.Items.First().Description.Name;
                if (!package.Price.HasValue)
                {
                    try
                    {
                        var task = Task.Run(async () => await items.GetPrice(package.Items.First(), this));
                        Program.WorkingProcessForm.AppendWorkingProcessInfo($"Processing price for '{itemName}'");
                        task.Wait();

                        var price = task.Result;
                        if (price != null)
                        {
                            package.Price = price;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex is ThreadAbortException)
                        {
                            return;
                        }

                        Logger.Error("Error on market price parse", ex);
                        Program.WorkingProcessForm.AppendWorkingProcessInfo(
                            $"ERROR on selling '{itemName}' - {ex.Message}{ex.InnerException?.Message}");

                        totalItemsCount -= package.Items.Sum(e => int.Parse(e.Asset.Amount));
                        continue;
                    }
                }

                var packageElementIndex = 1;
                var packageTotalItemsCount = package.Items.Count();
                var errorsCount = 0;
                foreach (var item in package.Items)
                {
                    try
                    {
                        Program.WorkingProcessForm.AppendWorkingProcessInfo(
                            $"[{currentItemIndex}/{totalItemsCount}] Selling - [{packageElementIndex++}/{packageTotalItemsCount}] - '{itemName}' for {package.Price}");
                        if (package.Price.HasValue)
                        {
                            this.SellOnMarket(item, package.Price.Value);
                        }
                        else
                        {
                            Program.WorkingProcessForm.AppendWorkingProcessInfo(
                                $"ERROR on selling '{itemName}' - Price is not loaded. Skipping item.");
                            currentItemIndex += package.Items.Count();
                            break;
                        }

                        if (currentItemIndex % itemsToConfirmCount == 0)
                        {
                            Task.Run(this.ConfirmMarketTransactions);

                            timeTracker.TrackTime(totalItemsCount - currentItemIndex);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex is ThreadAbortException)
                        {
                            return;
                        }

                        Logger.Error("Error on market sell", ex);
                        Program.WorkingProcessForm.AppendWorkingProcessInfo(
                            $"ERROR on selling '{itemName}' - {ex.Message}{ex.InnerException?.Message}");

                        if (++errorsCount == maxErrorsCount)
                        {
                            Program.WorkingProcessForm.AppendWorkingProcessInfo(
                                $"{maxErrorsCount} fails limit on sell {itemName} reached. Skipping item.");
                            totalItemsCount -= packageTotalItemsCount - packageElementIndex;
                            break;
                        }
                    }

                    currentItemIndex++;
                }
            }

            Task.Run(this.ConfirmMarketTransactions);
        }

        public void SellOnMarket(FullRgItem item, double price)
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

        public bool RemoveListing(long orderid)
        {
            var attempts = 0;
            while (attempts < 3)
            {
                var status = MarketClient.CancelSellOrder(orderid);
                if (status == ECancelSellOrderStatus.Canceled) return true;
                attempts++;
            }

            return false;
        }

        public async Task ConfirmMarketTransactions()
        {
            Program.WorkingProcessForm.AppendWorkingProcessInfo("Fetching confirmations");
            try
            {
                var confirmations = this.Guard.FetchConfirmations();
                var marketConfirmations = confirmations
                    .Where(item => item.ConfType == Confirmation.ConfirmationType.MarketSellTransaction).ToArray();
                Program.WorkingProcessForm.AppendWorkingProcessInfo("Accepting confirmations");
                this.Guard.AcceptMultipleConfirmations(marketConfirmations);
            }
            catch (SteamGuardAccount.WGTokenExpiredException)
            {
                Program.WorkingProcessForm.AppendWorkingProcessInfo("Session expired. Updating...");
                this.Guard.RefreshSession();
                this.ConfirmMarketTransactions();
            }
        }

        public double? GetAveragePrice(int appid, string hashName, int days)
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
                        Logger.Warning($"Error on getting average price of {hashName}", ex);
                    }
                }
            }

            return price;
        }

        public async Task<double?> GetCurrentPrice(int appid, string hashName)
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
                    var histogram = await this.MarketClient.ItemOrdersHistogramAsync(
                                        itemPageInfo.NameId,
                                        "RU",
                                        ELanguage.Russian,
                                        5);
                    price = histogram.MinSellPrice;
                    break;
                }
                catch (Exception ex)
                {
                    if (++attempts == 3)
                    {
                        Logger.Warning($"Error on getting current price of {hashName}", ex);
                    }
                }
            }

            return price;
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

        private double? IterateHistory(List<PriceHistoryDay> history, double? average = null)
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

        public bool SendTradeOffer(List<FullRgItem> items, string partnerId, string tradeToken)
        {
            var offer = new TradeOffer.TradeOffer(this.OfferSession, new SteamID(ulong.Parse(partnerId)));
            return offer.SendWithToken(out _, tradeToken);
        }

        public void ConfirmTradeTransactions(List<ulong> offerids)
        {
            try
            {
                var confirmations = this.Guard.FetchConfirmations();
                var conf = confirmations.Where(
                        item => item.ConfType == Confirmation.ConfirmationType.Trade && offerids.Contains(item.Creator))
                    .ToArray()[0];
                this.Guard.AcceptConfirmation(conf);
            }
            catch (SteamGuardAccount.WGTokenExpiredException)
            {
                Program.WorkingProcessForm.AppendWorkingProcessInfo("Session expired. Updating...");
                this.Guard.RefreshSession();
                this.ConfirmTradeTransactions(offerids);
            }
        }

        public OffersResponse ReceiveTradeOffers()
        {
            return this.TradeOfferWeb.GetActiveTradeOffers(false, true, true);
        }

        public void AcceptOffers(IEnumerable<Offer> offers)
        {
            foreach (var offer in offers)
            {
                this.OfferSession.Accept(offer.TradeOfferId);
            }
        }

        public bool FetchTradeToken()
        {
            var response = SteamWeb.Request(
                "https://steamcommunity.com/my/tradeoffers/privacy",
                "GET",
                string.Empty,
                this.Cookies);

            if (response == null)
            {
                return false;
            }

            var token = string.Empty;

            try
            {
                token = Regex.Match(response, @"https://steamcommunity\.com/tradeoffer/new/\?partner=.+&token=(.+?)""")
                    .Groups[1].Value;
            }
            catch (Exception)
            {
                // ignored
            }

            if (token != string.Empty)
            {
                var acc = SavedSteamAccount.Get().FirstOrDefault(a => a.Login == this.Guard.AccountName);
                if (acc != null)
                {
                    acc.TradeToken = token;
                    SavedSteamAccount.UpdateAll(SavedSteamAccount.Get());
                }
            }

            return !string.IsNullOrEmpty(token);
        }

        public bool SaveAccount(SteamGuardAccount account)
        {
            try
            {
                SavedSteamAccount.UpdateByLogin(account.AccountName, account);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error on session save", ex);
                return false;
            }
        }
    }
}