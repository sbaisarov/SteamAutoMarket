namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class NewTradeOfferResponse
    {
        public bool Status { get; set; }

        [JsonProperty("strError")]
        public string TradeError { get; set; }

        [JsonProperty("tradeofferid")]
        public string TradeOfferId { get; set; }
    }
}