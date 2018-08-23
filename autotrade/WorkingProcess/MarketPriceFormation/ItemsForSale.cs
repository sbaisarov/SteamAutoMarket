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

        public async Task<double?> GetPrice(Inventory.RgFullItem item, SteamManager manager)
        {
            double? price = null;
            switch (MarketSaleType)
            {
                case MarketSaleType.LOWER_THAN_CURRENT:
                    price = CURRENT_PRICES_CACHE.Get(item)?.Price;
                    if (price == null)
                    {
                        price = await manager.GetCurrentPrice(item.Asset, item.Description);
                        CURRENT_PRICES_CACHE.Cache(item.Description.market_hash_name, (double) price);
                    }

                    break;

                case MarketSaleType.LOWER_THAN_AVERAGE:
                    price = AVERAGE_PRICES_CACHE.Get(item)?.Price;
                    if (price == null)
                    {
                        price = manager.GetAveragePrice(item.Asset, item.Description);
                        AVERAGE_PRICES_CACHE.Cache(item.Description.market_hash_name, (double) price);
                    }
                    break;

                case MarketSaleType.RECOMENDED:
                    var currentPrice = CURRENT_PRICES_CACHE.Get(item)?.Price;
                    if (currentPrice == null)
                    {
                        currentPrice = await manager.GetCurrentPrice(item.Asset, item.Description);
                        CURRENT_PRICES_CACHE.Cache(item.Description.market_hash_name, (double) price);
                    }
                    
                    var averagePrice = AVERAGE_PRICES_CACHE.Get(item)?.Price;
                    if (averagePrice == null)
                    {
                        averagePrice = manager.GetAveragePrice(item.Asset, item.Description);
                        AVERAGE_PRICES_CACHE.Cache(item.Description.market_hash_name, (double) price);
                    }
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
