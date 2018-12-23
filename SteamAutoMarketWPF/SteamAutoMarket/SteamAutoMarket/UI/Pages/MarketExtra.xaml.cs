namespace SteamAutoMarket.UI.Pages
{
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.SteamIntegration;
    using SteamAutoMarket.UI.Utils.Logger;

    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class MarketExtra : UserControl
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

            Task.Run(
                () =>
                    {
                        WorkingProcess.ProcessMethod(
                            () =>
                                {
                                    MarketSellUtils.CancelMarketPendingListings(
                                        UiGlobalVariables.SteamManager,
                                        UiGlobalVariables.WorkingProcessDataContext);
                                },
                            "Cancel pending listings");
                    });
        }

        private void Confirm2Fa_OnClick(object sender, RoutedEventArgs e)
        {
            if (UiGlobalVariables.SteamManager == null)
            {
                ErrorNotify.CriticalMessageBox("You should login first!");
                return;
            }

            Task.Run(
                () =>
                    {
                        WorkingProcess.ProcessMethod(
                            () =>
                                {
                                    MarketSellUtils.ConfirmMarketTransactionsWorkingProcess(
                                        UiGlobalVariables.SteamManager.Guard,
                                        UiGlobalVariables.WorkingProcessDataContext);
                                },
                            "Confirm market 2FA");
                    });
        }
    }
}