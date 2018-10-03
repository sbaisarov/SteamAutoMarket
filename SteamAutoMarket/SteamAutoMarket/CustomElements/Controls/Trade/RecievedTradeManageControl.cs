namespace SteamAutoMarket.CustomElements.Controls.Trade
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using SteamAutoMarket.CustomElements.Utils;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.Utils;
    using SteamAutoMarket.WorkingProcess;
    using SteamAutoMarket.WorkingProcess.Settings;

    using SteamKit2;

    public partial class ReceivedTradeManageControl : UserControl
    {
        private readonly Dictionary<string, FullTradeOffer> allTrades = new Dictionary<string, FullTradeOffer>();

        private AssetDescription selectedItemDescription;

        private string selectedOfferId;

        public ReceivedTradeManageControl()
        {
            try
            {
                this.InitializeComponent();

                var settings = SavedSettings.Get();
                this.SentOffersCheckBox.Checked = settings.ActiveTradesSent;
                this.ReceivedOffersCheckBox.Checked = settings.ActiveTradesReceived;
                this.ActiveOnlyCheckBox.Checked = settings.ActiveTradesActiveOnly;
                this.DescriptionLanguageComboBox.Text = settings.ActiveTradesLanguage;
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

        private static void UpdateItemDescriptionTextBox(
            RichTextBox textBox,
            Control label,
            AssetDescription description)
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

            if (string.IsNullOrWhiteSpace(tagsText))
            {
                return;
            }

            textBox.AppendText("\n");
            CommonUtils.AppendBoldText(textBox, "Tags: ");
            textBox.AppendText(tagsText);
        }

        private void LoadInventoryButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (CurrentSession.SteamManager == null)
                {
                    MessageBox.Show(
                        @"You should login first",
                        @"Error trades loading",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Logger.Error("Error on trades history loading. No logged account found.");
                    return;
                }

                var sentOffers = this.SentOffersCheckBox.Checked;
                var receivedOffers = this.ReceivedOffersCheckBox.Checked;

                if (!sentOffers && !receivedOffers)
                {
                    MessageBox.Show(
                        @"You should select at least one type of offers to load (Received/Sent)",
                        @"Error trades loading",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Logger.Error("Error on trades history loading. No trades type selected.");
                    return;
                }

                CommonUtils.ClearGrids(
                    this.CurrentTradesGridView,
                    this.MyItemsGridView,
                    this.HisItemsGridView,
                    this.ExtraTradeInfoGridView);
                this.selectedItemDescription = null;
                this.selectedOfferId = null;
                this.LoadTradesButton.Enabled = false;
                var activeOnly = this.ActiveOnlyCheckBox.Checked;

                var language = this.DescriptionLanguageComboBox.Text ?? "en_US";

                Task.Run(
                    () =>
                        {
                            this.allTrades.Clear();
                            Program.LoadingForm.InitCurrentTradesLoadingProcess(
                                sentOffers,
                                receivedOffers,
                                activeOnly,
                                language);
                            var fullTradeOffers = Program.LoadingForm.GetLoadedCurrentTrades();
                            Program.LoadingForm.DeactivateForm();

                            foreach (var trade in fullTradeOffers)
                            {
                                this.allTrades.Add(trade.Offer.TradeOfferId, trade);

                                Dispatcher.AsMainForm(
                                    () =>
                                        {
                                            var steamId = new SteamID(
                                                (uint)trade.Offer.AccountIdOther,
                                                EUniverse.Public,
                                                EAccountType.Individual).ConvertToUInt64();

                                            var state = trade.Offer.TradeOfferState.ToString().Replace(
                                                "TradeOfferState",
                                                string.Empty);

                                            this.CurrentTradesGridView.Rows.Add(
                                                trade.Offer.TradeOfferId,
                                                steamId,
                                                state);
                                        });
                            }

                            Dispatcher.AsMainForm(() => { this.LoadTradesButton.Enabled = true; });
                        });
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
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
            try
            {
                CommonUtils.ClearGrids(this.MyItemsGridView, this.HisItemsGridView, this.ExtraTradeInfoGridView);
                var tradeOffer = this.allTrades[this.selectedOfferId];

                if (tradeOffer.ItemsToGive != null)
                {
                    var gropedItems = tradeOffer.ItemsToGive.GroupBy(item => item.Description.MarketHashName);
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

                if (tradeOffer.ItemsToReceive != null)
                {
                    var gropedItems = tradeOffer.ItemsToReceive.GroupBy(item => item.Description.MarketHashName);
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

                this.ExtraTradeInfoGridView.Rows.Add("TradeOfferId", tradeOffer.Offer.TradeOfferId);
                this.ExtraTradeInfoGridView.Rows.Add(
                    "TradeOfferState",
                    tradeOffer.Offer.TradeOfferState.ToString().Replace("TradeOfferState", string.Empty));
                this.ExtraTradeInfoGridView.Rows.Add("Message", tradeOffer.Offer.Message);
                this.ExtraTradeInfoGridView.Rows.Add("IsOurOffer", tradeOffer.Offer.IsOurOffer.ToString());
                this.ExtraTradeInfoGridView.Rows.Add("AccountIdOther", tradeOffer.Offer.AccountIdOther.ToString());

                var expirationTime = CommonUtils.ParseSteamUnixDate(tradeOffer.Offer.ExpirationTime)
                    .ToString(CultureInfo.InvariantCulture);
                this.ExtraTradeInfoGridView.Rows.Add("ExpirationTime", expirationTime);

                this.ExtraTradeInfoGridView.Rows.Add(
                    "ConfirmationMethod",
                    tradeOffer.Offer.EConfirmationMethod.ToString().Replace("TradeOfferConfirmation", string.Empty));

                var createdTime = CommonUtils.ParseSteamUnixDate(tradeOffer.Offer.TimeCreated)
                    .ToString(CultureInfo.InvariantCulture);

                this.ExtraTradeInfoGridView.Rows.Add("TimeCreated", createdTime);

                var updatedTime = CommonUtils.ParseSteamUnixDate(tradeOffer.Offer.TimeUpdated)
                    .ToString(CultureInfo.InvariantCulture);
                this.ExtraTradeInfoGridView.Rows.Add("TimeUpdated", updatedTime);

                this.ExtraTradeInfoGridView.Rows.Add("EscrowEndDate", tradeOffer.Offer.EscrowEndDate.ToString());
                this.ExtraTradeInfoGridView.Rows.Add(
                    "FromRealTimeTrade",
                    tradeOffer.Offer.FromRealTimeTrade.ToString());
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void CurrentTradesGridViewCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                {
                    return;
                }

                if (e.ColumnIndex != 1)
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
                if (!(sender is DataGridView grid) || grid.Rows.Count != 1)
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

        private void DescriptionLanguageComboBoxTextChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(
                    ref SavedSettings.Get().ActiveTradesLanguage,
                    this.DescriptionLanguageComboBox.Text);
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
                SavedSettings.UpdateField(ref SavedSettings.Get().ActiveTradesSent, this.SentOffersCheckBox.Checked);
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
                SavedSettings.UpdateField(
                    ref SavedSettings.Get().ActiveTradesReceived,
                    this.ReceivedOffersCheckBox.Checked);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void ActiveOnlyCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(
                    ref SavedSettings.Get().ActiveTradesActiveOnly,
                    this.ActiveOnlyCheckBox.Checked);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void AcceptTradeButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (this.selectedOfferId == null)
                {
                    return;
                }

                string text;
                MessageBoxIcon icon;
                try
                {
                    var response = CurrentSession.SteamManager.OfferSession.Accept(this.selectedOfferId);

                    text = $"Accept state - {response.Accepted}.";
                    if (!string.IsNullOrEmpty(response.TradeError))
                    {
                        text += $"Error - {response.TradeError}";
                    }

                    icon = MessageBoxIcon.Information;
                }
                catch (Exception ex)
                {
                    text = ex.Message;
                    icon = MessageBoxIcon.Error;
                }

                MessageBox.Show(text, @"Trade info", MessageBoxButtons.OK, icon);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void DeclineTradeButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (this.selectedOfferId == null)
                {
                    return;
                }

                string text;
                MessageBoxIcon icon;
                try
                {
                    var response = CurrentSession.SteamManager.OfferSession.Decline(this.selectedOfferId);

                    text = $"Decline state - {response}.";
                    icon = MessageBoxIcon.Information;
                }
                catch (Exception ex)
                {
                    text = ex.Message;
                    icon = MessageBoxIcon.Error;
                }

                MessageBox.Show(text, @"Trade info", MessageBoxButtons.OK, icon);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }
    }
}