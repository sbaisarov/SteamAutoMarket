using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using SteamAutoMarket.CustomElements.Utils;
using SteamAutoMarket.Steam.TradeOffer.Models.Full;
using SteamAutoMarket.Utils;
using SteamAutoMarket.WorkingProcess.Settings;

namespace SteamAutoMarket.WorkingProcess.PriceLoader
{
    internal class PriceLoader
    {
        private static AllItemsListGridUtils allItemsListGridUtils;

        private static bool isForced;

        private static DataGridView allItemsGrid;

        private static DataGridView itemsToSaleGrid;

        private static BackgroundWorker allItemsWorkingThread;

        private static BackgroundWorker itemsToSaleWorkingThread;

        private static bool allItemThreadShouldWait;

        public static PricesCache CurrentPricesCache { get; private set; }

        public static PricesCache AveragePricesCache { get; private set; }

        public static void Init(DataGridView allItemsGrid, DataGridView itemsToSaleGrid)
        {
            if (PriceLoader.itemsToSaleGrid != null && PriceLoader.allItemsGrid != null)
            {
                return;
            }

            PriceLoader.allItemsGrid = allItemsGrid;
            allItemsListGridUtils = new AllItemsListGridUtils(allItemsGrid);
            PriceLoader.itemsToSaleGrid = itemsToSaleGrid;

            allItemsWorkingThread = new BackgroundWorker { WorkerSupportsCancellation = true };
            allItemsWorkingThread.DoWork += async (o, e) => await LoadAllItemsToSalePrices();

            itemsToSaleWorkingThread = new BackgroundWorker { WorkerSupportsCancellation = true };
            itemsToSaleWorkingThread.DoWork += async (o, e) => await LoadItemsToSalePrices();

            if (CurrentPricesCache == null)
                CurrentPricesCache = new PricesCache(
                    "current_prices_cache.ini",
                    SavedSettings.Get().SettingsHoursToBecomeOldCurrentPrice);
            if (AveragePricesCache == null)
                AveragePricesCache = new PricesCache(
                    "average_prices_cache.ini",
                    SavedSettings.Get().SettingsHoursToBecomeOldAveragePrice);
        }

        public static void StopAll()
        {
            if (allItemsWorkingThread.IsBusy) allItemsWorkingThread.CancelAsync();
            if (itemsToSaleWorkingThread.IsBusy) itemsToSaleWorkingThread.CancelAsync();
        }

        public static void StartPriceLoading(TableToLoad tableToLoad, bool force = false)
        {
            isForced = force;
            if (isForced) ClearAllPriceCells(tableToLoad);

            switch (tableToLoad)
            {
                case TableToLoad.AllItemsTable:
                    {
                        if (allItemsWorkingThread.IsBusy) return;
                        if (itemsToSaleWorkingThread.IsBusy) allItemThreadShouldWait = true;

                        allItemsWorkingThread.RunWorkerAsync();
                        break;
                    }

                case TableToLoad.ItemsToSaleTable:
                    {
                        if (itemsToSaleWorkingThread.IsBusy) return;
                        allItemThreadShouldWait = true;

                        itemsToSaleWorkingThread.RunWorkerAsync();
                        break;
                    }
            }
        }

        private static void WaitFor_AllItemThreadShouldWait()
        {
            while (allItemThreadShouldWait) Thread.Sleep(1000);
        }

        public static void ClearAllPriceCells(TableToLoad tableToLoad)
        {
            DataGridViewTextBoxCell cell;

            if (tableToLoad == TableToLoad.AllItemsTable)
                foreach (var row in allItemsGrid.Rows.Cast<DataGridViewRow>())
                {
                    cell = allItemsListGridUtils.GetGridCurrentPriceTextBoxCell(row.Index).Cell;
                    if (cell != null) cell.Value = null;

                    cell = allItemsListGridUtils.GetGridAveragePriceTextBoxCell(row.Index).Cell;
                    if (cell != null) cell.Value = null;
                }
            else if (tableToLoad == TableToLoad.ItemsToSaleTable)
                foreach (var row in itemsToSaleGrid.Rows.Cast<DataGridViewRow>())
                {
                    cell = ItemsToSaleGridUtils.GetGridCurrentPriceTextBoxCell(itemsToSaleGrid, row.Index);
                    if (cell != null) cell.Value = null;

                    cell = ItemsToSaleGridUtils.GetGridAveragePriceTextBoxCell(itemsToSaleGrid, row.Index);
                    if (cell != null) cell.Value = null;
                }
        }

