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

    using SteamAutoMarket.Properties;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.Repository.Settings;
    using SteamAutoMarket.UI.SteamIntegration;
    using SteamAutoMarket.UI.Utils.Logger;

    /// <summary>
    /// Interaction logic for TradeSend.xaml
    /// </summary>
    public partial class TradeSend : INotifyPropertyChanged
    {
        private List<string> rarityFilters;

        private List<string> realGameFilters;

        private List<string> tradabilityFilters;

        private SteamItemsModel tradeSendSelectedItem;

        private List<string> typeFilters;

        public TradeSend()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<SteamAppId> AppIdList => SettingsProvider.GetInstance().AppIdList;

        public List<string> MarketableFilters
        {
            get => this.tradabilityFilters;
            set
            {
                this.tradabilityFilters = value;
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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void AddOneToAllSelectedButtonClick(object sender, RoutedEventArgs e) =>
            FunctionalButtonsActions.AddPlusOneAmountToAllItems(
                (IEnumerable<SteamItemsModel>)this.MarketItemsToTradeGrid.SelectedItems);

        private void ApplyFiltersButtonClick(object sender, RoutedEventArgs e)
        {
            var resultView = this.TradeSendItemsList.ToList();

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

            if (this.MarketableSelectedFilter != null)
            {
                bool? value = null;
                if (this.MarketableSelectedFilter == "Marketable") value = true;
                else if (this.MarketableSelectedFilter == "Not marketable") value = false;
                if (value != null)
                {
                    resultView = resultView.Where(g => g.ItemModel.Description.IsMarketable == value).ToList();
                }
            }

            this.MarketItemsToTradeGrid.ItemsSource = resultView;
        }

        private void Filter_OnDropDownOpened(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            switch (comboBox?.Name)
            {
                case "RealGameComboBox":
                    this.RealGameFilters = this.TradeSendItemsList.Select(model => model.Game).ToHashSet()
                        .OrderBy(q => q).ToList();
                    break;
                case "TypeComboBox":
                    this.TypeFilters = this.TradeSendItemsList.Select(model => model.Type).ToHashSet().OrderBy(q => q)
                        .ToList();
                    break;
                case "RarityComboBox":
                    this.RarityFilters = this.TradeSendItemsList.Select(
                            model => model.ItemModel?.Description?.Tags
                                ?.FirstOrDefault(tag => tag.LocalizedCategoryName == "Rarity")?.LocalizedTagName)
                        .ToHashSet().OrderBy(q => q).ToList();
                    break;
                case "MarketableComboBox":
                    this.MarketableFilters = this.TradeSendItemsList.Select(
                            model => (model.ItemModel?.Description.IsMarketable == true)
                                         ? "Marketable"
                                         : "Not marketable")
                        .ToHashSet().ToList();
                    break;
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
                        wp.SteamManager.LoadItemsToTradeWorkingProcess(
                            this.TradeSendSelectedAppid,
                            contextId,
                            this.TradeSendItemsList,
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

        private void RefreshAllPricesPriceButton_OnClick(object sender, RoutedEventArgs e) =>
            GridPriceLoaderUtils.StartPriceLoading((IEnumerable<SteamItemsModel>)this.MarketItemsToTradeGrid.ItemsSource);

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
                itemsToSell.AddRange(
                    steamItemsModel.ItemsList.ToList().GetRange(0, steamItemsModel.NumericUpDown.AmountToSell));
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