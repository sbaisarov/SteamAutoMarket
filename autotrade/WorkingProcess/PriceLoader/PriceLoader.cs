﻿using autotrade.CustomElements;
using autotrade.WorkingProcess.Settings;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using autotrade.CustomElements.Utils;
using Accord;
using static autotrade.Steam.TradeOffer.Inventory;

namespace autotrade.WorkingProcess.PriceLoader {
    class PriceLoader {
        private static bool IS_FORCED = false;

        public static PricesCache CURRENT_PRICES_CACHE;
        public static PricesCache AVERAGE_PRICES_CACHE;

        private static DataGridView ALL_ITEMS_GRID;
        private static DataGridView ITEMS_TO_SALE_GRID;

        private static BackgroundWorker ALL_ITEMS_WORKING_THREAD;
        private static BackgroundWorker ITEMS_TO_SALE_WORKING_THREAD;

        private static bool AllItemThreadShouldWait = false;

        public static void Init(DataGridView allItemsGrid, DataGridView itemsToSaleGrid) {
            if (ITEMS_TO_SALE_GRID == null || ALL_ITEMS_GRID == null) {

                ALL_ITEMS_GRID = allItemsGrid;
                ITEMS_TO_SALE_GRID = itemsToSaleGrid;

                ALL_ITEMS_WORKING_THREAD = new BackgroundWorker
                {
                    WorkerSupportsCancellation = true
                };
                ALL_ITEMS_WORKING_THREAD.DoWork += async (o, e) => await LoadAllItemsToSalePrices(o, e);

                ITEMS_TO_SALE_WORKING_THREAD = new BackgroundWorker
                {
                    WorkerSupportsCancellation = true
                };
                ITEMS_TO_SALE_WORKING_THREAD.DoWork += async (o, e) => await LoadAllItemsToSalePrices(o, e);

                if (CURRENT_PRICES_CACHE == null) CURRENT_PRICES_CACHE = new PricesCache("current_prices_cache.ini", SavedSettings.Get().SETTINGS_HOURS_TO_BECOME_OLD_CURRENT_PRICE);
                if (AVERAGE_PRICES_CACHE == null) AVERAGE_PRICES_CACHE = new PricesCache("average_prices_cache.ini", SavedSettings.Get().SETTINGS_HOURS_TO_BECOME_OLD_AVERAGE_PRICE);
            }
        }

        public static void StopAll() {
            if (ALL_ITEMS_WORKING_THREAD.IsBusy) ALL_ITEMS_WORKING_THREAD.CancelAsync();
            if (ITEMS_TO_SALE_WORKING_THREAD.IsBusy) ITEMS_TO_SALE_WORKING_THREAD.CancelAsync();
        }

        public static void StartPriceLoading(TableToLoad tableToLoad, bool force = false) {
            IS_FORCED = force;
            if (IS_FORCED) {
                ClearAllPriceCells(tableToLoad);
            }

            switch (tableToLoad) {
                case TableToLoad.ALL_ITEMS_TABLE: {
                        if (ALL_ITEMS_WORKING_THREAD.IsBusy) return;
                        if (ITEMS_TO_SALE_WORKING_THREAD.IsBusy) AllItemThreadShouldWait = true;

                        ALL_ITEMS_WORKING_THREAD.RunWorkerAsync();
                        break;
                    }

                case TableToLoad.ITEMS_TO_SALE_TABLE: {
                        if (ITEMS_TO_SALE_WORKING_THREAD.IsBusy) return;
                        AllItemThreadShouldWait = true;

                        ITEMS_TO_SALE_WORKING_THREAD.RunWorkerAsync();
                        break;
                    }
            }

        }

        private static void WaitFor_AllItemThreadShouldWait() {
            if (AllItemThreadShouldWait == false) return;
            else {
                Thread.Sleep(1000);
                WaitFor_AllItemThreadShouldWait();
            }
        }

