using autotrade.Steam.TradeOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using autotrade.Steam;

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

        public async Task<double?> GetPrice(Inventory.RgFullItem item, SteamManager manager)
        {
            double? price = null;
            switch (MarketSaleType)
            {
                case MarketSaleType.LOWER_THAN_CURRENT:
                    price = await manager.GetCurrentPrice(item.Asset, item.Description);
                    break;

                case MarketSaleType.LOWER_THAN_AVERAGE:
                    manager.GetAveragePrice(out double? priceValue, item.Asset, item.Description);
                    price = priceValue;
                    break;

                case MarketSaleType.RECOMENDED:
                    var currentPrice = await manager.GetCurrentPrice(item.Asset, item.Description);
                    manager.GetAveragePrice(out double? averagePrice, item.Asset, item.Description);

                    if (averagePrice == null || currentPrice == null) {
                        price = null;
                    } else if (averagePrice > currentPrice) {
                        price = averagePrice;
                    } else if (currentPrice >= averagePrice) {
                        price = currentPrice - 0.01;
                    }

                    if (price == null || price.Value <= 0) {
                        price = null;
                    }
                    break;                
            }

            return price;
        }
    }
}
