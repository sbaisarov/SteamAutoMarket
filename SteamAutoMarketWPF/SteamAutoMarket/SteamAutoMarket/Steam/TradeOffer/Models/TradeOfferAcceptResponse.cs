namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class TradeOfferAcceptResponse
    {
        public TradeOfferAcceptResponse()
        {
            this.TradeId = string.Empty;
            this.TradeError = string.Empty;
        }

        public bool Accepted { get; set; }

        [JsonProperty("strError")]
        public string TradeError { get; set; }

        [JsonProperty("tradeid")]
        public string TradeId { get; set; }
    }
}