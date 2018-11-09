namespace SteamAutoMarket.SteamUtils
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;

    using Core;

    using OxyPlot;

    using Steam;
    using Steam.SteamAuth;
    using Steam.TradeOffer.Models;
    using Steam.TradeOffer.Models.Full;

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

        public void LoadMyInventoryWorkingProcess(
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
                            // form.ChartModel.AddDispatch(new DataPoint(0, 0));
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
                                page = this.LoadInventoryPage(this.SteamId, appid.AppId, contextId, page.LastAssetid);

                                this.ProcessInventoryPage(marketSellItems, page);

                                form.AppendLog($"Page {currentPage++}/{totalPagesCount} loaded");
                                form.IncrementProgress();
                            }
                        }
                        catch (Exception e)
                        {
                            var message = $"Error on {appid} inventory loading - {e.Message}";

                            form.AppendLog(message);
                            ErrorNotify.CriticalMessageBox(message);
                            marketSellItems.ClearDispatch();
                        }
                    });
        }

        private void ProcessInventoryPage(
            ICollection<MarketSellModel> marketSellItems,
            InventoryRootModel inventoryPage)
        {
            var items = this.Inventory.ProcessInventoryPage(inventoryPage);

            var groupedItems = items.GroupBy(i => i.Description.MarketHashName);
            foreach (var group in groupedItems)
            {
                marketSellItems.AddDispatch(new MarketSellModel(group.ToList()));
            }
        }
    }
}