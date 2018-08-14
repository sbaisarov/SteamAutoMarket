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

namespace autotrade.CustomElements.Controls {
    public partial class RecievedTradeManageControl : UserControl {
        public RecievedTradeManageControl() {
            InitializeComponent();
        }

        private Dictionary<string, FullTradeOffer> ALL_TRADES = new Dictionary<string, FullTradeOffer>();
        private string SELECTED_OFFER_ID;
        private TradeFullItem SELECTED_ITEM;

        private void LoadInventoryButton_Click(object sender, EventArgs e) {
            if (CurrentSession.SteamManager == null) {
                MessageBox.Show("You should login first", "Error trades loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error on trades history loading. No logined account found.");
                return;
            }

            LoadTradesButton.Enabled = false;

            GridUtils.ClearGrids(CurrentTradesGridView, MyItemsGridView, HisItemsGridView, ExtraTradeInfoGridView);

            Task.Run(() => {
                Program.LoadingForm.InitTradesLoadingProcess(false, true, true, false, false);
                List<FullTradeOffer> fullTradeOffers = Program.LoadingForm.GetLoadedTrades();
                Program.LoadingForm.Disactivate();

                foreach (var trade in fullTradeOffers) {
                    ALL_TRADES.Add(trade.Offers.TradeOfferId, trade);

                    Dispatcher.Invoke(Program.MainForm, () => {
                        CurrentTradesGridView.Rows.Add(
                            trade.Offers.TradeOfferId,
                            trade.Offers.AccountIdOther,
                            trade.Offers.TradeOfferState.ToString().Replace("TradeOfferState", "")
                        );
                        LoadTradesButton.Enabled = true;
                    });
                }
            });

        }

        private void CurrentTradesGridView_SelectionChanged(object sender, EventArgs e) {
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
                        SteamItemsUtils.GetClearType(firstItem)
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
                        SteamItemsUtils.GetClearType(firstItem)
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
    }
}

