namespace SteamAutoMarket
{
    using System;
    using System.Net;

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
            SettingsProvider.GetInstance();
            XmlConfigurator.Configure();
            Logger.UpdateLoggerLevel(SettingsProvider.GetInstance().LoggerLevel);
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;
            UiGlobalVariables.MainWindow = this;
            this.DataContext = this;

            AppearanceManager.Current.ThemeSource = ModernUiThemeUtils.GetTheme(SettingsProvider.GetInstance().Theme);
            AppearanceManager.Current.AccentColor = ModernUiThemeUtils.GetColor(SettingsProvider.GetInstance().Color);

            this.InitializeComponent();
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // todo add critical error send here
            ErrorNotify.CriticalMessageBox("Oops. Seems application is crushed", (Exception)e.ExceptionObject);
        }
    }
}