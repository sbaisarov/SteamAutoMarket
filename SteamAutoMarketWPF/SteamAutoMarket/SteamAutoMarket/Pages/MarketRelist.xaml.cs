namespace SteamAutoMarket.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

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

        private static readonly Dictionary<string, List<MyListingsSalesItem>> MyListings =
            new Dictionary<string, List<MyListingsSalesItem>>();

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

        public ObservableCollection<MarketRelistModel> RelistItemsList { get; } =
            new ObservableCollection<MarketRelistModel>();

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
            try
            {
                MyListings.Clear();
                /*Task.Run(
                    () =>
                        {
                            // get listings var listings = 

                            var groupedListings = listings?.Sales?.GroupBy(x => new { x.HashName, x.Price });
                            if (groupedListings == null)
                            {
                                return;
                            }

                            foreach (var group in groupedListings)
                            {
                                var item = group.FirstOrDefault();
                                if (item == null)
                                {
                                    return;
                                }
                            }

                            WorkingProcessForm.PriceLoader.StartPriceLoading(ETableToLoad.RelistTable);
                        });*/
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error on loading listed market items", ex);
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