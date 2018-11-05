namespace SteamAutoMarket.Pages.Settings
{
    using System.Windows.Controls;

    using SteamAutoMarket.Repository.Context;

    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }
    }
}