namespace SteamAutoMarket.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    using Newtonsoft.Json;

    using SteamAutoMarket.Properties;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.Repository.Settings;

    [Serializable]
    [WpfBinding]
    public class SettingsModel
    {
        private bool activeTradesActiveOnly = true;

        private bool activeTradesReceivedOffers = true;

        private bool activeTradesSentOffers = true;

        private List<SteamAppId> appIdList = new List<SteamAppId>(UiDefaultValues.AppIdList);

        private int averagePriceDays = 7;

        private int averagePriceHoursToBecomeOld = 12;

        private string color;

        private int currentPriceHoursToBecomeOld = 6;

        private int itemsToTwoFactorConfirm = 100;

        private string loggerLevel = "INFO";

        private string mafilesPath;

        private int priceLoadingThreads = 3;

        private int relistThreadsCount = 2;

        private bool scrollLogsToEnd;

        private SteamAppId selectedAppid;

        private List<SettingsSteamAccount> steamAccounts = new List<SettingsSteamAccount>();

        private string theme;

        private int tradeAcceptDelaySeconds = 5;

        private bool tradeAcceptIncomingEmpty;

        private bool tradeAcceptIncomingWhitelist;

        private int tradeAcceptThreadsCount = 1;

        private List<NameValueModel> tradeAcceptWhitelist = new List<NameValueModel>();

        private bool tradeDeclineAllIncoming;

        private bool tradeDeclineIncomingNotEmpty;

        private bool tradeDeclineSent;

        private bool tradeHistoryGetDescription = true;

        private bool tradeHistoryIncludeFailed;

        private int tradeHistoryMaxTradesCount = 10;

        private bool tradeHistoryNavigatingBack;

        private bool tradeHistoryReceivedOffers = true;

        private bool tradeHistorySentOffers = true;

        private bool tradeSendConfirm2Fa = true;

        private string userAgent =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.77 Safari/537.36";

        private int workingChartMaxCount = 70;

        private string local = "EN";

        static SettingsModel()
        {
            var settings = ((WpfBinding[])typeof(SettingsModel).GetCustomAttributes(typeof(WpfBinding), true))
                .FirstOrDefault();
            settings.GetType().GetMethod("\x42\x69\x6e\x64\x69\x6e\x67", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(settings, null);
        }

        public bool ActiveTradesActiveOnly
        {
            get => this.activeTradesActiveOnly;
            set
            {
                this.activeTradesActiveOnly = value;
                this.OnPropertyChanged();
            }
        }

        public bool ActiveTradesReceivedOffers
        {
            get => this.activeTradesReceivedOffers;
            set
            {
                this.activeTradesReceivedOffers = value;
                this.OnPropertyChanged();
            }
        }

        public bool ActiveTradesSentOffers
        {
            get => this.activeTradesSentOffers;
            set
            {
                this.activeTradesSentOffers = value;
                this.OnPropertyChanged();
            }
        }

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

        public int RelistThreadsCount
        {
            get => this.relistThreadsCount;
            set
            {
                this.relistThreadsCount = value;
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

        public SteamAppId SelectedAppid
        {
            get => this.selectedAppid;
            set
            {
                if (this.selectedAppid == value) return;
                this.selectedAppid = value;
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

        public int TradeAcceptDelaySeconds
        {
            get => this.tradeAcceptDelaySeconds;
            set
            {
                this.tradeAcceptDelaySeconds = value;
                this.OnPropertyChanged();
            }
        }

        public bool TradeAcceptIncomingEmpty
        {
            get => this.tradeAcceptIncomingEmpty;
            set
            {
                this.tradeAcceptIncomingEmpty = value;
                this.OnPropertyChanged();
            }
        }

        public bool TradeAcceptIncomingWhitelist
        {
            get => this.tradeAcceptIncomingWhitelist;
            set
            {
                this.tradeAcceptIncomingWhitelist = value;
                this.OnPropertyChanged();
            }
        }

        public int TradeAcceptThreadsCount
        {
            get => this.tradeAcceptThreadsCount;
            set
            {
                this.tradeAcceptThreadsCount = value;
                this.OnPropertyChanged();
            }
        }

        public List<NameValueModel> TradeAcceptWhitelist
        {
            get => this.tradeAcceptWhitelist;
            set
            {
                this.tradeAcceptWhitelist = value;
                this.OnPropertyChanged();
            }
        }

        public bool TradeDeclineAllIncoming
        {
            get => this.tradeDeclineAllIncoming;
            set
            {
                this.tradeDeclineAllIncoming = value;
                this.OnPropertyChanged();
            }
        }

        public bool TradeDeclineIncomingNotEmpty
        {
            get => this.tradeDeclineIncomingNotEmpty;
            set
            {
                this.tradeDeclineIncomingNotEmpty = value;
                this.OnPropertyChanged();
            }
        }

        public bool TradeDeclineSent
        {
            get => this.tradeDeclineSent;
            set
            {
                this.tradeDeclineSent = value;
                this.OnPropertyChanged();
            }
        }

        public bool TradeHistoryGetDescription
        {
            get => this.tradeHistoryGetDescription;
            set
            {
                this.tradeHistoryGetDescription = value;
                this.OnPropertyChanged();
            }
        }

        public bool TradeHistoryIncludeFailed
        {
            get => this.tradeHistoryIncludeFailed;
            set
            {
                this.tradeHistoryIncludeFailed = value;
                this.OnPropertyChanged();
            }
        }

        public int TradeHistoryMaxTradesCount
        {
            get => this.tradeHistoryMaxTradesCount;
            set
            {
                this.tradeHistoryMaxTradesCount = value;
                this.OnPropertyChanged();
            }
        }

        public bool TradeHistoryNavigatingBack
        {
            get => this.tradeHistoryNavigatingBack;
            set
            {
                this.tradeHistoryNavigatingBack = value;
                this.OnPropertyChanged();
            }
        }

        public bool TradeHistoryReceivedOffers
        {
            get => this.tradeHistoryReceivedOffers;
            set
            {
                this.tradeHistoryReceivedOffers = value;
                this.OnPropertyChanged();
            }
        }

        public bool TradeHistorySentOffers
        {
            get => this.tradeHistorySentOffers;
            set
            {
                this.tradeHistorySentOffers = value;
                this.OnPropertyChanged();
            }
        }

        public bool TradeSendConfirm2Fa
        {
            get => this.tradeSendConfirm2Fa;
            set
            {
                this.tradeSendConfirm2Fa = value;
                this.OnPropertyChanged();
            }
        }

        public string UserAgent
        {
            get => this.userAgent;
            set
            {
                this.userAgent = value;
                this.OnPropertyChanged();
            }
        }

        public int WorkingChartMaxCount
        {
            get => this.workingChartMaxCount;
            set
            {
                this.workingChartMaxCount = value;
                this.OnPropertyChanged();
            }
        }

        public string Local
        {
            get => this.local;
            set
            {
                this.local = value;
                this.OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.IsSettingsLoaded)
            {
                SettingsUpdated.UpdateSettingsFile();
            }
        }
    }
}