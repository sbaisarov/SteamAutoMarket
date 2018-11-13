namespace SteamAutoMarket.Utils
{
    using System.Diagnostics;
    using System.Reflection;
    using System.Windows;

    public class AppUtils
    {
        public static void Restart()
        {
            Process.Start(Assembly.GetExecutingAssembly().Location); // to start new instance of application
            Application.Current.Shutdown();
        }
    }
}