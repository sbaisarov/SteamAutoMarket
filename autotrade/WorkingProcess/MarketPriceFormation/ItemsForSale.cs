using autotrade.Steam.TradeOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autotrade.WorkingProcess.MarketPriceFormation {
    public class ItemsForSale {
        public IEnumerable<Inventory.RgFullItem> Items { get; set; }
        public double? Price { get; set; } = null;

        public ItemsForSale(IEnumerable<Inventory.RgFullItem> items, double? price) {
            this.Items = items;
            if (price.HasValue) this.Price = Math.Round(price.Value, 2);
        }
    }

    public class ToSaleObject {
        public List<ItemsForSale> ItemsForSaleList { get; set; }
        public MarketSaleType MarketSaleType { get; set; }
        public ChangeValueType ChangeValueType { get; set; }
        public double ChangeValue { get; set; }

        public ToSaleObject(List<ItemsForSale> itemsForSales, MarketSaleType marketSaleType, ChangeValueType changeValueType, double changeValue) {
            ItemsForSaleList = itemsForSales;
            MarketSaleType = marketSaleType;
            ChangeValueType = changeValueType;
            ChangeValue = changeValue;
        }
    }
}
