namespace SteamAutoMarket.CustomElements.Controls.Market
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using SteamAutoMarket.CustomElements.Utils;
    using SteamAutoMarket.Steam.Market.Models;
    using SteamAutoMarket.Utils;
    using SteamAutoMarket.WorkingProcess;

    public partial class MarketRelistControl : UserControl
    {
        private const int CheckBoxStateCellIndex = 0;

        private const int ItemNameCellIndex = 1;

        private const int ItemAmountCellIndex = 2;

        private const int ItemTypeCellIndex = 3;

        private const int ListingDateCellIndex = 4;

        private const int ListingPriceCellIndex = 5;

        private const int ListingCurrentPriceCellIndex = 6;

        private const int ListingAveragePriceCellIndex = 7;

        private const int ListingCancelButtonCellIndex = 8;

        private const int ListingHiddenMarketHashNameCellIndex = 9;

        private readonly Dictionary<string, List<MyListingsSalesItem>> myListings =
            new Dictionary<string, List<MyListingsSalesItem>>();

        public MarketRelistControl()
        {
            try
            {
                this.InitializeComponent();
                this.AddHeaderCheckBox();
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
                this.AllSteamItemsGridView.Rows.Clear();
                this.myListings.Clear();

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
                var groupedListings = listings.Sales.GroupBy(x => new { x.HashName, x.Price });
                foreach (var group in groupedListings)
                {
                    var item = group.FirstOrDefault();
                    if (item == null)
                    {
                        return;
                    }

                    this.myListings.Add(item.HashName + item.Price, group.ToList());
                    this.AddListing(item.Name, group.Count(), "TODO TYPE", item.Date, item.Price, item.HashName);
                }
            }
            catch (Exception ex)
            {
                Logger.Critical("Error on loading listed market items", ex);
            }
        }

        private void AddListing(string name, int count, string type, string date, double price, string hashName)
        {
            this.AllSteamItemsGridView.Rows.Add(false, name, count, type, date, price, null, null, null, hashName);
        }

        private void HeaderCheckBoxOnCheckStateChanged(object sender, EventArgs e)
        {
            var state = this.HeaderCheckBox.Checked;

            foreach (var row in this.AllSteamItemsGridView.Rows.Cast<DataGridViewRow>())
            {
                row.Cells[CheckBoxStateCellIndex].Value = state;
            }
        }

        private void AllSteamItemsGridViewSelectionChanged(object sender, EventArgs e)
        {
            this.ItemDescriptionTextBox.Clear();
            var rows = this.AllSteamItemsGridView.SelectedRows;
            if (rows.Count == 0)
            {
                return;
            }

            var selectedHashName = (string)rows[0].Cells[ListingHiddenMarketHashNameCellIndex].Value;
            var selectedPrice = (double)rows[0].Cells[ListingPriceCellIndex].Value;

            var items = this.myListings[selectedHashName + selectedPrice];
            var item = items.FirstOrDefault();

            if (item == null)
            {
                return;
            }

            CommonUtils.AppendBoldText(this.ItemDescriptionTextBox, "Bold info: ");
            this.ItemDescriptionTextBox.AppendText("Some info\n\n");

            CommonUtils.AppendBoldText(this.ItemDescriptionTextBox, item.Url + Environment.NewLine);
            this.ItemDescriptionTextBox.AppendText(item.SaleId.ToString());

            // ImageUtils.UpdateItemImageOnPanelAsync(description, imageBox);
        }
    }
}