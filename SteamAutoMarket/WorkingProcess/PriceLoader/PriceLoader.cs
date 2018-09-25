namespace SteamAutoMarket.WorkingProcess.PriceLoader
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using SteamAutoMarket.CustomElements.Controls.Market;
    using SteamAutoMarket.CustomElements.Utils;
    using SteamAutoMarket.Utils;
    using SteamAutoMarket.WorkingProcess.Settings;

    internal class PriceLoader
    {
        private static readonly Queue<PriceLoadTask> WorkingTasksQueue = new Queue<PriceLoadTask>();

        private static readonly List<PriceLoadTask> ProcessingTasks = new List<PriceLoadTask>();

        private static readonly Semaphore Semaphore = new Semaphore(
            SavedSettings.Get().PriceLoadingThreads,
            SavedSettings.Get().PriceLoadingThreads);

        private static BackgroundWorker workingThread;

        private static bool isForced;

        private static DataGridView allItemsGrid;

        private static AllItemsListGridUtils allItemsListGridUtils;

        private static DataGridView itemsToSaleGrid;

        private static DataGridView relistGridView;

        public static PricesCache AveragePricesCache { get; } = new PricesCache(
            "average_prices_cache.ini",
            SavedSettings.Get().SettingsHoursToBecomeOldAveragePrice);

        public static PricesCache CurrentPricesCache { get; } = new PricesCache(
            "current_prices_cache.ini",
            SavedSettings.Get().SettingsHoursToBecomeOldCurrentPrice);

        public static void Init(DataGridView table, ETableToLoad tableToLoad)
        {
            switch (tableToLoad)
            {
                case ETableToLoad.AllItemsTable:
                    allItemsGrid = table;
                    allItemsListGridUtils = new AllItemsListGridUtils(table);
                    break;

                case ETableToLoad.ItemsToSaleTable:
                    itemsToSaleGrid = table;
                    break;

                case ETableToLoad.RelistTable:
                    relistGridView = table;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(tableToLoad), tableToLoad, null);
            }
        }

        public static void StopAll()
        {
            WorkingTasksQueue.Clear();
        }

        public static void WaitForLoadFinish()
        {
            try
            {
                if (ProcessingTasks.Count > 0)
                {
                    Program.WorkingProcessForm.AppendWorkingProcessInfo(
                        $"Waiting for {ProcessingTasks.Count} price loading threads finish.");
                }

                while (ProcessingTasks.Count > 0)
                {
                    Task.Delay(500).ConfigureAwait(true);
                }
            }
            catch
            {
                // ignored
            }
        }

        public static void StartPriceLoading(ETableToLoad tableToLoad, bool force = false)
        {
            if (workingThread == null)
            {
                workingThread = new BackgroundWorker();
                workingThread.DoWork += ProcessPendingTasks;
            }

            isForced = force;
            if (isForced)
            {
                ClearAllPriceCells(tableToLoad);
            }

            switch (tableToLoad)
            {
                case ETableToLoad.AllItemsTable:
                    {
                        AddAllItemsToSaleTasksToQueue();
                        StartWorkingThread();
                        break;
                    }

                case ETableToLoad.ItemsToSaleTable:
                    {
                        WorkingTasksQueue.Clear();
                        AddItemsToSaleTasksToQueue();
                        AddAllItemsToSaleTasksToQueue();

                        StartWorkingThread();
                        break;
                    }

                case ETableToLoad.RelistTable:
                    {
                        // todo
                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(tableToLoad), tableToLoad, null);
            }
        }

        private static void ProcessPendingTasks(object sender, DoWorkEventArgs args)
        {
            while (WorkingTasksQueue.Count > 0)
            {
                try
                {
                    Semaphore.WaitOne();
                    var priceLoadTask = WorkingTasksQueue.Dequeue();

                    if (priceLoadTask == null)
                    {
                        Semaphore.Release();
                        continue;
                    }

                    Task.Run(
                        () =>
                            {
                                ProcessingTasks.Add(priceLoadTask);

                                if (priceLoadTask.Task.Status == TaskStatus.Created)
                                {
                                    priceLoadTask.Task.Start();
                                    Task.WaitAll(priceLoadTask.Task);
                                }

                                ProcessingTasks.Remove(priceLoadTask);

                                Semaphore.Release();
                            });
                }
                catch (InvalidOperationException)
                {
                    Semaphore.Release();
                }
            }
        }

        private static void StartWorkingThread()
        {
            if (workingThread.IsBusy)
            {
                return;
            }

            workingThread.RunWorkerAsync();
        }

        private static void ClearAllPriceCells(ETableToLoad tableToLoad)
        {
            DataGridViewTextBoxCell cell;

            switch (tableToLoad)
            {
                case ETableToLoad.AllItemsTable:
                    {
                        foreach (var row in allItemsGrid.Rows.Cast<DataGridViewRow>())
                        {
                            cell = allItemsListGridUtils.GetGridCurrentPriceTextBoxCell(row.Index).Cell;
                            if (cell != null)
                            {
                                cell.Value = null;
                            }

                            cell = allItemsListGridUtils.GetGridAveragePriceTextBoxCell(row.Index).Cell;
                            if (cell != null)
                            {
                                cell.Value = null;
                            }
                        }

                        break;
                    }

                case ETableToLoad.ItemsToSaleTable:
                    foreach (var row in itemsToSaleGrid.Rows.Cast<DataGridViewRow>())
                    {
                        cell = ItemsToSaleGridUtils.GetGridCurrentPriceTextBoxCell(itemsToSaleGrid, row.Index);
                        if (cell != null)
                        {
                            cell.Value = null;
                        }

                        cell = ItemsToSaleGridUtils.GetGridAveragePriceTextBoxCell(itemsToSaleGrid, row.Index);
                        if (cell != null)
                        {
                            cell.Value = null;
                        }
                    }

                    break;

                case ETableToLoad.RelistTable:
                    // todo
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(tableToLoad), tableToLoad, null);
            }
        }

        #region ALL ITEMS

        private static void AddAllItemsToSaleTasksToQueue()
        {
            var rows = GetAllItemsRowsWithNoPrice();
            var tasks = rows.Select(row => new Task(() => { SetAllItemsListGridValues(row); })).ToList();
            tasks.ForEach(t => WorkingTasksQueue.Enqueue(new PriceLoadTask(ETableToLoad.AllItemsTable, t)));
        }

        private static void SetAllItemsListGridValues(DataGridViewRow row)
        {
            try
            {
                var currentPriceCell = allItemsListGridUtils.GetGridCurrentPriceTextBoxCell(row.Index).Cell;
                var averagePriceCell = allItemsListGridUtils.GetGridAveragePriceTextBoxCell(row.Index).Cell;

                var prices = GetAllItemsRowPrice(row).Result;

                var currentPrice = prices.Item1;
                if (currentPrice != null && currentPrice != -1)
                {
                    currentPriceCell.Value = currentPrice;
                }

                var averagePrice = prices.Item2;
                if (averagePrice != null && averagePrice != -1)
                {
                    averagePriceCell.Value = averagePrice;
                }
            }
            catch (Exception ex)
            {
                Logger.Warning("Error on parsing item price", ex);
            }
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
                    var cachedCurrentPrice = CurrentPricesCache.Get(item.Description.MarketHashName);
                    if (cachedCurrentPrice != null)
                    {
                        currentPrice = cachedCurrentPrice.Price;
                    }
                }

                if (currentPrice == null)
                {
                    currentPrice = await CurrentSession.SteamManager.GetCurrentPrice(item.Asset.Appid, item.Description.MarketHashName);
                    if (currentPrice != null && currentPrice > 0)
                    {
                        CurrentPricesCache.Cache(item.Description.MarketHashName, currentPrice.Value);
                    }
                }

                double? averagePrice = null;
                if (isForced == false)
                {
                    var cachedCurrentPrice = AveragePricesCache.Get(item.Description.MarketHashName);
                    if (cachedCurrentPrice != null)
                    {
                        averagePrice = cachedCurrentPrice.Price;
                    }
                }

                if (averagePrice != null)
                {
                    return new Tuple<double?, double?>(currentPrice, averagePrice);
                }

                averagePrice = CurrentSession.SteamManager.GetAveragePrice(
                    item.Asset.Appid,
                    item.Description.MarketHashName,
                    SavedSettings.Get().SettingsAveragePriceParseDays);

                if (averagePrice != null && (double)averagePrice > 0)
                {
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

        private static void AddItemsToSaleTasksToQueue()
        {
            var rows = GetItemsToSaleRowsWithNoPrice();
            var tasks = rows.Select(row => new Task(() => SetItemToSaleRowCurrentPrices(row))).ToList();
            tasks.ForEach(t => WorkingTasksQueue.Enqueue(new PriceLoadTask(ETableToLoad.ItemsToSaleTable, t)));
        }

        private static void SetItemToSaleRowCurrentPrices(DataGridViewRow row)
        {
            try
            {
                var currentPriceCell = ItemsToSaleGridUtils.GetGridCurrentPriceTextBoxCell(itemsToSaleGrid, row.Index);
                var averagePriceCell = ItemsToSaleGridUtils.GetGridAveragePriceTextBoxCell(itemsToSaleGrid, row.Index);

                var prices = GetItemsToSaleRowPrice(row).Result;

                var currentPrice = prices.Item1;
                if (currentPrice != -1)
                {
                    currentPriceCell.Value = currentPrice;
                }

                var averagePrice = prices.Item2;
                if (averagePrice != -1)
                {
                    averagePriceCell.Value = averagePrice;
                }
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
                if (itemsCell == null || itemsCell.Value == null)
                {
                    return new Tuple<double?, double?>(-1, -1);
                }

                var item = ItemsToSaleGridUtils.GetRowItemsList(itemsToSaleGrid, row.Index).FirstOrDefault();
                if (item == null)
                {
                    return new Tuple<double?, double?>(-1, -1);
                }

                double? currentPrice = null;
                if (isForced == false)
                {
                    var cachedCurrentPrice = CurrentPricesCache.Get(item.Description.MarketHashName);
                    if (cachedCurrentPrice != null)
                    {
                        currentPrice = cachedCurrentPrice.Price;
                    }
                }

                if (currentPrice == null)
                {
                    currentPrice = await CurrentSession.SteamManager.GetCurrentPrice(item.Asset.Appid, item.Description.MarketHashName);
                    if (currentPrice != null && currentPrice != 0)
                    {
                        CurrentPricesCache.Cache(item.Description.MarketHashName, currentPrice.Value);
                    }
                }

                double? averagePrice = null;
                if (isForced == false)
                {
                    var cachedCurrentPrice = AveragePricesCache.Get(item.Description.MarketHashName);
                    if (cachedCurrentPrice != null)
                    {
                        averagePrice = cachedCurrentPrice.Price;
                    }
                }

                if (averagePrice != null)
                {
                    return new Tuple<double?, double?>(currentPrice, averagePrice);
                }

                averagePrice = CurrentSession.SteamManager.GetAveragePrice(
                    item.Asset.Appid,
                    item.Description.MarketHashName,
                    SavedSettings.Get().SettingsAveragePriceParseDays);

                if (averagePrice != null && averagePrice != 0)
                {
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

        #region Relist

        private static void AddRelistTasksToQueue()
        {
            var rows = GetRelistRowsWithNoPrice();
            var tasks = rows.Select(row => new Task(() => SetRelistRowCurrentPrices(row))).ToList();
            tasks.ForEach(t => WorkingTasksQueue.Enqueue(new PriceLoadTask(ETableToLoad.RelistTable, t)));
        }

        private static void SetRelistRowCurrentPrices(DataGridViewRow row)
        {
            try
            {
                var currentPriceCell = relistGridView.Rows[row.Index].Cells[MarketRelistControl.ListingCurrentPriceCellIndex];
                var averagePriceCell = relistGridView.Rows[row.Index].Cells[MarketRelistControl.ListingAveragePriceCellIndex];

                var prices = GetRelistRowPrice(row).Result;

                var currentPrice = prices.Item1;
                if (currentPrice != -1)
                {
                    currentPriceCell.Value = currentPrice;
                }

                var averagePrice = prices.Item2;
                if (averagePrice != -1)
                {
                    averagePriceCell.Value = averagePrice;
                }
            }
            catch (Exception ex)
            {
                Logger.Warning("Error on parsing item price", ex);
            }
        }

        private static IEnumerable<DataGridViewRow> GetRelistRowsWithNoPrice()
        {
            return relistGridView.Rows.Cast<DataGridViewRow>().Where(
                r => GetMyListingsCurrentRowPrice(r.Index).HasValue == false
                     || GetMyListingsAverageRowPrice(r.Index).HasValue == false);
        }

        private static double? GetMyListingsCurrentRowPrice(int index)
        {
            return (double?)relistGridView.Rows[index].Cells[MarketRelistControl.ListingCurrentPriceCellIndex].Value;
        }

        private static double? GetMyListingsAverageRowPrice(int index)
        {
            return (double?)relistGridView.Rows[index].Cells[MarketRelistControl.ListingAveragePriceCellIndex].Value;
        }

        private static async Task<Tuple<double?, double?>> GetRelistRowPrice(DataGridViewRow row)
        {
            try
            {
                var hashCell = relistGridView.Rows[row.Index].Cells[MarketRelistControl.ListingHiddenMarketHashNameCellIndex];
                if (hashCell?.Value == null)
                {
                    return new Tuple<double?, double?>(-1, -1);
                }

                var item = MarketRelistControl.MyListings?[(string)hashCell.Value]?.FirstOrDefault();
                if (item == null)
                {
                    return new Tuple<double?, double?>(-1, -1);
                }

                double? currentPrice = null;
                if (isForced == false)
                {
                    var cachedCurrentPrice = CurrentPricesCache.Get(item.HashName);
                    if (cachedCurrentPrice != null)
                    {
                        currentPrice = cachedCurrentPrice.Price;
                    }
                }

                if (currentPrice == null)
                {
                    currentPrice = await CurrentSession.SteamManager.GetCurrentPrice(item.AppId, item.HashName);
                    if (currentPrice != null && currentPrice != 0)
                    {
                        CurrentPricesCache.Cache(item.HashName, currentPrice.Value);
                    }
                }

                double? averagePrice = null;
                if (isForced == false)
                {
                    var cachedCurrentPrice = AveragePricesCache.Get(item.HashName);
                    if (cachedCurrentPrice != null)
                    {
                        averagePrice = cachedCurrentPrice.Price;
                    }
                }

                if (averagePrice != null)
                {
                    return new Tuple<double?, double?>(currentPrice, averagePrice);
                }

                averagePrice = CurrentSession.SteamManager.GetAveragePrice(
                    item.AppId,
                    item.HashName,
                    SavedSettings.Get().SettingsAveragePriceParseDays);

                if (averagePrice != null && averagePrice != 0)
                {
                    AveragePricesCache.Cache(item.HashName, averagePrice.Value);
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