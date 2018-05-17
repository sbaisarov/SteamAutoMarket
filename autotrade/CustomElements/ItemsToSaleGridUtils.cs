using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static autotrade.Interfaces.Steam.TradeOffer.Inventory;

namespace autotrade.CustomElements {
    class ItemsToSaleGridUtils {

        public static void AddItemsToSale(DataGridView itemsToSaleGrid, List<RgFullItem> items) {
            var row = GetDataGridViewRowByMarketHashName(itemsToSaleGrid, items.First().Description.market_hash_name);
            if (row != null) {
                var countCell = GetGridCountTextBoxCell(itemsToSaleGrid, row.Index);
                var hidenItemsList = (List<RgFullItem>)GetGridHidenItemsListCell(itemsToSaleGrid, row.Index).Value;

                hidenItemsList.AddRange(items);
                countCell.Value = hidenItemsList.Sum(item => int.Parse(item.Asset.amount));
            }
            else {
                var rowIndex = itemsToSaleGrid.Rows.Add();

                var nameCell = GetGridNameTextBoxCell(itemsToSaleGrid, rowIndex);
                var countCell = GetGridCountTextBoxCell(itemsToSaleGrid, rowIndex);
                var priceCell = GetGridPriceTextBoxCell(itemsToSaleGrid, rowIndex);
                var hidenMarketHashNameCell = GetGridHidenItemsMarketHashName(itemsToSaleGrid, rowIndex);
                var hidenItemsListCell = GetGridHidenItemsListCell(itemsToSaleGrid, rowIndex);

                var firstItem = items.First();

                nameCell.Value = firstItem.Description.name;
                countCell.Value = items.Sum(item => int.Parse(item.Asset.amount));
                priceCell.Value = null;
                hidenMarketHashNameCell.Value = firstItem.Description.market_hash_name;
                hidenItemsListCell.Value = items;
            }
        }

        private static DataGridViewRow GetDataGridViewRowByMarketHashName(DataGridView itemsToSaleGrid, string marketHashName) {
            for (int i = 0; i < itemsToSaleGrid.RowCount; i++) {
                if (GetGridHidenItemsMarketHashName(itemsToSaleGrid, i).Value.Equals(marketHashName)) {
                    return itemsToSaleGrid.Rows[i];
                }
            }
            return null;
        }

        public static void RowClick(DataGridView itemsToSaleGrid, int row, Dictionary<string, RgDescription> descriptions, DataGridView allItemsGrid, RichTextBox textBox, Panel imageBox, Label lable) {
            var hidenItemsListCell = GetGridHidenItemsListCell(itemsToSaleGrid, row);
            var hidenItemsList = (List<RgFullItem>)hidenItemsListCell.Value;
            if (hidenItemsList == null) return;

            var itemMarketHashName = hidenItemsList.First().Description.market_hash_name;

            AllItemsListGridUtils.UpdateItemDescription(SaleControl.AllDescriptionsDictionary[itemMarketHashName], textBox, imageBox, lable);
        }

        public static void DeleteButtonClick(DataGridView allItemsGrid, DataGridView itemsToSaleGrid) {
            var selectedRow = itemsToSaleGrid.CurrentCell.RowIndex;
            var hidenItemsListCell = GetGridHidenItemsListCell(itemsToSaleGrid, selectedRow);
            var hidenItemsList = (List<RgFullItem>)hidenItemsListCell.Value;
            var itemMarketHashName = hidenItemsList.First().Description.market_hash_name;

            var allItemsGridRow = AllItemsListGridUtils.GetRowByItemMarketHashName(allItemsGrid, itemMarketHashName);
            if (allItemsGridRow != null) {
                AllItemsListGridUtils.AddItemsToExistRow(allItemsGrid, allItemsGridRow.Index, hidenItemsList);
            }
            else {
                AllItemsListGridUtils.AddNewItemCellAllItemsDataGridView(allItemsGrid, hidenItemsList);
            }

            itemsToSaleGrid.Rows.RemoveAt(selectedRow);
        }

        #region Cells getters
        private static DataGridViewTextBoxCell GetGridNameTextBoxCell(DataGridView grid, int rowIndex) {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[0];
        }

        private static DataGridViewTextBoxCell GetGridCountTextBoxCell(DataGridView grid, int rowIndex) {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[1];
        }

        private static DataGridViewTextBoxCell GetGridPriceTextBoxCell(DataGridView grid, int rowIndex) {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[2];
        }

        private static DataGridViewTextBoxCell GetGridHidenItemsMarketHashName(DataGridView grid, int rowIndex) {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[3];
        }

        private static DataGridViewTextBoxCell GetGridHidenItemsListCell(DataGridView grid, int rowIndex) {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[4];
        }
        #endregion
    }
}
