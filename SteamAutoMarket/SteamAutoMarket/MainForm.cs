namespace SteamAutoMarket
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Management;
    using System.Net;
    using System.Text;
    using System.Windows.Forms;
    using System.Collections.Specialized;

    using Newtonsoft.Json;
    using AutoUpdaterDotNET;

    public partial class MainForm : Form
    {
        private Point dragCursorPoint;

        private Point dragFormPoint;

        private bool dragging;

        public MainForm()
        {
            this.InitializeComponent();
            this.FocusSidePanelToMenuElement(this.SettingsLinkButton);
            if (!File.Exists("license.txt"))
            {
                throw new UnauthorizedAccessException("Cant get requered info");
            }

            var main = File.ReadAllText("license.txt");
            if (!this.Check(main))
            {
                throw new UnauthorizedAccessException("Access denied");
            }

            this.UpdateProgram();
        }

        public bool Check(string main)
        {
            var wb = WebRequest.Create("\u0068\u0074\u0074\u0070\u0073\u003a\u002f\u002f\u0077\u0077\u0077\u002e\u0073\u0074\u0065\u0061\u006d\u0062\u0069\u007a\u002e\u0073\u0074\u006f\u0072\u0065\u002f\u0061\u0070\u0069\u002f\u0063\u0068\u0065\u0063\u006b\u006c\u0069\u0063\u0065\u006e\u0073\u0065");
            wb.Method = "POST";
            var data = new NameValueCollection { ["key"] = main };

            // read key from user database.
            // read from the input field if key is not present in user database
            var mc = new ManagementClass("win32_processor");
            var moc = mc.GetInstances();
            var uid = moc.Cast<ManagementObject>().Select(x => x.Properties["processorID"]).FirstOrDefault()?.Value
                .ToString();

            try
            {
                var dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + "C" + @":""");
                dsk.Get();
                uid += dsk["VolumeSerialNumber"].ToString();
            }
            catch
            {
                throw new UnauthorizedAccessException("Cant get requered info");
            }

            data["hwid"] = uid;
            var postDataString = string.Empty;
            foreach (string key in data)
            {
                postDataString += key + "&=" + data[key];
            }

            var postData = Encoding.UTF8.GetBytes(postDataString);
            var dataStream = wb.GetRequestStream();
            dataStream.Write(postData, 0, postData.Length);
            dataStream.Close();
            var resp = wb.GetResponse();
            if (((HttpWebResponse)resp).StatusDescription != "OK")
            {
                throw new UnauthorizedAccessException("Access denied");
            }

            dataStream = resp.GetResponseStream();
            var reader = new StreamReader(dataStream);
            dynamic responseJson = JsonConvert.DeserializeObject(
                Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(reader.ReadToEnd())));
            dataStream.Close();
            reader.Close();
            resp.Close();
            return responseJson["success_3248237582"];
        }

        private void UpdateProgram()
        {
//            ServicePointManager.ServerCertificateValidationCallback +=
//                (sender, certificate, chain, sslPolicyErrors) => true;  // NOT FOR PRODUCTION
            AutoUpdater.RunUpdateAsAdmin = true;
            AutoUpdater.DownloadPath = Environment.CurrentDirectory;
            AutoUpdater.Start("https://www.steambiz.store/release/release.xml");
        }

        public void AppExitButtonClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void LeftPanelHideShowButtonClick(object sender, EventArgs e)
        {
            // 1051; 630
            const int SizeChange = 115;

            if (this.Width == 1061)
            {
                this.LogoImageBox.Visible = false;
                this.leftHeaderPanel.Width -= SizeChange;
                this.BotEdge.Left -= SizeChange;
                this.RightEdge.Left -= SizeChange;
                this.LeftPanelHideShowButton.Left = 13;
                this.MarketControlTab.Left -= SizeChange;
                this.SettingsControlTab.Left -= SizeChange;
                this.AppCurtailButton.Left -= SizeChange;
                this.AppExitButton.Left -= SizeChange;
                this.TradeControlTab.Left -= SizeChange;
                this.Width -= SizeChange;
                this.UserControlTab.Left -= SizeChange;
            }
            else
            {
                this.LogoImageBox.Visible = true;
                this.leftHeaderPanel.Width += SizeChange;
                this.BotEdge.Left += SizeChange;
                this.RightEdge.Left += SizeChange;
                this.LeftPanelHideShowButton.Left = 132;
                this.MarketControlTab.Left += SizeChange;
                this.SettingsControlTab.Left += SizeChange;
                this.AppCurtailButton.Left += SizeChange;
                this.AppExitButton.Left += SizeChange;
                this.TradeControlTab.Left += SizeChange;
                this.Width += SizeChange;
                this.UserControlTab.Left += SizeChange;
            }
        }

        public void SettingsLinkButtonClick(object sender, EventArgs e)
        {
            this.FocusSidePanelToMenuElement(this.SettingsLinkButton);
            this.SettingsControlTab.BringToFront();

            this.BringEdgesToFront();
        }

        public void SaleLinkButtonClick(object sender, EventArgs e)
        {
            this.FocusSidePanelToMenuElement(this.SaleLinkButton);
            this.MarketControlTab.BringToFront();

            this.BringEdgesToFront();
        }

        public void TradeLinkButtonClick(object sender, EventArgs e)
        {
            this.FocusSidePanelToMenuElement(this.TradeLinkButton);
            this.TradeControlTab.BringToFront();

            this.BringEdgesToFront();
        }

        public void UserLinkButtonClick(object sender, EventArgs e)
        {
            this.FocusSidePanelToMenuElement(this.UserLinkButton);
            this.UserControlTab.BringToFront();

            this.BringEdgesToFront();
        }

        public void BringEdgesToFront()
        {
            this.RightEdge.BringToFront();
            this.LeftEdge.BringToFront();
            this.BotEdge.BringToFront();
        }

        public void MoveMouseDown(object sender, MouseEventArgs e)
        {
            this.dragging = true;
            this.dragCursorPoint = Cursor.Position;
            this.dragFormPoint = this.Location;
        }

        public void MoveMouseMove(object sender, MouseEventArgs e)
        {
            if (!this.dragging)
            {
                return;
            }

            var dif = Point.Subtract(Cursor.Position, new Size(this.dragCursorPoint));
            this.Location = Point.Add(this.dragFormPoint, new Size(dif));
        }

        public void MoveMouseUp(object sender, MouseEventArgs e)
        {
            this.dragging = false;
        }

        public void AppCurtailButtonClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        public void FocusSidePanelToMenuElement(Button button)
        {
            this.SidePanel.Height = button.Height;
            this.SidePanel.Top = button.Top;
        }
    }
}