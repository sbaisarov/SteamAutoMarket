namespace SteamAutoMarket.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using SteamAutoMarket.Properties;
    using SteamAutoMarket.Steam;
    using SteamAutoMarket.Steam.Market.Models;
    using SteamAutoMarket.UI.Models.Enums;
    using SteamAutoMarket.UI.Repository.Image;
    using SteamAutoMarket.UI.SteamIntegration;

    [Serializable]
    public class MarketRelistModel : INotifyPropertyChanged
    {
        private double? averagePrice;

        private int count;

        private double? currentPrice;

        private string image;

        public MarketRelistModel(IReadOnlyCollection<MyListingsSalesItem> itemsToSaleList)
        {
            this.ItemsList = new ObservableCollection<MyListingsSalesItem>(itemsToSaleList);

            this.Count = itemsToSaleList.Count;

            this.ItemModel = itemsToSaleList.FirstOrDefault();

            this.ItemName = this.ItemModel?.Name;

            this.Game = SteamUtils.GetClearItemType(this.ItemModel?.Game);

            this.ListedPrice = this.ItemModel?.Price;

            this.ListedDate = this.ItemModel?.Date;

            this.Description = new Lazy<string>(() => SteamUtils.GetClearDescription(this.ItemModel));

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

        public Lazy<string> Description { get; }

        public string Game { get; }

        public bool IsImageNotLoaded => this.image == null;

        public string Image
        {
            get
            {
                if (this.image != null) return this.image;

                var imageHashName = this.ItemModel?.HashName;
                if (ImageCache.IsImageCached(imageHashName))
                {
                    ImageCache.TryGetImage(imageHashName, out this.image);

                    // ReSharper disable once ExplicitCallerInfoArgument
                    this.OnPropertyChanged("IsImageNotLoaded");
                    return this.image;
                }

                Task.Run(
                    () =>
                    {
                        var downloadedImage = ImageProvider.GetItemImage(
                            imageHashName,
                            this.ItemModel?.ImageUrl);

                        this.image = downloadedImage;
                        this.OnPropertyChanged();

                        // ReSharper disable once ExplicitCallerInfoArgument
                        this.OnPropertyChanged("IsImageNotLoaded");
                    });

                return null;
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
                            this.RelistPrice.Value = this.CurrentPrice;
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
                            this.RelistPrice.Value = this.AveragePrice;
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
                    else if (this.AveragePrice == this.ListedPrice)
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
