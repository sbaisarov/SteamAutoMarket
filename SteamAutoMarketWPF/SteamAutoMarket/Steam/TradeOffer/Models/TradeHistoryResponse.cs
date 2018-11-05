namespace Steam.TradeOffer.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class TradeHistoryResponse
    {
        [JsonProperty("descriptions")]
        public List<AssetDescription> Descriptions { get; set; }

        [JsonProperty("more")]
        public bool More { get; set; }

        [JsonProperty("total_trades")]
        public int TotalTrades { get; set; }

        [JsonProperty("trades")]
        public List<TradeHistoryItem> Trades { get; set; }
    }
}