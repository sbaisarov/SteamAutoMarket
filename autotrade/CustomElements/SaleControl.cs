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

namespace autotrade {
    public partial class SaleControl : UserControl {
        bool isMousePressed = false;
        ApiServices services = new ApiServices();

        public static Dictionary<string, RgDescription> AllDescriptionsDictionary { get; set; }
        public static RgDescription LastSelectedItemDescription { get; set; }
        public static Dictionary<string, Image> ImageDictionary { get; set; }


        public SaleControl() {
            InitializeComponent();
        }

        public void ActivateTradeMode() {
            TradeGroupBox.BringToFront();
            TradeSendGroupBox.BringToFront();
        }

        public void ActivateSellMode() {
            PriceSettingsGroupBox.BringToFront();
            SellGroupBox.BringToFront();
        }

        private void SaleControl_Load(object sender, EventArgs e) {
            AllDescriptionsDictionary = new Dictionary<string, RgDescription>();
            ImageDictionary = new Dictionary<string, Image>();
        }

        public void AuthCurrentAccount() {
            this.AccountNameLable.Text = WorkingProcess.CurrentSession.SteamManager.Guard.AccountName;
            this.SplitterPanel.BackgroundImage = WorkingProcess.CurrentSession.AccountImage;
        }

        public void LoadInventory(string steamid, string appid, string contextid) {
            AllSteamItemsGridView.Rows.Clear();
            ItemsToSaleGridView.Rows.Clear();

            ManualResetEvent dialogLoadedFlag = new ManualResetEvent(false);
            Task.Run(() => {
                Logger.Info("Inventory loading started");
                Form waitDialog = new Form()
                {
                    Name = "Inventory loading",
                    Text = "Loading inventory, please wait...",
                    ControlBox = false,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    StartPosition = FormStartPosition.CenterParent,
                    Width = 300,
                    Height = 70,
                    Enabled = true
                };

                ProgressBar ScrollingBar = new ProgressBar()
                {
                    Style = ProgressBarStyle.Marquee,
                    Parent = waitDialog,
                    Dock = DockStyle.Fill,
                    Enabled = true
                };

                waitDialog.Load += new EventHandler((x, y) => {
                    dialogLoadedFlag.Set();
                });

                waitDialog.Shown += new EventHandler((x, y) => {
                    Task.Run(() => {
                        List<RgFullItem> allItemsList = CurrentSession.SteamManager.LoadInventory(steamid, appid, contextid);
                        Dispatcher.Invoke(Program.MainForm, () => {
                            AllItemsListGridUtils.FillSteamSaleDataGrid(AllSteamItemsGridView, allItemsList);
                        });
                        this.Invoke((MethodInvoker)(() => waitDialog.Close()));
                    });
                });

                this.Invoke((MethodInvoker)(() => waitDialog.ShowDialog(this)));
            });

            while (dialogLoadedFlag.WaitOne(100, true) == false)
                Application.DoEvents();

            Logger.Info("Inventory loading done");
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
            } else if (e.ColumnIndex == 5) {
                AllItemsListGridUtils.GridAddAllButtonClick(AllSteamItemsGridView, e.RowIndex, ItemsToSaleGridView);
            }
        }

        private void AllSteamItemsGridView_CurrentCellChanged(object sender, EventArgs e) {
            var cell = AllSteamItemsGridView.CurrentCell;
            if (cell == null) return;

            var row = cell.RowIndex;
            if (row < 0) return;

            if (AllSteamItemsGridView.SelectedRows.Count > 1) {
                AllItemsListGridUtils.UpdateItemDescription(AllSteamItemsGridView, AllSteamItemsGridView.SelectedRows[AllSteamItemsGridView.SelectedRows.Count - 1].Cells[0].RowIndex, ItemDescriptionTextBox, ItemImageBox, ItemNameLable);
            } else {
                AllItemsListGridUtils.UpdateItemDescription(AllSteamItemsGridView, AllSteamItemsGridView.CurrentCell.RowIndex, ItemDescriptionTextBox, ItemImageBox, ItemNameLable);
            }
        }

