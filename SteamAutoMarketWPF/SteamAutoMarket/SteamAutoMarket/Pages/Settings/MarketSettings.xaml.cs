namespace SteamAutoMarket.Pages.Settings
{
    using System.Windows.Controls;

    using SteamAutoMarket.Repository.Context;

    /// <summary>
    /// Interaction logic for Market.xaml
    /// </summary>
    public partial class MarketSettings : UserControl
    {
        public MarketSettings()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }
    }
}