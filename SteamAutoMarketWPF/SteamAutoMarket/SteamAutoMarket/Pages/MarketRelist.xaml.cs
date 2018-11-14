namespace SteamAutoMarket.Pages
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    using SteamAutoMarket.Annotations;
    using SteamAutoMarket.Models;
    using SteamAutoMarket.Repository.Context;
    using SteamAutoMarket.Utils.Logger;

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
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            var form = WorkingProcessForm.NewWorkingProcessWindow("Market listings loading");

            this.RelistItemsList.Clear();

            UiGlobalVariables.SteamManager.LoadMarketListings(form, this.RelistItemsList);
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

        private void ReformatSellStrategyOnControlStateChanged(object sender, RoutedEventArgs e)
        {
            // todo
        }

        private void RefreshAllPricesPriceButton_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void RefreshSinglePriceButton_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void StopPriceLoadingButton_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void MarkOverpricesButton_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void MarkAllItemsButtonClick(object sender, RoutedEventArgs e)
        {
        }
    }
}