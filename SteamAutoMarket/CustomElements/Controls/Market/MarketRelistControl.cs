namespace SteamAutoMarket.CustomElements.Controls.Market
{
    using System;
    using System.Linq;
    using System.Windows.Forms;

    using SteamAutoMarket.Utils;
    using SteamAutoMarket.WorkingProcess;

    public partial class MarketRelistControl : UserControl
    {
        private const int CheckBoxStateCellIndex = 0;

        private const int ItemNameCellIndex = 1;

        private const int ItemAmountCellIndex = 2;

        private const int ItemTypeCellIndex = 3;

        private const int ListingDateCellIndex = 4;

        public MarketRelistControl()
        {
            try
            {
                this.InitializeComponent();
                this.AddHeaderCheckBox();

                // todo remove mocks
                this.AllSteamItemsGridView.Rows.Add(false, "name", 10, "type", "21-07-1996");
                this.AllSteamItemsGridView.Rows.Add(false, "name", 10, "type", "21-07-1996");
                this.AllSteamItemsGridView.Rows.Add(false, "name", 10, "type", "21-07-1996");
                this.AllSteamItemsGridView.Rows.Add(false, "name", 10, "type", "21-07-1996");
                this.AllSteamItemsGridView.Rows.Add(false, "name", 10, "type", "21-07-1996");
                this.AllSteamItemsGridView.Rows.Add(false, "name", 10, "type", "21-07-1996");
                this.AllSteamItemsGridView.Rows.Add(false, "name", 10, "type", "21-07-1996");
                this.AllSteamItemsGridView.Rows.Add(false, "name", 10, "type", "21-07-1996");
                this.AllSteamItemsGridView.Rows.Add(false, "name", 10, "type", "21-07-1996");
                this.AllSteamItemsGridView.Rows.Add(false, "name", 10, "type", "21-07-1996");
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

        private void HeaderCheckBoxOnCheckStateChanged(object sender, EventArgs e)
        {
            var state = this.HeaderCheckBox.Checked;

            foreach (var row in this.AllSteamItemsGridView.Rows.Cast<DataGridViewRow>())
            {
                row.Cells[CheckBoxStateCellIndex].Value = state;
            }
        }
    }
}