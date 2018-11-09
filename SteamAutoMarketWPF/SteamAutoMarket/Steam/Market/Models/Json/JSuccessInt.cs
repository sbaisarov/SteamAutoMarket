namespace Steam.Market.Models.Json
{
    using Newtonsoft.Json;

    public class JSuccessInt
    {
        [JsonProperty("success")]
        public int Success { get; set; }
    }
}