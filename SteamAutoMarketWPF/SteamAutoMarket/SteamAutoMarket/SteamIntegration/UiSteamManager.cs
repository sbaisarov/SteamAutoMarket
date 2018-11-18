namespace SteamAutoMarket.SteamIntegration
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Core;
    using Core.Waiter;

    using Steam;
    using Steam.Market.Enums;
    using Steam.Market.Models;
    using Steam.SteamAuth;
    using Steam.TradeOffer.Models;
    using Steam.TradeOffer.Models.Full;

    using SteamAutoMarket.Models;
    using SteamAutoMarket.Pages;
    using SteamAutoMarket.Repository.PriceCache;
    using SteamAutoMarket.Repository.Settings;
    using SteamAutoMarket.Utils.Extension;
    using SteamAutoMarket.Utils.Logger;

    public class UiSteamManager : SteamManager
    {
        public UiSteamManager(SettingsSteamAccount account, bool forceSessionRefresh = false)
            : base(
                account.Login,
                account.Password,
                account.Mafile,
                account.SteamApi,
                account.TradeToken,
                account.Currency,
                SettingsProvider.GetInstance().UserAgent,
                forceSessionRefresh)
        {
            this.SaveAccount(account);

            this.CurrentPriceCache = PriceCacheProvider.GetCurrentPriceCache(
                $"{this.MarketClient.CurrentCurrency}",
                SettingsProvider.GetInstance().CurrentPriceHoursToBecomeOld);

            this.AveragePriceCache = PriceCacheProvider.GetAveragePriceCache(
                $"{this.MarketClient.CurrentCurrency}",
                SettingsProvider.GetInstance().AveragePriceDays,
                SettingsProvider.GetInstance().AveragePriceHoursToBecomeOld);
        }

        public PriceCache AveragePriceCache { get; set; }

        public PriceCache CurrentPriceCache { get; set; }

        public void ConfirmMarketTransactions(WorkingProcessForm form)
        {
            form.AppendLog("Fetching confirmations");
            try
            {
                var confirmations = this.Guard.FetchConfirmations();
                var marketConfirmations = confirmations
                    .Where(item => item.ConfType == Confirmation.ConfirmationType.MarketSellTransaction).ToArray();
                form.AppendLog($"{marketConfirmations.Length} confirmations found. Accepting confirmations");
                this.Guard.AcceptMultipleConfirmations(marketConfirmations);
                form.AppendLog($"{marketConfirmations.Length} confirmations was successfully accepted");
            }
            catch (Exception e)
            {
                form.AppendLog($"Error on 2FA confirm - {e.Message}");
            }
        }

        public override double? GetAveragePrice(int appid, string hashName, int days)
        {
            var price = base.GetAveragePrice(appid, hashName, days);
            if (price.HasValue) this.AveragePriceCache.Cache(hashName, price.Value);
            return price;
        }

        public double? GetAveragePriceWithCache(int appid, string hashName, int days) =>
            this.AveragePriceCache.Get(hashName)?.Price ?? this.GetAveragePrice(appid, hashName, days);

        public override double? GetCurrentPrice(int appid, string hashName)
        {
            var price = base.GetCurrentPrice(appid, hashName);
            if (price.HasValue) this.CurrentPriceCache.Cache(hashName, price.Value);
            return price;
        }

        public double? GetCurrentPriceWithCache(int appid, string hashName) =>
            this.CurrentPriceCache.Get(hashName)?.Price ?? this.GetCurrentPrice(appid, hashName);

        public void LoadItemsToSaleWorkingProcess(
            WorkingProcessForm form,
            SteamAppId appid,
            int contextId,
            ObservableCollection<MarketSellModel> marketSellItems)
        {
            form.ProcessMethod(
                () =>
                    {
                        try
                        {
                            form.AppendLog($"{appid.AppId}-{contextId} inventory loading started");

                            var page = this.LoadInventoryPage(this.SteamId, appid.AppId, contextId);
                            form.AppendLog($"{page.TotalInventoryCount} items found");

                            var totalPagesCount = (int)Math.Ceiling(page.TotalInventoryCount / 5000d);
                            var currentPage = 1;

                            form.ProgressBarMaximum = totalPagesCount;
                            this.ProcessMarketSellInventoryPage(marketSellItems, page);

                            form.AppendLog($"Page {currentPage++}/{totalPagesCount} loaded");
                            form.IncrementProgress();

                            while (page.MoreItems == 1)
                            {
                                if (form.CancellationToken.IsCancellationRequested)
                                {
                                    form.AppendLog($"{appid.Name} inventory loading was force stopped");
                                    return;
                                }

                                page = this.LoadInventoryPage(this.SteamId, appid.AppId, contextId, page.LastAssetid);

                                this.ProcessMarketSellInventoryPage(marketSellItems, page);

                                form.AppendLog($"Page {currentPage++}/{totalPagesCount} loaded");
                                form.IncrementProgress();
                            }

                            form.AppendLog(
                                marketSellItems.Any()
                                    ? $"{marketSellItems.ToArray().Sum(i => i.Count)} marketable items was loaded"
                                    : $"Seems like no items found on {appid.Name} inventory");
                        }
                        catch (Exception e)
                        {
                            var message = $"Error on {appid.Name} inventory loading";

                            form.AppendLog(message);
                            ErrorNotify.CriticalMessageBox(message, e);
                            marketSellItems.ClearDispatch();
                        }
                    });
        }

        public void LoadItemsToTradeWorkingProcess(
            WorkingProcessForm form,
            SteamAppId appid,
            int contextId,
            ObservableCollection<SteamItemsModel> itemsToTrade,
            bool onlyUnmarketable)
        {
            form.ProcessMethod(
                () =>
                    {
                        try
                        {
                            form.AppendLog($"{appid.AppId}-{contextId} inventory loading started");

                            var page = this.LoadInventoryPage(this.SteamId, appid.AppId, contextId);
                            form.AppendLog($"{page.TotalInventoryCount} items found");

                            var totalPagesCount = (int)Math.Ceiling(page.TotalInventoryCount / 5000d);
                            var currentPage = 1;

                            form.ProgressBarMaximum = totalPagesCount;
                            this.ProcessTradeSendInventoryPage(itemsToTrade, page, onlyUnmarketable);

                            form.AppendLog($"Page {currentPage++}/{totalPagesCount} loaded");
                            form.IncrementProgress();

                            while (page.MoreItems == 1)
                            {
                                if (form.CancellationToken.IsCancellationRequested)
                                {
                                    form.AppendLog($"{appid.Name} inventory loading was force stopped");
                                    return;
                                }

                                page = this.LoadInventoryPage(this.SteamId, appid.AppId, contextId, page.LastAssetid);

                                this.ProcessTradeSendInventoryPage(itemsToTrade, page, onlyUnmarketable);

                                form.AppendLog($"Page {currentPage++}/{totalPagesCount} loaded");
                                form.IncrementProgress();
                            }

                            form.AppendLog(
                                itemsToTrade.Any()
                                    ? $"{itemsToTrade.ToArray().Sum(i => i.Count)} tradable items was loaded"
                                    : $"Seems like no items found on {appid.Name} inventory");
                        }
                        catch (Exception e)
                        {
                            var message = $"Error on {appid.Name} inventory loading";

                            form.AppendLog(message);
                            ErrorNotify.CriticalMessageBox(message, e);
                            itemsToTrade.ClearDispatch();
                        }
                    });
        }

        public void LoadMarketListings(WorkingProcessForm form, ObservableCollection<MarketRelistModel> relistItemsList)
        {
            form.ProcessMethod(
                () =>
                    {
                        try
                        {
                            form.AppendLog("Market listings loading started");

                            var page = this.MarketClient.FetchSellOrders();

                            form.AppendLog($"{page.TotalCount} items found");
                            var totalCount = page.TotalCount;
                            var totalPagesCount = (int)Math.Ceiling(totalCount / 100d);
                            var currentPage = 1;
                            form.ProgressBarMaximum = totalPagesCount;

                            this.ProcessMarketSellListingsPage(page, relistItemsList);
                            form.AppendLog($"Page {currentPage++}/{totalPagesCount} loaded");
                            form.IncrementProgress();

                            for (var startItemIndex = 100; startItemIndex < totalCount; startItemIndex += 100)
                            {
                                if (form.CancellationToken.IsCancellationRequested)
                                {
                                    form.AppendLog("Market listings loading was force stopped");
                                    return;
                                }

                                page = this.MarketClient.FetchSellOrders(startItemIndex);
                                this.ProcessMarketSellListingsPage(page, relistItemsList);
                                form.AppendLog($"Page {currentPage++}/{totalPagesCount} loaded");
                                form.IncrementProgress();
                            }
                        }
                        catch (Exception e)
                        {
                            var message = "Error on market listings loading";

                            form.AppendLog(message);
                            ErrorNotify.CriticalMessageBox(message, e);
                            relistItemsList.ClearDispatch();
                        }
                    });
        }

        public void RelistListings(
            WorkingProcessForm form,
            Task[] priceLoadTasksList,
            MarketRelistModel[] marketRelistModels,
            MarketSellStrategy sellStrategy)
        {
            form.ProcessMethod(
                () =>
                    {
                        try
                        {
                            var notReadyTasksCount = priceLoadTasksList.Count(task => task.IsCompleted == false);
                            if (notReadyTasksCount > 0)
                            {
                                form.AppendLog(
                                    $"Waiting for {notReadyTasksCount} not finished price load threads to avoid steam ban on requests");
                                new Waiter { Timeout = TimeSpan.FromMinutes(1) }.UntilSoft(
                                    () => priceLoadTasksList.All(task => task.IsCompleted));
                            }

                            var currentItemIndex = 1;
                            var totalItemsCount = marketRelistModels.Sum(x => x.ItemsList.Count);
                            form.ProgressBarMaximum = totalItemsCount;

                            var threadsCount = SettingsProvider.GetInstance().RelistThreadsCount;
                            var semaphore = new Semaphore(threadsCount, threadsCount);
                            foreach (var marketSellModel in marketRelistModels)
                            {
                                var packageElementIndex = 1;
                                foreach (var item in marketSellModel.ItemsList)
                                {
                                    if (form.CancellationToken.IsCancellationRequested)
                                    {
                                        form.AppendLog("Market relist process was force stopped");
                                        return;
                                    }

                                    try
                                    {
                                        semaphore.WaitOne();

                                        form.AppendLog(
                                            $"[{currentItemIndex}/{totalItemsCount}] Removing - [{packageElementIndex++}/{marketSellModel.Count}] - '{marketSellModel.ItemName}'");

                                        var itemCancelId = item.SaleId;
                                        var realIndex = currentItemIndex;
                                        Task.Run(
                                            () =>
                                                {
                                                    try
                                                    {
                                                        var result = this.MarketClient.CancelSellOrder(itemCancelId);
                                                        if (result != ECancelSellOrderStatus.Canceled)
                                                        {
                                                            form.AppendLog(
                                                                $"[{realIndex}/{totalItemsCount}] - Error on canceling sell order {marketSellModel.ItemName}");
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        form.AppendLog(
                                                            $"Error on canceling sell order {marketSellModel.ItemName} - {ex.Message}");
                                                        Logger.Log.Error(ex);
                                                    }

                                                    form.IncrementProgress();
                                                    semaphore.Release();
                                                });
                                    }
                                    catch (Exception ex)
                                    {
                                        form.AppendLog(
                                            $"Error on canceling sell order {marketSellModel.ItemName} - {ex.Message}");
                                        Logger.Log.Error(ex);
                                    }

                                    currentItemIndex++;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            var message = $"Critical error on market relist - {ex.Message}";
                            form.AppendLog(message);
                            ErrorNotify.CriticalMessageBox(message);
                        }
                    });
        }

        public void SaveAccount(SettingsSteamAccount account)
        {
            if (this.IsSessionUpdated == false) return;
            this.IsSessionUpdated = false;

            account.SteamApi = this.ApiKey;
            account.TradeToken = this.TradeToken;
            account.Currency = this.Currency;
            SettingsProvider.GetInstance().OnPropertyChanged("SteamAccounts");
        }

        public void SellOnMarket(
            WorkingProcessForm form,
            Task[] priceLoadTasksList,
            MarketSellProcessModel[] marketSellModels,
            MarketSellStrategy sellStrategy)
        {
            form.ProcessMethod(
                () =>
                    {
                        try
                        {
                            var notReadyTasksCount = priceLoadTasksList.Count(task => task.IsCompleted == false);
                            if (notReadyTasksCount > 0)
                            {
                                form.AppendLog(
                                    $"Waiting for {notReadyTasksCount} not finished price load threads to avoid steam ban on requests");
                                new Waiter { Timeout = TimeSpan.FromMinutes(1) }.UntilSoft(
                                    () => priceLoadTasksList.All(task => task.IsCompleted));
                            }

                            var currentItemIndex = 1;
                            var totalItemsCount = marketSellModels.Sum(x => x.Count);
                            form.ProgressBarMaximum = totalItemsCount;
                            var averagePriceDays = SettingsProvider.GetInstance().AveragePriceDays;

                            foreach (var marketSellModel in marketSellModels)
                            {
                                if (!marketSellModel.SellPrice.HasValue)
                                {
                                    form.AppendLog(
                                        $"Price for '{marketSellModel.ItemName}' is not loaded. Processing price");
                                    if (form.CancellationToken.IsCancellationRequested)
                                    {
                                        form.AppendLog("Market sell process was force stopped");
                                        return;
                                    }

                                    try
                                    {
                                        var price = this.GetAveragePriceWithCache(
                                            marketSellModel.ItemModel.Asset.Appid,
                                            marketSellModel.ItemModel.Description.MarketHashName,
                                            averagePriceDays);

                                        form.AppendLog(
                                            $"Average price for {averagePriceDays} days for '{marketSellModel.ItemName}' is - {price}");

                                        marketSellModel.AveragePrice = price;

                                        price = this.GetCurrentPriceWithCache(
                                            marketSellModel.ItemModel.Asset.Appid,
                                            marketSellModel.ItemModel.Description.MarketHashName);

                                        form.AppendLog($"Current price for '{marketSellModel.ItemName}' is - {price}");
                                        marketSellModel.CurrentPrice = price;
                                        marketSellModel.ProcessSellPrice(sellStrategy);
                                    }
                                    catch (Exception ex)
                                    {
                                        form.AppendLog($"Error on market price parse - {ex.Message}");
                                        form.AppendLog($"Skipping '{marketSellModel.ItemName}'");

                                        totalItemsCount -= marketSellModel.Count;
                                        form.ProgressBarMaximum = totalItemsCount;
                                        continue;
                                    }
                                }

                                var packageElementIndex = 1;
                                var errorsCount = 0;
                                foreach (var item in marketSellModel.ItemsList)
                                {
                                    if (form.CancellationToken.IsCancellationRequested)
                                    {
                                        form.AppendLog("Market sell process was force stopped");
                                        return;
                                    }

                                    try
                                    {
                                        form.AppendLog(
                                            $"[{currentItemIndex}/{totalItemsCount}] Selling - [{packageElementIndex++}/{marketSellModel.Count}] - '{marketSellModel.ItemName}' for {marketSellModel.SellPrice}");

                                        if (marketSellModel.SellPrice.HasValue)
                                        {
                                            this.SellOnMarket(item, marketSellModel.SellPrice.Value);
                                        }
                                        else
                                        {
                                            form.AppendLog(
                                                $"Error on selling '{marketSellModel.ItemName}' - Price is not loaded. Skipping item.");
                                            totalItemsCount -= marketSellModel.Count;
                                            form.ProgressBarMaximum = totalItemsCount;
                                            break;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        form.AppendLog($"Error on selling '{marketSellModel.ItemName}' - {ex.Message}");
                                        Logger.Log.Error(ex);
                                        if (++errorsCount == SettingsProvider.GetInstance().ErrorsOnSellToSkip)
                                        {
                                            form.AppendLog(
                                                $"{SettingsProvider.GetInstance().ErrorsOnSellToSkip} fails limit on sell {marketSellModel.ItemName} reached. Skipping item.");
                                            totalItemsCount -= marketSellModel.Count - packageElementIndex;
                                            form.ProgressBarMaximum = totalItemsCount;
                                            break;
                                        }
                                    }

                                    if (currentItemIndex % SettingsProvider.GetInstance().ItemsToTwoFactorConfirm == 0)
                                    {
                                        Task.Run(() => this.ConfirmMarketTransactions(form));
                                    }

                                    currentItemIndex++;
                                    form.IncrementProgress();
                                }
                            }

                            this.ConfirmMarketTransactions(form);
                        }
                        catch (Exception ex)
                        {
                            var message = $"Critical error on market sell - {ex.Message}";
                            form.AppendLog(message);
                            ErrorNotify.CriticalMessageBox(message);
                        }
                    });
        }

        public void SendTrade(WorkingProcessForm form, FullRgItem[] itemsToTrade, bool acceptTwoFactor)
        {
            // this.OfferSession.SendTradeOffer()
        }

        private void ProcessMarketSellInventoryPage(
            ObservableCollection<MarketSellModel> marketSellItems,
            InventoryRootModel inventoryPage)
        {
            var items = this.Inventory.ProcessInventoryPage(inventoryPage).ToArray();

            items = this.Inventory.FilterInventory(items, true, false);

            var groupedItems = items.GroupBy(i => i.Description.MarketHashName).ToArray();

            foreach (var group in groupedItems)
            {
                var existModel = marketSellItems.FirstOrDefault(
                    item => item.ItemModel.Description.MarketHashName == group.Key);

                if (existModel != null)
                {
                    foreach (var groupItem in group.ToArray())
                    {
                        existModel.ItemsList.Add(groupItem);
                    }

                    existModel.RefreshCount();
                }
                else
                {
                    marketSellItems.AddDispatch(new MarketSellModel(group.ToArray()));
                }
            }
        }

        private void ProcessMarketSellListingsPage(
            SellListingsPage sellListingsPage,
            ObservableCollection<MarketRelistModel> marketSellListings)
        {
            var groupedItems = sellListingsPage.SellListings.ToArray().GroupBy(x => new { x.HashName, x.Price });

            foreach (var group in groupedItems)
            {
                var existModel = marketSellListings.FirstOrDefault(
                    item => item.ItemModel.HashName == group.Key.HashName && item.ItemModel.Price == group.Key.Price);

                if (existModel != null)
                {
                    foreach (var groupItem in group.ToArray())
                    {
                        existModel.ItemsList.Add(groupItem);
                    }

                    existModel.RefreshCount();
                }
                else
                {
                    marketSellListings.AddDispatch(new MarketRelistModel(group.ToArray()));
                }
            }
        }

        private void ProcessTradeSendInventoryPage(
            ObservableCollection<SteamItemsModel> marketSellItems,
            InventoryRootModel inventoryPage,
            bool onlyUnmarketable)
        {
            var items = this.Inventory.ProcessInventoryPage(inventoryPage).ToArray();

            items = this.Inventory.FilterInventory(items, false, true);

            if (onlyUnmarketable)
            {
                items = items.Where(i => i.Description.IsMarketable == false).ToArray();
            }

            var groupedItems = items.GroupBy(i => i.Description.MarketHashName).ToArray();

            foreach (var group in groupedItems)
            {
                var existModel = marketSellItems.FirstOrDefault(
                    item => item.ItemModel.Description.MarketHashName == group.Key);

                if (existModel != null)
                {
                    foreach (var groupItem in group.ToArray())
                    {
                        existModel.ItemsList.Add(groupItem);
                    }

                    existModel.RefreshCount();
                }
                else
                {
                    marketSellItems.AddDispatch(new SteamItemsModel(group.ToArray()));
                }
            }
        }
    }
}