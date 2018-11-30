namespace SteamAutoMarket.Steam.Market.Models.Json.SteamStatus
{
    using Newtonsoft.Json;

    public class JServices
    {
        [JsonProperty("cms")]
        public JBaseService Cms { get; set; }

        [JsonProperty("community")]
        public JBaseService Community { get; set; }

        [JsonProperty("online")]
        public JBaseService Online { get; set; }

        [JsonProperty("steam")]
        public JBaseService Steam { get; set; }

        [JsonProperty("Store")]
        public JBaseService Store { get; set; }

        [JsonProperty("webapi")]
        public JBaseService WebApi { get; set; }
    }
}