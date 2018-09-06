using Newtonsoft.Json;

namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    public class NewTradeOfferResponse
    {
        [JsonProperty("tradeofferid")] public string TradeOfferId { get; set; }

        [JsonProperty("strError")] public string TradeError { get; set; }

        public bool Status { get; set; }
    }
}