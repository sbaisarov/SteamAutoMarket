namespace Steam.Market.Models.Json
{
    using Newtonsoft.Json;

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