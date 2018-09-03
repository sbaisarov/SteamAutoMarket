using System.Collections.Generic;
using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class OfferResponse
    {
        [JsonProperty("offer")] public Offer Offer { get; set; }

        [JsonProperty("descriptions")] public List<AssetDescription> Descriptions { get; set; }
    }
}