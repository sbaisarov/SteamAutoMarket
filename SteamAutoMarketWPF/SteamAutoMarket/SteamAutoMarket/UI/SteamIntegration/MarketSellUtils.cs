namespace SteamAutoMarket.UI.SteamIntegration
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.Steam.Auth;
    using SteamAutoMarket.Steam.Market.Enums;
    using SteamAutoMarket.Steam.Market.Interface;
    using SteamAutoMarket.Steam.TradeOffer;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Pages;
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

                wp.AppendLog("Waiting for 10 seconds to restart confirmation process");
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }

        public static void ProcessErrorOnMarketSell(
            MarketSellProcessModel sellProcessModel,
            FullRgItem item,
            UiSteamManager uiSteamManager,
            string exMessage)
        {
            // todo
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
            SteamGuardAccount guard,
            MarketClient marketClient,
            int currency,
            WorkingProcessDataContext wp,
            CancellationToken cancellationToken)
        {
            wp.AppendLog("Seems market listings stacked. Forsering two factor confirmation");
            ConfirmMarketTransactionsWorkingProcess(guard, wp);

            var myListings = marketClient.MyListings(currency.ToString())?.ConfirmationSales?.ToArray();
            if (myListings == null || myListings.Length <= 0) return;

            wp.AppendLog(
                $"Seems there are {myListings.Length} market listings that do not have two factor confirmation request. Trying to cancel them");
            foreach (var listing in myListings)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                try
                {
                    wp.AppendLog($"Removing {listing.Name}");
                    var result = marketClient.CancelSellOrder(listing.SaleId);
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
            }
        }
    }
}