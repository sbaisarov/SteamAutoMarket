using System.Collections.Generic;
using Newtonsoft.Json;

namespace Market.Models.Json
{
    public class JMarketAppFilterFacet
    {
        [JsonProperty("tags")]
        public IDictionary<string, JMarketAppFilterTag> Tags { get; set; }
    }
}