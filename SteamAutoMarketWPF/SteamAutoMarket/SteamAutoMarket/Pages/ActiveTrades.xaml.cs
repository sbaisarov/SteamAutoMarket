namespace SteamAutoMarket.Pages
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
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

        private ActiveTradeModel selectedTradeOffer;

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

        public ObservableCollection<ActiveTradeModel> ActiveTradesList { get; } =
            new ObservableCollection<ActiveTradeModel>();

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

        public ActiveTradeModel SelectedTradeOffer
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

        private void AcceptTradeOfferButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.SelectedTradeOffer == null)
            {
                ErrorNotify.CriticalMessageBox("No trade offer selected. You should select trade offer first");
                return;
            }

            this.IsLoadButtonEnabled = false;
            Task.Run(
                () =>
                    {
                        try
                        {
                            var response =
                                UiGlobalVariables.SteamManager.OfferSession.Accept(this.SelectedTradeOffer.TradeId);
                            if (response.Accepted == false)
                            {
                                ErrorNotify.CriticalMessageBox($"Error on confirm trade offer - {response.TradeError}");
                            }
                            else
                            {
                                ErrorNotify.InfoMessageBox("Trade was successful accepted");
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorNotify.CriticalMessageBox($"Error on confirm trade offer", ex);
                        }

                        this.IsLoadButtonEnabled = true;
                    });
        }

        private void DeclineTradeOfferButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.SelectedTradeOffer == null)
            {
                ErrorNotify.CriticalMessageBox("No trade offer selected. You should select trade offer first");
                return;
            }

            this.IsLoadButtonEnabled = false;
            Task.Run(
                () =>
                    {
                        try
                        {
                            bool response;

                            if (this.SelectedTradeOffer.Offer.Offer.IsOurOffer)
                            {
                                response = UiGlobalVariables.SteamManager.OfferSession.Cancel(
                                    this.SelectedTradeOffer.TradeId);
                            }
                            else
                            {
                                response = UiGlobalVariables.SteamManager.OfferSession.Decline(
                                    this.SelectedTradeOffer.TradeId);
                            }

                            if (response == false)
                            {
                                ErrorNotify.CriticalMessageBox($"Error on confirm decline offer");
                            }
                            else
                            {
                                ErrorNotify.InfoMessageBox("Trade was successful declined");
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorNotify.CriticalMessageBox($"Error on decline trade offer", ex);
                        }

                        this.IsLoadButtonEnabled = true;
                    });
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e) =>
            Process.Start(e.Uri.ToString());

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
                                this.ActiveTradesList.AddDispatch(new ActiveTradeModel(offer));
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