namespace SteamAutoMarket.UI.Pages
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using SteamAutoMarket.Properties;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.Repository.Settings;
    using SteamAutoMarket.UI.Utils;
    using SteamAutoMarket.UI.Utils.Logger;

    using Xceed.Wpf.DataGrid;
    using Xceed.Wpf.Toolkit;

    /// <summary>
    /// Interaction logic for TradeSend.xaml
    /// </summary>
    public partial class TradeSend : INotifyPropertyChanged
    {
        private SteamItemsModel tradeSendSelectedItem;

        public TradeSend()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<SteamAppId> AppIdList => SettingsProvider.GetInstance().AppIdList;

        public bool TradeSendConfirm2Fa
        {
            get => SettingsProvider.GetInstance().TradeSendConfirm2Fa;

            set
            {
                SettingsProvider.GetInstance().TradeSendConfirm2Fa = value;
                this.OnPropertyChanged();
            }
        }

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
            get => SettingsProvider.GetInstance().SelectedAppid;

            set
            {
                SettingsProvider.GetInstance().SelectedAppid = value;
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

            var onlyUnmarketable = this.TradeSendLoadOnlyUnmarketable;

            this.TradeSendItemsList.Clear();

            UiGlobalVariables.SteamManager.LoadItemsToTradeWorkingProcess(
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

        private void MarketSellMarkSelectedItemsClick(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < this.MarketItemsToTradeGrid.SelectedItems.Count; i++)
            {
                ((SteamItemsModel)this.MarketItemsToTradeGrid.SelectedItems[i]).NumericUpDown.SetToMaximum();
            }
        }

        private void MarketSellUnmarkAllItemsClick(object sender, RoutedEventArgs e)
        {
            var items = this.TradeSendItemsList.ToArray();
            foreach (var t in items)
            {
                t.NumericUpDown.AmountToSell = 0;
            }
        }

        private void SendTradeOfferButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            var steamId = this.TradeSteamIdTextBox.Text;
            if (string.IsNullOrEmpty(steamId))
            {
                ErrorNotify.CriticalMessageBox("Specified steam id is in incorrect format");
                return;
            }

            var tradeToken = this.TradeTokenTextBox.Text;
            if (string.IsNullOrEmpty(tradeToken))
            {
                ErrorNotify.CriticalMessageBox("Specified trade token is in incorrect format");
                return;
            }

            var itemsToSell = new List<FullRgItem>();

            foreach (var steamItemsModel in this.TradeSendItemsList.ToArray()
                .Where(i => i.NumericUpDown.AmountToSell > 0))
            {
                itemsToSell.AddRange(
                    steamItemsModel.ItemsList.ToList().GetRange(0, steamItemsModel.NumericUpDown.AmountToSell));
            }

            if (itemsToSell.Any() == false)
            {
                ErrorNotify.CriticalMessageBox("No items was marked to send! Mark items before starting trade send");
                return;
            }

            UiGlobalVariables.SteamManager.SendTrade(
                steamId,
                tradeToken,
                itemsToSell.ToArray(),
                this.TradeSendConfirm2Fa);
        }
    }
}