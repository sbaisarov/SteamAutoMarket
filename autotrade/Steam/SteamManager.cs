namespace SteamAutoMarket.Steam
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

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
    using SteamAutoMarket.WorkingProcess.MarketPriceFormation;
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
            var index = 1;
            var itemsToConfirm = SavedSettings.Get().Settings_2FaItemsToConfirm;
            var total = items.ItemsForSaleList.Sum(x => x.Items.Count());
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
                            Program.WorkingProcessForm.AppendWorkingProcessInfo($"Price for '{itemName}' is {price}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("Error on market price parse", ex);
                        Program.WorkingProcessForm.AppendWorkingProcessInfo(
                            $"ERROR on selling '{itemName}' - {ex.Message}{ex.InnerException?.Message}");
                    }
                }

                foreach (var item in package.Items)
                {
                    try
                    {
                        Program.WorkingProcessForm.AppendWorkingProcessInfo(
                            $"[{index}/{total}] Selling - '{itemName}' for {package.Price}");
                        if (package.Price != null)
                        {
                            this.SellOnMarket(item, package.Price.Value);
                        }
                        else
                        {
                            throw new NullReferenceException($"Price for '{itemName}' is not loaded");
                        }

                        if (index % itemsToConfirm == 0)
                        {
                            this.ConfirmMarketTransactions();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("Error on market sell", ex);
                        Program.WorkingProcessForm.AppendWorkingProcessInfo(
                            $"ERROR on selling '{itemName}' - {ex.Message}{ex.InnerException?.Message}");
                    }

                    index++;
                }
            }

            this.ConfirmMarketTransactions();
        }

        public void SellOnMarket(FullRgItem item, double price)
        {
            var asset = item.Asset;
            var description = item.Description;
            while (true)
            {
                try
                {
                    JSellItem resp = this.MarketClient.SellItem(
                        description.Appid,
                        int.Parse(asset.Contextid),
                        long.Parse(asset.Assetid),
                        int.Parse(item.Asset.Amount),
                        price * 0.87);

                    var message = resp.Message; // error message
                    if (message != null)
                    {
                        Logger.Warning(message);
                        Program.WorkingProcessForm.AppendWorkingProcessInfo(
                            $"ERROR on selling '{item.Description.Name}' - {message}");

                        if (message.Contains("You already have a listing for this item pending confirmation"))
                        {
                            this.ConfirmMarketTransactions();
                            continue;
                        }

                        Thread.Sleep(5000);
                        continue;
                    }

                    break;
                }
                catch (JsonSerializationException ex)
                {
                    Logger.Warning(ex.Message);
                    Program.WorkingProcessForm.AppendWorkingProcessInfo(
                        $"ERROR on selling '{item.Description.Name}' - {ex.Message}{ex.InnerException?.Message}");
                }
            }
        }

        public void ConfirmMarketTransactions()
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

        public double? GetAveragePrice(RgInventory asset, RgDescription description, int days)
        {
            double? price = null;
            var attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    var history = this.MarketClient.PriceHistory(asset.Appid, description.MarketHashName);
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
                        Logger.Warning($"Error on getting average price of {description.MarketHashName}", ex);
                    }
                }
            }

            return price;
        }

        public async Task<double?> GetCurrentPrice(RgInventory asset, RgDescription description)
        {
            var itemPageInfo = MarketInfoCache.Get(asset.Appid, description.MarketHashName);

            if (itemPageInfo == null)
            {
                itemPageInfo = this.MarketClient.ItemPage(asset.Appid, description.MarketHashName);
                MarketInfoCache.Cache(asset.Appid, description.MarketHashName, itemPageInfo);
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
                        Logger.Warning($"Error on getting current price of {description.MarketHashName}", ex);
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
            var token = Regex.Match(response, @"https://steamcommunity\.com/tradeoffer/new/\?partner=.+&token=(.+?)\b")
                .Groups[1].Value;

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