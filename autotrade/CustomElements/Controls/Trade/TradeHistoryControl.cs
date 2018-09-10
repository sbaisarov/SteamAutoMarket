using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteamAutoMarket.CustomElements.Utils;
using SteamAutoMarket.Steam.TradeOffer.Models;
using SteamAutoMarket.Steam.TradeOffer.Models.Full;
using SteamAutoMarket.Utils;
using SteamAutoMarket.WorkingProcess;
using SteamAutoMarket.WorkingProcess.Settings;

namespace SteamAutoMarket.CustomElements.Controls.Trade
{
    public partial class TradeHistoryControl : UserControl
    {
        private readonly Dictionary<string, FullHistoryTradeOffer> _allTrades =
            new Dictionary<string, FullHistoryTradeOffer>();

        private AssetDescription _selectedItemDescription;
        private string _selectedOfferId;

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
                MessageBox.Show(@"You should login first", @"Error trades history loading", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Logger.Error("Error on trades history loading. No logged account found.");
                return;
            }

            var sentOffers = SentOffersCheckBox.Checked;
            var receivedOffers = ReceivedOffersCheckBox.Checked;

            if (!sentOffers && !receivedOffers)
            {
                MessageBox.Show(@"You should select at least one type of offers to load (Received/Sent)",
                    @"Error trades history loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error on trades history loading. No trades type selected.");
                return;
            }

            var startTime = CommonUtils.GetSecondsFromDateTime(CommonUtils.ResetTimeToDauStart(DateTimePicker.Value));
            var startTradeId = TradeIdComboBox.Text;
            var maxTrades = (int)MaxTradesNumericUpDown.Value;
            var navigatingBack = NavigatingBackCheckBox.Checked;
            var includeFailed = IncludeFailedCheckBox.Checked;
            var language = LanguageComboBox.Text;
            if (language == null) language = "en_US";

            CommonUtils.ClearGrids(CurrentTradesGridView, MyItemsGridView, HisItemsGridView, ExtraTradeInfoGridView);

            Task.Run(() =>
            {
                _allTrades.Clear();

                Program.LoadingForm.InitTradesHistoryLoadingProcess(maxTrades, startTime, startTradeId, navigatingBack,
                    true, language, includeFailed);
                var fullTradeOffers = Program.LoadingForm.GetLoadedTradesHistory();
                Program.LoadingForm.DeactivateForm();

                Dispatcher.AsMainForm(() =>
                {
                    TradeIdComboBox.Items.Clear();
                    foreach (var trade in fullTradeOffers)
                    {
                        _allTrades.Add(trade.TradeId, trade);

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

            var selected = (string)CurrentTradesGridView.Rows[index].Cells[0].Value;
            if (selected == _selectedOfferId) return;

            _selectedOfferId = selected;
            ChangeSelectedTrade();
        }

        private void ChangeSelectedTrade()
        {
            CommonUtils.ClearGrids(MyItemsGridView, HisItemsGridView, ExtraTradeInfoGridView);
            var tradeOffer = _allTrades[_selectedOfferId];

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

                var steamId = cell?.Value;
                if (steamId == null) return;

                Process.Start($"https://steamcommunity.com/profiles/{steamId}/");
            }
        }

        private void ItemsGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!(sender is DataGridView grid)) return;
            var selectedRows = grid.SelectedRows;
            if (selectedRows.Count == 0) return;

            var index = selectedRows[0].Index;
            var description = (AssetDescription)grid.Rows[index].Cells[3].Value;
            if (description.Equals(_selectedItemDescription)) return;
            _selectedItemDescription = description;
            ChangeSelectedItem();
        }

        private void ChangeSelectedItem()
        {
            ImageUtils.UpdateItemImageOnPanelAsync(_selectedItemDescription, ItemImageBox);
            UpdateItemDescriptionTextBox(ItemDescriptionTextBox, ItemNameLable, _selectedItemDescription);
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
            if (!(sender is DataGridView grid) || grid.Rows.Count != 1) return;

            var selectedRows = grid.SelectedRows;
            if (selectedRows.Count == 0) return;

            var index = selectedRows[0].Index;
            var description = (AssetDescription)grid.Rows[index].Cells[3].Value;
            if (!description.Equals(_selectedItemDescription))
            {
                _selectedItemDescription = description;
                ChangeSelectedItem();
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
                (int)MaxTradesNumericUpDown.Value);
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