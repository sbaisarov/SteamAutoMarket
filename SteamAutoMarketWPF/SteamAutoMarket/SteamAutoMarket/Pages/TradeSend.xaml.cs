namespace SteamAutoMarket.Pages
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    using Core;

    using global::Steam.TradeOffer.Models;
    using global::Steam.TradeOffer.Models.Full;

    using SteamAutoMarket.Annotations;
    using SteamAutoMarket.Models;
    using SteamAutoMarket.Repository.Settings;

    /// <summary>
    /// Interaction logic for TradeSend.xaml
    /// </summary>
    public partial class TradeSend : INotifyPropertyChanged
    {
        private SteamAppId tradeSendSelectedAppid;

        private TradeSendModel tradeSendSelectedItem;

        public TradeSend()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<TradeSendModel> TradeSendItemsList { get; } =
            new ObservableCollection<TradeSendModel>();

        public List<SteamAppId> AppIdList => SettingsProvider.GetInstance().AppIdList;

        public SteamAppId TradeSendSelectedAppid
        {
            get => this.tradeSendSelectedAppid;

            set
            {
                if (this.tradeSendSelectedAppid == value) return;
                this.tradeSendSelectedAppid = value;
                this.OnPropertyChanged();
            }
        }

        public TradeSendModel TradeSendSelectedItem
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

        private void LoadInventoryButtonClick(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < 10; i++)
            {
                this.TradeSendItemsList.Add(
                    new TradeSendModel(
                        new List<FullRgItem>
                            {
                                new FullRgItem
                                    {
                                        Asset = new RgInventory { Amount = $"{RandomUtils.RandomInt(1, 100)}", },
                                        Description = new RgDescription
                                                          {
                                                              Type = RandomUtils.RandomString(15),
                                                              MarketHashName = RandomUtils.RandomString(15),
                                                              MarketName = RandomUtils.RandomString(15),
                                                              IconUrlLarge =
                                                                  "https://steamcommunity-a.akamaihd.net/economy/image/IzMF03bk9WpSBq-S-ekoE33L-iLqGFHVaU25ZzQNQcXdA3g5gMEPvUZZEaiHLrVJRsl8q3CWTo7Qi89ehDNVzDMFfXqviiQrcex4NM6b5gni6vaCV2D_bXzpIC7XWldlHOQLOG7cqzKs4rmSEDCYQu4pEl9SKPcB9zZBPpvbOxpvgIcNrTDgxhQkS0RmYstBNg202HAWI4IsxSAVIJYEzyKkd8bQhFhgOUViDOq0Au2WbNCmkSgmXExvG_RJNdiWv3Puq5-ndLfYeu5xafCepGZ_Rg/360fx360f"
                                                          }
                                    }
                            }));
            }
        }

        private void MarketSellMarkAllItemsClick(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < this.TradeSendItemsList.Count; i++)
            {
                this.TradeSendItemsList[i].NumericUpDown.SetToMaximum();
            }
        }
    }
}