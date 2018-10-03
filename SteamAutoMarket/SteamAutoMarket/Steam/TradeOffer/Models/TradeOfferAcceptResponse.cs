using Newtonsoft.Json;

namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    public class TradeOfferAcceptResponse
    {
        public TradeOfferAcceptResponse()
        {
            TradeId = string.Empty;
            TradeError = string.Empty;
        }

        public bool Accepted { get; set; }

        [JsonProperty("tradeid")] public string TradeId { get; set; }

        [JsonProperty("strError")] public string TradeError { get; set; }
    }
}