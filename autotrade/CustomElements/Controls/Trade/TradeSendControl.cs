using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using autotrade.CustomElements.Elements;
using autotrade.CustomElements.Utils;
using autotrade.Steam.TradeOffer.Models;
using autotrade.Steam.TradeOffer.Models.Full;
using autotrade.Utils;
using autotrade.WorkingProcess;
using autotrade.WorkingProcess.PriceLoader;
using autotrade.WorkingProcess.Settings;
using SteamKit2;

namespace autotrade.CustomElements.Controls.Trade
{
    public partial class TradeSendControl : UserControl
    {
        public TradeSendControl()
        {
            InitializeComponent();

            var settings = SavedSettings.Get();
            InventoryAppIdComboBox.Text = settings.TRADE_INVENTORY_APP_ID;
            InventoryContextIdComboBox.Text = settings.TRADE_INVENTORY_CONTEX_ID;
            TradeParthenIdTextBox.Text = settings.TRADE_PARTNER_ID;
            TradeTokenTextBox.Text = settings.TRADE_TOKEN;

            foreach (var acc in SavedSteamAccount.Get().OrderBy(x => x.Login))
                LoadedAccountCombobox.AddItem(acc.Login,
                    ImageUtils.GetSteamProfileSmallImage(acc.MaFile.Session.SteamID));
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
                AllSteamItemsToTradeGridView.Rows.Clear();
                ItemsToTradeGridView.Rows.Clear();
            });

            Program.LoadingForm.InitInventoryLoadingProcess();
            var allItemsList = Program.LoadingForm.GetLoadedItems();
            Program.LoadingForm.DeactivateForm();

            allItemsList.RemoveAll(item => item.Description.IsTradable == false);

