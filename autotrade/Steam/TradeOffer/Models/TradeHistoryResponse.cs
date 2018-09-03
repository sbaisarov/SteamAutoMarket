using System.Collections.Generic;
using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class TradeHistoryResponse
    {
        [JsonProperty("total_trades")] public int TotalTrades { get; set; }

        [JsonProperty("more")] public bool More { get; set; }

        [JsonProperty("trades")] public List<TradeHistoryItem> Trades { get; set; }

        [JsonProperty("descriptions")] public List<AssetDescription> Descriptions { get; set; }
    }
}