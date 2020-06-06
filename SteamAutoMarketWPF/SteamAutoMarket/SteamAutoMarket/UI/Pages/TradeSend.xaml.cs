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
    using SteamAutoMarket.Core;
    using SteamAutoMarket.Properties;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.Repository.Settings;
    using SteamAutoMarket.UI.SteamIntegration;
    using SteamAutoMarket.UI.SteamIntegration.InventoryLoad;
    using SteamAutoMarket.UI.Utils;
    using SteamAutoMarket.UI.Utils.ItemFilters;
    using SteamAutoMarket.UI.Utils.Logger;

    /// <summary>
    /// Interaction logic for TradeSend.xaml
    /// </summary>
    public partial class TradeSend : INotifyPropertyChanged
    {
        private bool isTotalPriceRefreshPlanned;

        private IEnumerable<string> rarityFilters;

        private IEnumerable<string> realGameFilters;

        private string totalListedItemsAveragePrice = UiConstants.FractionalZeroString;

        private string totalListedItemsCurrentPrice = UiConstants.FractionalZeroString;

        private int totalSelectedItemsCount;

        private IEnumerable<string> tradabilityFilters;

        private SteamItemsModel tradeSendSelectedItem;

        private IEnumerable<string> typeFilters;

        public TradeSend()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<SteamAppId> AppIdList => SettingsProvider.GetInstance().AppIdList;

        public IEnumerable<string> MarketableFilters
        {
            get => this.tradabilityFilters;
            set
            {
                this.tradabilityFilters = value;
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

        public string TotalListedItemsAveragePrice
        {
            get => this.totalListedItemsAveragePrice;
            set
            {
                this.totalListedItemsAveragePrice = value;
                this.OnPropertyChanged();
            }
        }

        public string TotalListedItemsCurrentPrice
        {
            get => this.totalListedItemsCurrentPrice;
            set
            {
                this.totalListedItemsCurrentPrice = value;
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

        public bool TradeSendConfirm2Fa
        {
            get => SettingsProvider.GetInstance().TradeSendConfirm2Fa;

            set
            {
                SettingsProvider.GetInstance().TradeSendConfirm2Fa = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<SteamItemsModel> TradeSendItemsList { get; } =
            new ObservableCollection<SteamItemsModel>();

        public string TradeSendNewAppid
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

                this.TradeSendSelectedAppid = steamAppId;
            }
            get => TradeSendSelectedAppid.Name;
        }

        public SteamAppId TradeSendSelectedAppid
        {
            get => SettingsProvider.GetInstance().SelectedAppid;

            set
            {
                SettingsProvider.GetInstance().SelectedAppid = value;
                this.OnPropertyChanged();
            }
        }

        public SteamItemsModel TradeSendSelectedItem
        {
            get => this.tradeSendSelectedItem;

            set
            {
                if (this.tradeSendSelectedItem == value) return;

                this.tradeSendSelectedItem = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<SettingsSteamAccount> TradeSteamUserList =>
            new ObservableCollection<SettingsSteamAccount>(SettingsProvider.GetInstance().SteamAccounts);

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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void AddOneToAllSelectedButtonClick(object sender, RoutedEventArgs e) =>
            FunctionalButtonsActions.AddPlusOneAmountToAllItems(
                this.MarketItemsToTradeGrid.SelectedItems.OfType<SteamItemsModel>());

        private void AmountToSend_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.RefreshSelectedItemsInfo();
        }

        private void ApplyFiltersButtonClick(object sender, RoutedEventArgs e)
        {
            var resultView = this.TradeSendItemsList.ToList();

            SteamItemsFilters<SteamItemsModel>.RealGameFilter.Process(this.RealGameSelectedFilter, resultView);
            SteamItemsFilters<SteamItemsModel>.TypeFilter.Process(this.TypeSelectedFilter, resultView);
            SteamItemsFilters<SteamItemsModel>.RarityFilter.Process(this.RaritySelectedFilter, resultView);
            SteamItemsFilters<SteamItemsModel>.MarketableFilter.Process(this.MarketableSelectedFilter, resultView);

            this.MarketItemsToTradeGrid.ItemsSource = resultView;
        }

        private void Filter_OnDropDownOpened(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            switch (comboBox?.Name)
            {
                case "RealGameComboBox":
                    this.RealGameFilters = FiltersFormatter<SteamItemsModel>.GetRealGameFilters(this.TradeSendItemsList);
                    break;

                case "TypeComboBox":
                    this.TypeFilters = FiltersFormatter<SteamItemsModel>.GetTypeFilters(this.TradeSendItemsList);
                    break;

                case "RarityComboBox":
                    this.RarityFilters = FiltersFormatter<SteamItemsModel>.GetRarityFilters(this.TradeSendItemsList);
                    break;

                case "MarketableComboBox":
                    this.MarketableFilters = FiltersFormatter<SteamItemsModel>.GetMarketableFilters(this.TradeSendItemsList);
                    break;

                default:
                    Logger.Log.Error($"{comboBox?.Name} is unknown filter type");
                    return;
            }
        }

        private void LoadItemsToTradeButtonClick(object sender, RoutedEventArgs e)
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

            this.TradeSendItemsList.Clear();
            this.ResetFilters();

            var wp = WorkingProcessProvider.GetNewInstance($"{this.TradeSendSelectedAppid.Name} inventory loading");
            wp?.StartWorkingProcess(
                () =>
                {
                    wp.SteamManager.LoadInventoryWorkingProcess(
                        this.TradeSendSelectedAppid,
                        contextId,
                        this.TradeSendItemsList,
                        TradeSendInventoryProcessStrategy.TradeSendStrategy,
                        wp);
                });
        }

        private void MarketSellMarkAllItemsClick(object sender, RoutedEventArgs e)
        {
            foreach (var t in this.MarketItemsToTradeGrid.ItemsSource)
            {
                ((SteamItemsModel)t).NumericUpDown.SetToMaximum();
            }
        }

        private void MarketSellMarkSelectedItemsClick(object sender, RoutedEventArgs e)
        {
            foreach (var t in this.MarketItemsToTradeGrid.SelectedItems)
            {
                ((SteamItemsModel)t).NumericUpDown.SetToMaximum();
            }
        }

        private void MarketSellUnmarkAllItemsClick(object sender, RoutedEventArgs e)
        {
            var items = this.TradeSendItemsList.ToArray();
            foreach (var t in items)
            {
                t.NumericUpDown.AmountToSell = 0;
            }
        }

        private void OpenOnSteamMarket_OnClick(object sender, RoutedEventArgs e) =>
            FunctionalButtonsActions.OpenOnSteamMarket(this.TradeSendSelectedItem);

        private void PriceTextBox_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.RefreshSelectedItemsInfo();
        }

        private void RefreshAllPricesPriceButton_OnClick(object sender, RoutedEventArgs e) =>
            GridPriceLoaderUtils.StartPriceLoading(
                (IEnumerable<SteamItemsModel>)this.MarketItemsToTradeGrid.ItemsSource);

        private void RefreshSelectedItemsInfo()
        {
            if (this.isTotalPriceRefreshPlanned) return;

            Task.Run(
                () =>
                {
                    this.isTotalPriceRefreshPlanned = true;
                    Thread.Sleep(300);
                    this.TotalSelectedItemsCount =
                        this.TradeSendItemsList?.Sum(m => m.NumericUpDown.AmountToSell) ?? 0;

                    this.TotalListedItemsCurrentPrice = this.TradeSendItemsList?.Sum(
                        m =>
                        {
                            if (m.NumericUpDown.AmountToSell == 0 || m.CurrentPrice.HasValue == false) return 0;

                            return m.NumericUpDown.AmountToSell * m.CurrentPrice.Value;
                        }).ToString(UiConstants.DoubleToStringFormat);

                    this.TotalListedItemsAveragePrice = this.TradeSendItemsList?.Sum(
                        m =>
                        {
                            if (m.NumericUpDown.AmountToSell == 0 || m.AveragePrice.HasValue == false) return 0;

                            return m.NumericUpDown.AmountToSell * m.AveragePrice.Value;
                        }).ToString(UiConstants.DoubleToStringFormat);

                    this.isTotalPriceRefreshPlanned = false;
                });
        }

        private void RefreshSinglePriceButton_OnClick(object sender, RoutedEventArgs e) =>
            GridPriceLoaderUtils.RefreshSingleModelPrice(this.TradeSendSelectedItem);

        private void ResetFilters()
        {
            this.RealGameSelectedFilter = null;
            this.RealGameComboBox.Text = null;

            this.TypeSelectedFilter = null;
            this.TypeComboBox.Text = null;

            this.RaritySelectedFilter = null;
            this.RarityComboBox.Text = null;

            this.MarketableFilters = null;
            this.MarketableComboBox.Text = null;

            this.MarketItemsToTradeGrid.ItemsSource = this.TradeSendItemsList;
        }

        private void ResetFiltersClick(object sender, RoutedEventArgs e)
        {
            this.ResetFilters();
        }

        private void SendTradeOfferButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            var steamId = this.TradeSteamIdTextBox.Text;
            if (string.IsNullOrEmpty(steamId))
            {
                ErrorNotify.CriticalMessageBox("Specified steam id is in incorrect format");
                return;
            }

            var tradeToken = this.TradeTokenTextBox.Text;
            if (string.IsNullOrEmpty(tradeToken))
            {
                ErrorNotify.CriticalMessageBox("Specified trade token is in incorrect format");
                return;
            }

            var itemsToSell = new List<FullRgItem>();

            foreach (var steamItemsModel in this.TradeSendItemsList.ToArray()
                .Where(i => i.NumericUpDown.AmountToSell > 0))
            {
                if (int.TryParse(steamItemsModel.ItemsList.FirstOrDefault()?.Asset.Amount, out var amount) && amount > 1)
                {
                    var itemToAdd = steamItemsModel.ItemsList.First().CloneAsset();
                    itemToAdd.Asset.Amount = steamItemsModel.NumericUpDown.AmountToSell.ToString();
                    itemsToSell.Add(itemToAdd);
                }
                else
                {
                    itemsToSell.AddRange(
                        steamItemsModel.ItemsList.ToList().GetRange(0, steamItemsModel.NumericUpDown.AmountToSell));
                }
            }

            if (itemsToSell.Any() == false)
            {
                ErrorNotify.CriticalMessageBox("No items was marked to send! Mark items before starting trade send");
                return;
            }

            var wp = WorkingProcessProvider.GetNewInstance("Trade send");
            wp?.StartWorkingProcess(
                () => wp.SteamManager.SendTrade(
                    steamId,
                    tradeToken,
                    itemsToSell.ToArray(),
                    this.TradeSendConfirm2Fa,
                    wp));
        }

        private void StopPriceLoadingButton_OnClick(object sender, RoutedEventArgs e) =>
            GridPriceLoaderUtils.InvokePriceLoadingStop();
    }
}
