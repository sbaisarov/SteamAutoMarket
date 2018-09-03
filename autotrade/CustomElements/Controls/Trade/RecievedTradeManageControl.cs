using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using autotrade.CustomElements.Utils;
using autotrade.Steam.TradeOffer.Models;
using autotrade.Steam.TradeOffer.Models.Full;
using autotrade.Utils;
using autotrade.WorkingProcess;
using autotrade.WorkingProcess.Settings;
using SteamKit2;

namespace autotrade.CustomElements.Controls.Trade
{
    public partial class ReceivedTradeManageControl : UserControl
    {
        private readonly Dictionary<string, FullTradeOffer> _allTrades = new Dictionary<string, FullTradeOffer>();
        private AssetDescription _selectedItemDescription;
        private string _selectedOfferId;

        public ReceivedTradeManageControl()
        {
            InitializeComponent();

            var settings = SavedSettings.Get();
            SentOffersCheckBox.Checked = settings.ActiveTradesSent;
            ReceivedOffersCheckBox.Checked = settings.ActiveTradesReceived;
            ActiveOnlyCheckBox.Checked = settings.ActiveTradesActiveOnly;
            DescriptionLanguageComboBox.Text = settings.ActiveTradesLanguage;
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
                MessageBox.Show(@"You should login first", @"Error trades loading", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Logger.Error("Error on trades history loading. No logged account found.");
                return;
            }

            var sentOffers = SentOffersCheckBox.Checked;
            var receivedOffers = ReceivedOffersCheckBox.Checked;

            if (!sentOffers && !receivedOffers)
            {
                MessageBox.Show(@"You should select at least one type of offers to load (Received/Sent)",
                    @"Error trades loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error on trades history loading. No trades type selected.");
                return;
            }

            CommonUtils.ClearGrids(CurrentTradesGridView, MyItemsGridView, HisItemsGridView, ExtraTradeInfoGridView);
            _selectedItemDescription = null;
            _selectedOfferId = null;
            LoadTradesButton.Enabled = false;
            var activeOnly = ActiveOnlyCheckBox.Checked;

            var language = DescriptionLanguageComboBox.Text;
            if (language == null) language = "en_US";

            Task.Run(() =>
            {
                _allTrades.Clear();
                Program.LoadingForm.InitCurrentTradesLoadingProcess(sentOffers, receivedOffers, activeOnly, language);
                var fullTradeOffers = Program.LoadingForm.GetLoadedCurrentTrades();
                Program.LoadingForm.DeactivateForm();

                foreach (var trade in fullTradeOffers)
                {
                    _allTrades.Add(trade.Offer.TradeOfferId, trade);

                    Dispatcher.AsMainForm(() =>
                    {
                        CurrentTradesGridView.Rows.Add(
                            trade.Offer.TradeOfferId,
                            new SteamID((uint)trade.Offer.AccountIdOther, EUniverse.Public, EAccountType.Individual)
                                .ConvertToUInt64(),
                            trade.Offer.TradeOfferState.ToString().Replace("TradeOfferState", "")
                        );
                    });
                }

                Dispatcher.AsMainForm(() => { LoadTradesButton.Enabled = true; });
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

            if (tradeOffer.ItemsToGive != null)
            {
                var gropedItems = tradeOffer.ItemsToGive.GroupBy(item => item.Description.MarketHashName);
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

            if (tradeOffer.ItemsToReceive != null)
            {
                var gropedItems = tradeOffer.ItemsToReceive.GroupBy(item => item.Description.MarketHashName);
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

            ExtraTradeInfoGridView.Rows.Add("TradeOfferId", tradeOffer.Offer.TradeOfferId);
            ExtraTradeInfoGridView.Rows.Add("TradeOfferState",
                tradeOffer.Offer.TradeOfferState.ToString().Replace("TradeOfferState", ""));
            ExtraTradeInfoGridView.Rows.Add("Message", tradeOffer.Offer.Message);
            ExtraTradeInfoGridView.Rows.Add("IsOurOffer", tradeOffer.Offer.IsOurOffer.ToString());
            ExtraTradeInfoGridView.Rows.Add("AccountIdOther", tradeOffer.Offer.AccountIdOther.ToString());
            ExtraTradeInfoGridView.Rows.Add("ExpirationTime",
                CommonUtils.ParseSteamUnixDate(tradeOffer.Offer.ExpirationTime).ToString(CultureInfo.InvariantCulture));
            ExtraTradeInfoGridView.Rows.Add("ConfirmationMethod",
                tradeOffer.Offer.EConfirmationMethod.ToString().Replace("TradeOfferConfirmation", ""));
            ExtraTradeInfoGridView.Rows.Add("TimeCreated",
                CommonUtils.ParseSteamUnixDate(tradeOffer.Offer.TimeCreated).ToString(CultureInfo.InvariantCulture));
            ExtraTradeInfoGridView.Rows.Add("TimeUpdated",
                CommonUtils.ParseSteamUnixDate(tradeOffer.Offer.TimeUpdated).ToString(CultureInfo.InvariantCulture));
            ExtraTradeInfoGridView.Rows.Add("EscrowEndDate", tradeOffer.Offer.EscrowEndDate.ToString());
            ExtraTradeInfoGridView.Rows.Add("FromRealTimeTrade", tradeOffer.Offer.FromRealTimeTrade.ToString());
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
            if (!(sender is DataGridView grid)) return;

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
            if (!(sender is DataGridView grid)) return;

            if (grid.Rows.Count == 1)
            {
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
        }

        private void DescriptionLanguageComboBox_TextChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().ActiveTradesLanguage, DescriptionLanguageComboBox.Text);
        }

        private void SentOffersCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().ActiveTradesSent, SentOffersCheckBox.Checked);
        }

        private void ReceivedOffersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().ActiveTradesReceived, ReceivedOffersCheckBox.Checked);
        }

        private void ActiveOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().ActiveTradesActiveOnly, ActiveOnlyCheckBox.Checked);
        }

        private void AcceptTradeButton_Click(object sender, EventArgs e)
        {
            if (_selectedOfferId == null) return;

            string text;
            MessageBoxIcon icon;
            try
            {
                var response = CurrentSession.SteamManager.OfferSession.Accept(_selectedOfferId);

                text = $"Accept state - {response.Accepted}.";
                if (!string.IsNullOrEmpty(response.TradeError)) text += $"Error - {response.TradeError}";
                icon = MessageBoxIcon.Information;
            }
            catch (Exception ex)
            {
                text = ex.Message;
                icon = MessageBoxIcon.Error;
            }

            MessageBox.Show(text, @"Trade info", MessageBoxButtons.OK, icon);
        }

        private void DeclineTradeButton_Click(object sender, EventArgs e)
        {
            if (_selectedOfferId == null) return;

            string text;
            MessageBoxIcon icon;
            try
            {
                var response = CurrentSession.SteamManager.OfferSession.Decline(_selectedOfferId);

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
    }
}