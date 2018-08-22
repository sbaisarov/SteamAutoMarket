using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Globalization;
using OPSkins.Model.Inventory;
using static autotrade.Steam.TradeOffer.Inventory;
using SteamKit2;
using System.Collections;
using System.Threading.Tasks;
using System.Linq;
using autotrade.CustomElements;
using System.Threading;
using autotrade.Utils;
using autotrade.WorkingProcess;
using System.Runtime.CompilerServices;
using autotrade.WorkingProcess.PriceLoader;
using autotrade.WorkingProcess.Settings;
using static autotrade.Steam.SteamManager;
using autotrade.WorkingProcess.MarketPriceFormation;
using autotrade.CustomElements.Utils;

namespace autotrade {
    public partial class SaleControl : UserControl {

        public static Dictionary<string, RgDescription> AllDescriptionsDictionary { get; set; }
        public static RgDescription LastSelectedItemDescription { get; set; }

        public SaleControl() {
            InitializeComponent();
            PriceLoader.Init(this.AllSteamItemsGridView, this.ItemsToSaleGridView);

            InventoryAppIdComboBox.Text = SavedSettings.Get().MARKET_INVENTORY_APP_ID;
            InventoryContextIdComboBox.Text = SavedSettings.Get().MARKET_INVENTORY_CONTEX_ID;
        }

        private void SaleControl_Load(object sender, EventArgs e) {
            AllDescriptionsDictionary = new Dictionary<string, RgDescription>();
        }

        public void AuthCurrentAccount() {
            this.AccountNameLable.Text = CurrentSession.SteamManager.Guard.AccountName;
            this.SplitterPanel.BackgroundImage = CurrentSession.AccountImage;
        }

        public void LoadInventory(string steamid, string appid, string contextid) {
            Dispatcher.Invoke(Program.MainForm, () => {
                AllSteamItemsGridView.Rows.Clear();
                ItemsToSaleGridView.Rows.Clear();
            });

            Program.LoadingForm.InitInventoryLoadingProcess();
            List<RgFullItem> allItemsList = Program.LoadingForm.GetLoadedItems();
            Program.LoadingForm.DisactivateForm();

            allItemsList.RemoveAll(item => item.Description.marketable == false);

            Dispatcher.Invoke(Program.MainForm, () => {
                AllItemsListGridUtils.FillSteamSaleDataGrid(AllSteamItemsGridView, allItemsList);
            });
        }

        private void SteamSaleDataGridView_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) {
                return;
            }

            AllItemsListGridUtils.UpdateItemDescription(AllSteamItemsGridView, AllSteamItemsGridView.CurrentCell.RowIndex, ItemDescriptionTextBox, ItemImageBox, ItemNameLable);

