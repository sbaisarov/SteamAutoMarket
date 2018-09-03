using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using autotrade.CustomElements.Utils;
using autotrade.Steam.TradeOffer.Models.Full;
using autotrade.Utils;
using autotrade.WorkingProcess.Settings;

namespace autotrade.WorkingProcess.PriceLoader
{
    internal class PriceLoader
    {
        private static bool _isForced;

        public static PricesCache CurrentPricesCache;
        public static PricesCache AveragePricesCache;

        private static DataGridView _allItemsGrid;
        private static DataGridView _itemsToSaleGrid;

        private static BackgroundWorker _allItemsWorkingThread;
        private static BackgroundWorker _itemsToSaleWorkingThread;

        private static bool _allItemThreadShouldWait;

        public static void Init(DataGridView allItemsGrid, DataGridView itemsToSaleGrid)
        {
            if (_itemsToSaleGrid == null || _allItemsGrid == null)
            {
                _allItemsGrid = allItemsGrid;
                _itemsToSaleGrid = itemsToSaleGrid;

                _allItemsWorkingThread = new BackgroundWorker
                {
                    WorkerSupportsCancellation = true
                };
                _allItemsWorkingThread.DoWork += async (o, e) => await LoadAllItemsToSalePrices();

                _itemsToSaleWorkingThread = new BackgroundWorker
                {
                    WorkerSupportsCancellation = true
                };
                _itemsToSaleWorkingThread.DoWork += async (o, e) => await LoadItemsToSalePrices();

                if (CurrentPricesCache == null)
                    CurrentPricesCache = new PricesCache("current_prices_cache.ini",
                        SavedSettings.Get().SettingsHoursToBecomeOldCurrentPrice);
                if (AveragePricesCache == null)
                    AveragePricesCache = new PricesCache("average_prices_cache.ini",
                        SavedSettings.Get().SettingsHoursToBecomeOldAveragePrice);
            }
        }

        public static void StopAll()
        {
            if (_allItemsWorkingThread.IsBusy) _allItemsWorkingThread.CancelAsync();
            if (_itemsToSaleWorkingThread.IsBusy) _itemsToSaleWorkingThread.CancelAsync();
        }

        public static void StartPriceLoading(TableToLoad tableToLoad, bool force = false)
        {
            _isForced = force;
            if (_isForced) ClearAllPriceCells(tableToLoad);

            switch (tableToLoad)
            {
                case TableToLoad.AllItemsTable:
                {
                    if (_allItemsWorkingThread.IsBusy) return;
                    if (_itemsToSaleWorkingThread.IsBusy) _allItemThreadShouldWait = true;

                    _allItemsWorkingThread.RunWorkerAsync();
                    break;
                }

                case TableToLoad.ItemsToSaleTable:
                {
                    if (_itemsToSaleWorkingThread.IsBusy) return;
                    _allItemThreadShouldWait = true;

                    _itemsToSaleWorkingThread.RunWorkerAsync();
                    break;
                }
            }
        }

        private static void WaitFor_AllItemThreadShouldWait()
        {
            while (_allItemThreadShouldWait) Thread.Sleep(1000);
        }

