using Newtonsoft.Json;

namespace Market.Models.Json
{
    public class JSuccessInt
    {
        [JsonProperty("success")]
        public int Success { get; set; }
    }
}