            if (e.ColumnIndex == 5) {
                AllItemsListGridUtils.GridComboBoxClick(AllSteamItemsGridView, e.RowIndex);
            } else if (e.ColumnIndex == 6) {
                AllItemsListGridUtils.GridAddButtonClick(AllSteamItemsGridView, e.RowIndex, ItemsToSaleGridView);
                PriceLoader.StartPriceLoading(TableToLoad.ITEMS_TO_SALE_TABLE);
            }
        }

        private void AllSteamItemsGridView_CurrentCellChanged(object sender, EventArgs e) {
            var cell = AllSteamItemsGridView.CurrentCell;
            if (cell == null) return;

            var row = cell.RowIndex;
            if (row < 0) return;

            int rowIndex = 0;
            if (AllSteamItemsGridView.SelectedRows.Count > 1) {
                rowIndex = AllSteamItemsGridView.SelectedRows[AllSteamItemsGridView.SelectedRows.Count - 1].Cells[0].RowIndex;
            } else {
                rowIndex = AllSteamItemsGridView.CurrentCell.RowIndex;
            }
            AllItemsListGridUtils.UpdateItemDescription(AllSteamItemsGridView, rowIndex, ItemDescriptionTextBox, ItemImageBox, ItemNameLable);
            var list = AllItemsListGridUtils.GetRgFullItems(AllSteamItemsGridView, rowIndex);
            if (list != null && list.Count > 0) LastSelectedItemDescription = list[0].Description;
        }

        private void ItemsToSaleGridView_CurrentCellChanged(object sender, EventArgs e) {
            var cell = ItemsToSaleGridView.CurrentCell;
            if (cell == null) return;

            var row = cell.RowIndex;
            if (row < 0) return;

            int rowIndex = 0;
            if (ItemsToSaleGridView.SelectedRows.Count > 1) {
                rowIndex = ItemsToSaleGridView.SelectedRows[ItemsToSaleGridView.SelectedRows.Count - 1].Cells[0].RowIndex;
            } else {
                rowIndex = row;
            }
            ItemsToSaleGridUtils.RowClick(ItemsToSaleGridView, row, AllDescriptionsDictionary, AllSteamItemsGridView, ItemDescriptionTextBox, ItemImageBox, ItemNameLable);
            var list = ItemsToSaleGridUtils.GetRgFullItems(ItemsToSaleGridView, rowIndex);
            if (list != null && list.Count > 0) LastSelectedItemDescription = list[0].Description;
        }

        private void SteamSaleDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            if (e.Control is ComboBox cb) {
                cb.IntegralHeight = false;
                cb.MaxDropDownItems = 10;
            }
        }

        private void OpskinsPanel_Click(object sender, EventArgs e) {
            if (LastSelectedItemDescription == null) return;
            System.Diagnostics.Process.Start("https://" +
                $"opskins.com/?loc=shop_search&search_item={LastSelectedItemDescription.name}&sort=lh");
        }

        private void DeleteAccountButton_Click(object sender, EventArgs e) {
            var cell = ItemsToSaleGridView.CurrentCell;
            if (cell == null) {
                Logger.Warning("No accounts selected");
                return;
            }

            if (cell.RowIndex < 0) return;

            ItemsToSaleGridUtils.DeleteButtonClick(AllSteamItemsGridView, ItemsToSaleGridView, cell.RowIndex);
        }

        private void DeleteUnmarketableSteamButton_Click(object sender, EventArgs e) {
            ItemsToSaleGridUtils.DeleteUnmarketable(AllSteamItemsGridView, ItemsToSaleGridView);
        }

        private void DeleteUntradableSteamButton_Click(object sender, EventArgs e) {
            ItemsToSaleGridUtils.DeleteUntradable(AllSteamItemsGridView, ItemsToSaleGridView);
        }

        private void HalfAutoPriceRadioButton_CheckedChanged(object sender, EventArgs e) {
            if (CurrentMinusPriceRadioButton.Checked) {
                CurrentPriceNumericUpDown.Visible = true;
                CurrentPricePercentNumericUpDown.Visible = true;
            } else {
                CurrentPriceNumericUpDown.Visible = false;
                CurrentPricePercentNumericUpDown.Visible = false;
            }
        }

        private void ItemsToSaleGridView_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0) return;
            if (ItemsToSaleGridView.Rows.Count == 1) ItemsToSaleGridView_CurrentCellChanged(sender, e);
            if (e.ColumnIndex != 2) return;
            if (!ManualPriceRadioButton.Checked) {
                ItemToSalePriceColumn.ReadOnly = true;
                return;
            } else {
                ItemToSalePriceColumn.ReadOnly = false;
                ItemsToSaleGridUtils.CellClick(ItemsToSaleGridView, e.RowIndex);
            }
        }

        private void ItemsToSaleGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            ItemsToSaleGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void InventoryAppIdComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            switch (InventoryAppIdComboBox.Text) {
                case "STEAM": { InventoryContextIdComboBox.Text = "6"; break; }
                case "TF": { InventoryContextIdComboBox.Text = "2"; break; }
                case "CS:GO": { InventoryContextIdComboBox.Text = "2"; break; }
                case "PUBG": { InventoryContextIdComboBox.Text = "2"; break; }
            }
            SavedSettings.UpdateField(ref SavedSettings.Get().MARKET_INVENTORY_APP_ID, InventoryAppIdComboBox.Text);
        }

        private void LoadInventoryButton_Click(object sender, EventArgs e) {
            if (CurrentSession.SteamManager == null) {
                MessageBox.Show("You should login first", "Error inventory loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error on inventory loading. No logined account found.");
                return;
            }
            if (String.IsNullOrEmpty(InventoryAppIdComboBox.Text) || String.IsNullOrEmpty(InventoryContextIdComboBox.Text)) {
                MessageBox.Show("You should chose inventory type first", "Error inventory loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error on inventory loading. No inventory type chosed.");
                return;
            }
            LoadInventoryButton.Enabled = false;

            string appid;
            switch (InventoryAppIdComboBox.Text) {
                case "STEAM": appid = "753"; break;
                case "TF": appid = "440"; break;
                case "CS:GO": appid = "730"; break;
                case "PUBG": appid = "578080"; break;
                default: appid = InventoryAppIdComboBox.Text; break;
            }
            string contextId = InventoryContextIdComboBox.Text;
            CurrentSession.CurrentInventoryAppId = appid;
            CurrentSession.CurrentInventoryContextId = contextId;
            PriceLoader.StopAll();

            Logger.Debug($"Inventory {appid} - {contextId} loading started");

            Task.Run(() => {
                LoadInventory(CurrentSession.SteamManager.Guard.Session.SteamID.ToString(), appid, contextId);
                Logger.Info($"Inventory {appid} - {contextId} loading finished");
                Dispatcher.Invoke(Program.MainForm, () => {
                    LoadInventoryButton.Enabled = true;
                });
                PriceLoader.StartPriceLoading(TableToLoad.ALL_ITEMS_TABLE);
            });
        }

        private void StartSteamSellButton_Click(object sender, EventArgs e) {
            if (CurrentSession.SteamManager == null) {
                MessageBox.Show("You should log in first", "Error sending trade offer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error on inventory loading. No logged in accounts found.");
                return;
            }

            double changeValue = 0, changePercentValue = 0;

            MarketSaleType marketSaleType;
            if (ManualPriceRadioButton.Checked) {
                marketSaleType = MarketSaleType.MANUAL;

            } else if (RecomendedPriceRadioButton.Checked) {
                marketSaleType = MarketSaleType.RECOMENDED;

            } else if (AveregeMinusPriceRadioButton.Checked) {
                marketSaleType = MarketSaleType.LOWER_THAN_AVERAGE;
                changeValue = (double)AveragePriceNumericUpDown.Value;
                changePercentValue = (double)AveragePricePercentNumericUpDown.Value;

            } else if (CurrentMinusPriceRadioButton.Checked) {
                marketSaleType = MarketSaleType.LOWER_THAN_CURRENT;
                changeValue = (double)CurrentPriceNumericUpDown.Value;
                changePercentValue = (double)CurrentPricePercentNumericUpDown.Value;

            } else throw new InvalidOperationException("Not implemented market sale type");

            PriceLoader.StopAll();
            ToSaleObject itemsToSale = new PriceShaper(ItemsToSaleGridView, marketSaleType, changeValue, changePercentValue).GetItemsForSales();

            Program.WorkingProcessForm.InitProcess(() => CurrentSession.SteamManager.SellOnMarket(itemsToSale));
        }

        public void DeleteSoldItem(string marketHashName) {
            var row = ItemsToSaleGridUtils.GetDataGridViewRowByMarketHashName(ItemsToSaleGridView, marketHashName);
            if (row != null) ItemsToSaleGridView.Rows.Remove(row);
        }

        private void AddAllButton_Click(object sender, EventArgs e) {
            this.AllSteamItemsGridView.CurrentCellChanged -= new EventHandler(this.AllSteamItemsGridView_CurrentCellChanged);
            this.ItemsToSaleGridView.CurrentCellChanged -= new EventHandler(this.ItemsToSaleGridView_CurrentCellChanged);

            AllItemsListGridUtils.AddCellListToSale(AllSteamItemsGridView, ItemsToSaleGridView, AllSteamItemsGridView.SelectedRows.Cast<DataGridViewRow>().ToArray());

            this.AllSteamItemsGridView.CurrentCellChanged += new EventHandler(this.AllSteamItemsGridView_CurrentCellChanged);
            this.ItemsToSaleGridView.CurrentCellChanged += new EventHandler(this.ItemsToSaleGridView_CurrentCellChanged);

            Logger.Debug("All selected items was added to sale list");
            PriceLoader.StartPriceLoading(TableToLoad.ITEMS_TO_SALE_TABLE);
        }

        private void OpenMarketPageButtonClick_Click(object sender, EventArgs e) {
            if (LastSelectedItemDescription == null) return;
            System.Diagnostics.Process.Start("https://" +
                $"steamcommunity.com/market/listings/{LastSelectedItemDescription.appid}/" +
                LastSelectedItemDescription.market_hash_name);
        }

        private void OpenGameInventoryPageButton_Click(object sender, EventArgs e) {
            if (LastSelectedItemDescription == null) return;
            System.Diagnostics.Process.Start("https://" + $"steamcommunity.com/profiles/{CurrentSession.SteamManager.Guard.Session.SteamID}" +
                $"/inventory/#{CurrentSession.CurrentInventoryAppId}_{CurrentSession.CurrentInventoryContextId}");
        }

        private void InventoryContextIdComboBox_TextChanged(object sender, EventArgs e) {
            SavedSettings.UpdateField(ref SavedSettings.Get().MARKET_INVENTORY_CONTEX_ID, InventoryContextIdComboBox.Text);
        }

        private void CurrentPricePercentNumericUpDown_ValueChanged(object sender, EventArgs e) {
            if (CurrentPriceNumericUpDown.Value != 0) {
                var tmp = CurrentPricePercentNumericUpDown.Value;
                CurrentPriceNumericUpDown.Value = 0;
                CurrentPricePercentNumericUpDown.Value = tmp;
            }
        }

        private void CurrentPriceNumericUpDown_ValueChanged(object sender, EventArgs e) {
            if (CurrentPricePercentNumericUpDown.Value != 0) {
                var tmp = CurrentPriceNumericUpDown.Value;
                CurrentPricePercentNumericUpDown.Value = 0;
                CurrentPriceNumericUpDown.Value = tmp;
            }
        }

        private void AveragePriceNumericUpDown_ValueChanged(object sender, EventArgs e) {
            if (AveragePricePercentNumericUpDown.Value != 0) {
                var tmp = AveragePriceNumericUpDown.Value;
                AveragePricePercentNumericUpDown.Value = 0;
                AveragePriceNumericUpDown.Value = tmp;
            }
        }

        private void AveragePricePercentNumericUpDown_ValueChanged(object sender, EventArgs e) {
            if (AveragePriceNumericUpDown.Value != 0) {
                var tmp = AveragePricePercentNumericUpDown.Value;
                AveragePriceNumericUpDown.Value = 0;
                AveragePricePercentNumericUpDown.Value = tmp;
            }
        }

        private void AveregeMinusPriceRadioButton_CheckedChanged(object sender, EventArgs e) {
            if (AveregeMinusPriceRadioButton.Checked) {
                AveragePriceNumericUpDown.Visible = true;
                AveragePricePercentNumericUpDown.Visible = true;
            } else {
                AveragePriceNumericUpDown.Visible = false;
                AveragePricePercentNumericUpDown.Visible = false;
            }
        }

        private void RefreshPricesButton_Click(object sender, EventArgs e) {
            PriceLoader.StartPriceLoading(TableToLoad.ITEMS_TO_SALE_TABLE, true);
            PriceLoader.StartPriceLoading(TableToLoad.ALL_ITEMS_TABLE, true);
        }

        private void ForcePricesReloadButton_Click(object sender, EventArgs e) {
            PriceLoader.StartPriceLoading(TableToLoad.ITEMS_TO_SALE_TABLE, true);
        }
    }
}