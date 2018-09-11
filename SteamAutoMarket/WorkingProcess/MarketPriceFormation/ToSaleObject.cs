namespace SteamAutoMarket.WorkingProcess.MarketPriceFormation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SteamAutoMarket.Steam;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.WorkingProcess.Settings;

    public class ToSaleObject
    {
        public ToSaleObject(
            List<ItemsForSale> itemsForSales,
            MarketSaleType marketSaleType,
            ChangeValueType changeValueType,
            double changeValue)
        {
            this.ItemsForSaleList = itemsForSales;
            this.MarketSaleType = marketSaleType;
            this.ChangeValueType = changeValueType;
            this.ChangeValue = changeValue;
        }

        public List<ItemsForSale> ItemsForSaleList { get; set; }

        public MarketSaleType MarketSaleType { get; set; }

        public ChangeValueType ChangeValueType { get; set; }

        public double ChangeValue { get; set; }

        public async Task<double?> GetPrice(FullRgItem item, SteamManager manager)
        {
            double? price = null;
            switch (this.MarketSaleType)
            {
                case MarketSaleType.LowerThanCurrent:
                    price = PriceLoader.PriceLoader.CurrentPricesCache.Get(item)?.Price;
                    if (!price.HasValue)
                    {
                        price = await manager.GetCurrentPrice(item.Asset, item.Description);
                        PriceLoader.PriceLoader.CurrentPricesCache.Cache(
                            item.Description.MarketHashName,
                            (double)price);
                    }

                    break;

                case MarketSaleType.LowerThanAverage:
                    price = PriceLoader.PriceLoader.AveragePricesCache.Get(item)?.Price;

                    if (!price.HasValue)
                    {
                        price = manager.GetAveragePrice(
                            item.Asset,
                            item.Description,
                            SavedSettings.Get().SettingsAveragePriceParseDays);
                        if (price != null)
                        {
                            PriceLoader.PriceLoader.AveragePricesCache.Cache(
                                item.Description.MarketHashName,
                                (double)price);
                        }
                    }

                    break;

                case MarketSaleType.Recommended:
                    var currentPrice = PriceLoader.PriceLoader.CurrentPricesCache.Get(item)?.Price;
                    if (!currentPrice.HasValue)
                    {
                        currentPrice = await manager.GetCurrentPrice(item.Asset, item.Description);
                        PriceLoader.PriceLoader.CurrentPricesCache.Cache(
                            item.Description.MarketHashName,
                            (double)currentPrice);
                    }

                    var averagePrice = PriceLoader.PriceLoader.AveragePricesCache.Get(item)?.Price;
                    if (!averagePrice.HasValue)
                    {
                        averagePrice = manager.GetAveragePrice(
                            item.Asset,
                            item.Description,
                            SavedSettings.Get().SettingsAveragePriceParseDays);
                        if (averagePrice != null)
                        {
                            PriceLoader.PriceLoader.AveragePricesCache.Cache(
                                item.Description.MarketHashName,
                                (double)averagePrice);
                        }
                    }

                    if (averagePrice > currentPrice)
                    {
                        price = averagePrice;
                    }
                    else if (currentPrice >= averagePrice)
                    {
                        price = currentPrice - 0.01;
                    }

                    if (!price.HasValue || price <= 0 || price == double.NaN)
                    {
                        price = null;
                    }

                    break;
            }

            return price;
        }
    }
}