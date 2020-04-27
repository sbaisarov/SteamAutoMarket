namespace SteamAutoMarket.Steam.Auth
{
    public static class ApiEndpoints
    {
        public const string CommunityBase = "https://steamcommunity.com";

        public const string MobileAuthBase = SteamApiBase + "/IMobileAuthService/%s/v0001";

        public const string SteamApiBase = "https://api.steampowered.com";

        public const string TwoFactorBase = SteamApiBase + "/ITwoFactorService/%s/v0001";

        public static readonly string MobileAuthGetWgToken = MobileAuthBase.Replace("%s", "GetWGToken");

        public static readonly string TwoFactorTimeQuery = TwoFactorBase.Replace("%s", "QueryTime");
    }
}