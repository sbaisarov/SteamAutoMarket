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
        
        Dictionary<int, InventoryRootModel> inventCollectionsTemp = new Dictionary<int, InventoryRootModel>();

        List<RgInventory> listInvent = new List<RgInventory>();

        ArrayList row;

        DataGridViewButtonColumn btn;

        public SaleSteamControl()
        {
            InitializeComponent();
        }
        
        private void SaleControl_Load(object sender, EventArgs e)
        {

            //List from inventory
            this.baseInvList = services.steamAllInventory();



            PagesCount = Convert.ToInt32(Math.Ceiling(baseInvList.descriptions.Count * 1.0 / pageRows));


            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "Name";
            dataGridView1.Columns[1].Name = "Inventory counts";

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
            //dataGridView1.Rows.Clear();

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
                    inventCollectionsTemp.Add(i, tempInvList);
                }
            }
            this.addingGridViewItems();
        }

        public void addingGridViewItems()
        {
            dataGridView1.Rows.Clear();
            btn = new DataGridViewButtonColumn();
            btn.HeaderText = "add";
            btn.Text = "add to items";
            btn.UseColumnTextForButtonValue = true;
            btn.Name = "add";
            btn.DefaultCellStyle.BackColor = Color.Blue;
            btn.Width = 70;

            foreach (InventoryRootModel keyValInv in this.inventCollectionsTemp.Values)
            {
                DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
                cmb.Name = "items";


                int s = 1;
                if (keyValInv.assets.Count != 0)
                {
                    for (; s <= keyValInv.assets.Count; s++)
                    {
                        cmb.Items.Add("" + s);
                    }
                }
                else
                {
                    MessageBox.Show("no Data");
                }
                
                dataGridView1.Rows.Add(keyValInv.descriptions[0].market_name, keyValInv.assets.Count);

                dataGridView1.Columns.RemoveAt(2);
                dataGridView1.Columns.Insert(2, cmb);
                if (dataGridView1.Columns[3].Name != "add")
                {
                    dataGridView1.Columns.RemoveAt(3);
                    dataGridView1.Columns.Insert(3, btn);
                }
            }
        }
        
        //Get Jey from Inventory
        public string getDescription_key(RgInventory rgInventory)
        {
            return rgInventory.classid + "_" + rgInventory.instanceid;
        }

        //Pagination
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


        
        /* hello world
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
        
        //Logo view
        public void keyboardKeyDataGrid(Object sender, KeyEventArgs e)
        {
            this.setInventLogoToPanel(this.dataGridView1.CurrentCell.RowIndex);
        }

        InventoryRootModel keyValInvView = new InventoryRootModel();
        private void dataGridView1_CellContentClick(Object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int column = e.ColumnIndex;
            
            if (this.dataGridView1.Rows[row].Cells[2].Value != null & this.dataGridView1.Rows[row].Cells[3].Selected == true)
            {
                string result = this.dataGridView1.Rows[row].Cells[2].Value.ToString();
                keyValInvView = this.inventCollectionsTemp[row];
                int c = Convert.ToInt32(result) - 1;
                for (; c >= 0; c--)
                {
                    //tempInvList.assets.Add(res.Value.assets[c]);
                    listInvent.Add(keyValInvView.assets[c]);
                    keyValInvView.assets.RemoveAt(c);
                }
                //keyValInvView.assets.ForEach( number => Console.WriteLine(number));
                this.ItemsCount.Text = "" + listInvent.Count;
                this.addingGridViewItems();
            }
            else if(this.dataGridView1.Rows[row].Cells[2].Value == null & this.dataGridView1.Rows[row].Cells[3].Selected == true)
            {
                MessageBox.Show("No data");
            }

            this.setInventLogoToPanel(e.RowIndex);
        }

        //Set logo to panel
        public void setInventLogoToPanel(int row)
        {
            Image image;
            int column = this.dataGridView1.CurrentCell.ColumnIndex;
            image = LoadImage("https://steamcommunity-a.akamaihd.net/economy/image/" + this.inventCollectionsTemp[row].descriptions[0].icon_url + "/192fx192f");
            this.panel1.BackgroundImage = image;
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
    }
}