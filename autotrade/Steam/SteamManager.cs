using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using autotrade.Steam.Market;
using autotrade.Steam.Market.Enums;
using autotrade.Steam.Market.Exceptions;
using autotrade.Steam.Market.Interface;
using autotrade.Steam.Market.Models;
using autotrade.Steam.Market.Models.Json;
using autotrade.Steam.TradeOffer;
using autotrade.Steam.TradeOffer.Models;
using autotrade.Steam.TradeOffer.Models.Full;
using autotrade.Utils;
using autotrade.WorkingProcess.MarketPriceFormation;
using autotrade.WorkingProcess.Settings;
using Newtonsoft.Json;
using SteamAuth;
using SteamKit2;

namespace autotrade.Steam
{
    public class SteamManager
    {
        public SteamManager()
        {
        }

        public SteamManager(string login, string password, SteamGuardAccount mafile, string apiKey)
        {
            Guard = mafile;
            SteamClient = new UserLogin(login, password)
            {
                TwoFactorCode = Guard.GenerateSteamGuardCode()
            };

            var isSessionRefreshed = Guard.RefreshSession();
            if (isSessionRefreshed == false)
            {
                Logger.Debug($"Saved steam session for {login} is expired. Refreshing session.");
                LoginResult loginResult;
                var tryCount = 0;
                do
                {
                    loginResult = SteamClient.DoLogin();
                    if (loginResult != LoginResult.LoginOkay)
                    {
                        Logger.Warning($"Login status is - {loginResult}");

                        if (++tryCount == 3) throw new WebException("Login failed after 3 attempts!");

                        Thread.Sleep(3000);
                    }
                } while (loginResult != LoginResult.LoginOkay);

                Guard.Session = SteamClient.Session;
                SaveAccount(Guard);
            }

            ApiKey = apiKey;

            TradeOfferWeb = new TradeOfferWebAPI(apiKey);
            OfferSession = new OfferSession(TradeOfferWeb, Cookies, Guard.Session.SessionID);
            Guard.Session.AddCookies(Cookies);
            var market = new SteamMarketHandler(ELanguage.English, "user-agent");
            var auth = new Auth(market, Cookies)
            {
                IsAuthorized = true
            };

            FetchTradeToken();
            market.Auth = auth;
            MarketClient = new MarketClient(market);
            Inventory = new Inventory();
        }

        public string ApiKey { get; set; }
        public OfferSession OfferSession { get; set; }
        public TradeOfferWebAPI TradeOfferWeb { get; }
        public Inventory Inventory { get; set; }
        public UserLogin SteamClient { get; set; }
        public MarketClient MarketClient { get; set; }
        public SteamGuardAccount Guard { get; set; }
        public CookieContainer Cookies { get; set; } = new CookieContainer();

        public List<FullRgItem> LoadInventory(string steamid, string appid, string contextid, bool withLogs = false)
        {
            if (withLogs)
                return Inventory.GetInventoryWithLogs(new SteamID(ulong.Parse(steamid)), int.Parse(appid),
                    int.Parse(contextid));
            return Inventory.GetInventory(new SteamID(ulong.Parse(steamid)), int.Parse(appid), int.Parse(contextid));
        }

        public void SellOnMarket(ToSaleObject items)
        {
            var index = 1;
            var itemsToConfirm = SavedSettings.Get().SETTINGS_2FA_ITEMS_TO_CONFIRM;
            var total = items.ItemsForSaleList.Sum(x => x.Items.Count());
            string itemName;
            foreach (var package in items.ItemsForSaleList)
            {
                itemName = package.Items.First().Description.Name;
                if (!package.Price.HasValue)
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
                        Logger.Error("Erron on market price parse", ex);
                        Program.WorkingProcessForm.AppendWorkingProcessInfo(
                            $"ERROR on selling {itemName} - {ex.Message}");
                    }

                foreach (var item in package.Items)
                {
                    try
                    {
                        Program.WorkingProcessForm.AppendWorkingProcessInfo(
                            $"[{index}/{total}] Selling - '{itemName}' for {package.Price}");
                        SellOnMarket(item, package.Price.Value);
                        if (index % itemsToConfirm == 0) ConfirmMarketTransactions();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("Erron on market sell", ex);
                        Program.WorkingProcessForm.AppendWorkingProcessInfo(
                            $"ERROR on selling '{itemName}' - {ex.Message}");
                    }

                    index++;
                }
            }

            ConfirmMarketTransactions();
        }

