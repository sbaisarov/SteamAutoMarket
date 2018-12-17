namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    [Serializable]
    public class OfferResponse
    {
        [JsonProperty("descriptions")]
        public List<AssetDescription> Descriptions { get; set; }

        [JsonProperty("offer")]
        public Offer Offer { get; set; }
    }
}