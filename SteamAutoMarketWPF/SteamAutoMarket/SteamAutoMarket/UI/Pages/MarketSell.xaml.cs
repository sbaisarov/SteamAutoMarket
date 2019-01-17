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

    using SteamAutoMarket.Properties;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Models.Enums;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.Repository.Settings;
    using SteamAutoMarket.UI.SteamIntegration;
    using SteamAutoMarket.UI.Utils;
    using SteamAutoMarket.UI.Utils.Logger;

    using Xceed.Wpf.DataGrid;
    using Xceed.Wpf.Toolkit;

    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class MarketSell : INotifyPropertyChanged
    {
        private readonly List<Task> priceLoadSubTasks = new List<Task>();

        private CancellationTokenSource cancellationTokenSource;

        private MarketSellModel marketSellSelectedItem;

        private MarketSellStrategy marketSellStrategy;

        private Task priceLoadingTask;

        private List<string> rarityFilters;

        private List<string> realGameFilters;

        private List<string> tradabilityFilters;

        private List<string> typeFilters;

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
                if (SettingsProvider.GetInstance().SelectedAppid == value) return;
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

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void AddOneToAllSelectedButtonClick(object sender, RoutedEventArgs e) =>
            FunctionalButtonsActions.AddPlusOneAmountToAllItems(
                (IEnumerable<MarketSellModel>)this.MarketItemsToSellGrid.SelectedItems);

        private void ApplyFiltersButtonClick(object sender, RoutedEventArgs e)
        {
            var resultView = this.MarketSellItems.ToList();

            if (this.RealGameSelectedFilter != null)
            {
                resultView = resultView.Where(g => g.Game == this.RealGameSelectedFilter).ToList();
            }

            if (this.TypeSelectedFilter != null)
            {
                resultView = resultView.Where(g => g.Type == this.TypeSelectedFilter).ToList();
            }

            if (this.RaritySelectedFilter != null)
            {
                resultView = resultView.Where(
                    model => model.ItemModel?.Description?.Tags
                                 ?.FirstOrDefault(tag => tag.LocalizedCategoryName == "Rarity")?.LocalizedTagName
                             == this.RaritySelectedFilter).ToList();
            }

            if (this.TradabilitySelectedFilter != null)
            {
                bool? value = null;
                if (this.TradabilitySelectedFilter == "Tradable") value = true;
                else if (this.TradabilitySelectedFilter == "Not tradable") value = false;
                if (value != null)
                {
                    resultView = resultView.Where(g => g.ItemModel.Description.IsTradable == value).ToList();
                }
            }

            this.MarketItemsToSellGrid.ItemsSource = resultView;
        }

        private void Filter_OnDropDownOpened(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            switch (comboBox?.Name)
            {
                case "RealGameComboBox":
                    this.RealGameFilters = this.MarketSellItems.Select(model => model.Game).ToHashSet().OrderBy(q => q)
                        .ToList();
                    break;
                case "TypeComboBox":
                    this.TypeFilters = this.MarketSellItems.Select(model => model.Type).ToHashSet().OrderBy(q => q)
                        .ToList();
                    break;
                case "RarityComboBox":
                    this.RarityFilters = this.MarketSellItems.Select(
                            model => model.ItemModel?.Description?.Tags
                                ?.FirstOrDefault(tag => tag.LocalizedCategoryName == "Rarity")?.LocalizedTagName)
                        .ToHashSet().OrderBy(q => q).ToList();
                    break;
                case "TradabilityComboBox":
                    this.TradabilityFilters = this.MarketSellItems.Select(
                            model => (model.ItemModel?.Description.IsTradable == true) ? "Tradable" : "Not tradable")
                        .ToHashSet().ToList();
                    break;
            }
        }

        private void FocusNumericUpDown(int rowIndex)
        {
            var cell =
                (this.MarketItemsToSellGrid.GetContainerFromItem(this.MarketItemsToSellGrid.Items[rowIndex]) as DataRow)
                ?.Cells[6];
            if (cell != null)
            {
                var integerUpDown = UiElementsUtils.FindVisualChild<IntegerUpDown>(cell);
                var textBoxView = UiElementsUtils.FindVisualChild<WatermarkTextBox>(integerUpDown);
                textBoxView?.Focus();
                textBoxView?.SelectAll();
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

            if (int.TryParse(this.MarketContextIdTextBox.Text, out var contextId) == false)
            {
                ErrorNotify.CriticalMessageBox($"Incorrect context id provided - {contextId}");
                return;
            }

            this.MarketSellItems.Clear();
            this.ResetFilters();

            var wp = WorkingProcessProvider.GetNewInstance($"{this.MarketSellSelectedAppid.Name} inventory loading");
            wp?.StartWorkingProcess(
                () =>
                    {
                        wp.SteamManager.LoadItemsToSaleWorkingProcess(
                            this.MarketSellSelectedAppid,
                            contextId,
                            this.MarketSellItems,
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
                                        this.priceLoadSubTasks.ToArray(),
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