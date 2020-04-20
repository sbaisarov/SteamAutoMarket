namespace SteamAutoMarket.UI.Pages
{
    using System.Windows;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.SteamIntegration;
    using SteamAutoMarket.UI.Utils.Logger;

    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class MarketExtra
    {
        public MarketExtra()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        private void CancelPendingListings_OnClick(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            var wp = WorkingProcessProvider.GetNewInstance("Cancel pending listings");
            wp?.StartWorkingProcess(() => { MarketSellUtils.CancelMarketPendingListings(wp.SteamManager, wp); });
        }

        private void Confirm2Fa_OnClick(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            var wp = WorkingProcessProvider.GetNewInstance("Confirm market 2FA");
            wp?.StartWorkingProcess(
                () => { MarketSellUtils.ConfirmMarketTransactionsWorkingProcess(wp.SteamManager.Guard, wp); });
        }
    }
}