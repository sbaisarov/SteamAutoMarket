namespace SteamAutoMarket.Steam.Market.Models.Json
{
    using Newtonsoft.Json;

    public class JMyListings : JSuccess
    {
        [JsonProperty("num_active_listings")]
        public int ActiveListingsCount { get; set; }

        [JsonProperty("results_html")]
        public string ResultsHtml { get; set; }
    }
}