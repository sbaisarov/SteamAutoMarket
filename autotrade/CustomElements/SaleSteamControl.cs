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
using autotrade.CustomElements;

namespace autotrade {
    public partial class SaleSteamControl : UserControl {
        ApiServices services = new ApiServices();
        Dictionary<string, RgDescription> AllDescriptionsDictionary;

        public SaleSteamControl() {
            InitializeComponent();
        }

        private void SaleControl_Load(object sender, EventArgs e) {
            List<RgFullItem> allItemsList = ProcessSteamInventory();
            AllDescriptionsDictionary = SaleSteamControlAllItemsListGrid.FillSteamSaleDataGrid(AllSteamItemsGridView, allItemsList);
        }

        private void SteamSaleDataGridView_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) {
                return;
            }

            SaleSteamControlAllItemsListGrid.UpdateItemDescription(AllSteamItemsGridView, e.RowIndex, AllDescriptionsDictionary, ItemDescriptionTextBox, ItemImageBox, ItemNameLable);

            if (e.ColumnIndex == 2) {
                SaleSteamControlAllItemsListGrid.GridComboBoxClick(AllSteamItemsGridView, e.RowIndex);
            }
            else if (e.ColumnIndex == 3) {
                SaleSteamControlAllItemsListGrid.GridAddButtonClick(AllSteamItemsGridView, e.RowIndex, ItemsToSaleGridView);
            }
            else if (e.ColumnIndex == 4) {
                SaleSteamControlAllItemsListGrid.GridAddAllButtonClick(AllSteamItemsGridView, e.RowIndex, ItemsToSaleGridView);
            }
        }

        private void SteamSaleDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            if (e.Control is ComboBox cb) {
                cb.IntegralHeight = false;
                cb.MaxDropDownItems = 10;
            }
        }

        private List<RgFullItem> ProcessSteamInventory() {
            var allItemsInventory = services.SteamAllInventory();
            var allItemsList = new List<RgFullItem>();

            foreach (var item in allItemsInventory.assets) {
                RgFullItem rgFullItem = new RgFullItem {
                    Asset = item,
                    Description = InventoryRootModel.GetDescription(item, allItemsInventory.descriptions)
                };
                allItemsList.Add(rgFullItem);
            }

            return allItemsList;
        }

        private void DeleteItemButton_Click(object sender, EventArgs e) {
            SaleSteamControlItemsToSaleGrid.DeleteButtonClick(AllSteamItemsGridView, ItemsToSaleGridView);
        }

        #region Items to sale design
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
            DeleteItemButton.Enabled = (ItemsToSaleGridView.RowCount > 0) ? true : false;
        }
        #endregion

        private void ItemsToSaleGridView_CellClick(object sender, DataGridViewCellEventArgs e) {
            SaleSteamControlItemsToSaleGrid.RowClick(ItemsToSaleGridView, e.RowIndex, AllDescriptionsDictionary, AllSteamItemsGridView, ItemDescriptionTextBox, ItemImageBox, ItemNameLable);
        }
    }
}