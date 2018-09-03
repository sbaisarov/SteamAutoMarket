using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using autotrade.CustomElements.Utils;
using autotrade.Steam.TradeOffer.Models.Full;

namespace autotrade.WorkingProcess.MarketPriceFormation
{
    internal class PriceShaper
    {
        public PriceShaper(DataGridView itemsToSaleGridView, MarketSaleType type, double lowerValue,
            double lowerPercentValue)
        {
            GRID = itemsToSaleGridView;
            TYPE = type;
            LOWER_VALUE = lowerValue;
            LOWER_PERCENT_VALUE = lowerPercentValue;
        }

        private static DataGridView GRID { get; set; }
        private static MarketSaleType TYPE { get; set; }
        private static double LOWER_VALUE { get; set; }
        private static double LOWER_PERCENT_VALUE { get; set; }

        public ToSaleObject GetItemsForSales()
        {
            List<ItemsForSale> itemsForSales;

            switch (TYPE)
            {
                case MarketSaleType.RECOMENDED:
                    itemsForSales = GetRecomendedPriceItemsForSale();
                    break;

                case MarketSaleType.LOWER_THAN_AVERAGE:
                    itemsForSales = GetAveragePriceItemsForSale();
                    break;

                case MarketSaleType.LOWER_THAN_CURRENT:
                    itemsForSales = GetCurrentPriceItemsForSale();
                    break;

                case MarketSaleType.MANUAL:
                    itemsForSales = GetManualPriceItemsForSale();
                    break;

                default: throw new InvalidOperationException("Not implemented market sale type");
            }

            var changeValueType = ChangeValueType.ChangeValueTypeNone;
            double changeValue = 0;

            if (LOWER_VALUE != 0)
            {
                changeValueType = ChangeValueType.ChangeValueTypeByValue;
                changeValue = LOWER_VALUE;
            }
            else if (LOWER_PERCENT_VALUE != 0)
            {
                changeValueType = ChangeValueType.ChangeValueTypeByPercent;
                changeValue = LOWER_PERCENT_VALUE;
            }

            return new ToSaleObject(itemsForSales, TYPE, changeValueType, changeValue);
        }

        private List<ItemsForSale> GetRecomendedPriceItemsForSale()
        {
            var itemsForSale = new List<ItemsForSale>();

            var priceShapingStrategy = GetPriceShapingStrategy();

            foreach (var row in GRID.Rows.Cast<DataGridViewRow>())
            {
                var items = GetGridItemsList(row.Index);
                if (items == null) continue;

                double? resultPrice = null;
                var averagePrice = GetGridAveragePrice(row.Index);
                var currentPrice = GetGridCurrentPrice(row.Index);

                if (averagePrice == null || currentPrice == null)
                    resultPrice = null;
                else if (averagePrice > currentPrice)
                    resultPrice = averagePrice;
                else if (currentPrice >= averagePrice) resultPrice = currentPrice - 0.01;

                if (resultPrice == null || resultPrice.Value <= 0) resultPrice = null;

                itemsForSale.Add(new ItemsForSale(items, resultPrice));
            }

            return itemsForSale;
        }

        private List<ItemsForSale> GetCurrentPriceItemsForSale()
        {
            var itemsForSale = new List<ItemsForSale>();

            var priceShapingStrategy = GetPriceShapingStrategy();

            foreach (var row in GRID.Rows.Cast<DataGridViewRow>())
            {
                var items = GetGridItemsList(row.Index);
                if (items == null) continue;

                var price = GetGridCurrentPrice(row.Index);
                if (price == null || price.Value <= 0)
                    price = null;
                else
                    price = priceShapingStrategy.Format(price.Value);

                itemsForSale.Add(new ItemsForSale(items, price));
            }

            return itemsForSale;
        }

        private List<ItemsForSale> GetAveragePriceItemsForSale()
        {
            var itemsForSale = new List<ItemsForSale>();

            var priceShapingStrategy = GetPriceShapingStrategy();

            foreach (var row in GRID.Rows.Cast<DataGridViewRow>())
            {
                var items = GetGridItemsList(row.Index);
                if (items == null) continue;

                var price = GetGridAveragePrice(row.Index);
                if (price == null || price.Value <= 0)
                    price = null;
                else
                    price = priceShapingStrategy.Format(price.Value);

                itemsForSale.Add(new ItemsForSale(items, price));
            }

            return itemsForSale;
        }

        private List<ItemsForSale> GetManualPriceItemsForSale()
        {
            var itemsForSale = new List<ItemsForSale>();

            foreach (var row in GRID.Rows.Cast<DataGridViewRow>())
            {
                var items = GetGridItemsList(row.Index);
                if (items == null) continue;

                var price = GetGridCurrentPrice(row.Index);
                if (price == null) continue;

                itemsForSale.Add(new ItemsForSale(items, price.Value));
            }

            return itemsForSale;
        }

        private List<FullRgItem> GetGridItemsList(int index)
        {
            var items = new List<FullRgItem>();

            var row = ItemsToSaleGridUtils.GetGridHidenItemsListCell(GRID, index);
            if (row == null || row.Value == null) return items;

            items = row.Value as List<FullRgItem>;
            if (items.Count == 0) return null;

            return items;
        }

        private double? GetGridCurrentPrice(int index)
        {
            var row = ItemsToSaleGridUtils.GetGridCurrentPriceTextBoxCell(GRID, index);
            if (row == null || row.Value == null) return null;

            if (GetDouble(row.Value, out var price))
            {
                if (price == 0) return null;
                return price;
            }

            return null;
        }

        private double? GetGridAveragePrice(int index)
        {
            var row = ItemsToSaleGridUtils.GetGridAveragePriceTextBoxCell(GRID, index);
            if (row == null || row.Value == null) return null;

            if (GetDouble(row.Value, out var price))
            {
                if (price == 0) return null;
                return price;
            }

            return null;
        }

        private bool GetDouble(object value, out double result)
        {
            string stringValue;
            if (value is double)
            {
                result = (double) value;
                return true;
            }

            if (value is string)
                stringValue = (string) value;
            else
                stringValue = value.ToString();

            if (!double.TryParse(stringValue, NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
                !double.TryParse(stringValue, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                !double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return false;
            return true;
        }

        private PriceShapingStrategy GetPriceShapingStrategy()
        {
            if (LOWER_VALUE != 0) return PriceShapingStrategyContainer.BY_VALUE;
            if (LOWER_PERCENT_VALUE != 0) return PriceShapingStrategyContainer.BY_PERCENT;
            return PriceShapingStrategyContainer.DONT_CHANGE;
        }

        public abstract class PriceShapingStrategy
        {
            public abstract double Format(double oldPrice);
        }

        public class ChangeByValueStrategy : PriceShapingStrategy
        {
            public override double Format(double oldValue)
            {
                return oldValue + LOWER_VALUE;
            }
        }

        public class ChangeByPercentStrategy : PriceShapingStrategy
        {
            public override double Format(double oldValue)
            {
                return oldValue + oldValue * LOWER_PERCENT_VALUE / 100;
            }
        }

        public class DontChangeStrategy : PriceShapingStrategy
        {
            public override double Format(double oldValue)
            {
                return oldValue;
            }
        }

        public class PriceShapingStrategyContainer
        {
            public static PriceShapingStrategy BY_VALUE = new ChangeByValueStrategy();
            public static PriceShapingStrategy BY_PERCENT = new ChangeByPercentStrategy();
            public static PriceShapingStrategy DONT_CHANGE = new DontChangeStrategy();
        }
    }
}