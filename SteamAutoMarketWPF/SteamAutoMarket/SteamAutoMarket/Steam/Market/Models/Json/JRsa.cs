namespace SteamAutoMarket.Steam.Market.Models.Json
{
    using Newtonsoft.Json;

    public class JRsa : JSuccess
    {
        [JsonProperty("publickey_exp")]
        public string Exponent { get; set; }

        [JsonProperty("publickey_mod")]
        public string Module { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("token_gid")]
        public string TokenGid { get; set; }
    }
}