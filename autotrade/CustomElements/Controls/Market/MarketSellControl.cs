using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteamAutoMarket.CustomElements.Utils;
using SteamAutoMarket.Steam.TradeOffer.Models;
using SteamAutoMarket.Utils;
using SteamAutoMarket.WorkingProcess;
using SteamAutoMarket.WorkingProcess.MarketPriceFormation;
using SteamAutoMarket.WorkingProcess.PriceLoader;
using SteamAutoMarket.WorkingProcess.Settings;

namespace SteamAutoMarket.CustomElements.Controls.Market
{
    public partial class SaleControl : UserControl
    {
        public SaleControl()
        {
            InitializeComponent();
            PriceLoader.Init(AllSteamItemsGridView, ItemsToSaleGridView);

            InventoryAppIdComboBox.Text = SavedSettings.Get().MarketInventoryAppId;
            InventoryContextIdComboBox.Text = SavedSettings.Get().MarketInventoryContexId;
        }

        public static Dictionary<string, RgDescription> AllDescriptionsDictionary { get; set; }
        public static RgDescription LastSelectedItemDescription { get; set; }

        private void SaleControl_Load(object sender, EventArgs e)
        {
            AllDescriptionsDictionary = new Dictionary<string, RgDescription>();
        }

        public void AuthCurrentAccount()
        {
            AccountNameLable.Text = CurrentSession.SteamManager.Guard.AccountName;
            SplitterPanel.BackgroundImage = CurrentSession.AccountImage;
        }

        public void LoadInventory(string steamid, string appid, string contextid)
        {
            Dispatcher.AsMainForm(() =>
            {
                AllSteamItemsGridView.Rows.Clear();
                ItemsToSaleGridView.Rows.Clear();
            });

            Program.LoadingForm.InitInventoryLoadingProcess();
            var allItemsList = Program.LoadingForm.GetLoadedItems();
            Program.LoadingForm.DeactivateForm();

            allItemsList.RemoveAll(item => item.Description.IsMarketable == false);

            Dispatcher.AsMainForm(() =>
            {
                AllItemsListGridUtils.FillSteamSaleDataGrid(AllSteamItemsGridView, allItemsList);
            });
        }

        private void SteamSaleDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            AllItemsListGridUtils.UpdateItemDescription(AllSteamItemsGridView,
                AllSteamItemsGridView.CurrentCell.RowIndex, ItemDescriptionTextBox, ItemImageBox, ItemNameLable);

            if (e.ColumnIndex == 5)
            {
                AllItemsListGridUtils.GridComboBoxClick(AllSteamItemsGridView, e.RowIndex);
            }
            else if (e.ColumnIndex == 6)
            {
                AllItemsListGridUtils.GridAddButtonClick(AllSteamItemsGridView, e.RowIndex, ItemsToSaleGridView);
                PriceLoader.StartPriceLoading(TableToLoad.ItemsToSaleTable);
            }
        }

        private void AllSteamItemsGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            var cell = AllSteamItemsGridView.CurrentCell;
            if (cell == null) return;

            var row = cell.RowIndex;
            if (row < 0) return;

