using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using SteamAuth;
using Market;
using Market.Enums;
using Market.Models;
using Market.Models.Json;
using Market.Exceptions;
using System.Threading;
using System.Web.Caching;
using SteamKit2;
using autotrade.Steam.TradeOffer;
using Market.Interface;
using autotrade.Steam.Market;
using autotrade.Utils;
using autotrade.WorkingProcess;
using autotrade.WorkingProcess.Settings;
using autotrade.WorkingProcess.MarketPriceFormation;
using autotrade.WorkingProcess.PriceLoader;

namespace autotrade.Steam {
    public class SteamManager {
        public string ApiKey { get; set; }
        public OfferSession OfferSession { get; set; }
        public TradeOfferWebAPI TradeOfferWeb { get; }
        public Inventory Inventory { get; set; }
        public UserLogin SteamClient { get; set; }
        public MarketClient MarketClient { get; set; }
        public SteamGuardAccount Guard { get; set; }

        public SteamManager() {
        }

        public SteamManager(string login, string password, SteamGuardAccount mafile, string apiKey) {
            Guard = mafile;
            SteamClient = new UserLogin(login, password)
            {
                TwoFactorCode = Guard.GenerateSteamGuardCode()
            };

            bool isSessionRefreshed = Guard.RefreshSession();
            if (isSessionRefreshed == false) {
                Logger.Debug($"Saved steam session for {login} is expired. Refreshing session.");
                LoginResult loginResult;
                int tryCount = 0;
                do {
                    loginResult = SteamClient.DoLogin();
                    if (loginResult != LoginResult.LoginOkay) {
                        Logger.Warning($"Login status is - {loginResult}");

                        if (++tryCount == 3) {
                            throw new WebException("Login failed after 3 attempts!");
                        }

                        Thread.Sleep(3000);
                    }
                }
                while (loginResult != LoginResult.LoginOkay);

                Guard.Session = SteamClient.Session;
                SaveAccount(Guard);
            }

            this.ApiKey = apiKey;

            CookieContainer cookies = new CookieContainer();
            TradeOfferWeb = new TradeOfferWebAPI(apiKey);
            OfferSession = new OfferSession(TradeOfferWeb, cookies, Guard.Session.SessionID);
            Guard.Session.AddCookies(cookies);
            var market = new SteamMarketHandler(ELanguage.English, "user-agent");
            Auth auth = new Auth(market, cookies)
            {
                IsAuthorized = true
            };

            market.Auth = auth;
            MarketClient = new MarketClient(market);
            Inventory = new Inventory();
        }

        public List<Inventory.RgFullItem> LoadInventory(string steamid, string appid, string contextid, bool withLogs = false) {
            if (withLogs) {
                return Inventory.GetInventoryWithLogs(new SteamID(ulong.Parse(steamid)), int.Parse(appid), int.Parse(contextid));
            } else {
                return Inventory.GetInventory(new SteamID(ulong.Parse(steamid)), int.Parse(appid), int.Parse(contextid));
            }
        }

        public void SellOnMarket(ToSaleObject items) {
            int index = 1;
            int itemsToConfirm = SavedSettings.Get().SETTINGS_2FA_ITEMS_TO_CONFIRM;
            int total = items.ItemsForSaleList.Sum(x => x.Items.Count());
            string itemName;
            foreach (var package in items.ItemsForSaleList) {
                itemName = package.Items.First().Description.name;
                if (!package.Price.HasValue) {
                    try {
                        Task<double?> task = Task.Run(async () => await items.GetPrice(package.Items.First(), this));
                        Program.WorkingProcessForm.AppendWorkingProcessInfo($"Processing price for '{itemName}'");
                        task.Wait();

                        var price = task.Result;
                        if (price != null) {
                            package.Price = price;
                            Program.WorkingProcessForm.AppendWorkingProcessInfo($"Price for '{itemName}' is {price}");
                        }
                    } catch (Exception ex) {
                        Logger.Error("Erron on market price parse", ex);
                        Program.WorkingProcessForm.AppendWorkingProcessInfo($"ERROR on selling {itemName} - {ex.Message}");
                    }
                }

                foreach (var item in package.Items) {
                    try {
                        Program.WorkingProcessForm.AppendWorkingProcessInfo($"[{index}/{total}] Selling - '{itemName}' for {package.Price}");
                        SellOnMarket(item, package.Price.Value);
                        if (index % itemsToConfirm == 0) {
                            ConfirmMarketTransactions();
                        }
                    } catch (Exception ex) {
                        Logger.Error("Erron on market sell", ex);
                        Program.WorkingProcessForm.AppendWorkingProcessInfo($"ERROR on selling '{itemName}' - {ex.Message}");
                    }
                    index++;
                }
            }

            ConfirmMarketTransactions();
        }