        public void SellOnMarket(FullRgItem item, double price)
        {
            var asset = item.Asset;
            var description = item.Description;
            while (true)
                try
                {
                    JSellItem resp = MarketClient.SellItem(description.Appid, int.Parse(asset.Contextid),
                        long.Parse(asset.Assetid), int.Parse(item.Asset.Amount), price * 0.87);

                    var message = resp.Message; // error message
                    if (message != null)
                    {
                        Logger.Warning(message);
                        Program.WorkingProcessForm.AppendWorkingProcessInfo(
                            $"ERROR on selling {item.Description.Name} - {message}");

                        if (message.Contains("You already have a listing for this item pending confirmation"))
                        {
                            ConfirmMarketTransactions();
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
                        $"ERROR on selling {item.Description.Name} - {ex.Message}");
                }
        }

        public void ConfirmMarketTransactions()
        {
            Program.WorkingProcessForm.AppendWorkingProcessInfo("Fetching confirmations");
            try
            {
                var confirmations = Guard.FetchConfirmations();
                var marketConfirmations = confirmations
                    .Where(item => item.ConfType == Confirmation.ConfirmationType.MarketSellTransaction)
                    .ToArray();
                Program.WorkingProcessForm.AppendWorkingProcessInfo("Accepting confirmations");
                Guard.AcceptMultipleConfirmations(marketConfirmations);
            }
            catch (SteamGuardAccount.WGTokenExpiredException)
            {
                Program.WorkingProcessForm.AppendWorkingProcessInfo("Session expired. Updating...");
                Guard.RefreshSession();
                ConfirmMarketTransactions();
            }
        }

        public double? GetAveragePrice(RgInventory asset, RgDescription description)
        {
            double? price = null;
            var attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    var history = MarketClient.PriceHistory(asset.Appid, description.MarketHashName);
                    price = Math.Round(CountAveragePrice(history), 2);
                    break;
                }
                catch (Exception ex)
                {
                    Logger.Warning($"Error on geting average price of ${description.MarketHashName}", ex);
                }

                attempts++;
            }

            return price;
        }

        public async Task<double?> GetCurrentPrice(RgInventory asset, RgDescription description)
        {
            var itemPageInfo = MarketInfoCache.Get(asset.Appid, description.MarketHashName);

            if (itemPageInfo == null)
            {
                itemPageInfo = MarketClient.ItemPage(asset.Appid, description.MarketHashName);
                MarketInfoCache.Cache(asset.Appid, description.MarketHashName, itemPageInfo);
            }

            var attempts = 0;
            double? price = null;
            while (attempts < 3)
            {
                var histogram = await MarketClient.ItemOrdersHistogramAsync(
                    itemPageInfo.NameId, "RU", ELanguage.Russian, 5);
                price = histogram.MinSellPrice;
                if (price is null)
                {
                    Logger.Warning($"Error on geting current price of ${description.MarketHashName}");
                    attempts++;
                    continue;
                }

                break;
            }

            return price;
        }

        private double CountAveragePrice(List<PriceHistoryDay> history)
        {
            // days are sorted from oldest to newest, we need the contrary
            history.Reverse();
            var firstSevenDays = history.GetRange(0, 7);
            var average = IterateHistory(firstSevenDays);
            if (average is null) throw new SteamException("No prices recorded during the week");
            average = IterateHistory(firstSevenDays, average);
            return (double) average;
        }

        private double? IterateHistory(List<PriceHistoryDay> history, double? average = null)
        {
            double sum = 0;
            var count = 0;
            foreach (var item in history)
            foreach (var data in item.History)
            {
                if (!(average is null))
                    if (data.Price < average / 2 || data.Price > average * 2)
                        continue;
                sum += data.Price * data.Count;
                count += data.Count;
            }

            double? result = sum / count;
            if (!double.IsNaN((double) result)) return result;
            result = null;
            if (!(average is null)) result = average;

            return result;
        }

        public void SendTradeOffer(List<FullRgItem> items, string partnerId, string tradeToken)
        {
            var offer = new TradeOffer.TradeOffer(OfferSession, new SteamID(ulong.Parse(partnerId)));
            var status = offer.SendWithToken(out var offerId, tradeToken);
            if (status is false) Logger.Info(offer.Session.Error);
        }

        public void ConfirmTradeTransactions(List<ulong> offerids)
        {
            try
            {
                var confirmations = Guard.FetchConfirmations();
                var conf = confirmations
                    .Where(item => item.ConfType == Confirmation.ConfirmationType.Trade
                                   && offerids.Contains(item.Creator))
                    .ToArray()[0];
                Guard.AcceptConfirmation(conf);
            }
            catch (SteamGuardAccount.WGTokenExpiredException)
            {
                Program.WorkingProcessForm.AppendWorkingProcessInfo("Session expired. Updating...");
                Guard.RefreshSession();
                ConfirmTradeTransactions(offerids);
            }
        }

        public OffersResponse ReceiveTradeOffers()
        {
            return TradeOfferWeb.GetActiveTradeOffers(false, true, true);
        }

        public void AcceptOffers(IEnumerable<Offer> offers)
        {
            foreach (var offer in offers) OfferSession.Accept(offer.TradeOfferId);
        }

        public bool FetchTradeToken()
        {
            var response = SteamWeb.Request("https://steamcommunity.com/my/tradeoffers/privacy", "GET", "", Cookies);
            var token = Regex.Match(response,
                @"https://steamcommunity\.com/tradeoffer/new/\?partner=.+&token=(.+?)\b").Groups[1].Value;
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