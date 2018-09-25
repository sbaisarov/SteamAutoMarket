namespace SteamAutoMarket.CustomElements.Controls.Market
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using SteamAutoMarket.CustomElements.Utils;
    using SteamAutoMarket.Steam.Market.Models;
    using SteamAutoMarket.Utils;
    using SteamAutoMarket.WorkingProcess;
    using SteamAutoMarket.WorkingProcess.PriceLoader;

    public partial class MarketRelistControl : UserControl
    {
        public const int CheckBoxStateCellIndex = 0;

        public const int ItemNameCellIndex = 1;

        public const int ItemAmountCellIndex = 2;

        public const int ItemTypeCellIndex = 3;

        public const int ListingDateCellIndex = 4;

        public const int ListingPriceCellIndex = 5;

        public const int ListingCurrentPriceCellIndex = 6;

        public const int ListingAveragePriceCellIndex = 7;

        public const int ListingCancelButtonCellIndex = 8;

        public const int ListingHiddenMarketHashNameCellIndex = 9;

        public static readonly Dictionary<string, List<MyListingsSalesItem>> MyListings =
            new Dictionary<string, List<MyListingsSalesItem>>();

        public MarketRelistControl()
        {
            try
            {
                this.InitializeComponent();
                this.AddHeaderCheckBox();
                PriceLoader.Init(this.AllSteamItemsGridView, ETableToLoad.RelistTable);
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

                this.AllSteamItemsGridView.Rows.Clear();
                MyListings.Clear();
                this.LoadListingButton.Enabled = false;
                Task.Run(
                    () =>
                        {
                            Program.LoadingForm.InitMyListingsLoadingProcess();
                            var listings = Program.LoadingForm.GetMyListings();
                            Program.LoadingForm.DeactivateForm();

                            var groupedListings = listings?.Sales?.GroupBy(x => new { x.HashName, x.Price });
                            foreach (var group in groupedListings)
                            {
                                var item = group.FirstOrDefault();
                                if (item == null)
                                {
                                    return;
                                }

                                MyListings.Add(item.HashName + item.Price, group.ToList());
                                this.AddListing(
                                    item.Name,
                                    group.Count(),
                                    SteamItemsUtils.GetClearType(item.Game),
                                    item.Date,
                                    item.Price,
                                    item.HashName);
                            }
                            
                            PriceLoader.StartPriceLoading(ETableToLoad.RelistTable);
                        });
                this.LoadListingButton.Enabled = true;
            }
            catch (Exception ex)
            {
                Logger.Critical("Error on loading listed market items", ex);
            }
        }

        private void AddListing(string name, int count, string type, string date, double price, string hashName)
        {
            Dispatcher.AsMainForm(() => this.AllSteamItemsGridView.Rows.Add(false, name, count, type, date, price, null, null, null, hashName));
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

            var items = MyListings[selectedHashName + selectedPrice];
            var item = items.FirstOrDefault();

            if (item == null)
            {
                return;
            }

            CommonUtils.AppendBoldText(this.ItemDescriptionTextBox, "Hash name: ");
            this.ItemDescriptionTextBox.AppendText(item.HashName + Environment.NewLine);

            CommonUtils.AppendBoldText(this.ItemDescriptionTextBox, "Game: ");
            this.ItemDescriptionTextBox.AppendText(item.Game + Environment.NewLine);

            CommonUtils.AppendBoldText(this.ItemDescriptionTextBox, "Appid: ");
            this.ItemDescriptionTextBox.AppendText(item.AppId + Environment.NewLine);

            CommonUtils.AppendBoldText(this.ItemDescriptionTextBox, "Url: ");
            this.ItemDescriptionTextBox.AppendText(item.Url);

            ImageUtils.UpdateImageOnItemPanelAsync(item.HashName, item.ImageUrl, this.ItemImageBox);
        }

        private void AveragePriceNumericUpDownValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.AveragePricePercentNumericUpDown.Value == 0)
                {
                    return;
                }

                var tmp = this.AveragePriceNumericUpDown.Value;
                this.AveragePricePercentNumericUpDown.Value = 0;
                this.AveragePriceNumericUpDown.Value = tmp;
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void AveragePricePercentNumericUpDownValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.AveragePriceNumericUpDown.Value == 0)
                {
                    return;
                }

                var tmp = this.AveragePricePercentNumericUpDown.Value;
                this.AveragePriceNumericUpDown.Value = 0;
                this.AveragePricePercentNumericUpDown.Value = tmp;
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void CurrentPriceNumericUpDownValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentPricePercentNumericUpDown.Value == 0)
                {
                    return;
                }

                var tmp = this.CurrentPriceNumericUpDown.Value;
                this.CurrentPricePercentNumericUpDown.Value = 0;
                this.CurrentPriceNumericUpDown.Value = tmp;
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void CurrentPricePercentNumericUpDownValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentPriceNumericUpDown.Value == 0)
                {
                    return;
                }

                var tmp = this.CurrentPricePercentNumericUpDown.Value;
                this.CurrentPriceNumericUpDown.Value = 0;
                this.CurrentPricePercentNumericUpDown.Value = tmp;
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void AverageMinusPriceRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.AverageMinusPriceRadioButton.Checked)
                {
                    this.AveragePriceNumericUpDown.Visible = true;
                    this.AveragePricePercentNumericUpDown.Visible = true;
                }
                else
                {
                    this.AveragePriceNumericUpDown.Visible = false;
                    this.AveragePricePercentNumericUpDown.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void CurrentMinusPriceRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentMinusPriceRadioButton.Checked)
                {
                    this.CurrentPriceNumericUpDown.Visible = true;
                    this.CurrentPricePercentNumericUpDown.Visible = true;
                }
                else
                {
                    this.CurrentPriceNumericUpDown.Visible = false;
                    this.CurrentPricePercentNumericUpDown.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }
    }
}