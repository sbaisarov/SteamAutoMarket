namespace SteamAutoMarket
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Windows.Forms;
    using FirstFloor.ModernUI.Presentation;
    using SteamAutoMarket.Core;
    using SteamAutoMarket.Localization;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.Repository.Settings;
    using SteamAutoMarket.UI.Utils.Logger;
    using SteamAutoMarket.UI.Utils.Resources;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private string windowTitle;

        public MainWindow()
        {
            WindowTitle = $"SteamAutoMarket {Assembly.GetExecutingAssembly().GetName().Version}";

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            ServicePointManager.ServerCertificateValidationCallback += OnServerCertificateValidationCallback;

            Logger.Setup(Logger.NewFileAppender(), new UiLogAppender());
            Logger.UpdateLoggerLevel(SettingsProvider.GetInstance().LoggerLevel);

            AppearanceManager.Current.ThemeSource = ModernUiThemeUtils.GetTheme(SettingsProvider.GetInstance().Theme);
            AppearanceManager.Current.AccentColor = ModernUiThemeUtils.GetColor(SettingsProvider.GetInstance().Color);

            UiGlobalVariables.MainWindow = this;
            this.DataContext = this;

            this.InitializeComponent();
            this.LocalizeMenuLinks();

        }

        public string WindowTitle
        {
            get => windowTitle;
            set
            {
                windowTitle = value;
                OnPropertyChanged();
            }
        }

        public void LocalizeMenuLinks()
        {
            this.InfoLink.DisplayName = StringsProvider.Strings.MenuLink_Info;
            this.NewsLink.DisplayName = StringsProvider.Strings.MenuLink_News;
            this.SteamInfoLink.DisplayName = StringsProvider.Strings.MenuLink_Info;
            this.MarketLink.DisplayName = StringsProvider.Strings.MenuLink_Market;
            this.SellLink.DisplayName = StringsProvider.Strings.MenuLink_Sell;
            this.RelistLink.DisplayName = StringsProvider.Strings.MenuLink_Relist;
            this.UtilityLink.DisplayName = StringsProvider.Strings.MenuLink_Utility;
            this.MarketUtilityLink.DisplayName = StringsProvider.Strings.MenuLink_Market;
            this.GemsLink.DisplayName = StringsProvider.Strings.MenuLink_Gems;
            this.TradeLink.DisplayName = StringsProvider.Strings.MenuLink_Trade;
            this.SendLink.DisplayName = StringsProvider.Strings.MenuLink_Send;
            this.AutoAccepterLink.DisplayName = StringsProvider.Strings.MenuLink_AutoAccepter;
            this.ActiveLink.DisplayName = StringsProvider.Strings.MenuLink_Active;
            this.HistoryLink.DisplayName = StringsProvider.Strings.MenuLink_History;
            this.ActionLink.DisplayName = StringsProvider.Strings.MenuLink_Action;
            this.CurrentProcessLink.DisplayName = StringsProvider.Strings.MenuLink_CurrentProcess;
            this.SettingsLink.DisplayName = StringsProvider.Strings.MenuLink_Settings;
            this.LogsLink.DisplayName = StringsProvider.Strings.MenuLink_Logs;

            if (this.Account.DisplayName == StringsProvider.RussianStrings.MenuLink_NotLoggedIn
                || this.Account.DisplayName == StringsProvider.EnglishStrings.MenuLink_NotLoggedIn)
            {
                this.Account.DisplayName = StringsProvider.Strings.MenuLink_NotLoggedIn;
            }
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ErrorNotify.CriticalMessageBox("Oops. Seems application is crushed", (Exception)e.ExceptionObject);
        }

        private static bool OnServerCertificateValidationCallback(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return true;

            //if (certificate.Subject.Contains("pythonanywhere"))
            //{
            //    return true;
            //}

            //return sslPolicyErrors.ToString() == "None";
        }

        private void UpdateProgram()
        {
            AutoUpdater.AutoUpdater.RunUpdateAsAdmin = true;
            AutoUpdater.AutoUpdater.DownloadPath = Environment.CurrentDirectory;
            AutoUpdater.AutoUpdater.AppCastURL = "http://shamanovski.pythonanywhere.com/release.xml";
            AutoUpdater.AutoUpdater.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
