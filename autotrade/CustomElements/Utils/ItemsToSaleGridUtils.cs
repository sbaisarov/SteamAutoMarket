using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SteamAutoMarket.CustomElements.Controls.Trade;
using SteamAutoMarket.Steam.TradeOffer.Models;
using SteamAutoMarket.Steam.TradeOffer.Models.Full;
using SteamAutoMarket.Utils;
using SteamAutoMarket.WorkingProcess.PriceLoader;

namespace SteamAutoMarket.CustomElements.Utils
{
    internal static class ItemsToSaleGridUtils
    {
        public static readonly int ItemNameColumnIndex = 0;
        public static readonly int CountColumnIndex = 1;
        public static readonly int CurrentColumnIndex = 2;
        public static readonly int AverageColumnIndex = 3;
        public static readonly int HiddenItemMarketHashNameIndex = 4;
        public static readonly int HiddenItemListIndex = 5;

        public static void CellClick(DataGridView itemsToSaleGrid, int row)
        {
            var cell = GetGridCurrentPriceTextBoxCell(itemsToSaleGrid, row);
            itemsToSaleGrid.CurrentCell = cell;
            itemsToSaleGrid.BeginEdit(true);
        }

        public static void AddItemsToSale(DataGridView itemsToSaleGrid, List<FullRgItem> items)
        {
            var row = GetDataGridViewRowByMarketHashName(itemsToSaleGrid, items.First().Description.MarketHashName);
            if (row != null)
            {
                var countCell = GetGridCountTextBoxCell(itemsToSaleGrid, row.Index);
                var hidenItemsList = (List<FullRgItem>)GetGridHidenItemsListCell(itemsToSaleGrid, row.Index).Value;

                hidenItemsList.AddRange(items);
                countCell.Value = hidenItemsList.Sum(item => int.Parse(item.Asset.Amount));
            }
            else
            {
                var rowIndex = itemsToSaleGrid.Rows.Add();

                var nameCell = GetGridNameTextBoxCell(itemsToSaleGrid, rowIndex);
                var countCell = GetGridCountTextBoxCell(itemsToSaleGrid, rowIndex);
                var hidenMarketHashNameCell = GetGridHidenItemsMarketHashName(itemsToSaleGrid, rowIndex);
                var hidenItemsListCell = GetGridHidenItemsListCell(itemsToSaleGrid, rowIndex);
                var currentPriceCell = GetGridCurrentPriceTextBoxCell(itemsToSaleGrid, rowIndex);
                var averagePriceCell = GetGridAveragePriceTextBoxCell(itemsToSaleGrid, rowIndex);

                var firstItem = items.First();
                var currentPriceObj = PriceLoader.CurrentPricesCache.Get(firstItem);
                var averagePriceObj = PriceLoader.AveragePricesCache.Get(firstItem);

                nameCell.Value = firstItem.Description.Name;
                countCell.Value = items.Sum(item => int.Parse(item.Asset.Amount));
                if (currentPriceObj != null) currentPriceCell.Value = currentPriceObj.Price;
                if (averagePriceObj != null) averagePriceCell.Value = averagePriceObj.Price;
                hidenMarketHashNameCell.Value = firstItem.Description.MarketHashName;
                hidenItemsListCell.Value = items;
            }
        }

        public static DataGridViewRow GetDataGridViewRowByMarketHashName(DataGridView itemsToSaleGrid,
            string marketHashName)
        {
            for (var i = 0; i < itemsToSaleGrid.RowCount; i++)
            {
                var row = GetGridHidenItemsMarketHashName(itemsToSaleGrid, i);
                if (row == null) continue;
                if (row.Value.Equals(marketHashName)) return itemsToSaleGrid.Rows[i];
            }

            return null;
        }

        public static void RowClick(DataGridView itemsToSaleGrid, int row,
            Dictionary<string, RgDescription> descriptions, DataGridView allItemsGrid, RichTextBox textBox,
            Panel imageBox, Label lable)
        {
            var hidenItemsListCell = GetGridHidenItemsListCell(itemsToSaleGrid, row);
            var hidenItemsList = (List<FullRgItem>)hidenItemsListCell.Value;
            if (hidenItemsList == null) return;

            var itemMarketHashName = hidenItemsList.First().Description.MarketHashName;

            AllItemsListGridUtils.UpdateItemDescription(TradeSendControl.AllDescriptionsDictionary[itemMarketHashName],
                textBox, imageBox, lable);
        }

        public static void DeleteButtonClick(DataGridView allItemsGrid, DataGridView itemsToSaleGrid, int selectedRow)
        {
            var hidenItemsListCell = GetGridHidenItemsListCell(itemsToSaleGrid, selectedRow);
            var hidenItemsList = (List<FullRgItem>)hidenItemsListCell.Value;
            var itemMarketHashName = hidenItemsList.First().Description.MarketHashName;

            var allItemsGridRow = AllItemsListGridUtils.GetRowByItemMarketHashName(allItemsGrid, itemMarketHashName);
            if (allItemsGridRow != null)
                AllItemsListGridUtils.AddItemsToExistRow(allItemsGrid, allItemsGridRow.Index, hidenItemsList);
            else
                AllItemsListGridUtils.AddNewItemCellAllItemsDataGridView(allItemsGrid, hidenItemsList);

            itemsToSaleGrid.Rows.RemoveAt(selectedRow);

            Logger.Debug(
                $"{hidenItemsList.Count} of {hidenItemsList.First().Description.Name} was deleted from sale list");
        }

