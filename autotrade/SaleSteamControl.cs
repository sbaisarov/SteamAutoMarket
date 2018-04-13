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

namespace autotrade
{
    public partial class SaleSteamControl : UserControl
    {
        //ApiServices services = new ApiServices();
        ApiServices services = new ApiServices();

        private int CurrentPage = 1;
        int PagesCount = 1;
        int pageRows = 19;

        InventoryRootModel baseInvList = new InventoryRootModel();

        InventoryRootModel saleInvList = new InventoryRootModel();

        public SaleSteamControl()
        {
            InitializeComponent();
        }


        Interfaces.Steam.TradeOffer.Inventory inventory = new Interfaces.Steam.TradeOffer.Inventory();

        
        private void SaleControl_Load(object sender, EventArgs e)
        {
            //List from inventory
            this.baseInvList = services.steamAllInventory();



            PagesCount = Convert.ToInt32(Math.Ceiling(baseInvList.descriptions.Count * 1.0 / pageRows));

            // Set the column header names.
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Name";
            dataGridView1.Columns[1].Name = "No. to Sell";

            CurrentPage = 1;
            RefreshPagination();
            RebindGridForPageChange();

            //foreach for imageList images get from url
            /*
            foreach (Inventory invent in baseInvList)
            {

                imageList1.Images.Add(""+ invent.market_name +"", LoadImage(invent.img));
            }
            listView2.LargeImageList = imageList1;
            listView1.LargeImageList = imageList1;
            
            //foreach for listView
            int i = 0;
        
            foreach (Inventory invent in inventList)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.ImageKey = invent.market_name;
                listViewItem.Text = invent.market_name;
                listViewItem.BackColor = Color.Azure;
                this.listView2.Items.Add(listViewItem);
            }*/
        }

        private void RebindGridForPageChange()
        {
            //Rebinding the Datagridview with data
            int datasourcestartIndex = (CurrentPage - 1) * pageRows;
            dataGridView1.Rows.Clear();
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Click Data";
            btn.Text = "";
            btn.UseColumnTextForButtonValue = true;
            btn.Name = "+";
            btn.DefaultCellStyle.BackColor = Color.Green;
            btn.Width = 40;
            btn.FlatStyle = FlatStyle.Popup;

            ICollection<KeyValuePair<String, InventoryRootModel>> inventCollectionsTemp = new Dictionary<String, InventoryRootModel>();
            RgDescription rgDescription = new RgDescription();

            for (int i = datasourcestartIndex; i < datasourcestartIndex + pageRows; i++)
            {
                if (i >= baseInvList.descriptions.Count)
                    break;

                for(int k = 0; k < baseInvList.descriptions.Count; k++)
                {
                    rgDescription = this.baseInvList.descriptions[k];
                    InventoryRootModel tempInvList = new InventoryRootModel();
                    tempInvList.descriptions.Add(rgDescription);

                    //inventCollectionsTemp.Add(new KeyValuePair <string, InventoryRootModel>("sd", this.baseInvList.descriptions[i]));
                    //this.tempInvList.descriptions.Add(baseInvList.descriptions[i]);
                    for (int y = 0; y < this.baseInvList.assets.Count; y++)
                    {
                        string desc_key_value = getDescription_key(this.baseInvList.assets[y]);
                        if (desc_key_value == rgDescription.classid + "_" + rgDescription.instanceid)
                        {
                            tempInvList.assets.Add(this.baseInvList.assets[y]);
                        }
                    }
                    inventCollectionsTemp.Add(new KeyValuePair<string, InventoryRootModel>(i + "", tempInvList));
                }
            }
            foreach(KeyValuePair<string, InventoryRootModel> keyValInv in inventCollectionsTemp)
            {
                dataGridView1.Rows.Add(keyValInv.Value.descriptions[0].market_name, keyValInv.Value.assets.Count);
            }
            //dataGridView1.DataSource = inventCollectionsTemp;
            dataGridView1.Columns.Add(btn);
            dataGridView1.Refresh();
            //dataGridView1.Rows[0].Selected = true;
        }

        public string getDescription_key(RgInventory rgInventory)
        {
            return rgInventory.classid + "_" + rgInventory.instanceid;
        }

