namespace SteamAutoMarket.Repository.Context
{
    using SteamAutoMarket.Pages;

    public class UiGlobalVariables
    {
        public static MainWindow MainWindow { get; set; }

        public static MarketSell MarketSellPage { get; set; }

        public static SteamAccountLogin SteamAccountLogin { get; set; }

        public static MarketRelist MarketRelist { get; set; }
    }
}