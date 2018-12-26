namespace SteamAutoMarket.UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.Core.Waiter;
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

            UiGlobalVariables.SteamManager.LoadItemsToSaleWorkingProcess(
                this.MarketSellSelectedAppid,
                contextId,
                this.MarketSellItems);
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

        private void OpenOnSteamMarket_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.MarketSellSelectedItem == null) return;

                Process.Start(
                    "https://"
                    + $"steamcommunity.com/market/listings/{this.MarketSellSelectedItem.ItemModel.Asset.Appid}/"
                    + this.MarketSellSelectedItem.ItemModel.Description.MarketHashName);
            }
            catch (Exception ex)
            {
                ErrorNotify.CriticalMessageBox(ex);
            }
        }

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

                case Key.Right:
                    this.FocusNumericUpDown(currentIndex);
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

        private void RefreshAllPricesPriceButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.priceLoadingTask?.IsCompleted == false)
            {
                ErrorNotify.InfoMessageBox("Price loading is already in progress");
                return;
            }

            this.cancellationTokenSource = new CancellationTokenSource();
            this.priceLoadingTask = Task.Run(
                () =>
                    {
                        try
                        {
                            Logger.Log.Debug("Starting market sell price loading task");

                            var items = ((IEnumerable<MarketSellModel>)this.MarketItemsToSellGrid.ItemsSource).ToList();
                            items.ForEach(i => i.CleanItemPrices());

                            var averagePriceDays = SettingsProvider.GetInstance().AveragePriceDays;
                            var sellStrategy = this.MarketSellStrategy;

                            var priceLoadingSemaphore = new Semaphore(
                                SettingsProvider.GetInstance().PriceLoadingThreads,
                                SettingsProvider.GetInstance().PriceLoadingThreads);

                            foreach (var item in items)
                            {
                                priceLoadingSemaphore.WaitOne();
                                Logger.Log.Debug($"Processing price for {item.ItemName}");

                                var task = Task.Run(
                                    () =>
                                        {
                                            var price = UiGlobalVariables.SteamManager.GetCurrentPriceWithCache(
                                                item.ItemModel.Asset.Appid,
                                                item.ItemModel.Description.MarketHashName);

                                            Logger.Log.Debug($"Current price for {item.ItemName} is - {price}");
                                            item.CurrentPrice = price;
                                            item.ProcessSellPrice(sellStrategy);
                                            priceLoadingSemaphore.Release();
                                        },
                                    this.cancellationTokenSource.Token);
                                this.priceLoadSubTasks.Add(task);

                                priceLoadingSemaphore.WaitOne();
                                task = Task.Run(
                                    () =>
                                        {
                                            var price = UiGlobalVariables.SteamManager.GetAveragePriceWithCache(
                                                item.ItemModel.Asset.Appid,
                                                item.ItemModel.Description.MarketHashName,
                                                averagePriceDays);

                                            Logger.Log.Debug(
                                                $"Average price for {averagePriceDays} days for {item.ItemName} is - {price}");

                                            item.AveragePrice = price;
                                            item.ProcessSellPrice(sellStrategy);
                                            priceLoadingSemaphore.Release();
                                        },
                                    this.cancellationTokenSource.Token);
                                this.priceLoadSubTasks.Add(task);

                                if (this.cancellationTokenSource.Token.IsCancellationRequested)
                                {
                                    this.WaitForPriceLoadingSubTasksEnd();
                                    Logger.Log.Debug("Market sell price loading was force stopped");
                                    return;
                                }
                            }

                            this.WaitForPriceLoadingSubTasksEnd();
                            Logger.Log.Debug("Market sell price loading task is finished");
                        }
                        catch (Exception ex)
                        {
                            ErrorNotify.CriticalMessageBox("Error on items price update", ex);
                        }
                    },
                this.cancellationTokenSource.Token);
        }

        private void RefreshSinglePriceButton_OnClick(object sender, RoutedEventArgs e)
        {
            var task = Task.Run(
                () =>
                    {
                        var item = this.MarketSellSelectedItem;
                        if (item == null) return;
                        try
                        {
                            item.CleanItemPrices();

                            var averagePriceDays = SettingsProvider.GetInstance().AveragePriceDays;

                            var price = UiGlobalVariables.SteamManager.GetCurrentPrice(
                                item.ItemModel.Asset.Appid,
                                item.ItemModel.Description.MarketHashName);

                            Logger.Log.Debug($"Current price for {item.ItemName} is - {price}");

                            item.CurrentPrice = price;

                            price = UiGlobalVariables.SteamManager.GetAveragePrice(
                                item.ItemModel.Asset.Appid,
                                item.ItemModel.Description.MarketHashName,
                                averagePriceDays);

                            Logger.Log.Debug(
                                $"Average price for {averagePriceDays} days for {item.ItemName} is - {price}");

                            item.AveragePrice = price;

                            item.ProcessSellPrice(this.MarketSellStrategy);
                        }
                        catch (Exception ex)
                        {
                            ErrorNotify.CriticalMessageBox("Error on item price update", ex);
                        }
                    });

            this.priceLoadSubTasks.Add(task);
        }

        private void ResetFiltersClick(object sender, RoutedEventArgs e)
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

        private void StartMarketSellButtonClick_OnClick(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            Task.Run(() => this.StopPriceLoadingTasks());

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

                        UiGlobalVariables.SteamManager.SellOnMarketWorkingProcess(
                            this.priceLoadSubTasks.ToArray(),
                            itemsToSell,
                            this.MarketSellStrategy);
                    });
        }

        private void StopPriceLoadingButton_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => this.StopPriceLoadingTasks());
        }

        private void StopPriceLoadingTasks()
        {
            if (this.cancellationTokenSource == null || this.priceLoadingTask == null
                                                     || this.priceLoadingTask.IsCompleted)
            {
                Logger.Log.Debug("No active market sell price loading task found. Nothing to stop");
                return;
            }

            Logger.Log.Debug("Active market sell price loading task found. Trying to force stop it");
            this.cancellationTokenSource.Cancel();
            new Waiter().Until(() => this.priceLoadingTask.IsCompleted);
            this.cancellationTokenSource = new CancellationTokenSource();
        }

        private void WaitForPriceLoadingSubTasksEnd()
        {
            if (this.priceLoadSubTasks.Any(t => t.IsCompleted == false))
            {
                Logger.Log.Debug(
                    $"Waiting for {this.priceLoadSubTasks.Count(t => t.IsCompleted == false)} price loading threads to finish");
                new Waiter().Until(() => this.priceLoadSubTasks.All(t => t.IsCompleted));
            }

            this.priceLoadSubTasks.Clear();
        }
    }
}