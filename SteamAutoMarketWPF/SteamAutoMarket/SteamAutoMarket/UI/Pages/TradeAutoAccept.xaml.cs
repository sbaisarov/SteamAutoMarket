namespace SteamAutoMarket.UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    using SteamAutoMarket.Properties;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Models.Enums;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.Repository.Settings;
    using SteamAutoMarket.UI.Utils.Logger;

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
            var whitelist = this.TradeAcceptWhitelist?.Select(a => a.Value) ?? new List<string>();
            var threadsCount = this.ThreadsCount;
            var wp = WorkingProcessProvider.GetNewInstance("Trade accepter");
            wp?.StartWorkingProcess(
                () =>
                    {
                        wp.SteamManager.TradeAccepterWorkingProcess(wp, modes, threadsCount, timeSpanDelay, whitelist);
                    });
        }
    }
}