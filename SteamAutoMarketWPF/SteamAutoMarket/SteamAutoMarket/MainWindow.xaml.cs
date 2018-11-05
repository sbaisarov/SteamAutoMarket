namespace SteamAutoMarket
{
    using FirstFloor.ModernUI.Presentation;
    using FirstFloor.ModernUI.Windows.Controls;

    using log4net.Config;

    using SteamAutoMarket.Repository.Context;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            XmlConfigurator.Configure();

            this.InitializeComponent();
            UiGlobalVariables.MainWindow = this;
            this.DataContext = this;

            AppearanceManager.Current.ThemeSource = AppearanceManager.DarkThemeSource;

        }
    }
}