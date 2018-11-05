namespace Steam.Market.Models.Json
{
    using Newtonsoft.Json;

    public class JSuccess
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}