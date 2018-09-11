namespace SteamAutoMarket.WorkingProcess.MarketPriceFormation
{
    using System;
    using System.Collections.Generic;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;

    public class ItemsForSale
    {
        public ItemsForSale(IEnumerable<FullRgItem> items, double? price)
        {
            this.Items = items;
            if (price.HasValue)
            {
                this.Price = Math.Round(price.Value, 2);
            }
        }

        public IEnumerable<FullRgItem> Items { get; set; }

        public double? Price { get; set; }
    }
}