        public static void ClearAllPriceCells(TableToLoad tableToLoad) {
            DataGridViewTextBoxCell cell;

            if (tableToLoad == TableToLoad.ALL_ITEMS_TABLE) {
                foreach (var row in ALL_ITEMS_GRID.Rows.Cast<DataGridViewRow>()) {
                    cell = AllItemsListGridUtils.GetGridCurrentPriceTextBoxCell(ALL_ITEMS_GRID, row.Index);
                    if (cell != null) cell.Value = null;

                    cell = AllItemsListGridUtils.GetGridAveragePriceTextBoxCell(ALL_ITEMS_GRID, row.Index);
                    if (cell != null) cell.Value = null;
                }
            } else if (tableToLoad == TableToLoad.ITEMS_TO_SALE_TABLE) {
                foreach (var row in ITEMS_TO_SALE_GRID.Rows.Cast<DataGridViewRow>()) {
                    cell = ItemsToSaleGridUtils.GetGridCurrentPriceTextBoxCell(ITEMS_TO_SALE_GRID, row.Index);
                    if (cell != null) cell.Value = null;

                    cell = ItemsToSaleGridUtils.GetGridAveragePriceTextBoxCell(ITEMS_TO_SALE_GRID, row.Index);
                    if (cell != null) cell.Value = null;
                }
            }
        }

        #region ALL ITEMS
        private static async Task LoadAllItemsToSalePrices(object sender, DoWorkEventArgs ev) {
            var rows = GetAllItemsRowsWithNoPrice();
            var tasks = new List<Task>();
            foreach (var row in rows)
            {
                tasks.Add(SetAllItemsListGridValues(row));
            }

            await Task.WhenAll(tasks);
            Thread.Sleep(1000);
            rows = GetItemsToSaleRowsWithNoPrice();
            if (rows.Count() != 0) await LoadItemsToSalePrices(sender, ev);
        }

        private static async Task SetAllItemsListGridValues(DataGridViewRow row)
        {
            try
            {
                var currentPriceCell = AllItemsListGridUtils.GetGridCurrentPriceTextBoxCell(ALL_ITEMS_GRID, row.Index);
                var averagePriceCell = AllItemsListGridUtils.GetGridAveragePriceTextBoxCell(ALL_ITEMS_GRID, row.Index);

                var prices = await GetAllItemsRowPrice(row);

                double? currentPrice = prices.Item1;
                if (currentPrice != -1)
                {
                    currentPriceCell.Value = currentPrice;
                }

                //todo
                double? averagePrice = prices.Item2;
                if (averagePrice != -1)
                {
                    averagePriceCell.Value = averagePrice;
                }
            }
            catch (Exception e)
            {
                Utils.Logger.Warning("Error on parsing item price", e);
            }

            WaitFor_AllItemThreadShouldWait();
        }

        private static IEnumerable<DataGridViewRow> GetAllItemsRowsWithNoPrice() {
            return ALL_ITEMS_GRID.Rows.Cast<DataGridViewRow>()
                .Where(r =>
                AllItemsListGridUtils.GetRowItemPrice(ALL_ITEMS_GRID, r.Index).Equals(null)
                ||
                AllItemsListGridUtils.GetRowAveragePrice(ALL_ITEMS_GRID, r.Index).Equals(null)
                );
        }

        private static async Task<Tuple<double?, double?>> GetAllItemsRowPrice(DataGridViewRow row) {
            RgFullItem item = new RgFullItem();
            try {
                var itemsCell = AllItemsListGridUtils.GetGridHidenItemsListCell(ALL_ITEMS_GRID, row.Index);
                if (itemsCell == null || itemsCell.Value == null) return new Tuple<double?, double?>(-1, -1);

                item = ((List<RgFullItem>)itemsCell.Value).FirstOrDefault();
                if (item == null) return new Tuple<double?, double?>(-1, -1);

                double? currentPrice = null;
                if (IS_FORCED == false) {
                    var cachedCurrentPrice = CURRENT_PRICES_CACHE.Get(item);
                    if (cachedCurrentPrice != null) {
                        currentPrice = cachedCurrentPrice.Price;
                    }
                }

                if (currentPrice == null) {
                    currentPrice = await CurrentSession.SteamManager.GetCurrentPrice(item.Asset, item.Description);
                    if (currentPrice != null && (double) currentPrice > 0) {
                        CURRENT_PRICES_CACHE.Cache(item.Description.market_hash_name, currentPrice.Value);
                    }
                }

                double? averagePrice = null;
                if (IS_FORCED == false) {
                    var cachedCurrentPrice = AVERAGE_PRICES_CACHE.Get(item);
                    if (cachedCurrentPrice != null) {
                        averagePrice = cachedCurrentPrice.Price;
                    }
                }

                if (averagePrice == null) {
                    CurrentSession.SteamManager.GetAveragePrice(out averagePrice, item.Asset, item.Description);
                    if (averagePrice != null && (double) averagePrice > 0) {
                        AVERAGE_PRICES_CACHE.Cache(item.Description.market_hash_name, averagePrice.Value);
                    }
                }

                return new Tuple<double?, double?>(currentPrice, averagePrice);
            } catch (Exception e) {
                Utils.Logger.Debug($"Error on parsing item price - {e.Message}");
                return new Tuple<double?, double?>(0, 0);
            }
        }
        #endregion

