namespace SteamAutoMarket.Pages
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Navigation;

    using Steam.TradeOffer.Enums;

    using SteamAutoMarket.Models;
    using SteamAutoMarket.Properties;
    using SteamAutoMarket.Repository.Context;
    using SteamAutoMarket.Repository.Settings;
    using SteamAutoMarket.Utils.Extension;
    using SteamAutoMarket.Utils.Logger;

    /// <summary>
    /// Interaction logic for ActiveTrades.xaml
    /// </summary>
    public partial class ActiveTrades : INotifyPropertyChanged
    {
        private bool isLoadButtonEnabled = true;

        private SteamTradeItemsModel selectedTradeItem;

        private TradeHistoryModel selectedTradeOffer;

        public ActiveTrades()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool ActiveOnlyCheckBox
        {
            get => SettingsProvider.GetInstance().ActiveTradesActiveOnly;
            set
            {
                if (SettingsProvider.GetInstance().ActiveTradesActiveOnly == value) return;
                SettingsProvider.GetInstance().ActiveTradesActiveOnly = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<TradeHistoryModel> ActiveTradesList { get; } =
            new ObservableCollection<TradeHistoryModel>();

        public bool IsLoadButtonEnabled
        {
            get => this.isLoadButtonEnabled;
            set
            {
                if (this.isLoadButtonEnabled == value) return;
                this.isLoadButtonEnabled = value;
                this.OnPropertyChanged();
            }
        }

        public bool ReceivedOffersCheckBox
        {
            get => SettingsProvider.GetInstance().ActiveTradesReceivedOffers;
            set
            {
                if (SettingsProvider.GetInstance().ActiveTradesReceivedOffers == value) return;
                SettingsProvider.GetInstance().ActiveTradesReceivedOffers = value;
                this.OnPropertyChanged();
            }
        }

        public SteamTradeItemsModel SelectedTradeItem
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

        public bool SentOffersCheckBox
        {
            get => SettingsProvider.GetInstance().ActiveTradesSentOffers;
            set
            {
                if (SettingsProvider.GetInstance().ActiveTradesSentOffers == value) return;
                SettingsProvider.GetInstance().ActiveTradesSentOffers = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
        }

        private void LoadActiveTradesButtonClick(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            this.ActiveTradesList.Clear();

            Task.Run(
                () =>
                    {
                        this.IsLoadButtonEnabled = false;
                        try
                        {
                            var offers = UiGlobalVariables.SteamManager.ReceiveTradeOffers(
                                this.SentOffersCheckBox,
                                this.ReceivedOffersCheckBox);
                            if (this.ActiveOnlyCheckBox)
                            {
                                offers = offers.Where(
                                    o => o.Offer.TradeOfferState == TradeOfferState.TradeOfferStateActive);
                            }

                            foreach (var offer in offers)
                            {
                                this.ActiveTradesList.AddDispatch(new TradeHistoryModel(offer));
                            }

                            ErrorNotify.InfoMessageBox($"{offers.Count()} offers loaded");
                        }
                        catch (Exception ex)
                        {
                            ErrorNotify.CriticalMessageBox(ex);
                        }

                        this.IsLoadButtonEnabled = true;
                    });
        }
    }
}