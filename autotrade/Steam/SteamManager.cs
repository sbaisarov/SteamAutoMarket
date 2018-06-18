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
    public class SteamManager
    {
        public string apiKey;
        public static TradeOffer.OfferSession offerSession;
        public TradeOffer.Inventory inventory = new TradeOffer.Inventory();
        public static UserLogin steamClient;
        public Market.Interface.Client marketClient;
        SteamGuardAccount guard = new SteamGuardAccount();

        public SteamManager()
        {
        }

        public SteamManager(string login, string password, string guardPath, string apiKey = null)
        {
            guard = JsonConvert.DeserializeObject<SteamGuardAccount>(File.ReadAllText(guardPath));
            steamClient = new UserLogin(login, password)
            {
                TwoFactorCode = guard.GenerateSteamGuardCode()
            };
            steamClient.DoLogin();
            CookieContainer cookies = new CookieContainer();
            steamClient.Session.AddCookies(cookies);
            this.apiKey = apiKey;
            // offerSession = new TradeOffer.OfferSession(new TradeOffer.TradeOfferWebAPI(this.apiKey), cookies, steamClient.Session.SessionID);
            Market.Steam market = new Market.Steam(ELanguage.English, "user-agent");
            Auth auth = new Auth(market, cookies)
            {
                IsAuthorized = true
            };
            market.Auth = auth;
            marketClient = new Market.Interface.Client(market);
        }
    }
}
