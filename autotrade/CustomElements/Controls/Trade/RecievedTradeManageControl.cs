using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using autotrade.Steam.TradeOffer;
using static autotrade.Steam.TradeOffer.Inventory;
using autotrade.Utils;
using autotrade.CustomElements.Utils;
using autotrade.WorkingProcess;
using SteamKit2;
using autotrade.WorkingProcess.Settings;

namespace autotrade.CustomElements.Controls {
    public partial class RecievedTradeManageControl : UserControl {
        public RecievedTradeManageControl() {
            InitializeComponent();

            var settings = SavedSettings.Get();
            SentOffersCheckBox.Checked = settings.ACTIVE_TRADES_SENT;
            RecievedOffersCheckBox.Checked = settings.ACTIVE_TRADES_RECIEVED;
            ActiveOnlyCheckBox.Checked = settings.ACTIVE_TRADES_ACTIVE_ONLY;
            DescriptionLanguageComboBox.Text = settings.ACTIVE_TRADES_LANGUAGE;
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
                MessageBox.Show("You should login first", "Error trades loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error on trades history loading. No logined account found.");
                return;
            }

            bool sentOffers = SentOffersCheckBox.Checked;
            bool recievedOffers = RecievedOffersCheckBox.Checked;
            if (!sentOffers && !recievedOffers) {
                MessageBox.Show("You should select at least one type of offers to load (Recieved/Sent)", "Error trades loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error on trades history loading. No trades type selected.");
                return;
            }

            CommonUtils.ClearGrids(CurrentTradesGridView, MyItemsGridView, HisItemsGridView, ExtraTradeInfoGridView);
            LoadTradesButton.Enabled = false;
            bool activeOnly = ActiveOnlyCheckBox.Checked;

            string language = DescriptionLanguageComboBox.Text;
            if (language == null) {
                language = "en_US";
            }

            Task.Run(() => {
                ALL_TRADES.Clear();
                Program.LoadingForm.InitCurrentTradesLoadingProcess(sentOffers, recievedOffers, activeOnly, language);
                List<FullTradeOffer> fullTradeOffers = Program.LoadingForm.GetLoadedCurrentTrades();
                Program.LoadingForm.DisactivateForm();

                foreach (var trade in fullTradeOffers) {
                    ALL_TRADES.Add(trade.Offer.TradeOfferId, trade);

                    Dispatcher.Invoke(Program.MainForm, () => {
                        CurrentTradesGridView.Rows.Add(
                            trade.Offer.TradeOfferId,
                            new SteamID((uint)trade.Offer.AccountIdOther, EUniverse.Public, EAccountType.Individual).ConvertToUInt64(),
                            trade.Offer.TradeOfferState.ToString().Replace("TradeOfferState", "")
                        );
                        LoadTradesButton.Enabled = true;
                    });
                }
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
            CommonUtils.ClearGrids(MyItemsGridView, HisItemsGridView, ExtraTradeInfoGridView);
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

            ExtraTradeInfoGridView.Rows.Add("TradeOfferId", tradeOffer.Offer.TradeOfferId);
            ExtraTradeInfoGridView.Rows.Add("TradeOfferState", tradeOffer.Offer.TradeOfferState.ToString().Replace("TradeOfferState", ""));
            ExtraTradeInfoGridView.Rows.Add("Message", tradeOffer.Offer.Message);
            ExtraTradeInfoGridView.Rows.Add("IsOurOffer", tradeOffer.Offer.IsOurOffer.ToString());
            ExtraTradeInfoGridView.Rows.Add("AccountIdOther", tradeOffer.Offer.AccountIdOther.ToString());
            ExtraTradeInfoGridView.Rows.Add("ExpirationTime", CommonUtils.ParseSteamUnixDate(tradeOffer.Offer.ExpirationTime).ToString());
            ExtraTradeInfoGridView.Rows.Add("ConfirmationMethod", tradeOffer.Offer.ConfirmationMethod.ToString().Replace("TradeOfferConfirmation", ""));
            ExtraTradeInfoGridView.Rows.Add("TimeCreated", CommonUtils.ParseSteamUnixDate(tradeOffer.Offer.TimeCreated).ToString());
            ExtraTradeInfoGridView.Rows.Add("TimeUpdated", CommonUtils.ParseSteamUnixDate(tradeOffer.Offer.TimeUpdated).ToString());
            ExtraTradeInfoGridView.Rows.Add("EscrowEndDate", tradeOffer.Offer.EscrowEndDate.ToString());
            ExtraTradeInfoGridView.Rows.Add("FromRealTimeTrade", tradeOffer.Offer.FromRealTimeTrade.ToString());
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

            CommonUtils.AppendBoldText(textBox, "Game: ");
            textBox.AppendText(description.AppId.ToString() + "\n");

            CommonUtils.AppendBoldText(textBox, "Name: ");
            textBox.AppendText(description.MarketHashName + "\n");

            CommonUtils.AppendBoldText(textBox, "Type: ");
            textBox.AppendText(description.Type);

            if (!string.IsNullOrWhiteSpace(descriptionText)) {
                textBox.AppendText("\n");
                CommonUtils.AppendBoldText(textBox, "Description: ");
                textBox.AppendText(descriptionText);
            }

            if (!string.IsNullOrWhiteSpace(tagsText)) {
                textBox.AppendText("\n");
                CommonUtils.AppendBoldText(textBox, "Tags: ");
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
            SavedSettings.UpdateField(ref SavedSettings.Get().ACTIVE_TRADES_LANGUAGE, DescriptionLanguageComboBox.Text);
        }

        private void SentOffersCheckBox_CheckStateChanged(object sender, EventArgs e) {
            SavedSettings.UpdateField(ref SavedSettings.Get().ACTIVE_TRADES_SENT, SentOffersCheckBox.Checked);
        }

        private void RecievedOffersCheckBox_CheckedChanged(object sender, EventArgs e) {
            SavedSettings.UpdateField(ref SavedSettings.Get().ACTIVE_TRADES_RECIEVED, RecievedOffersCheckBox.Checked);
        }

        private void ActiveOnlyCheckBox_CheckedChanged(object sender, EventArgs e) {
            SavedSettings.UpdateField(ref SavedSettings.Get().ACTIVE_TRADES_ACTIVE_ONLY, ActiveOnlyCheckBox.Checked);
        }
    }
}

