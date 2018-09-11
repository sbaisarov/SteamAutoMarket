namespace SteamAutoMarket.CustomElements.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.Utils;
    using SteamAutoMarket.WorkingProcess;

    public partial class LoadingForm : Form
    {
        private int currentPage;

        private bool stopButtonPressed;

        private int totalPagesCount;

        private Thread workingThread;

        public LoadingForm()
        {
            this.InitializeComponent();
        }

        public void SetTotalItemsCount(int count, int totalPages, string text)
        {
            this.totalPagesCount = totalPages;

            Dispatcher.AsLoadingForm(
                () =>
                    {
                        this.TotalItemsLable.Text = $@"{text} - {count}";
                        this.ProgressBar.Maximum = this.totalPagesCount;
                    });
        }

        public void TrackLoadedIteration(string text)
        {
            Dispatcher.AsLoadingForm(
                () =>
                    {
                        this.PageLable.Text = text.Replace("{currentPage}", (++this.currentPage).ToString())
                            .Replace("{totalPages}", this.totalPagesCount.ToString());

                        if (this.currentPage > this.ProgressBar.Maximum)
                        {
                            this.currentPage = this.ProgressBar.Maximum;
                        }

                        this.ProgressBar.Value = this.currentPage;
                    });
        }

        public void DeactivateForm()
        {
            Dispatcher.AsMainForm(
                () =>
                    {
                        this.Close();
                        Program.LoadingForm = new LoadingForm();
                    });
        }

        private void ActivateForm()
        {
            Dispatcher.AsMainForm(this.Show);
        }

        private void StopWorkingProcessButtonClick(object sender, EventArgs e)
        {
            this.stopButtonPressed = true;
            Dispatcher.AsLoadingForm(
                () =>
                    {
                        Logger.Debug(
                            $"Inventory {CurrentSession.CurrentInventoryAppId}-{CurrentSession.CurrentInventoryContextId} loading process aborted");
                        this.items = new List<FullRgItem>();
                        this.currentTrades = new List<FullTradeOffer>();
                        this.workingThread.Abort();
                        this.DeactivateForm();
                    });
        }

        private void InventoryLoadingFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.stopButtonPressed)
            {
                this.StopWorkingProcessButtonClick(sender, e);
            }
        }

        #region Inventory

        private List<FullRgItem> items;

        public void InitInventoryLoadingProcess()
        {
            this.Text =
                $@"{CurrentSession.CurrentInventoryAppId}-{CurrentSession.CurrentInventoryContextId} inventory loading";
            this.ActivateForm();
            this.workingThread = new Thread(this.LoadCurrentInventory);
            this.workingThread.Start();
        }

        public void LoadCurrentInventory()
        {
            this.items = CurrentSession.SteamManager.LoadInventory(
                CurrentSession.SteamManager.Guard.Session.SteamID.ToString(),
                CurrentSession.CurrentInventoryAppId,
                CurrentSession.CurrentInventoryContextId,
                true);
        }

        public List<FullRgItem> GetLoadedItems()
        {
            while (this.items == null)
            {
                Thread.Sleep(500);
            }

            return this.items;
        }

        #endregion

        #region Current Trades

        private List<FullTradeOffer> currentTrades;

        public void InitCurrentTradesLoadingProcess(
            bool getSentOffers,
            bool getReceivedOffers,
            bool activeOnly,
            string language)
        {
            this.Text = @"Trade history loading";
            this.ActivateForm();
            this.workingThread = new Thread(
                () => { this.LoadCurrentTradeOffers(getSentOffers, getReceivedOffers, activeOnly, language); });
            this.workingThread.Start();
        }

        public void LoadCurrentTradeOffers(bool getSentOffers, bool getReceivedOffers, bool activeOnly, string language)
        {
            var tradesList = new List<FullTradeOffer>();

            var response = CurrentSession.SteamManager.TradeOfferWeb.GetTradeOffers(
                getSentOffers,
                getReceivedOffers,
                true,
                activeOnly,
                false,
                language: language);
            Program.LoadingForm.SetTotalItemsCount(
                response.AllOffers.Count(),
                response.AllOffers.Count(),
                "Total trades count");

            foreach (var trade in response.AllOffers)
            {
                tradesList.Add(
                    new FullTradeOffer
                        {
                            Offer = trade,
                            ItemsToGive = FullTradeItem.GetFullItemsList(trade.ItemsToGive, response.Descriptions),
                            ItemsToReceive = FullTradeItem.GetFullItemsList(trade.ItemsToReceive, response.Descriptions)
                        });
                Program.LoadingForm.TrackLoadedIteration("{currentPage} of {totalPages} trades loaded");
            }

            this.currentTrades = tradesList;
        }

        public List<FullTradeOffer> GetLoadedCurrentTrades()
        {
            while (this.currentTrades == null)
            {
                Thread.Sleep(500);
            }

            return this.currentTrades;
        }

        #endregion

        #region Trades History

        private List<FullHistoryTradeOffer> tradesHistory;

        public void InitTradesHistoryLoadingProcess(
            int? maxTrades,
            long? startAfterTime,
            string startAfterTradeId,
            bool navigatingBack = false,
            bool getDescriptions = false,
            string lanugage = "en",
            bool includeFailed = false)
        {
            this.Text = @"Trade history loading";
            this.ActivateForm();
            this.workingThread = new Thread(
                () =>
                    {
                        this.LoadTradeOffersHistory(
                            maxTrades,
                            startAfterTime,
                            startAfterTradeId,
                            navigatingBack,
                            getDescriptions,
                            lanugage,
                            includeFailed);
                    });
            this.workingThread.Start();
        }

        public void LoadTradeOffersHistory(
            int? maxTrades,
            long? startAfterTime,
            string startAfterTradeId,
            bool navigatingBack,
            bool getDescriptions,
            string language,
            bool includeFailed)
        {
            var tradesList = new List<FullHistoryTradeOffer>();

            var response = CurrentSession.SteamManager.TradeOfferWeb.GetTradeHistory(
                maxTrades,
                startAfterTime,
                startAfterTradeId,
                navigatingBack,
                getDescriptions,
                language,
                includeFailed);

            var totalCount = 0;
            if (response.Trades != null)
            {
                totalCount = response.Trades.Count();
            }

            Program.LoadingForm.SetTotalItemsCount(totalCount, totalCount, "Total trades count");

            if (response.Trades == null)
            {
                this.tradesHistory = new List<FullHistoryTradeOffer>();
                return;
            }

            foreach (var trade in response.Trades)
            {
                tradesList.Add(new FullHistoryTradeOffer(trade, response.Descriptions));
                Program.LoadingForm.TrackLoadedIteration("{currentPage} of {totalPages} trades loaded");
            }

            this.tradesHistory = tradesList;
        }

        public List<FullHistoryTradeOffer> GetLoadedTradesHistory()
        {
            while (this.tradesHistory == null)
            {
                Thread.Sleep(500);
            }

            return this.tradesHistory;
        }

        #endregion
    }
}