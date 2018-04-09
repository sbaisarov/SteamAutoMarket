using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using SteamAuth;
using Market;
using Market.Models;
using Market.Models.Json;
using Market.Exceptions;
using Market.Enums;

namespace autotrade.Steam
{
    class Manager
    {
        public string apiKey;
        private CookieContainer cookies;
        private OfferSession offerSession;
        private Inventory inventory;
        private UserLogin steamClient;
        private Market.Interface.Client marketClient;
        SteamGuardAccount guard = new SteamGuardAccount();

        public Manager(string login, string password, string guardPath, string apiKey = null)
        {
            guard = JsonConvert.DeserializeObject<SteamGuardAccount>(File.ReadAllText(guardPath));
            steamClient = new UserLogin(login, password);
            steamClient.TwoFactorCode = guard.GenerateSteamGuardCode();
            steamClient.DoLogin();
            steamClient.Session.AddCookies(cookies);
            this.apiKey = apiKey;
            offerSession = new OfferSession(new TradeOfferWebAPI(this.apiKey), cookies, steamClient.Session.SessionID);
        }

        public List<PriceHistoryDay> PriceHistory(int appId, string hashName)
        {
            var url = Urls.Market + $"/pricehistory/?appid={appId}&market_hash_name={hashName}";

            var resp = SteamWeb.Request(url, "GET", data: null, referer: Urls.Market);

            var respDes = JsonConvert.DeserializeObject<JPriceHistory>(resp);

            if (!respDes.Success)
                throw new SteamException($"Connot get price history for [{hashName}]");

            IEnumerable<dynamic> prices = Enumerable.ToList(respDes.Prices);

            var list = prices
                .Select((g, index) =>
                {
                    int count = 0;
                    if (g[2] != null && !int.TryParse(g[2].ToString(), out count))
                        throw new SteamException($"Cannot parse items count in price history. Index: {index}");

                    double price = 0;
                    if (g[1] != null && !double.TryParse(g[1].ToString(), out price))
                        throw new SteamException($"Cannot parse item price in price history. Index: {index}");

                    return new PriceHistoryItem
                    {
                        DateTime = Utils.SteamTimeConvertor(g[0].ToString()),
                        Count = count,
                        Price = Math.Round(price, 2)
                    };
                })
                .GroupBy(x => x.DateTime.Date)
                .Select(g => new PriceHistoryDay
                {
                    Date = g.Key,
                    History = g.Select(p => p).ToList()
                })
                .ToList();

            return list;
        }

        public ItemOrdersHistogram ItemOrdersHistogram(int nameId, string country, ELanguage lang, int currency)
        {
            var url = Urls.Market +
                $"/itemordershistogram?country={country}&language={lang}&currency={currency}&item_nameid={nameId}";
            var resp = SteamWeb.Request(url, "GET", data: null, referer: Urls.Market);
            var respDes = JsonConvert.DeserializeObject<JItemOrdersHistogram>(resp);

            if (respDes.Success != 1)
                throw new SteamException("Cannot load orders histogram");

            var histogram = new ItemOrdersHistogram
            {
                SellOrderGraph = marketClient.ConvertOrderGraph(respDes.SellOrderGraph),
            };

            if (respDes.BuyOrderGraph != null)
            {
                histogram.BuyOrderGraph = marketClient.ConvertOrderGraph(respDes.BuyOrderGraph);
            }

            if (respDes.MinSellPrice != null)
                histogram.MinSellPrice = (double)respDes.MinSellPrice / 100;
            if (respDes.HighBuyOrder != null)
                histogram.HighBuyOrder = (double)respDes.HighBuyOrder / 100;

            return histogram;
        }

        public static double GetAvgSales(IEnumerable<PriceHistoryDay> priceHistory, int range)
        {
            var history = priceHistory.Where(w => w.Date > DateTime.Now.AddDays(range * -1)).ToList();
            var missedDays = Enumerable.Repeat(0, range - history.Count);

            if (!history.Any())
                return 0;

            var sallesPerDay = history
                .Select(s => s.History.Sum(sum => sum.Count))
                .Concat(missedDays);

            return Math.Floor(sallesPerDay.Average());
        }
    }

}
