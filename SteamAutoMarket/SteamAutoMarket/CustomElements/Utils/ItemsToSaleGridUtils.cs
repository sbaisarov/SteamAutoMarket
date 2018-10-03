namespace SteamAutoMarket.CustomElements.Utils
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using SteamAutoMarket.CustomElements.Controls.Trade;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.Utils;
    using SteamAutoMarket.WorkingProcess.PriceLoader;

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
                var hiddenItemsList = (List<FullRgItem>)GetGridHidenItemsListCell(itemsToSaleGrid, row.Index).Value;

                hiddenItemsList.AddRange(items);
                countCell.Value = hiddenItemsList.Sum(item => int.Parse(item.Asset.Amount));
            }
            else
            {
                var rowIndex = itemsToSaleGrid.Rows.Add();

                var nameCell = GetGridNameTextBoxCell(itemsToSaleGrid, rowIndex);
                var countCell = GetGridCountTextBoxCell(itemsToSaleGrid, rowIndex);
                var hiddenMarketHashNameCell = GetGridHidenItemsMarketHashName(itemsToSaleGrid, rowIndex);
                var hiddenItemsListCell = GetGridHidenItemsListCell(itemsToSaleGrid, rowIndex);
                var currentPriceCell = GetGridCurrentPriceTextBoxCell(itemsToSaleGrid, rowIndex);
                var averagePriceCell = GetGridAveragePriceTextBoxCell(itemsToSaleGrid, rowIndex);

                var firstItem = items.First();
                var currentPriceObj = PriceLoader.CurrentPricesCache.Get(firstItem.Description.MarketHashName);
                var averagePriceObj = PriceLoader.AveragePricesCache.Get(firstItem.Description.MarketHashName);

                nameCell.Value = firstItem.Description.Name;
                countCell.Value = items.Sum(item => int.Parse(item.Asset.Amount));
                if (currentPriceObj != null)
                {
                    currentPriceCell.Value = currentPriceObj.Price;
                }

                if (averagePriceObj != null)
                {
                    averagePriceCell.Value = averagePriceObj.Price;
                }

                hiddenMarketHashNameCell.Value = firstItem.Description.MarketHashName;
                hiddenItemsListCell.Value = items;
            }
        }

        public static DataGridViewRow GetDataGridViewRowByMarketHashName(
            DataGridView itemsToSaleGrid,
            string marketHashName)
        {
            for (var i = 0; i < itemsToSaleGrid.RowCount; i++)
            {
                var row = GetGridHidenItemsMarketHashName(itemsToSaleGrid, i);
                if (row == null)
                {
                    continue;
                }

                if (row.Value.Equals(marketHashName))
                {
                    return itemsToSaleGrid.Rows[i];
                }
            }

            return null;
        }

        public static void RowClick(
            DataGridView itemsToSaleGrid,
            int row,
            Dictionary<string, RgDescription> descriptions,
            DataGridView allItemsGrid,
            RichTextBox textBox,
            Panel imageBox,
            Label label)
        {
            var hiddenItemsListCell = GetGridHidenItemsListCell(itemsToSaleGrid, row);
            var hiddenItemsList = (List<FullRgItem>)hiddenItemsListCell.Value;
            if (hiddenItemsList == null)
            {
                return;
            }

            var itemMarketHashName = hiddenItemsList.First().Description.MarketHashName;

            AllItemsListGridUtils.UpdateItemDescription(
                TradeSendControl.AllDescriptionsDictionary[itemMarketHashName],
                textBox,
                imageBox,
                label);
        }

        public static void DeleteButtonClick(DataGridView allItemsGrid, DataGridView itemsToSaleGrid, int selectedRow)
        {
            var allItemsListGridUtils = new AllItemsListGridUtils(allItemsGrid);
            var hiddenItemsListCell = GetGridHidenItemsListCell(itemsToSaleGrid, selectedRow);
            var hiddenItemsList = (List<FullRgItem>)hiddenItemsListCell.Value;
            var itemMarketHashName = hiddenItemsList.First().Description.MarketHashName;

            var allItemsGridRow = allItemsListGridUtils.GetRowByItemMarketHashName(itemMarketHashName);
            if (allItemsGridRow != null)
            {
                allItemsListGridUtils.AddItemsToExistRow(allItemsGridRow.Index, hiddenItemsList);
            }
            else
            {
                allItemsListGridUtils.AddNewItemCellAllItemsDataGridView(hiddenItemsList);
            }

            itemsToSaleGrid.Rows.RemoveAt(selectedRow);

            Logger.Debug(
                $"{hiddenItemsList.Count} of {hiddenItemsList.First().Description.Name} was deleted from sale list");
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
            var hiddenItemsListCell = GetGridHidenItemsListCell(grid, rowIndex);
            if (hiddenItemsListCell == null)
            {
                return new List<FullRgItem>();
            }

            return (List<FullRgItem>)hiddenItemsListCell.Value;
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