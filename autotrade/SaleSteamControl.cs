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
        Dictionary<string, int> TableItemsCount = new Dictionary<string, int>();

        public SaleSteamControl() {
            InitializeComponent();
            this.CountColumn.Width = 60;
            this.AddButtonColumn.Width = 50;
            this.AddAllButtonColumn.Width = 60;
            this.CountColumn.Width = 150;
            this.NameColumn.ReadOnly = true;
            this.CountColumn.ReadOnly = true;
        }

        private void SaleControl_Load(object sender, EventArgs e) {
            var allItemsInventory = services.SteamAllInventory();

            foreach (var item in allItemsInventory.assets) {
                RgFullItem rgFullItem = new RgFullItem();
                rgFullItem.Asset = item;
                rgFullItem.Description = InventoryRootModel.GetDescription(item, allItemsInventory.descriptions);
                AllItemsList.Add(rgFullItem);
            }

            AllItemsList
                .GroupBy(item => item.Description.market_hash_name)
                .ToList()
                .ForEach(item => TableItemsCount.Add(
                    allItemsInventory.descriptions.First(description => item.Key == description.market_hash_name).name,
                    item.Sum(sum => int.Parse(sum.Asset.amount))));

            int currentRowNumber = 0;
            foreach (var item in TableItemsCount) {
                SteamSaleDataGridView.Rows.Add(item.Key, item.Value);
                DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)this.SteamSaleDataGridView.Rows[currentRowNumber].Cells[2];
                FillComboBoxCellFromMinToMaxValues(comboBoxCell, 0, item.Value);
                comboBoxCell.Value = "0";
                currentRowNumber++;
            }
        }

        private void SteamSaleDataGridView_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) {
                return;
            }

            if (e.ColumnIndex == 2) {
                DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)SteamSaleDataGridView.Rows[e.RowIndex].Cells[2];
                SteamSaleDataGridView.BeginEdit(true);
                comboBoxCell.Selected = true;
                ((DataGridViewComboBoxEditingControl)SteamSaleDataGridView.EditingControl).DroppedDown = true;
            }
            else if (e.ColumnIndex == 4) {
                DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)SteamSaleDataGridView.Rows[e.RowIndex].Cells[2];
                DataGridViewTextBoxCell textBoxCell = (DataGridViewTextBoxCell)SteamSaleDataGridView.Rows[e.RowIndex].Cells[0];
                string itemsCount = TableItemsCount[textBoxCell.Value.ToString()].ToString();
                comboBoxCell.Value = itemsCount;
                ItemsToSaleListBox.Items.Add($"{itemsCount} - {textBoxCell.Value.ToString()}");

            }
        }

        #region utils
        private void FillComboBoxCellFromMinToMaxValues(DataGridViewComboBoxCell comboBoxCell, int minValue, int maxValue) {
            if (minValue > maxValue) return;
            for (int i = minValue; i <= maxValue; i++) {
                comboBoxCell.Items.Add(i.ToString());
            }
        }
        #endregion

        #region old
        private int CurrentPage = 1;
        int PagesCount = 1;
        int pageRows = 19;

        InventoryRootModel baseInvList = new InventoryRootModel();

        InventoryRootModel saleInvList = new InventoryRootModel();

        Dictionary<int, InventoryRootModel> inventCollectionsTemp = new Dictionary<int, InventoryRootModel>();

        List<RgInventory> listInvent = new List<RgInventory>();

        InventoryRootModel keyValInvView = new InventoryRootModel();

        DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();



        //private void SaleControl_Load(object sender, EventArgs e) {
        //    //List from inventory
        //    this.baseInvList = services.SteamAllInventory();

        //    PagesCount = Convert.ToInt32(Math.Ceiling(baseInvList.descriptions.Count * 1.0 / pageRows));
        //    this.AddButtonColumn.Width = 60;
        //    CurrentPage = 1;
        //    RefreshPagination();
        //    RebindGridForPageChange();
        //}

        private void RebindGridForPageChange() {
            //Rebinding the Datagridview with data
            int datasourcestartIndex = (CurrentPage - 1) * pageRows;

            RgDescription rgDescription = new RgDescription();
            for (int i = datasourcestartIndex; i < 1; i++) {
                if (i >= baseInvList.descriptions.Count)
                    break;

                for (int k = 0; k < baseInvList.descriptions.Count; k++) {
                    rgDescription = this.baseInvList.descriptions[k];
                    InventoryRootModel tempInvList = new InventoryRootModel();
                    tempInvList.descriptions.Add(rgDescription);

                    for (int y = 0; y < this.baseInvList.assets.Count; y++) {
                        string desc_key_value = getDescription_key(this.baseInvList.assets[y]);
                        if (desc_key_value == rgDescription.classid + "_" + rgDescription.instanceid) {
                            tempInvList.assets.Add(this.baseInvList.assets[y]);
                        }
                    }
                    inventCollectionsTemp.Add(k, tempInvList);
                }
            }
            this.AddingGridViewItems();
        }

        #region Adding inventory to the DataGridView and DataGridViewComboBoxColumn
        public void AddingGridViewItems() {
            SteamSaleDataGridView.Rows.Clear();
            int rowIndex = 0;
            foreach (InventoryRootModel keyValInv in this.inventCollectionsTemp.Values) {
                SteamSaleDataGridView.Rows.Add(keyValInv.descriptions[0].market_name, keyValInv.assets.Count);
                SetCellComboBoxItems(SteamSaleDataGridView, rowIndex, 2, keyValInv);
                rowIndex++;
            }
        }
        #endregion

        #region Get identify from Inventory
        public string getDescription_key(RgInventory rgInventory) {
            return rgInventory.classid + "_" + rgInventory.instanceid;
        }
        #endregion

        #region Pagination
        private void RefreshPagination() {
            ToolStripButton[] btnNumberItems = new ToolStripButton[] { button1, button2, button3, button4, button5 };

            //pageStartIndex contains the first button number of pagination.
            int pageStartIndex = 1;

            if (PagesCount > 5 && CurrentPage > 2)
                pageStartIndex = CurrentPage - 2;

            if (PagesCount > 5 && CurrentPage > PagesCount - 2)
                pageStartIndex = PagesCount - 4;

            for (int i = pageStartIndex; i < pageStartIndex + 5; i++) {
                if (i > PagesCount) {
                    btnNumberItems[i - pageStartIndex].Visible = false;
                }
                else {
                    //Changing the page numbers
                    btnNumberItems[i - pageStartIndex].Text = i.ToString(CultureInfo.InvariantCulture);

                    //Setting the Appearance of the page number buttons
                    if (i == CurrentPage) {
                        btnNumberItems[i - pageStartIndex].BackColor = Color.Black;
                        btnNumberItems[i - pageStartIndex].ForeColor = Color.White;
                    }
                    else {
                        btnNumberItems[i - pageStartIndex].BackColor = Color.White;
                        btnNumberItems[i - pageStartIndex].ForeColor = Color.Black;
                    }
                }
            }

            //Enabling or Disalbing pagination first, last, previous , next buttons
            if (CurrentPage == 1)
                btnBackward.Enabled = btnFirst.Enabled = false;
            else
                btnBackward.Enabled = btnFirst.Enabled = true;

            if (CurrentPage == PagesCount)
                btnForward.Enabled = btnLast.Enabled = false;

            else
                btnForward.Enabled = btnLast.Enabled = true;
        }
        #endregion

        #region Method that handles the pagination button clicks
        private void ToolStripButtonClick(object sender, EventArgs e) {
            try {
                ToolStripButton button = ((ToolStripButton)sender);

                //Determining the current page
                if (button == btnBackward)
                    CurrentPage--;
                else if (button == btnForward)
                    CurrentPage++;
                else if (button == btnLast)
                    CurrentPage = PagesCount;
                else if (button == btnFirst)
                    CurrentPage = 1;
                else
                    CurrentPage = Convert.ToInt32(button.Text, CultureInfo.InvariantCulture);

                if (CurrentPage < 1)
                    CurrentPage = 1;
                else if (CurrentPage > PagesCount)
                    CurrentPage = PagesCount;

                //Rebind the Datagridview with the data.
                RebindGridForPageChange();

                //Change the pagiantions buttons according to page number
                RefreshPagination();
            } catch (Exception) { }
        }
        #endregion

        #region Event to add icons to the panel
        public void KeyboardKeyDataGrid(Object sender, KeyEventArgs e) {
            this.SetInventLogoToPanel(this.SteamSaleDataGridView.CurrentCell.RowIndex);
        }
        #endregion

        //private void DataGridView1_CellContentClick(Object sender, DataGridViewCellEventArgs e) {
        //    if (e.RowIndex < 0 || e.ColumnIndex < 0) {
        //        return;
        //    }

        //    if (e.ColumnIndex == 2) {
        //        //DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
        //        //DataGridViewCell itemCountCell = this.dataGridView1.Rows[e.RowIndex].Cells[1];
        //        //for (int i = 0; i < itemCountCell.Value; i++) {

        //        //}
        //        //comboBoxCell.Items =
        //    }
        //    if (this.dataGridView1.Rows[e.RowIndex].Cells[2].Value != null & this.dataGridView1.Rows[e.RowIndex].Cells[3].Selected == true) {
        //        string result = this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        //        keyValInvView = this.inventCollectionsTemp[e.RowIndex];
        //        int c = Convert.ToInt32(result) - 1;
        //        for (; c >= 0; c--) {
        //            //tempInvList.assets.Add(res.Value.assets[c]);
        //            listInvent.Add(keyValInvView.assets[c]);
        //            keyValInvView.assets.RemoveAt(c);
        //        }
        //        //keyValInvView.assets.ForEach( number => Console.WriteLine(number));
        //        this.ItemsCount.Text = "" + listInvent.Count;
        //        //this.addingGridViewItems();
        //    }
        //    else if (this.dataGridView1.Rows[e.RowIndex].Cells[2].Value == null & this.dataGridView1.Rows[e.RowIndex].Cells[3].Selected == true) {
        //        MessageBox.Show("No data");
        //    }

        //    this.SetInventLogoToPanel(e.RowIndex);
        //}

        #region Work with the logo of the inventory
        public void SetInventLogoToPanel(int row) {
            //Image image;
            //int column = this.dataGridView1.CurrentCell.ColumnIndex;
            //image = LoadImage("https://steamcommunity-a.akamaihd.net/economy/image/" + this.inventCollectionsTemp[row].descriptions[0].icon_url + "/192fx192f");
            //this.panel1.BackgroundImage = image;
        }

        private Image LoadImage(string url) {
            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();
            Stream imgStream = res.GetResponseStream();
            Image img1 = Image.FromStream(imgStream);
            imgStream.Close();
            return img1;
        }

        static Image ImageSize(Image img, int width, int height) {
            var res = new Bitmap(width, height);
            res.SetResolution(img.HorizontalResolution, img.VerticalResolution);
            using (var g = Graphics.FromImage(res)) {
                var dst = new Rectangle(0, 0, res.Width, res.Height);
                g.DrawImage(img, dst);
            }
            return res;
        }
        #endregion

        #region Adding items to Combo box from dataGrid View
        private void SetCellComboBoxItems(DataGridView dataGrid, int rowIndex, int colIndex, InventoryRootModel itemsToAdd) {
            DataGridViewComboBoxCell dgvcbc = (DataGridViewComboBoxCell)dataGrid.Rows[rowIndex].Cells[colIndex];
            //You might pass a boolean to determine whether to clear or not.
            dgvcbc.Items.Clear();
            for (int count = 1; count <= itemsToAdd.assets.Count; count++) {
                dgvcbc.Items.Add("" + count);
            }
        }
        #endregion

        #endregion

        private void ItemsToSaleListBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (ItemsToSaleListBox.SelectedItem.ToString() == "0") DeleteItemButton.Enabled = false;
            else DeleteItemButton.Enabled = true;

        }

        private void DeleteItemButton_Click(object sender, EventArgs e) {
            string selectedName = ItemsToSaleListBox.SelectedItem.ToString();
        }
    }
}