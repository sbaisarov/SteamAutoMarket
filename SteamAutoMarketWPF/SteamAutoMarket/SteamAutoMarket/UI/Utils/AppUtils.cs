namespace SteamAutoMarket.UI.Utils
{
    using System.Diagnostics;
    using System.Reflection;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;

    using FirstFloor.ModernUI.Windows.Navigation;

    using SteamAutoMarket.UI.Repository.Context;

    public static class AppUtils
    {
        public static void OpenTab(string tabPath)
        {
            Application.Current.Dispatcher.Invoke(
                () =>
                    {
                        var target = NavigationHelper.FindFrame("_top", UiGlobalVariables.MainWindow);
                        NavigationCommands.GoToPage.Execute(tabPath, target);
                    });

            Thread.Sleep(500);
        }

        public static void Restart()
        {
            Process.Start(Assembly.GetExecutingAssembly().Location); // to start new instance of application
            Application.Current.Shutdown();
        }
    }
}