namespace SteamAutoMarket.SteamUtils
{
    using Steam;
    using Steam.SteamAuth;

    using SteamAutoMarket.Repository.Settings;

    public class UiSteamManager : SteamManager
    {
        public UiSteamManager(
            string login,
            string password,
            SteamGuardAccount mafile,
            string apiKey,
            bool forceSessionRefresh = false)
            : base(login, password, mafile, apiKey, forceSessionRefresh)
        {
            this.SaveAccount();
        }

        public void SaveAccount()
        {
            if (this.IsSessionUpdated == false) return;
            SettingsProvider.GetInstance().OnPropertyChanged("SteamAccounts");
        }
    }
}