namespace SteamAutoMarket.Repository.Settings
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;

    using Newtonsoft.Json;

    using SteamAutoMarket.Models;
    using SteamAutoMarket.Repository.Context;

    public class SettingsModel : INotifyPropertyChanged
    {
        private SteamAppId marketSellSelectedAppid;

        private List<SteamAppId> appIdList = new List<SteamAppId>(UiDefaultValues.AppIdList);

        private string mafilesPath;

        private SteamAppId tradeSelectedAppId;

        private List<SettingsSteamAccount> steamAccounts = new List<SettingsSteamAccount>();

        private string theme;

        private string color;

        public event PropertyChangedEventHandler PropertyChanged;

        [JsonIgnore]
        public bool IsSettingsLoaded { get; set; }

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

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (this.IsSettingsLoaded) SettingsUpdated.UpdateSettingsFile();
        }
    }
}