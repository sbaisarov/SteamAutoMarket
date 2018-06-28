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
            this.ApiKey = apiKey;
            // offerSession = new TradeOffer.OfferSession(new TradeOffer.TradeOfferWebAPI(this.apiKey), cookies, steamClient.Session.SessionID);
            Market.SteamMarketHandler market = new SteamMarketHandler(ELanguage.English, "user-agent");
            Auth auth = new Auth(market, cookies)
            {
                IsAuthorized = true
            };
            market.Auth = auth;
            MarketClient = new Market.Interface.MarketClient(market);
            Inventory = new TradeOffer.Inventory();
        }

        public RgFullItem(string steamid, string appid, string contextid)
        {

        }
    }
}