        #region ITEMS TO SALE
        private static async Task LoadItemsToSalePrices(object sender, DoWorkEventArgs ev) {
            var rows = GetItemsToSaleRowsWithNoPrice();
            var tasks = new List<Task>();
            foreach (var row in rows)
            {
                tasks.Add(SetRowCurrentPrices(row));
            }

            await Task.WhenAll(tasks);

            Thread.Sleep(1000);
            rows = GetItemsToSaleRowsWithNoPrice();
            if (rows.Count() != 0) {
                await LoadItemsToSalePrices(sender, ev);
            } else {
                AllItemThreadShouldWait = false;
            }

        }

        private static async Task SetRowCurrentPrices(DataGridViewRow row)
        {
            try
            {
                var currentPriceCell = ItemsToSaleGridUtils.GetGridCurrentPriceTextBoxCell(ITEMS_TO_SALE_GRID, row.Index);
                var averagePriceCell = ItemsToSaleGridUtils.GetGridAveragePriceTextBoxCell(ITEMS_TO_SALE_GRID, row.Index);


                var prices = await GetItemsToSaleRowPrice(row);

                double? currentPrice = prices.Item1;
                if (currentPrice != -1)
                {
                    currentPriceCell.Value = currentPrice;
                }

                double? averagePrice = prices.Item2;
                if (averagePrice != -1)
                {
                    averagePriceCell.Value = averagePrice;
                }
            }
            catch (Exception e)
            {
                Utils.Logger.Warning("Error on parsing item price", e);
            }
        }

        private static IEnumerable<DataGridViewRow> GetItemsToSaleRowsWithNoPrice() {
            return ITEMS_TO_SALE_GRID.Rows.Cast<DataGridViewRow>()
                .Where(
                r =>
                ItemsToSaleGridUtils.GetRowItemPrice(ITEMS_TO_SALE_GRID, r.Index).Equals(null)
                ||
                ItemsToSaleGridUtils.GetRowAveragePrice(ITEMS_TO_SALE_GRID, r.Index).Equals(null)
                );
        }

        private static async Task<Tuple<double?, double?>> GetItemsToSaleRowPrice(DataGridViewRow row) {
            RgFullItem item = new RgFullItem();
            try {
                var itemsCell = ItemsToSaleGridUtils.GetGridHidenItemsListCell(ITEMS_TO_SALE_GRID, row.Index);
                if (itemsCell == null || itemsCell.Value == null) return new Tuple<double?, double?>(-1, -1);

                item = ItemsToSaleGridUtils.GetRowItemsList(ITEMS_TO_SALE_GRID, row.Index).FirstOrDefault();
                if (item == null) return new Tuple<double?, double?>(-1, -1);

                double? currentPrice = null;
                if (IS_FORCED == false) {
                    var cachedCurrentPrice = CURRENT_PRICES_CACHE.Get(item);
                    if (cachedCurrentPrice != null) {
                        currentPrice = cachedCurrentPrice.Price;
                    }
                }

                if (currentPrice == null) {
                    currentPrice = await CurrentSession.SteamManager.GetCurrentPrice(item.Asset, item.Description);
                    if (currentPrice != null && currentPrice != 0) {
                        CURRENT_PRICES_CACHE.Cache(item.Description.market_hash_name, currentPrice.Value);
                    }
                }

                double? averagePrice = null;
                if (IS_FORCED == false) {
                    var cachedCurrentPrice = AVERAGE_PRICES_CACHE.Get(item);
                    if (cachedCurrentPrice != null) {
                        averagePrice = cachedCurrentPrice.Price;
                    }
                }

                if (averagePrice == null) {
                    CurrentSession.SteamManager.GetAveragePrice(out averagePrice, item.Asset, item.Description);
                    if (averagePrice != null && averagePrice != 0) {
                        AVERAGE_PRICES_CACHE.Cache(item.Description.market_hash_name, averagePrice.Value);
                    }
                }

                return new Tuple<double?, double?>(currentPrice, averagePrice);
            } catch (Exception e) {
                Utils.Logger.Debug($"Error on parsing item price - {e.Message}");
                return new Tuple<double?, double?>(0, 0);
            }
        }
        #endregion

    }
}
