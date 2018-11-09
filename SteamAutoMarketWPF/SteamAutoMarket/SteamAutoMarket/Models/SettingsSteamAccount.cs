namespace SteamAutoMarket.Models
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using Core;

    using global::Steam.SteamAuth;

    using Newtonsoft.Json;

    using SteamAutoMarket.Annotations;
    using SteamAutoMarket.Repository.Image;

    public class SettingsSteamAccount : INotifyPropertyChanged
    {
        private string avatar = ResourceUtils.GetResourceImageUri("NoAvatarSmall.jpg");

        private string steamApi;

        private string tradeToken;

        public SettingsSteamAccount(string login, string password, string mafilePath)
        {
            try
            {
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    throw new ArgumentException("Login or password is empty");
                }

                this.Login = login;
                this.Password = password;

                this.Mafile = JsonConvert.DeserializeObject<SteamGuardAccount>(File.ReadAllText(mafilePath));
            }
            catch (Exception e)
            {
                Logger.Log.Error("Error on mafile process", e);
                throw new ArgumentException($"Error on mafile process - {e.Message}");
            }

            try
            {
                this.SteamId = this.Mafile.Session.SteamID;
            }
            catch
            {
            }
        }

        public SettingsSteamAccount()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Login { get; set; }

        public string Password { get; set; }

        public ulong SteamId { get; set; }

        public SteamGuardAccount Mafile { get; set; }

        public string Avatar
        {
            get => this.avatar;
            set
            {
                this.avatar = value;
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

        public string TradeToken
        {
            get => this.tradeToken;
            set
            {
                this.tradeToken = value;
                this.OnPropertyChanged();
            }
        }

        public void DownloadAvatarAsync()
        {
            Task.Run(() => { this.Avatar = ImageProvider.GetSmallSteamProfileImage(this.SteamId.ToString()); });
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}