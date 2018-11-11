namespace SteamAutoMarket.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Core;

    using Steam.Market.Models;

    using SteamAutoMarket.Annotations;

    public class MarketRelistModel : INotifyPropertyChanged
    {
        private double? averagePrice;

        private double? currentPrice;

        public MarketRelistModel(List<MyListingsSalesItem> itemsToSaleList)
        {
            this.ItemsList = new ObservableCollection<MyListingsSalesItem>(itemsToSaleList);

            this.ItemModel = itemsToSaleList.FirstOrDefault();

            this.Count = itemsToSaleList.Count;

            this.ItemName = this.ItemModel?.Name;

            this.Game = this.ItemModel?.Game;

            this.ListedPrice = this.ItemModel?.Price;

            this.ListedDate = this.ItemModel?.Date;

            this.Description = "TODO - GENERATE DESCRIPTION" + RandomUtils.RandomString(500);

            this.Checked = new CheckedModel();

            this.RelistPrice = new PriceModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public double? AveragePrice
        {
            get => this.averagePrice;
            set
            {
                this.averagePrice = value;
                this.ProcessSellPrice();
                this.OnPropertyChanged();
            }
        }

        public CheckedModel Checked { get; }

        public int Count { get; }

        public double? CurrentPrice
        {
            get => this.currentPrice;
            set
            {
                this.currentPrice = value;
                this.ProcessSellPrice();
                this.OnPropertyChanged();
            }
        }

        public string Description { get; }

        public string Game { get; }

        public string Image => null; //ImageProvider.GetItemImage(this.ItemModel?.HashName, this.ItemModel?.ImageUrl);

        public MyListingsSalesItem ItemModel { get; }

        public string ItemName { get; }

        public ObservableCollection<MyListingsSalesItem> ItemsList { get; }

        public string ListedDate { get; }

        public double? ListedPrice { get; }

        public PriceModel RelistPrice { get; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void ProcessSellPrice()
        {
            if (this.averagePrice != null || this.currentPrice != null)
            {
                this.RelistPrice.Value = 10;
            }
        }
    }
}