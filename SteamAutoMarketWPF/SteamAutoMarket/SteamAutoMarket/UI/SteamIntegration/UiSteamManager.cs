namespace SteamAutoMarket.UI.SteamIntegration
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.Core.Waiter;
    using SteamAutoMarket.Steam;
    using SteamAutoMarket.Steam.Auth;
    using SteamAutoMarket.Steam.Market.Enums;
    using SteamAutoMarket.Steam.Market.Exceptions;
    using SteamAutoMarket.Steam.Market.Models;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Pages;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.Repository.PriceCache;
    using SteamAutoMarket.UI.Repository.Settings;
    using SteamAutoMarket.UI.Utils.Extension;
    using SteamAutoMarket.UI.Utils.Logger;

    using SteamKit2;

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
            SteamAppId appid,
            int contextId,
            ObservableCollection<MarketSellModel> marketSellItems)
        {
            var wp = UiGlobalVariables.WorkingProcessDataContext;

            WorkingProcess.ProcessMethod(
                () =>
                    {
                        try
                        {
                            wp.AppendLog($"{appid.AppId}-{contextId} inventory loading started");

                            var page = this.LoadInventoryPage(
                                this.SteamId,
                                appid.AppId,
                                contextId,
                                cookies: this.Cookies);

                            wp.AppendLog($"{page.TotalInventoryCount} items found");

                            var totalPagesCount = (int)Math.Ceiling(page.TotalInventoryCount / 5000d);
                            var currentPage = 1;

                            wp.ProgressBarMaximum = totalPagesCount;
                            MarketSellUtils.ProcessMarketSellInventoryPage(marketSellItems, page, this.Inventory);

                            wp.AppendLog($"Page {currentPage++}/{totalPagesCount} loaded");
                            wp.IncrementProgress();

                            while (page.MoreItems == 1)
                            {
                                if (wp.CancellationToken.IsCancellationRequested)
                                {
                                    wp.AppendLog($"{appid.Name} inventory loading was force stopped");
                                    return;
                                }

                                page = this.LoadInventoryPage(
                                    this.SteamId,
                                    appid.AppId,
                                    contextId,
                                    page.LastAssetid,
                                    cookies: this.Cookies);
                                if (page == null)
                                {
                                    wp.AppendLog($"{appid.Name} No items found");
                                    return;
                                }

                                MarketSellUtils.ProcessMarketSellInventoryPage(marketSellItems, page, this.Inventory);

                                wp.AppendLog($"Page {currentPage++}/{totalPagesCount} loaded");
                                wp.IncrementProgress();
                            }

                            wp.AppendLog(
                                marketSellItems.Any()
                                    ? $"{marketSellItems.ToArray().Sum(i => i.Count)} marketable items was loaded"
                                    : $"Seems like no items found on {appid.Name} inventory");
                        }
                        catch (Exception e)
                        {
                            var message = $"Error on {appid.Name} inventory loading";

                            wp.AppendLog(message);
                            ErrorNotify.CriticalMessageBox(message, e);
                            marketSellItems.ClearDispatch();
                        }
                    },
                $"{appid.Name} inventory loading");
        }

        public void LoadItemsToTradeWorkingProcess(
            SteamAppId appid,
            int contextId,
            ObservableCollection<SteamItemsModel> itemsToTrade,
            bool onlyUnmarketable)
        {
            var wp = UiGlobalVariables.WorkingProcessDataContext;

            WorkingProcess.ProcessMethod(
                () =>
                    {
                        try
                        {
                            wp.AppendLog($"{appid.AppId}-{contextId} inventory loading started");

                            var page = this.LoadInventoryPage(
                                this.SteamId,
                                appid.AppId,
                                contextId,
                                cookies: this.Cookies);
                            wp.AppendLog($"{page.TotalInventoryCount} items found");

                            var totalPagesCount = (int)Math.Ceiling(page.TotalInventoryCount / 5000d);
                            var currentPage = 1;

                            wp.ProgressBarMaximum = totalPagesCount;
                            this.ProcessTradeSendInventoryPage(itemsToTrade, page, onlyUnmarketable);

                            wp.AppendLog($"Page {currentPage++}/{totalPagesCount} loaded");
                            wp.IncrementProgress();

                            while (page.MoreItems == 1)
                            {
                                if (wp.CancellationToken.IsCancellationRequested)
                                {
                                    wp.AppendLog($"{appid.Name} inventory loading was force stopped");
                                    return;
                                }

                                page = this.LoadInventoryPage(
                                    this.SteamId,
                                    appid.AppId,
                                    contextId,
                                    page.LastAssetid,
                                    cookies: this.Cookies);

                                this.ProcessTradeSendInventoryPage(itemsToTrade, page, onlyUnmarketable);

                                wp.AppendLog($"Page {currentPage++}/{totalPagesCount} loaded");
                                wp.IncrementProgress();
                            }

                            wp.AppendLog(
                                itemsToTrade.Any()
                                    ? $"{itemsToTrade.ToArray().Sum(i => i.Count)} tradable items was loaded"
                                    : $"Seems like no items found on {appid.Name} inventory");
                        }
                        catch (Exception e)
                        {
                            var message = $"Error on {appid.Name} inventory loading";

                            wp.AppendLog(message);
                            ErrorNotify.CriticalMessageBox(message, e);
                            itemsToTrade.ClearDispatch();
                        }
                    },
                $"{appid.Name} inventory loading");
        }

        public void LoadMarketListings(ObservableCollection<MarketRelistModel> relistItemsList)
        {
            var wp = UiGlobalVariables.WorkingProcessDataContext;
            WorkingProcess.ProcessMethod(
                () =>
                    {
                        try
                        {
                            wp.AppendLog("Market listings loading started");

                            var page = this.MarketClient.FetchSellOrders();

                            wp.AppendLog($"{page.TotalCount} items found");
                            var totalCount = page.TotalCount;
                            Logger.Log.Debug($"[LoadMarketListings] Total items count is - {totalCount}");
                            var totalPagesCount = (int)Math.Ceiling(totalCount / 100d);
                            Logger.Log.Debug($"[LoadMarketListings] Total pages count is - {totalPagesCount}");
                            var currentPage = 1;
                            wp.ProgressBarMaximum = totalPagesCount;

                            this.ProcessMarketSellListingsPage(page, relistItemsList);
                            wp.AppendLog($"Page {currentPage++}/{totalPagesCount} loaded");
                            wp.IncrementProgress();

                            for (var startItemIndex = 100; startItemIndex < totalCount; startItemIndex += 100)
                            {
                                Logger.Log.Debug(
                                    $"[LoadMarketListings] Processing listing page with start index - {startItemIndex}");
                                if (wp.CancellationToken.IsCancellationRequested)
                                {
                                    wp.AppendLog("Market listings loading was force stopped");
                                    return;
                                }

                                page = this.MarketClient.FetchSellOrders(startItemIndex);
                                this.ProcessMarketSellListingsPage(page, relistItemsList);
                                wp.AppendLog($"Page {currentPage++}/{totalPagesCount} loaded");
                                wp.IncrementProgress();
                            }
                        }
                        catch (Exception e)
                        {
                            const string Message = "Error on market listings loading";
                            wp.AppendLog(Message);
                            ErrorNotify.CriticalMessageBox(Message, e);
                            relistItemsList.ClearDispatch();
                        }
                    },
                "Market listings loading");
        }

        public void RelistListings(
            Task[] priceLoadTasksList,
            MarketRelistModel[] marketRelistModels,
            MarketSellStrategy sellStrategy)
        {
            var wp = UiGlobalVariables.WorkingProcessDataContext;
            WorkingProcess.ProcessMethod(
                () =>
                    {
                        try
                        {
                            var notReadyTasksCount = priceLoadTasksList.Count(task => task.IsCompleted == false);
                            if (notReadyTasksCount > 0)
                            {
                                wp.AppendLog(
                                    $"Waiting for {notReadyTasksCount} not finished price load threads to avoid steam ban on requests");
                                new Waiter { Timeout = TimeSpan.FromMinutes(1) }.UntilSoft(
                                    () => priceLoadTasksList.All(task => task.IsCompleted));
                            }

                            var currentItemIndex = 1;
                            var totalItemsCount = marketRelistModels.Sum(x => x.ItemsList.Count);
                            wp.ProgressBarMaximum = totalItemsCount;

                            var threadsCount = SettingsProvider.GetInstance().RelistThreadsCount;
                            var semaphore = new Semaphore(threadsCount, threadsCount);
                            foreach (var marketSellModel in marketRelistModels)
                            {
                                var packageElementIndex = 1;
                                foreach (var item in marketSellModel.ItemsList)
                                {
                                    if (wp.CancellationToken.IsCancellationRequested)
                                    {
                                        wp.AppendLog("Market relist process was force stopped");
                                        return;
                                    }

                                    try
                                    {
                                        semaphore.WaitOne();

                                        wp.AppendLog(
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
                                                            wp.AppendLog(
                                                                $"[{realIndex}/{totalItemsCount}] - Error on canceling sell order {marketSellModel.ItemName}");
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        wp.AppendLog(
                                                            $"Error on canceling sell order {marketSellModel.ItemName} - {ex.Message}");
                                                        Logger.Log.Error(
                                                            $"Error on canceling sell order {marketSellModel.ItemName} - {ex.Message}",
                                                            ex);
                                                    }

                                                    wp.IncrementProgress();
                                                    semaphore.Release();
                                                });
                                    }
                                    catch (Exception ex)
                                    {
                                        wp.AppendLog(
                                            $"Error on canceling sell order {marketSellModel.ItemName} - {ex.Message}");
                                        Logger.Log.Error(
                                            $"Error on canceling sell order {marketSellModel.ItemName} - {ex.Message}",
                                            ex);
                                    }

                                    currentItemIndex++;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            var message = $"Critical error on market relist - {ex.Message}";
                            wp.AppendLog(message);
                            ErrorNotify.CriticalMessageBox(message);
                        }
                    },
                "Market relist");
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

        public void SellOnMarketWorkingProcess(
            Task[] priceLoadTasksList,
            MarketSellProcessModel[] marketSellModels,
            MarketSellStrategy sellStrategy)
        {
            var wp = UiGlobalVariables.WorkingProcessDataContext;
            WorkingProcess.ProcessMethod(
                () =>
                    {
                        try
                        {
                            var notReadyTasksCount = priceLoadTasksList.Count(task => task.IsCompleted == false);
                            if (notReadyTasksCount > 0)
                            {
                                wp.AppendLog(
                                    $"Waiting for {notReadyTasksCount} not finished price load threads to avoid steam ban on requests");
                                new Waiter { Timeout = TimeSpan.FromMinutes(1) }.UntilSoft(
                                    () => priceLoadTasksList.All(task => task.IsCompleted));
                            }

                            var currentItemIndex = 1;
                            var totalItemsCount = marketSellModels.Sum(x => x.Count);
                            wp.ProgressBarMaximum = totalItemsCount;
                            var averagePriceDays = SettingsProvider.GetInstance().AveragePriceDays;

                            foreach (var marketSellModel in marketSellModels)
                            {
                                if (!marketSellModel.SellPrice.HasValue)
                                {
                                    wp.AppendLog(
                                        $"Price for '{marketSellModel.ItemName}' is not loaded. Processing price");
                                    if (wp.CancellationToken.IsCancellationRequested)
                                    {
                                        wp.AppendLog("Market sell process was force stopped");
                                        return;
                                    }

                                    try
                                    {
                                        var price = this.GetAveragePriceWithCache(
                                            marketSellModel.ItemModel.Asset.Appid,
                                            marketSellModel.ItemModel.Description.MarketHashName,
                                            averagePriceDays);

                                        wp.AppendLog(
                                            $"Average price for {averagePriceDays} days for '{marketSellModel.ItemName}' is - {price}");

                                        marketSellModel.AveragePrice = price;

                                        price = this.GetCurrentPriceWithCache(
                                            marketSellModel.ItemModel.Asset.Appid,
                                            marketSellModel.ItemModel.Description.MarketHashName);

                                        wp.AppendLog($"Current price for '{marketSellModel.ItemName}' is - {price}");
                                        marketSellModel.CurrentPrice = price;
                                        marketSellModel.ProcessSellPrice(sellStrategy);
                                    }
                                    catch (Exception ex)
                                    {
                                        wp.AppendLog($"Error on market price parse - {ex.Message}");
                                        wp.AppendLog($"Skipping '{marketSellModel.ItemName}'");

                                        totalItemsCount -= marketSellModel.Count;
                                        wp.ProgressBarMaximum = totalItemsCount;
                                        continue;
                                    }
                                }

                                var packageElementIndex = 1;
                                foreach (var item in marketSellModel.ItemsList)
                                {
                                    if (wp.CancellationToken.IsCancellationRequested)
                                    {
                                        wp.AppendLog("Market sell process was force stopped");
                                        return;
                                    }

                                    try
                                    {
                                        wp.AppendLog(
                                            $"[{currentItemIndex}/{totalItemsCount}] Selling - [{packageElementIndex++}/{marketSellModel.Count}] - '{marketSellModel.ItemName}' for {marketSellModel.SellPrice}");

                                        if (marketSellModel.SellPrice.HasValue)
                                        {
                                            this.SellOnMarket(item, marketSellModel.SellPrice.Value);
                                        }
                                        else
                                        {
                                            wp.AppendLog(
                                                $"Error on selling '{marketSellModel.ItemName}' - Price is not loaded. Skipping item.");
                                            totalItemsCount -= marketSellModel.Count;
                                            wp.ProgressBarMaximum = totalItemsCount;
                                            break;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        wp.AppendLog($"Error on selling '{marketSellModel.ItemName}' - {ex.Message}");
                                        Logger.Log.Error(
                                            $"Error on selling '{marketSellModel.ItemName}' - {ex.Message}",
                                            ex);

                                        if (ex.Message.Contains("You have too many listings pending confirmation"))
                                        {
                                            MarketSellUtils.ProcessTooManyListingsPendingConfirmation(
                                                this.Guard,
                                                this.MarketClient,
                                                this.Currency,
                                                wp,
                                                wp.CancellationToken);
                                        }
                                        else
                                        {
                                            MarketSellUtils.ProcessErrorOnMarketSell(
                                                marketSellModel,
                                                item,
                                                this,
                                                ex.Message, 
                                                wp);
                                        }
                                    }

                                    if (currentItemIndex % SettingsProvider.GetInstance().ItemsToTwoFactorConfirm == 0)
                                    {
                                        MarketSellUtils.ConfirmMarketTransactionsWorkingProcess(this.Guard, wp);
                                    }

                                    currentItemIndex++;
                                    wp.IncrementProgress();
                                }
                            }

                            MarketSellUtils.ConfirmMarketTransactionsWorkingProcess(this.Guard, wp);
                        }
                        catch (Exception ex)
                        {
                            Logger.Log.Error($"Critical error on market sell - {ex.Message}", ex);
                            wp.AppendLog($"Critical error on market sell - {ex.Message}");
                            ErrorNotify.CriticalMessageBox($"Critical error on market sell - {ex.Message}");
                        }
                    },
                "Market sell");
        }

        public void ConfirmTradeTransactionsWorkingProcess(ulong offerId, WorkingProcessDataContext wp)
        {
            var notFoundRetry = 0;
            while (true)
            {
                try
                {
                    var confirmations = this.Guard.FetchConfirmations();
                    var conf = confirmations.FirstOrDefault(
                        item => item.ConfType == Confirmation.ConfirmationType.Trade && (item.Creator == offerId));
                    if (conf == null)
                    {
                        notFoundRetry++;
                        if (notFoundRetry > 3)
                        {
                            wp.AppendLog("Trade not found more then 3 times. Seems send is failed. Aborting confirmation process");
                            break;
                        }

                        wp.AppendLog($"{offerId} trade not found. Retrying in 10 seconds");
                        Thread.Sleep(TimeSpan.FromSeconds(10));
                    }
                    else
                    {
                        var success = this.Guard.AcceptConfirmation(conf);
                        if (success)
                        {
                            wp.AppendLog($"{offerId} trade was successfully confirmed.");
                            wp.AppendLog("Waiting 5 seconds to fetch confirmations to verify they are empty");
                            Thread.Sleep(TimeSpan.FromSeconds(5));
                            confirmations = this.Guard.FetchConfirmations();
                            conf = confirmations.FirstOrDefault(
                                item => item.ConfType == Confirmation.ConfirmationType.Trade && (item.Creator == offerId));
                            if (conf == null)
                            {
                                wp.AppendLog("Trade not found. Confirmation process finished");
                                break;
                            }
                        }
                        else
                        {
                            wp.AppendLog($"{offerId} trade confirmation was failed. Retrying in 10 seconds");
                            Thread.Sleep(TimeSpan.FromSeconds(10));
                        }
                    }
                }
                catch (Exception e)
                {
                    wp.AppendLog($"Error on trade confirm - {e.Message}. Retrying in 10 seconds");
                    Logger.Log.Error(($"Error on trade confirm.", e));
                    Thread.Sleep(TimeSpan.FromSeconds(10));
                }
            }
        }

        public void SendTrade(string targetSteamId, string tradeToken, FullRgItem[] itemsToTrade, bool acceptTwoFactor)
        {
            var wp = UiGlobalVariables.WorkingProcessDataContext;

            WorkingProcess.ProcessMethod(
                () =>
                    {
                        try
                        {
                            var targetSteamIdObj = targetSteamId.StartsWith("76561198")
                                                       ? new SteamID(ulong.Parse(targetSteamId))
                                                       : new SteamID(
                                                           uint.Parse(targetSteamId),
                                                           EUniverse.Public,
                                                           EAccountType.Individual);

                            wp.AppendLog($"Sending trade offer to {targetSteamIdObj.ConvertToUInt64()} - {tradeToken}");

                            var tradeId = this.SendTradeOffer(itemsToTrade, targetSteamIdObj, tradeToken);
                            if (string.IsNullOrEmpty(tradeId))
                            {
                                throw new SteamException("Steam returned empty trade id");
                            }

                            wp.AppendLog($"Sent trade offer id is - {tradeId}");

                            if (acceptTwoFactor)
                            {
                                var numberTradeId = ulong.Parse(tradeId);
                                this.ConfirmTradeTransactionsWorkingProcess(numberTradeId, wp);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Log.Debug("Error on trade offer send", ex);
                            wp.AppendLog($"Error on trade offer send - {ex.Message}");
                        }
                    },
                "Trade send");
        }

        private void ProcessMarketSellListingsPage(
            SellListingsPage sellListingsPage,
            ObservableCollection<MarketRelistModel> marketSellListings)
        {
            Logger.Log.Debug($"Processing listing page of {sellListingsPage?.TotalCount} items");

            var groupedItems = sellListingsPage?.SellListings.ToArray().GroupBy(x => new { x.HashName, x.Price });

            foreach (var group in groupedItems)
            {
                Logger.Log.Debug($"Processing {group.Key.HashName}-{group.Key.Price} group");

                var existModel = marketSellListings.FirstOrDefault(
                    item => item.ItemModel.HashName == group.Key.HashName && item.ItemModel.Price == group.Key.Price);

                if (existModel != null)
                {
                    Logger.Log.Debug("Group already exist in items collection, adding items.");
                    foreach (var groupItem in group.ToArray())
                    {
                        existModel.ItemsList.Add(groupItem);
                    }

                    existModel.RefreshCount();
                }
                else
                {
                    Logger.Log.Debug("Group not exist in items collection, creating new group");
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