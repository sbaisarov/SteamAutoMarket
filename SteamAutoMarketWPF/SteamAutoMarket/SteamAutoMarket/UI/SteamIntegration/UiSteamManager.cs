namespace SteamAutoMarket.UI.SteamIntegration
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.Core.Waiter;
    using SteamAutoMarket.Steam;
    using SteamAutoMarket.Steam.Auth;
    using SteamAutoMarket.Steam.Market;
    using SteamAutoMarket.Steam.Market.Enums;
    using SteamAutoMarket.Steam.Market.Exceptions;
    using SteamAutoMarket.Steam.Market.Models;
    using SteamAutoMarket.Steam.TradeOffer.Enums;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Models.Enums;
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
                forceSessionRefresh,
                account.Proxy)
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

        public void AcceptTradeOffersWorkingProcess(
            IEnumerable<FullTradeOffer> offers,
            TimeSpan delay,
            int threadsCount,
            WorkingProcessDataContext wp)
        {
            var semaphore = new Semaphore(threadsCount, threadsCount);

            foreach (var offer in offers)
            {
                if (wp.CancellationToken.IsCancellationRequested)
                {
                    wp.AppendLog("Trade offer accept process was force stopped");
                    return;
                }

                semaphore.WaitOne();
                Task.Run(
                    () =>
                        {
                            try
                            {
                                var response = this.OfferSession.Accept(offer.Offer.TradeOfferId);
                                if (response.Accepted == false)
                                {
                                    if (string.IsNullOrEmpty(response.TradeError))
                                        response.TradeError = "internal server";

                                    wp.AppendLog(
                                        $"Steam replied with {response.TradeError} error on {offer.Offer.TradeOfferId} trade offer accept.");
                                }
                                else
                                {
                                    wp.AppendLog($"{offer.Offer.TradeOfferId} trade offer accept was successful");
                                }
                            }
                            catch (Exception e)
                            {
                                wp.AppendLog($"Error on {offer.Offer.TradeOfferId} offer accept - {e.Message}");
                            }
                            finally
                            {
                                Thread.Sleep(delay);
                                semaphore.Release();
                            }
                        });
            }

            for (var i = 0; i < threadsCount; i++)
            {
                semaphore.WaitOne();
            }
        }

        public void ConfirmTradeTransactionsWorkingProcess(List<ulong> offerIdList, WorkingProcessDataContext wp)
        {
            var notFoundRetry = 0;
            while (true)
            {
                if (wp.CancellationToken.IsCancellationRequested)
                {
                    wp.AppendLog("Trade offer confirm process was force stopped");
                    return;
                }

                try
                {
                    var confirmations = this.Guard.FetchConfirmations();
                    var conf = confirmations.Where(
                        item => item.ConfType == Confirmation.ConfirmationType.Trade
                                && offerIdList.Contains(item.Creator)).ToArray();

                    if (conf.Any() == false)
                    {
                        if (notFoundRetry++ > 3)
                        {
                            wp.AppendLog("Trade not found more then 3 times. Aborting confirmation process");
                            break;
                        }

                        wp.AppendLog($"{offerIdList} trade not found. Retrying in 10 seconds");
                        Thread.Sleep(TimeSpan.FromSeconds(10));
                    }
                    else
                    {
                        var success = this.Guard.AcceptMultipleConfirmations(conf);
                        if (success)
                        {
                            wp.AppendLog($"{offerIdList.Count} trades was successfully confirmed.");
                            wp.AppendLog("Waiting 10 seconds to fetch confirmations to verify they are empty");
                            Thread.Sleep(TimeSpan.FromSeconds(10));
                            confirmations = this.Guard.FetchConfirmations();
                            conf = confirmations.Where(
                                item => item.ConfType == Confirmation.ConfirmationType.Trade
                                        && offerIdList.Contains(item.Creator)).ToArray();

                            if (conf.Any() == false)
                            {
                                wp.AppendLog("Trade not found. Confirmation process finished");
                                break;
                            }
                        }
                        else
                        {
                            wp.AppendLog($"{offerIdList.Count} trades confirmation was failed. Retrying in 10 seconds");
                            Thread.Sleep(TimeSpan.FromSeconds(10));
                        }
                    }
                }
                catch (Exception e)
                {
                    wp.AppendLog($"Error on trade confirm - {e.Message}. Retrying in 10 seconds");
                    Logger.Log.Error(($"Error on trade confirm - {e.Message}", e));
                    Thread.Sleep(TimeSpan.FromSeconds(10));
                }
            }
        }

        public void DeclineTradeOffersWorkingProcess(
            IEnumerable<FullTradeOffer> offers,
            TimeSpan delay,
            int threadsCount,
            WorkingProcessDataContext wp)
        {
            var semaphore = new Semaphore(threadsCount, threadsCount);

            foreach (var offer in offers)
            {
                if (wp.CancellationToken.IsCancellationRequested)
                {
                    wp.AppendLog("Trade offer decline process was force stopped");
                    return;
                }

                semaphore.WaitOne();
                Task.Run(
                    () =>
                        {
                            try
                            {
                                var response = this.OfferSession.Decline(offer.Offer.TradeOfferId);
                                if (response == false)
                                {
                                    wp.AppendLog(
                                        $"Steam replied with error on {offer.Offer.TradeOfferId} trade offer decline.");
                                }
                                else
                                {
                                    wp.AppendLog($"{offer.Offer.TradeOfferId} trade offer decline was successful");
                                }
                            }
                            catch (Exception e)
                            {
                                wp.AppendLog($"Error on {offer.Offer.TradeOfferId} offer decline - {e.Message}");
                            }
                            finally
                            {
                                Thread.Sleep(delay);
                                semaphore.Release();
                            }
                        });
            }

            for (var i = 0; i < threadsCount; i++)
            {
                semaphore.WaitOne();
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
            SteamAppId appid,
            int contextId,
            ObservableCollection<MarketSellModel> marketSellItems,
            WorkingProcessDataContext wp)
        {
            try
            {
                wp.AppendLog($"{appid.AppId}-{contextId} inventory loading started");

                var page = this.LoadInventoryPage(this.SteamId, appid.AppId, contextId, cookies: this.Cookies);

                if (page == null)
                {
                    wp.AppendLog($"{appid.Name} No items found");
                    return;
                }

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
        }

        public void LoadItemsToTradeWorkingProcess(
            SteamAppId appid,
            int contextId,
            ObservableCollection<SteamItemsModel> itemsToTrade,
            WorkingProcessDataContext wp)
        {
            try
            {
                wp.AppendLog($"{appid.AppId}-{contextId} inventory loading started");

                var page = this.LoadInventoryPage(this.SteamId, appid.AppId, contextId, cookies: this.Cookies);
                wp.AppendLog($"{page.TotalInventoryCount} items found");

                var totalPagesCount = (int)Math.Ceiling(page.TotalInventoryCount / 5000d);
                var currentPage = 1;

                wp.ProgressBarMaximum = totalPagesCount;
                this.ProcessTradeSendInventoryPage(itemsToTrade, page);

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

                    this.ProcessTradeSendInventoryPage(itemsToTrade, page);

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
        }

        public void LoadMarketListings(
            ObservableCollection<MarketRelistModel> relistItemsList,
            WorkingProcessDataContext wp)
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
        }

        public void RelistListings(
            Task[] priceLoadTasksList,
            MarketRelistModel[] marketRelistModels,
            MarketSellStrategy sellStrategy,
            WorkingProcessDataContext wp)
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
                        try
                        {
                            semaphore.WaitOne();

                            if (wp.CancellationToken.IsCancellationRequested)
                            {
                                wp.AppendLog("Market relist process was force stopped");
                                return;
                            }

                            var itemCancelId = item.SaleId;
                            var realIndex = currentItemIndex;
                            var realPackageIndex = packageElementIndex++;
                            Task.Run(
                                () =>
                                    {
                                        try
                                        {
                                            var tryCount = 0;
                                            while (true)
                                            {
                                                if (wp.CancellationToken.IsCancellationRequested)
                                                {
                                                    wp.AppendLog(
                                                        $"Market {marketSellModel.ItemName} relist task was force stopped");
                                                    return;
                                                }

                                                if (tryCount++ > 10)
                                                {
                                                    wp.AppendLog(
                                                        $"[{realIndex}/{totalItemsCount}] Removing - [{realPackageIndex}/{marketSellModel.Count}] - '{marketSellModel.ItemName}' failed more then 10 times. Skipping item.");
                                                    break;
                                                }

                                                wp.AppendLog(
                                                    $"[{realIndex}/{totalItemsCount}] Removing - [{realPackageIndex}/{marketSellModel.Count}] - '{marketSellModel.ItemName}'");

                                                var result = this.MarketClient.CancelSellOrder(itemCancelId);

                                                if (result != ECancelSellOrderStatus.Canceled)
                                                {
                                                    wp.AppendLog(
                                                        $"[{realIndex}/{totalItemsCount}] - Error on canceling sell order {marketSellModel.ItemName}");
                                                }
                                                else
                                                {
                                                    break;
                                                }
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
                                        finally
                                        {
                                            wp.IncrementProgress();
                                            semaphore.Release();
                                        }
                                    });
                        }
                        catch (Exception ex)
                        {
                            wp.AppendLog($"Error on canceling sell order {marketSellModel.ItemName} - {ex.Message}");
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
            MarketSellStrategy sellStrategy,
            WorkingProcessDataContext wp)
        {
            try
            {
                var itemsToConfirm = SettingsProvider.GetInstance().ItemsToTwoFactorConfirm;

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

                double totalSellPrice = 0;
                var currencySymbol = SteamCurrencies.Currencies[this.Currency.ToString()];

                foreach (var marketSellModel in marketSellModels)
                {
                    if (!marketSellModel.SellPrice.HasValue)
                    {
                        wp.AppendLog($"Price for '{marketSellModel.ItemName}' is not loaded. Processing price");
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
                                $"Average price for {averagePriceDays} days for '{marketSellModel.ItemName}' is - {price} {currencySymbol}");

                            marketSellModel.AveragePrice = price;

                            price = this.GetCurrentPriceWithCache(
                                marketSellModel.ItemModel.Asset.Appid,
                                marketSellModel.ItemModel.Description.MarketHashName);

                            wp.AppendLog(
                                $"Current price for '{marketSellModel.ItemName}' is - {price} {currencySymbol}");
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
                                $"[{currentItemIndex}/{totalItemsCount}] Selling - [{packageElementIndex++}/{marketSellModel.Count}] - '{marketSellModel.ItemName}' for {marketSellModel.SellPrice} {currencySymbol}");

                            if (marketSellModel.SellPrice.HasValue)
                            {
                                this.SellOnMarket(item, marketSellModel.SellPrice.Value);
                                totalSellPrice += marketSellModel.SellPrice.Value;
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
                            Logger.Log.Error($"Error on selling '{marketSellModel.ItemName}' - {ex.Message}", ex);

                            MarketSellUtils.ProcessErrorOnMarketSell(marketSellModel, item, this, ex.Message, wp);
                        }

                        if (currentItemIndex % itemsToConfirm == 0)
                        {
                            MarketSellUtils.ConfirmMarketTransactionsWorkingProcess(this.Guard, wp);
                            wp.AppendLog(
                                $"Total price of all successfully placed lots - {totalSellPrice:F} {currencySymbol}");
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
        }

        public void SendTrade(
            string targetSteamId,
            string tradeToken,
            FullRgItem[] itemsToTrade,
            bool acceptTwoFactor,
            WorkingProcessDataContext wp)
        {
            try
            {
                var targetSteamIdObj = targetSteamId.StartsWith("76561198")
                                           ? new SteamID(ulong.Parse(targetSteamId))
                                           : new SteamID(
                                               uint.Parse(targetSteamId),
                                               EUniverse.Public,
                                               EAccountType.Individual);

                wp.AppendLog(
                    $"Sending trade offer to {targetSteamIdObj.ConvertToUInt64()} - {tradeToken}. My items count - {itemsToTrade.Length}");

                var tradeId = this.SendTradeOffer(itemsToTrade, targetSteamIdObj, tradeToken);
                if (string.IsNullOrEmpty(tradeId))
                {
                    throw new SteamException("Steam returned empty trade id");
                }

                wp.AppendLog($"Sent trade offer id is - {tradeId}");

                if (acceptTwoFactor)
                {
                    this.ConfirmTradeTransactionsWorkingProcess(new List<ulong> { ulong.Parse(tradeId) }, wp);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Debug("Error on trade offer send", ex);
                wp.AppendLog($"Error on trade offer send - {ex.Message}");
            }
        }

        public void TradeAccepterWorkingProcess(
            WorkingProcessDataContext wp,
            IEnumerable<ETradeAccepter> modes,
            int threadsCount,
            TimeSpan timeSpanDelay,
            IEnumerable<string> whitelist)
        {
            while (true)
            {
                if (wp.CancellationToken.IsCancellationRequested)
                {
                    wp.AppendLog("TradeAccepter process was force stopped");
                    return;
                }

                try
                {
                    wp.AppendLog("Fetching trade offers..");

                    var allOffers = UiGlobalVariables.SteamManager
                        .ReceiveTradeOffers(true, true, getDescriptions: false).Where(
                            o => o.Offer.TradeOfferState == TradeOfferState.TradeOfferStateActive).ToList();

                    var receivedOffers = allOffers.Where(o => o.Offer.IsOurOffer == false).ToList();
                    var sentOffers = allOffers.Where(o => o.Offer.IsOurOffer).ToList();

                    if (allOffers.Any())
                    {
                        wp.AppendLog(
                            $"{allOffers.Count} trade offers found. Received - {receivedOffers.Count}, Sent - {sentOffers.Count})");
                        foreach (var mode in modes)
                        {
                            if (wp.CancellationToken.IsCancellationRequested)
                            {
                                wp.AppendLog("TradeAccepter process was force stopped");
                                return;
                            }

                            switch (mode)
                            {
                                case ETradeAccepter.AcceptIncomingEmpty:
                                    wp.AppendLog("Processing incoming empty from my side offers to accept");
                                    var incomingEmptyOffers = receivedOffers.Where(o => o.MyItems.Any() == false);

                                    wp.AppendLog($"{incomingEmptyOffers.Count()} offers found");

                                    if (incomingEmptyOffers.Any())
                                    {
                                        UiGlobalVariables.SteamManager.AcceptTradeOffersWorkingProcess(
                                            incomingEmptyOffers,
                                            timeSpanDelay,
                                            threadsCount,
                                            wp);

                                        RemoveProcessedOffers(receivedOffers, incomingEmptyOffers);
                                    }

                                    break;

                                case ETradeAccepter.AcceptIncomingWhitelist:
                                    wp.AppendLog(
                                        $"Processing incoming offers from {whitelist.Count()} whitelist users to accept");

                                    var incomingWhiteListOffers = receivedOffers.Where(
                                        o => whitelist.Contains(
                                            new SteamID(
                                                (uint)o.Offer.AccountIdOther,
                                                EUniverse.Public,
                                                EAccountType.Individual).ConvertToUInt64().ToString()));

                                    wp.AppendLog($"{incomingWhiteListOffers.Count()} offers found");

                                    if (incomingWhiteListOffers.Any())
                                    {
                                        UiGlobalVariables.SteamManager.AcceptTradeOffersWorkingProcess(
                                            incomingWhiteListOffers,
                                            timeSpanDelay,
                                            threadsCount,
                                            wp);

                                        UiGlobalVariables.SteamManager.ConfirmTradeTransactionsWorkingProcess(
                                            incomingWhiteListOffers.Select(o => ulong.Parse(o.Offer.TradeOfferId))
                                                .ToList(),
                                            wp);

                                        RemoveProcessedOffers(receivedOffers, incomingWhiteListOffers);
                                    }

                                    break;

                                case ETradeAccepter.DeclineIncomingNotEmpty:
                                    wp.AppendLog("Processing incoming not empty from my side offers to decline");
                                    var incomingNotEmptyOffers = receivedOffers.Where(o => o.MyItems.Any());

                                    wp.AppendLog($"{incomingNotEmptyOffers.Count()} offers found");

                                    if (incomingNotEmptyOffers.Any())
                                    {
                                        UiGlobalVariables.SteamManager.DeclineTradeOffersWorkingProcess(
                                            incomingNotEmptyOffers,
                                            timeSpanDelay,
                                            threadsCount,
                                            wp);

                                        RemoveProcessedOffers(receivedOffers, incomingNotEmptyOffers);
                                    }

                                    break;

                                case ETradeAccepter.DeclineAllIncoming:
                                    wp.AppendLog("Processing all incoming offers to decline");
                                    wp.AppendLog($"{receivedOffers.Count} offers found");

                                    if (receivedOffers.Any())
                                    {
                                        UiGlobalVariables.SteamManager.DeclineTradeOffersWorkingProcess(
                                            receivedOffers,
                                            timeSpanDelay,
                                            threadsCount,
                                            wp);

                                        receivedOffers.Clear();
                                    }

                                    break;

                                case ETradeAccepter.DeclineOutgoing:
                                    wp.AppendLog("Processing all outgoing offers to decline");
                                    wp.AppendLog($"{sentOffers.Count} offers found");

                                    if (sentOffers.Any())
                                    {
                                        UiGlobalVariables.SteamManager.DeclineTradeOffersWorkingProcess(
                                            sentOffers,
                                            timeSpanDelay,
                                            threadsCount,
                                            wp);

                                        sentOffers.Clear();
                                    }

                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (wp.CancellationToken.IsCancellationRequested)
                        {
                            wp.AppendLog("TradeAccepter process was force stopped");
                            return;
                        }

                        wp.AppendLog("No active offers found");
                        Thread.Sleep(timeSpanDelay);
                    }
                }
                catch (Exception ex)
                {
                    wp.AppendLog(
                        $"Error on TradeAutoAccepter - {ex.Message}. Restarting TradeAutoAccepter working process.");
                    Logger.Log.Error($"Error on TradeAutoAccepter - {ex.Message}.", ex);
                    Thread.Sleep(timeSpanDelay);
                }
            }
        }

        private static void RemoveProcessedOffers(
            ICollection<FullTradeOffer> allOffers,
            IEnumerable<FullTradeOffer> sublist)
        {
            foreach (var offer in sublist.ToList())
            {
                allOffers.Remove(offer);
            }
        }

        private void ProcessMarketSellListingsPage(
            SellListingsPage sellListingsPage,
            ObservableCollection<MarketRelistModel> marketSellListings)
        {
            Logger.Log.Debug($"Processing listing page of {sellListingsPage?.TotalCount} items");

            var groupedItems = sellListingsPage?.SellListings.ToArray()
                .GroupBy(x => new { x.HashName, x.Price, x.Date });

            if (groupedItems == null)
            {
                return;
            }

            foreach (var group in groupedItems)
            {
                Logger.Log.Debug($"Processing {group.Key.HashName}-{group.Key.Price}-{group.Key.Date} group");

                var existModel = marketSellListings.FirstOrDefault(
                    item => item.ItemModel.HashName == group.Key.HashName && item.ItemModel.Price == group.Key.Price
                                                                          && item.ItemModel.Date == group.Key.Date);

                if (existModel != null)
                {
                    Logger.Log.Debug("Group already exist in items collection, adding items.");
                    existModel.ItemsList.AddRangeDispatch(group);
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
            InventoryRootModel inventoryPage)
        {
            var items = this.Inventory.ProcessInventoryPage(inventoryPage).ToArray();

            items = this.Inventory.FilterInventory(items, false, true);

            var groupedItems = items.GroupBy(x => new { x.Description.MarketHashName, x.Description.IsTradable })
                .ToArray();

            foreach (var group in groupedItems)
            {
                var existModel = marketSellItems.FirstOrDefault(
                    item => item.ItemModel.Description.MarketHashName == group.Key.MarketHashName
                            && item.ItemModel.Description.IsTradable == group.Key.IsTradable);

                if (existModel != null)
                {
                    existModel.ItemsList.AddRangeDispatch(group);
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