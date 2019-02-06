namespace SteamAutoMarket.UI.Pages.Settings
{
    using System.Windows.Controls;

    using SteamAutoMarket.Localization;

    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            this.InitializeComponent();
            this.LocalizeMenuLinks();
            this.DataContext = this;
        }

        public void LocalizeMenuLinks()
        {
            this.AppearanceMenuLink.DisplayName = StringsProvider.Strings.MenuLink_Appearance;
            this.LicenseMenuLink.DisplayName = StringsProvider.Strings.MenuLink_License;
            this.MarketMenuLink.DisplayName = StringsProvider.Strings.MenuLink_Market;
            this.CacheMenuLink.DisplayName = StringsProvider.Strings.MenuLink_Cache;
        }
    }
}