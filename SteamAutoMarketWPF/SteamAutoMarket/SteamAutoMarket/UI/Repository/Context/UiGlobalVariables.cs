namespace SteamAutoMarket.UI.Repository.Context
{
    using SteamAutoMarket.UI.Pages;
    using SteamAutoMarket.UI.SteamIntegration;

    public class UiGlobalVariables
    {
        public static WorkingProcessDataContext LastInvokedWorkingProcessDataContext { get; set; } =
            WorkingProcessProvider.EmptyWorkingProcess;

        public static LogsWindow LogsWindow { get; set; }

        public static MainWindow MainWindow { get; set; }

        public static MarketRelist MarketRelist { get; set; }

        public static MarketSell MarketSellPage { get; set; }

        public static SteamAccountLogin SteamAccountLogin { get; set; }

        public static UiSteamManager SteamManager { get; set; }

        public static WorkingProcess WorkingProcessTab { get; set; }
    }
}