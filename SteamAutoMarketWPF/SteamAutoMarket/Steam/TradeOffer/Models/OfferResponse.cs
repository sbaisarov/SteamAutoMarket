namespace Steam.TradeOffer.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class OfferResponse
    {
        [JsonProperty("descriptions")]
        public List<AssetDescription> Descriptions { get; set; }

        [JsonProperty("offer")]
        public Offer Offer { get; set; }
    }
}