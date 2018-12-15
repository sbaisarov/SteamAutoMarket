namespace SteamAutoMarket.Steam.Market.Models.Json
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    [Serializable]
    public class JMyHistory : JSuccess
    {
        [JsonProperty("assets")]
        public Dictionary<int, Dictionary<long, Dictionary<long, JDescription>>> Assets { get; set; }

        [JsonProperty("hovers")]
        public string Hovers { get; set; }

        [JsonProperty("results_html")]
        public string Html { get; set; }

        [JsonProperty("total_count")]
        public int? TotalCount { get; set; }
    }
}