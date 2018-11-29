namespace SteamAutoMarket.Pages
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using AutoUpdaterDotNET;

    using FirstFloor.ModernUI.Windows.Navigation;

    using SteamAutoMarket.Repository.Context;
    using SteamAutoMarket.Utils;

    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        private void SteamLogin_OnClick(object sender, RoutedEventArgs e) =>
            AppUtils.OpenTab("/Pages/SteamAccountLogin.xaml");

        private void LicenseStatus_OnClick(object sender, RoutedEventArgs e) =>
            AppUtils.OpenTab("/Pages/Settings/License.xaml");

        private void Telegram_OnClick(object sender, RoutedEventArgs e) =>
            Process.Start("https://t.me/steamsolutions");

        private void Website_OnClick(object sender, RoutedEventArgs e) => Process.Start("https://www.steambiz.store/");

        private void Documentation_OnClick(object sender, RoutedEventArgs e) =>
            Process.Start("https://docs.google.com/document/d/17qZ8Y8wY60dLMxYyNM2kZE13JrU_iNRBHK8oOgVZgvM");

        private void TodoFeatures_OnClick(object sender, RoutedEventArgs e) =>
            Process.Start("https://docs.google.com/document/d/17qZ8Y8wY60dLMxYyNM2kZE13JrU_iNRBHK8oOgVZgvM");

        private void Update_OnClick(object sender, RoutedEventArgs e) => AutoUpdater.ShowUpdateForm();
    }
}