namespace SteamAutoMarket.Steam.Market.Models.Json
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    [Serializable]
    public class JMarketAppFilterFacet
    {
        [JsonProperty("tags")]
        public IDictionary<string, JMarketAppFilterTag> Tags { get; set; }
    }
}