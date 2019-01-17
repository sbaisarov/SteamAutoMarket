namespace SteamAutoMarket.UI.SteamIntegration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.Core.Waiter;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.Repository.Settings;
    using SteamAutoMarket.UI.Utils.Logger;

    public static class GridPriceLoaderUtils
    {
        private static readonly List<Task> PriceLoadSubTasks = new List<Task>();

        private static CancellationTokenSource cancellationTokenSource;

        private static Task priceLoadingTask;

        public static void RefreshSingleModelPrice(SteamItemsModel items) => RefreshSingleModelPriceRealization(items);

        public static void RefreshSingleModelPrice(MarketSellModel items, MarketSellStrategy sellStrategy) =>
            RefreshSingleModelPriceRealization(items, sellStrategy);

        public static void StartPriceLoading(IEnumerable<SteamItemsModel> itemsList) =>
            ProcessPriceLoadingTaskRealization(itemsList);

        public static void StartPriceLoading(IEnumerable<MarketSellModel> itemsList, MarketSellStrategy sellStrategy) =>
            ProcessPriceLoadingTaskRealization(itemsList, sellStrategy);

        private static void ProcessPriceLoadingTaskRealization(
            IEnumerable<SteamItemsModel> itemsList,
            MarketSellStrategy sellStrategy = null)
        {
            if (priceLoadingTask?.IsCompleted == false)
            {
                ErrorNotify.InfoMessageBox("Price loading is already in progress");
                return;
            }

            cancellationTokenSource = new CancellationTokenSource();
            priceLoadingTask = Task.Run(
                () =>
                    {
                        try
                        {
                            Logger.Log.Debug("Starting price loading task");

                            var items = itemsList.ToList();
                            items.ForEach(i => i.CleanItemPrices());

                            var averagePriceDays = SettingsProvider.GetInstance().AveragePriceDays;

                            var priceLoadingSemaphore = new Semaphore(
                                SettingsProvider.GetInstance().PriceLoadingThreads,
                                SettingsProvider.GetInstance().PriceLoadingThreads);

                            foreach (var item in items)
                            {
                                priceLoadingSemaphore.WaitOne();
                                Logger.Log.Debug($"Processing price for {item.ItemName}");

                                var task = Task.Run(
                                    () =>
                                        {
                                            var price = UiGlobalVariables.SteamManager.GetCurrentPriceWithCache(
                                                item.ItemModel.Asset.Appid,
                                                item.ItemModel.Description.MarketHashName);

                                            Logger.Log.Debug($"Current price for {item.ItemName} is - {price}");
                                            item.CurrentPrice = price;

                                            if (sellStrategy != null)
                                            {
                                                ((MarketSellModel)item).ProcessSellPrice(sellStrategy);
                                            }

                                            priceLoadingSemaphore.Release();
                                        },
                                    cancellationTokenSource.Token);

                                PriceLoadSubTasks.Add(task);

                                priceLoadingSemaphore.WaitOne();
                                task = Task.Run(
                                    () =>
                                        {
                                            var price = UiGlobalVariables.SteamManager.GetAveragePriceWithCache(
                                                item.ItemModel.Asset.Appid,
                                                item.ItemModel.Description.MarketHashName,
                                                averagePriceDays);

                                            Logger.Log.Debug(
                                                $"Average price for {averagePriceDays} days for {item.ItemName} is - {price}");

                                            item.AveragePrice = price;
                                            if (sellStrategy != null)
                                            {
                                                ((MarketSellModel)item).ProcessSellPrice(sellStrategy);
                                            }

                                            priceLoadingSemaphore.Release();
                                        },
                                    cancellationTokenSource.Token);

                                PriceLoadSubTasks.Add(task);

                                if (cancellationTokenSource.Token.IsCancellationRequested)
                                {
                                    WaitForPriceLoadingSubTasksEnd();
                                    Logger.Log.Debug("Market sell price loading was force stopped");
                                    return;
                                }
                            }

                            WaitForPriceLoadingSubTasksEnd();
                            Logger.Log.Debug("Market sell price loading task is finished");
                        }
                        catch (Exception ex)
                        {
                            ErrorNotify.CriticalMessageBox("Error on items price update", ex);
                        }
                    },
                cancellationTokenSource.Token);
        }

        private static void RefreshSingleModelPriceRealization(
            SteamItemsModel item,
            MarketSellStrategy sellStrategy = null)
        {
            var task = Task.Run(
                () =>
                    {
                        if (item == null)
                        {
                            return;
                        }

                        try
                        {
                            item.CleanItemPrices();

                            var averagePriceDays = SettingsProvider.GetInstance().AveragePriceDays;

                            var price = UiGlobalVariables.SteamManager.GetCurrentPrice(
                                item.ItemModel.Asset.Appid,
                                item.ItemModel.Description.MarketHashName);

                            Logger.Log.Debug($"Current price for {item.ItemName} is - {price}");

                            item.CurrentPrice = price;

                            price = UiGlobalVariables.SteamManager.GetAveragePrice(
                                item.ItemModel.Asset.Appid,
                                item.ItemModel.Description.MarketHashName,
                                averagePriceDays);

                            Logger.Log.Debug(
                                $"Average price for {averagePriceDays} days for {item.ItemName} is - {price}");

                            item.AveragePrice = price;

                            if (sellStrategy != null)
                            {
                                ((MarketSellModel)item).ProcessSellPrice(sellStrategy);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorNotify.CriticalMessageBox("Error on item price update", ex);
                        }
                    });

            PriceLoadSubTasks.Add(task);
        }

        private static void WaitForPriceLoadingSubTasksEnd()
        {
            if (PriceLoadSubTasks.Any(t => t.IsCompleted == false))
            {
                Logger.Log.Debug(
                    $"Waiting for {PriceLoadSubTasks.Count(t => t.IsCompleted == false)} price loading threads to finish");
                new Waiter().Until(() => PriceLoadSubTasks.All(t => t.IsCompleted));
            }

            PriceLoadSubTasks.Clear();
        }

        public static void InvokePriceLoadingStop()
        {
            Task.Run(
                () =>
                    {
                        if (cancellationTokenSource == null || priceLoadingTask == null || priceLoadingTask.IsCompleted)
                        {
                            Logger.Log.Debug("No active market sell price loading task found. Nothing to stop");
                            return;
                        }

                        Logger.Log.Debug("Active market sell price loading task found. Trying to force stop it");
                        cancellationTokenSource.Cancel();
                        new Waiter().Until(() => priceLoadingTask.IsCompleted);
                        cancellationTokenSource = new CancellationTokenSource();
                    });
        }
    }
}