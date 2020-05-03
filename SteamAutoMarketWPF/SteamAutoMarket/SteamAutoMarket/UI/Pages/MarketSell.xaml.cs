namespace SteamAutoMarket.UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using SteamAutoMarket.Core;
    using SteamAutoMarket.Properties;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Models.Enums;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.Repository.Settings;
    using SteamAutoMarket.UI.SteamIntegration;
    using SteamAutoMarket.UI.SteamIntegration.InventoryLoad;
    using SteamAutoMarket.UI.Utils;
    using SteamAutoMarket.UI.Utils.ItemFilters;
    using SteamAutoMarket.UI.Utils.Logger;
    using Xceed.Wpf.DataGrid;

    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class MarketSell : INotifyPropertyChanged
    {
        private bool isTotalPriceRefreshPlanned;

        private MarketSellModel marketSellSelectedItem;

        private MarketSellStrategy marketSellStrategy;

        private IEnumerable<string> rarityFilters;

        private IEnumerable<string> realGameFilters;

        private string totalListedItemsPrice = UiConstants.FractionalZeroString;

        private int totalSelectedItemsCount;

        private IEnumerable<string> tradabilityFilters;

        private IEnumerable<string> typeFilters;

        public MarketSell()
        {
            this.InitializeComponent();
            this.DataContext = this;
            UiGlobalVariables.MarketSellPage = this;

            this.marketSellStrategy = this.GetMarketSellStrategy();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<SteamAppId> AppIdList => SettingsProvider.GetInstance().AppIdList;

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

        public SteamAppId MarketSellSelectedAppid
        {
            get => SettingsProvider.GetInstance().SelectedAppid;

            set
            {
                var appid = SettingsProvider.GetInstance().SelectedAppid;
                if (appid != null && appid.Equals(value))
                {
                    return;
                }

                SettingsProvider.GetInstance().SelectedAppid = value;
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

        public MarketSellStrategy MarketSellStrategy
        {
            get => this.marketSellStrategy;
            set
            {
                if (this.marketSellStrategy != null && this.marketSellStrategy.Equals(value)) return;

                this.marketSellStrategy = value;
                this.ReformatAllSellPrices();
            }
        }

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

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void AddOneToAllSelectedButtonClick(object sender, RoutedEventArgs e) =>
            FunctionalButtonsActions.AddPlusOneAmountToAllItems(
                this.MarketItemsToSellGrid.SelectedItems.OfType<MarketSellModel>());

        private void ApplyFiltersButtonClick(object sender, RoutedEventArgs e)
        {
            var resultView = this.MarketSellItems.ToList();

            SteamItemsFilters<MarketSellModel>.RealGameFilter.Process(this.RealGameSelectedFilter, resultView);
            SteamItemsFilters<MarketSellModel>.TypeFilter.Process(this.TypeSelectedFilter, resultView);
            SteamItemsFilters<MarketSellModel>.RarityFilter.Process(this.RaritySelectedFilter, resultView);
            SteamItemsFilters<MarketSellModel>.TradabilityFilter.Process(this.TradabilitySelectedFilter, resultView);

            this.MarketItemsToSellGrid.ItemsSource = resultView;
        }

        private void Filter_OnDropDownOpened(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            switch (comboBox?.Name)
            {
                case "RealGameComboBox":
                    this.RealGameFilters = FiltersFormatter<MarketSellModel>.GetRealGameFilters(this.MarketSellItems);
                    break;

                case "TypeComboBox":
                    this.TypeFilters = FiltersFormatter<MarketSellModel>.GetTypeFilters(this.MarketSellItems);
                    break;

                case "RarityComboBox":
                    this.RarityFilters = FiltersFormatter<MarketSellModel>.GetRarityFilters(this.MarketSellItems);
                    break;

                case "TradabilityComboBox":
                    this.TradabilityFilters = FiltersFormatter<MarketSellModel>.GetTradabilityFilters(this.MarketSellItems);
                    break;

                default:
                    Logger.Log.Error($"{comboBox?.Name} is unknown filter type");
                    return;
            }
        }

        private void FocusTextBox(int rowIndex)
        {
            var cell =
                (this.MarketItemsToSellGrid.GetContainerFromItem(this.MarketItemsToSellGrid.Items[rowIndex]) as DataRow)
                ?.Cells[5];
            if (cell != null)
            {
                var textBox = UiElementsUtils.FindVisualChild<TextBox>(cell);
                textBox?.Focus();
                textBox?.SelectAll();
            }
        }

        private MarketSellStrategy GetMarketSellStrategy()
        {
            var sellStrategy = new MarketSellStrategy();

            if (this.ManualPriceRb?.IsChecked != null && this.ManualPriceRb.IsChecked == true)
            {
                sellStrategy.SaleType = EMarketSaleType.Manual;
            }
            else if (this.RecommendedPriceRb?.IsChecked != null && this.RecommendedPriceRb.IsChecked == true)
            {
                sellStrategy.SaleType = EMarketSaleType.Recommended;
            }
            else if (this.CurrentPriceRb?.IsChecked != null && this.CurrentPriceRb.IsChecked == true)
            {
                sellStrategy.SaleType = EMarketSaleType.LowerThanCurrent;
                sellStrategy.ChangeValue = this.CurrentPriceNumericUpDown?.Value ?? 0;
            }
            else if (this.AveragePriceRb?.IsChecked != null && this.AveragePriceRb.IsChecked == true)
            {
                sellStrategy.SaleType = EMarketSaleType.LowerThanAverage;
                sellStrategy.ChangeValue = this.AveragePriceNumericUpDown?.Value ?? 0;
            }
            else
            {
                ErrorNotify.CriticalMessageBox(
                    "Incorrect market sell strategy. Please check price formation radio buttons state");
                return null;
            }

            return sellStrategy;
        }

        private void LoadInventoryItems(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            var selectedValue = ((SteamAppId)this.MarketAppidCombobox.SelectedValue);

            if (selectedValue == null)
            {
                ErrorNotify.CriticalMessageBox($"Inventory type is not selected");
                return;
            }

            if (int.TryParse(this.MarketContextIdTextBox.Text, out var contextId) == false)
            {
                ErrorNotify.CriticalMessageBox($"Incorrect context id provided - {contextId}");
                return;
            }

            this.MarketSellItems.Clear();
            this.ResetFilters();

            var wp = WorkingProcessProvider.GetNewInstance($"{selectedValue.Name} inventory loading");
            wp?.StartWorkingProcess(
                () =>
                {
                    wp.SteamManager.LoadInventoryWorkingProcess(
                        selectedValue,
                        contextId,
                        this.MarketSellItems,
                        MarketSellInventoryProcessStrategy.MarketSellStrategy,
                        wp);
                });
        }

        private void MarketSellMarkAllItemsClick(object sender, RoutedEventArgs e)
        {
            foreach (var t in this.MarketItemsToSellGrid.ItemsSource)
            {
                ((MarketSellModel)t).NumericUpDown.SetToMaximum();
            }
        }

        private void MarketSellMarkSelectedItemsClick(object sender, RoutedEventArgs e)
        {
            foreach (var t in this.MarketItemsToSellGrid.SelectedItems)
            {
                ((MarketSellModel)t).NumericUpDown.SetToMaximum();
            }
        }

        private void MarketSellUnmarkAllItemsClick(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < this.MarketSellItems.Count; i++)
            {
                this.MarketSellItems[i].NumericUpDown.AmountToSell = 0;
            }
        }

        private void OpenOnSteamMarket_OnClick(object sender, RoutedEventArgs e) =>
            FunctionalButtonsActions.OpenOnSteamMarket(this.MarketSellSelectedItem);

        private void PriceTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var currentIndex = this.MarketItemsToSellGrid.SelectedIndex;

            switch (e.Key)
            {
                case Key.Down:
                    if (currentIndex + 1 == this.MarketItemsToSellGrid.Items.Count) return;

                    this.FocusTextBox(currentIndex + 1);
                    return;

                case Key.Up:
                    if (currentIndex == 0) return;

                    this.FocusTextBox(currentIndex - 1);
                    return;
            }
        }

        private void ReformatAllSellPrices()
        {
            var items = this.MarketSellItems.ToArray();
            foreach (var item in items)
            {
                item.ProcessSellPrice(this.MarketSellStrategy);
            }
        }

        private void ReformatSellStrategyOnControlStateChanged(object sender, RoutedEventArgs e)
        {
            this.MarketSellStrategy = this.GetMarketSellStrategy();
        }

        private void RefreshAllPricesPriceButton_OnClick(object sender, RoutedEventArgs e) =>
            GridPriceLoaderUtils.StartPriceLoading(
                (IEnumerable<MarketSellModel>)this.MarketItemsToSellGrid.ItemsSource,
                this.MarketSellStrategy);

        public void RefreshSelectedItemsInfo()
        {
            if (this.isTotalPriceRefreshPlanned) return;

            Task.Run(
                () =>
                {
                    this.isTotalPriceRefreshPlanned = true;
                    Thread.Sleep(300);
                    this.TotalSelectedItemsCount =
                        this.MarketSellItems?.Sum(m => m.NumericUpDown.AmountToSell) ?? 0;

                    var allItemsToSellWithPrice = this.MarketSellItems
                        ?.Where(m => m.NumericUpDown.AmountToSell != 0 && m.SellPrice.Value.HasValue)
                        .ToArray();

                    // ReSharper disable once PossibleInvalidOperationException
                    if (allItemsToSellWithPrice != null && allItemsToSellWithPrice.Any())
                    {
                        this.TotalListedItemsPrice = allItemsToSellWithPrice.Sum(
                                m => m.NumericUpDown.AmountToSell
                                    * double.Parse(UiGlobalVariables.SteamManager.MarketClient.GetSellingSteamPriceWithoutFee(m.SellPrice.Value.Value)) / 100)
                            .ToString(UiConstants.DoubleToStringFormat);
                    }
                    else
                    {
                        this.TotalListedItemsPrice = UiConstants.FractionalZeroString;
                    }

                    this.isTotalPriceRefreshPlanned = false;
                });
        }

        private void RefreshSinglePriceButton_OnClick(object sender, RoutedEventArgs e) =>
            GridPriceLoaderUtils.RefreshSingleModelPrice(this.MarketSellSelectedItem, this.MarketSellStrategy);

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

            this.MarketItemsToSellGrid.ItemsSource = this.MarketSellItems;
        }

        private void ResetFiltersClick(object sender, RoutedEventArgs e)
        {
            this.ResetFilters();
        }

        private void SelectedItemsUpDownBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.RefreshSelectedItemsInfo();
        }

        private void StartMarketSellButtonClick_OnClick(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            GridPriceLoaderUtils.InvokePriceLoadingStop();

            Task.Run(
                () =>
                {
                    var itemsToSell = this.MarketSellItems.ToArray().Where(i => i.NumericUpDown.AmountToSell > 0)
                        .Select(i => new MarketSellProcessModel(i)).Where(i => i.Count > 0).ToArray();

                    if (itemsToSell.Sum(i => i.Count) == 0)
                    {
                        ErrorNotify.CriticalMessageBox(
                            "No items was marked to sell! Mark items before starting sell process");
                        return;
                    }

                    var wp = WorkingProcessProvider.GetNewInstance("Market sell");
                    wp?.StartWorkingProcess(
                        () =>
                        {
                            wp.SteamManager.SellOnMarketWorkingProcess(
                                GridPriceLoaderUtils.PriceLoadSubTasks.ToArray(),
                                itemsToSell,
                                this.MarketSellStrategy,
                                this.MarketSellItems,
                                wp);
                        });
                });
        }

        private void StopPriceLoadingButton_OnClick(object sender, RoutedEventArgs e) =>
            GridPriceLoaderUtils.InvokePriceLoadingStop();
    }
}
