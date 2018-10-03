namespace SteamAutoMarket.CustomElements.Controls.Market
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using SteamAutoMarket.CustomElements.Forms;
    using SteamAutoMarket.CustomElements.Utils;
    using SteamAutoMarket.Steam.Market.Models;
    using SteamAutoMarket.Utils;
    using SteamAutoMarket.WorkingProcess;
    using SteamAutoMarket.WorkingProcess.MarketPriceFormation;
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

        public const int ListingHiddenMarketHashNameCellIndex = 8;

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
            try
            {
                this.AccountNameLable.Text = CurrentSession.SteamManager.Guard.AccountName;
                this.SplitterPanel.BackgroundImage = CurrentSession.AccountImage;
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
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
                            if (groupedListings == null)
                            {
                                return;
                            }

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
            Dispatcher.AsMainForm(
                () => this.AllSteamItemsGridView.Rows.Add(
                    false,
                    name,
                    count,
                    type,
                    date,
                    price,
                    null,
                    null,
                    hashName));
        }

        private void HeaderCheckBoxOnCheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                var state = this.HeaderCheckBox.Checked;

                foreach (var row in this.AllSteamItemsGridView.Rows.Cast<DataGridViewRow>())
                {
                    row.Cells[CheckBoxStateCellIndex].Value = state;
                }
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void AllSteamItemsGridViewSelectionChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
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

        private void CancelOverpricedButtonClick(object sender, EventArgs e)
        {
            try
            {
                var changeValueType = EChangeValueType.ChangeValueTypeNone;
                double? changeValue = 0;
                EMarketSaleType saleType;

                var rows = this.AllSteamItemsGridView.Rows.Cast<DataGridViewRow>();

                if (this.RecomendedPriceRadioButton.Checked)
                {
                    saleType = EMarketSaleType.Recommended;
                }
                else if (this.CurrentMinusPriceRadioButton.Checked)
                {
                    saleType = EMarketSaleType.LowerThanCurrent;
                    if (this.CurrentPriceNumericUpDown.Value != 0)
                    {
                        changeValueType = EChangeValueType.ChangeValueTypeByValue;
                        changeValue = (double)this.CurrentPriceNumericUpDown.Value;
                    }
                    else if (this.CurrentPricePercentNumericUpDown.Value != 0)
                    {
                        changeValueType = EChangeValueType.ChangeValueTypeByPercent;
                        changeValue = (double)this.CurrentPricePercentNumericUpDown.Value;
                    }
                }
                else if (this.AverageMinusPriceRadioButton.Checked)
                {
                    saleType = EMarketSaleType.LowerThanAverage;
                    if (this.AveragePriceNumericUpDown.Value != 0)
                    {
                        changeValueType = EChangeValueType.ChangeValueTypeByValue;
                        changeValue = (double)this.AveragePriceNumericUpDown.Value;
                    }
                    else if (this.AveragePricePercentNumericUpDown.Value != 0)
                    {
                        changeValueType = EChangeValueType.ChangeValueTypeByPercent;
                        changeValue = (double)this.AveragePricePercentNumericUpDown.Value;
                    }
                }
                else
                {
                    throw new ArgumentException("Unknown mark strategy");
                }

                foreach (var row in rows)
                {
                    double? minimalAllowedPrice = 0;

                    var listingPriceCell = row.Cells[ListingPriceCellIndex];
                    var currentPriceCell = row.Cells[ListingCurrentPriceCellIndex];
                    var averagePriceCell = row.Cells[ListingAveragePriceCellIndex];
                    var checkboxCell = (DataGridViewCheckBoxCell)row.Cells[CheckBoxStateCellIndex];
                    checkboxCell.Value = false;

                    var listingPrice = (double?)listingPriceCell?.Value;
                    var currentPrice = (double?)currentPriceCell?.Value;
                    var averagePrice = (double?)averagePriceCell?.Value;

                    if (listingPrice == currentPrice)
                    {
                        continue;
                    }

                    if (!listingPrice.HasValue || !currentPrice.HasValue || !averagePrice.HasValue)
                    {
                        continue;
                    }

                    switch (saleType)
                    {
                        case EMarketSaleType.Recommended:
                            if (averagePrice > currentPrice)
                            {
                                minimalAllowedPrice = averagePrice;
                            }
                            else if (currentPrice >= averagePrice)
                            {
                                minimalAllowedPrice = currentPrice - 0.01;
                            }

                            break;

                        case EMarketSaleType.LowerThanCurrent:
                            switch (changeValueType)
                            {
                                case EChangeValueType.ChangeValueTypeByValue:
                                    minimalAllowedPrice = currentPrice + changeValue;
                                    break;

                                case EChangeValueType.ChangeValueTypeByPercent:
                                    minimalAllowedPrice = currentPrice + (changeValue / 100 * currentPrice);
                                    break;

                                case EChangeValueType.ChangeValueTypeNone:
                                    minimalAllowedPrice = currentPrice;
                                    break;

                                default:
                                    throw new ArgumentOutOfRangeException();
                            }

                            break;

                        case EMarketSaleType.LowerThanAverage:

                            switch (changeValueType)
                            {
                                case EChangeValueType.ChangeValueTypeByValue:
                                    minimalAllowedPrice = averagePrice + changeValue;
                                    break;

                                case EChangeValueType.ChangeValueTypeByPercent:
                                    minimalAllowedPrice = averagePrice + (averagePrice / 100 * averagePrice);
                                    break;

                                case EChangeValueType.ChangeValueTypeNone:
                                    minimalAllowedPrice = averagePrice;
                                    break;

                                default:
                                    throw new ArgumentOutOfRangeException();
                            }

                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(saleType));
                    }

                    if (minimalAllowedPrice < listingPrice)
                    {
                        checkboxCell.Value = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void CancelSelectedButtonClick(object sender, EventArgs e)
        {
            try
            {
                var rows = this.AllSteamItemsGridView.Rows.Cast<DataGridViewRow>()
                    .Where(r => (bool)r.Cells[CheckBoxStateCellIndex].Value == true);

                var hashNames = rows.Select(
                    r => (string)r.Cells[ListingHiddenMarketHashNameCellIndex].Value
                         + (double)r.Cells[ListingPriceCellIndex].Value).ToList();

                var itemsToCancel = new List<MyListingsSalesItem>();
                hashNames.ForEach(n => itemsToCancel.AddRange(MyListings[n]));

                WorkingProcessForm.InitProcess(() => CurrentSession.SteamManager.CancelSellOrder(itemsToCancel));
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void CancelAllButtonClick(object sender, EventArgs e)
        {
            try
            {
                var rows = this.AllSteamItemsGridView.Rows.Cast<DataGridViewRow>();

                var hashNames = rows.Select(
                    r => (string)r.Cells[ListingHiddenMarketHashNameCellIndex].Value
                         + (double)r.Cells[ListingPriceCellIndex].Value).ToList();

                var itemsToCancel = new List<MyListingsSalesItem>();
                hashNames.ForEach(n => itemsToCancel.AddRange(MyListings[n]));

                WorkingProcessForm.InitProcess(() => CurrentSession.SteamManager.CancelSellOrder(itemsToCancel));
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }
    }
}