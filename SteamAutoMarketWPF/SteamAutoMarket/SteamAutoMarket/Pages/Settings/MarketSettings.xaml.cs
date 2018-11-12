namespace SteamAutoMarket.Pages.Settings
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using SteamAutoMarket.Annotations;
    using SteamAutoMarket.Repository.Settings;

    /// <summary>
    /// Interaction logic for Market.xaml
    /// </summary>
    public partial class MarketSettings : INotifyPropertyChanged
    {
        private int averagePriceDays = SettingsProvider.GetInstance().AveragePriceDays;

        private int itemsToTwoFactorConfirm = SettingsProvider.GetInstance().ItemsToTwoFactorConfirm;

        private int errorsOnSellToSkip = SettingsProvider.GetInstance().ErrorsOnSellToSkip;

        private int priceLoadingThreads = SettingsProvider.GetInstance().PriceLoadingThreads;

        public MarketSettings()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int AveragePriceDays
        {
            get => this.averagePriceDays;
            set
            {
                this.averagePriceDays = value;
                SettingsProvider.GetInstance().AveragePriceDays = value;
                this.OnPropertyChanged();
            }
        }

        public int ItemsToTwoFactorConfirm
        {
            get => this.itemsToTwoFactorConfirm;
            set
            {
                this.itemsToTwoFactorConfirm = value;
                SettingsProvider.GetInstance().ItemsToTwoFactorConfirm = value;
                this.OnPropertyChanged();
            }
        }

        public int ErrorsOnSellToSkip
        {
            get => this.errorsOnSellToSkip;
            set
            {
                this.errorsOnSellToSkip = value;
                SettingsProvider.GetInstance().ErrorsOnSellToSkip = value;
                this.OnPropertyChanged();
            }
        }

        public int PriceLoadingThreads
        {
            get => this.priceLoadingThreads;
            set
            {
                this.priceLoadingThreads = value;
                SettingsProvider.GetInstance().PriceLoadingThreads = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}