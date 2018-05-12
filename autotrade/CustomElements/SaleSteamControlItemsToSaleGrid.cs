using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static autotrade.Interfaces.Steam.TradeOffer.Inventory;

namespace autotrade.CustomElements {
    class SaleSteamControlItemsToSaleGrid {

        public static void RowClick(DataGridView itemsToSaleGrid, int row, Dictionary<string,RgDescription> descriptions, DataGridView allItemsGrid, RichTextBox textBox, Panel imageBox, Label lable) {
            var hidenItemsListCell = GetGridHidenItemsListCell(itemsToSaleGrid, row);
            var hidenItemsList = (List<RgFullItem>)hidenItemsListCell.Value;
            var itemMarketHashName = hidenItemsList.First().Description.market_hash_name;

            var allItemsRow = SaleSteamControlAllItemsListGrid.GetRowByItemMarketHashName(allItemsGrid, itemMarketHashName);
            SaleSteamControlAllItemsListGrid.UpdateItemDescription(allItemsGrid, allItemsRow.Index, descriptions, textBox, imageBox, lable);
        }

        public static void DeleteButtonClick(DataGridView allItemsGrid, DataGridView itemsToSaleGrid) {
            var selectedRow = itemsToSaleGrid.SelectedRows[0];
            var hidenItemsListCell = GetGridHidenItemsListCell(itemsToSaleGrid, selectedRow.Index);
            var hidenItemsList = (List<RgFullItem>)hidenItemsListCell.Value;
            var itemMarketHashName = hidenItemsList.First().Description.market_hash_name;

            var allItemsGridRow = SaleSteamControlAllItemsListGrid.GetRowByItemMarketHashName(allItemsGrid, itemMarketHashName);
            SaleSteamControlAllItemsListGrid.AddItemsToRow(allItemsGrid, allItemsGridRow.Index, hidenItemsList);

            itemsToSaleGrid.Rows.RemoveAt(selectedRow.Index);
        }

        #region Cells getters
        private static DataGridViewTextBoxCell GetGridNameTextBoxCell(DataGridView grid, int rowIndex) {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[0];
        }

        private static DataGridViewTextBoxCell GetGridCountTextBoxCell(DataGridView grid, int rowIndex) {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[1];
        }

        private static DataGridViewTextBoxCell GetGridHidenItemsListCell(DataGridView grid, int rowIndex) {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[2];
        }
        #endregion
    }
}
