using autotrade.Steam.TradeOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static autotrade.WorkingProcess.PriceLoader.PriceLoader;
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

        public async Task<double?> GetPrice(Inventory.RgFullItem item, SteamManager manager) {
            double? price = null;
            switch (MarketSaleType) {
                case MarketSaleType.LOWER_THAN_CURRENT:
                    price = CURRENT_PRICES_CACHE.Get(item)?.Price;
                    if (!price.HasValue) {
                        price = await manager.GetCurrentPrice(item.Asset, item.Description);
                        CURRENT_PRICES_CACHE.Cache(item.Description.market_hash_name, (double)price);
                    }

                    break;

                case MarketSaleType.LOWER_THAN_AVERAGE:
                    price = AVERAGE_PRICES_CACHE.Get(item)?.Price;
                    if (!price.HasValue) {
                        price = manager.GetAveragePrice(item.Asset, item.Description);
                        AVERAGE_PRICES_CACHE.Cache(item.Description.market_hash_name, (double)price);
                    }
                    break;

                case MarketSaleType.RECOMENDED:
                    var currentPrice = CURRENT_PRICES_CACHE.Get(item)?.Price;
                    if (!currentPrice.HasValue) {
                        currentPrice = await manager.GetCurrentPrice(item.Asset, item.Description);
                        CURRENT_PRICES_CACHE.Cache(item.Description.market_hash_name, (double)currentPrice);
                    }

                    var averagePrice = AVERAGE_PRICES_CACHE.Get(item)?.Price;
                    if (!averagePrice.HasValue) {
                        averagePrice = manager.GetAveragePrice(item.Asset, item.Description);
                        AVERAGE_PRICES_CACHE.Cache(item.Description.market_hash_name, (double)averagePrice);
                    }
                    if (!currentPrice.HasValue || !currentPrice.HasValue) {
                        price = null;
                    } else if (averagePrice > currentPrice) {
                        price = averagePrice;
                    } else if (currentPrice >= averagePrice) {
                        price = currentPrice - 0.01;
                    }

                    if (!price.HasValue || price <= 0 || price == double.NaN) {
                        price = null;
                    }

                    break;
            }

            return price;
        }
    }
}