            var rowIndex = 0;
            if (AllSteamItemsGridView.SelectedRows.Count > 1)
                rowIndex = AllSteamItemsGridView.SelectedRows[AllSteamItemsGridView.SelectedRows.Count - 1].Cells[0]
                    .RowIndex;
            else
                rowIndex = AllSteamItemsGridView.CurrentCell.RowIndex;
            AllItemsListGridUtils.UpdateItemDescription(AllSteamItemsGridView, rowIndex, ItemDescriptionTextBox,
                ItemImageBox, ItemNameLable);
            var list = AllItemsListGridUtils.GetFullRgItems(AllSteamItemsGridView, rowIndex);
            if (list != null && list.Count > 0) LastSelectedItemDescription = list[0].Description;
        }

        private void ItemsToSaleGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            var cell = ItemsToSaleGridView.CurrentCell;
            if (cell == null) return;

            var row = cell.RowIndex;
            if (row < 0) return;

            var rowIndex = 0;
            if (ItemsToSaleGridView.SelectedRows.Count > 1)
                rowIndex = ItemsToSaleGridView.SelectedRows[ItemsToSaleGridView.SelectedRows.Count - 1].Cells[0]
                    .RowIndex;
            else
                rowIndex = row;
            ItemsToSaleGridUtils.RowClick(ItemsToSaleGridView, row, AllDescriptionsDictionary, AllSteamItemsGridView,
                ItemDescriptionTextBox, ItemImageBox, ItemNameLable);
            var list = ItemsToSaleGridUtils.GetFullRgItems(ItemsToSaleGridView, rowIndex);
            if (list != null && list.Count > 0) LastSelectedItemDescription = list[0].Description;
        }

        private void SteamSaleDataGridView_EditingControlShowing(object sender,
            DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboBox cb)
            {
                cb.IntegralHeight = false;
                cb.MaxDropDownItems = 10;
            }
        }

        private void OpskinsPanel_Click(object sender, EventArgs e)
        {
            if (LastSelectedItemDescription == null) return;
            Process.Start("https://" +
                          $"opskins.com/?loc=shop_search&search_item={LastSelectedItemDescription.Name}&sort=lh");
        }

        private void DeleteAccountButton_Click(object sender, EventArgs e)
        {
            var cell = ItemsToSaleGridView.CurrentCell;
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

        private void InventoryAppIdComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (InventoryAppIdComboBox.Text)
            {
                case "STEAM":
                {
                    InventoryContextIdComboBox.Text = "6";
                    break;
                }
                case "TF":
                {
                    InventoryContextIdComboBox.Text = "2";
                    break;
                }
                case "CS:GO":
                {
                    InventoryContextIdComboBox.Text = "2";
                    break;
                }
                case "PUBG":
                {
                    InventoryContextIdComboBox.Text = "2";
                    break;
                }
            }

            SavedSettings.UpdateField(ref SavedSettings.Get().MarketInventoryAppId, InventoryAppIdComboBox.Text);
        }

        private void LoadInventoryButton_Click(object sender, EventArgs e)
        {
            if (CurrentSession.SteamManager == null)
            {
                MessageBox.Show("You should login first", "Error inventory loading", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Logger.Error("Error on inventory loading. No logined account found.");
                return;
            }

            if (string.IsNullOrEmpty(InventoryAppIdComboBox.Text) ||
                string.IsNullOrEmpty(InventoryContextIdComboBox.Text))
            {
                MessageBox.Show("You should chose inventory type first", "Error inventory loading",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                default:
                    appid = InventoryAppIdComboBox.Text;
                    break;
            }

            var contextId = InventoryContextIdComboBox.Text;
            CurrentSession.CurrentInventoryAppId = appid;
            CurrentSession.CurrentInventoryContextId = contextId;
            PriceLoader.StopAll();

            Logger.Debug($"Inventory {appid} - {contextId} loading started");

            Task.Run(() =>
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
                MessageBox.Show("You should log in first", "Error sending trade offer", MessageBoxButtons.OK,
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
                changeValue = (double) AveragePriceNumericUpDown.Value;
                changePercentValue = (double) AveragePricePercentNumericUpDown.Value;
            }
            else if (CurrentMinusPriceRadioButton.Checked)
            {
                marketSaleType = MarketSaleType.LowerThanCurrent;
                changeValue = (double) CurrentPriceNumericUpDown.Value;
                changePercentValue = (double) CurrentPricePercentNumericUpDown.Value;
            }
            else
            {
                throw new InvalidOperationException("Not implemented market sale type");
            }

            PriceLoader.StopAll();
            var itemsToSale = new PriceShaper(ItemsToSaleGridView, marketSaleType, changeValue, changePercentValue)
                .GetItemsForSales();

            Program.WorkingProcessForm.InitProcess(() => CurrentSession.SteamManager.SellOnMarket(itemsToSale));
        }

        public void DeleteSoldItem(string marketHashName)
        {
            var row = ItemsToSaleGridUtils.GetDataGridViewRowByMarketHashName(ItemsToSaleGridView, marketHashName);
            if (row != null) ItemsToSaleGridView.Rows.Remove(row);
        }

        private void AddAllButton_Click(object sender, EventArgs e)
        {
            AllSteamItemsGridView.CurrentCellChanged -= AllSteamItemsGridView_CurrentCellChanged;
            ItemsToSaleGridView.CurrentCellChanged -= ItemsToSaleGridView_CurrentCellChanged;

            AllItemsListGridUtils.AddCellListToSale(AllSteamItemsGridView, ItemsToSaleGridView,
                AllSteamItemsGridView.SelectedRows.Cast<DataGridViewRow>().ToArray());

            AllSteamItemsGridView.CurrentCellChanged += AllSteamItemsGridView_CurrentCellChanged;
            ItemsToSaleGridView.CurrentCellChanged += ItemsToSaleGridView_CurrentCellChanged;

            Logger.Debug("All selected items was added to sale list");
            PriceLoader.StartPriceLoading(TableToLoad.ItemsToSaleTable);
        }

        private void OpenMarketPageButtonClick_Click(object sender, EventArgs e)
        {
            if (LastSelectedItemDescription == null) return;
            Process.Start("https://" +
                          $"steamcommunity.com/market/listings/{LastSelectedItemDescription.Appid}/" +
                          LastSelectedItemDescription.MarketHashName);
        }

        private async void AllItemsUpdateOneButtonClick_Click(object sender, EventArgs e)
        {
            await Task.Run(async () =>
            {
                var selectedRows = AllSteamItemsGridView.SelectedRows.Cast<DataGridViewRow>();
                foreach (var row in selectedRows)
                {
                    var item = AllItemsListGridUtils.GetFullRgItems(AllSteamItemsGridView, row.Index).FirstOrDefault();
                    if (item == null) continue;

                    var averagePrice = CurrentSession.SteamManager.GetAveragePrice(item.Asset, item.Description, SavedSettings.Get().SettingsAveragePriceParseDays);
                    PriceLoader.AveragePricesCache.Cache(item.Description.MarketHashName, averagePrice.Value);

                    var currentPrice = await CurrentSession.SteamManager.GetCurrentPrice(item.Asset, item.Description);
                    PriceLoader.CurrentPricesCache.Cache(item.Description.MarketHashName, currentPrice.Value);

                    row.Cells[AllItemsListGridUtils.CurrentColumnIndex].Value = currentPrice;
                    row.Cells[AllItemsListGridUtils.AverageColumnIndex].Value = averagePrice;
                }
            });
        }

        private void InventoryContextIdComboBox_TextChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().MarketInventoryContexId,
                InventoryContextIdComboBox.Text);
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
            await Task.Run(async () =>
            {
                try
                {
                    var selectedRows = ItemsToSaleGridView.SelectedRows.Cast<DataGridViewRow>();
                    foreach (var row in selectedRows)
                    {
                        var item = ItemsToSaleGridUtils.GetFullRgItems(ItemsToSaleGridView, row.Index).FirstOrDefault();
                        if (item == null) continue;

                        var averagePrice = CurrentSession.SteamManager.GetAveragePrice(item.Asset, item.Description, SavedSettings.Get().SettingsAveragePriceParseDays);
                        PriceLoader.AveragePricesCache.Cache(item.Description.MarketHashName, averagePrice.Value);

                        var currentPrice =
                            await CurrentSession.SteamManager.GetCurrentPrice(item.Asset, item.Description);
                        PriceLoader.CurrentPricesCache.Cache(item.Description.MarketHashName, currentPrice.Value);

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
    }
}