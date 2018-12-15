namespace SteamAutoMarket.Steam.Market.Models
{
    using System;

    [Serializable]
    public class HistoryItemHover
    {
        public int AppId { get; set; }

        public long AssetId { get; set; }

        public int ContextId { get; set; }

        public string HoverId { get; set; }
    }
}