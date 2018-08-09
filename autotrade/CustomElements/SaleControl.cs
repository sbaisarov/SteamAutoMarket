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

namespace autotrade {
    public partial class SaleControl : UserControl {

        public static Dictionary<string, RgDescription> AllDescriptionsDictionary { get; set; }
        public static RgDescription LastSelectedItemDescription { get; set; }

        public SaleControl() {
            InitializeComponent();
            PriceLoader.Init(this.ItemsToSaleGridView);

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

            Program.InventoryLoadingForm.InitProcess();
            List<RgFullItem> allItemsList = Program.InventoryLoadingForm.GetLoadedItems();
            Program.InventoryLoadingForm.Disactivate();

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

            if (e.ColumnIndex == 3) {
                AllItemsListGridUtils.GridComboBoxClick(AllSteamItemsGridView, e.RowIndex);
            } else if (e.ColumnIndex == 4) {
                AllItemsListGridUtils.GridAddButtonClick(AllSteamItemsGridView, e.RowIndex, ItemsToSaleGridView);
                PriceLoader.StartPriceLoading();
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
            if (HalfAutoPriceRadioButton.Checked) {
                CurrentPriceNumericUpDown.Visible = true;
                CurrentPricePercentNumericUpDown.Visible = true;
                CurrentPriceNumericUpDown.Enabled = true;
                CurrentPricePercentNumericUpDown.Enabled = true;
            } else {
                CurrentPriceNumericUpDown.Visible = false;
                CurrentPricePercentNumericUpDown.Visible = false;
                CurrentPriceNumericUpDown.Enabled = false;
                CurrentPricePercentNumericUpDown.Enabled = false;
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
            if (SavedSettings.Get().MARKET_INVENTORY_APP_ID != InventoryAppIdComboBox.Text) {
                SavedSettings.Get().MARKET_INVENTORY_APP_ID = InventoryAppIdComboBox.Text;
                SavedSettings.UpdateAll();
            }
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
            CurrentSession.InventoryAppId = appid;
            CurrentSession.InventoryContextId = contextId;
            Logger.Debug($"Inventory {appid} - {contextId} loading started");

            Task.Run(() => {
                LoadInventory(CurrentSession.SteamManager.Guard.Session.SteamID.ToString(), appid, contextId);
                Logger.Info($"Inventory {appid} - {contextId} loading finished");
                Dispatcher.Invoke(Program.MainForm, () => {
                    LoadInventoryButton.Enabled = true;
                });
            });
        }

        private void StartSteamSellButton_Click(object sender, EventArgs e) {
            if (CurrentSession.SteamManager == null) {
                MessageBox.Show("You should log in first", "Error sending trade offer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error on inventory loading. No logged in accounts found.");
                return;
            }

            MarketSaleType marketSaleType;
            if (RecomendedPriceRadioButton.Checked) marketSaleType = MarketSaleType.RECOMMENDED;
            else if (ManualPriceRadioButton.Checked) marketSaleType = MarketSaleType.MANUAL;
            else if (HalfAutoPriceRadioButton.Checked) marketSaleType = MarketSaleType.LOWER_THAN_CURRENT;
            else return;

            var itemsToSale = new Dictionary<RgFullItem, double>();

            if (marketSaleType == MarketSaleType.MANUAL) {
                for (int i = 0; i < ItemsToSaleGridView.Rows.Count; i++) {
                    var itemsList = ItemsToSaleGridUtils.GetRowItemsList(ItemsToSaleGridView, i);
                    double.TryParse((string)ItemsToSaleGridView.Rows[i].Cells[2].Value, out double price);
                    if (price <= 0) {
                        MessageBox.Show($"Incorrect price of {itemsList.First().Description.name}", "Error price converting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.Error($"Incorrect price of {itemsList.First().Description.name} found");
                        return;
                    }
                    foreach (var item in itemsList) {
                        itemsToSale.Add(item, price);
                    }
                }
            }

            if (marketSaleType == MarketSaleType.RECOMMENDED) {
                for (int i = 0; i < ItemsToSaleGridView.Rows.Count; i++) {
                    var itemsList = ItemsToSaleGridUtils.GetRowItemsList(ItemsToSaleGridView, i);
                    foreach (var item in itemsList) {
                        itemsToSale.Add(item, 0);
                    }
                }
            }

            if (marketSaleType == MarketSaleType.LOWER_THAN_CURRENT) {
                double lowerAmount = (double)CurrentPriceNumericUpDown.Value;
                for (int i = 0; i < ItemsToSaleGridView.Rows.Count; i++) {
                    var itemsList = ItemsToSaleGridUtils.GetRowItemsList(ItemsToSaleGridView, i);
                    foreach (var item in itemsList) {
                        itemsToSale.Add(item, lowerAmount);
                    }
                }
            }
            Program.WorkingProcessForm.InitProcess(() => CurrentSession.SteamManager.SellOnMarket(itemsToSale, marketSaleType));
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

        private void AddAllButton_Click(object sender, EventArgs e) {
            this.AllSteamItemsGridView.CurrentCellChanged -= new EventHandler(this.AllSteamItemsGridView_CurrentCellChanged);
            this.ItemsToSaleGridView.CurrentCellChanged -= new EventHandler(this.ItemsToSaleGridView_CurrentCellChanged);

            AllItemsListGridUtils.AddCellListToSale(AllSteamItemsGridView, ItemsToSaleGridView, AllSteamItemsGridView.SelectedRows.Cast<DataGridViewRow>().ToArray());

            this.AllSteamItemsGridView.CurrentCellChanged += new EventHandler(this.AllSteamItemsGridView_CurrentCellChanged);
            this.ItemsToSaleGridView.CurrentCellChanged += new EventHandler(this.ItemsToSaleGridView_CurrentCellChanged);

            Logger.Debug("All selected items was added to sale list");
            PriceLoader.StartPriceLoading();
        }

        private void RefreshInventoryButton_Click(object sender, EventArgs e) {
            if (CurrentSession.SteamManager == null) {
                MessageBox.Show("You should login first", "Error inventory loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Warning("Error on inventory loading. No logined account found.");
                return;
            }
            if (String.IsNullOrEmpty(CurrentSession.InventoryAppId) || String.IsNullOrEmpty(CurrentSession.InventoryContextId)) {
                MessageBox.Show("You should load inventory first", "Error inventory loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Warning("Error on inventory loading. No inventory type chosed.");
                return;
            }

            Logger.Debug($"Refreshing ${CurrentSession.InventoryAppId} - ${CurrentSession.InventoryContextId} inventory");

            AllSteamItemsGridView.Rows.Clear();
            ItemsToSaleGridView.Rows.Clear();

            LoadInventory(CurrentSession.SteamManager.Guard.Session.SteamID.ToString(), CurrentSession.InventoryAppId, CurrentSession.InventoryContextId);
            AllSteamItemsGridView_CurrentCellChanged(null, null);
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
                $"/inventory/#{CurrentSession.InventoryAppId}_{CurrentSession.InventoryContextId}");
        }

        private void InventoryContextIdComboBox_TextChanged(object sender, EventArgs e) {
            if (SavedSettings.Get().MARKET_INVENTORY_CONTEX_ID != InventoryContextIdComboBox.Text) {
                SavedSettings.Get().MARKET_INVENTORY_CONTEX_ID = InventoryContextIdComboBox.Text;
                SavedSettings.UpdateAll();
            }
        }
    }
}