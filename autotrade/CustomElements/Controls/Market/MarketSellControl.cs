namespace SteamAutoMarket.CustomElements.Controls.Market
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using SteamAutoMarket.CustomElements.Forms;
    using SteamAutoMarket.CustomElements.Utils;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.Utils;
    using SteamAutoMarket.WorkingProcess;
    using SteamAutoMarket.WorkingProcess.MarketPriceFormation;
    using SteamAutoMarket.WorkingProcess.PriceLoader;
    using SteamAutoMarket.WorkingProcess.Settings;

    public partial class SaleControl : UserControl
    {
        private readonly AllItemsListGridUtils allItemsListGridUtils;

        public SaleControl()
        {
            this.InitializeComponent();
            this.allItemsListGridUtils = new AllItemsListGridUtils(this.AllSteamItemsGridView);
            PriceLoader.Init(this.AllSteamItemsGridView, this.ItemsToSaleGridView);

            this.InventoryAppIdComboBox.Text = SavedSettings.Get().MarketInventoryAppId;
            this.InventoryContextIdComboBox.Text = SavedSettings.Get().MarketInventoryContexId;
        }

        public static Dictionary<string, RgDescription> AllDescriptionsDictionary { get; set; }

        public static RgDescription LastSelectedItemDescription { get; set; }

        public void AuthCurrentAccount()
        {
            this.AccountNameLable.Text = CurrentSession.SteamManager.Guard.AccountName;
            this.SplitterPanel.BackgroundImage = CurrentSession.AccountImage;
        }

        public void LoadInventory(string steamid, string appid, string contextid)
        {
            Dispatcher.AsMainForm(
                () =>
                    {
                        this.AllSteamItemsGridView.Rows.Clear();
                        this.ItemsToSaleGridView.Rows.Clear();
                    });

            Program.LoadingForm.InitInventoryLoadingProcess();
            var allItemsList = Program.LoadingForm.GetLoadedItems();
            Program.LoadingForm.DeactivateForm();

            allItemsList.RemoveAll(item => item.Description.IsMarketable == false);

            Dispatcher.AsMainForm(() => { this.allItemsListGridUtils.FillSteamSaleDataGrid(allItemsList); });
        }

        private void SteamSaleDataGridViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            this.allItemsListGridUtils.UpdateItemDescription(
                this.AllSteamItemsGridView.CurrentCell.RowIndex,
                this.ItemDescriptionTextBox,
                this.ItemImageBox,
                this.ItemNameLable);

            switch (e.ColumnIndex)
            {
                case 5:
                    this.allItemsListGridUtils.GridComboBoxClick(e.RowIndex);
                    break;
                case 6:
                    this.allItemsListGridUtils.GridAddButtonClick(e.RowIndex, this.ItemsToSaleGridView);
                    PriceLoader.StartPriceLoading(TableToLoad.ItemsToSaleTable);
                    break;
            }
        }

        private void AllSteamItemsGridViewCurrentCellChanged(object sender, EventArgs e)
        {
            var cell = this.AllSteamItemsGridView.CurrentCell;
            if (cell == null)
            {
                return;
            }

            var row = cell.RowIndex;
            if (row < 0)
            {
                return;
            }

            var rowIndex = 0;
            if (this.AllSteamItemsGridView.SelectedRows.Count > 1)
            {
                rowIndex = this.AllSteamItemsGridView.SelectedRows[this.AllSteamItemsGridView.SelectedRows.Count - 1]
                    .Cells[0].RowIndex;
            }
            else
            {
                rowIndex = this.AllSteamItemsGridView.CurrentCell.RowIndex;
            }

            this.allItemsListGridUtils.UpdateItemDescription(
                rowIndex,
                this.ItemDescriptionTextBox,
                this.ItemImageBox,
                this.ItemNameLable);

            var list = this.allItemsListGridUtils.GetRowItemsList(rowIndex);
            if (list != null && list.Count > 0)
            {
                LastSelectedItemDescription = list[0].Description;
            }
        }

        public void ItemsToSaleGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            var cell = ItemsToSaleGridView.CurrentCell;
            if (cell == null)
            {
                return;
            }

            var row = cell.RowIndex;
            if (row < 0)
            {
                return;
            }

            var rowIndex = 0;
            if (this.ItemsToSaleGridView.SelectedRows.Count > 1)
            {
                rowIndex = this.ItemsToSaleGridView.SelectedRows[this.ItemsToSaleGridView.SelectedRows.Count - 1]
                    .Cells[0].RowIndex;
            }
            else
            {
                rowIndex = row;
            }

            ItemsToSaleGridUtils.RowClick(
                this.ItemsToSaleGridView,
                row,
                AllDescriptionsDictionary,
                this.AllSteamItemsGridView,
                this.ItemDescriptionTextBox,
                this.ItemImageBox,
                this.ItemNameLable);
            var list = ItemsToSaleGridUtils.GetFullRgItems(this.ItemsToSaleGridView, rowIndex);
            if (list != null && list.Count > 0)
            {
                LastSelectedItemDescription = list[0].Description;
            }
        }

        private void SteamSaleDataGridViewEditingControlShowing(
            object sender,
            DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboBox cb)
            {
                cb.IntegralHeight = false;
                cb.MaxDropDownItems = 10;
            }
        }

        private void DeleteAccountButtonClick(object sender, EventArgs e)
        {
            var cell = this.ItemsToSaleGridView.CurrentCell;
            if (cell == null)
            {
                Logger.Warning("No accounts selected");
                return;
            }

            if (cell.RowIndex < 0) return;

            ItemsToSaleGridUtils.DeleteButtonClick(AllSteamItemsGridView, ItemsToSaleGridView, cell.RowIndex);
        }

        private void DeleteUnmarketableSteamButton_Click(object sender, EventArgs e)
        {
            ItemsToSaleGridUtils.DeleteUnmarketable(AllSteamItemsGridView, ItemsToSaleGridView);
        }

        private void DeleteUntradableSteamButton_Click(object sender, EventArgs e)
        {
            ItemsToSaleGridUtils.DeleteUntradable(AllSteamItemsGridView, ItemsToSaleGridView);
        }

        private void HalfAutoPriceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentMinusPriceRadioButton.Checked)
            {
                CurrentPriceNumericUpDown.Visible = true;
                CurrentPricePercentNumericUpDown.Visible = true;
            }
            else
            {
                CurrentPriceNumericUpDown.Visible = false;
                CurrentPricePercentNumericUpDown.Visible = false;
            }
        }

        private void ItemsToSaleGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (ItemsToSaleGridView.Rows.Count == 1) ItemsToSaleGridView_CurrentCellChanged(sender, e);
            if (e.ColumnIndex != 2) return;
            if (!ManualPriceRadioButton.Checked)
            {
                ItemToSalePriceColumn.ReadOnly = true;
            }
            else
            {
                ItemToSalePriceColumn.ReadOnly = false;
                ItemsToSaleGridUtils.CellClick(ItemsToSaleGridView, e.RowIndex);
            }
        }

        private void ItemsToSaleGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ItemsToSaleGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void InventoryAppIdComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.InventoryAppIdComboBox.Text)
            {
                case "STEAM":
                    {
                        this.InventoryContextIdComboBox.Text = @"6";
                        break;
                    }

                case "TF":
                    {
                        this.InventoryContextIdComboBox.Text = @"2";
                        break;
                    }

                case "CS:GO":
                    {
                        this.InventoryContextIdComboBox.Text = @"2";
                        break;
                    }

                case "PUBG":
                    {
                        this.InventoryContextIdComboBox.Text = @"2";
                        break;
                    }

                case "DOTA":
                    {
                        this.InventoryContextIdComboBox.Text = @"2";
                        break;
                    }
            }

            SavedSettings.UpdateField(ref SavedSettings.Get().MarketInventoryAppId, InventoryAppIdComboBox.Text);
        }

        private void LoadInventoryButton_Click(object sender, EventArgs e)
        {
            if (CurrentSession.SteamManager == null)
            {
                MessageBox.Show(
                    "You should login first",
                    "Error inventory loading",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Logger.Error("Error on inventory loading. No logined account found.");
                return;
            }

            if (string.IsNullOrEmpty(InventoryAppIdComboBox.Text)
                || string.IsNullOrEmpty(InventoryContextIdComboBox.Text))
            {
                MessageBox.Show(
                    "You should chose inventory type first",
                    "Error inventory loading",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Logger.Error("Error on inventory loading. No inventory type chosed.");
                return;
            }

            LoadInventoryButton.Enabled = false;

            string appid;
            switch (InventoryAppIdComboBox.Text)
            {
                case "STEAM":
                    appid = "753";
                    break;
                case "TF":
                    appid = "440";
                    break;
                case "CS:GO":
                    appid = "730";
                    break;
                case "PUBG":
                    appid = "578080";
                    break;                
                case "DOTA":
                    appid = "570";
                    break;
                default:
                    appid = InventoryAppIdComboBox.Text;
                    break;
            }

            var contextId = InventoryContextIdComboBox.Text;
            CurrentSession.CurrentInventoryAppId = appid;
            CurrentSession.CurrentInventoryContextId = contextId;
            PriceLoader.StopAll();

            Logger.Debug($"Inventory {appid} - {contextId} loading started");

            Task.Run(
                () =>
                    {
                        LoadInventory(CurrentSession.SteamManager.Guard.Session.SteamID.ToString(), appid, contextId);
                        Logger.Info($"Inventory {appid} - {contextId} loading finished");
                        Dispatcher.AsMainForm(() => { LoadInventoryButton.Enabled = true; });
                        PriceLoader.StartPriceLoading(TableToLoad.AllItemsTable);
                    });
        }

        private void StartSteamSellButton_Click(object sender, EventArgs e)
        {
            if (CurrentSession.SteamManager == null)
            {
                MessageBox.Show(
                    "You should log in first",
                    "Error sending trade offer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Logger.Error("Error on inventory loading. No logged in accounts found.");
                return;
            }

            double changeValue = 0, changePercentValue = 0;

            MarketSaleType marketSaleType;
            if (ManualPriceRadioButton.Checked)
            {
                marketSaleType = MarketSaleType.Manual;
            }
            else if (RecomendedPriceRadioButton.Checked)
            {
                marketSaleType = MarketSaleType.Recommended;
            }
            else if (AveregeMinusPriceRadioButton.Checked)
            {
                marketSaleType = MarketSaleType.LowerThanAverage;
                changeValue = (double)AveragePriceNumericUpDown.Value;
                changePercentValue = (double)AveragePricePercentNumericUpDown.Value;
            }
            else if (CurrentMinusPriceRadioButton.Checked)
            {
                marketSaleType = MarketSaleType.LowerThanCurrent;
                changeValue = (double)CurrentPriceNumericUpDown.Value;
                changePercentValue = (double)CurrentPricePercentNumericUpDown.Value;
            }
            else
            {
                throw new InvalidOperationException("Not implemented market sale type");
            }

            PriceLoader.StopAll();
            var itemsToSale = new PriceShaper(ItemsToSaleGridView, marketSaleType, changeValue, changePercentValue)
                .GetItemsForSales();

            WorkingProcessForm.InitProcess(() => CurrentSession.SteamManager.SellOnMarket(itemsToSale));
        }

        public void DeleteSoldItem(string marketHashName)
        {
            var row = ItemsToSaleGridUtils.GetDataGridViewRowByMarketHashName(ItemsToSaleGridView, marketHashName);
            if (row != null) ItemsToSaleGridView.Rows.Remove(row);
        }

        private void AddAllButton_Click(object sender, EventArgs e)
        {
            AllSteamItemsGridView.CurrentCellChanged -= this.AllSteamItemsGridViewCurrentCellChanged;
            ItemsToSaleGridView.CurrentCellChanged -= ItemsToSaleGridView_CurrentCellChanged;

            this.allItemsListGridUtils.AddCellListToSale(
                ItemsToSaleGridView,
                AllSteamItemsGridView.SelectedRows.Cast<DataGridViewRow>().ToArray());

            AllSteamItemsGridView.CurrentCellChanged += this.AllSteamItemsGridViewCurrentCellChanged;
            ItemsToSaleGridView.CurrentCellChanged += ItemsToSaleGridView_CurrentCellChanged;

            Logger.Debug("All selected items was added to sale list");
            PriceLoader.StartPriceLoading(TableToLoad.ItemsToSaleTable);
        }

        private void OpenMarketPageButtonClick_Click(object sender, EventArgs e)
        {
            if (LastSelectedItemDescription == null) return;
            Process.Start(
                "https://" + $"steamcommunity.com/market/listings/{LastSelectedItemDescription.Appid}/"
                           + LastSelectedItemDescription.MarketHashName);
        }

        private async void AllItemsUpdateOneButtonClick_Click(object sender, EventArgs e)
        {
            await Task.Run(
                async () =>
                    {
                        var selectedRows = AllSteamItemsGridView.SelectedRows.Cast<DataGridViewRow>();
                        foreach (var row in selectedRows)
                        {
                            var item = this.allItemsListGridUtils.GetRowItemsList(row.Index).FirstOrDefault();
                            if (item == null)
                            {
                                continue;
                            }

                            var averagePrice = CurrentSession.SteamManager.GetAveragePrice(
                                item.Asset,
                                item.Description,
                                SavedSettings.Get().SettingsAveragePriceParseDays);

                            PriceLoader.AveragePricesCache.Cache(item.Description.MarketHashName, averagePrice.Value);

                            var currentPrice =
                                await CurrentSession.SteamManager.GetCurrentPrice(item.Asset, item.Description);
                            PriceLoader.CurrentPricesCache.Cache(item.Description.MarketHashName, currentPrice.Value);

                            row.Cells[this.allItemsListGridUtils.CurrentColumnIndex].Value = currentPrice;
                            row.Cells[this.allItemsListGridUtils.AverageColumnIndex].Value = averagePrice;
                        }
                    });
        }

        private void InventoryContextIdComboBox_TextChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().MarketInventoryContexId, InventoryContextIdComboBox.Text);
        }

        private void CurrentPricePercentNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (CurrentPriceNumericUpDown.Value != 0)
            {
                var tmp = CurrentPricePercentNumericUpDown.Value;
                CurrentPriceNumericUpDown.Value = 0;
                CurrentPricePercentNumericUpDown.Value = tmp;
            }
        }

        private void CurrentPriceNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (CurrentPricePercentNumericUpDown.Value != 0)
            {
                var tmp = CurrentPriceNumericUpDown.Value;
                CurrentPricePercentNumericUpDown.Value = 0;
                CurrentPriceNumericUpDown.Value = tmp;
            }
        }

        private void AveragePriceNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (AveragePricePercentNumericUpDown.Value != 0)
            {
                var tmp = AveragePriceNumericUpDown.Value;
                AveragePricePercentNumericUpDown.Value = 0;
                AveragePriceNumericUpDown.Value = tmp;
            }
        }

        private void AveragePricePercentNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (AveragePriceNumericUpDown.Value != 0)
            {
                var tmp = AveragePricePercentNumericUpDown.Value;
                AveragePriceNumericUpDown.Value = 0;
                AveragePricePercentNumericUpDown.Value = tmp;
            }
        }

        private void AveregeMinusPriceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (AveregeMinusPriceRadioButton.Checked)
            {
                AveragePriceNumericUpDown.Visible = true;
                AveragePricePercentNumericUpDown.Visible = true;
            }
            else
            {
                AveragePriceNumericUpDown.Visible = false;
                AveragePricePercentNumericUpDown.Visible = false;
            }
        }

        private void RefreshPricesButton_Click(object sender, EventArgs e)
        {
            PriceLoader.StartPriceLoading(TableToLoad.ItemsToSaleTable, true);
            PriceLoader.StartPriceLoading(TableToLoad.AllItemsTable, true);
        }

        private void ForcePricesReloadButton_Click(object sender, EventArgs e)
        {
            PriceLoader.StartPriceLoading(TableToLoad.ItemsToSaleTable, true);
        }

        private async void ItemsForSaleUpdateOneButtonClick_Click(object sender, EventArgs e)
        {
            await Task.Run(
                async () =>
                    {
                        try
                        {
                            var selectedRows = ItemsToSaleGridView.SelectedRows.Cast<DataGridViewRow>();
                            foreach (var row in selectedRows)
                            {
                                var item = ItemsToSaleGridUtils.GetFullRgItems(ItemsToSaleGridView, row.Index)
                                    .FirstOrDefault();
                                if (item == null) continue;

                                var averagePrice = CurrentSession.SteamManager.GetAveragePrice(
                                    item.Asset,
                                    item.Description,
                                    SavedSettings.Get().SettingsAveragePriceParseDays);
                                PriceLoader.AveragePricesCache.Cache(
                                    item.Description.MarketHashName,
                                    averagePrice.Value);

                                var currentPrice =
                                    await CurrentSession.SteamManager.GetCurrentPrice(item.Asset, item.Description);
                                PriceLoader.CurrentPricesCache.Cache(
                                    item.Description.MarketHashName,
                                    currentPrice.Value);

                                row.Cells[ItemsToSaleGridUtils.CurrentColumnIndex].Value = currentPrice;
                                row.Cells[ItemsToSaleGridUtils.AverageColumnIndex].Value = averagePrice;
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("Erron on update item price", ex);
                        }
                    });
        }

        private void SaleControlLoad(object sender, EventArgs e)
        {
            AllDescriptionsDictionary = new Dictionary<string, RgDescription>();
        }
    }
}