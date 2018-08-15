using autotrade.Steam.TradeOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autotrade.WorkingProcess.MarketPriceFormation {
    public struct ItemsForSale {
        public readonly IEnumerable<Inventory.RgFullItem> items;
        public readonly double price;

        public ItemsForSale(IEnumerable<Inventory.RgFullItem> items, double price) {
            this.items = items;
            this.price = Math.Round(price, 2);
        }
    }
}
