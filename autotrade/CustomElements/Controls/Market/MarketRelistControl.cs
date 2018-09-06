using System;
using System.Windows.Forms;
using SteamAutoMarket.Utils;
using SteamAutoMarket.WorkingProcess;

namespace SteamAutoMarket.CustomElements.Controls.Market
{
    public partial class MarketRelistControl : UserControl
    {
        public MarketRelistControl()
        {
            InitializeComponent();

            AllSteamItemsGridView.Rows.Add(false, "name", "type", "21-07-1996");
            AllSteamItemsGridView.Rows.Add(false, "name", "type", "21-07-1996");
            AllSteamItemsGridView.Rows.Add(false, "name", "type", "21-07-1996");
            AllSteamItemsGridView.Rows.Add(false, "name", "type", "21-07-1996");
            AllSteamItemsGridView.Rows.Add(false, "name", "type", "21-07-1996");
        }

        public void AuthCurrentAccount()
        {
            AccountNameLable.Text = CurrentSession.SteamManager.Guard.AccountName;
            SplitterPanel.BackgroundImage = CurrentSession.AccountImage;
        }

        private void LoadListingButton_Click(object sender, EventArgs e)
        {
            if (CurrentSession.SteamManager == null)
            {
                MessageBox.Show("You should login first", "Error market listing loading", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Logger.Error("Error on market listing loading. No logined account found.");
                return;
            }

            var listings = CurrentSession.SteamManager.MarketClient.MyListings();
        }
    }
}