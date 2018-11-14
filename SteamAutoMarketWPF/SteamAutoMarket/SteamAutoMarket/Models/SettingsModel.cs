namespace SteamAutoMarket.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Core;

    using Newtonsoft.Json;

    using SteamAutoMarket.Repository.Context;
    using SteamAutoMarket.Repository.Settings;

    public class SettingsModel : INotifyPropertyChanged
    {
        private List<SteamAppId> appIdList = new List<SteamAppId>(UiDefaultValues.AppIdList);

        #region Private

        private string color;

        private int errorsOnSellToSkip = 20;

        private int itemsToTwoFactorConfirm = 100;

        private string mafilesPath;

        private SteamAppId marketSellSelectedAppid;

        private int priceLoadingThreads = 3;

        private bool scrollLogsToEnd;

        private List<SettingsSteamAccount> steamAccounts = new List<SettingsSteamAccount>();

        private string theme;

        private int averagePriceDays = 7;

        private int averagePriceHoursToBecomeOld = 12;

        private int currentPriceHoursToBecomeOld = 6;

        private string loggerLevel = "INFO";

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

        public int AveragePriceDays
        {
            get => this.averagePriceDays;
            set
            {
                if (this.averagePriceDays == value) return;
                this.averagePriceDays = value;
                this.OnPropertyChanged();
            }
        }

        public int AveragePriceHoursToBecomeOld
        {
            get => this.averagePriceHoursToBecomeOld;
            set
            {
                if (this.averagePriceHoursToBecomeOld == value) return;
                this.averagePriceHoursToBecomeOld = value;
                this.OnPropertyChanged();
            }
        }

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

        public int CurrentPriceHoursToBecomeOld
        {
            get => this.currentPriceHoursToBecomeOld;
            set
            {
                if (this.currentPriceHoursToBecomeOld == value) return;
                this.currentPriceHoursToBecomeOld = value;
                this.OnPropertyChanged();
            }
        }

        public int ErrorsOnSellToSkip
        {
            get => this.errorsOnSellToSkip;
            set
            {
                if (this.errorsOnSellToSkip == value) return;
                this.errorsOnSellToSkip = value;
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
                if (this.itemsToTwoFactorConfirm == value) return;
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
                if (this.scrollLogsToEnd == value) return;
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

        public string LoggerLevel
        {
            get => this.loggerLevel;
            set
            {
                if (this.loggerLevel == value) return;
                this.loggerLevel = value;
                this.OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Logger.Log.Debug($"Updating '{propertyName}' setting");
            if (this.IsSettingsLoaded)
            {
                SettingsUpdated.UpdateSettingsFile();
            }
        }
    }
}