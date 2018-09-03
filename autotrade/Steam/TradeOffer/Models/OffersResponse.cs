using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class OffersResponse
    {
        [JsonProperty("trade_offers_sent")] public List<Offer> TradeOffersSent { get; set; }

        [JsonProperty("trade_offers_received")]
        public List<Offer> TradeOffersReceived { get; set; }

        [JsonProperty("descriptions")] public List<AssetDescription> Descriptions { get; set; }

        public IEnumerable<Offer> AllOffers
        {
            get
            {
                if (TradeOffersSent == null) return TradeOffersReceived;

                return TradeOffersReceived == null ? TradeOffersSent : TradeOffersSent.Union(TradeOffersReceived);
            }
        }
    }
}