using System;
using autotrade.Steam.Market.Enums;
using autotrade.Steam.Market.Models.Json;

namespace autotrade.Steam.Market.Models
{
    public class HistoryItem
    {
        public string Game { get; set; }
        public JDescription Asset { get; set; }
        public string Name { get; set; }
        public string Member { get; set; }
        public double? Price { get; set; }
        public EMyHistoryActionType ActionType { get; set; }
        public DateTime? PlaceDate { get; set; }
        public DateTime? DealDate { get; set; }
    }
}