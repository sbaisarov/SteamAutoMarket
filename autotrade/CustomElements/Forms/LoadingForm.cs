using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using autotrade.Steam.TradeOffer.Models.Full;
using autotrade.Utils;
using autotrade.WorkingProcess;

namespace autotrade.CustomElements.Forms
{
    public partial class LoadingForm : Form
    {
        private int currentPage;
        private bool stopButtonPressed;
        private int totalItemsCount;
        private int totalPages;
        private Thread workingThread;

        public LoadingForm()
        {
            InitializeComponent();
        }


        public void SetTotalItemsCount(int count, int totalPages, string text)
        {
            totalItemsCount = count;
            this.totalPages = totalPages;

            Dispatcher.AsLoadingForm(() =>
            {
                TotalItemsLable.Text = $"{text} - {count}";
                ProgressBar.Maximum = totalPages;
            });
        }

        public void TrackLoadedIteration(string text)
        {
            Dispatcher.AsLoadingForm(() =>
            {
                PageLable.Text = text
                    .Replace("{currentPage}", (++currentPage).ToString())
                    .Replace("{totalPages}", totalPages.ToString());

                if (currentPage > ProgressBar.Maximum) currentPage = ProgressBar.Maximum;

                ProgressBar.Value = currentPage;
            });
        }

        public void DisactivateForm()
        {
            Dispatcher.AsMainForm(() =>
            {
                Close();
                Program.LoadingForm = new LoadingForm();
            });
        }

        private void ActivateForm()
        {
            Dispatcher.AsMainForm(() =>
            {
                if (this != null) Show();
            });
        }

        private void StopWorkingProcessButton_Click(object sender, EventArgs e)
        {
            stopButtonPressed = true;
            Dispatcher.AsLoadingForm(() =>
            {
                Logger.Debug(
                    $"Inventory {CurrentSession.CurrentInventoryAppId}-{CurrentSession.CurrentInventoryContextId} loading process aborted");
                items = new List<FullRgItem>();
                currentTrades = new List<FullTradeOffer>();
                workingThread.Abort();
                DisactivateForm();
            });
        }

        private void InventoryLoadingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!stopButtonPressed) StopWorkingProcessButton_Click(sender, e);
        }

        #region Inventory

        private List<FullRgItem> items;

        public void InitInventoryLoadingProcess()
        {
            Text =
                $"{CurrentSession.CurrentInventoryAppId}-{CurrentSession.CurrentInventoryContextId} inventory loading";
            ActivateForm();
            workingThread = new Thread(() => { LoadCurrentInventory(); });
            workingThread.Start();
        }

        public void LoadCurrentInventory()
        {
            items = CurrentSession.SteamManager.LoadInventory(
                CurrentSession.SteamManager.Guard.Session.SteamID.ToString(), CurrentSession.CurrentInventoryAppId,
                CurrentSession.CurrentInventoryContextId, true);
        }

        public List<FullRgItem> GetLoadedItems()
        {
            while (items == null || this == null) Thread.Sleep(500);
            return items;
        }

        #endregion

        #region Current Trades

        private List<FullTradeOffer> currentTrades;

        public void InitCurrentTradesLoadingProcess(bool getSentOffers, bool getReceivedOffers, bool activeOnly,
            string language)
        {
            Text = "Trade history loading";
            ActivateForm();
            workingThread = new Thread(() =>
            {
                LoadCurrentTradeOffers(getSentOffers, getReceivedOffers, activeOnly, language);
            });
            workingThread.Start();
        }

        public void LoadCurrentTradeOffers(bool getSentOffers, bool getReceivedOffers, bool activeOnly, string language)
        {
            var tradesList = new List<FullTradeOffer>();

            var response = CurrentSession.SteamManager.TradeOfferWeb.GetTradeOffers(getSentOffers, getReceivedOffers,
                true, activeOnly, false, language: language);
            Program.LoadingForm.SetTotalItemsCount(response.AllOffers.Count(), response.AllOffers.Count(),
                "Total trades count");

            foreach (var trade in response.AllOffers)
            {
                tradesList.Add(new FullTradeOffer
                {
                    Offer = trade,
                    ItemsToGive = FullTradeItem.GetFullItemsList(trade.ItemsToGive, response.Descriptions),
                    ItemsToReceive = FullTradeItem.GetFullItemsList(trade.ItemsToReceive, response.Descriptions)
                });
                Program.LoadingForm.TrackLoadedIteration("{currentPage} of {totalPages} trades loaded");
            }

            currentTrades = tradesList;
        }

        public List<FullTradeOffer> GetLoadedCurrentTrades()
        {
            while (currentTrades == null || this == null) Thread.Sleep(500);
            return currentTrades;
        }

        #endregion

        #region Trades History

        private List<FullHistoryTradeOffer> tradesHistory;

        public void InitTradesHistoryLoadingProcess(int? maxTrades, long? startAfterTime, string startAfterTradeId,
            bool navigatingBack = false, bool getDescriptions = false, string lanugage = "en",
            bool includeFailed = false)
        {
            Text = "Trade history loading";
            ActivateForm();
            workingThread = new Thread(() =>
            {
                LoadTradeOffersHistory(maxTrades, startAfterTime, startAfterTradeId, navigatingBack,
                    getDescriptions, lanugage, includeFailed);
            });
            workingThread.Start();
        }

        public void LoadTradeOffersHistory(int? maxTrades, long? startAfterTime, string startAfterTradeId,
            bool navigatingBack, bool getDescriptions, string lanugage, bool includeFailed)
        {
            var tradesList = new List<FullHistoryTradeOffer>();

            var response = CurrentSession.SteamManager.TradeOfferWeb.GetTradeHistory(maxTrades, startAfterTime,
                startAfterTradeId, navigatingBack, getDescriptions, lanugage, includeFailed);

            var totalCount = 0;
            if (response.Trades != null) totalCount = response.Trades.Count();
            Program.LoadingForm.SetTotalItemsCount(totalCount, totalCount, "Total trades count");

            if (response.Trades == null)
            {
                tradesHistory = new List<FullHistoryTradeOffer>();
                return;
            }

            foreach (var trade in response.Trades)
            {
                tradesList.Add(new FullHistoryTradeOffer(trade, response.Descriptions));
                Program.LoadingForm.TrackLoadedIteration("{currentPage} of {totalPages} trades loaded");
            }

            tradesHistory = tradesList;
        }

        public List<FullHistoryTradeOffer> GetLoadedTradesHistory()
        {
            while (tradesHistory == null || this == null) Thread.Sleep(500);
            return tradesHistory;
        }

        #endregion
    }
}