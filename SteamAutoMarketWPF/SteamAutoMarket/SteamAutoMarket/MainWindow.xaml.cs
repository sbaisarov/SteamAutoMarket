namespace SteamAutoMarket
{
    using System;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;

    using Core;

    using FirstFloor.ModernUI.Presentation;
    using FirstFloor.ModernUI.Windows.Controls;

    using log4net.Config;

    using SteamAutoMarket.Repository.Context;
    using SteamAutoMarket.Repository.Settings;
    using SteamAutoMarket.Utils.Logger;
    using SteamAutoMarket.Utils.Resources;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            ServicePointManager.ServerCertificateValidationCallback += OnServerCertificateValidationCallback;
            SettingsProvider.GetInstance();
            XmlConfigurator.Configure();
            Logger.UpdateLoggerLevel(SettingsProvider.GetInstance().LoggerLevel);
            AppearanceManager.Current.ThemeSource = ModernUiThemeUtils.GetTheme(SettingsProvider.GetInstance().Theme);
            AppearanceManager.Current.AccentColor = ModernUiThemeUtils.GetColor(SettingsProvider.GetInstance().Color);
            UiGlobalVariables.MainWindow = this;
            this.DataContext = this;

            this.InitializeComponent();
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // todo add critical error send here
            ErrorNotify.CriticalMessageBox("Oops. Seems application is crushed", (Exception)e.ExceptionObject);
        }

        private static bool OnServerCertificateValidationCallback(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            if (certificate.Subject.Contains("pythonanywhere")
                && certificate.GetCertHashString() == "6889BBFB104CE4CC4D35400B309C9526B85CB69D")
            {
                return true;
            }

            return sslPolicyErrors.ToString() == "None";
        }
    }
}