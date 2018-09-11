namespace SteamAutoMarket
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        public Point DragCursorPoint;
        public Point DragFormPoint;
        public bool Dragging;

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
            //1051; 630
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
            this.Dragging = true;
            this.DragCursorPoint = Cursor.Position;
            this.DragFormPoint = Location;
        }

        public void Move_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.Dragging)
            {
                return;
            }

            var dif = Point.Subtract(Cursor.Position, new Size(this.DragCursorPoint));
            this.Location = Point.Add(this.DragFormPoint, new Size(dif));
        }

        public void Move_MouseUp(object sender, MouseEventArgs e)
        {
            this.Dragging = false;
        }

        public void AppCurtailButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        public void FocusSidePanelToMenuElement(Panel sidePanel, Button button)
        {
            sidePanel.Height = button.Height;
            sidePanel.Top = button.Top;
        }
    }
}