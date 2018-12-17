namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class OfferAccessToken
    {
        [JsonProperty("trade_offer_access_token")]
        public string TradeOfferAccessToken { get; set; }
    }
}