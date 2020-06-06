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
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.UI.Repository.Image;

    [Serializable]
    public class SteamTradeItemsModel : INotifyPropertyChanged
    {
        private string image;

        public SteamTradeItemsModel(IEnumerable<FullTradeItem> itemsList)
        {
            IEnumerable<FullTradeItem> fullTradeItems = itemsList as FullTradeItem[] ?? itemsList.ToArray();

            this.ItemsList = new ObservableCollection<FullTradeItem>(fullTradeItems);

            this.ItemModel = fullTradeItems.FirstOrDefault();

            this.Count = fullTradeItems.Sum(i => int.Parse(i.Asset.Amount));

            this.ItemName = this.ItemModel?.Description.MarketName;

            this.Type = SteamUtils.GetClearItemType(this.ItemModel?.Description.Type);

            this.Description = new Lazy<string>(() => SteamUtils.GetClearDescription(this.ItemModel));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Count { get; }

        public Lazy<string> Description { get; }

        public string Image
        {
            get
            {
                if (this.image != null) return this.image;

                var imageHashName = this.ItemModel?.Description?.MarketHashName;
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
                            this.ItemModel?.Description?.IconUrlLarge ?? this.ItemModel?.Description?.IconUrl);

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

        public FullTradeItem ItemModel { get; }

        public string ItemName { get; }

        public ObservableCollection<FullTradeItem> ItemsList { get; }

        public string Type { get; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}