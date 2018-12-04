using SteamAutoMarket.Core;

namespace SteamAutoMarket.UI.Repository.Context
{
    using SteamAutoMarket.UI.Pages;
    using SteamAutoMarket.UI.SteamIntegration;

    public class UiGlobalVariables
    {
        public static LogsWindow LogsWindow { get; set; }
        
        public static FileLogger FileLogger { get; set; }

        public static MainWindow MainWindow { get; set; }

        public static MarketRelist MarketRelist { get; set; }

        public static MarketSell MarketSellPage { get; set; }

        public static SteamAccountLogin SteamAccountLogin { get; set; }

        public static UiSteamManager SteamManager { get; set; }

        public static WorkingProcess WorkingProcess { get; set; } = new WorkingProcess();

        public static WorkingProcessDataContext WorkingProcessDataContext { get; set; } =
            new WorkingProcessDataContext();
    }
}