        public static void DeleteUnmarketable(DataGridView allItemsGrid, DataGridView itemsToSaleGrid)
        {
            var totalDeletedItemsCount = 0;

            for (var rowIndex = 0; rowIndex < itemsToSaleGrid.RowCount; rowIndex++)
            {
                var hidenItemsCell = GetGridHidenItemsListCell(itemsToSaleGrid, rowIndex);
                if (hidenItemsCell == null || hidenItemsCell.Value == null) continue;

                var items = (List<FullRgItem>)hidenItemsCell.Value;
                var stastCount = items.Count;

                var unmarketableList = new List<FullRgItem>();
                for (var i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    if (!item.Description.IsMarketable)
                    {
                        unmarketableList.Add(item);
                        items.RemoveAt(i--);
                    }
                }

                if (unmarketableList.Count > 0)
                {
                    totalDeletedItemsCount += unmarketableList.Count;
                    var untradableItem = unmarketableList.First();
                    var allItemsGridRow =
                        AllItemsListGridUtils.GetRowByItemMarketHashName(allItemsGrid,
                            untradableItem.Description.MarketHashName);
                    if (allItemsGridRow != null)
                        AllItemsListGridUtils.AddItemsToExistRow(allItemsGrid, allItemsGridRow.Index, unmarketableList);
                    else
                        AllItemsListGridUtils.AddNewItemCellAllItemsDataGridView(allItemsGrid, unmarketableList);
                }

                if (items.Count == 0) itemsToSaleGrid.Rows.RemoveAt(rowIndex--);
                else if (stastCount != items.Count)
                    GetGridCountTextBoxCell(itemsToSaleGrid, rowIndex).Value = items.Count;
            }

            Logger.Debug($"{totalDeletedItemsCount} unmarketable items was deleted from sale list");
        }

        public static void DeleteUntradable(DataGridView allItemsGrid, DataGridView itemsToSaleGrid)
        {
            var totalDeletedItemsCount = 0;

            for (var rowIndex = 0; rowIndex < itemsToSaleGrid.RowCount; rowIndex++)
            {
                var hidenItemsCell = GetGridHidenItemsListCell(itemsToSaleGrid, rowIndex);
                if (hidenItemsCell == null || hidenItemsCell.Value == null) continue;

                var items = (List<FullRgItem>)hidenItemsCell.Value;
                var stastCount = items.Count;

                var untradableList = new List<FullRgItem>();
                for (var i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    if (!item.Description.IsTradable)
                    {
                        untradableList.Add(item);
                        items.RemoveAt(i--);
                    }
                }

                if (untradableList.Count > 0)
                {
                    totalDeletedItemsCount += untradableList.Count;
                    var untradableItem = untradableList.First();
                    var allItemsGridRow =
                        AllItemsListGridUtils.GetRowByItemMarketHashName(allItemsGrid,
                            untradableItem.Description.MarketHashName);
                    if (allItemsGridRow != null)
                        AllItemsListGridUtils.AddItemsToExistRow(allItemsGrid, allItemsGridRow.Index, untradableList);
                    else
                        AllItemsListGridUtils.AddNewItemCellAllItemsDataGridView(allItemsGrid, untradableList);
                }

                if (items.Count == 0) itemsToSaleGrid.Rows.RemoveAt(rowIndex--);
                else if (stastCount != items.Count)
                    GetGridCountTextBoxCell(itemsToSaleGrid, rowIndex).Value = items.Count;
            }

            Logger.Debug($"{totalDeletedItemsCount} unmarketable items was deleted from sale list");
        }

        public static List<FullRgItem> GetRowItemsList(DataGridView itemsToSaleGrid, int rowIndex)
        {
            return (List<FullRgItem>)GetGridHidenItemsListCell(itemsToSaleGrid, rowIndex).Value;
        }

        public static double? GetRowItemPrice(DataGridView itemsToSaleGrid, int rowIndex)
        {
            var row = GetGridCurrentPriceTextBoxCell(itemsToSaleGrid, rowIndex);
            return (double?)row?.Value;
        }

        public static double? GetRowAveragePrice(DataGridView itemsToSaleGrid, int rowIndex)
        {
            var row = GetGridAveragePriceTextBoxCell(itemsToSaleGrid, rowIndex);
            return (double?)row?.Value;
        }

        public static List<FullRgItem> GetFullRgItems(DataGridView grid, int rowIndex)
        {
            var hidenItemsListCell = GetGridHidenItemsListCell(grid, rowIndex);
            if (hidenItemsListCell == null) return new List<FullRgItem>();
            return (List<FullRgItem>)hidenItemsListCell.Value;
        }

        #region Cells getters

        public static DataGridViewTextBoxCell GetGridNameTextBoxCell(DataGridView grid, int rowIndex)
        {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[ItemNameColumnIndex];
        }

        public static DataGridViewTextBoxCell GetGridCountTextBoxCell(DataGridView grid, int rowIndex)
        {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[CountColumnIndex];
        }

        public static DataGridViewTextBoxCell GetGridCurrentPriceTextBoxCell(DataGridView grid, int rowIndex)
        {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[CurrentColumnIndex];
        }

        public static DataGridViewTextBoxCell GetGridAveragePriceTextBoxCell(DataGridView grid, int rowIndex)
        {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[AverageColumnIndex];
        }

        public static DataGridViewTextBoxCell GetGridHidenItemsListCell(DataGridView grid, int rowIndex)
        {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[HiddenItemListIndex];
        }

        public static DataGridViewTextBoxCell GetGridHidenItemsMarketHashName(DataGridView grid, int rowIndex)
        {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[HiddenItemMarketHashNameIndex];
        }

        #endregion
    }
}