        public static void ClearAllPriceCells(TableToLoad tableToLoad)
        {
            DataGridViewTextBoxCell cell;

            if (tableToLoad == TableToLoad.AllItemsTable)
                foreach (var row in _allItemsGrid.Rows.Cast<DataGridViewRow>())
                {
                    cell = AllItemsListGridUtils.GetGridCurrentPriceTextBoxCell(_allItemsGrid, row.Index);
                    if (cell != null) cell.Value = null;

                    cell = AllItemsListGridUtils.GetGridAveragePriceTextBoxCell(_allItemsGrid, row.Index);
                    if (cell != null) cell.Value = null;
                }
            else if (tableToLoad == TableToLoad.ItemsToSaleTable)
                foreach (var row in _itemsToSaleGrid.Rows.Cast<DataGridViewRow>())
                {
                    cell = ItemsToSaleGridUtils.GetGridCurrentPriceTextBoxCell(_itemsToSaleGrid, row.Index);
                    if (cell != null) cell.Value = null;

                    cell = ItemsToSaleGridUtils.GetGridAveragePriceTextBoxCell(_itemsToSaleGrid, row.Index);
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
            _allItemThreadShouldWait = false;
        }

        private static async Task SetAllItemsListGridValues(DataGridViewRow row)
        {
            try
            {
                var currentPriceCell = AllItemsListGridUtils.GetGridCurrentPriceTextBoxCell(_allItemsGrid, row.Index);
                var averagePriceCell = AllItemsListGridUtils.GetGridAveragePriceTextBoxCell(_allItemsGrid, row.Index);

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
            return _allItemsGrid.Rows.Cast<DataGridViewRow>()
                .Where(r =>
                    AllItemsListGridUtils.GetRowItemPrice(_allItemsGrid, r.Index).Equals(null)
                    ||
                    AllItemsListGridUtils.GetRowAveragePrice(_allItemsGrid, r.Index).Equals(null)
                );
        }

        private static async Task<Tuple<double?, double?>> GetAllItemsRowPrice(DataGridViewRow row)
        {
            try
            {
                var itemsCell = AllItemsListGridUtils.GetGridHidenItemsListCell(_allItemsGrid, row.Index);
                if (itemsCell?.Value == null) return new Tuple<double?, double?>(-1, -1);

                var item = ((List<FullRgItem>) itemsCell.Value).FirstOrDefault();
                if (item == null) return new Tuple<double?, double?>(-1, -1);

                double? currentPrice = null;
                if (_isForced == false)
                {
                    var cachedCurrentPrice = CurrentPricesCache.Get(item);
                    if (cachedCurrentPrice != null) currentPrice = cachedCurrentPrice.Price;
                }

                if (currentPrice == null)
                {
                    currentPrice = await CurrentSession.SteamManager.GetCurrentPrice(item.Asset, item.Description);
                    if (currentPrice != null && (double) currentPrice > 0)
                        CurrentPricesCache.Cache(item.Description.MarketHashName, currentPrice.Value);
                }

                double? averagePrice = null;
                if (_isForced == false)
                {
                    var cachedCurrentPrice = AveragePricesCache.Get(item);
                    if (cachedCurrentPrice != null) averagePrice = cachedCurrentPrice.Price;
                }

                if (averagePrice == null)
                {
                    averagePrice = CurrentSession.SteamManager.GetAveragePrice(item.Asset, item.Description, SavedSettings.Get().SettingsAveragePriceParseDays);
                    if (averagePrice != null && (double) averagePrice > 0)
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
                _allItemThreadShouldWait = false;
        }

        private static async Task SetRowCurrentPrices(DataGridViewRow row)
        {
            try
            {
                var currentPriceCell =
                    ItemsToSaleGridUtils.GetGridCurrentPriceTextBoxCell(_itemsToSaleGrid, row.Index);
                var averagePriceCell =
                    ItemsToSaleGridUtils.GetGridAveragePriceTextBoxCell(_itemsToSaleGrid, row.Index);


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
            return _itemsToSaleGrid.Rows.Cast<DataGridViewRow>()
                .Where(
                    r =>
                        ItemsToSaleGridUtils.GetRowItemPrice(_itemsToSaleGrid, r.Index).Equals(null)
                        ||
                        ItemsToSaleGridUtils.GetRowAveragePrice(_itemsToSaleGrid, r.Index).Equals(null)
                );
        }

        private static async Task<Tuple<double?, double?>> GetItemsToSaleRowPrice(DataGridViewRow row)
        {
            try
            {
                var itemsCell = ItemsToSaleGridUtils.GetGridHidenItemsListCell(_itemsToSaleGrid, row.Index);
                if (itemsCell == null || itemsCell.Value == null) return new Tuple<double?, double?>(-1, -1);

                var item = ItemsToSaleGridUtils.GetRowItemsList(_itemsToSaleGrid, row.Index).FirstOrDefault();
                if (item == null) return new Tuple<double?, double?>(-1, -1);

                double? currentPrice = null;
                if (_isForced == false)
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
                if (_isForced == false)
                {
                    var cachedCurrentPrice = AveragePricesCache.Get(item);
                    if (cachedCurrentPrice != null) averagePrice = cachedCurrentPrice.Price;
                }

                if (averagePrice == null)
                {
                    averagePrice = CurrentSession.SteamManager.GetAveragePrice(item.Asset, item.Description, SavedSettings.Get().SettingsAveragePriceParseDays);
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