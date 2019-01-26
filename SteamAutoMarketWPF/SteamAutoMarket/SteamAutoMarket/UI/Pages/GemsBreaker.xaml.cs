namespace SteamAutoMarket.UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Repository;

    /// <summary>
    /// Interaction logic for GemsBreaker.xaml
    /// </summary>
    public partial class GemsBreaker : UserControl, INotifyPropertyChanged
    {
        private ObservableCollection<GemsBreakSteamItemsModel> gemsBreakerItems;

        private GemsBreakSteamItemsModel gemsBreakerSelectedItem;

        private List<string> marketableFilters;

        private List<string> rarityFilters;

        private List<string> realGameFilters;

        private string totalListedItemsPrice = UiConstants.FractionalZeroString;

        private int totalSelectedItemsCount;

        private List<string> tradabilityFilters;

        private List<string> typeFilters;

        public GemsBreaker()
        {
            this.DataContext = this;
            this.InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<GemsBreakSteamItemsModel> GemsBreakerItems
        {
            get => this.gemsBreakerItems;
            set
            {
                this.gemsBreakerItems = value;
                this.OnPropertyChanged();
            }
        }

        public GemsBreakSteamItemsModel GemsBreakerSelectedItem
        {
            get => this.gemsBreakerSelectedItem;
            set
            {
                this.gemsBreakerSelectedItem = value;
                this.OnPropertyChanged();
            }
        }

        public List<string> MarketableFilters
        {
            get => this.marketableFilters;
            set
            {
                this.marketableFilters = value;
                this.OnPropertyChanged();
            }
        }

        public string MarketableSelectedFilter { get; set; }

        public List<string> RarityFilters
        {
            get => this.rarityFilters;
            set
            {
                this.rarityFilters = value;
                this.OnPropertyChanged();
            }
        }

        public string RaritySelectedFilter { get; set; }

        public List<string> RealGameFilters
        {
            get => this.realGameFilters;
            set
            {
                this.realGameFilters = value;
                this.OnPropertyChanged();
            }
        }

        public string RealGameSelectedFilter { get; set; }

        public string TotalListedItemsPrice
        {
            get => this.totalListedItemsPrice;
            set
            {
                this.totalListedItemsPrice = value;
                this.OnPropertyChanged();
            }
        }

        public int TotalSelectedItemsCount
        {
            get => this.totalSelectedItemsCount;
            set
            {
                this.totalSelectedItemsCount = value;
                this.OnPropertyChanged();
            }
        }

        public List<string> TradabilityFilters
        {
            get => this.tradabilityFilters;
            set
            {
                this.tradabilityFilters = value;
                this.OnPropertyChanged();
            }
        }

        public string TradabilitySelectedFilter { get; set; }

        public List<string> TypeFilters
        {
            get => this.typeFilters;
            set
            {
                this.typeFilters = value;
                this.OnPropertyChanged();
            }
        }

        public string TypeSelectedFilter { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void AddOneToAllSelectedButtonClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ApplyFiltersButtonClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Filter_OnDropDownOpened(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MarkAllItemsClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MarkSelectedItemsClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OpenOnSteamMarket_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RefreshAllGemsCountPriceButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RefreshSingleGemsCountButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ResetFiltersClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SelectedItemsUpDownBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            throw new NotImplementedException();
        }

        private void StartGemsBreakButtonClick_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StopGemsCountLoadingButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UnmarkAllItemsClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}