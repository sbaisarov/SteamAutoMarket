using Newtonsoft.Json;

namespace Market.Models.Json
{
    public class JMarketSearch : JSuccess
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("pagesize")]
        public int PageSize { get; set; }

        [JsonProperty("results_html")]
        public string ResultsHtml { get; set; }
    }
}