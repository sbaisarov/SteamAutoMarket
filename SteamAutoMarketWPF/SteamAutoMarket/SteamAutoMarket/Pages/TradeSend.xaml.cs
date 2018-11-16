namespace SteamAutoMarket.Pages
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;

    using SteamAutoMarket.Annotations;
    using SteamAutoMarket.Models;
    using SteamAutoMarket.Repository.Context;
    using SteamAutoMarket.Repository.Settings;
    using SteamAutoMarket.Utils.Logger;

    /// <summary>
    /// Interaction logic for TradeSend.xaml
    /// </summary>
    public partial class TradeSend : INotifyPropertyChanged
    {
        private SteamAppId tradeSendSelectedAppid;

        private SteamItemsModel tradeSendSelectedItem;

        public TradeSend()
        {
            this.InitializeComponent();
            this.DataContext = this;

            this.TradeSendSelectedAppid = this.AppIdList.FirstOrDefault(
                appid => appid?.Name == SettingsProvider.GetInstance().TradeSendSelectedAppid?.Name);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<SteamAppId> AppIdList => SettingsProvider.GetInstance().AppIdList;

        public ObservableCollection<SteamItemsModel> TradeSendItemsList { get; } =
            new ObservableCollection<SteamItemsModel>();

        public bool TradeSendLoadOnlyUnmarketable
        {
            get => SettingsProvider.GetInstance().TradeSendLoadOnlyUnmarketable;
            set
            {
                SettingsProvider.GetInstance().TradeSendLoadOnlyUnmarketable = value;
                this.OnPropertyChanged();
            }
        }

        public string TradeSendNewAppid
        {
            set
            {
                if (string.IsNullOrEmpty(value) || !int.TryParse(value, out var longValue))
                {
                    return;
                }

                var steamAppId = this.AppIdList.FirstOrDefault(e => e.AppId == longValue);
                if (steamAppId == null)
                {
                    steamAppId = new SteamAppId(longValue);
                    this.AppIdList.Add(steamAppId);
                }

                this.TradeSendSelectedAppid = steamAppId;
            }
        }

        public SteamAppId TradeSendSelectedAppid
        {
            get => SettingsProvider.GetInstance().TradeSendSelectedAppid;

            set
            {
                if (this.tradeSendSelectedAppid == value) return;
                SettingsProvider.GetInstance().TradeSendSelectedAppid = value;
                this.OnPropertyChanged();
            }
        }

        public SteamItemsModel TradeSendSelectedItem
        {
            get => this.tradeSendSelectedItem;

            set
            {
                if (this.tradeSendSelectedItem == value) return;
                this.tradeSendSelectedItem = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<SettingsSteamAccount> TradeSteamUserList =>
            new ObservableCollection<SettingsSteamAccount>(SettingsProvider.GetInstance().SteamAccounts);

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void LoadItemsToTradeButtonClick(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            if (int.TryParse(this.MarketContextIdTextBox.Text, out var contextId) == false)
            {
                ErrorNotify.CriticalMessageBox($"Incorrect context id provided - {contextId}");
                return;
            }

            var form = WorkingProcessForm.NewWorkingProcessWindow(
                $"{this.TradeSendSelectedAppid.Name} inventory loading");

            var onlyUnmarketable = this.TradeSendLoadOnlyUnmarketable;

            this.TradeSendItemsList.Clear();

            UiGlobalVariables.SteamManager.LoadItemsToTradeWorkingProcess(
                form,
                this.TradeSendSelectedAppid,
                contextId,
                this.TradeSendItemsList,
                onlyUnmarketable);
        }

        private void MarketSellMarkAllItemsClick(object sender, RoutedEventArgs e)
        {
            var items = this.TradeSendItemsList.ToArray();
            foreach (var t in items)
            {
                t.NumericUpDown.SetToMaximum();
            }
        }

        private void SendTradeOfferButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            Task.Run(
                () =>
                    {
                        var itemsToSell = this.TradeSendItemsList.ToArray().Where(i => i.NumericUpDown.AmountToSell > 0)
                            .SelectMany(i => i.ItemsList).ToArray();

                        if (itemsToSell.Any() == false)
                        {
                            ErrorNotify.CriticalMessageBox(
                                "No items was marked to send! Mark items before starting trade send");
                            return;
                        }

                        var form = WorkingProcessForm.NewWorkingProcessWindow("Trade send");

                        UiGlobalVariables.SteamManager.SendTrade(form, itemsToSell, false);
                    });
        }
    }
}