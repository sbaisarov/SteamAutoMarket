using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace autotrade
{
    public partial class SaleControl : UserControl
    {
        ApiService services = new ApiService();
        List<Inventory> inventList;

        public SaleControl()
        {
            InitializeComponent();

        }
        
        private async void SaleControl_Load(object sender, EventArgs e)
        {
            //List from inventory
            inventList = await services.GetInventoryAll();
            
            
            //image get from url
            /*WebRequest req = WebRequest.Create(inventList[0].img);
            WebResponse res = req.GetResponse();
            Stream imgStream = res.GetResponseStream();
            Image img1 = Image.FromStream(imgStream);
            imgStream.Close();
            //jsonLabel1.Image = img1;

            Image img2 = LoadImage(img1, 120, 100);
            button1.BackgroundImage = img2;
            Button btn = new Button();
            btn.Text = "btn";
            Padding padding = new Padding();
            padding.Top = 200;
            btn.Margin = padding;
            btn.BackgroundImage = img2;
            */
            
            if (inventList != null)
            {
                foreach(Inventory invent in inventList)
                {
                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.Text = "Item" + invent.market_name;
                    this.listView1.Items.Add(listViewItem);
                }
            }
            
        }

        static Image LoadImage(Image img, int width, int height)
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
