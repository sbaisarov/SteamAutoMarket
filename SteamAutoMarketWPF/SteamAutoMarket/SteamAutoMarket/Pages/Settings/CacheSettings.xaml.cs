namespace SteamAutoMarket.Pages.Settings
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    using SteamAutoMarket.Annotations;

    /// <summary>
    /// Interaction logic for CacheSettings.xaml
    /// </summary>
    public partial class CacheSettings : INotifyPropertyChanged
    {
        private int obsoleteCurrentPricesCount;

        private int currentPricesCount;

        private int obsoleteAveragePricesCount;

        private int averagePricesCount;

        private int marketIdCount;

        private int cachedImagesCount;

        public CacheSettings()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int ObsoleteCurrentPricesCount
        {
            get => this.obsoleteCurrentPricesCount;
            set
            {
                this.obsoleteCurrentPricesCount = value;
                this.OnPropertyChanged();
            }
        }

        public int CurrentPricesCount
        {
            get => this.currentPricesCount;
            set
            {
                this.currentPricesCount = value;
                this.OnPropertyChanged();
            }
        }

        public int ObsoleteAveragePricesCount
        {
            get => this.obsoleteAveragePricesCount;
            set
            {
                this.obsoleteAveragePricesCount = value;
                this.OnPropertyChanged();
            }
        }

        public int AveragePricesCount
        {
            get => this.averagePricesCount;
            set
            {
                this.averagePricesCount = value;
                this.OnPropertyChanged();
            }
        }

        public int MarketIdCount
        {
            get => this.marketIdCount;
            set
            {
                this.marketIdCount = value;
                this.OnPropertyChanged();
            }
        }

        public int CachedImagesCount
        {
            get => this.cachedImagesCount;
            set
            {
                this.cachedImagesCount = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void ImagesOpenFolderOnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void MarketIdClear(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}