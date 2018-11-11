namespace SteamAutoMarket.Repository.Context
{
    using SteamAutoMarket.Pages;
    using SteamAutoMarket.SteamIntegration;

    public class UiGlobalVariables
    {
        public static LogsWindow LogsWindow { get; set; }

        public static MainWindow MainWindow { get; set; }

        public static MarketRelist MarketRelist { get; set; }

        public static MarketSell MarketSellPage { get; set; }

        public static SteamAccountLogin SteamAccountLogin { get; set; }

        public static UiSteamManager SteamManager { get; set; }
    }
}