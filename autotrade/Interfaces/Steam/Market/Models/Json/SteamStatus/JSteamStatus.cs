using Newtonsoft.Json;

namespace Market.Models.Json.SteamStatus
{
    public class JSteamStatus
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("services")]
        public JServices Services { get; set; }
        [JsonProperty("time")]
        public long Time { get; set; }
        [JsonProperty("online")]
        public double Online { get; set; }
        [JsonProperty("online_info")]
        public string OnlineInfo { get; set; }
    }
}