namespace SteamAutoMarket.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using Newtonsoft.Json;

    using SteamAutoMarket.Repository.Context;
    using SteamAutoMarket.Repository.Settings;

    public class SettingsModel : INotifyPropertyChanged
    {
        private List<SteamAppId> appIdList = new List<SteamAppId>(UiDefaultValues.AppIdList);

        #region Private

        private string color;

        private int errorsOnSellToSkip = 20;

        private int hoursToBecomeOld = 10;

        private int itemsToTwoFactorConfirm = 100;

        private string mafilesPath;

        private SteamAppId marketSellSelectedAppid;

        private int priceLoadingThreads = 3;

        private bool scrollLogsToEnd;

        private List<SettingsSteamAccount> steamAccounts = new List<SettingsSteamAccount>();

        private string theme;

        private SteamAppId tradeSelectedAppId;

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public List<SteamAppId> AppIdList
        {
            get => this.appIdList;
            set
            {
                if (this.appIdList == value) return;
                this.appIdList = value;
                this.OnPropertyChanged();
            }
        }

        public int AveragePriceDays { get; set; } = 7;

        public int AveragePriceHoursToBecomeOld { get; set; }

        public string Color
        {
            get => this.color;
            set
            {
                if (this.color == value) return;
                this.color = value;
                this.OnPropertyChanged();
            }
        }

        public int CurrentPriceHoursToBecomeOld { get; set; }

        public int ErrorsOnSellToSkip
        {
            get => this.errorsOnSellToSkip;
            set
            {
                this.errorsOnSellToSkip = value;
                this.OnPropertyChanged();
            }
        }

        public int HoursToBecomeOld
        {
            get => this.hoursToBecomeOld;
            set
            {
                this.hoursToBecomeOld = value;
                this.OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public bool IsSettingsLoaded { get; set; }

        public int ItemsToTwoFactorConfirm
        {
            get => this.itemsToTwoFactorConfirm;
            set
            {
                this.itemsToTwoFactorConfirm = value;
                this.OnPropertyChanged();
            }
        }

        public string MafilesPath
        {
            get => this.mafilesPath;
            set
            {
                if (this.mafilesPath == value) return;
                this.mafilesPath = value;
                this.OnPropertyChanged();
            }
        }

        public SteamAppId MarketSellSelectedAppid
        {
            get => this.marketSellSelectedAppid;
            set
            {
                if (this.marketSellSelectedAppid == value) return;
                this.marketSellSelectedAppid = value;
                this.OnPropertyChanged();
            }
        }

        public int PriceLoadingThreads
        {
            get => this.priceLoadingThreads;
            set
            {
                if (this.priceLoadingThreads == value) return;
                this.priceLoadingThreads = value;
                this.OnPropertyChanged();
            }
        }

        public bool ScrollLogsToEnd
        {
            get => this.scrollLogsToEnd;

            set
            {
                this.scrollLogsToEnd = value;
                this.OnPropertyChanged();
            }
        }

        public List<SettingsSteamAccount> SteamAccounts
        {
            get => this.steamAccounts;
            set
            {
                if (this.steamAccounts == value) return;
                this.steamAccounts = value;
                this.OnPropertyChanged();
            }
        }

        public string Theme
        {
            get => this.theme;
            set
            {
                if (this.theme == value) return;
                this.theme = value;
                this.OnPropertyChanged();
            }
        }

        public SteamAppId TradeSelectedAppId
        {
            get => this.tradeSelectedAppId;
            set
            {
                if (this.tradeSelectedAppId == value) return;
                this.tradeSelectedAppId = value;
                this.OnPropertyChanged();
            }
        }

        public void OnPropertyChanged(string propertyName = null)
        {
            if (this.IsSettingsLoaded) SettingsUpdated.UpdateSettingsFile();
        }
    }
}