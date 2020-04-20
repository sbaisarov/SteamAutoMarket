namespace SteamAutoMarket.UI.Utils
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Windows;
    using SteamAutoMarket.UI.Repository.Context;

    public static class AppUtils
    {
        public static void OpenTab(string tabPath)
        {
            UiGlobalVariables.MainWindow.ContentSource = new Uri(tabPath, UriKind.Relative);
        }

        public static void Restart()
        {
            Process.Start(Assembly.GetExecutingAssembly().Location); // to start new instance of application
            Application.Current.Shutdown();
        }
    }
}