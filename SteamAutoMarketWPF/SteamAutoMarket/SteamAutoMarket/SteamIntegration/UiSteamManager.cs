namespace SteamAutoMarket.SteamIntegration
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using Core.Waiter;

    using Steam;
    using Steam.SteamAuth;
    using Steam.TradeOffer.Models;

    using SteamAutoMarket.Models;
    using SteamAutoMarket.Pages;
    using SteamAutoMarket.Repository.Context;
    using SteamAutoMarket.Repository.PriceCache;
    using SteamAutoMarket.Repository.Settings;
    using SteamAutoMarket.Utils.Extension;
    using SteamAutoMarket.Utils.Logger;

    public class UiSteamManager : SteamManager
    {
        public UiSteamManager(
            string login,
            string password,
            SteamGuardAccount mafile,
            string apiKey,
            bool forceSessionRefresh = false)
            : base(login, password, mafile, apiKey, forceSessionRefresh)
        {
            this.SaveAccount();

            this.CurrentPriceCache = PriceCacheProvider.GetCurrentPriceCache(
                "ru", // todo actual currency
                SettingsProvider.GetInstance().HoursToBecomeOld);

            this.AveragePriceCache = PriceCacheProvider.GetAveragePriceCache(
                "ru",
                SettingsProvider.GetInstance().AveragePriceDays,
                SettingsProvider.GetInstance().HoursToBecomeOld);
        }

        public PriceCache AveragePriceCache { get; set; }

        public PriceCache CurrentPriceCache { get; set; }

        public async Task ConfirmMarketTransactions(WorkingProcessForm form)
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

        public override double? GetCurrentPrice(int appid, string hashName)
        {
            var price = base.GetCurrentPrice(appid, hashName);
            if (price.HasValue) this.CurrentPriceCache.Cache(hashName, price.Value);
            return price;
        }

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
                            this.ProcessInventoryPage(marketSellItems, page);

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

                                this.ProcessInventoryPage(marketSellItems, page);

                                form.AppendLog($"Page {currentPage++}/{totalPagesCount} loaded");
                                form.IncrementProgress();
                            }

                            form.AppendLog($"{marketSellItems.ToList().Sum(i => i.Count)} marketable items was loaded");
                        }
                        catch (Exception e)
                        {
                            var message = $"Error on {appid} inventory loading";

                            form.AppendLog(message);
                            ErrorNotify.CriticalMessageBox(message, e);
                            marketSellItems.ClearDispatch();
                        }
                    });
        }

        public void SaveAccount()
        {
            if (this.IsSessionUpdated == false) return;
            this.IsSessionUpdated = false;

            SettingsProvider.GetInstance().OnPropertyChanged("SteamAccounts");
        }

        public void SellOnMarket(
            WorkingProcessForm form,
            List<Task> priceLoadTasksList,
            List<MarketSellProcessModel> marketSellModels,
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
                                        var price =
                                            this.AveragePriceCache.Get(
                                                marketSellModel.ItemModel.Description.MarketHashName)?.Price
                                            ?? UiGlobalVariables.SteamManager.GetAveragePrice(
                                                marketSellModel.ItemModel.Asset.Appid,
                                                marketSellModel.ItemModel.Description.MarketHashName,
                                                averagePriceDays);

                                        form.AppendLog(
                                            $"Average price for {averagePriceDays} days for '{marketSellModel.ItemName}' is - {price}");

                                        marketSellModel.AveragePrice = price;

                                        price =
                                            this.CurrentPriceCache.Get(
                                                marketSellModel.ItemModel.Description.MarketHashName)?.Price
                                            ?? UiGlobalVariables.SteamManager.GetCurrentPrice(
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

                            Task.Run(() => this.ConfirmMarketTransactions(form));
                        }
                        catch (Exception ex)
                        {
                            var message = $"Critical error on market sell - {ex.Message}";
                            form.AppendLog(message);
                            ErrorNotify.CriticalMessageBox(message);
                        }
                    });
        }

        private void ProcessInventoryPage(
            ICollection<MarketSellModel> marketSellItems,
            InventoryRootModel inventoryPage)
        {
            var items = this.Inventory.ProcessInventoryPage(inventoryPage);

            var groupedItems = items.Where(i => i.Description.IsMarketable).GroupBy(i => i.Description.MarketHashName)
                .ToList();

            foreach (var group in groupedItems) marketSellItems.AddDispatch(new MarketSellModel(group.ToList()));
        }
    }
}