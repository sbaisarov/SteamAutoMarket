using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SteamAutoMarket.CustomElements.Controls.Trade;
using SteamAutoMarket.Steam.TradeOffer.Models;
using SteamAutoMarket.Steam.TradeOffer.Models.Full;
using SteamAutoMarket.Utils;

namespace SteamAutoMarket.CustomElements.Utils
{
    internal static class AllItemsListGridUtils
    {
        public static readonly int ItemNameColumnIndex = 0;
        public static readonly int CountColumnIndex = 1;
        public static readonly int TypeColumnIndex = 2;
        public static readonly int CurrentColumnIndex = 3;
        public static readonly int AverageColumnIndex = 4;
        public static readonly int AmountToAddIndex = 5;
        public static readonly int AddButtonIndex = 6;
        public static readonly int HidenItemListIndex = 7;
        public static readonly int HidenItemImageIndex = 8;
        public static readonly int HidenItemMarketHashNameIndex = 9;

        public static DataGridViewRow GetRowByItemMarketHashName(DataGridView allItemsGrid, string marketHashName)
        {
            for (var i = 0; i < allItemsGrid.RowCount; i++)
            {
                var hidenMarketHashNameCell = GetGridHidenItemMarketHashNameCell(allItemsGrid, i);

                if ((string)hidenMarketHashNameCell.Value == marketHashName) return allItemsGrid.Rows[i];
            }

            return null;
        }

        public static void FillSteamSaleDataGrid(DataGridView allItemsGrid, List<FullRgItem> itemsToAddList)
        {
            var groupedItems = itemsToAddList.GroupBy(item => item.Description.MarketHashName);

            foreach (var itemsGroup in groupedItems)
            {
                AddNewItemCellAllItemsDataGridView(allItemsGrid, itemsGroup.ToList());

                var firstItem = itemsGroup.First();
                if (!TradeSendControl.AllDescriptionsDictionary.ContainsKey(firstItem.Description.MarketHashName))
                    TradeSendControl.AllDescriptionsDictionary.Add(firstItem.Description.MarketHashName,
                        firstItem.Description);
            }
        }

        public static void AddNewItemCellAllItemsDataGridView(DataGridView allItemsGrid,
            List<FullRgItem> itemsToAddList)
        {
            var currentRowNumber = allItemsGrid.Rows.Add();

            var nameCell = GetGridNameTextBoxCell(allItemsGrid, currentRowNumber);
            var countCell = GetGridCountTextBoxCell(allItemsGrid, currentRowNumber);
            var typeCell = GetGridTypeTextBoxCell(allItemsGrid, currentRowNumber);
            var comboBoxCell = GetGridCountToAddComboBoxCell(allItemsGrid, currentRowNumber);
            var hidenItemsListCell = GetGridHidenItemsListCell(allItemsGrid, currentRowNumber);
            var hidenMarketHashNameCell = GetGridHidenItemMarketHashNameCell(allItemsGrid, currentRowNumber);

            var firstItem = itemsToAddList.First();

            nameCell.Value = firstItem.Description.Name;
            countCell.Value = itemsToAddList.Sum(item => int.Parse(item.Asset.Amount));
            typeCell.Value = SteamItemsUtils.GetClearType(itemsToAddList.First());
            SetDefaultAmountToAddComboBoxCellValue(comboBoxCell);
            hidenItemsListCell.Value = itemsToAddList;
            hidenMarketHashNameCell.Value = firstItem.Description.MarketHashName;
        }


        public static void UpdateItemDescription(DataGridView allItemsGrid, int row, RichTextBox textBox,
            Panel imageBox, Label label)
        {
            var cell = GetGridHidenItemsListCell(allItemsGrid, row);
            if (cell == null) return;

            var hidenItemsList = (List<FullRgItem>)cell.Value;
            if (hidenItemsList == null) return;

            UpdateItemDescription(
                TradeSendControl.AllDescriptionsDictionary[hidenItemsList.First().Description.MarketHashName], textBox,
                imageBox, label);
        }

        public static void UpdateItemDescription(RgDescription description, RichTextBox textBox, Panel imageBox,
            Label label)
        {
            TradeSendControl.LastSelectedItemDescription = description;

            UpdateItemTextDescription(description, textBox, label);
            ImageUtils.UpdateItemImageOnPanelAsync(description, imageBox);
        }

        public static void GridComboBoxClick(DataGridView allItemsGrid, int row)
        {
            var comboBoxCell = GetGridCountToAddComboBoxCell(allItemsGrid, row);
            var leftItemsCount = int.Parse(GetGridCountTextBoxCell(allItemsGrid, row).Value.ToString());

            if (comboBoxCell.Items.Count != leftItemsCount + 1)
                FillComboBoxCellFromZeroToMaxValue(comboBoxCell, leftItemsCount);
            allItemsGrid.BeginEdit(true);
            comboBoxCell.Selected = true;
            ((DataGridViewComboBoxEditingControl)allItemsGrid.EditingControl).DroppedDown = true;
        }

