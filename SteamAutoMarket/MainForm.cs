using System;
using System.Drawing;
using System.Windows.Forms;

namespace SteamAutoMarket
{
    public partial class MainForm : Form
    {
        public Point dragCursorPoint;
        public Point dragFormPoint;
        public bool dragging;

        public MainForm()
        {
            InitializeComponent();
            FocusSidePanelToMenuElement(SidePanel, SettingsLinkButton);
        }

        public void AppExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void LeftPanelHideShowButton_Click(object sender, EventArgs e)
        {
            //1051; 630
            var sizeChange = 115;

            if (Width == 1061)
            {
                LogoImageBox.Visible = false;
                leftHeaderPanel.Width -= sizeChange;
                BotEdge.Left -= sizeChange;
                RightEdge.Left -= sizeChange;
                LeftPanelHideShowButton.Left = 13;
                MarketControlTab.Left -= sizeChange;
                SettingsControlTab.Left -= sizeChange;
                appCurtailButton.Left -= sizeChange;
                appExitButton.Left -= sizeChange;
                TradeControlTab.Left -= sizeChange;
                Width -= sizeChange;
            }
            else
            {
                LogoImageBox.Visible = true;
                leftHeaderPanel.Width += sizeChange;
                BotEdge.Left += sizeChange;
                RightEdge.Left += sizeChange;
                LeftPanelHideShowButton.Left = 132;
                MarketControlTab.Left += sizeChange;
                SettingsControlTab.Left += sizeChange;
                appCurtailButton.Left += sizeChange;
                appExitButton.Left += sizeChange;
                TradeControlTab.Left += sizeChange;
                Width += sizeChange;
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
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = Location;
        }

        public void Move_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                var dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        public void Move_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
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