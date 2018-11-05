namespace SteamAutoMarket.Pages
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    using Core;

    using Steam.Market.Models;

    using SteamAutoMarket.Annotations;
    using SteamAutoMarket.Models;
    using SteamAutoMarket.Repository.Context;

    /// <summary>
    /// Interaction logic for MarketRelist.xaml
    /// </summary>
    public partial class MarketRelist : INotifyPropertyChanged
    {
        private ObservableCollection<MarketRelistModel> relistItemsList = new ObservableCollection<MarketRelistModel>();

        private MarketRelistModel relistSelectedItem;

        public MarketRelist()
        {
            this.InitializeComponent();
            this.DataContext = this;

            UiGlobalVariables.MarketRelist = this;

            this.RelistItemsList.Add(
                new MarketRelistModel(
                    new List<MyListingsSalesItem>
                        {
                            new MyListingsSalesItem
                                {
                                    AppId = 123,
                                    Date = "21-07-1996",
                                    Game = "123",
                                    HashName = "7555-Card",
                                    ImageUrl =
                                        "https://steamcommunity-a.akamaihd.net/economy/image/IzMF03bk9WpSBq-S-ekoE33L-iLqGFHVaU25ZzQNQcXdA3g5gMEPvUZZEaiHLrVJRsl8q3CWTo7Qi89ehDNVzDMAe3eriSQrcex4NM6b4wTppfHLFXn2bzKZf3LcS11rGbRfMz2I-mf35LuVRTqYFL59SwgCL6MDp2EcNc6JOBds1ZlLpWL-lUtvGhM6TcxLcQi-l3BGYuVzmXARI8kGmCbyIsGLgwlmbkZuWbizVe3LPNX2kikhWB02FqYEJNXCrmPh-WVoRmmi/360fx360f",
                                    Name = "Card name",
                                    Price = 12.1,
                                    SaleId = 1111,
                                    Url = "https://steamcommunity.com/market/listings/753/615340-Cards?filter=card"
                                }
                        }));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<MarketRelistModel> RelistItemsList
        {
            get => this.relistItemsList;
            set
            {
                this.relistItemsList = value;
                this.OnPropertyChanged();
            }
        }

        public MarketRelistModel RelistSelectedItem
        {
            get => this.relistSelectedItem;
            set
            {
                this.relistSelectedItem = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void LoadMarketListingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                this.RelistItemsList.Add(
                    new MarketRelistModel(
                        new List<MyListingsSalesItem>
                            {
                                new MyListingsSalesItem
                                    {
                                        AppId = RandomUtils.RandomInt(0,700),
                                        Date = RandomUtils.RandomString(10),
                                        Game = RandomUtils.RandomString(3),
                                        HashName = RandomUtils.RandomString(10),
                                        ImageUrl =
                                            "https://steamcommunity-a.akamaihd.net/economy/image/IzMF03bk9WpSBq-S-ekoE33L-iLqGFHVaU25ZzQNQcXdA3g5gMEPvUZZEaiHLrVJRsl8q3CWTo7Qi89ehDNVzDMAe3eriSQrcex4NM6b4wTppfHLFXn2bzKZf3LcS11rGbRfMz2I-mf35LuVRTqYFL59SwgCL6MDp2EcNc6JOBds1ZlLpWL-lUtvGhM6TcxLcQi-l3BGYuVzmXARI8kGmCbyIsGLgwlmbkZuWbizVe3LPNX2kikhWB02FqYEJNXCrmPh-WVoRmmi/360fx360f",
                                        Name = RandomUtils.RandomString(10),
                                        Price = RandomUtils.RandomInt(0, 10),
                                        SaleId = RandomUtils.RandomInt(0, 10),
                                        Url = "https://steamcommunity.com/market/listings/753/615340-Cards?filter=card"
                                    }
                            }));
            }
        }

        private void RelistMarkAllItemsButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.RelistItemsList)
            {
                item.Checked.CheckBoxChecked = true;
            }
        }

        private void StartRelistButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.RelistItemsList)
            {
                item.RelistPrice.Value = 123;
            }
        }
    }
}