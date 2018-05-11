using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Globalization;
using OPSkins.Model.Inventory;
using static autotrade.Interfaces.Steam.TradeOffer.Inventory;
using SteamKit2;
using System.Collections;
using System.Threading.Tasks;
using System.Linq;

namespace autotrade {
    public partial class SaleSteamControl : UserControl {
        ApiServices services = new ApiServices();
        List<RgFullItem> AllItemsList = new List<RgFullItem>();

        public SaleSteamControl() {
            InitializeComponent();
        }

        private void SaleControl_Load(object sender, EventArgs e) {
            ProcessSteamInventory();
            FillSteamSaleDataGrid(AllItemsList);
        }

        #region GRID FUNCTIONS
        private void FillSteamSaleDataGrid(List<RgFullItem> itemsToAddList) {
            var groupedItems = itemsToAddList.GroupBy(item => item.Description.market_hash_name);

            int currentRowNumber = 0;
            foreach (var itemsGroup in groupedItems) {
                string itemName = itemsGroup.First().Description.name;
                int totalAmount = itemsGroup.Sum(sum => int.Parse(sum.Asset.amount));

                SteamSaleDataGridView.Rows.Add(itemName, totalAmount);
                GetGridHidenItemsListButtonCell(currentRowNumber).Value = itemsGroup.ToList();

                SetDefaultAmountToAddComboBoxCellValue(GetGridCountToAddComboBoxCell(currentRowNumber));

                currentRowNumber++;
            }
        }

        private void SteamSaleDataGridView_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) {
                return;
            }

            var hidenItemsList = (List<RgFullItem>)GetGridHidenItemsListButtonCell(e.RowIndex).Value;

            Task.Run(() => {
                var image = DownloadImage("https://steamcommunity-a.akamaihd.net/economy/image/" + hidenItemsList.First().Description.icon_url + "/192fx192f");
                if (image != null) ItemImageBox.BackgroundImage = ResizeImage(image, 100, 100);
            });

