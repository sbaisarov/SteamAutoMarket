namespace SteamAutoMarket.Steam.Market.Models
{
    using System;

    using SteamAutoMarket.Steam.Market.Enums;
    using SteamAutoMarket.Steam.Market.Models.Json;

    public class HistoryItem
    {
        public EMyHistoryActionType ActionType { get; set; }

        public JDescription Asset { get; set; }

        public DateTime? DealDate { get; set; }

        public string Game { get; set; }

        public string Member { get; set; }

        public string Name { get; set; }

        public DateTime? PlaceDate { get; set; }

        public double? Price { get; set; }
    }
}