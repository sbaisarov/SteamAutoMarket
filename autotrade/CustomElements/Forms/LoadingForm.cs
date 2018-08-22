using autotrade.Steam.TradeOffer;
using autotrade.Utils;
using autotrade.WorkingProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static autotrade.Steam.TradeOffer.Inventory;

namespace autotrade {
    public partial class LoadingForm : Form {
        private Thread workingThread;
        private int totalItemsCount;
        private int currentPage;
        private int totalPages;
        bool stopButtonPressed = false;

        public LoadingForm() {
            InitializeComponent();
        }


        public void SetTotalItemsCount(int count, int totalPages, String text) {
            this.totalItemsCount = count;
            this.totalPages = totalPages;

            Dispatcher.Invoke(Program.LoadingForm, () => {
                TotalItemsLable.Text = $"{text} - {count}";
                ProgressBar.Maximum = totalPages;
            });
        }
        public void TrackLoadedIteration(String text) {
            Dispatcher.Invoke(Program.LoadingForm, () => {

                PageLable.Text = text
                    .Replace("{currentPage}", (++currentPage).ToString())
                    .Replace("{totalPages}", totalPages.ToString());

                ProgressBar.Value = currentPage;
            });
        }
        public void DisactivateForm() {
            Dispatcher.Invoke(Program.MainForm, () => {
                this.Close();
                Program.LoadingForm = new LoadingForm();
            });
        }

        private void ActivateForm() {
            Dispatcher.Invoke(Program.MainForm, () => {
                if (this != null) {
                    this.Show();
                }
            });
        }

        private void StopWorkingProcessButton_Click(object sender, EventArgs e) {
            stopButtonPressed = true;
            Dispatcher.Invoke(Program.LoadingForm, () => {
                Logger.Debug($"Inventory {CurrentSession.CurrentInventoryAppId}-{CurrentSession.CurrentInventoryContextId} loading process aborted");
                items = new List<RgFullItem>();
                currentTrades = new List<FullTradeOffer>();
                workingThread.Abort();
                DisactivateForm();
            });
        }
        private void InventoryLoadingForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (!stopButtonPressed) {
                StopWorkingProcessButton_Click(sender, e);
            }
        }

        #region Inventory
        private List<RgFullItem> items;

        public void InitInventoryLoadingProcess() {
            this.Text = $"{CurrentSession.CurrentInventoryAppId}-{CurrentSession.CurrentInventoryContextId} inventory loading";
            ActivateForm();
            workingThread = new Thread(() => {
                LoadCurrentInventory();
            });
            workingThread.Start();
        }
        public void LoadCurrentInventory() {
            items = CurrentSession.SteamManager.LoadInventory(CurrentSession.SteamManager.Guard.Session.SteamID.ToString(), CurrentSession.CurrentInventoryAppId, CurrentSession.CurrentInventoryContextId, true);
        }
        public List<RgFullItem> GetLoadedItems() {
            while (items == null || this == null) {
                Thread.Sleep(500);
            }
            return items;
        }
        #endregion

        #region Current Trades
        private List<FullTradeOffer> currentTrades;

        public void InitCurrentTradesLoadingProcess(bool getSentOffers, bool getReceivedOffers, bool activeOnly, string language) {
            this.Text = $"Trade history loading";
            ActivateForm();
            workingThread = new Thread(() => {
                LoadCurrentTradeOffers(getSentOffers, getReceivedOffers, activeOnly, language);
            });
            workingThread.Start();
        }
        public void LoadCurrentTradeOffers(bool getSentOffers, bool getReceivedOffers, bool activeOnly, string language) {
            List<FullTradeOffer> tradesList = new List<FullTradeOffer>();

            var response = CurrentSession.SteamManager.TradeOfferWeb.GetTradeOffers(getSentOffers, getReceivedOffers, true, activeOnly, false, language: language);
            Program.LoadingForm.SetTotalItemsCount(response.AllOffers.Count(), response.AllOffers.Count(), "Total trades count");

            foreach (var trade in response.AllOffers) {
                tradesList.Add(new FullTradeOffer
                {
                    Offers = trade,
                    ItemsToGive = TradeFullItem.GetFullItemsList(trade.ItemsToGive, response.Descriptions),
                    ItemsToRecieve = TradeFullItem.GetFullItemsList(trade.ItemsToReceive, response.Descriptions)
                });
                Program.LoadingForm.TrackLoadedIteration("{currentPage} of {totalPages} trades loaded");
            }

            currentTrades = tradesList;
        }
        public List<FullTradeOffer> GetLoadedCurrentTrades() {
            if (currentTrades == null || this == null) {
                Thread.Sleep(500);
                return GetLoadedCurrentTrades();
            }
            return currentTrades;
        }
        #endregion

        #region Trades History
        private List<FullTradeOffer> tradesHistory;

        public void InitTradesHistoryLoadingProcess(bool getSentOffers, bool getReceivedOffers, string timeHistoricalCutoff = "0", string language = "en_us") {
            Text = $"Trade history loading";
            ActivateForm();
            workingThread = new Thread(() => {
                LoadTradeOffersHistory(getSentOffers, getReceivedOffers, true, false, true, timeHistoricalCutoff, language);
            });
            workingThread.Start();
        }
        public void LoadTradeOffersHistory(bool getSentOffers, bool getReceivedOffers, bool getDescriptions, bool activeOnly, bool historicalOnly, string timeHistoricalCutoff, string language) {
            List<FullTradeOffer> tradesList = new List<FullTradeOffer>();

            var response = CurrentSession.SteamManager.TradeOfferWeb.GetTradeOffers(getSentOffers, getReceivedOffers, getDescriptions, activeOnly, historicalOnly, timeHistoricalCutoff, language);
            Program.LoadingForm.SetTotalItemsCount(response.AllOffers.Count(), response.AllOffers.Count(), "Total trades count");

            foreach (var trade in response.AllOffers) {
                tradesList.Add(new FullTradeOffer
                {
                    Offers = trade,
                    ItemsToGive = TradeFullItem.GetFullItemsList(trade.ItemsToGive, response.Descriptions),
                    ItemsToRecieve = TradeFullItem.GetFullItemsList(trade.ItemsToReceive, response.Descriptions)
                });
                Program.LoadingForm.TrackLoadedIteration("{currentPage} of {totalPages} trades loaded");
            }

            tradesHistory = tradesList;
        }
        public List<FullTradeOffer> GetLoadedTradesHistory() {
            if (tradesHistory == null || this == null) {
                Thread.Sleep(500);
                return GetLoadedTradesHistory();
            }
            return tradesHistory;
        }
        #endregion
    }
}