        private void RefreshPagination()
        {
            ToolStripButton[] btnNumberItems = new ToolStripButton[] { button1, button2, button3, button4, button5 };

            //pageStartIndex contains the first button number of pagination.
            int pageStartIndex = 1;

            if (PagesCount > 5 && CurrentPage > 2)
                pageStartIndex = CurrentPage - 2;

            if (PagesCount > 5 && CurrentPage > PagesCount - 2)
                pageStartIndex = PagesCount - 4;

            for (int i = pageStartIndex; i < pageStartIndex + 5; i++)
            {
                if (i > PagesCount)
                {
                    btnNumberItems[i - pageStartIndex].Visible = false;
                }
                else
                {
                    //Changing the page numbers
                    btnNumberItems[i - pageStartIndex].Text = i.ToString(CultureInfo.InvariantCulture);

                    //Setting the Appearance of the page number buttons
                    if (i == CurrentPage)
                    {
                        btnNumberItems[i - pageStartIndex].BackColor = Color.Black;
                        btnNumberItems[i - pageStartIndex].ForeColor = Color.White;
                    }
                    else
                    {
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

        //Method that handles the pagination button clicks
        private void ToolStripButtonClick(object sender, EventArgs e)
        {
            try
            {
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
            }
            catch (Exception) { }
        }


        private Image LoadImage(string url)
        {
            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();
            Stream imgStream = res.GetResponseStream();
            Image img1 = Image.FromStream(imgStream);
            imgStream.Close();
            return img1;
        }
        static Image ImageSize(Image img, int width, int height)
        {
            var res = new Bitmap(width, height);
            res.SetResolution(img.HorizontalResolution, img.VerticalResolution);
            using (var g = Graphics.FromImage(res))
            {
                var dst = new Rectangle(0, 0, res.Width, res.Height);
                g.DrawImage(img, dst);
            }
            return res;
        }
        /*
        public void onclickListItemView1(Object sender, EventArgs e)
        {
            ListViewItem viewItem = new ListViewItem();
            viewItem = listView1.SelectedItems[0];
            listView1.Items.Remove(viewItem);
            listView2.Items.Add(viewItem);
        }

        public void onclickListItemView2(Object sender, EventArgs e)
        {
            ListViewItem viewItem = new ListViewItem();
            viewItem = listView2.SelectedItems[0];
            listView2.Items.Remove(viewItem);
            listView1.Items.Add(viewItem);
        }
        */

        public void mouseHoverDataGridView1(Object sender, EventArgs e)
        {
            int row = this.dataGridView1.CurrentCell.RowIndex;

            if (row > 0)

                this.dataGridView1.Rows[row].Selected = true;
            DataGridViewRow viewRow = this.dataGridView1.Rows[row];
            //Image image = LoadImage(viewRow.Cells["img"].Value.ToString());

            //this.panel1.BackgroundImage = image;
        }

        public void mouseKeyDataGrid(Object sender, KeyEventArgs e)
        {
            int row = this.dataGridView1.CurrentCell.RowIndex;
            int column = this.dataGridView1.CurrentCell.ColumnIndex;
            this.dataGridView1.ColumnHeadersVisible = true;
            if (row > 0)

                this.dataGridView1.Rows[row].Cells[column].Selected = true;
            DataGridViewRow viewRow = this.dataGridView1.Rows[row];
            Image image = LoadImage(viewRow.Cells["img"].Value.ToString());

            this.panel1.BackgroundImage = image;
        }

        private void dataGridView1_CellContentClick(Object sender, DataGridViewCellEventArgs e)
        { 
            //Is bool gren and red
            if(this.dataGridView1.Rows[e.RowIndex].Cells[4].Style.BackColor != Color.Green && this.dataGridView1.Rows[e.RowIndex].Cells[4].Style.BackColor != Color.Red)
            {
                this.dataGridView1.Rows[e.RowIndex].Cells[4].Style.BackColor = Color.Green;
            }

            if (this.dataGridView1.Rows[e.RowIndex].Cells[4].Style.BackColor == Color.Green && e.ColumnIndex == 4)
            {
                dataGridView1.Rows[e.RowIndex].Cells[4].Style.BackColor = Color.Red;
                this.saleInvList.descriptions.Add(this.baseInvList.descriptions[e.RowIndex]);
                openWith.Add(new KeyValuePair<String, RgDescription>(e.RowIndex + "", this.baseInvList.descriptions[e.RowIndex]));
                ItemsLength.Text = openWith.Count +"";
            }
            else if(this.dataGridView1.Rows[e.RowIndex].Cells[4].Style.BackColor == Color.Red  && e.ColumnIndex == 4)
            {
                openWith.Remove(new KeyValuePair<String, RgDescription>(e.RowIndex +"", this.baseInvList.descriptions[e.RowIndex]));
                ItemsLength.Text = openWith.Count + "";
                dataGridView1.Rows[e.RowIndex].Cells[4].Style.BackColor = Color.Green;
            }
            
        }
        ICollection<KeyValuePair<String, RgDescription>> openWith = new Dictionary<String, RgDescription>();
    }
}