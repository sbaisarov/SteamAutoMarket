namespace Steam.Market.Models.Json.SteamStatus
{
    using Newtonsoft.Json;

    public class JBaseService
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}