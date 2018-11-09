namespace SteamAutoMarket.SteamUtils
{
    using Steam;
    using Steam.SteamAuth;

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
        }
    }
}