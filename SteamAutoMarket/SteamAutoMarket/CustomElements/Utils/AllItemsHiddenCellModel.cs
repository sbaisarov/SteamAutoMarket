namespace SteamAutoMarket.CustomElements.Utils
{
    using System.Collections.Generic;
    using System.Drawing;

    using SteamAutoMarket.Steam.TradeOffer.Models.Full;

    internal class AllItemsHiddenCellModel
    {
        public string HashName { get; set; }

        public List<FullRgItem> ItemsList { get; set; } = new List<FullRgItem>();

        public Image Image { get; set; }
    }
}