            if (e.ColumnIndex == 2) {
                var comboBoxCell = GetGridCountToAddComboBoxCell(e.RowIndex);
                int leftItemsCount = int.Parse(GetGridCountTextBoxCell(e.RowIndex).Value.ToString());

                if (comboBoxCell.Items.Count != leftItemsCount + 1) {
                    FillComboBoxCellFromMinToMaxValues(comboBoxCell, 0, leftItemsCount);
                }
                SteamSaleDataGridView.BeginEdit(true);
                comboBoxCell.Selected = true;
                ((DataGridViewComboBoxEditingControl)SteamSaleDataGridView.EditingControl).DroppedDown = true;
            }
            else if (e.ColumnIndex == 3) {
                var amountToAddComboBoxCell = GetGridCountToAddComboBoxCell(e.RowIndex);
                int itemsToAddCount = int.Parse(amountToAddComboBoxCell.Value.ToString());
                if (itemsToAddCount <= 0) return;

                var itemNameTextBoxCell = GetGridNameTextBoxCell(e.RowIndex);

                var countTextBoxCell = GetGridCountTextBoxCell(e.RowIndex);

                var itemsToSell = new List<RgFullItem>();

                int addedCount = 0;
                for (int i = hidenItemsList.Count - 1; i >= 0; i--) {
                    var item = hidenItemsList[i];
                    itemsToSell.Add(item);
                    hidenItemsList.RemoveAt(i);
                    if (++addedCount == itemsToAddCount) break;
                }

                ItemsToSaleGrid.Rows.Add(itemNameTextBoxCell.Value.ToString(), itemsToAddCount, itemsToSell);

                int totalAmount = int.Parse(countTextBoxCell.Value.ToString());
                countTextBoxCell.Value = totalAmount - itemsToAddCount;
                amountToAddComboBoxCell.Items.Add(0);
                amountToAddComboBoxCell.Value = 0;
            }
            else if (e.ColumnIndex == 4) {
                var countTextBoxCell = GetGridCountTextBoxCell(e.RowIndex);
                int itemsCount = int.Parse(countTextBoxCell.Value.ToString());
                if (itemsCount <= 0) return;

                var itemNameTextBoxCell = GetGridNameTextBoxCell(e.RowIndex);
                var amountToAddComboBoxCell = GetGridCountToAddComboBoxCell(e.RowIndex);

                ItemsToSaleGrid.Rows.Add(itemNameTextBoxCell.Value.ToString(), itemsCount, null);

                countTextBoxCell.Value = 0;
                SetDefaultAmountToAddComboBoxCellValue(amountToAddComboBoxCell);
            }
        }

        private void SteamSaleDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            if (e.Control is ComboBox cb) {
                cb.IntegralHeight = false;
                cb.MaxDropDownItems = 10;
            }
        }

        private void FillComboBoxCellFromMinToMaxValues(DataGridViewComboBoxCell comboBoxCell, int minValue, int maxValue) {
            if (minValue > maxValue) return;
            comboBoxCell.Value = null;
            comboBoxCell.Items.Clear();
            comboBoxCell.Value = "0";
            for (int i = minValue; i <= maxValue && i < 999; i++) {
                comboBoxCell.Items.Add(i.ToString());
            }
        }

        public void SetDefaultAmountToAddComboBoxCellValue(DataGridViewComboBoxCell cell) {
            cell.Items.Add(0);
            cell.Value = 0;
        }

        #region Cells getters
        private DataGridViewTextBoxCell GetGridNameTextBoxCell(int rowIndex) {
            return (DataGridViewTextBoxCell)SteamSaleDataGridView.Rows[rowIndex].Cells[0];
        }

        private DataGridViewTextBoxCell GetGridCountTextBoxCell(int rowIndex) {
            return (DataGridViewTextBoxCell)SteamSaleDataGridView.Rows[rowIndex].Cells[1];
        }

        private DataGridViewComboBoxCell GetGridCountToAddComboBoxCell(int rowIndex) {
            return (DataGridViewComboBoxCell)SteamSaleDataGridView.Rows[rowIndex].Cells[2];
        }

        private DataGridViewButtonCell GetGridAddButtonCell(int rowIndex) {
            return (DataGridViewButtonCell)SteamSaleDataGridView.Rows[rowIndex].Cells[3];
        }

        private DataGridViewButtonCell GetGridAddAllButtonCell(int rowIndex) {
            return (DataGridViewButtonCell)SteamSaleDataGridView.Rows[rowIndex].Cells[4];
        }

        private DataGridViewTextBoxCell GetGridHidenItemsListButtonCell(int rowIndex) {
            return (DataGridViewTextBoxCell)SteamSaleDataGridView.Rows[rowIndex].Cells[5];
        }
        #endregion

        #endregion

        #region utils
        private void ProcessSteamInventory() {
            var allItemsInventory = services.SteamAllInventory();
            foreach (var item in allItemsInventory.assets) {
                RgFullItem rgFullItem = new RgFullItem {
                    Asset = item,
                    Description = InventoryRootModel.GetDescription(item, allItemsInventory.descriptions)
                };
                AllItemsList.Add(rgFullItem);
            }
        }
        #endregion

        #region Item description
        private Image DownloadImage(string url) {
            try {
                WebRequest req = WebRequest.Create(url);
                WebResponse res = req.GetResponse();
                Stream imgStream = res.GetResponseStream();
                Image img1 = Image.FromStream(imgStream);
                imgStream.Close();
                return img1;
            } catch (WebException e) {
                return null;
            }
        }

        Image ResizeImage(Image img, int width, int height) {
            var res = new Bitmap(width, height);
            res.SetResolution(img.HorizontalResolution, img.VerticalResolution);
            using (var g = Graphics.FromImage(res)) {
                var dst = new Rectangle(0, 0, res.Width, res.Height);
                g.DrawImage(img, dst);
            }
            return res;
        }
        #endregion

        #region Items to sale
        private void DeleteItemButton_Click(object sender, EventArgs e) {

        }

        #region design
        private void DeleteItemButton_MouseLeave(object sender, EventArgs e) {
            DeleteItemButton.BackColor = Color.RoyalBlue;
        }

        private void DeleteItemButton_MouseEnter(object sender, EventArgs e) {
            DeleteItemButton.BackColor = Color.DodgerBlue;
        }

        private void ItemsToSaleGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
            if (!DeleteItemButton.Enabled) DeleteItemButton.Enabled = true;
        }

        private void ItemsToSaleGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) {
            DeleteItemButton.Enabled = (ItemsToSaleGrid.RowCount > 0) ? true : false;
        }
        #endregion

        #endregion
    }
}