using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using autotrade.CustomElements.Utils;
using autotrade.Steam.TradeOffer.Models;
using autotrade.Steam.TradeOffer.Models.Full;
using autotrade.Utils;
using autotrade.WorkingProcess;
using autotrade.WorkingProcess.Settings;

namespace autotrade.CustomElements.Controls.Trade
{
    public partial class TradeHistoryControl : UserControl
    {
        private readonly Dictionary<string, FullHistoryTradeOffer> ALL_TRADES =
            new Dictionary<string, FullHistoryTradeOffer>();

        private AssetDescription SELECTED_ITEM_DESCRIPTION;
        private string SELECTED_OFFER_ID;

        public TradeHistoryControl()
        {
            InitializeComponent();

            var settings = SavedSettings.Get();
            TradeIdComboBox.Text = settings.TradeHistoryTradeId;
            MaxTradesNumericUpDown.Value = settings.TradeHistoryMaxTrades;
            LanguageComboBox.Text = settings.TradeHistoryLanguage;
            NavigatingBackCheckBox.Checked = settings.TradeHistoryNavigatingBack;
            IncludeFailedCheckBox.Checked = settings.TradeHistoryIncludeFailed;
            ReceivedOffersCheckBox.Checked = settings.TradeHistoryReceived;
            SentOffersCheckBox.Checked = settings.TradeHistorySent;
        }

        public void AuthCurrentAccount()
        {
            AccountNameLable.Text = CurrentSession.SteamManager.Guard.AccountName;
            SplitterPanel.BackgroundImage = CurrentSession.AccountImage;
        }

        private void LoadInventoryButton_Click(object sender, EventArgs e)
        {
            if (CurrentSession.SteamManager == null)
            {
                MessageBox.Show("You should login first", "Error trades history loading", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Logger.Error("Error on trades history loading. No logined account found.");
                return;
            }

            var sentOffers = SentOffersCheckBox.Checked;
            var ReceivedOffers = ReceivedOffersCheckBox.Checked;

            if (!sentOffers && !ReceivedOffers)
            {
                MessageBox.Show("You should select at least one type of offers to load (Received/Sent)",
                    "Error trades history loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error on trades history loading. No trades type selected.");
                return;
            }

            var startTime = CommonUtils.GetSecondsFromDateTime(CommonUtils.ResetTimeToDauStart(DateTimePicker.Value));
            var startTradeId = TradeIdComboBox.Text;
            var maxTrades = (int) MaxTradesNumericUpDown.Value;
            var navigatingBack = NavigatingBackCheckBox.Checked;
            var includeFailed = IncludeFailedCheckBox.Checked;
            var language = LanguageComboBox.Text;
            if (language == null) language = "en_US";

            CommonUtils.ClearGrids(CurrentTradesGridView, MyItemsGridView, HisItemsGridView, ExtraTradeInfoGridView);

            Task.Run(() =>
            {
                ALL_TRADES.Clear();

                Program.LoadingForm.InitTradesHistoryLoadingProcess(maxTrades, startTime, startTradeId, navigatingBack,
                    true, language, includeFailed);
                var fullTradeOffers = Program.LoadingForm.GetLoadedTradesHistory();
                Program.LoadingForm.DeactivateForm();

                Dispatcher.AsMainForm(() =>
                {
                    TradeIdComboBox.Items.Clear();
                    foreach (var trade in fullTradeOffers)
                    {
                        ALL_TRADES.Add(trade.TradeId, trade);

                        CurrentTradesGridView.Rows.Add(
                            trade.TradeId,
                            trade.SteamIdOther.ConvertToUInt64(),
                            trade.Status.ToString().Replace("TradeState", "")
                        );

                        TradeIdComboBox.Items.Add(trade.TradeId);
                    }

                    LoadTradesButton.Enabled = true;
                });
            });
        }

        private void CurrentTradesGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (CurrentTradesGridView.SelectedCells.Count == 0) return;

            var index = CurrentTradesGridView.SelectedCells[0].RowIndex;
            if (index < 0) return;

            var selected = (string) CurrentTradesGridView.Rows[index].Cells[0].Value;
            if (selected == SELECTED_OFFER_ID) return;

            SELECTED_OFFER_ID = selected;
            ChangeSelectedTrade();
        }

