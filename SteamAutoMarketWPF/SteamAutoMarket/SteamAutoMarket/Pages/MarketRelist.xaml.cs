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

        public MarketRelist()
        {
            this.InitializeComponent();
            this.DataContext = this;
            UiGlobalVariables.MarketRelist = this;
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

        private void MarkAllItemsButtonClick(object sender, RoutedEventArgs e)
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

        private void ReformatSellStrategyOnControlStateChanged(object sender, RoutedEventArgs e)
        {
            // todo
        }

        private void RefreshAllPricesPriceButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RefreshSinglePriceButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StopPriceLoadingButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MarkOverpricesButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}