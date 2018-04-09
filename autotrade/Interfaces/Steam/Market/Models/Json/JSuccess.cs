using Newtonsoft.Json;

namespace Market.Models.Json
{
    public class JSuccess
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}