        private void ItemsToSaleGridView_CurrentCellChanged(object sender, EventArgs e) {
            var cell = ItemsToSaleGridView.CurrentCell;
            if (cell == null) return;

            var row = cell.RowIndex;
            if (row < 0) return;

            if (ItemsToSaleGridView.SelectedRows.Count > 1) {
                ItemsToSaleGridUtils.RowClick(ItemsToSaleGridView, ItemsToSaleGridView.SelectedRows[ItemsToSaleGridView.SelectedRows.Count - 1].Cells[0].RowIndex, AllDescriptionsDictionary, AllSteamItemsGridView, ItemDescriptionTextBox, ItemImageBox, ItemNameLable);
            } else {
                ItemsToSaleGridUtils.RowClick(ItemsToSaleGridView, row, AllDescriptionsDictionary, AllSteamItemsGridView, ItemDescriptionTextBox, ItemImageBox, ItemNameLable);
            }
        }

        private void SteamSaleDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            if (e.Control is ComboBox cb) {
                cb.IntegralHeight = false;
                cb.MaxDropDownItems = 10;
            }
        }

        private void AddAllPanel_Click(object sender, EventArgs e) {
            this.AllSteamItemsGridView.CurrentCellChanged -= new EventHandler(this.AllSteamItemsGridView_CurrentCellChanged);
            this.ItemsToSaleGridView.CurrentCellChanged -= new EventHandler(this.ItemsToSaleGridView_CurrentCellChanged);

            AllItemsListGridUtils.AddCellListToSale(AllSteamItemsGridView, ItemsToSaleGridView, AllSteamItemsGridView.SelectedRows.Cast<DataGridViewRow>().ToArray());

            this.AllSteamItemsGridView.CurrentCellChanged += new EventHandler(this.AllSteamItemsGridView_CurrentCellChanged);
            this.ItemsToSaleGridView.CurrentCellChanged += new EventHandler(this.ItemsToSaleGridView_CurrentCellChanged);

            Logger.Info("All selected items was added to sale list");
        }

        private void RefreshPanel_Click(object sender, EventArgs e) {
            if (CurrentSession.SteamManager == null) {
                MessageBox.Show("You should login first", "Error inventory loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error on inventory loading. No logined account found.");
                return;
            }
            if (String.IsNullOrEmpty(CurrentSession.InventoryAppId) || String.IsNullOrEmpty(CurrentSession.InventoryContextId)) {
                MessageBox.Show("You should load inventory first", "Error inventory loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error on inventory loading. No inventory type chosed.");
                return;
            }

            Logger.Info("Refreshing inventory");

            AllSteamItemsGridView.Rows.Clear();
            ItemsToSaleGridView.Rows.Clear();

            LoadInventory(CurrentSession.SteamManager.Guard.Session.SteamID.ToString(), CurrentSession.InventoryAppId, CurrentSession.InventoryContextId);
            AllSteamItemsGridView_CurrentCellChanged(null, null);
        }

        #region GridMenu buttuns design
        private void AddAllPanel_MouseEnter(object sender, EventArgs e) {
            //AddAllPanel.BackColor = Color.LightGray;
        }

        private void AddAllPanel_MouseLeave(object sender, EventArgs e) {
            //AddAllPanel.BackColor = Color.Silver;
        }

        private void AddAllPanel_MouseDown(object sender, MouseEventArgs e) {
            //AddAllPanel.BackColor = SystemColors.ControlLight;
            //AddAllPanel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void AddAllPanel_MouseUp(object sender, MouseEventArgs e) {
            //AddAllPanel.BackColor = Color.LightGray;
            //AddAllPanel.BorderStyle = BorderStyle.None;
        }

        private void RefreshPanel_MouseDown(object sender, MouseEventArgs e) {
            //RefreshPanel.BackColor = SystemColors.ControlLight;
            //RefreshPanel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void RefreshPanel_MouseEnter(object sender, EventArgs e) {
            //RefreshPanel.BackColor = Color.LightGray;
        }

        private void RefreshPanel_MouseLeave(object sender, EventArgs e) {
            //RefreshPanel.BackColor = Color.Silver;
        }

        private void RefreshPanel_MouseUp(object sender, MouseEventArgs e) {
            //RefreshPanel.BackColor = Color.LightGray;
            //RefreshPanel.BorderStyle = BorderStyle.None;
        }

        private void SteamPanel_MouseDown(object sender, MouseEventArgs e) {
            //SteamPanel.BackColor = SystemColors.ControlLight;
            //SteamPanel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void SteamPanel_MouseEnter(object sender, EventArgs e) {
            //SteamPanel.BackColor = Color.LightGray;
        }

        private void SteamPanel_MouseLeave(object sender, EventArgs e) {
            //SteamPanel.BackColor = Color.Silver;
        }

        private void SteamPanel_MouseUp(object sender, MouseEventArgs e) {
            //SteamPanel.BackColor = Color.LightGray;
            //SteamPanel.BorderStyle = BorderStyle.None;
        }

        private void OpskinsPanel_MouseDown(object sender, MouseEventArgs e) {
            //OpskinsPanel.BackColor = SystemColors.ControlLight;
            // OpskinsPanel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void OpskinsPanel_MouseEnter(object sender, EventArgs e) {
            //OpskinsPanel.BackColor = Color.LightGray;
        }

        private void OpskinsPanel_MouseLeave(object sender, EventArgs e) {
            //OpskinsPanel.BackColor = Color.Silver;
        }

        private void OpskinsPanel_MouseUp(object sender, MouseEventArgs e) {
            // OpskinsPanel.BackColor = Color.LightGray;
            //OpskinsPanel.BorderStyle = BorderStyle.None;
        }
        #endregion

        private void SteamPanel_Click(object sender, EventArgs e) {
            if (LastSelectedItemDescription == null) return;
            System.Diagnostics.Process.Start("https://" +
                $"steamcommunity.com/market/listings/{LastSelectedItemDescription.appid}/" +
                LastSelectedItemDescription.market_hash_name);
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
                CurrentPriceNumericUpDown.Enabled = true;
                CurrentPricePercentNumericUpDown.Enabled = true;
            } else {
                CurrentPriceNumericUpDown.Enabled = false;
                CurrentPricePercentNumericUpDown.Enabled = false;
            }
        }

        private void ItemsToSaleGridView_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0) return;
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
            Logger.Info($"Inventory {appid} - {contextId} loading started");

            LoadInventory(CurrentSession.SteamManager.Guard.Session.SteamID.ToString(), appid, contextId);

            Logger.Info($"Inventory {appid} - {contextId} loading finished");
            LoadInventoryButton.Enabled = true;
        }

        private void StartSteamSellButton_Click(object sender, EventArgs e) {
            if (CurrentSession.SteamManager == null) {
                MessageBox.Show("You should log in first", "Error sending trade offer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error on inventory loading. No logged in accounts found.");
                return;
            }
            if (String.IsNullOrEmpty(TradeParthenIdTextBox.Text) || String.IsNullOrEmpty(TradeTokenTextBox.Text)) {
                MessageBox.Show("You should choose target partner first", "Error sending trade offer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error sending trade offer. No target partner selected.");
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

        private void SendTradeButton_Click(object sender, EventArgs e) {
            if (CurrentSession.SteamManager == null) {
                MessageBox.Show("You should login first", "Error sending trade offer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error on inventory loading. No logined account found.");
                return;
            }
            if (String.IsNullOrEmpty(TradeParthenIdTextBox.Text) || String.IsNullOrEmpty(TradeTokenTextBox.Text)) {
                MessageBox.Show("You should chose target parthner first", "Error sending trade offer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error sending trade offer. No target parthner selected.");
                return;
            }

            var itemsToSale = new List<RgFullItem>();

            for (int i = 0; i < ItemsToSaleGridView.Rows.Count; i++) {
                var itemsList = ItemsToSaleGridUtils.GetRowItemsList(ItemsToSaleGridView, i);
                foreach (var item in itemsList) {
                    itemsToSale.Add(item);
                }
            }

            CurrentSession.SteamManager.SendTradeOffer(itemsToSale, TradeParthenIdTextBox.Text, TradeTokenTextBox.Text);
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

        private void SaleControl_MouseDown(object sender, MouseEventArgs e) {

        }

        private void SaleControl_MouseUp(object sender, MouseEventArgs e) {

        }
    }
}