        public static void GridAddButtonClick(DataGridView allItemsGrid, int row, DataGridView itemsToSaleGrid)
        {
            var countTextBoxCell = GetGridCountTextBoxCell(allItemsGrid, row);
            var amountToAddComboBoxCell = GetGridCountToAddComboBoxCell(allItemsGrid, row);
            var hiddenItemsList = (List<FullRgItem>)GetGridHidenItemsListCell(allItemsGrid, row).Value;

            var itemsToAddCount = int.Parse(amountToAddComboBoxCell.Value.ToString());
            if (itemsToAddCount <= 0) return;

            var itemsToSell = new List<FullRgItem>();

            var addedCount = 0;
            for (var i = hiddenItemsList.Count - 1; i >= 0; i--)
            {
                var item = hiddenItemsList[i];

                if (item.Asset.Amount.Equals("1"))
                {
                    //если это не стакающийся предмет
                    itemsToSell.Add(item);
                    hiddenItemsList.RemoveAt(i);
                    addedCount++;
                }
                else
                {
                    //если это стакающийся предмет
                    var itemsInSlotCount = int.Parse(item.Asset.Amount);
                    if (itemsInSlotCount + addedCount > itemsToAddCount)
                    {
                        //если нам нужно добавить НЕ ВСЕ предметы с 1 слота
                        var neededCount = itemsToAddCount - addedCount;
                        var neededAmountOfItem = item.CloneAsset();
                        neededAmountOfItem.Asset.Amount = neededCount.ToString();
                        itemsToSell.Add(neededAmountOfItem);
                        item.Asset.Amount = (int.Parse(item.Asset.Amount) - neededCount).ToString();
                    }
                    else
                    {
                        //если нам нужно добавить ВСЕ предметы с 1 слота
                        itemsToSell.Add(item);
                        hiddenItemsList.RemoveAt(i);
                        addedCount += itemsInSlotCount;
                    }
                }

                if (addedCount == itemsToAddCount) break;
            }

            ItemsToSaleGridUtils.AddItemsToSale(itemsToSaleGrid, itemsToSell);

            var totalAmount = int.Parse(countTextBoxCell.Value.ToString()); //изменение ячейки Общее количество
            if (itemsToAddCount == totalAmount)
            {
                allItemsGrid.Rows.RemoveAt(row);
            }
            else
            {
                countTextBoxCell.Value = totalAmount - itemsToAddCount;
                amountToAddComboBoxCell.Items.Add(0);
                amountToAddComboBoxCell.Value = 0;
            }
        }

        public static void GridAddAllButtonClick(DataGridView allItemsGrid, int row, DataGridView itemsToSaleGrid)
        {
            AddCellListToSale(allItemsGrid, itemsToSaleGrid, allItemsGrid.Rows[row]);
        }

        public static void AddCellListToSale(DataGridView allItemsGrid, DataGridView itemsToSaleGrid,
            params DataGridViewRow[] rows)
        {
            foreach (var row in rows)
            {
                var countTextBoxCell = GetGridCountTextBoxCell(allItemsGrid, row.Index);
                var hidenItemsListCell = GetGridHidenItemsListCell(allItemsGrid, row.Index);
                var hidenItemsList = (List<FullRgItem>)hidenItemsListCell.Value;

                var itemsCount = int.Parse(countTextBoxCell.Value.ToString());
                if (itemsCount <= 0) return;

                ItemsToSaleGridUtils.AddItemsToSale(itemsToSaleGrid, hidenItemsList);

                allItemsGrid.Rows.RemoveAt(row.Index);
            }
        }

        public static void AddItemsToExistRow(DataGridView allItemsGrid, int row, List<FullRgItem> items)
        {
            var countTextBoxCell = GetGridCountTextBoxCell(allItemsGrid, row);
            var hidenItemsListCell = GetGridHidenItemsListCell(allItemsGrid, row);
            var hidenItemsList = (List<FullRgItem>)hidenItemsListCell.Value;

            if (hidenItemsList == null)
            {
                hidenItemsListCell.Value = items;
                hidenItemsList = (List<FullRgItem>)hidenItemsListCell.Value;
            }
            else
            {
                hidenItemsList.AddRange(items);
            }

            countTextBoxCell.Value = hidenItemsList.Sum(item => int.Parse(item.Asset.Amount));
        }

