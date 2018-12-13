namespace SteamAutoMarket.Steam.Market.Models
{
    using System.Net;

    using RestSharp;

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