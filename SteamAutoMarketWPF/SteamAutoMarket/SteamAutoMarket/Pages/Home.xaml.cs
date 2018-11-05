namespace SteamAutoMarket.Pages
{
    using System.Windows.Controls;

    using SteamAutoMarket.Repository.Context;

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
    }
}