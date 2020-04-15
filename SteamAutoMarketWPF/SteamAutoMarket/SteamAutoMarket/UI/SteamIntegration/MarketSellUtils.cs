namespace SteamAutoMarket.UI.SteamIntegration
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.Steam.Auth;
    using SteamAutoMarket.Steam.Market;
    using SteamAutoMarket.Steam.Market.Enums;
    using SteamAutoMarket.Steam.Market.Models;
    using SteamAutoMarket.Steam.TradeOffer;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Pages;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.Repository.Settings;
    using SteamAutoMarket.UI.Utils.Extension;

    [Obfuscation(Exclude = true)]
    public static class MarketSellUtils
    {
        public static void CancelMarketPendingListings(UiSteamManager steamManager, WorkingProcessDataContext wp)
        {
            MyListingsSalesItem[] myListings;

            try
            {
                myListings = steamManager.MarketClient.MyListings(steamManager.Currency.ToString())?.ConfirmationSales
                    ?.ToArray();
            }
            catch (Exception e)
            {
                wp.AppendLog($"Error on fetching market listings - {e.Message}");
                return;
            }

            if (myListings == null || myListings.Length <= 0)
            {
                wp.AppendLog("No market listing found. Nothing to cancel");
                return;
            }

            wp.AppendLog(
                $"Seems there are {myListings.Length} market listings that do not have two factor confirmation request. Trying to cancel them");

            var semaphore = new Semaphore(
                SettingsProvider.GetInstance().RelistThreadsCount,
                SettingsProvider.GetInstance().RelistThreadsCount);

            var index = 1;
            foreach (var listing in myListings)
            {
                if (wp.CancellationToken.IsCancellationRequested)
                {
                    return;
                }

                if (listing == null) continue;

                try
                {
                    semaphore.WaitOne();
                    var realListing = listing;
                    var realIndex = index++;
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
                                                $"Cancel market {realListing.Name} pending listings process was force stopped");
                                            break;
                                        }

                                        wp.AppendLog($"[{realIndex}/{myListings.Length}] Removing {realListing.Name}");
                                        var result = steamManager.MarketClient.CancelSellOrder(realListing.SaleId);
                                        if (result != ECancelSellOrderStatus.Canceled)
                                        {
                                            wp.AppendLog($"ERROR on removing {realListing.Name}");
                                            if (tryCount++ > 10) break;
                                            Thread.Sleep(1000);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    wp.AppendLog($"ERROR on removing {realListing.Name} - {e.Message}");
                                    Logger.Log.Error($"ERROR on removing {realListing.Name} - {e.Message}", e);
                                }
                                finally
                                {
                                    semaphore.Release();
                                }
                            });
                }
                catch (Exception e)
                {
                    wp.AppendLog($"ERROR on removing {listing.Name} - {e.Message}");
                    Logger.Log.Error($"ERROR on removing {listing.Name} - {e.Message}", e);
                }
            }
        }

        public static void ConfirmMarketTransactionsWorkingProcess(
            SteamGuardAccount guard,
            WorkingProcessDataContext wp)
        {
            while (true)
            {
                try
                {
                    if (wp.CancellationToken.IsCancellationRequested)
                    {
                        wp.AppendLog("2FA confirmation process was force stopped.");
                        return;
                    }

                    wp.AppendLog("Fetching 2FA confirmations..");

                    var confirmations = guard.FetchConfirmations().Where(
                        item => item.ConfType == Confirmation.ConfirmationType.MarketSellTransaction).ToArray();

                    wp.AppendLog($"{confirmations.Length} confirmations found. Accepting confirmations");

                    if (confirmations.Length == 0)
                    {
                        wp.AppendLog("No confirmations found. Nothing to confirm");
                        return;
                    }

                    var confirmationAcceptSuccess = guard.AcceptMultipleConfirmations(confirmations);

                    wp.AppendLog(
                        confirmationAcceptSuccess
                            ? "Confirmations confirm was successful"
                            : "Confirmations confirm was failed");

                    if (confirmationAcceptSuccess)
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
            MarketSellProcessModel marketSellModel,
            FullRgItem item,
            UiSteamManager uiSteamManager,
            string exMessage,
            WorkingProcessDataContext wp)
        {
            if (exMessage.Contains("There was a problem listing your item. Refresh the page and try again.")
                || exMessage.Contains("A connection that was expected to be kept alive was closed by the server"))
            {
                RetryMarketSell(3, 0, marketSellModel, item, uiSteamManager, wp);
            }
            else if (exMessage.Contains("We were unable to contact the game's item server."))
            {
                RetryMarketSell(10, 2, marketSellModel, item, uiSteamManager, wp);
            }
            else if (exMessage.Contains("You have too many listings pending confirmation"))
            {
                ProcessTooManyListingsPendingConfirmation(uiSteamManager, wp);
            }
            else if (exMessage.Contains("The item specified is no longer in your inventory"))
            {
            }
            else if (exMessage.Contains("You already have a listing for this item pending confirmation"))
            {
            }
            else if (exMessage.Contains("You cannot sell any items until your previous action completes"))
            {
                RetryMarketSell(int.MaxValue, 5, marketSellModel, item, uiSteamManager, wp);
            }
            else
            {
                wp.AppendLog($"Unknown error on market sell - {exMessage}");
                Logger.Log.Error($"Unknown error on market sell - {exMessage}");
            }
        }

        public static void ProcessTooManyListingsPendingConfirmation(
            UiSteamManager steamManager,
            WorkingProcessDataContext wp)
        {
            wp.AppendLog("Seems market listings stacked. Forsering two factor confirmation");
            ConfirmMarketTransactionsWorkingProcess(steamManager.Guard, wp);
            CancelMarketPendingListings(steamManager, wp);
        }

        public static void RetryMarketSell(
            int retryCount,
            int delaySeconds,
            MarketSellProcessModel marketSellModel,
            FullRgItem item,
            UiSteamManager uiSteamManager,
            WorkingProcessDataContext wp)
        {
            var currencySymbol = SteamCurrencies.Currencies[uiSteamManager.Currency.ToString()];

            wp.AppendLog(
                retryCount < int.MaxValue
                    ? $"Retrying to sell {marketSellModel.ItemName} {retryCount} more times."
                    : $"Retrying to sell {marketSellModel.ItemName} until success.");

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
                    wp.AppendLog(
                        $"Selling - '{marketSellModel.ItemName}' for {marketSellModel.SellPrice} {currencySymbol}");
                    uiSteamManager.SellOnMarket(item, marketSellModel.SellPrice.Value);
                    break;
                }
                catch (Exception e)
                {
                    wp.AppendLog($"Retry {index} failed with error - {e.Message}");
                    if (e.Message.Contains("You already have a listing for this item pending confirmation")
                        || e.Message.Contains("The item specified is no longer in your inventory")) return;
                    Thread.Sleep(TimeSpan.FromSeconds(delaySeconds));
                }
            }
        }
    }
}