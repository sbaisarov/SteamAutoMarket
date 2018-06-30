using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using SteamAuth;
using Market;
using Market.Enums;
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
            int tryCount = 0;
            do {
                loginResult = SteamClient.DoLogin();
                if (loginResult != LoginResult.LoginOkay) {
                    Utils.Logger.Warning($"Login status is - {loginResult}");

                    if (++tryCount == 3) {
                        throw new WebException("Login failed after 3 attempts!");
                    }

                    Thread.Sleep(3000);
                }
            }
            while (loginResult != LoginResult.LoginOkay);

            CookieContainer cookies = new CookieContainer();
            SteamClient.Session.AddCookies(cookies);
            this.ApiKey = apiKey;
            // offerSession = new TradeOffer.OfferSession(new TradeOffer.TradeOfferWebAPI(this.apiKey), cookies, steamClient.Session.SessionID);
            SteamMarketHandler market = new SteamMarketHandler(ELanguage.English, "user-agent");
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
            //some logic
        }

        public void SendTradeOffer(List<RgFullItem> items, string partherId, string tradeToken) {
            //some logic
        }
    }
}
