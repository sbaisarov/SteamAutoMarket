namespace SteamAutoMarket.SteamUtils
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;

    using Steam;
    using Steam.SteamAuth;
    using Steam.TradeOffer.Models;

    using SteamAutoMarket.Models;
    using SteamAutoMarket.Pages;
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
        }

        public void SaveAccount()
        {
            if (this.IsSessionUpdated == false) return;
            this.IsSessionUpdated = false;

            SettingsProvider.GetInstance().OnPropertyChanged("SteamAccounts");
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

                            form.AppendLog($"{marketSellItems.Sum(i => i.Count)} marketable items was loaded");
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

        private void ProcessInventoryPage(
            ICollection<MarketSellModel> marketSellItems,
            InventoryRootModel inventoryPage)
        {
            var items = this.Inventory.ProcessInventoryPage(inventoryPage);

            var groupedItems = items.Where(i => i.Description.IsMarketable).GroupBy(i => i.Description.MarketHashName).ToList();

            foreach (var group in groupedItems) marketSellItems.AddDispatch(new MarketSellModel(@group.ToList()));
        }
    }
}