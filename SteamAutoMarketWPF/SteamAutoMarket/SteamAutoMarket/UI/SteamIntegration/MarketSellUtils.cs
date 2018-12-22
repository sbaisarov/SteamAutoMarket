namespace SteamAutoMarket.UI.SteamIntegration
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.Steam.Auth;
    using SteamAutoMarket.Steam.Market.Enums;
    using SteamAutoMarket.Steam.TradeOffer;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Pages;
    using SteamAutoMarket.UI.Repository.Settings;
    using SteamAutoMarket.UI.Utils.Extension;

    public static class MarketSellUtils
    {
        public static void ConfirmMarketTransactionsWorkingProcess(
            SteamGuardAccount guard,
            WorkingProcessDataContext wp)
        {
            while (true)
            {
                try
                {
                    wp.AppendLog("Fetching 2FA confirmations..");

                    var confirmations = guard.FetchConfirmations().Where(
                        item => item.ConfType == Confirmation.ConfirmationType.MarketSellTransaction).ToArray();
                    wp.AppendLog($"{confirmations.Length} confirmations found. Accepting confirmations");

                    var chunks = confirmations.Select((s, i) => new { Value = s, Index = i }).GroupBy(x => x.Index / 50)
                        .Select(grp => grp.Select(x => x.Value).ToArray()).ToArray();

                    var confirmationsSuccess = new List<bool>();
                    wp.AppendLog($"Splitting confirmation list by 50. {chunks.Length} chunks was obtained");
                    var chunkIndex = 1;
                    foreach (var chunk in chunks)
                    {
                        wp.AppendLog($"Trying to confirm {chunkIndex} confirmations chunk");
                        var status = guard.AcceptMultipleConfirmations(chunk);
                        confirmationsSuccess.Add(status);

                        wp.AppendLog(
                            status
                                ? $"{chunkIndex} confirmations chunk is successful"
                                : $"{chunkIndex} confirmations chunk was failed");
                        chunkIndex++;

                        Thread.Sleep(TimeSpan.FromSeconds(2));
                    }

                    if (confirmationsSuccess.All(c => c))
                    {
                        wp.AppendLog($"{confirmations.Length} confirmations was successfully accepted");
                        wp.AppendLog("Fetching 2FA confirmations to verify they are empty");

                        confirmations = guard.FetchConfirmations().Where(
                            item => item.ConfType == Confirmation.ConfirmationType.MarketSellTransaction).ToArray();

                        if (confirmations.Any())
                        {
                            wp.AppendLog($"{confirmations.Length} confirmations found");
                        }
                        else
                        {
                            wp.AppendLog("All confirmation was successfully confirmed!");
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    wp.AppendLog($"Error on 2FA confirm - {e.Message}");
                }

                wp.AppendLog("Waiting for 5 seconds to restart confirmation process");
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }

        public static void ProcessErrorOnMarketSell(
            MarketSellProcessModel marketSellModel,
            FullRgItem item,
            UiSteamManager uiSteamManager,
            string exMessage,
            WorkingProcessDataContext wp)
        {
            if (exMessage.Contains("You have too many listings pending confirmation"))
            {
                ProcessTooManyListingsPendingConfirmation(uiSteamManager, wp);
            }
            else if (exMessage.Contains("We were unable to contact the game's item server."))
            {
                RetryMarketSell(10, 2, marketSellModel, item, uiSteamManager, wp);
            }
            else if (exMessage.Contains("The item specified is no longer in your inventory or is not allowed to be traded on the Community Market."))
            {
                RetryMarketSell(1, 0, marketSellModel, item, uiSteamManager, wp);
            }
            else if (exMessage.Contains("There was a problem listing your item. Refresh the page and try again."))
            {
                RetryMarketSell(3, 0, marketSellModel, item, uiSteamManager, wp);
            }
        }

        public static void RetryMarketSell(int retryCount, int delaySeconds, MarketSellProcessModel marketSellModel, FullRgItem item, UiSteamManager uiSteamManager, WorkingProcessDataContext wp)
        {
            wp.AppendLog($"Retrying to sell {marketSellModel.ItemName} 3 more times.");

            for (var index = 1; index <= retryCount; index++)
            {
                if (marketSellModel.SellPrice == null)
                {
                    wp.AppendLog(
                        $"Selling price for {marketSellModel.ItemName} not found. Aborting sell retrying process");
                    break;
                }

                if (wp.CancellationToken.IsCancellationRequested)
                {
                    wp.AppendLog("Retrying to sell was force stopped");
                    break;
                }

                try
                {
                    wp.AppendLog($"Selling - '{marketSellModel.ItemName}' for {marketSellModel.SellPrice}");
                    uiSteamManager.SellOnMarket(item, marketSellModel.SellPrice.Value);
                    break;
                }
                catch (Exception e)
                {
                    wp.AppendLog($"Retry {index} failed with error - {e.Message}");
                    Thread.Sleep(TimeSpan.FromSeconds(delaySeconds));
                }
            }
        }

        public static void ProcessMarketSellInventoryPage(
            ObservableCollection<MarketSellModel> marketSellItems,
            InventoryRootModel inventoryPage,
            Inventory inventory)
        {
            var items = inventory.ProcessInventoryPage(inventoryPage).ToArray();

            items = inventory.FilterInventory(items, true, false);

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

        public static void ProcessTooManyListingsPendingConfirmation(
            UiSteamManager steamManager,
            WorkingProcessDataContext wp)
        {
            wp.AppendLog("Seems market listings stacked. Forsering two factor confirmation");
            ConfirmMarketTransactionsWorkingProcess(steamManager.Guard, wp);

            var myListings = steamManager.MarketClient.MyListings(steamManager.Currency.ToString())?.ConfirmationSales
                ?.ToArray();
            if (myListings == null || myListings.Length <= 0) return;

            wp.AppendLog(
                $"Seems there are {myListings.Length} market listings that do not have two factor confirmation request. Trying to cancel them");

            var semaphore = new Semaphore(
                SettingsProvider.GetInstance().RelistThreadsCount,
                SettingsProvider.GetInstance().RelistThreadsCount);

            foreach (var listing in myListings)
            {
                if (wp.CancellationToken.IsCancellationRequested)
                {
                    return;
                }

                try
                {
                    semaphore.WaitOne();
                    wp.AppendLog($"Removing {listing.Name}");
                    var listingId = listing.SaleId;

                    Task.Run(
                        () =>
                            {
                                try
                                {
                                    var result = steamManager.MarketClient.CancelSellOrder(listingId);
                                    if (result != ECancelSellOrderStatus.Canceled)
                                    {
                                        wp.AppendLog($"ERROR on removing {listing.Name}");
                                    }
                                }
                                catch (Exception e)
                                {
                                    wp.AppendLog($"ERROR on removing {listing.Name} - {e.Message}");
                                    Logger.Log.Error($"ERROR on removing {listing.Name} - {e.Message}", e);
                                }
                            });
                }
                catch (Exception e)
                {
                    wp.AppendLog($"ERROR on removing {listing.Name} - {e.Message}");
                    Logger.Log.Error($"ERROR on removing {listing.Name} - {e.Message}", e);
                }

                semaphore.Release();
            }
        }
    }
}