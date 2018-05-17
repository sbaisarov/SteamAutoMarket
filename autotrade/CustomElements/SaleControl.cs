﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Globalization;
using OPSkins.Model.Inventory;
using static autotrade.Interfaces.Steam.TradeOffer.Inventory;
using SteamKit2;
using System.Collections;
using System.Threading.Tasks;
using System.Linq;
using autotrade.CustomElements;
using System.Threading;
using autotrade.Utils;

namespace autotrade {
    public partial class SaleControl : UserControl {
        ApiServices services = new ApiServices();

        public static Dictionary<string, RgDescription> AllDescriptionsDictionary { get; set; }
        public static RgDescription LastSelectedItemDescription { get; set; }
        public static Dictionary<string, Image> ImageDictionary { get; set; }


        public SaleControl() {
            InitializeComponent();
        }

        private void SaleControl_Load(object sender, EventArgs e) {
            AllDescriptionsDictionary = new Dictionary<string, RgDescription>();
            ImageDictionary = new Dictionary<string, Image>();
        }

        public void LoadInventory() {
            ManualResetEvent dialogLoadedFlag = new ManualResetEvent(false);
            Task.Run(() => {
                Form waitDialog = new Form() {
                    Name = "Загрузка инвентаря",
                    Text = "Идет загрузка инвентаря, пожалуйста подождите...",
                    ControlBox = false,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    StartPosition = FormStartPosition.CenterParent,
                    Width = 300,
                    Height = 70,
                    Enabled = true
                };

                ProgressBar ScrollingBar = new ProgressBar() {
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
                        List<RgFullItem> allItemsList = ProcessSteamInventory();
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
        }

        private void SteamSaleDataGridView_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) {
                return;
            }
            AllItemsListGridUtils.UpdateItemDescription(AllSteamItemsGridView, AllSteamItemsGridView.CurrentCell.RowIndex, ItemDescriptionTextBox, ItemImageBox, ItemNameLable);

            if (e.ColumnIndex == 2) {
                AllItemsListGridUtils.GridComboBoxClick(AllSteamItemsGridView, e.RowIndex);
            }
            else if (e.ColumnIndex == 3) {
                AllItemsListGridUtils.GridAddButtonClick(AllSteamItemsGridView, e.RowIndex, ItemsToSaleGridView);
            }
            else if (e.ColumnIndex == 4) {
                AllItemsListGridUtils.GridAddAllButtonClick(AllSteamItemsGridView, e.RowIndex, ItemsToSaleGridView);
            }
        }

        private void AllSteamItemsGridView_CurrentCellChanged(object sender, EventArgs e) {
            var cell = AllSteamItemsGridView.CurrentCell;
            if (cell == null) return;

            var row = cell.RowIndex;
            if (row < 0) return;

            AllItemsListGridUtils.UpdateItemDescription(AllSteamItemsGridView, AllSteamItemsGridView.CurrentCell.RowIndex, ItemDescriptionTextBox, ItemImageBox, ItemNameLable);
        }

        private void ItemsToSaleGridView_CurrentCellChanged(object sender, EventArgs e) {
            var cell = ItemsToSaleGridView.CurrentCell;
            if (cell == null) return;

            var row = cell.RowIndex;
            if (row < 0) return;

            ItemsToSaleGridUtils.RowClick(ItemsToSaleGridView, row, AllDescriptionsDictionary, AllSteamItemsGridView, ItemDescriptionTextBox, ItemImageBox, ItemNameLable);
        }

        private void SteamSaleDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            if (e.Control is ComboBox cb) {
                cb.IntegralHeight = false;
                cb.MaxDropDownItems = 10;
            }
        }

        private List<RgFullItem> ProcessSteamInventory() {
            var allItemsInventory = services.SteamAllInventory();
            var allItemsList = new List<RgFullItem>();

            foreach (var item in allItemsInventory.assets) {
                RgFullItem rgFullItem = new RgFullItem {
                    Asset = item,
                    Description = InventoryRootModel.GetDescription(item, allItemsInventory.descriptions)
                };
                allItemsList.Add(rgFullItem);
            }

            return allItemsList;
        }

        private void DeleteItemButton_Click(object sender, EventArgs e) {
            ItemsToSaleGridUtils.DeleteButtonClick(AllSteamItemsGridView, ItemsToSaleGridView);
        }

