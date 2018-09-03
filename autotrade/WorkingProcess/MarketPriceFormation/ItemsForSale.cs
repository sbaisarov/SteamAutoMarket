using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using autotrade.Steam;
using autotrade.Steam.TradeOffer.Models.Full;
using static autotrade.WorkingProcess.PriceLoader.PriceLoader;

namespace autotrade.WorkingProcess.MarketPriceFormation
{
    public class ItemsForSale
    {
        public ItemsForSale(IEnumerable<FullRgItem> items, double? price)
        {
            Items = items;
            if (price.HasValue) Price = Math.Round(price.Value, 2);
        }

        public IEnumerable<FullRgItem> Items { get; set; }
        public double? Price { get; set; }
    }

    public class ToSaleObject
    {
        public ToSaleObject(List<ItemsForSale> itemsForSales, MarketSaleType marketSaleType,
            ChangeValueType changeValueType, double changeValue)
        {
            ItemsForSaleList = itemsForSales;
            MarketSaleType = marketSaleType;
            ChangeValueType = changeValueType;
            ChangeValue = changeValue;
        }

        public List<ItemsForSale> ItemsForSaleList { get; set; }
        public MarketSaleType MarketSaleType { get; set; }
        public ChangeValueType ChangeValueType { get; set; }
        public double ChangeValue { get; set; }

        public async Task<double?> GetPrice(FullRgItem item, SteamManager manager)
        {
            double? price = null;
            switch (MarketSaleType)
            {
                case MarketSaleType.LOWER_THAN_CURRENT:
                    price = CURRENT_PRICES_CACHE.Get(item)?.Price;
                    if (!price.HasValue)
                    {
                        price = await manager.GetCurrentPrice(item.Asset, item.Description);
                        CURRENT_PRICES_CACHE.Cache(item.Description.MarketHashName, (double) price);
                    }

                    break;

                case MarketSaleType.LOWER_THAN_AVERAGE:
                    price = AVERAGE_PRICES_CACHE.Get(item)?.Price;
                    if (!price.HasValue)
                    {
                        price = manager.GetAveragePrice(item.Asset, item.Description);
                        AVERAGE_PRICES_CACHE.Cache(item.Description.MarketHashName, (double) price);
                    }

                    break;

                case MarketSaleType.RECOMENDED:
                    var currentPrice = CURRENT_PRICES_CACHE.Get(item)?.Price;
                    if (!currentPrice.HasValue)
                    {
                        currentPrice = await manager.GetCurrentPrice(item.Asset, item.Description);
                        CURRENT_PRICES_CACHE.Cache(item.Description.MarketHashName, (double) currentPrice);
                    }

                    var averagePrice = AVERAGE_PRICES_CACHE.Get(item)?.Price;
                    if (!averagePrice.HasValue)
                    {
                        averagePrice = manager.GetAveragePrice(item.Asset, item.Description);
                        AVERAGE_PRICES_CACHE.Cache(item.Description.MarketHashName, (double) averagePrice);
                    }

                    if (!currentPrice.HasValue || !currentPrice.HasValue)
                        price = null;
                    else if (averagePrice > currentPrice)
                        price = averagePrice;
                    else if (currentPrice >= averagePrice) price = currentPrice - 0.01;

                    if (!price.HasValue || price <= 0 || price == double.NaN) price = null;

                    break;
            }

            return price;
        }
    }
}