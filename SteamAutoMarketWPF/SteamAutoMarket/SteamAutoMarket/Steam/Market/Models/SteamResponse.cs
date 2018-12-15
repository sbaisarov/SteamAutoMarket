namespace SteamAutoMarket.Steam.Market.Models
{
    using System;
    using System.Net;

    using RestSharp;

    [Serializable]
    public class SteamResponse
    {
        public SteamResponse(IRestResponse data, CookieContainer cookieContainer)
        {
            this.Data = data;
            this.CookieContainer = cookieContainer;
        }

        public CookieContainer CookieContainer { get; }

        public IRestResponse Data { get; }
    }
}