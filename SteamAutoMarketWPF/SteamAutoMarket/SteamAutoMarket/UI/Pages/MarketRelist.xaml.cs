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

    /// <summary>
    /// Interaction logic for MarketRelist.xaml
    /// </summary>
    public partial class MarketRelist : INotifyPropertyChanged
    {
        private readonly List<Task> priceLoadSubTasks = new List<Task>();

        private CancellationTokenSource cancellationTokenSource;

        private bool isTotalPriceRefreshPlanned;

        private MarketSellStrategy marketSellStrategy;

        private Task priceLoadingTask;

        private MarketRelistModel relistSelectedItem;

        private string totalListedItemsListedPrice = UiConstants.FractionalZeroString;

        private string totalListedItemsRelistPrice = UiConstants.FractionalZeroString;

        private int totalSelectedItemsCount;

        public MarketRelist()
        {
            this.InitializeComponent();
            this.DataContext = this;
            UiGlobalVariables.MarketRelist = this;

            this.marketSellStrategy = this.GetMarketSellStrategy();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<MarketRelistModel> MarketListedItemsList { get; } =
            new ObservableCollection<MarketRelistModel>();

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

        public MarketRelistModel RelistSelectedItem
        {
            get => this.relistSelectedItem;
            set
            {
                this.relistSelectedItem = value;
                this.OnPropertyChanged();
            }
        }

        public string TotalListedItemsListedPrice
        {
            get => this.totalListedItemsListedPrice;
            set
            {
                this.totalListedItemsListedPrice = value;
                this.OnPropertyChanged();
            }
        }

        public string TotalListedItemsRelistPrice
        {
            get => this.totalListedItemsRelistPrice;
            set
            {
                this.totalListedItemsRelistPrice = value;
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

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void CheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            this.RefreshSelectedItemsInfo();
        }

        private void FocusTextBox(int rowIndex)
        {
            var cell =
                (this.MarketItemsToSellGrid.GetContainerFromItem(this.MarketItemsToSellGrid.Items[rowIndex]) as DataRow)
                ?.Cells[8];
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
                    "Incorrect market relist strategy. Please check price formation radio buttons state");
                return null;
            }

            return sellStrategy;
        }

        private void LoadMarketListingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            this.MarketListedItemsList.Clear();
            var wp = WorkingProcessProvider.GetNewInstance("Market listings loading");
            wp?.StartWorkingProcess(() => { wp.SteamManager.LoadMarketListings(this.MarketListedItemsList, wp); });
        }

        private void MarkAllItemsButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.MarketListedItemsList)
            {
                item.Checked.CheckBoxChecked = true;
            }
        }

        private void MarkOverpricesButton_OnClick(object sender, RoutedEventArgs e)
        {
            var strategy = this.MarketSellStrategy;
            foreach (var item in this.MarketListedItemsList)
            {
                switch (strategy.SaleType)
                {
                    case EMarketSaleType.Manual:
                        item.Checked.CheckBoxChecked = item.RelistPrice.Value.HasValue;

                        break;

                    case EMarketSaleType.Recommended:
                        if (item.RelistPrice.Value.HasValue)
                        {
                            if (item.CurrentPrice > item.AveragePrice && item.CurrentPrice != item.ListedPrice)
                            {
                                item.Checked.CheckBoxChecked = true;
                            }
                            else if (item.CurrentPrice < item.AveragePrice && item.AveragePrice != item.ListedPrice)
                            {
                                item.Checked.CheckBoxChecked = true;
                            }
                            else
                            {
                                item.Checked.CheckBoxChecked = false;
                            }
                        }

                        break;

                    case EMarketSaleType.LowerThanCurrent:
                        if (item.RelistPrice.Value.HasValue && item.CurrentPrice != item.ListedPrice)
                        {
                            item.Checked.CheckBoxChecked = true;
                        }
                        else
                        {
                            item.Checked.CheckBoxChecked = false;
                        }

                        break;

                    case EMarketSaleType.LowerThanAverage:
                        if (item.RelistPrice.Value.HasValue
                            && item.AveragePrice + strategy.ChangeValue != item.ListedPrice)
                        {
                            item.Checked.CheckBoxChecked = true;
                        }
                        else
                        {
                            item.Checked.CheckBoxChecked = false;
                        }

                        break;

                    default:
                        ErrorNotify.CriticalMessageBox(
                            "Incorrect market relist strategy. Please check price formation radio buttons state");
                        return;
                }
            }
        }

        private void MarkSelectedItemsButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.MarketItemsToSellGrid.SelectedItems)
            {
                ((MarketRelistModel)item).Checked.CheckBoxChecked = true;
            }
        }

        private void OpenItemPageButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.RelistSelectedItem == null) return;

                Process.Start(
                    "https://" + $"steamcommunity.com/market/listings/{this.RelistSelectedItem.ItemModel.AppId}/"
                               + this.RelistSelectedItem.ItemModel.HashName);
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
            }
        }

        private void ReformatAllSellPrices()
        {
            var items = this.MarketListedItemsList.ToArray();
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

                            var items = this.MarketListedItemsList.ToList();
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
                                                item.ItemModel.AppId,
                                                item.ItemModel.HashName);

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
                                                item.ItemModel.AppId,
                                                item.ItemModel.HashName,
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
                                    Logger.Log.Debug("Market relist price loading was force stopped");
                                    return;
                                }
                            }

                            this.WaitForPriceLoadingSubTasksEnd();
                            Logger.Log.Debug("Market relist price loading task is finished");
                        }
                        catch (Exception ex)
                        {
                            ErrorNotify.CriticalMessageBox("Error on items price update", ex);
                        }
                    },
                this.cancellationTokenSource.Token);
        }

        private void RefreshSelectedItemsInfo()
        {
            if (this.isTotalPriceRefreshPlanned) return;

            Task.Run(
                () =>
                    {
                        this.isTotalPriceRefreshPlanned = true;
                        Thread.Sleep(300);
                        this.TotalSelectedItemsCount =
                            this.MarketListedItemsList?.Where(m => m.Checked.CheckBoxChecked).Sum(m => m.Count) ?? 0;

                        this.TotalListedItemsListedPrice = this.MarketListedItemsList
                            ?.Where(m => m.Checked.CheckBoxChecked).Sum(
                                m =>
                                    {
                                        if (m.ListedPrice.HasValue == false) return 0;
                                        return m.Count * m.ListedPrice.Value;
                                    }).ToString(UiConstants.DoubleToStringFormat);

                        this.TotalListedItemsRelistPrice = this.MarketListedItemsList
                            ?.Where(m => m.Checked.CheckBoxChecked).Sum(
                                m =>
                                    {
                                        if (m.RelistPrice.Value.HasValue == false) return 0;
                                        return m.Count * m.RelistPrice.Value.Value;
                                    }).ToString(UiConstants.DoubleToStringFormat);

                        this.isTotalPriceRefreshPlanned = false;
                    });
        }

        private void RefreshSinglePriceButton_OnClick(object sender, RoutedEventArgs e)
        {
            var task = Task.Run(
                () =>
                    {
                        var item = this.RelistSelectedItem;
                        if (item == null) return;
                        try
                        {
                            item.CleanItemPrices();

                            var averagePriceDays = SettingsProvider.GetInstance().AveragePriceDays;

                            var price = UiGlobalVariables.SteamManager.GetCurrentPrice(
                                item.ItemModel.AppId,
                                item.ItemModel.HashName);

                            Logger.Log.Debug($"Current price for {item.ItemName} is - {price}");

                            item.CurrentPrice = price;

                            price = UiGlobalVariables.SteamManager.GetAveragePrice(
                                item.ItemModel.AppId,
                                item.ItemModel.HashName,
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

        private void RelistTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            this.RefreshSelectedItemsInfo();
        }

        private void RelistTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.RefreshSelectedItemsInfo();
        }

        private void StartRelistButton_OnClick(object sender, RoutedEventArgs e)
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
                        var itemsToSell = this.MarketListedItemsList.ToArray().Where(i => i.Checked.CheckBoxChecked)
                            .ToArray();

                        if (itemsToSell.Sum(i => i.Count) == 0)
                        {
                            ErrorNotify.CriticalMessageBox(
                                "No items was marked to relist! Mark items before starting relist process");
                            return;
                        }

                        var wp = WorkingProcessProvider.GetNewInstance("Market relist");
                        wp?.StartWorkingProcess(
                            () =>
                                {
                                    wp.SteamManager.RelistListings(
                                        this.priceLoadSubTasks.ToArray(),
                                        itemsToSell,
                                        this.MarketSellStrategy,
                                        this.MarketListedItemsList,
                                        wp);
                                });
                    });
        }

        private void StartRemoveButton_OnClick(object sender, RoutedEventArgs e)
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
                        var itemsToSell = this.MarketListedItemsList.ToArray().Where(i => i.Checked.CheckBoxChecked)
                            .ToArray();

                        if (itemsToSell.Sum(i => i.Count) == 0)
                        {
                            ErrorNotify.CriticalMessageBox(
                                "No items was marked to relist! Mark items before starting relist process");
                            return;
                        }

                        var wp = WorkingProcessProvider.GetNewInstance("Market listings remove");
                        wp?.StartWorkingProcess(
                            () =>
                                {
                                    wp.SteamManager.RemoveListings(
                                        this.priceLoadSubTasks.ToArray(),
                                        itemsToSell,
                                        this.MarketSellStrategy,
                                        this.MarketListedItemsList,
                                        wp);
                                });
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
                Logger.Log.Debug("No active market relist price loading task found. Nothing to stop");
                return;
            }

            Logger.Log.Debug("Active market relist price loading task found. Trying to force stop it");
            this.cancellationTokenSource.Cancel();
            new Waiter().Until(() => this.priceLoadingTask.IsCompleted);
            this.cancellationTokenSource = new CancellationTokenSource();
        }

        private void UnmarkAllItemsButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.MarketListedItemsList)
            {
                item.Checked.CheckBoxChecked = false;
            }
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