        private void ChangeSelectedTrade()
        {
            CommonUtils.ClearGrids(MyItemsGridView, HisItemsGridView, ExtraTradeInfoGridView);
            var tradeOffer = ALL_TRADES[SELECTED_OFFER_ID];

            if (tradeOffer.MyItems != null)
            {
                var gropedItems = tradeOffer.MyItems.GroupBy(item => item.Description.MarketHashName);
                foreach (var groupedList in gropedItems)
                {
                    var firstItem = groupedList.First();
                    MyItemsGridView.Rows.Add(
                        firstItem.Description.Name,
                        groupedList.Sum(x => int.Parse(x.Asset.Amount)),
                        SteamItemsUtils.GetClearType(firstItem),
                        firstItem.Description
                    );
                }
            }

            if (tradeOffer.HisItems != null)
            {
                var gropedItems = tradeOffer.HisItems.GroupBy(item => item.Description.MarketHashName);
                foreach (var groupedList in gropedItems)
                {
                    var firstItem = groupedList.First();
                    HisItemsGridView.Rows.Add(
                        firstItem.Description.Name,
                        groupedList.Sum(x => int.Parse(x.Asset.Amount)),
                        SteamItemsUtils.GetClearType(firstItem),
                        firstItem.Description
                    );
                }
            }

            ExtraTradeInfoGridView.Rows.Add("TradeId", tradeOffer.TradeId);
            ExtraTradeInfoGridView.Rows.Add("TradeOfferState", tradeOffer.Status.ToString().Replace("TradeState", ""));
            ExtraTradeInfoGridView.Rows.Add("SteamIdOtherIdOther", tradeOffer.SteamIdOther);
            ExtraTradeInfoGridView.Rows.Add("TimeInit", tradeOffer.TimeInit);
            ExtraTradeInfoGridView.Rows.Add("TimeEscrowEnd", tradeOffer.TimeEscrowEnd);
        }

        private void CurrentTradesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (e.ColumnIndex == 1)
            {
                var cell = CurrentTradesGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell == null) return;

                var steamId = cell.Value;
                if (steamId == null) return;

                Process.Start($"https://steamcommunity.com/profiles/{steamId}/");
            }
        }

        private void ItemsGridView_SelectionChanged(object sender, EventArgs e)
        {
            var grid = sender as DataGridView;
            var selectedRows = grid.SelectedRows;
            if (selectedRows.Count == 0) return;

            var index = selectedRows[0].Index;
            var description = (AssetDescription) grid.Rows[index].Cells[3].Value;
            if (!description.Equals(SELECTED_ITEM_DESCRIPTION))
            {
                SELECTED_ITEM_DESCRIPTION = description;
                ChangeSelectedItem();
            }
        }

        private void ChangeSelectedItem()
        {
            ImageUtils.UpdateItemImageOnPanelAsync(SELECTED_ITEM_DESCRIPTION, ItemImageBox);
            UpdateItemDescriptionTextBox(ItemDescriptionTextBox, ItemNameLable, SELECTED_ITEM_DESCRIPTION);
        }

        private void UpdateItemDescriptionTextBox(RichTextBox textBox, Label label, AssetDescription description)
        {
            textBox.Clear();

            label.Text = description.Name;

            var descriptionText = "";
            if (description.Descriptions != null)
            {
                foreach (var item in description.Descriptions)
                {
                    var text = item.Value.Trim();
                    if (!string.IsNullOrWhiteSpace(text)) descriptionText += text + ", ";
                }

                if (descriptionText.EndsWith(", "))
                    descriptionText = descriptionText.Substring(0, descriptionText.Length - 2);
            }

            var tagsText = "";

            CommonUtils.AppendBoldText(textBox, "Game: ");
            textBox.AppendText(description.AppId + "\n");

            CommonUtils.AppendBoldText(textBox, "Name: ");
            textBox.AppendText(description.MarketHashName + "\n");

            CommonUtils.AppendBoldText(textBox, "Type: ");
            textBox.AppendText(description.Type);

            if (!string.IsNullOrWhiteSpace(descriptionText))
            {
                textBox.AppendText("\n");
                CommonUtils.AppendBoldText(textBox, "Description: ");
                textBox.AppendText(descriptionText);
            }

            if (!string.IsNullOrWhiteSpace(tagsText))
            {
                textBox.AppendText("\n");
                CommonUtils.AppendBoldText(textBox, "Tags: ");
                textBox.AppendText(tagsText);
            }
        }

        private void ItemsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid.Rows.Count == 1)
            {
                var selectedRows = grid.SelectedRows;
                if (selectedRows.Count == 0) return;

                var index = selectedRows[0].Index;
                var description = (AssetDescription) grid.Rows[index].Cells[3].Value;
                if (!description.Equals(SELECTED_ITEM_DESCRIPTION))
                {
                    SELECTED_ITEM_DESCRIPTION = description;
                    ChangeSelectedItem();
                }
            }
        }

        private void SentOffersCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().TradeHistorySent, SentOffersCheckBox.Checked);
        }

        private void ReceivedOffersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().TradeHistoryReceived, ReceivedOffersCheckBox.Checked);
        }

        private void TradeIdComboBox_TextChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().TradeHistoryTradeId, TradeIdComboBox.Text);
        }

        private void MaxTradesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().TradeHistoryMaxTrades,
                (int) MaxTradesNumericUpDown.Value);
        }

        private void LanguageComboBox_TextChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().TradeHistoryLanguage, LanguageComboBox.Text);
        }

        private void NavigatingBackCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().TradeHistoryNavigatingBack,
                NavigatingBackCheckBox.Checked);
        }

        private void IncludeFailedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().TradeHistoryIncludeFailed,
                IncludeFailedCheckBox.Checked);
        }
    }
}