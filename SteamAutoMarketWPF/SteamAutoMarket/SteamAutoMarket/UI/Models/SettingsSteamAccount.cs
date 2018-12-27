namespace SteamAutoMarket.UI.Models
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.Properties;
    using SteamAutoMarket.Steam.Auth;
    using SteamAutoMarket.UI.Repository.Image;

    using SteamKit2;

    [Serializable]
    public class SettingsSteamAccount : INotifyPropertyChanged
    {
        private string avatar = ResourceUtils.GetResourceImageUri("NoAvatarSmall.jpg");

        private int? currency;

        private string proxy;

        private string steamApi;

        private string tradeToken;

        public SettingsSteamAccount(string login, string password, string mafilePath, string proxy)
        {
            try
            {
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    throw new ArgumentException("Login or password is empty");
                }

                this.Login = login;
                this.Password = password;
                this.Proxy = proxy;

                this.Mafile = JsonConvert.DeserializeObject<SteamGuardAccount>(File.ReadAllText(mafilePath));
            }
            catch (Exception e)
            {
                Logger.Log.Error($"Error on mafile process - {e.Message}", e);
                throw;
            }

            this.SteamId = this.Mafile?.Session?.SteamID;
        }

        public SettingsSteamAccount()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [JsonIgnore]
        public uint? AccountId
        {
            get
            {
                var steamId = this.SteamId;
                if (steamId != null) return new SteamID(steamId.Value).AccountID;
                return null;
            }
        }

        public string Avatar
        {
            get => this.avatar;
            set
            {
                this.avatar = value;
                this.OnPropertyChanged();
            }
        }

        public int? Currency
        {
            get => this.currency;
            set
            {
                this.currency = value;
                this.OnPropertyChanged();
            }
        }

        public string Login { get; set; }

        public SteamGuardAccount Mafile { get; set; }

        public string Password { get; set; }

        public string Proxy
        {
            get => this.proxy;
            set
            {
                this.proxy = value;
                this.OnPropertyChanged();
            }
        }

        public string SteamApi
        {
            get => this.steamApi;
            set
            {
                this.steamApi = value;
                this.OnPropertyChanged();
            }
        }

        public ulong? SteamId { get; set; }

        public string TradeToken
        {
            get => this.tradeToken;
            set
            {
                this.tradeToken = value;
                this.OnPropertyChanged();
            }
        }

        public void DownloadAvatarAsync(bool useCached = true)
        {
            Task.Run(
                () => { this.Avatar = ImageProvider.GetSmallSteamProfileImage(this.SteamId.ToString(), useCached); });
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}