using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Net;
using System.IO;
using SteamAuth;
using OPSkins;
using OPSkins.Model.Inventory;
using SteamKit2;

namespace autotrade
{
    public partial class SaleControl : UserControl
    {
        OPSkinsClient opsClient = new OPSkinsClient("0c60904c51f9a9c38a6439da2cad21");
        UserLogin steamClient = new UserLogin("", "");
        SteamGuardAccount guard = new SteamGuardAccount();
        OPSkins.Interfaces.IInventory opskinsInventory;

        public SaleControl()
        {
            InitializeComponent();
            opskinsInventory = new OPSkins.Interfaces.IInventory(opsClient);
        }
        
        private async void SaleControl_Load(object sender, EventArgs e)
        {
            //List from inventory
            var opsInventList = opskinsInventory.GetInventory().Items;
            //guard = JsonConvert.DeserializeObject<SteamGuardAccount>(File.ReadAllText(@""));
            //steamClient.TwoFactorCode = guard.GenerateSteamGuardCode();
            //steamClient.DoLogin();
            SteamID steamid = new SteamID(76561198177211015);
            var steamInventList = Interfaces.Steam.Inventory.GetInventory(steamid);

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

            if (opsInventList != null)
            {
                foreach(InventorySale invent in opsInventList)
                {
                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.Text = "Item" + invent.MarketName;
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
