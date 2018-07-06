using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using SteamAuth;
using Market;
using Market.Enums;
using Market.Models;
using Market.Models.Json;
using Market.Exceptions;
using System.Threading;
using SteamKit2;
using static autotrade.Steam.TradeOffer.Inventory;

namespace autotrade.Steam {
    public class SteamManager {
        public string ApiKey { get; set; }
        public static TradeOffer.OfferSession OfferSession { get; set; }
        public TradeOffer.Inventory Inventory { get; set; }
        public static UserLogin SteamClient { get; set; }
        public Market.Interface.MarketClient MarketClient { get; set; }
        public SteamGuardAccount Guard { get; set; }

        public SteamManager() {
        }

        public SteamManager(string login, string password, SteamGuardAccount mafile, string apiKey = null) {
            Guard = mafile;
            SteamClient = new UserLogin(login, password)
            {
                TwoFactorCode = Guard.GenerateSteamGuardCode()
            };

            LoginResult loginResult;
            do {
                loginResult = SteamClient.DoLogin();
                if (loginResult != LoginResult.LoginOkay) {
                    Utils.Logger.Warning($"Login status is - {loginResult}");
                    Thread.Sleep(3000);
                }
            }
            while (loginResult != LoginResult.LoginOkay);

            CookieContainer cookies = new CookieContainer();
            SteamClient.Session.AddCookies(cookies);
            Guard.RefreshSession();
            this.ApiKey = apiKey;
            // offerSession = new TradeOffer.OfferSession(new TradeOffer.TradeOfferWebAPI(this.apiKey), cookies, steamClient.Session.SessionID);
            var market = new SteamMarketHandler(ELanguage.English, "user-agent");
            Auth auth = new Auth(market, cookies)
            {
                IsAuthorized = true
            };
            market.Auth = auth;
            MarketClient = new Market.Interface.MarketClient(market);
            Inventory = new TradeOffer.Inventory();
        }

        public List<RgFullItem> LoadInventory(string steamid, string appid, string contextid) {
            return Inventory.GetInventory(new SteamID(ulong.Parse(steamid)), int.Parse(appid), int.Parse(contextid));
        }

        public void SellOnMarket(Dictionary<RgFullItem, double> items, WorkingProcess.MarketSaleType saleType) {
            RgInventory asset;
            RgDescription description;
            foreach (KeyValuePair<RgFullItem, double> item in items)
            {
                double? price = null;
                double amount;
                asset = item.Key.Asset;
                description = item.Key.Description;
                amount = item.Value;
                MarketItemInfo itemPageInfo = MarketClient.ItemPage(asset.appid, description.market_hash_name);
                switch (saleType)
                {
                    case WorkingProcess.MarketSaleType.LOWER_THAN_CURRENT:
                        ItemOrdersHistogram histogram = MarketClient.ItemOrdersHistogram(
                            itemPageInfo.NameId, "RU", ELanguage.Russian, 5);
                        price = histogram.MinSellPrice as double?;
                        if (price is null)
                        {
                            // Log error on ui
                            continue;
                        }
                        price -= amount;
                        break;

                    case WorkingProcess.MarketSaleType.MANUAL:
                        price = amount;
                        break;

                    case WorkingProcess.MarketSaleType.RECOMMENDED:
                        try
                        {
                            List<PriceHistoryDay> history = MarketClient
                                .PriceHistory(asset.appid, description.market_hash_name);
                            price = CountAveragePrice(history);
                        }
                        catch (SteamException err)
                        {
                            // Log error on ui
                            continue;
                        }
                        break;
                }
                if (price is null) continue;

                while (true)
                {
                    try
                    {
                        JSellItem resp = MarketClient.SellItem(description.appid, int.Parse(asset.contextid),
                                            long.Parse(asset.assetid), 1, (double)price * 0.87);

                        string message = resp.Message; // error message
                        if (message != null)
                        {
                            // log error message
                            Thread.Sleep(5000);
                            continue;
                        }
                        break;
                    }
                    catch (JsonSerializationException)
                    {
                        // log error
                        continue;
                    }
                }
            }

            // log confirming process
            Confirmation[] confirmations = Guard.FetchConfirmations();
            var marketConfirmations = confirmations
                .Where(item => item.ConfType == Confirmation.ConfirmationType.MarketSellTransaction)
                .ToArray();
            Guard.AcceptMultipleConfirmations(marketConfirmations);
        }

        private double CountAveragePrice(List<PriceHistoryDay> history)
        {
            double average = 0;
            // days are sorted from oldest to newest, we need the contrary
            history.Reverse();
            var FirstSevenDays = history.GetRange(0, 7);
            average = IterateHistory(FirstSevenDays, average);
            average = IterateHistory(FirstSevenDays, average);
            return average;
        }

        private double IterateHistory(List<PriceHistoryDay> history, double average)
        {
            double sum = 0;
            int count = 0;
            foreach (var item in history)
             {
                foreach (PriceHistoryItem data in item.History)
                {
                    if (average > 0)
                    {
                        // skip lowball or highball prices
                        if (data.Price < average / 2 || data.Price > average * 2)
                        {
                            continue;
                        }
                    }
                    sum += data.Price * data.Count;
                    count += data.Count;
                }
             }
            try
            {
                average = sum / count;
            } catch (DivideByZeroException)
            {
                average = 0;
            }
            return average;
        }
    }
}
