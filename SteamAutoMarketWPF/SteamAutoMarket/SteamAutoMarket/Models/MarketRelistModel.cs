namespace SteamAutoMarket.Models
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Core;

    using Steam.Market.Models;

    using SteamAutoMarket.Annotations;
    using SteamAutoMarket.Models.Enums;
    using SteamAutoMarket.Repository.Image;
    using SteamAutoMarket.SteamIntegration;

    public class MarketRelistModel : INotifyPropertyChanged
    {
        private double? averagePrice;

        private double? currentPrice;

        private string image;

        private int count;

        public MarketRelistModel(MyListingsSalesItem[] itemsToSaleList)
        {
            this.ItemsList = new ObservableCollection<MyListingsSalesItem>(itemsToSaleList);

            this.Count = itemsToSaleList.Length;

            this.ItemModel = itemsToSaleList.FirstOrDefault();

            this.ItemName = this.ItemModel?.Name;

            this.Game = SteamUtils.GetClearItemType(this.ItemModel?.Game);

            this.ListedPrice = this.ItemModel?.Price;

            this.ListedDate = this.ItemModel?.Date;

            this.Description = SteamUtils.GetClearDescription(this.ItemModel);

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
                this.OnPropertyChanged();
            }
        }

        public CheckedModel Checked { get; }

        public int Count
        {
            get => this.count;
            private set
            {
                if (value == this.count) return;
                this.count = value;
                this.OnPropertyChanged();
            }
        }

        public double? CurrentPrice
        {
            get => this.currentPrice;
            set
            {
                this.currentPrice = value;
                this.OnPropertyChanged();
            }
        }

        public string Description { get; }

        public string Game { get; }

        public string Image
        {
            get
            {
                if (this.image != null) return this.image;

                this.image = ImageProvider.GetItemImage(
                    a => this.Image = a,
                    this.ItemModel?.HashName,
                    this.ItemModel?.ImageUrl);

                return this.image;
            }

            set
            {
                this.image = value;
                this.OnPropertyChanged();
            }
        }

        public MyListingsSalesItem ItemModel { get; }

        public string ItemName { get; }

        public ObservableCollection<MyListingsSalesItem> ItemsList { get; }

        public string ListedDate { get; }

        public double? ListedPrice { get; }

        public PriceModel RelistPrice { get; }

        public void CleanItemPrices()
        {
            this.CurrentPrice = null;
            this.AveragePrice = null;
            this.RelistPrice.Value = null;
        }

        public void ProcessSellPrice(MarketSellStrategy strategy)
        {
            switch (strategy.SaleType)
            {
                case EMarketSaleType.Recommended:
                    {
                        if (this.CurrentPrice == null || this.AveragePrice == null)
                        {
                            this.RelistPrice.Value = null;
                        }
                        else if (this.CurrentPrice > this.AveragePrice)
                        {
                            if (this.CurrentPrice == this.ListedPrice)
                            {
                                this.RelistPrice.Value = this.CurrentPrice;
                            }
                            else
                            {
                                this.RelistPrice.Value = this.CurrentPrice - 0.01;
                            }
                        }
                        else
                        {
                            if (this.CurrentPrice == this.ListedPrice)
                            {
                                this.RelistPrice.Value = this.CurrentPrice;
                            }
                            else
                            {
                                this.RelistPrice.Value = this.AveragePrice - 0.01;
                            }
                        }

                        break;
                    }

                case EMarketSaleType.LowerThanCurrent:
                    {
                        if (this.CurrentPrice == null)
                        {
                            this.RelistPrice.Value = null;
                        }
                        else
                        {
                            if (this.CurrentPrice == this.ListedPrice)
                            {
                                this.RelistPrice.Value = this.CurrentPrice;
                            }
                            else
                            {
                                this.RelistPrice.Value = this.CurrentPrice + strategy.ChangeValue;
                            }
                        }

                        break;
                    }

                case EMarketSaleType.LowerThanAverage:
                    {
                        if (this.AveragePrice == null)
                        {
                            this.RelistPrice.Value = null;
                        }
                        else if (this.AveragePrice - 0.1 == this.ListedPrice)
                        {
                            this.RelistPrice.Value = this.AveragePrice;
                        }
                        else
                        {
                            this.RelistPrice.Value = this.AveragePrice + strategy.ChangeValue;
                        }

                        break;
                    }

                default:
                    {
                        this.RelistPrice.Value = null;
                        return;
                    }
            }
        }

        public void RefreshCount()
        {
            this.Count = this.ItemsList.Count;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}