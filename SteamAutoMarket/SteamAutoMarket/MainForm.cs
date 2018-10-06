namespace SteamAutoMarket
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

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
                throw new UnauthorizedAccessException();
            }

            var a = File.ReadAllText("license.txt");
            if (!Program.CheckLicense(a))
            {
                throw new UnauthorizedAccessException();
            }

            Program.Update();
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