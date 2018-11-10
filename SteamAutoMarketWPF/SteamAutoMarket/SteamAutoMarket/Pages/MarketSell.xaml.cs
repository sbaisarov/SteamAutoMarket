namespace SteamAutoMarket.Pages
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    using Steam.TradeOffer.Models;
    using Steam.TradeOffer.Models.Full;

    using SteamAutoMarket.Models;
    using SteamAutoMarket.Properties;
    using SteamAutoMarket.Repository.Context;
    using SteamAutoMarket.Repository.Settings;
    using SteamAutoMarket.Utils.Logger;

    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class MarketSell : INotifyPropertyChanged
    {
        #region Private variables

        private MarketSellModel marketSellSelectedItem;

        private SteamAppId marketSellSelectedAppid = SettingsProvider.GetInstance().MarketSellSelectedAppid;

        #endregion

        public MarketSell()
        {
            this.InitializeComponent();
            this.DataContext = this;
            UiGlobalVariables.MarketSellPage = this;

            this.MarketSellSelectedAppid = this.AppIdList.FirstOrDefault(
                appid => appid?.Name == SettingsProvider.GetInstance().MarketSellSelectedAppid?.Name);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<SteamAppId> AppIdList => SettingsProvider.GetInstance().AppIdList;

        public SteamAppId MarketSellSelectedAppid
        {
            get => this.marketSellSelectedAppid;

            set
            {
                if (this.marketSellSelectedAppid == value) return;
                this.marketSellSelectedAppid = value;
                SettingsProvider.GetInstance().MarketSellSelectedAppid = value;
                this.OnPropertyChanged();
            }
        }

        public MarketSellModel MarketSellSelectedItem
        {
            get => this.marketSellSelectedItem;

            set
            {
                if (this.marketSellSelectedItem == value) return;
                this.marketSellSelectedItem = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<MarketSellModel> MarketSellItems { get; } =
            new ObservableCollection<MarketSellModel>();

        public string MarketSellNewAppid
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

                this.MarketSellSelectedAppid = steamAppId;
            }
        }

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void MarketSellMarkAllItemsClick(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < this.MarketSellItems.Count; i++)
            {
                this.MarketSellItems[i].MarketSellNumericUpDown.SetToMaximum();
            }
        }

        private void StartMarketSellButtonClick_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void LoadInventoryItems(object sender, RoutedEventArgs e)
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
                $"{this.MarketSellSelectedAppid.Name} inventory loading");

            this.MarketSellItems.Clear();

            UiGlobalVariables.SteamManager.LoadItemsToSaleWorkingProcess(
                form,
                this.MarketSellSelectedAppid,
                contextId,
                this.MarketSellItems);
        }
    }
}