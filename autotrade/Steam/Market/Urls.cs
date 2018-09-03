namespace autotrade.Steam.Market
{
    public static class Urls
    {
        public const string SteamCommunity = "https://steamcommunity.com";
        public const string Login = SteamCommunity + "/login";
        public const string Logout = Login + "/logout";
        public const string LoginRsa = Login + "/getrsakey";
        public const string LoginDo = Login + "/dologin";
        public const string Market = SteamCommunity + "/market";
        public const string Inventory = SteamCommunity + "/inventory/";
        public const string CdnImage = "http://cdn.steamcommunity.com/economy/image/";
        public const string SteamStatus = "https://crowbar.steamstat.us/Barney";
    }
}