        public void SellOnMarket(Inventory.RgFullItem item, double price) {
            Inventory.RgInventory asset = item.Asset;
            Inventory.RgDescription description = item.Description;
            while (true) {
                try {
                    JSellItem resp = MarketClient.SellItem(description.appid, int.Parse(asset.contextid),
                        long.Parse(asset.assetid), 1, price * 0.87);

                    string message = resp.Message; // error message
                    if (message != null) {
                        Logger.Warning(message);
                        Program.WorkingProcessForm.AppendWorkingProcessInfo($"ERROR on selling {item.Description.name} - {message}");

                        if (message.Contains("You already have a listing for this item pending confirmation")) {
                            ConfirmMarketTransactions();
                            continue;
                        }

                        Thread.Sleep(5000);
                        continue;
                    }
                    break;
                } catch (JsonSerializationException ex) {
                    Logger.Warning(ex.Message);
                    Program.WorkingProcessForm.AppendWorkingProcessInfo($"ERROR on selling {item.Description.name} - {ex.Message}");
                }
            }
        }

        public void ConfirmMarketTransactions() {
            Program.WorkingProcessForm.AppendWorkingProcessInfo($"Fetching confirmations");
            Confirmation[] confirmations = Guard.FetchConfirmations();
            var marketConfirmations = confirmations
                .Where(item => item.ConfType == Confirmation.ConfirmationType.MarketSellTransaction)
                .ToArray();
            Program.WorkingProcessForm.AppendWorkingProcessInfo($"Accepting confirmations");
            Guard.AcceptMultipleConfirmations(marketConfirmations);
        }

        public double? GetAveragePrice(Inventory.RgInventory asset, Inventory.RgDescription description) {
            double? price = null;
            var attempts = 0;
            while (attempts < 3) {
                try {
                    var history = MarketClient.PriceHistory(asset.appid, description.market_hash_name);
                    price = Math.Round(CountAveragePrice(history), 2);
                    break;
                } catch (Exception ex) {
                    Logger.Warning($"Error on geting average price of ${description.market_hash_name}", ex);
                }

                attempts++;
            }

            return price;
        }

        public async Task<double?> GetCurrentPrice(Inventory.RgInventory asset, Inventory.RgDescription description) {
            MarketItemInfo itemPageInfo = MarketInfoCache.Get(asset.appid, description.market_hash_name);

            if (itemPageInfo == null) {
                itemPageInfo = MarketClient.ItemPage(asset.appid, description.market_hash_name);
                MarketInfoCache.Cache(asset.appid, description.market_hash_name, itemPageInfo);
            }

            var attempts = 0;
            double? price = null;
            while (attempts < 3) {
                ItemOrdersHistogram histogram = await MarketClient.ItemOrdersHistogramAsync(
                    itemPageInfo.NameId, "RU", ELanguage.Russian, 5);
                price = histogram.MinSellPrice;
                if (price is null) {
                    Logger.Warning($"Error on geting current price of ${description.market_hash_name}");
                    attempts++;
                    continue;
                }

                break;
            }
            return price;
        }

        private double CountAveragePrice(List<PriceHistoryDay> history) {
            // days are sorted from oldest to newest, we need the contrary
            history.Reverse();
            var firstSevenDays = history.GetRange(0, 7);
            var average = IterateHistory(firstSevenDays);
            if (average is null) throw new SteamException("No prices recorded during the week");
            average = IterateHistory(firstSevenDays, average);
            return (double)average;
        }

        private double? IterateHistory(List<PriceHistoryDay> history, double? average = null) {
            double sum = 0;
            int count = 0;
            foreach (var item in history) {
                foreach (PriceHistoryItem data in item.History) {
                    if (!(average is null)) {
                        // skip lowball and highball prices
                        if (data.Price < average / 2 || data.Price > average * 2) {
                            continue;
                        }
                    }
                    sum += data.Price * data.Count;
                    count += data.Count;
                }
            }

            double? result = sum / count;
            if (!(double.IsNaN((double)result))) return result;
            result = null;
            if (!(average is null)) result = average;

            return result;
        }

        public void SendTradeOffer(List<Inventory.RgFullItem> items, string partnerId, string tradeToken) {
            TradeOffer.TradeOffer offer = new TradeOffer.TradeOffer(OfferSession, new SteamID(ulong.Parse(partnerId)));
            bool status = offer.SendWithToken(out string offerId, tradeToken);
            if (status is false) {
                Utils.Logger.Info(offer.Session.Error);
            }
        }

        public void ConfirmTradeTransactions(List<ulong> offerids) {
            Confirmation[] confirmations = Guard.FetchConfirmations();
            var conf = confirmations
                .Where(item => item.ConfType == Confirmation.ConfirmationType.Trade
                               && offerids.Contains(item.Creator))
                .ToArray()[0];
            Guard.AcceptConfirmation(conf);
        }

        public OffersResponse ReceiveTradeOffers() {
            return TradeOfferWeb.GetActiveTradeOffers(false, true, true);
        }

        public void AcceptOffers(IEnumerable<Offer> offers) {
            foreach (var offer in offers) {
                OfferSession.Accept(offer.TradeOfferId);
            }
        }

        public bool SaveAccount(SteamGuardAccount account) {
            try {
                SavedSteamAccount.UpdateByLogin(account.AccountName, account);
                return true;
            } catch (Exception ex) {
                Utils.Logger.Error("Error on session save", ex);
                return false;
            }
        }
    }
}
