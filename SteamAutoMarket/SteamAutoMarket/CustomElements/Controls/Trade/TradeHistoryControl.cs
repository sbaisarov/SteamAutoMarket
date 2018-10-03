namespace SteamAutoMarket.CustomElements.Controls.Trade
{
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

    public partial class TradeHistoryControl : UserControl
    {
        private readonly Dictionary<string, FullHistoryTradeOffer> allTrades =
            new Dictionary<string, FullHistoryTradeOffer>();

        private AssetDescription selectedItemDescription;

        private string selectedOfferId;

        public TradeHistoryControl()
        {
            try
            {
                this.InitializeComponent();

                var settings = SavedSettings.Get();
                this.TradeIdComboBox.Text = settings.TradeHistoryTradeId;
                this.MaxTradesNumericUpDown.Value = settings.TradeHistoryMaxTrades;
                this.LanguageComboBox.Text = settings.TradeHistoryLanguage;
                this.NavigatingBackCheckBox.Checked = settings.TradeHistoryNavigatingBack;
                this.IncludeFailedCheckBox.Checked = settings.TradeHistoryIncludeFailed;
                this.ReceivedOffersCheckBox.Checked = settings.TradeHistoryReceived;
                this.SentOffersCheckBox.Checked = settings.TradeHistorySent;
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

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

        private static void UpdateItemDescriptionTextBox(RichTextBox textBox, Label label, AssetDescription description)
        {
            textBox.Clear();

            label.Text = description.Name;

            var descriptionText = string.Empty;
            if (description.Descriptions != null)
            {
                foreach (var item in description.Descriptions)
                {
                    var text = item.Value.Trim();
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        descriptionText += text + ", ";
                    }
                }

                if (descriptionText.EndsWith(", "))
                {
                    descriptionText = descriptionText.Substring(0, descriptionText.Length - 2);
                }
            }

            var tagsText = string.Empty;

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

        private void LoadInventoryButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (CurrentSession.SteamManager == null)
                {
                    MessageBox.Show(
                        @"You should login first",
                        @"Error trades history loading",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Logger.Error("Error on trades history loading. No signed in account found.");
                    return;
                }

                var sentOffers = this.SentOffersCheckBox.Checked;
                var receivedOffers = this.ReceivedOffersCheckBox.Checked;

                if (!sentOffers && !receivedOffers)
                {
                    MessageBox.Show(
                        @"You should select at least one type of offers to load (Received/Sent)",
                        @"Error trades history loading",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Logger.Error("Error on trades history loading. No trades type selected.");
                    return;
                }

                var startTime =
                    CommonUtils.GetSecondsFromDateTime(CommonUtils.ResetTimeToDauStart(this.SteamDateTimePicker.Value));
                var startTradeId = this.TradeIdComboBox.Text;
                var maxTrades = (int)this.MaxTradesNumericUpDown.Value;
                var navigatingBack = this.NavigatingBackCheckBox.Checked;
                var includeFailed = this.IncludeFailedCheckBox.Checked;
                var language = this.LanguageComboBox.Text ?? "en_US";

                CommonUtils.ClearGrids(
                    this.CurrentTradesGridView,
                    this.MyItemsGridView,
                    this.HisItemsGridView,
                    this.ExtraTradeInfoGridView);

                Task.Run(
                    () =>
                        {
                            this.allTrades.Clear();

                            Program.LoadingForm.InitTradesHistoryLoadingProcess(
                                maxTrades,
                                startTime,
                                startTradeId,
                                navigatingBack,
                                true,
                                language,
                                includeFailed);
                            var fullTradeOffers = Program.LoadingForm.GetLoadedTradesHistory();
                            Program.LoadingForm.DeactivateForm();

                            Dispatcher.AsMainForm(
                                () =>
                                    {
                                        this.TradeIdComboBox.Items.Clear();
                                        foreach (var trade in fullTradeOffers)
                                        {
                                            this.allTrades.Add(trade.TradeId, trade);

                                            this.CurrentTradesGridView.Rows.Add(
                                                trade.TradeId,
                                                trade.SteamIdOther.ConvertToUInt64(),
                                                trade.Status.ToString().Replace("TradeState", string.Empty));

                                            this.TradeIdComboBox.Items.Add(trade.TradeId);
                                        }

                                        this.LoadTradesButton.Enabled = true;
                                    });
                        });
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
                this.LoadTradesButton.Enabled = true;
            }
        }

        private void CurrentTradesGridViewSelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentTradesGridView.SelectedCells.Count == 0)
                {
                    return;
                }

                var index = this.CurrentTradesGridView.SelectedCells[0].RowIndex;
                if (index < 0)
                {
                    return;
                }

                var selected = (string)this.CurrentTradesGridView.Rows[index].Cells[0].Value;
                if (selected == this.selectedOfferId)
                {
                    return;
                }

                this.selectedOfferId = selected;
                this.ChangeSelectedTrade();
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void ChangeSelectedTrade()
        {
            CommonUtils.ClearGrids(this.MyItemsGridView, this.HisItemsGridView, this.ExtraTradeInfoGridView);
            var tradeOffer = this.allTrades[this.selectedOfferId];

            if (tradeOffer.MyItems != null)
            {
                var gropedItems = tradeOffer.MyItems.GroupBy(item => item.Description.MarketHashName);
                foreach (var groupedList in gropedItems)
                {
                    var firstItem = groupedList.First();
                    this.MyItemsGridView.Rows.Add(
                        firstItem.Description.Name,
                        groupedList.Sum(x => int.Parse(x.Asset.Amount)),
                        SteamItemsUtils.GetClearType(firstItem),
                        firstItem.Description);
                }
            }

            if (tradeOffer.HisItems != null)
            {
                var gropedItems = tradeOffer.HisItems.GroupBy(item => item.Description.MarketHashName);
                foreach (var groupedList in gropedItems)
                {
                    var firstItem = groupedList.First();
                    this.HisItemsGridView.Rows.Add(
                        firstItem.Description.Name,
                        groupedList.Sum(x => int.Parse(x.Asset.Amount)),
                        SteamItemsUtils.GetClearType(firstItem),
                        firstItem.Description);
                }
            }

            this.ExtraTradeInfoGridView.Rows.Add("TradeId", tradeOffer.TradeId);
            this.ExtraTradeInfoGridView.Rows.Add(
                "TradeOfferState",
                tradeOffer.Status.ToString().Replace("TradeState", string.Empty));
            this.ExtraTradeInfoGridView.Rows.Add("SteamIdOtherIdOther", tradeOffer.SteamIdOther);
            this.ExtraTradeInfoGridView.Rows.Add("TimeInit", tradeOffer.TimeInit);
            this.ExtraTradeInfoGridView.Rows.Add("TimeEscrowEnd", tradeOffer.TimeEscrowEnd);
        }

        private void CurrentTradesGridViewCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.ColumnIndex != 1)
                {
                    return;
                }

                var cell = this.CurrentTradesGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                var steamId = cell?.Value;
                if (steamId == null)
                {
                    return;
                }

                Process.Start($"https://steamcommunity.com/profiles/{steamId}/");
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void ItemsGridViewSelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is DataGridView grid))
                {
                    return;
                }

                var selectedRows = grid.SelectedRows;
                if (selectedRows.Count == 0)
                {
                    return;
                }

                var index = selectedRows[0].Index;
                var description = (AssetDescription)grid.Rows[index].Cells[3].Value;
                if (description.Equals(this.selectedItemDescription))
                {
                    return;
                }

                this.selectedItemDescription = description;
                this.ChangeSelectedItem();
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void ChangeSelectedItem()
        {
            ImageUtils.UpdateItemImageOnPanelAsync(this.selectedItemDescription, this.ItemImageBox);
            UpdateItemDescriptionTextBox(this.ItemDescriptionTextBox, this.ItemNameLable, this.selectedItemDescription);
        }

        private void ItemsGridViewCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!(sender is DataGridView grid))
                {
                    return;
                }

                var selectedRows = grid.SelectedRows;
                if (selectedRows.Count == 0)
                {
                    return;
                }

                var index = selectedRows[0].Index;
                var description = (AssetDescription)grid.Rows[index].Cells[3].Value;
                if (description.Equals(this.selectedItemDescription))
                {
                    return;
                }

                this.selectedItemDescription = description;
                this.ChangeSelectedItem();
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void SentOffersCheckBoxCheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(ref SavedSettings.Get().TradeHistorySent, this.SentOffersCheckBox.Checked);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void ReceivedOffersCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(ref SavedSettings.Get().TradeHistoryReceived, this.ReceivedOffersCheckBox.Checked);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void TradeIdComboBoxTextChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(ref SavedSettings.Get().TradeHistoryTradeId, this.TradeIdComboBox.Text);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void MaxTradesNumericUpDownValueChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(
                    ref SavedSettings.Get().TradeHistoryMaxTrades,
                    (int)this.MaxTradesNumericUpDown.Value);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void LanguageComboBoxTextChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(ref SavedSettings.Get().TradeHistoryLanguage, this.LanguageComboBox.Text);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void NavigatingBackCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(
                    ref SavedSettings.Get().TradeHistoryNavigatingBack,
                    this.NavigatingBackCheckBox.Checked);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void IncludeFailedCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(
                    ref SavedSettings.Get().TradeHistoryIncludeFailed,
                    this.IncludeFailedCheckBox.Checked);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }
    }
}