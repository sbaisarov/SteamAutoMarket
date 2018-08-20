using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using autotrade.CustomElements.Utils;
using autotrade.Steam.TradeOffer;
using autotrade.Utils;
using SteamKit2;
using autotrade.WorkingProcess;
using autotrade.WorkingProcess.Settings;

namespace autotrade {
    public partial class TradeHistoryControl : UserControl {
        public TradeHistoryControl() {
            InitializeComponent();

            var settings = SavedSettings.Get(); //todo
            SentOffersCheckBox.Checked = settings.ACTIVE_TRADES_LOAD_SENT;
            RecievedOffersCheckBox.Checked = settings.ACTIVE_TRADES_LOAD_RECIEVED;
            DescriptionLanguageComboBox.Text = settings.ACTIVE_TRADES_DESCRIPTION_LANGUAGE;
        }

        private Dictionary<string, FullTradeOffer> ALL_TRADES = new Dictionary<string, FullTradeOffer>();
        private string SELECTED_OFFER_ID;
        private AssetDescription SELECTED_ITEM_DESCRIPTION;

        public void AuthCurrentAccount() {
            this.AccountNameLable.Text = CurrentSession.SteamManager.Guard.AccountName;
            this.SplitterPanel.BackgroundImage = CurrentSession.AccountImage;
        }

        private void LoadInventoryButton_Click(object sender, EventArgs e) {
            if (CurrentSession.SteamManager == null) {
                MessageBox.Show("You should login first", "Error trades history loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error on trades history loading. No logined account found.");
                return;
            }

            bool sentOffers = SentOffersCheckBox.Checked;
            bool recievedOffers = RecievedOffersCheckBox.Checked;
            if (!sentOffers && !recievedOffers) {
                MessageBox.Show("You should select at least one type of offers to load (Recieved/Sent)", "Error trades history loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error on trades history loading. No trades type selected.");
                return;
            }

            GridUtils.ClearGrids(CurrentTradesGridView, MyItemsGridView, HisItemsGridView, ExtraTradeInfoGridView);

            string language = DescriptionLanguageComboBox.Text;
            if (language == null) {
                language = "en_US";
            }

            Task.Run(() => {
                ALL_TRADES.Clear();
                Program.LoadingForm.InitTradesHistoryLoadingProcess(sentOffers, recievedOffers, language: language);
                List<FullTradeOffer> fullTradeOffers = Program.LoadingForm.GetLoadedTradesHistory();
                Program.LoadingForm.Disactivate();

                Dispatcher.Invoke(Program.MainForm, () => {
                    foreach (var trade in fullTradeOffers) {
                        ALL_TRADES.Add(trade.Offers.TradeOfferId, trade);

                        CurrentTradesGridView.Rows.Add(
                            trade.Offers.TradeOfferId,
                            new SteamID((uint)trade.Offers.AccountIdOther, EUniverse.Public, EAccountType.Individual).ConvertToUInt64(),
                            trade.Offers.TradeOfferState.ToString().Replace("TradeOfferState", "")
                        );
                    }
                    LoadTradesButton.Enabled = true;
                });
            });

        }

        private void CurrentTradesGridView_SelectionChanged(object sender, EventArgs e) {
            if (CurrentTradesGridView.SelectedCells.Count == 0) return;

            int index = CurrentTradesGridView.SelectedCells[0].RowIndex;
            if (index < 0) return;

            var selected = (string)CurrentTradesGridView.Rows[index].Cells[0].Value;
            if (selected == SELECTED_OFFER_ID) return;

            SELECTED_OFFER_ID = selected;
            ChangeSelectedTrade();
        }

        private void ChangeSelectedTrade() {
            GridUtils.ClearGrids(MyItemsGridView, HisItemsGridView, ExtraTradeInfoGridView);
            FullTradeOffer tradeOffer = ALL_TRADES[SELECTED_OFFER_ID];

            if (tradeOffer.ItemsToGive != null) {
                var gropedItems = tradeOffer.ItemsToGive.GroupBy(item => item.Description.MarketHashName);
                foreach (var groupedList in gropedItems) {
                    var firstItem = groupedList.First();
                    MyItemsGridView.Rows.Add(
                        firstItem.Description.Name,
                        groupedList.Sum(x => int.Parse(x.Asset.Amount)),
                        SteamItemsUtils.GetClearType(firstItem),
                        firstItem.Description
                    );
                }
            }

            if (tradeOffer.ItemsToRecieve != null) {
                var gropedItems = tradeOffer.ItemsToRecieve.GroupBy(item => item.Description.MarketHashName);
                foreach (var groupedList in gropedItems) {
                    var firstItem = groupedList.First();
                    HisItemsGridView.Rows.Add(
                        firstItem.Description.Name,
                        groupedList.Sum(x => int.Parse(x.Asset.Amount)),
                        SteamItemsUtils.GetClearType(firstItem),
                        firstItem.Description
                    );
                }
            }

            ExtraTradeInfoGridView.Rows.Add("TradeOfferId", tradeOffer.Offers.TradeOfferId);
            ExtraTradeInfoGridView.Rows.Add("TradeOfferState", tradeOffer.Offers.TradeOfferState.ToString().Replace("TradeOfferState", ""));
            ExtraTradeInfoGridView.Rows.Add("Message", tradeOffer.Offers.Message);
            ExtraTradeInfoGridView.Rows.Add("IsOurOffer", tradeOffer.Offers.IsOurOffer.ToString());
            ExtraTradeInfoGridView.Rows.Add("AccountIdOther", tradeOffer.Offers.AccountIdOther.ToString());
            ExtraTradeInfoGridView.Rows.Add("ExpirationTime", GridUtils.ParseSteamUnixDate(tradeOffer.Offers.ExpirationTime).ToString());
            ExtraTradeInfoGridView.Rows.Add("ConfirmationMethod", tradeOffer.Offers.ConfirmationMethod.ToString().Replace("TradeOfferConfirmation", ""));
            ExtraTradeInfoGridView.Rows.Add("TimeCreated", GridUtils.ParseSteamUnixDate(tradeOffer.Offers.TimeCreated).ToString());
            ExtraTradeInfoGridView.Rows.Add("TimeUpdated", GridUtils.ParseSteamUnixDate(tradeOffer.Offers.TimeUpdated).ToString());
            ExtraTradeInfoGridView.Rows.Add("EscrowEndDate", tradeOffer.Offers.EscrowEndDate.ToString());
            ExtraTradeInfoGridView.Rows.Add("FromRealTimeTrade", tradeOffer.Offers.FromRealTimeTrade.ToString());
        }

