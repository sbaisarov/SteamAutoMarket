namespace SteamAutoMarket.UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.Properties;
    using SteamAutoMarket.Steam.TradeOffer.Enums;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Models.Enums;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.Repository.Settings;
    using SteamAutoMarket.UI.Utils.Logger;

    using SteamKit2;

    /// <summary>
    /// Interaction logic for SteamAccountInfo.xaml
    /// </summary>
    public partial class TradeAutoAccept : INotifyPropertyChanged
    {
        private string newWhiteListAccountName;

        private string newWhiteListSteamId;

        private ObservableCollection<NameValueModel> tradeAcceptWhitelist =
            new ObservableCollection<NameValueModel>(SettingsProvider.GetInstance().TradeAcceptWhitelist);

        public TradeAutoAccept()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool AcceptIncomingEmpty
        {
            get => SettingsProvider.GetInstance().TradeAcceptIncomingEmpty;
            set
            {
                SettingsProvider.GetInstance().TradeAcceptIncomingEmpty = value;
                this.OnPropertyChanged();
            }
        }

        public bool AcceptIncomingWhitelist
        {
            get => SettingsProvider.GetInstance().TradeAcceptIncomingWhitelist;
            set
            {
                SettingsProvider.GetInstance().TradeAcceptIncomingWhitelist = value;
                this.OnPropertyChanged();
            }
        }

        public bool DeclineAllIncoming
        {
            get => SettingsProvider.GetInstance().TradeDeclineAllIncoming;
            set
            {
                SettingsProvider.GetInstance().TradeDeclineAllIncoming = value;
                this.OnPropertyChanged();
            }
        }

        public bool DeclineIncomingNotEmpty
        {
            get => SettingsProvider.GetInstance().TradeDeclineIncomingNotEmpty;
            set
            {
                SettingsProvider.GetInstance().TradeDeclineIncomingNotEmpty = value;
                this.OnPropertyChanged();
            }
        }

        public bool DeclineSent
        {
            get => SettingsProvider.GetInstance().TradeDeclineSent;
            set
            {
                SettingsProvider.GetInstance().TradeDeclineSent = value;
                this.OnPropertyChanged();
            }
        }

        public string NewWhiteListAccountName
        {
            get => this.newWhiteListAccountName;
            set
            {
                this.newWhiteListAccountName = value;
                this.OnPropertyChanged();
            }
        }

        public string NewWhiteListSteamId
        {
            get => this.newWhiteListSteamId;
            set
            {
                this.newWhiteListSteamId = value;
                this.OnPropertyChanged();
            }
        }

        public int ThreadsCount
        {
            get => SettingsProvider.GetInstance().TradeAcceptThreadsCount;
            set
            {
                SettingsProvider.GetInstance().TradeAcceptThreadsCount = value;
                this.OnPropertyChanged();
            }
        }

        public int TradeAcceptDelaySeconds
        {
            get => SettingsProvider.GetInstance().TradeAcceptDelaySeconds;
            set
            {
                SettingsProvider.GetInstance().TradeAcceptDelaySeconds = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<NameValueModel> TradeAcceptWhitelist
        {
            get => this.tradeAcceptWhitelist;
            set
            {
                this.tradeAcceptWhitelist = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private static void RemoveProcessedOffers(
            ICollection<FullTradeOffer> allOffers,
            IEnumerable<FullTradeOffer> sublist)
        {
            foreach (var offer in sublist.ToList())
            {
                allOffers.Remove(offer);
            }
        }

        private void AddNewSteamWhitelistAccountClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.NewWhiteListAccountName))
            {
                ErrorNotify.CriticalMessageBox("Account name can not be empty");
                return;
            }

            if (string.IsNullOrEmpty(this.NewWhiteListSteamId) || this.NewWhiteListSteamId.StartsWith("76") == false)
            {
                ErrorNotify.CriticalMessageBox("Account SteamID in in incorrect format");
                return;
            }

            this.TradeAcceptWhitelist.Add(new NameValueModel(this.NewWhiteListAccountName, this.NewWhiteListSteamId));
            SettingsProvider.GetInstance().TradeAcceptWhitelist = this.TradeAcceptWhitelist.ToList();
        }

        private void RemoveSelectedAccountButtonClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = (NameValueModel)this.WhiteListGrid.SelectedItem;
            if (selectedItem == null) return;

            this.TradeAcceptWhitelist.Remove(selectedItem);
            SettingsProvider.GetInstance().TradeAcceptWhitelist = this.TradeAcceptWhitelist.ToList();
        }

        private void StartTradeAcceptProcess(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            var modes = new List<ETradeAccepter>();

            if (AcceptIncomingEmpty) modes.Add(ETradeAccepter.AcceptIncomingEmpty);
            if (AcceptIncomingWhitelist) modes.Add(ETradeAccepter.AcceptIncomingWhitelist);
            if (DeclineSent) modes.Add(ETradeAccepter.DeclineOutgoing);
            if (DeclineIncomingNotEmpty) modes.Add(ETradeAccepter.DeclineIncomingNotEmpty);
            if (DeclineAllIncoming) modes.Add(ETradeAccepter.DeclineAllIncoming);

            if (modes.Any() == false)
            {
                ErrorNotify.CriticalMessageBox("Any checkbox selected!");
                return;
            }

            var seconds = this.TradeAcceptDelaySeconds;
            var timeSpanDelay = TimeSpan.FromSeconds(seconds);
            var wp = UiGlobalVariables.WorkingProcessDataContext;
            var whitelist = this.TradeAcceptWhitelist?.Select(a => a.Value) ?? new List<string>();
            var threadsCount = this.ThreadsCount;
            Task.Run(
                () =>
                    {
                        WorkingProcess.ProcessMethod(
                            () =>
                                {
                                    while (true)
                                    {
                                        if (wp.CancellationToken.IsCancellationRequested)
                                        {
                                            wp.AppendLog("TradeAccepter process was force stopped");
                                            return;
                                        }

                                        try
                                        {
                                            wp.AppendLog("Fetching trade offers..");

                                            var allOffers = UiGlobalVariables.SteamManager
                                                .ReceiveTradeOffers(true, true).Where(
                                                    o => o.Offer.TradeOfferState
                                                         == TradeOfferState.TradeOfferStateActive).ToList();

                                            var receivedOffers = allOffers.Where(o => o.Offer.IsOurOffer == false)
                                                .ToList();
                                            var sentOffers = allOffers.Where(o => o.Offer.IsOurOffer).ToList();

                                            if (allOffers.Any())
                                            {
                                                wp.AppendLog(
                                                    $"{allOffers.Count} trade offers found. Received - {receivedOffers.Count}, Sent - {sentOffers.Count})");
                                                foreach (var mode in modes)
                                                {
                                                    if (wp.CancellationToken.IsCancellationRequested)
                                                    {
                                                        wp.AppendLog("TradeAccepter process was force stopped");
                                                        return;
                                                    }

                                                    switch (mode)
                                                    {
                                                        case ETradeAccepter.AcceptIncomingEmpty:
                                                            wp.AppendLog(
                                                                "Processing incoming empty from my side offers to accept");
                                                            var incomingEmptyOffers = receivedOffers.Where(
                                                                o => o.MyItems.Any() == false);

                                                            wp.AppendLog($"{incomingEmptyOffers.Count()} offers found");

                                                            if (incomingEmptyOffers.Any())
                                                            {
                                                                UiGlobalVariables.SteamManager
                                                                    .AcceptTradeOffersWorkingProcess(
                                                                        incomingEmptyOffers,
                                                                        timeSpanDelay,
                                                                        threadsCount,
                                                                        wp);

                                                                RemoveProcessedOffers(
                                                                    receivedOffers,
                                                                    incomingEmptyOffers);
                                                            }

                                                            break;

                                                        case ETradeAccepter.AcceptIncomingWhitelist:
                                                            wp.AppendLog(
                                                                $"Processing incoming offers from {whitelist.Count()} whitelist users to accept");

                                                            var incomingWhiteListOffers = receivedOffers.Where(
                                                                o => whitelist.Contains(
                                                                    new SteamID(
                                                                            (uint)o.Offer.AccountIdOther,
                                                                            EUniverse.Public,
                                                                            EAccountType.Individual).ConvertToUInt64()
                                                                        .ToString()));

                                                            wp.AppendLog(
                                                                $"{incomingWhiteListOffers.Count()} offers found");

                                                            if (incomingWhiteListOffers.Any())
                                                            {
                                                                UiGlobalVariables.SteamManager
                                                                    .AcceptTradeOffersWorkingProcess(
                                                                        incomingWhiteListOffers,
                                                                        timeSpanDelay,
                                                                        threadsCount,
                                                                        wp);

                                                                UiGlobalVariables.SteamManager
                                                                    .ConfirmTradeTransactionsWorkingProcess(
                                                                        incomingWhiteListOffers.Select(
                                                                                o => ulong.Parse(o.Offer.TradeOfferId))
                                                                            .ToList(),
                                                                        wp);

                                                                RemoveProcessedOffers(
                                                                    receivedOffers,
                                                                    incomingWhiteListOffers);
                                                            }

                                                            break;

                                                        case ETradeAccepter.DeclineIncomingNotEmpty:
                                                            wp.AppendLog(
                                                                "Processing incoming not empty from my side offers to decline");
                                                            var incomingNotEmptyOffers = receivedOffers.Where(
                                                                o => o.MyItems.Any());

                                                            wp.AppendLog(
                                                                $"{incomingNotEmptyOffers.Count()} offers found");

                                                            if (incomingNotEmptyOffers.Any())
                                                            {
                                                                UiGlobalVariables.SteamManager
                                                                    .DeclineTradeOffersWorkingProcess(
                                                                        incomingNotEmptyOffers,
                                                                        timeSpanDelay,
                                                                        threadsCount,
                                                                        wp);

                                                                RemoveProcessedOffers(
                                                                    receivedOffers,
                                                                    incomingNotEmptyOffers);
                                                            }

                                                            break;

                                                        case ETradeAccepter.DeclineAllIncoming:
                                                            wp.AppendLog("Processing all incoming offers to decline");
                                                            wp.AppendLog($"{receivedOffers.Count} offers found");

                                                            if (receivedOffers.Any())
                                                            {
                                                                UiGlobalVariables.SteamManager
                                                                    .DeclineTradeOffersWorkingProcess(
                                                                        receivedOffers,
                                                                        timeSpanDelay,
                                                                        threadsCount,
                                                                        wp);

                                                                receivedOffers.Clear();
                                                            }

                                                            break;

                                                        case ETradeAccepter.DeclineOutgoing:
                                                            wp.AppendLog("Processing all outgoing offers to decline");
                                                            wp.AppendLog($"{sentOffers.Count} offers found");

                                                            if (sentOffers.Any())
                                                            {
                                                                UiGlobalVariables.SteamManager
                                                                    .DeclineTradeOffersWorkingProcess(
                                                                        sentOffers,
                                                                        timeSpanDelay,
                                                                        threadsCount,
                                                                        wp);

                                                                sentOffers.Clear();
                                                            }

                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (wp.CancellationToken.IsCancellationRequested)
                                                {
                                                    wp.AppendLog("TradeAccepter process was force stopped");
                                                    return;
                                                }

                                                wp.AppendLog("No active offers found");
                                                Thread.Sleep(timeSpanDelay);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            wp.AppendLog(
                                                $"Error on TradeAutoAccepter - {ex.Message}. Restarting TradeAutoAccepter working process.");
                                            Logger.Log.Error($"Error on TradeAutoAccepter - {ex.Message}.", ex);
                                            Thread.Sleep(timeSpanDelay);
                                        }
                                    }
                                },
                            "Trade accepter");
                    });
        }
    }
}