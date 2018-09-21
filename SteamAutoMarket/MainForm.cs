namespace SteamAutoMarket
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        private Point dragCursorPoint;

        private Point dragFormPoint;

        private bool dragging;

        public MainForm()
        {
            this.InitializeComponent();
            this.FocusSidePanelToMenuElement(this.SidePanel, this.SettingsLinkButton);
        }

        public void AppExitButtonClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void LeftPanelHideShowButton_Click(object sender, EventArgs e)
        {
            // 1051; 630
            const int SizeChange = 115;

            if (this.Width == 1061)
            {
                LogoImageBox.Visible = false;
                leftHeaderPanel.Width -= SizeChange;
                BotEdge.Left -= SizeChange;
                RightEdge.Left -= SizeChange;
                LeftPanelHideShowButton.Left = 13;
                MarketControlTab.Left -= SizeChange;
                SettingsControlTab.Left -= SizeChange;
                appCurtailButton.Left -= SizeChange;
                appExitButton.Left -= SizeChange;
                TradeControlTab.Left -= SizeChange;
                Width -= SizeChange;
            }
            else
            {
                LogoImageBox.Visible = true;
                leftHeaderPanel.Width += SizeChange;
                BotEdge.Left += SizeChange;
                RightEdge.Left += SizeChange;
                LeftPanelHideShowButton.Left = 132;
                MarketControlTab.Left += SizeChange;
                SettingsControlTab.Left += SizeChange;
                appCurtailButton.Left += SizeChange;
                appExitButton.Left += SizeChange;
                TradeControlTab.Left += SizeChange;
                Width += SizeChange;
            }
        }

        public void SettingsLinkButton_Click(object sender, EventArgs e)
        {
            FocusSidePanelToMenuElement(SidePanel, SettingsLinkButton);
            SettingsControlTab.BringToFront();

            RightEdge.BringToFront();
            LeftEdge.BringToFront();
            BotEdge.BringToFront();
        }

        public void SaleLinkButton_Click(object sender, EventArgs e)
        {
            FocusSidePanelToMenuElement(SidePanel, SaleLinkButton);
            MarketControlTab.BringToFront();

            RightEdge.BringToFront();
            LeftEdge.BringToFront();
            BotEdge.BringToFront();
        }

        private void TradeLinkButton_Click(object sender, EventArgs e)
        {
            FocusSidePanelToMenuElement(SidePanel, TradeLinkButton);
            TradeControlTab.BringToFront();

            RightEdge.BringToFront();
            LeftEdge.BringToFront();
            BotEdge.BringToFront();
        }

        public void Move_MouseDown(object sender, MouseEventArgs e)
        {
            this.dragging = true;
            this.dragCursorPoint = Cursor.Position;
            this.dragFormPoint = Location;
        }

        public void Move_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.dragging)
            {
                return;
            }

            var dif = Point.Subtract(Cursor.Position, new Size(this.dragCursorPoint));
            this.Location = Point.Add(this.dragFormPoint, new Size(dif));
        }

        public void Move_MouseUp(object sender, MouseEventArgs e)
        {
            this.dragging = false;
        }

        public void AppCurtailButton_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        public void FocusSidePanelToMenuElement(Panel sidePanel, Button button)
        {
            sidePanel.Height = button.Height;
            sidePanel.Top = button.Top;
        }
    }
}