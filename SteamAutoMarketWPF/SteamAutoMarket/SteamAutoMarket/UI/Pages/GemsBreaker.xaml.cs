namespace SteamAutoMarket.UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Repository;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.SteamIntegration.InventoryLoad;
    using SteamAutoMarket.UI.Utils;
    using SteamAutoMarket.UI.Utils.ItemFilters;
    using SteamAutoMarket.UI.Utils.Logger;

    /// <summary>
    /// Interaction logic for GemsBreaker.xaml
    /// </summary>
    public partial class GemsBreaker : INotifyPropertyChanged
    {
        private ObservableCollection<GemsBreakerSteamItems> gemsBreakerItems =
            new ObservableCollection<GemsBreakerSteamItems>();

        private GemsBreakerSteamItems gemsBreakerSelectedItem;

        private IEnumerable<string> marketableFilters;

        private IEnumerable<string> rarityFilters;

        private IEnumerable<string> realGameFilters;

        private string totalListedItemsPrice = UiConstants.FractionalZeroString;

        private int totalSelectedItemsCount;

        private IEnumerable<string> tradabilityFilters;

        private IEnumerable<string> typeFilters;

        public GemsBreaker()
        {
            this.DataContext = this;
            this.InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<GemsBreakerSteamItems> GemsBreakerItems
        {
            get => this.gemsBreakerItems;
            set
            {
                this.gemsBreakerItems = value;
                this.OnPropertyChanged();
            }
        }

        public GemsBreakerSteamItems GemsBreakerSelectedItem
        {
            get => this.gemsBreakerSelectedItem;
            set
            {
                this.gemsBreakerSelectedItem = value;
                this.OnPropertyChanged();
            }
        }

        public IEnumerable<string> MarketableFilters
        {
            get => this.marketableFilters;
            set
            {
                this.marketableFilters = value;
                this.OnPropertyChanged();
            }
        }

        public string MarketableSelectedFilter { get; set; }

        public IEnumerable<string> RarityFilters
        {
            get => this.rarityFilters;
            set
            {
                this.rarityFilters = value;
                this.OnPropertyChanged();
            }
        }

        public string RaritySelectedFilter { get; set; }

        public IEnumerable<string> RealGameFilters
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

        public IEnumerable<string> TradabilityFilters
        {
            get => this.tradabilityFilters;
            set
            {
                this.tradabilityFilters = value;
                this.OnPropertyChanged();
            }
        }

        public string TradabilitySelectedFilter { get; set; }

        public IEnumerable<string> TypeFilters
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
            var resultView = this.GemsBreakerItems.ToList();

            SteamItemsFilters<GemsBreakerSteamItems>.RealGameFilter.Process(this.RealGameSelectedFilter, resultView);
            SteamItemsFilters<GemsBreakerSteamItems>.TypeFilter.Process(this.TypeSelectedFilter, resultView);
            SteamItemsFilters<GemsBreakerSteamItems>.RarityFilter.Process(this.RaritySelectedFilter, resultView);
            SteamItemsFilters<GemsBreakerSteamItems>.TradabilityFilter.Process(this.TradabilitySelectedFilter, resultView);
            SteamItemsFilters<GemsBreakerSteamItems>.MarketableFilter.Process(this.MarketableSelectedFilter, resultView);

            this.GemsBreakerGrid.ItemsSource = resultView;
        }

        private void Filter_OnDropDownOpened(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            switch (comboBox?.Name)
            {
                case "RealGameComboBox":
                    this.RealGameFilters =
                        FiltersFormatter<GemsBreakerSteamItems>.GetRealGameFilters(this.GemsBreakerItems);
                    break;

                case "TypeComboBox":
                    this.TypeFilters =
                        FiltersFormatter<GemsBreakerSteamItems>.GetTypeFilters(this.GemsBreakerItems);
                    break;

                case "RarityComboBox":
                    this.RarityFilters =
                        FiltersFormatter<GemsBreakerSteamItems>.GetRarityFilters(this.GemsBreakerItems);
                    break;

                case "TradabilityComboBox":
                    this.TradabilityFilters = FiltersFormatter<GemsBreakerSteamItems>.GetTradabilityFilters(this.GemsBreakerItems);
                    break;

                case "MarketableComboBox":
                    this.MarketableFilters = FiltersFormatter<GemsBreakerSteamItems>.GetMarketableFilters(this.GemsBreakerItems);
                    break;

                default:
                    Logger.Log.Error($"{comboBox?.Name} is unknown filter type");
                    return;
            }
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

        private void LoadSteamInventory_OnClick(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            this.GemsBreakerItems.Clear();
            this.ResetFilters();

            var wp = WorkingProcessProvider.GetNewInstance("STEAM inventory loading");
            wp?.StartWorkingProcess(
                () =>
                    {
                        wp.SteamManager.LoadInventoryWorkingProcess(
                            UiConstants.SteamAppId,
                            6,
                            this.GemsBreakerItems,
                            GemsBreakerInventoryProcessStrategy.GemsBreakerStrategy,
                            wp);
                    });
        }

        private void ResetFilters()
        {
            this.RealGameSelectedFilter = null;
            this.RealGameComboBox.Text = null;

            this.TypeSelectedFilter = null;
            this.TypeComboBox.Text = null;

            this.RaritySelectedFilter = null;
            this.RarityComboBox.Text = null;

            this.TradabilityFilters = null;
            this.TradabilityComboBox.Text = null;

            this.MarketableFilters = null;
            this.MarketableComboBox.Text = null;

            this.GemsBreakerGrid.ItemsSource = this.GemsBreakerItems;
        }
    }
}