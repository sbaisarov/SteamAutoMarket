namespace SteamAutoMarket.Pages.Settings
{
    using System.Windows.Controls;

    using SteamAutoMarket.Repository.Context;

    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }
    }
}