namespace SteamAutoMarket.CustomElements.Controls.Trade
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using SteamAutoMarket.CustomElements.Elements;
    using SteamAutoMarket.CustomElements.Utils;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.Utils;
    using SteamAutoMarket.WorkingProcess;
    using SteamAutoMarket.WorkingProcess.PriceLoader;
    using SteamAutoMarket.WorkingProcess.Settings;

    using SteamKit2;

    public partial class TradeSendControl : UserControl
    {
        private readonly AllItemsListGridUtils allItemsListGridUtils;

        public TradeSendControl()
        {
            try
            {
                this.InitializeComponent();
                this.allItemsListGridUtils = new AllItemsListGridUtils(this.AllSteamItemsToTradeGridView);
                var settings = SavedSettings.Get();
                this.InventoryAppIdComboBox.Text = settings.TradeInventoryAppId;
                this.InventoryContextIdComboBox.Text = settings.TradeInventoryContexId;
                this.TradeParthenIdTextBox.Text = settings.TradePartnerId;
                this.TradeTokenTextBox.Text = settings.TradeToken;

                this.LoadSavedAccountToSendTargetComboBox();
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
            this.AccountNameLable.Text = CurrentSession.SteamManager.Guard.AccountName;
            this.SplitterPanel.BackgroundImage = CurrentSession.AccountImage;
        }

        public void LoadInventory(string steamId, string appid, string contextId)
        {
            Dispatcher.AsMainForm(
                () =>
                    {
                        this.AllSteamItemsToTradeGridView.Rows.Clear();
                        this.ItemsToTradeGridView.Rows.Clear();
                    });

            Program.LoadingForm.InitInventoryLoadingProcess();
            var allItemsList = Program.LoadingForm.GetLoadedItems();
            Program.LoadingForm.DeactivateForm();

            allItemsList.RemoveAll(item => item.Description.IsTradable == false);

            if (this.OnlyUnmarketableCheckBox.Checked)
            {
                allItemsList.RemoveAll(item => item.Description.IsMarketable);
            }

            Dispatcher.AsMainForm(() => { this.allItemsListGridUtils.FillSteamSaleDataGrid(allItemsList); });
        }

        private void LoadSavedAccountToSendTargetComboBox()
        {
            foreach (var acc in SavedSteamAccount.Get().OrderBy(x => x.Login))
            {
                this.LoadedAccountCombobox.AddItem(
                    acc.Login,
                    ImageUtils.GetSteamProfileSmallImage(acc.MaFile.Session.SteamID));
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

        private void AllSteamItemsGridViewCurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                var cell = this.AllSteamItemsToTradeGridView.CurrentCell;
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
                if (this.AllSteamItemsToTradeGridView.SelectedRows.Count > 1)
                {
                    rowIndex = this.AllSteamItemsToTradeGridView
                        .SelectedRows[this.AllSteamItemsToTradeGridView.SelectedRows.Count - 1].Cells[0].RowIndex;
                }
                else
                {
                    rowIndex = this.AllSteamItemsToTradeGridView.CurrentCell.RowIndex;
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
                Logger.Critical(ex);
            }
        }

        private void ItemsToSaleGridViewCurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                var cell = this.ItemsToTradeGridView.CurrentCell;
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
                if (this.ItemsToTradeGridView.SelectedRows.Count > 1)
                {
                    rowIndex = this.ItemsToTradeGridView.SelectedRows[this.ItemsToTradeGridView.SelectedRows.Count - 1]
                        .Cells[0].RowIndex;
                }
                else
                {
                    rowIndex = row;
                }

                ItemsToSaleGridUtils.RowClick(
                    this.ItemsToTradeGridView,
                    row,
                    AllDescriptionsDictionary,
                    this.AllSteamItemsToTradeGridView,
                    this.ItemDescriptionTextBox,
                    this.ItemImageBox,
                    this.ItemNameLable);

                var list = ItemsToSaleGridUtils.GetFullRgItems(this.ItemsToTradeGridView, rowIndex);
                if (list != null && list.Count > 0)
                {
                    LastSelectedItemDescription = list[0].Description;
                }
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void DeleteAccountButtonClick(object sender, EventArgs e)
        {
            try
            {
                var cell = this.ItemsToTradeGridView.CurrentCell;
                if (cell == null)
                {
                    Logger.Warning("No accounts selected");
                    return;
                }

                if (cell.RowIndex < 0)
                {
                    return;
                }

                ItemsToSaleGridUtils.DeleteButtonClick(
                    this.AllSteamItemsToTradeGridView,
                    this.ItemsToTradeGridView,
                    cell.RowIndex);
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
                this.ItemsToTradeGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
                    Logger.Error("Error on inventory loading. No inventory type chosen.");
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
                        });
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void SendTradeButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (CurrentSession.SteamManager == null)
                {
                    MessageBox.Show(
                        @"You should login first",
                        @"Error sending trade offer",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Logger.Error("Error on inventory loading. No signed in account found.");
                    return;
                }

                if (string.IsNullOrEmpty(this.TradeParthenIdTextBox.Text)
                    || string.IsNullOrEmpty(this.TradeTokenTextBox.Text))
                {
                    MessageBox.Show(
                        @"You should chose target partner first",
                        @"Error sending trade offer",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Logger.Error("Error sending trade offer. No target partner selected.");
                    return;
                }

                var itemsToSale = new List<FullRgItem>();

                for (var i = 0; i < this.ItemsToTradeGridView.Rows.Count; i++)
                {
                    var itemsList = ItemsToSaleGridUtils.GetRowItemsList(this.ItemsToTradeGridView, i);
                    itemsToSale.AddRange(itemsList);
                }

                var response = CurrentSession.SteamManager.SendTradeOffer(
                    itemsToSale,
                    this.TradeParthenIdTextBox.Text,
                    this.TradeTokenTextBox.Text,
                    out var offerId);

                if (this.Confirm2FACheckBox.Checked && response == true)
                {
                    CurrentSession.SteamManager.ConfirmTradeTransactions(new List<ulong> { ulong.Parse(offerId) });
                }

                MessageBox.Show(
                    $@"Trade sent - {response}",
                    @"Trade info",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void AddAllButtonClick(object sender, EventArgs e)
        {
            try
            {
                this.AllSteamItemsToTradeGridView.CurrentCellChanged -= this.AllSteamItemsGridViewCurrentCellChanged;
                this.ItemsToTradeGridView.CurrentCellChanged -= this.ItemsToSaleGridViewCurrentCellChanged;

                this.allItemsListGridUtils.AddCellListToSale(
                    this.ItemsToTradeGridView,
                    this.AllSteamItemsToTradeGridView.SelectedRows.Cast<DataGridViewRow>().ToArray());

                this.AllSteamItemsToTradeGridView.CurrentCellChanged += this.AllSteamItemsGridViewCurrentCellChanged;
                this.ItemsToTradeGridView.CurrentCellChanged += this.ItemsToSaleGridViewCurrentCellChanged;

                Logger.Debug("All selected items was added to sale list");
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void RefreshInventoryButtonClick(object sender, EventArgs e)
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
                    Logger.Warning("Error on inventory loading. No signed account found.");
                    return;
                }

                if (string.IsNullOrEmpty(CurrentSession.CurrentInventoryAppId)
                    || string.IsNullOrEmpty(CurrentSession.CurrentInventoryContextId))
                {
                    MessageBox.Show(
                        @"You should load inventory first",
                        @"Error inventory loading",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Logger.Warning("Error on inventory loading. No inventory type chosen.");
                    return;
                }

                Logger.Debug(
                    $"Refreshing ${CurrentSession.CurrentInventoryAppId} - ${CurrentSession.CurrentInventoryContextId} inventory");

                this.AllSteamItemsToTradeGridView.Rows.Clear();
                this.ItemsToTradeGridView.Rows.Clear();

                this.LoadInventory(
                    CurrentSession.SteamManager.Guard.Session.SteamID.ToString(),
                    CurrentSession.CurrentInventoryAppId,
                    CurrentSession.CurrentInventoryContextId);
                this.AllSteamItemsGridViewCurrentCellChanged(null, null);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
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

        private void OpenGameInventoryPageButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (LastSelectedItemDescription == null)
                {
                    return;
                }

                Process.Start(
                    "https://" + $"steamcommunity.com/profiles/{CurrentSession.SteamManager.Guard.Session.SteamID}"
                               + $"/inventory/#{CurrentSession.CurrentInventoryAppId}_{CurrentSession.CurrentInventoryContextId}");
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
                    ref SavedSettings.Get().TradeInventoryContexId,
                    this.InventoryContextIdComboBox.Text);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void InventoryAppIdComboBoxTextChanged(object sender, EventArgs e)
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
                    ref SavedSettings.Get().TradeInventoryAppId,
                    this.InventoryAppIdComboBox.Text);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void TradePartnerIdTextBoxTextChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(ref SavedSettings.Get().TradePartnerId, this.TradeParthenIdTextBox.Text);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void TradeTokenTextBoxTextChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(ref SavedSettings.Get().TradeToken, this.TradeTokenTextBox.Text);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void ItemsToTradeGridViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                if (this.ItemsToTradeGridView.Rows.Count == 1)
                {
                    this.ItemsToSaleGridViewCurrentCellChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void AllSteamItemsToTradeGridViewCurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                var cell = this.AllSteamItemsToTradeGridView.CurrentCell;
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
                if (this.AllSteamItemsToTradeGridView.SelectedRows.Count > 1)
                {
                    rowIndex = this.AllSteamItemsToTradeGridView
                        .SelectedRows[this.AllSteamItemsToTradeGridView.SelectedRows.Count - 1].Cells[0].RowIndex;
                }
                else
                {
                    rowIndex = this.AllSteamItemsToTradeGridView.CurrentCell.RowIndex;
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
                Logger.Critical(ex);
            }
        }

        private void AllSteamItemsToTradeGridViewEditingControlShowing(
            object sender,
            DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (!(e.Control is ComboBox cb))
                {
                    return;
                }

                cb.IntegralHeight = false;
                cb.MaxDropDownItems = 10;
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void AllSteamItemsToTradeGridViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                {
                    return;
                }

                this.allItemsListGridUtils.UpdateItemDescription(
                    this.AllSteamItemsToTradeGridView.CurrentCell.RowIndex,
                    this.ItemDescriptionTextBox,
                    this.ItemImageBox,
                    this.ItemNameLable);

                switch (e.ColumnIndex)
                {
                    case 5:
                        this.allItemsListGridUtils.GridComboBoxClick(e.RowIndex);
                        break;
                    case 6:
                        this.allItemsListGridUtils.GridAddButtonClick(e.RowIndex, this.ItemsToTradeGridView);
                        PriceLoader.StartPriceLoading(ETableToLoad.ItemsToSaleTable);
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void ComboboxWithImageMeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 36;
        }

        private void ComboboxWithImageDrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                var box = sender as ComboboxWithImage;

                if (box is null)
                {
                    return;
                }

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

                            g.DrawString(
                                box.Items[e.Index].ToString(),
                                e.Font,
                                textBrush,
                                e.Bounds.Left + 32,
                                e.Bounds.Top + 10,
                                StringFormat.GenericDefault);

                            g.DrawImage(image, e.Bounds.Left, e.Bounds.Top + 2, 32, 32);
                        }
                    }
                }

                e.DrawFocusRectangle();
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void LoadedAccountComboboxSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var login = this.LoadedAccountCombobox.Text;
                var acc = SavedSteamAccount.Get().FirstOrDefault(x => x.Login == login);
                if (acc == null)
                {
                    return;
                }

                this.TradeParthenIdTextBox.Text = new SteamID(acc.MaFile.Session.SteamID).AccountID.ToString();
                this.TradeTokenTextBox.Text = @"todo"; // todo
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }
    }
}