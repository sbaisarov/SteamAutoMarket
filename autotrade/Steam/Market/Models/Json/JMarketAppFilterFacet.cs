using System.Collections.Generic;
using Newtonsoft.Json;

namespace SteamAutoMarket.Steam.Market.Models.Json
{
    public class JMarketAppFilterFacet
    {
        [JsonProperty("tags")] public IDictionary<string, JMarketAppFilterTag> Tags { get; set; }
    }
}