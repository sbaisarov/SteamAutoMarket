namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using Newtonsoft.Json;

    public class NewTradeOfferResponse
    {
        public bool Status { get; set; }

        [JsonProperty("strError")]
        public string TradeError { get; set; }

        [JsonProperty("tradeofferid")]
        public string TradeOfferId { get; set; }
    }
}