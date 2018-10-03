using System.Collections.Generic;
using Newtonsoft.Json;

namespace SteamAutoMarket.Steam.Market.Models.Json
{
    public class JMyHistory : JSuccess
    {
        [JsonProperty("total_count")] public int? TotalCount { get; set; }

        [JsonProperty("results_html")] public string Html { get; set; }

        [JsonProperty("assets")]
        public Dictionary<int, Dictionary<long, Dictionary<long, JDescription>>> Assets { get; set; }

        [JsonProperty("hovers")] public string Hovers { get; set; }
    }
}