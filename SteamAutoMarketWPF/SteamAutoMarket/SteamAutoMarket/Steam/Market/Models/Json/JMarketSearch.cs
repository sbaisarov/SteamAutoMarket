namespace SteamAutoMarket.Steam.Market.Models.Json
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class JMarketSearch : JSuccess
    {
        [JsonProperty("pagesize")]
        public int PageSize { get; set; }

        [JsonProperty("results_html")]
        public string ResultsHtml { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
    }
}