        private static void FillComboBoxCellFromZeroToMaxValue(DataGridViewComboBoxCell comboBoxCell, int maxValue)
        {
            if (maxValue < 0) return;

            SetDefaultAmountToAddComboBoxCellValue(comboBoxCell);

            var step = 1;
            if (maxValue > 1000) step = (maxValue.ToString().Length - 2) * 10;

            for (var i = step; i <= maxValue; i += step) comboBoxCell.Items.Add(i.ToString());
        }

        private static void SetDefaultAmountToAddComboBoxCellValue(DataGridViewComboBoxCell comboBoxCell)
        {
            comboBoxCell.Value = null;
            comboBoxCell.Items.Clear();
            comboBoxCell.Items.Add("0");
            comboBoxCell.Value = "0";
        }

        private static void UpdateItemTextDescription(RgDescription description, RichTextBox textBox, Label label)
        {
            textBox.Clear();

            label.Text = description.Name;

            var descriptionText = "";
            if (description.Descriptions != null)
            {
                foreach (var item in description.Descriptions)
                {
                    var text = item.Value.Trim();
                    if (!string.IsNullOrWhiteSpace(text)) descriptionText += text + ", ";
                }

                if (descriptionText.EndsWith(", "))
                    descriptionText = descriptionText.Substring(0, descriptionText.Length - 2);
            }

            var tagsText = "";
            if (description.Tags != null)
            {
                foreach (var item in description.Tags)
                {
                    var text = item.LocalizedTagName.Trim();
                    if (!string.IsNullOrWhiteSpace(text)) tagsText += text + ", ";
                }

                if (tagsText.EndsWith(", ")) tagsText = tagsText.Substring(0, tagsText.Length - 2);
            }

            CommonUtils.AppendBoldText(textBox, "Game: ");
            textBox.AppendText(description.Appid + "\n");

            CommonUtils.AppendBoldText(textBox, "Name: ");
            textBox.AppendText(description.MarketHashName + "\n");

            CommonUtils.AppendBoldText(textBox, "Type: ");
            textBox.AppendText(description.Type);

            if (!string.IsNullOrWhiteSpace(descriptionText))
            {
                textBox.AppendText("\n");
                CommonUtils.AppendBoldText(textBox, "Description: ");
                textBox.AppendText(descriptionText);
            }

            if (!string.IsNullOrWhiteSpace(tagsText))
            {
                textBox.AppendText("\n");
                CommonUtils.AppendBoldText(textBox, "Tags: ");
                textBox.AppendText(tagsText);
            }
        }


        public static List<FullRgItem> GetFullRgItems(DataGridView grid, int rowIndex)
        {
            var hiddenItemsListCell = GetGridHidenItemsListCell(grid, rowIndex);
            if (hiddenItemsListCell == null) return new List<FullRgItem>();
            return (List<FullRgItem>)hiddenItemsListCell.Value;
        }

        public static double? GetRowItemPrice(DataGridView grid, int rowIndex)
        {
            var row = GetGridCurrentPriceTextBoxCell(grid, rowIndex);

            return (double?)row?.Value;
        }

        public static double? GetRowAveragePrice(DataGridView grid, int rowIndex)
        {
            var row = GetGridAveragePriceTextBoxCell(grid, rowIndex);
            return (double?)row?.Value;
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

        public static DataGridViewTextBoxCell GetGridTypeTextBoxCell(DataGridView grid, int rowIndex)
        {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[TypeColumnIndex];
        }

        public static DataGridViewTextBoxCell GetGridCurrentPriceTextBoxCell(DataGridView grid, int rowIndex)
        {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[CurrentColumnIndex];
        }

        public static DataGridViewTextBoxCell GetGridAveragePriceTextBoxCell(DataGridView grid, int rowIndex)
        {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[AverageColumnIndex];
        }

        public static DataGridViewComboBoxCell GetGridCountToAddComboBoxCell(DataGridView grid, int rowIndex)
        {
            return (DataGridViewComboBoxCell)grid.Rows[rowIndex].Cells[AmountToAddIndex];
        }

        public static DataGridViewButtonCell GetGridAddButtonCell(DataGridView grid, int rowIndex)
        {
            return (DataGridViewButtonCell)grid.Rows[rowIndex].Cells[AddButtonIndex];
        }

        public static DataGridViewTextBoxCell GetGridHidenItemsListCell(DataGridView grid, int rowIndex)
        {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[HidenItemListIndex];
        }

        public static DataGridViewTextBoxCell GetGridHidenItemImageCell(DataGridView grid, int rowIndex)
        {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[HidenItemImageIndex];
        }

        public static DataGridViewTextBoxCell GetGridHidenItemMarketHashNameCell(DataGridView grid, int rowIndex)
        {
            return (DataGridViewTextBoxCell)grid.Rows[rowIndex].Cells[HidenItemMarketHashNameIndex];
        }

        #endregion
    }
}