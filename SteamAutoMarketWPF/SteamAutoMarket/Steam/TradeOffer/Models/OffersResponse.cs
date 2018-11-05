namespace Steam.TradeOffer.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    public class OffersResponse
    {
        public IEnumerable<Offer> AllOffers
        {
            get
            {
                if (this.TradeOffersSent == null) return this.TradeOffersReceived;

                return this.TradeOffersReceived == null
                           ? this.TradeOffersSent
                           : this.TradeOffersSent.Union(this.TradeOffersReceived);
            }
        }

        [JsonProperty("descriptions")]
        public List<AssetDescription> Descriptions { get; set; }

        [JsonProperty("trade_offers_received")]
        public List<Offer> TradeOffersReceived { get; set; }

        [JsonProperty("trade_offers_sent")]
        public List<Offer> TradeOffersSent { get; set; }
    }
}