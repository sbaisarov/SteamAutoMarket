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
        public void Disactivate() {
            Dispatcher.Invoke(Program.MainForm, () => {
                this.Close();
                Program.LoadingForm = new LoadingForm();
            });
        }

        private void Activate() => Dispatcher.Invoke(Program.MainForm, () => {
            this.Show();
        });
        private void StopWorkingProcessButton_Click(object sender, EventArgs e) {
            stopButtonPressed = true;
            Dispatcher.Invoke(Program.LoadingForm, () => {
                Logger.Debug($"Inventory {CurrentSession.CurrentInventoryAppId}-{CurrentSession.CurrentInventoryContextId} loading process aborted");
                items = new List<RgFullItem>();
                trades = new List<FullTradeOffer>();
                workingThread.Abort();
                Disactivate();
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
            Activate();
            workingThread = new Thread(() => {
                LoadCurrentInventory();
            });
            workingThread.Start();
        }
        public void LoadCurrentInventory() {
            items = CurrentSession.SteamManager.LoadInventory(CurrentSession.SteamManager.Guard.Session.SteamID.ToString(), CurrentSession.CurrentInventoryAppId, CurrentSession.CurrentInventoryContextId, true);
        }
        public List<RgFullItem> GetLoadedItems() {
            if (items == null || this == null) {
                Thread.Sleep(300);
                return GetLoadedItems();
            }
            return items;
        }
        #endregion

        #region Trades
        private List<FullTradeOffer> trades;

        public void InitTradesLoadingProcess(bool getSentOffers, bool getReceivedOffers, bool getDescriptions, bool activeOnly, bool historicalOnly, string timeHistoricalCutoff = "1389106496", string language = "en_us") {
            this.Text = $"Trade history loading";
            Activate();
            workingThread = new Thread(() => {
                LoadTradeOffers(getSentOffers, getReceivedOffers, getDescriptions, activeOnly, historicalOnly, timeHistoricalCutoff, language);
            });
            workingThread.Start();
        }
        public void LoadTradeOffers(bool getSentOffers, bool getReceivedOffers, bool getDescriptions, bool activeOnly, bool historicalOnly, string timeHistoricalCutoff = "1389106496", string language = "en_us") {
            List<FullTradeOffer> tradesList = new List<FullTradeOffer>();

            var response = CurrentSession.SteamManager.TradeOfferWeb.GetTradeOffers(false, true, true, false, false);
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

            trades = tradesList;
        }
        public List<FullTradeOffer> GetLoadedTrades() {
            if (trades == null || this == null) {
                Thread.Sleep(300);
                return GetLoadedTrades();
            }
            return trades;
        }
        #endregion




    }
}