        #region ALL ITEMS

        private static async Task LoadAllItemsToSalePrices()
        {
            var rows = GetAllItemsRowsWithNoPrice();
            var tasks = new List<Task>();
            foreach (var row in rows) tasks.Add(SetAllItemsListGridValues(row));

            await Task.WhenAll(tasks);
            Thread.Sleep(1000);
            rows = GetItemsToSaleRowsWithNoPrice();
            if (rows.Count() != 0) await LoadItemsToSalePrices();
            allItemThreadShouldWait = false;
        }

        private static async Task SetAllItemsListGridValues(DataGridViewRow row)
        {
            try
            {
                var currentPriceCell = allItemsListGridUtils.GetGridCurrentPriceTextBoxCell(row.Index).Cell;
                var averagePriceCell = allItemsListGridUtils.GetGridAveragePriceTextBoxCell(row.Index).Cell;

                var prices = await GetAllItemsRowPrice(row);

                var currentPrice = prices.Item1;
                if (currentPrice != null && currentPrice != -1) currentPriceCell.Value = currentPrice;

                var averagePrice = prices.Item2;
                if (averagePrice != null && averagePrice != -1) averagePriceCell.Value = averagePrice;
            }
            catch (Exception ex)
            {
                Logger.Warning("Error on parsing item price", ex);
            }

            WaitFor_AllItemThreadShouldWait();
        }

        private static IEnumerable<DataGridViewRow> GetAllItemsRowsWithNoPrice()
        {
            return allItemsGrid.Rows.Cast<DataGridViewRow>().Where(
                r => allItemsListGridUtils.GetGridCurrentPriceTextBoxCell(r.Index).Value.Equals(null)
                     || allItemsListGridUtils.GetGridAveragePriceTextBoxCell(r.Index).Value.Equals(null));
        }

        private static async Task<Tuple<double?, double?>> GetAllItemsRowPrice(DataGridViewRow row)
        {
            try
            {
                var itemsCell = allItemsListGridUtils.GetGridHiddenItemsListCell(row.Index);
                if (itemsCell?.Cell == null)
                {
                    return new Tuple<double?, double?>(-1, -1);
                }

                var item = itemsCell.Value?.ItemsList?.FirstOrDefault();
                if (item == null)
                {
                    return new Tuple<double?, double?>(-1, -1);
                }

                double? currentPrice = null;
                if (isForced == false)
                {
                    var cachedCurrentPrice = CurrentPricesCache.Get(item);
                    if (cachedCurrentPrice != null) currentPrice = cachedCurrentPrice.Price;
                }

                if (currentPrice == null)
                {
                    currentPrice = await CurrentSession.SteamManager.GetCurrentPrice(item.Asset, item.Description);
                    if (currentPrice != null && (double)currentPrice > 0)
                        CurrentPricesCache.Cache(item.Description.MarketHashName, currentPrice.Value);
                }

                double? averagePrice = null;
                if (isForced == false)
                {
                    var cachedCurrentPrice = AveragePricesCache.Get(item);
                    if (cachedCurrentPrice != null) averagePrice = cachedCurrentPrice.Price;
                }

                if (averagePrice == null)
                {
                    averagePrice = CurrentSession.SteamManager.GetAveragePrice(
                        item.Asset,
                        item.Description,
                        SavedSettings.Get().SettingsAveragePriceParseDays);
                    if (averagePrice != null && (double)averagePrice > 0)
                        AveragePricesCache.Cache(item.Description.MarketHashName, averagePrice.Value);
                }

                return new Tuple<double?, double?>(currentPrice, averagePrice);
            }
            catch (Exception ex)
            {
                Logger.Warning($"Error on parsing row price - {ex.Message}");
                return new Tuple<double?, double?>(0, 0);
            }
        }

