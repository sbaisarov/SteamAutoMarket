using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SteamAutoMarket.Steam.TradeOffer.Models.Full;
using SteamAutoMarket.Utils;
using SteamAutoMarket.WorkingProcess;

namespace SteamAutoMarket.CustomElements.Forms
{
    public partial class LoadingForm : Form
    {
        private int _currentPage;
        private bool _stopButtonPressed;
        private int _totalPages;
        private Thread _workingThread;

        public LoadingForm()
        {
            InitializeComponent();
        }


        public void SetTotalItemsCount(int count, int totalPages, string text)
        {
            _totalPages = totalPages;

            Dispatcher.AsLoadingForm(() =>
            {
                TotalItemsLable.Text = $@"{text} - {count}";
                ProgressBar.Maximum = totalPages;
            });
        }

        public void TrackLoadedIteration(string text)
        {
            Dispatcher.AsLoadingForm(() =>
            {
                PageLable.Text = text
                    .Replace("{currentPage}", (++_currentPage).ToString())
                    .Replace("{totalPages}", _totalPages.ToString());

                if (_currentPage > ProgressBar.Maximum) _currentPage = ProgressBar.Maximum;
                ProgressBar.Value = _currentPage;
            });
        }

        public void DeactivateForm()
        {
            Dispatcher.AsMainForm(() =>
            {
                Close();
                Program.LoadingForm = new LoadingForm();
            });
        }

        private void ActivateForm()
        {
            Dispatcher.AsMainForm(Show);
        }

        private void StopWorkingProcessButton_Click(object sender, EventArgs e)
        {
            _stopButtonPressed = true;
            Dispatcher.AsLoadingForm(() =>
            {
                Logger.Debug(
                    $"Inventory {CurrentSession.CurrentInventoryAppId}-{CurrentSession.CurrentInventoryContextId} loading process aborted");
                _items = new List<FullRgItem>();
                _currentTrades = new List<FullTradeOffer>();
                _workingThread.Abort();
                DeactivateForm();
            });
        }

        private void InventoryLoadingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_stopButtonPressed) StopWorkingProcessButton_Click(sender, e);
        }

        #region Inventory

        private List<FullRgItem> _items;

        public void InitInventoryLoadingProcess()
        {
            Text =
                $@"{CurrentSession.CurrentInventoryAppId}-{CurrentSession.CurrentInventoryContextId} inventory loading";
            ActivateForm();
            _workingThread = new Thread(LoadCurrentInventory);
            _workingThread.Start();
        }

        public void LoadCurrentInventory()
        {
            _items = CurrentSession.SteamManager.LoadInventory(
                CurrentSession.SteamManager.Guard.Session.SteamID.ToString(), CurrentSession.CurrentInventoryAppId,
                CurrentSession.CurrentInventoryContextId, true);
        }

        public List<FullRgItem> GetLoadedItems()
        {
            while (_items == null) Thread.Sleep(500);
            return _items;
        }

        #endregion

        #region Current Trades

        private List<FullTradeOffer> _currentTrades;

        public void InitCurrentTradesLoadingProcess(bool getSentOffers, bool getReceivedOffers, bool activeOnly,
            string language)
        {
            Text = @"Trade history loading";
            ActivateForm();
            _workingThread = new Thread(() =>
            {
                LoadCurrentTradeOffers(getSentOffers, getReceivedOffers, activeOnly, language);
            });
            _workingThread.Start();
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

            _currentTrades = tradesList;
        }

        public List<FullTradeOffer> GetLoadedCurrentTrades()
        {
            while (_currentTrades == null) Thread.Sleep(500);
            return _currentTrades;
        }

        #endregion

        #region Trades History

        private List<FullHistoryTradeOffer> _tradesHistory;

        public void InitTradesHistoryLoadingProcess(int? maxTrades, long? startAfterTime, string startAfterTradeId,
            bool navigatingBack = false, bool getDescriptions = false, string lanugage = "en",
            bool includeFailed = false)
        {
            Text = @"Trade history loading";
            ActivateForm();
            _workingThread = new Thread(() =>
            {
                LoadTradeOffersHistory(maxTrades, startAfterTime, startAfterTradeId, navigatingBack,
                    getDescriptions, lanugage, includeFailed);
            });
            _workingThread.Start();
        }

        public void LoadTradeOffersHistory(int? maxTrades, long? startAfterTime, string startAfterTradeId,
            bool navigatingBack, bool getDescriptions, string language, bool includeFailed)
        {
            var tradesList = new List<FullHistoryTradeOffer>();

            var response = CurrentSession.SteamManager.TradeOfferWeb.GetTradeHistory(maxTrades, startAfterTime,
                startAfterTradeId, navigatingBack, getDescriptions, language, includeFailed);

            var totalCount = 0;
            if (response.Trades != null) totalCount = response.Trades.Count();
            Program.LoadingForm.SetTotalItemsCount(totalCount, totalCount, "Total trades count");

            if (response.Trades == null)
            {
                _tradesHistory = new List<FullHistoryTradeOffer>();
                return;
            }

            foreach (var trade in response.Trades)
            {
                tradesList.Add(new FullHistoryTradeOffer(trade, response.Descriptions));
                Program.LoadingForm.TrackLoadedIteration("{currentPage} of {totalPages} trades loaded");
            }

            _tradesHistory = tradesList;
        }

        public List<FullHistoryTradeOffer> GetLoadedTradesHistory()
        {
            while (_tradesHistory == null) Thread.Sleep(500);
            return _tradesHistory;
        }

        #endregion
    }
}