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

        private string color;

        private string mafilesPath;

        private SteamAppId marketSellSelectedAppid;

        private int priceLoadingThreads = 3;

        private List<SettingsSteamAccount> steamAccounts = new List<SettingsSteamAccount>();

        private string theme;

        private SteamAppId tradeSelectedAppId;

        private int hoursToBecomeOld = 10;

        private int errorsOnSellToSkip = 20;

        private int itemsTo2FaConfirm = 100;

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

        [JsonIgnore]
        public bool IsSettingsLoaded { get; set; }

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

        public int HoursToBecomeOld
        {
            get => this.hoursToBecomeOld;
            set
            {
                this.hoursToBecomeOld = value;
                this.OnPropertyChanged();
            }
        }

        public int ErrorsOnSellToSkip
        {
            get => this.errorsOnSellToSkip;
            set
            {
                this.errorsOnSellToSkip = value;
                this.OnPropertyChanged();
            }
        }

        public int ItemsTo2FAConfirm
        {
            get => this.itemsTo2FaConfirm;
            set
            {
                this.itemsTo2FaConfirm = value;
                this.OnPropertyChanged();
            }
        }

        public void OnPropertyChanged(string propertyName = null)
        {
            if (this.IsSettingsLoaded) SettingsUpdated.UpdateSettingsFile();
        }
    }
}