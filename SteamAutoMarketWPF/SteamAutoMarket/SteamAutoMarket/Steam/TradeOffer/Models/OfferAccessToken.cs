namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using Newtonsoft.Json;

    public class OfferAccessToken
    {
        [JsonProperty("trade_offer_access_token")]
        public string TradeOfferAccessToken { get; set; }
    }
}