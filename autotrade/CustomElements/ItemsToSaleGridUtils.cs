using autotrade.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static autotrade.Steam.TradeOffer.Inventory;

namespace autotrade.CustomElements {
    class ItemsToSaleGridUtils {

        public static void CellClick(DataGridView itemsToSaleGrid, int row) {
            var cell = GetGridPriceTextBoxCell(itemsToSaleGrid, row);
            itemsToSaleGrid.CurrentCell = cell;
            itemsToSaleGrid.BeginEdit(true);
        }

        public static void AddItemsToSale(DataGridView itemsToSaleGrid, List<RgFullItem> items) {
            var row = GetDataGridViewRowByMarketHashName(itemsToSaleGrid, items.First().Description.market_hash_name);
            if (row != null) {
                var countCell = GetGridCountTextBoxCell(itemsToSaleGrid, row.Index);
                var hidenItemsList = (List<RgFullItem>)GetGridHidenItemsListCell(itemsToSaleGrid, row.Index).Value;

                hidenItemsList.AddRange(items);
                countCell.Value = hidenItemsList.Sum(item => int.Parse(item.Asset.amount));
            } else {
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
            Logger.Info($"{items.Count} of {items.First().Description.name} was added to sale list");
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
            if (hidenItemsList == null) {
                return;
            }

            var itemMarketHashName = hidenItemsList.First().Description.market_hash_name;

            AllItemsListGridUtils.UpdateItemDescription(SaleControl.AllDescriptionsDictionary[itemMarketHashName], textBox, imageBox, lable);
        }

        public static void DeleteButtonClick(DataGridView allItemsGrid, DataGridView itemsToSaleGrid, int selectedRow) {
            var hidenItemsListCell = GetGridHidenItemsListCell(itemsToSaleGrid, selectedRow);
            var hidenItemsList = (List<RgFullItem>)hidenItemsListCell.Value;
            var itemMarketHashName = hidenItemsList.First().Description.market_hash_name;

            var allItemsGridRow = AllItemsListGridUtils.GetRowByItemMarketHashName(allItemsGrid, itemMarketHashName);
            if (allItemsGridRow != null) {
                AllItemsListGridUtils.AddItemsToExistRow(allItemsGrid, allItemsGridRow.Index, hidenItemsList);
            } else {
                AllItemsListGridUtils.AddNewItemCellAllItemsDataGridView(allItemsGrid, hidenItemsList);
            }

            itemsToSaleGrid.Rows.RemoveAt(selectedRow);

            Logger.Info($"{hidenItemsList.Count} of {hidenItemsList.First().Description.name} was deleted from sale list");
        }

        public static void DeleteUnmarketable(DataGridView allItemsGrid, DataGridView itemsToSaleGrid) {
            int totalDeletedItemsCount = 0;

            for (int rowIndex = 0; rowIndex < itemsToSaleGrid.RowCount; rowIndex++) {
                var hidenItemsCell = GetGridHidenItemsListCell(itemsToSaleGrid, rowIndex);
                if (hidenItemsCell == null || hidenItemsCell.Value == null) continue;

                var items = ((List<RgFullItem>)hidenItemsCell.Value);
                var stastCount = items.Count;

                var unmarketableList = new List<RgFullItem>();
                for (int i = 0; i < items.Count; i++) {
                    var item = items[i];
                    if (!item.Description.marketable) {
                        unmarketableList.Add(item);
                        items.RemoveAt(i--);
                    }
                }
                if (unmarketableList.Count > 0) {
                    totalDeletedItemsCount += unmarketableList.Count;
                    var untradableItem = unmarketableList.First();
                    var allItemsGridRow = AllItemsListGridUtils.GetRowByItemMarketHashName(allItemsGrid, untradableItem.Description.market_hash_name);
                    if (allItemsGridRow != null) {
                        AllItemsListGridUtils.AddItemsToExistRow(allItemsGrid, allItemsGridRow.Index, unmarketableList);
                    } else {
                        AllItemsListGridUtils.AddNewItemCellAllItemsDataGridView(allItemsGrid, unmarketableList);
                    }
                }
                if (items.Count == 0) itemsToSaleGrid.Rows.RemoveAt(rowIndex--);
                else if (stastCount != items.Count) GetGridCountTextBoxCell(itemsToSaleGrid, rowIndex).Value = items.Count;
            }
            Logger.Info($"{totalDeletedItemsCount} unmarketable items was deleted from sale list");
        }

        public static void DeleteUntradable(DataGridView allItemsGrid, DataGridView itemsToSaleGrid) {
            int totalDeletedItemsCount = 0;

            for (int rowIndex = 0; rowIndex < itemsToSaleGrid.RowCount; rowIndex++) {
                var hidenItemsCell = GetGridHidenItemsListCell(itemsToSaleGrid, rowIndex);
                if (hidenItemsCell == null || hidenItemsCell.Value == null) continue;

                var items = ((List<RgFullItem>)hidenItemsCell.Value);
                var stastCount = items.Count;

                var untradableList = new List<RgFullItem>();
                for (int i = 0; i < items.Count; i++) {
                    var item = items[i];
                    if (!item.Description.tradable) {
                        untradableList.Add(item);
                        items.RemoveAt(i--);
                    }
                }
                if (untradableList.Count > 0) {
                    totalDeletedItemsCount += untradableList.Count;
                    var untradableItem = untradableList.First();
                    var allItemsGridRow = AllItemsListGridUtils.GetRowByItemMarketHashName(allItemsGrid, untradableItem.Description.market_hash_name);
                    if (allItemsGridRow != null) {
                        AllItemsListGridUtils.AddItemsToExistRow(allItemsGrid, allItemsGridRow.Index, untradableList);
                    } else {
                        AllItemsListGridUtils.AddNewItemCellAllItemsDataGridView(allItemsGrid, untradableList);
                    }
                }

                if (items.Count == 0) itemsToSaleGrid.Rows.RemoveAt(rowIndex--);
                else if (stastCount != items.Count) GetGridCountTextBoxCell(itemsToSaleGrid, rowIndex).Value = items.Count;
            }
            Logger.Info($"{totalDeletedItemsCount} unmarketable items was deleted from sale list");
        }

        public static List<RgFullItem> GetRowItemsList(DataGridView itemsToSaleGrid, int rowIndex) {
            return (List<RgFullItem>)GetGridHidenItemsListCell(itemsToSaleGrid, rowIndex).Value;
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