            Dispatcher.AsMainForm(() =>
            {
                AllItemsListGridUtils.FillSteamSaleDataGrid(AllSteamItemsToTradeGridView, allItemsList);
            });
        }

        private void SteamSaleDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            AllItemsListGridUtils.UpdateItemDescription(AllSteamItemsToTradeGridView,
                AllSteamItemsToTradeGridView.CurrentCell.RowIndex, ItemDescriptionTextBox, ItemImageBox, ItemNameLable);

            if (e.ColumnIndex == 3)
            {
                AllItemsListGridUtils.GridComboBoxClick(AllSteamItemsToTradeGridView, e.RowIndex);
            }
            else if (e.ColumnIndex == 4)
            {
                AllItemsListGridUtils.GridAddButtonClick(AllSteamItemsToTradeGridView, e.RowIndex,
                    ItemsToTradeGridView);
                PriceLoader.StartPriceLoading(TableToLoad.ItemsToSaleTable);
            }
        }

        private void AllSteamItemsGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            var cell = AllSteamItemsToTradeGridView.CurrentCell;
            if (cell == null) return;

            var row = cell.RowIndex;
            if (row < 0) return;

            var rowIndex = 0;
            if (AllSteamItemsToTradeGridView.SelectedRows.Count > 1)
                rowIndex = AllSteamItemsToTradeGridView
                    .SelectedRows[AllSteamItemsToTradeGridView.SelectedRows.Count - 1].Cells[0].RowIndex;
            else
                rowIndex = AllSteamItemsToTradeGridView.CurrentCell.RowIndex;
            AllItemsListGridUtils.UpdateItemDescription(AllSteamItemsToTradeGridView, rowIndex, ItemDescriptionTextBox,
                ItemImageBox, ItemNameLable);
            var list = AllItemsListGridUtils.GetFullRgItems(AllSteamItemsToTradeGridView, rowIndex);
            if (list != null && list.Count > 0) LastSelectedItemDescription = list[0].Description;
        }

        private void ItemsToSaleGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            var cell = ItemsToTradeGridView.CurrentCell;
            if (cell == null) return;

            var row = cell.RowIndex;
            if (row < 0) return;

            var rowIndex = 0;
            if (ItemsToTradeGridView.SelectedRows.Count > 1)
                rowIndex = ItemsToTradeGridView.SelectedRows[ItemsToTradeGridView.SelectedRows.Count - 1].Cells[0]
                    .RowIndex;
            else
                rowIndex = row;
            ItemsToSaleGridUtils.RowClick(ItemsToTradeGridView, row, AllDescriptionsDictionary,
                AllSteamItemsToTradeGridView, ItemDescriptionTextBox, ItemImageBox, ItemNameLable);
            var list = ItemsToSaleGridUtils.GetFullRgItems(ItemsToTradeGridView, rowIndex);
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

        private void DeleteAccountButton_Click(object sender, EventArgs e)
        {
            var cell = ItemsToTradeGridView.CurrentCell;
            if (cell == null)
            {
                Logger.Warning("No accounts selected");
                return;
            }

            if (cell.RowIndex < 0) return;

            ItemsToSaleGridUtils.DeleteButtonClick(AllSteamItemsToTradeGridView, ItemsToTradeGridView, cell.RowIndex);
        }

        private void DeleteUnmarketableSteamButton_Click(object sender, EventArgs e)
        {
            ItemsToSaleGridUtils.DeleteUnmarketable(AllSteamItemsToTradeGridView, ItemsToTradeGridView);
        }

        private void DeleteUntradableSteamButton_Click(object sender, EventArgs e)
        {
            ItemsToSaleGridUtils.DeleteUntradable(AllSteamItemsToTradeGridView, ItemsToTradeGridView);
        }

        private void ItemsToSaleGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ItemsToTradeGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
            Logger.Debug($"Inventory {appid} - {contextId} loading started");

            Task.Run(() =>
            {
                LoadInventory(CurrentSession.SteamManager.Guard.Session.SteamID.ToString(), appid, contextId);
                Logger.Info($"Inventory {appid} - {contextId} loading finished");
                Dispatcher.AsMainForm(() => { LoadInventoryButton.Enabled = true; });
            });
        }

        private void SendTradeButton_Click(object sender, EventArgs e)
        {
            if (CurrentSession.SteamManager == null)
            {
                MessageBox.Show("You should login first", "Error sending trade offer", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Logger.Error("Error on inventory loading. No logined account found.");
                return;
            }

            if (string.IsNullOrEmpty(TradeParthenIdTextBox.Text) || string.IsNullOrEmpty(TradeTokenTextBox.Text))
            {
                MessageBox.Show("You should chose target parthner first", "Error sending trade offer",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error sending trade offer. No target parthner selected.");
                return;
            }

            var itemsToSale = new List<FullRgItem>();

            for (var i = 0; i < ItemsToTradeGridView.Rows.Count; i++)
            {
                var itemsList = ItemsToSaleGridUtils.GetRowItemsList(ItemsToTradeGridView, i);
                itemsToSale.AddRange(itemsList);
            }

            var response = CurrentSession.SteamManager.SendTradeOffer(itemsToSale, TradeParthenIdTextBox.Text, TradeTokenTextBox.Text);

            MessageBox.Show($@"Trade sent - {response}", @"Trade info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AddAllButton_Click(object sender, EventArgs e)
        {
            AllSteamItemsToTradeGridView.CurrentCellChanged -= AllSteamItemsGridView_CurrentCellChanged;
            ItemsToTradeGridView.CurrentCellChanged -= ItemsToSaleGridView_CurrentCellChanged;

            AllItemsListGridUtils.AddCellListToSale(AllSteamItemsToTradeGridView, ItemsToTradeGridView,
                AllSteamItemsToTradeGridView.SelectedRows.Cast<DataGridViewRow>().ToArray());

            AllSteamItemsToTradeGridView.CurrentCellChanged += AllSteamItemsGridView_CurrentCellChanged;
            ItemsToTradeGridView.CurrentCellChanged += ItemsToSaleGridView_CurrentCellChanged;

            Logger.Debug("All selected items was added to sale list");
        }

        private void RefreshInventoryButton_Click(object sender, EventArgs e)
        {
            if (CurrentSession.SteamManager == null)
            {
                MessageBox.Show("You should login first", "Error inventory loading", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Logger.Warning("Error on inventory loading. No logined account found.");
                return;
            }

            if (string.IsNullOrEmpty(CurrentSession.CurrentInventoryAppId) ||
                string.IsNullOrEmpty(CurrentSession.CurrentInventoryContextId))
            {
                MessageBox.Show("You should load inventory first", "Error inventory loading", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Logger.Warning("Error on inventory loading. No inventory type chosed.");
                return;
            }

            Logger.Debug(
                $"Refreshing ${CurrentSession.CurrentInventoryAppId} - ${CurrentSession.CurrentInventoryContextId} inventory");

            AllSteamItemsToTradeGridView.Rows.Clear();
            ItemsToTradeGridView.Rows.Clear();

            LoadInventory(CurrentSession.SteamManager.Guard.Session.SteamID.ToString(),
                CurrentSession.CurrentInventoryAppId, CurrentSession.CurrentInventoryContextId);
            AllSteamItemsGridView_CurrentCellChanged(null, null);
        }

        private void OpenMarketPageButtonClick_Click(object sender, EventArgs e)
        {
            if (LastSelectedItemDescription == null) return;
            Process.Start("https://" +
                          $"steamcommunity.com/market/listings/{LastSelectedItemDescription.Appid}/" +
                          LastSelectedItemDescription.MarketHashName);
        }

        private void OpenGameInventoryPageButton_Click(object sender, EventArgs e)
        {
            if (LastSelectedItemDescription == null) return;
            Process.Start("https://" +
                          $"steamcommunity.com/profiles/{CurrentSession.SteamManager.Guard.Session.SteamID}" +
                          $"/inventory/#{CurrentSession.CurrentInventoryAppId}_{CurrentSession.CurrentInventoryContextId}");
        }

        private void InventoryContextIdComboBox_TextChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().TRADE_INVENTORY_CONTEX_ID,
                InventoryContextIdComboBox.Text);
        }

        private void InventoryAppIdComboBox_TextChanged(object sender, EventArgs e)
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

            SavedSettings.UpdateField(ref SavedSettings.Get().TRADE_INVENTORY_APP_ID, InventoryAppIdComboBox.Text);
        }

        private void TradeParthenIdTextBox_TextChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().TRADE_PARTNER_ID, TradeParthenIdTextBox.Text);
        }

        private void TradeTokenTextBox_TextChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().TRADE_TOKEN, TradeTokenTextBox.Text);
        }

        private void ItemsToTradeGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (ItemsToTradeGridView.Rows.Count == 1) ItemsToSaleGridView_CurrentCellChanged(sender, e);
        }

        private void AllSteamItemsToTradeGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            var cell = AllSteamItemsToTradeGridView.CurrentCell;
            if (cell == null) return;

            var row = cell.RowIndex;
            if (row < 0) return;

            var rowIndex = 0;
            if (AllSteamItemsToTradeGridView.SelectedRows.Count > 1)
                rowIndex = AllSteamItemsToTradeGridView
                    .SelectedRows[AllSteamItemsToTradeGridView.SelectedRows.Count - 1].Cells[0].RowIndex;
            else
                rowIndex = AllSteamItemsToTradeGridView.CurrentCell.RowIndex;
            AllItemsListGridUtils.UpdateItemDescription(AllSteamItemsToTradeGridView, rowIndex, ItemDescriptionTextBox,
                ItemImageBox, ItemNameLable);
            var list = AllItemsListGridUtils.GetFullRgItems(AllSteamItemsToTradeGridView, rowIndex);
            if (list != null && list.Count > 0) LastSelectedItemDescription = list[0].Description;
        }

        private void AllSteamItemsToTradeGridView_EditingControlShowing(object sender,
            DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboBox cb)
            {
                cb.IntegralHeight = false;
                cb.MaxDropDownItems = 10;
            }
        }

        private void AllSteamItemsToTradeGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            AllItemsListGridUtils.UpdateItemDescription(AllSteamItemsToTradeGridView,
                AllSteamItemsToTradeGridView.CurrentCell.RowIndex, ItemDescriptionTextBox, ItemImageBox, ItemNameLable);

            if (e.ColumnIndex == 5)
            {
                AllItemsListGridUtils.GridComboBoxClick(AllSteamItemsToTradeGridView, e.RowIndex);
            }
            else if (e.ColumnIndex == 6)
            {
                AllItemsListGridUtils.GridAddButtonClick(AllSteamItemsToTradeGridView, e.RowIndex,
                    ItemsToTradeGridView);
                PriceLoader.StartPriceLoading(TableToLoad.ItemsToSaleTable);
            }
        }

        private void ComboboxWithImage1_MeasureItem_1(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 36;
        }

        private void ComboboxWithImage1_DrawItem(object sender, DrawItemEventArgs e)
        {
            var box = sender as ComboboxWithImage;

            if (box is null) return;

            e.DrawBackground();

            if (e.Index >= 0)
            {
                var g = e.Graphics;

                using (Brush brush = (e.State & DrawItemState.Selected) == DrawItemState.Selected
                    ? new SolidBrush(SystemColors.Highlight)
                    : new SolidBrush(e.BackColor))
                {
                    using (Brush textBrush = new SolidBrush(e.ForeColor))
                    {
                        g.FillRectangle(brush, e.Bounds);
                        var image = box.GetImageByIndex(e.Index);

                        g.DrawString(box.Items[e.Index].ToString(),
                            e.Font,
                            textBrush,
                            e.Bounds.Left + 32, e.Bounds.Top + 10,
                            StringFormat.GenericDefault);

                        g.DrawImage(image, e.Bounds.Left, e.Bounds.Top + 2, 32, 32);
                    }
                }
            }

            e.DrawFocusRectangle();
        }

        private void LoadedAccountCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var login = LoadedAccountCombobox.Text;
            var acc = SavedSteamAccount.Get().FirstOrDefault(x => x.Login == login);
            if (acc == null) return;

            TradeParthenIdTextBox.Text = new SteamID(acc.MaFile.Session.SteamID).AccountID.ToString();
            TradeTokenTextBox.Text = "todo";
        }
    }
}