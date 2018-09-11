namespace SteamAutoMarket.CustomElements.Controls.Market
{
    using System;
    using System.Windows.Forms;

    using SteamAutoMarket.Utils;
    using SteamAutoMarket.WorkingProcess;

    public partial class MarketRelistControl : UserControl
    {
        public MarketRelistControl()
        {
            try
            {
                this.InitializeComponent();

                // todo remove mocks
                this.AllSteamItemsGridView.Rows.Add(false, "name", "type", "21-07-1996");
                this.AllSteamItemsGridView.Rows.Add(false, "name", "type", "21-07-1996");
                this.AllSteamItemsGridView.Rows.Add(false, "name", "type", "21-07-1996");
                this.AllSteamItemsGridView.Rows.Add(false, "name", "type", "21-07-1996");
                this.AllSteamItemsGridView.Rows.Add(false, "name", "type", "21-07-1996");
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        public void AuthCurrentAccount()
        {
            this.AccountNameLable.Text = CurrentSession.SteamManager.Guard.AccountName;
            this.SplitterPanel.BackgroundImage = CurrentSession.AccountImage;
        }

        private void LoadListingButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (CurrentSession.SteamManager == null)
                {
                    MessageBox.Show(
                        @"You should login first",
                        @"Error market listing loading",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Logger.Error("Error on market listing loading. No signed in account found.");
                    return;
                }

                var listings = CurrentSession.SteamManager.MarketClient.MyListings();
            }
            catch (Exception ex)
            {
                Logger.Critical("Error on loading listed market items", ex);
            }
        }
    }
}