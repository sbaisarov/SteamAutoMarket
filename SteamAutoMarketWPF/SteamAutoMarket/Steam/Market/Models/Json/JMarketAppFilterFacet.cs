namespace Steam.Market.Models.Json
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class JMarketAppFilterFacet
    {
        [JsonProperty("tags")]
        public IDictionary<string, JMarketAppFilterTag> Tags { get; set; }
    }
}