namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using Newtonsoft.Json;

    public class MyRgInventory
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("id")]
        public string Assetid { get; set; }

        [JsonProperty("classid")]
        public string Classid { get; set; }

        [JsonProperty("instanceid")]
        public string Instanceid { get; set; }

        [JsonProperty("pos")]
        public int Position { get; set; }
    }
}