namespace SteamAutoMarket.UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Navigation;

    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.Repository.Settings;
    using SteamAutoMarket.UI.Utils.Extension;
    using SteamAutoMarket.UI.Utils.Logger;

    /// <summary>
    /// Interaction logic for TradeHistory.xaml
    /// </summary>
    public partial class TradeHistory : INotifyPropertyChanged
    {
        private bool isLoadButtonEnabled = true;

        private SteamTradeHistoryItemsModel selectedTradeItem;

        private TradeHistoryModel selectedTradeOffer;

        private IEnumerable<string> startAfterTimeItems;

        private IEnumerable<string> startAfterTradeIdItems;

        public TradeHistory()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool GetDescriptionCheckbox
        {
            get => SettingsProvider.GetInstance().TradeHistoryGetDescription;
            set
            {
                SettingsProvider.GetInstance().TradeHistoryGetDescription = value;
                this.OnPropertyChanged();
            }
        }

        public bool IncludeFailedCheckbox
        {
            get => SettingsProvider.GetInstance().TradeHistoryIncludeFailed;
            set
            {
                SettingsProvider.GetInstance().TradeHistoryIncludeFailed = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsLoadButtonEnabled
        {
            get => this.isLoadButtonEnabled;
            set
            {
                this.isLoadButtonEnabled = value;
                this.OnPropertyChanged();
            }
        }

        public int MaxTradesCount
        {
            get => SettingsProvider.GetInstance().TradeHistoryMaxTradesCount;
            set
            {
                SettingsProvider.GetInstance().TradeHistoryMaxTradesCount = value;
                this.OnPropertyChanged();
            }
        }

        public bool NavigatingBackCheckbox
        {
            get => SettingsProvider.GetInstance().TradeHistoryNavigatingBack;
            set
            {
                SettingsProvider.GetInstance().TradeHistoryNavigatingBack = value;
                this.OnPropertyChanged();
            }
        }

        public bool ReceivedOffersCheckbox
        {
            get => SettingsProvider.GetInstance().TradeHistoryReceivedOffers;
            set
            {
                SettingsProvider.GetInstance().TradeHistoryReceivedOffers = value;
                this.OnPropertyChanged();
            }
        }

        public SteamTradeHistoryItemsModel SelectedTradeItem
        {
            get => this.selectedTradeItem;
            set
            {
                this.selectedTradeItem = value;
                this.OnPropertyChanged();
            }
        }

        public TradeHistoryModel SelectedTradeOffer
        {
            get => this.selectedTradeOffer;
            set
            {
                this.selectedTradeOffer = value;
                this.OnPropertyChanged();
            }
        }

        public bool SentOffersCheckbox
        {
            get => SettingsProvider.GetInstance().TradeHistorySentOffers;
            set
            {
                SettingsProvider.GetInstance().TradeHistorySentOffers = value;
                this.OnPropertyChanged();
            }
        }

        public IEnumerable<string> StartAfterTimeItems
        {
            get => this.startAfterTimeItems;
            set
            {
                this.startAfterTimeItems = value;
                this.OnPropertyChanged();
            }
        }

        public string StartAfterTimeText { get; set; }

        public IEnumerable<string> StartAfterTradeIdItems
        {
            get => this.startAfterTradeIdItems;
            set
            {
                this.startAfterTradeIdItems = value;
                this.OnPropertyChanged();
            }
        }

        public string StartAfterTradeIdText { get; set; }

        public ObservableCollection<TradeHistoryModel> TradesHistoryList { get; } =
            new ObservableCollection<TradeHistoryModel>();

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void HyperlinkRequestNavigate(object sender, RequestNavigateEventArgs e) =>
            Process.Start(e.Uri.ToString());

        private void LoadTradeOfferHistoryButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            this.IsLoadButtonEnabled = false;
            this.TradesHistoryList.Clear();
            Task.Run(
                () =>
                    {
                        try
                        {
                            var response = UiGlobalVariables.SteamManager.TradeOfferWeb.GetTradeHistory(
                                this.MaxTradesCount,
                                this.StartAfterTimeText,
                                this.StartAfterTradeIdText,
                                this.NavigatingBackCheckbox,
                                this.GetDescriptionCheckbox,
                                this.IncludeFailedCheckbox);

                            if (response.Trades == null)
                            {
                                ErrorNotify.InfoMessageBox("No trades matching current query found");
                                return;
                            }

                            foreach (var trade in response.Trades)
                            {
                                this.TradesHistoryList.AddDispatch(
                                    new TradeHistoryModel(new FullHistoryTradeOffer(trade, response.Descriptions)));
                            }

                            this.StartAfterTimeItems = this.TradesHistoryList?.Select(t => t.Offer.Offer.TimeInit);
                            this.StartAfterTradeIdItems = this.TradesHistoryList?.Select(t => t.Offer.TradeId);

                            ErrorNotify.InfoMessageBox($"{response.Trades.Count} trade history items was loaded");
                        }
                        catch (Exception ex)
                        {
                            ErrorNotify.CriticalMessageBox("Error on getting trade offers history", ex);
                        }

                        this.IsLoadButtonEnabled = true;
                    });
        }
    }
}