        #endregion

        #region ITEMS TO SALE

        private static async Task LoadItemsToSalePrices()
        {
            var rows = GetItemsToSaleRowsWithNoPrice();
            var tasks = new List<Task>();
            foreach (var row in rows) tasks.Add(SetRowCurrentPrices(row));

            await Task.WhenAll(tasks);

            Thread.Sleep(1000);
            rows = GetItemsToSaleRowsWithNoPrice();
            if (rows.Count() != 0)
                await LoadItemsToSalePrices();
            else
                allItemThreadShouldWait = false;
        }

        private static async Task SetRowCurrentPrices(DataGridViewRow row)
        {
            try
            {
                var currentPriceCell = ItemsToSaleGridUtils.GetGridCurrentPriceTextBoxCell(itemsToSaleGrid, row.Index);
                var averagePriceCell = ItemsToSaleGridUtils.GetGridAveragePriceTextBoxCell(itemsToSaleGrid, row.Index);

                var prices = await GetItemsToSaleRowPrice(row);

                var currentPrice = prices.Item1;
                if (currentPrice != -1) currentPriceCell.Value = currentPrice;

                var averagePrice = prices.Item2;
                if (averagePrice != -1) averagePriceCell.Value = averagePrice;
            }
            catch (Exception ex)
            {
                Logger.Warning("Error on parsing item price", ex);
            }
        }

        private static IEnumerable<DataGridViewRow> GetItemsToSaleRowsWithNoPrice()
        {
            return itemsToSaleGrid.Rows.Cast<DataGridViewRow>().Where(
                r => ItemsToSaleGridUtils.GetRowItemPrice(itemsToSaleGrid, r.Index).Equals(null)
                     || ItemsToSaleGridUtils.GetRowAveragePrice(itemsToSaleGrid, r.Index).Equals(null));
        }

        private static async Task<Tuple<double?, double?>> GetItemsToSaleRowPrice(DataGridViewRow row)
        {
            try
            {
                var itemsCell = ItemsToSaleGridUtils.GetGridHidenItemsListCell(itemsToSaleGrid, row.Index);
                if (itemsCell == null || itemsCell.Value == null) return new Tuple<double?, double?>(-1, -1);

                var item = ItemsToSaleGridUtils.GetRowItemsList(itemsToSaleGrid, row.Index).FirstOrDefault();
                if (item == null) return new Tuple<double?, double?>(-1, -1);

                double? currentPrice = null;
                if (isForced == false)
                {
                    var cachedCurrentPrice = CurrentPricesCache.Get(item);
                    if (cachedCurrentPrice != null) currentPrice = cachedCurrentPrice.Price;
                }

                if (currentPrice == null)
                {
                    currentPrice = await CurrentSession.SteamManager.GetCurrentPrice(item.Asset, item.Description);
                    if (currentPrice != null && currentPrice != 0)
                        CurrentPricesCache.Cache(item.Description.MarketHashName, currentPrice.Value);
                }

                double? averagePrice = null;
                if (isForced == false)
                {
                    var cachedCurrentPrice = AveragePricesCache.Get(item);
                    if (cachedCurrentPrice != null) averagePrice = cachedCurrentPrice.Price;
                }

                if (averagePrice == null)
                {
                    averagePrice = CurrentSession.SteamManager.GetAveragePrice(
                        item.Asset,
                        item.Description,
                        SavedSettings.Get().SettingsAveragePriceParseDays);
                    if (averagePrice != null && averagePrice != 0)
                        AveragePricesCache.Cache(item.Description.MarketHashName, averagePrice.Value);
                }

                return new Tuple<double?, double?>(currentPrice, averagePrice);
            }
            catch (Exception ex)
            {
                Logger.Debug($"Error on parsing item price - {ex.Message}");
                return new Tuple<double?, double?>(0, 0);
            }
        }

        #endregion
    }
}