        private void AddAllPanel_Click(object sender, EventArgs e) {
            this.AllSteamItemsGridView.CurrentCellChanged -= new System.EventHandler(this.AllSteamItemsGridView_CurrentCellChanged);

            while (AllSteamItemsGridView.RowCount > 0) {
                AllItemsListGridUtils.GridAddAllButtonClick(AllSteamItemsGridView, 0, ItemsToSaleGridView);
            }

            this.AllSteamItemsGridView.CurrentCellChanged += new System.EventHandler(this.AllSteamItemsGridView_CurrentCellChanged);
            ItemsToSaleGridView_CurrentCellChanged(null, null);
        }

        private void RefreshPanel_Click(object sender, EventArgs e) {
            AllSteamItemsGridView.Rows.Clear();
            ItemsToSaleGridView.Rows.Clear();

            LoadInventory();
            AllSteamItemsGridView_CurrentCellChanged(null, null);
        }

        #region Items to sale design
        private void DeleteItemButton_MouseLeave(object sender, EventArgs e) {
            DeleteItemButton.BackColor = Color.RoyalBlue;
        }

        private void DeleteItemButton_MouseEnter(object sender, EventArgs e) {
            DeleteItemButton.BackColor = Color.DodgerBlue;
        }

        private void ItemsToSaleGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
            if (!DeleteItemButton.Enabled) DeleteItemButton.Enabled = true;
        }

        private void ItemsToSaleGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) {
            DeleteItemButton.Enabled = (ItemsToSaleGridView.RowCount > 0) ? true : false;
        }
        #endregion

        #region GridMenu buttuns design
        private void AddAllPanel_MouseEnter(object sender, EventArgs e) {
            AddAllPanel.BackColor = Color.LightGray;
        }

        private void AddAllPanel_MouseLeave(object sender, EventArgs e) {
            AddAllPanel.BackColor = Color.Silver;
        }

        private void AddAllPanel_MouseDown(object sender, MouseEventArgs e) {
            AddAllPanel.BackColor = SystemColors.ControlLight;
            AddAllPanel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void AddAllPanel_MouseUp(object sender, MouseEventArgs e) {
            AddAllPanel.BackColor = Color.LightGray;
            AddAllPanel.BorderStyle = BorderStyle.None;
        }

        private void RefreshPanel_MouseDown(object sender, MouseEventArgs e) {
            RefreshPanel.BackColor = SystemColors.ControlLight;
            RefreshPanel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void RefreshPanel_MouseEnter(object sender, EventArgs e) {
            RefreshPanel.BackColor = Color.LightGray;
        }

        private void RefreshPanel_MouseLeave(object sender, EventArgs e) {
            RefreshPanel.BackColor = Color.Silver;
        }

        private void RefreshPanel_MouseUp(object sender, MouseEventArgs e) {
            RefreshPanel.BackColor = Color.LightGray;
            RefreshPanel.BorderStyle = BorderStyle.None;
        }

        private void SteamPanel_MouseDown(object sender, MouseEventArgs e) {
            SteamPanel.BackColor = SystemColors.ControlLight;
            SteamPanel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void SteamPanel_MouseEnter(object sender, EventArgs e) {
            SteamPanel.BackColor = Color.LightGray;
        }

        private void SteamPanel_MouseLeave(object sender, EventArgs e) {
            SteamPanel.BackColor = Color.Silver;
        }

        private void SteamPanel_MouseUp(object sender, MouseEventArgs e) {
            SteamPanel.BackColor = Color.LightGray;
            SteamPanel.BorderStyle = BorderStyle.None;
        }

        private void OpskinsPanel_MouseDown(object sender, MouseEventArgs e) {
            OpskinsPanel.BackColor = SystemColors.ControlLight;
            OpskinsPanel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void OpskinsPanel_MouseEnter(object sender, EventArgs e) {
            OpskinsPanel.BackColor = Color.LightGray;
        }

        private void OpskinsPanel_MouseLeave(object sender, EventArgs e) {
            OpskinsPanel.BackColor = Color.Silver;
        }

        private void OpskinsPanel_MouseUp(object sender, MouseEventArgs e) {
            OpskinsPanel.BackColor = Color.LightGray;
            OpskinsPanel.BorderStyle = BorderStyle.None;
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
    }
}