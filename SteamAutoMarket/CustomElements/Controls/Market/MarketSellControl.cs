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
            try
            {
                this.InitializeComponent();
                this.allItemsListGridUtils = new AllItemsListGridUtils(this.AllSteamItemsGridView);
                PriceLoader.Init(this.AllSteamItemsGridView, ETableToLoad.AllItemsTable);
                PriceLoader.Init(this.ItemsToSaleGridView, ETableToLoad.ItemsToSaleTable);

                this.InventoryAppIdComboBox.Text = SavedSettings.Get().MarketInventoryAppId;
                this.InventoryContextIdComboBox.Text = SavedSettings.Get().MarketInventoryContexId;
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        public static Dictionary<string, RgDescription> AllDescriptionsDictionary { get; set; }

        public static RgDescription LastSelectedItemDescription { get; set; }

        public void AuthCurrentAccount()
        {
            try
            {
                this.AccountNameLable.Text = CurrentSession.SteamManager.Guard.AccountName;
                this.SplitterPanel.BackgroundImage = CurrentSession.AccountImage;
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
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

        public void AllSteamItemsGridViewCurrentCellChanged(object sender, EventArgs e)
        {
            try
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

                int rowIndex;
                if (this.AllSteamItemsGridView.SelectedRows.Count > 1)
                {
                    rowIndex = this.AllSteamItemsGridView
                        .SelectedRows[this.AllSteamItemsGridView.SelectedRows.Count - 1].Cells[0].RowIndex;
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
            catch (Exception ex)
            {
                Logger.Critical($"Error on 'Market sell' - 'All items grid' current cell changed", ex);
            }
        }

        public void ItemsToSaleGridViewCurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                var cell = this.ItemsToSaleGridView.CurrentCell;
                if (cell == null)
                {
                    return;
                }

                var row = cell.RowIndex;
                if (row < 0)
                {
                    return;
                }

                int rowIndex;
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
            catch (Exception ex)
            {
                Logger.Critical($"Error on 'Market sell' - 'Items to sale grid' current cell changed", ex);
            }
        }

        private void SteamSaleDataGridViewEditingControlShowing(
            object sender,
            DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control is ComboBox cb)
                {
                    cb.IntegralHeight = false;
                    cb.MaxDropDownItems = 10;
                }
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void DeleteSelectedItemButtonClick(object sender, EventArgs e)
        {
            try
            {
                var cell = this.ItemsToSaleGridView.CurrentCell;

                if (cell == null || cell.RowIndex < 0)
                {
                    return;
                }

                ItemsToSaleGridUtils.DeleteButtonClick(
                    this.AllSteamItemsGridView,
                    this.ItemsToSaleGridView,
                    cell.RowIndex);
            }
            catch (Exception ex)
            {
                Logger.Critical("Error on 'Market sell' - 'Delete items to sale selected item' button click", ex);
            }
        }

        private void HalfAutoPriceRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentMinusPriceRadioButton.Checked)
                {
                    this.CurrentPriceNumericUpDown.Visible = true;
                    this.CurrentPricePercentNumericUpDown.Visible = true;
                }
                else
                {
                    this.CurrentPriceNumericUpDown.Visible = false;
                    this.CurrentPricePercentNumericUpDown.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void ItemsToSaleGridViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                if (this.ItemsToSaleGridView.Rows.Count == 1)
                {
                    this.ItemsToSaleGridViewCurrentCellChanged(sender, e);
                }

                if (e.ColumnIndex != 2)
                {
                    return;
                }

                if (!this.ManualPriceRadioButton.Checked)
                {
                    this.ItemToSalePriceColumn.ReadOnly = true;
                }
                else
                {
                    this.ItemToSalePriceColumn.ReadOnly = false;
                    ItemsToSaleGridUtils.CellClick(this.ItemsToSaleGridView, e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void ItemsToSaleGridViewCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.ItemsToSaleGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void InventoryAppIdComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            try
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

                SavedSettings.UpdateField(
                    ref SavedSettings.Get().MarketInventoryAppId,
                    this.InventoryAppIdComboBox.Text);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void LoadInventoryButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (CurrentSession.SteamManager == null)
                {
                    MessageBox.Show(
                        @"You should login first",
                        @"Error inventory loading",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Logger.Error("Error on inventory loading. No signed in account found.");
                    return;
                }

                if (string.IsNullOrEmpty(this.InventoryAppIdComboBox.Text)
                    || string.IsNullOrEmpty(this.InventoryContextIdComboBox.Text))
                {
                    MessageBox.Show(
                        @"You should chose inventory type first",
                        @"Error inventory loading",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Logger.Error("Error on inventory loading. No inventory type chosed.");
                    return;
                }

                this.LoadInventoryButton.Enabled = false;

                string appid;
                switch (this.InventoryAppIdComboBox.Text)
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
                        appid = this.InventoryAppIdComboBox.Text;
                        break;
                }

                var contextId = this.InventoryContextIdComboBox.Text;
                CurrentSession.CurrentInventoryAppId = appid;
                CurrentSession.CurrentInventoryContextId = contextId;
                PriceLoader.StopAll();

                Logger.Debug($"Inventory {appid} - {contextId} loading started");

                Task.Run(
                    () =>
                        {
                            this.LoadInventory(
                                CurrentSession.SteamManager.Guard.Session.SteamID.ToString(),
                                appid,
                                contextId);
                            Logger.Info($"Inventory {appid} - {contextId} loading finished");
                            Dispatcher.AsMainForm(() => { this.LoadInventoryButton.Enabled = true; });
                            PriceLoader.StartPriceLoading(ETableToLoad.AllItemsTable);
                        });
            }
            catch (Exception ex)
            {
                Logger.Critical("Error on 'Market sell' - 'Load inventory' button click", ex);
            }
        }

        private void StartSteamSellButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (CurrentSession.SteamManager == null)
                {
                    MessageBox.Show(
                        @"You should log in first",
                        @"Error sending trade offer",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Logger.Error("Error on inventory loading. No logged in accounts found.");
                    return;
                }

                double changeValue = 0, changePercentValue = 0;

                MarketSaleType marketSaleType;
                if (this.ManualPriceRadioButton.Checked)
                {
                    marketSaleType = MarketSaleType.Manual;
                }
                else if (this.RecomendedPriceRadioButton.Checked)
                {
                    marketSaleType = MarketSaleType.Recommended;
                }
                else if (this.AveregeMinusPriceRadioButton.Checked)
                {
                    marketSaleType = MarketSaleType.LowerThanAverage;
                    changeValue = (double)this.AveragePriceNumericUpDown.Value;
                    changePercentValue = (double)this.AveragePricePercentNumericUpDown.Value;
                }
                else if (this.CurrentMinusPriceRadioButton.Checked)
                {
                    marketSaleType = MarketSaleType.LowerThanCurrent;
                    changeValue = (double)this.CurrentPriceNumericUpDown.Value;
                    changePercentValue = (double)this.CurrentPricePercentNumericUpDown.Value;
                }
                else
                {
                    throw new InvalidOperationException("Not implemented market sale type");
                }

                PriceLoader.StopAll();
                var itemsToSale = new PriceShaper(
                    this.ItemsToSaleGridView,
                    marketSaleType,
                    changeValue,
                    changePercentValue).GetItemsForSales();

                WorkingProcessForm.InitProcess(() => CurrentSession.SteamManager.SellOnMarket(itemsToSale));
            }
            catch (Exception ex)
            {
                Logger.Critical("Error on 'Market sell' - 'Start items sell' button click", ex);
            }
        }

        private void AddAllButtonClick(object sender, EventArgs e)
        {
            try
            {
                this.AllSteamItemsGridView.CurrentCellChanged -= this.AllSteamItemsGridViewCurrentCellChanged;

                this.ItemsToSaleGridView.CurrentCellChanged -= this.ItemsToSaleGridViewCurrentCellChanged;

                this.allItemsListGridUtils.AddCellListToSale(
                    this.ItemsToSaleGridView,
                    this.AllSteamItemsGridView.SelectedRows.Cast<DataGridViewRow>().ToArray());

                this.AllSteamItemsGridView.CurrentCellChanged += this.AllSteamItemsGridViewCurrentCellChanged;
                this.ItemsToSaleGridView.CurrentCellChanged += this.ItemsToSaleGridViewCurrentCellChanged;

                Logger.Debug("All selected items was added to sale list");
                PriceLoader.StartPriceLoading(ETableToLoad.ItemsToSaleTable);
            }
            catch (Exception ex)
            {
                Logger.Critical("Error on 'Market Sell' - 'Add all' button click", ex);
            }
        }

        private void OpenMarketPageButtonClickClick(object sender, EventArgs e)
        {
            try
            {
                if (LastSelectedItemDescription == null)
                {
                    return;
                }

                Process.Start(
                    "https://" + $"steamcommunity.com/market/listings/{LastSelectedItemDescription.Appid}/"
                               + LastSelectedItemDescription.MarketHashName);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private async void AllItemsUpdateOneButtonClickClick(object sender, EventArgs e)
        {
            try
            {
                await Task.Run(
                    async () =>
                        {
                            var selectedRows = this.AllSteamItemsGridView.SelectedRows.Cast<DataGridViewRow>();
                            foreach (var row in selectedRows)
                            {
                                var item = this.allItemsListGridUtils.GetRowItemsList(row.Index).FirstOrDefault();
                                if (item == null)
                                {
                                    continue;
                                }

                                var averagePrice = CurrentSession.SteamManager.GetAveragePrice(
                                    item.Asset.Appid,
                                    item.Description.MarketHashName,
                                    SavedSettings.Get().SettingsAveragePriceParseDays);

                                if (averagePrice.HasValue)
                                {
                                    PriceLoader.AveragePricesCache.Cache(
                                        item.Description.MarketHashName,
                                        averagePrice.Value);
                                }

                                var currentPrice =
                                    await CurrentSession.SteamManager.GetCurrentPrice(item.Asset.Appid, item.Description.MarketHashName);
                                PriceLoader.CurrentPricesCache.Cache(
                                    item.Description.MarketHashName,
                                    currentPrice.Value);

                                row.Cells[this.allItemsListGridUtils.CurrentColumnIndex].Value = currentPrice;
                                row.Cells[this.allItemsListGridUtils.AverageColumnIndex].Value = averagePrice;
                            }
                        });
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void InventoryContextIdComboBoxTextChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(
                    ref SavedSettings.Get().MarketInventoryContexId,
                    this.InventoryContextIdComboBox.Text);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void CurrentPricePercentNumericUpDownValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentPriceNumericUpDown.Value == 0)
                {
                    return;
                }

                var tmp = this.CurrentPricePercentNumericUpDown.Value;
                this.CurrentPriceNumericUpDown.Value = 0;
                this.CurrentPricePercentNumericUpDown.Value = tmp;
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void CurrentPriceNumericUpDownValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentPricePercentNumericUpDown.Value == 0)
                {
                    return;
                }

                var tmp = this.CurrentPriceNumericUpDown.Value;
                this.CurrentPricePercentNumericUpDown.Value = 0;
                this.CurrentPriceNumericUpDown.Value = tmp;
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void AveragePriceNumericUpDownValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.AveragePricePercentNumericUpDown.Value == 0)
                {
                    return;
                }

                var tmp = this.AveragePriceNumericUpDown.Value;
                this.AveragePricePercentNumericUpDown.Value = 0;
                this.AveragePriceNumericUpDown.Value = tmp;
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void AveragePricePercentNumericUpDownValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.AveragePriceNumericUpDown.Value == 0)
                {
                    return;
                }

                var tmp = this.AveragePricePercentNumericUpDown.Value;
                this.AveragePriceNumericUpDown.Value = 0;
                this.AveragePricePercentNumericUpDown.Value = tmp;
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void AverageMinusPriceRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.AveregeMinusPriceRadioButton.Checked)
                {
                    this.AveragePriceNumericUpDown.Visible = true;
                    this.AveragePricePercentNumericUpDown.Visible = true;
                }
                else
                {
                    this.AveragePriceNumericUpDown.Visible = false;
                    this.AveragePricePercentNumericUpDown.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void RefreshPricesButtonClick(object sender, EventArgs e)
        {
            try
            {
                PriceLoader.StartPriceLoading(ETableToLoad.ItemsToSaleTable, true);
                PriceLoader.StartPriceLoading(ETableToLoad.AllItemsTable, true);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void ForcePricesReloadButtonClick(object sender, EventArgs e)
        {
            try
            {
                PriceLoader.StartPriceLoading(ETableToLoad.ItemsToSaleTable, true);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private async void ItemsForSaleUpdateOneButtonClickClick(object sender, EventArgs e)
        {
            try
            {
                await Task.Run(
                    async () =>
                        {
                            try
                            {
                                var selectedRows = this.ItemsToSaleGridView.SelectedRows.Cast<DataGridViewRow>();
                                foreach (var row in selectedRows)
                                {
                                    var item = ItemsToSaleGridUtils.GetFullRgItems(this.ItemsToSaleGridView, row.Index)
                                        .FirstOrDefault();
                                    if (item == null)
                                    {
                                        continue;
                                    }

                                    var averagePrice = CurrentSession.SteamManager.GetAveragePrice(
                                        item.Asset.Appid,
                                        item.Description.MarketHashName,
                                        SavedSettings.Get().SettingsAveragePriceParseDays);

                                    if (averagePrice.HasValue)
                                    {
                                        PriceLoader.AveragePricesCache.Cache(
                                            item.Description.MarketHashName,
                                            averagePrice.Value);
                                    }

                                    var currentPrice =
                                        await CurrentSession.SteamManager.GetCurrentPrice(item.Asset.Appid, item.Description.MarketHashName);
                                    PriceLoader.CurrentPricesCache.Cache(
                                        item.Description.MarketHashName,
                                        currentPrice.Value);

                                    row.Cells[ItemsToSaleGridUtils.CurrentColumnIndex].Value = currentPrice;
                                    row.Cells[ItemsToSaleGridUtils.AverageColumnIndex].Value = averagePrice;
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.Error("Error on update item price", ex);
                            }
                        });
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void SaleControlLoad(object sender, EventArgs e)
        {
            try
            {
                AllDescriptionsDictionary = new Dictionary<string, RgDescription>();
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void SteamSaleDataGridViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
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
                        PriceLoader.StartPriceLoading(ETableToLoad.ItemsToSaleTable);
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Critical(@"Error on 'Market sell' - 'All items grid' cell click", ex);
            }
        }
    }
}