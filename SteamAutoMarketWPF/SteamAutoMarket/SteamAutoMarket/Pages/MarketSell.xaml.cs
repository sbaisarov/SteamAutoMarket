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

            this.MarketSellItems.Add(
                new MarketSellModel(
                    new List<FullRgItem>
                        {
                            new FullRgItem
                                {
                                    Asset = new RgInventory { Amount = "5", },
                                    Description = new RgDescription
                                                      {
                                                          Type = "Type1",
                                                          MarketHashName = "HashName",
                                                          MarketName = "MarketName",
                                                          IconUrlLarge =
                                                              "https://steamcommunity-a.akamaihd.net/economy/image/IzMF03bk9WpSBq-S-ekoE33L-iLqGFHVaU25ZzQNQcXdA3g5gMEPvUZZEaiHLrVJRsl8q3CWTo7Qi89ehDNVzDMFfXqviiQrcex4NM6b5gni6vaCV2D_bXzpIC7XWldlHOQLOG7cqzKs4rmSEDCYQu4pEl9SKPcB9zZBPpvbOxpvgIcNrTDgxhQkS0RmYstBNg202HAWI4IsxSAVIJYEzyKkd8bQhFhgOUViDOq0Au2WbNCmkSgmXExvG_RJNdiWv3Puq5-ndLfYeu5xafCepGZ_Rg/360fx360f"
                                                      }
                                }
                        }));

            this.MarketSellItems.Add(
                new MarketSellModel(
                    new List<FullRgItem>
                        {
                            new FullRgItem
                                {
                                    Asset = new RgInventory { Amount = "4" },
                                    Description = new RgDescription
                                                      {
                                                          Type = "Type2",
                                                          MarketHashName = "HashName1",
                                                          MarketName = "SomeName",
                                                          IconUrl =
                                                              "https://steamcommunity-a.akamaihd.net/economy/image/IzMF03bk9WpSBq-S-ekoE33L-iLqGFHVaU25ZzQNQcXdA3g5gMEPvUZZEaiHLrVJRsl8q3CWTo7Qi89ehDNVzDMFfXqviiQrcex4NM6b5gni6vaCV2D_bXzpIC7XWldlHOQLOG7cqzKs4rmSEDCYQu4pEl9SKPcB9zZBPpvbOxpvgIcNrTDgxhQkS0RmYstBNg202HAWI4IsxSAVIJYEzyKkd8bQhFhgOUViDOq0Au2WbNCmkSgmXExvG_RJNdiWv3Puq5-ndLfYeu5xafCepGZ_Rg/360fx360f"
                                                      }
                                }
                        }));
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

        public ObservableCollection<MarketSellModel> MarketSellItems { get; } = new ObservableCollection<MarketSellModel>();

        public string MarketSellNewAppid
        {
            set
            {
                if (string.IsNullOrEmpty(value) || !long.TryParse(value, out var longValue))
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
            WorkingProcessExample.Test();
        }
    }
}