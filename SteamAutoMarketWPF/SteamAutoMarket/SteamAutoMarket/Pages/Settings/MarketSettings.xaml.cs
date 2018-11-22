namespace SteamAutoMarket.Pages.Settings
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using SteamAutoMarket.Properties;
    using SteamAutoMarket.Repository.Settings;

    /// <summary>
    /// Interaction logic for Market.xaml
    /// </summary>
    public partial class MarketSettings : INotifyPropertyChanged
    {
        public MarketSettings()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int AveragePriceDays
        {
            get => SettingsProvider.GetInstance().AveragePriceDays;
            set
            {
                SettingsProvider.GetInstance().AveragePriceDays = value;
                this.OnPropertyChanged();
            }
        }

        public int ErrorsOnSellToSkip
        {
            get => SettingsProvider.GetInstance().ErrorsOnSellToSkip;
            set
            {
                SettingsProvider.GetInstance().ErrorsOnSellToSkip = value;
                this.OnPropertyChanged();
            }
        }

        public int ItemsToTwoFactorConfirm
        {
            get => SettingsProvider.GetInstance().ItemsToTwoFactorConfirm;
            set
            {
                SettingsProvider.GetInstance().ItemsToTwoFactorConfirm = value;
                this.OnPropertyChanged();
            }
        }

        public int ObsoleteAveragePrice
        {
            get => SettingsProvider.GetInstance().AveragePriceHoursToBecomeOld;
            set
            {
                SettingsProvider.GetInstance().AveragePriceHoursToBecomeOld = value;
                this.OnPropertyChanged();
            }
        }

        public int ObsoleteCurrentPrice
        {
            get => SettingsProvider.GetInstance().CurrentPriceHoursToBecomeOld;
            set
            {
                SettingsProvider.GetInstance().CurrentPriceHoursToBecomeOld = value;
                this.OnPropertyChanged();
            }
        }

        public int PriceLoadingThreads
        {
            get => SettingsProvider.GetInstance().PriceLoadingThreads;
            set
            {
                SettingsProvider.GetInstance().PriceLoadingThreads = value;
                this.OnPropertyChanged();
            }
        }

        public int RelistThreads
        {
            get => SettingsProvider.GetInstance().RelistThreadsCount;
            set
            {
                SettingsProvider.GetInstance().RelistThreadsCount = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}