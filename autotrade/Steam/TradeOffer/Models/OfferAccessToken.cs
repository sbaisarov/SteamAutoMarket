using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class OfferAccessToken
    {
        [JsonProperty("trade_offer_access_token")]
        public string TradeOfferAccessToken { get; set; }
    }
}