        private void CurrentTradesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (e.ColumnIndex == 1) {
                var cell = CurrentTradesGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell == null) return;

                var steamId = cell.Value;
                if (steamId == null) return;

                System.Diagnostics.Process.Start($"https://steamcommunity.com/profiles/{steamId.ToString()}/");
            }
        }

        private void ItemsGridView_SelectionChanged(object sender, EventArgs e) {
            var grid = sender as DataGridView;
            var selectedRows = grid.SelectedRows;
            if (selectedRows.Count == 0) return;

            var index = selectedRows[0].Index;
            var description = (AssetDescription)grid.Rows[index].Cells[3].Value;
            if (!description.Equals(SELECTED_ITEM_DESCRIPTION)) {
                SELECTED_ITEM_DESCRIPTION = description;
                ChangeSelectedItem();
            }
        }

        private void ChangeSelectedItem() {
            ImageUtils.UpdateItemImageOnPanelAsync(SELECTED_ITEM_DESCRIPTION, ItemImageBox);
            UpdateItemDescriptionTextBox(ItemDescriptionTextBox, ItemNameLable, SELECTED_ITEM_DESCRIPTION);
        }

        private void UpdateItemDescriptionTextBox(RichTextBox textBox, Label label, AssetDescription description) {
            textBox.Clear();

            label.Text = description.Name;

            var descriptionText = "";
            if (description.Descriptions != null) {
                foreach (var item in description.Descriptions) {
                    string text = item.Value.Trim();
                    if (!string.IsNullOrWhiteSpace(text)) descriptionText += text + ", ";
                }
                if (descriptionText.EndsWith(", ")) descriptionText = descriptionText.Substring(0, descriptionText.Length - 2);
            }

            var tagsText = "";

            ElementsUtils.appendBoldText(textBox, "Game: ");
            textBox.AppendText(description.AppId.ToString() + "\n");

            ElementsUtils.appendBoldText(textBox, "Name: ");
            textBox.AppendText(description.MarketHashName + "\n");

            ElementsUtils.appendBoldText(textBox, "Type: ");
            textBox.AppendText(description.Type);

            if (!string.IsNullOrWhiteSpace(descriptionText)) {
                textBox.AppendText("\n");
                ElementsUtils.appendBoldText(textBox, "Description: ");
                textBox.AppendText(descriptionText);
            }

            if (!string.IsNullOrWhiteSpace(tagsText)) {
                textBox.AppendText("\n");
                ElementsUtils.appendBoldText(textBox, "Tags: ");
                textBox.AppendText(tagsText);
            }
        }

        private void ItemsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            var grid = sender as DataGridView;
            if (grid.Rows.Count == 1) {
                var selectedRows = grid.SelectedRows;
                if (selectedRows.Count == 0) return;

                var index = selectedRows[0].Index;
                var description = (AssetDescription)grid.Rows[index].Cells[3].Value;
                if (!description.Equals(SELECTED_ITEM_DESCRIPTION)) {
                    SELECTED_ITEM_DESCRIPTION = description;
                    ChangeSelectedItem();
                }
            }
        }

        private void DescriptionLanguageComboBox_TextChanged(object sender, EventArgs e) {
            SavedSettings.UpdateField(ref SavedSettings.Get().ACTIVE_TRADES_DESCRIPTION_LANGUAGE, DescriptionLanguageComboBox.Text);
        }

        private void SentOffersCheckBox_CheckStateChanged(object sender, EventArgs e) {
            SavedSettings.UpdateField(ref SavedSettings.Get().ACTIVE_TRADES_LOAD_SENT, SentOffersCheckBox.Checked);
        }

        private void RecievedOffersCheckBox_CheckedChanged(object sender, EventArgs e) {
            SavedSettings.UpdateField(ref SavedSettings.Get().ACTIVE_TRADES_LOAD_RECIEVED, RecievedOffersCheckBox.Checked);
        }
    }
}

