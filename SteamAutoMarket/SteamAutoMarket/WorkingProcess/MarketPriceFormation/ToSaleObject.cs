namespace SteamAutoMarket.WorkingProcess.MarketPriceFormation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SteamAutoMarket.Steam;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.WorkingProcess.Settings;

    public class ToSaleObject
    {
        public ToSaleObject(
            List<ItemsForSale> itemsForSales,
            EMarketSaleType marketSaleType,
            EChangeValueType changeValueType,
            double changeValue)
        {
            this.ItemsForSaleList = itemsForSales;
            this.MarketSaleType = marketSaleType;
            this.ChangeValueType = changeValueType;
            this.ChangeValue = changeValue;
        }

        public List<ItemsForSale> ItemsForSaleList { get; set; }

        public EMarketSaleType MarketSaleType { get; set; }

        public EChangeValueType ChangeValueType { get; set; }

        public double ChangeValue { get; set; }

        public async Task<double?> GetPrice(FullRgItem item, SteamManager manager)
        {
            var itemName = item.Description.Name;

            double? price = null;
            switch (this.MarketSaleType)
            {
                case EMarketSaleType.LowerThanCurrent:
                    price = PriceLoader.PriceLoader.CurrentPricesCache.Get(item.Description.MarketHashName)?.Price;
                    if (!price.HasValue)
                    {
                        price = await manager.GetCurrentPrice(item.Asset.Appid, item.Description.MarketHashName);
                        PriceLoader.PriceLoader.CurrentPricesCache.Cache(
                            item.Description.MarketHashName,
                            (double)price);
                    }

                    Program.WorkingProcessForm.AppendWorkingProcessInfo($"Current price for '{itemName}' is {price}");
                    price = this.HandleChangeValue(price);
                    break;

                case EMarketSaleType.LowerThanAverage:
                    price = PriceLoader.PriceLoader.AveragePricesCache.Get(item.Description.MarketHashName)?.Price;

                    if (!price.HasValue)
                    {
                        price = manager.GetAveragePrice(
                            item.Asset.Appid,
                            item.Description.MarketHashName,
                            SavedSettings.Get().SettingsAveragePriceParseDays);
                        if (price != null)
                        {
                            PriceLoader.PriceLoader.AveragePricesCache.Cache(
                                item.Description.MarketHashName,
                                (double)price);
                        }
                    }

                    price = this.HandleChangeValue(price);
                    Program.WorkingProcessForm.AppendWorkingProcessInfo($"Average price for '{itemName}' is {price}");
                    break;

                case EMarketSaleType.Recommended:
                    var currentPrice = PriceLoader.PriceLoader.CurrentPricesCache.Get(item.Description.MarketHashName)?.Price;
                    if (!currentPrice.HasValue)
                    {
                        currentPrice = await manager.GetCurrentPrice(item.Asset.Appid, item.Description.MarketHashName);
                        PriceLoader.PriceLoader.CurrentPricesCache.Cache(
                            item.Description.MarketHashName,
                            (double)currentPrice);
                    }

                    var averagePrice = PriceLoader.PriceLoader.AveragePricesCache.Get(item.Description.MarketHashName)?.Price;
                    if (!averagePrice.HasValue)
                    {
                        averagePrice = manager.GetAveragePrice(
                            item.Asset.Appid,
                            item.Description.MarketHashName,
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

                    Program.WorkingProcessForm.AppendWorkingProcessInfo($"Current price for '{itemName}' is {currentPrice}");
                    Program.WorkingProcessForm.AppendWorkingProcessInfo($"Average price for '{itemName}' is {averagePrice}");
                    break;
            }

            return price;
        }

        private double? HandleChangeValue(double? price)
        {
            if (!price.HasValue)
            {
                return null;
            }

            switch (this.ChangeValueType)
            {
                case EChangeValueType.ChangeValueTypeByValue:
                    return price.Value + this.ChangeValue;

                case EChangeValueType.ChangeValueTypeByPercent:
                    return price.Value + (this.ChangeValue / 100 * price.Value);

                case EChangeValueType.ChangeValueTypeNone:
                    return price;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}