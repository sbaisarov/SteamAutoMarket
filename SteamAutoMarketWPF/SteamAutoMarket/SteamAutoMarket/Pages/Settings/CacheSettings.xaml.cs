namespace SteamAutoMarket.Pages.Settings
{
    using System.Windows.Controls;

    using SteamAutoMarket.Repository.Context;

    /// <summary>
    /// Interaction logic for CacheSettings.xaml
    /// </summary>
    public partial class CacheSettings : UserControl
    {
        public CacheSettings()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }
    }
}