namespace SteamAutoMarket.CustomElements.Utils
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using SteamAutoMarket.CustomElements.Controls.Trade;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.Utils;

    internal class AllItemsListGridUtils
    {
        #region Cells indexes

        public readonly int ItemNameColumnIndex = 0;

        public readonly int CountColumnIndex = 1;

        public readonly int TypeColumnIndex = 2;

        public readonly int CurrentColumnIndex = 3;

        public readonly int AverageColumnIndex = 4;

        public readonly int AmountToAddIndex = 5;

        public readonly int AddButtonIndex = 6;

        public readonly int HiddenItemListIndex = 7;

        public readonly int HiddenItemImageIndex = 8;

        public readonly int HiddenItemMarketHashNameIndex = 9;

        #endregion

        private readonly DataGridView dataGrid;

        public AllItemsListGridUtils(DataGridView dataGrid)
        {
            this.dataGrid = dataGrid;
        }

        public static void UpdateItemDescription(
            RgDescription description,
            RichTextBox textBox,
            Panel imageBox,
            Label label)
        {
            TradeSendControl.LastSelectedItemDescription = description;

            UpdateItemTextDescription(description, textBox, label);
            ImageUtils.UpdateItemImageOnPanelAsync(description, imageBox);
        }

        public DataGridViewRow GetRowByItemMarketHashName(string marketHashName)
        {
            for (var i = 0; i < this.dataGrid.RowCount; i++)
            {
                var hiddenMarketHashNameCell = this.GetGridHiddenItemsListCell(i);

                if (hiddenMarketHashNameCell.Value.HashName == marketHashName)
                {
                    return this.dataGrid.Rows[i];
                }
            }

            return null;
        }

        public void FillSteamSaleDataGrid(List<FullRgItem> itemsToAddList)
        {
            var groupedItems = itemsToAddList.GroupBy(item => item.Description.MarketHashName);

            foreach (var itemsGroup in groupedItems)
            {
                this.AddNewItemCellAllItemsDataGridView(itemsGroup.ToList());

                var firstItem = itemsGroup.First();
                if (!TradeSendControl.AllDescriptionsDictionary.ContainsKey(firstItem.Description.MarketHashName))
                {
                    TradeSendControl.AllDescriptionsDictionary.Add(
                        firstItem.Description.MarketHashName,
                        firstItem.Description);
                }
            }
        }

        public void AddNewItemCellAllItemsDataGridView(List<FullRgItem> itemsToAddList)
        {
            var firstItem = itemsToAddList.First();
            var name = firstItem.Description.Name;
            var count = itemsToAddList.Sum(item => int.Parse(item.Asset.Amount));
            var type = SteamItemsUtils.GetClearType(itemsToAddList.First());
            var hiddenModel = new AllItemsHiddenCellModel
                                  {
                                      HashName = firstItem.Description.MarketHashName, ItemsList = itemsToAddList
                                  };

            this.CreateNewCell(name, count, type, hiddenModel);
        }

        public void UpdateItemDescription(int row, RichTextBox textBox, Panel imageBox, Label label)
        {
            var cell = this.GetGridHiddenItemsListCell(row);

            if (cell.Value?.ItemsList == null)
            {
                return;
            }

            UpdateItemDescription(
                TradeSendControl.AllDescriptionsDictionary[cell.Value.ItemsList.First().Description.MarketHashName],
                textBox,
                imageBox,
                label);
        }

        public List<FullRgItem> GetRowItemsList(int rowIndex)
        {
            return this.GetGridHiddenItemsListCell(rowIndex)?.Value?.ItemsList;
        }

        public void GridComboBoxClick(int row)
        {
            var comboBoxCell = this.GetGridCountToAddComboBoxCell(row);
            var leftItemsCount = int.Parse(this.GetGridCountTextBoxCell(row).Value.ToString());

            if (comboBoxCell.Cell.Items.Count != leftItemsCount + 1)
            {
                FillComboBoxCellFromZeroToMaxValue(comboBoxCell, leftItemsCount);
            }

            this.dataGrid.BeginEdit(true);
            comboBoxCell.Cell.Selected = true;
            ((DataGridViewComboBoxEditingControl)this.dataGrid.EditingControl).DroppedDown = true;
        }

        public void GridAddButtonClick(int row, DataGridView itemsToSaleGrid)
        {
            var countTextBoxCell = this.GetGridCountTextBoxCell(row);
            var amountToAddComboBoxCell = this.GetGridCountToAddComboBoxCell(row);
            var hiddenItemsList = this.GetGridHiddenItemsListCell(row)?.Value?.ItemsList;

            var itemsToAddCount = int.Parse(amountToAddComboBoxCell.Value);
            if (itemsToAddCount <= 0)
            {
                return;
            }

            if (hiddenItemsList == null)
            {
                hiddenItemsList = new List<FullRgItem>();
            }

            var itemsToAdd = new List<FullRgItem>();
            var addedCount = 0;
            for (var i = hiddenItemsList.Count - 1; i >= 0; i--)
            {
                var item = hiddenItemsList[i];

                if (item.Asset.Amount.Equals("1"))
                {
                    // если это не стакающийся предмет
                    itemsToAdd.Add(item);
                    hiddenItemsList.RemoveAt(i);
                    addedCount++;
                }
                else
                {
                    // если это стакающийся предмет
                    var itemsInSlotCount = int.Parse(item.Asset.Amount);
                    if (itemsInSlotCount + addedCount > itemsToAddCount)
                    {
                        // если нам нужно добавить НЕ ВСЕ предметы с 1 слота
                        var neededCount = itemsToAddCount - addedCount;
                        var neededAmountOfItem = item.CloneAsset();
                        neededAmountOfItem.Asset.Amount = neededCount.ToString();
                        itemsToAdd.Add(neededAmountOfItem);
                        item.Asset.Amount = (int.Parse(item.Asset.Amount) - neededCount).ToString();
                    }
                    else
                    {
                        // если нам нужно добавить ВСЕ предметы с 1 слота
                        itemsToAdd.Add(item);
                        hiddenItemsList.RemoveAt(i);
                        addedCount += itemsInSlotCount;
                    }
                }

                if (addedCount == itemsToAddCount)
                {
                    break;
                }
            }

            ItemsToSaleGridUtils.AddItemsToSale(itemsToSaleGrid, itemsToAdd);

            var totalAmount = int.Parse(countTextBoxCell.Value.ToString()); // изменение ячейки Общее количество
            if (itemsToAddCount == totalAmount)
            {
                this.dataGrid.Rows.RemoveAt(row);
            }
            else
            {
                countTextBoxCell.ChangeCellValue(totalAmount - itemsToAddCount);
                SetDefaultAmountToAddComboBoxCellValue(amountToAddComboBoxCell);
            }
        }

        public void AddCellListToSale(DataGridView itemsToSaleGrid, params DataGridViewRow[] rows)
        {
            foreach (var row in rows)
            {
                var countTextBoxCell = this.GetGridCountTextBoxCell(row.Index);
                var hiddenItemsListCell = this.GetGridHiddenItemsListCell(row.Index);

                var itemsCount = int.Parse(countTextBoxCell.Value.ToString());
                if (itemsCount <= 0)
                {
                    return;
                }

                ItemsToSaleGridUtils.AddItemsToSale(itemsToSaleGrid, hiddenItemsListCell.Value?.ItemsList);

                this.dataGrid.Rows.RemoveAt(row.Index);
            }
        }

        public void AddItemsToExistRow(int row, List<FullRgItem> items)
        {
            var countTextBoxCell = this.GetGridCountTextBoxCell(row);
            var hiddenItemsListCell = this.GetGridHiddenItemsListCell(row);
            var hiddenItemsList = hiddenItemsListCell.Value;

            if (hiddenItemsList?.ItemsList == null)
            {
                hiddenItemsListCell.Value.ItemsList = items;
                hiddenItemsList = hiddenItemsListCell.Value;
            }
            else
            {
                hiddenItemsList.ItemsList.AddRange(items);
            }

            countTextBoxCell.ChangeCellValue(hiddenItemsList.ItemsList.Sum(item => int.Parse(item.Asset.Amount)));
        }

        #region Cells getters

        public CellProcessor<DataGridViewTextBoxCell, string> GetGridNameTextBoxCell(int rowIndex)
        {
            return new CellProcessor<DataGridViewTextBoxCell, string>(
                this.dataGrid.Rows[rowIndex].Cells[this.ItemNameColumnIndex]);
        }

        public CellProcessor<DataGridViewTextBoxCell, long> GetGridCountTextBoxCell(int rowIndex)
        {
            return new CellProcessor<DataGridViewTextBoxCell, long>(
                (DataGridViewTextBoxCell)this.dataGrid.Rows[rowIndex].Cells[this.CountColumnIndex]);
        }

        public CellProcessor<DataGridViewTextBoxCell, string> GetGridTypeTextBoxCell(int rowIndex)
        {
            return new CellProcessor<DataGridViewTextBoxCell, string>(
                (DataGridViewTextBoxCell)this.dataGrid.Rows[rowIndex].Cells[this.TypeColumnIndex]);
        }

        public CellProcessor<DataGridViewTextBoxCell, double?> GetGridCurrentPriceTextBoxCell(int rowIndex)
        {
            return new CellProcessor<DataGridViewTextBoxCell, double?>(
                this.dataGrid.Rows[rowIndex].Cells[this.CurrentColumnIndex]);
        }

        public CellProcessor<DataGridViewTextBoxCell, double?> GetGridAveragePriceTextBoxCell(int rowIndex)
        {
            return new CellProcessor<DataGridViewTextBoxCell, double?>(
                this.dataGrid.Rows[rowIndex].Cells[this.AverageColumnIndex]);
        }

        public CellProcessor<DataGridViewComboBoxCell, string> GetGridCountToAddComboBoxCell(int rowIndex)
        {
            return new CellProcessor<DataGridViewComboBoxCell, string>(
                this.dataGrid.Rows[rowIndex].Cells[this.AmountToAddIndex]);
        }

        public CellProcessor<DataGridViewButtonCell, object> GetGridAddButtonCell(int rowIndex)
        {
            return new CellProcessor<DataGridViewButtonCell, object>(
                this.dataGrid.Rows[rowIndex].Cells[this.AddButtonIndex]);
        }

        public CellProcessor<DataGridViewTextBoxCell, AllItemsHiddenCellModel> GetGridHiddenItemsListCell(int rowIndex)
        {
            return new CellProcessor<DataGridViewTextBoxCell, AllItemsHiddenCellModel>(
                this.dataGrid.Rows[rowIndex].Cells[this.HiddenItemListIndex]);
        }

        #endregion

        private static void UpdateItemTextDescription(RgDescription description, RichTextBox textBox, Control label)
        {
            textBox.Clear();

            label.Text = description.Name;

            var descriptionText = string.Empty;
            if (description.Descriptions != null)
            {
                foreach (var item in description.Descriptions)
                {
                    var text = item.Value.Trim();
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        descriptionText += text + ", ";
                    }
                }

                if (descriptionText.EndsWith(", "))
                {
                    descriptionText = descriptionText.Substring(0, descriptionText.Length - 2);
                }
            }

            var tagsText = string.Empty;
            if (description.Tags != null)
            {
                foreach (var item in description.Tags)
                {
                    var text = item.LocalizedTagName.Trim();
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        tagsText += text + ", ";
                    }
                }

                if (tagsText.EndsWith(", "))
                {
                    tagsText = tagsText.Substring(0, tagsText.Length - 2);
                }
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

        private static void SetDefaultAmountToAddComboBoxCellValue(
            CellProcessor<DataGridViewComboBoxCell, string> comboBoxCell)
        {
            comboBoxCell.Cell.Value = null;
            comboBoxCell.Cell.Items.Clear();
            comboBoxCell.Cell.Items.Add("0");
            comboBoxCell.ChangeCellValue("0");
        }

        private static void FillComboBoxCellFromZeroToMaxValue(
            CellProcessor<DataGridViewComboBoxCell, string> comboBoxCell,
            int maxValue)
        {
            if (maxValue < 0)
            {
                return;
            }

            SetDefaultAmountToAddComboBoxCellValue(comboBoxCell);

            var step = 1;
            if (maxValue > 1000)
            {
                step = (maxValue.ToString().Length - 2) * 10;
            }

            for (var i = step; i <= maxValue; i += step)
            {
                comboBoxCell.Cell.Items.Add(i.ToString());
            }
        }

        private void CreateNewCell(string name, long count, string type, AllItemsHiddenCellModel hiddenCellModel)
        {
            var currentRowNumber = this.dataGrid.Rows.Add();

            var nameCell = this.GetGridNameTextBoxCell(currentRowNumber);
            var countCell = this.GetGridCountTextBoxCell(currentRowNumber);
            var typeCell = this.GetGridTypeTextBoxCell(currentRowNumber);
            var comboBoxCell = this.GetGridCountToAddComboBoxCell(currentRowNumber);
            var hiddenCell = this.GetGridHiddenItemsListCell(currentRowNumber);

            nameCell.ChangeCellValue(name);
            countCell.ChangeCellValue(count);
            typeCell.ChangeCellValue(type);
            hiddenCell.ChangeCellValue(hiddenCellModel);

            SetDefaultAmountToAddComboBoxCellValue(comboBoxCell);
        }
    }
}