using autotrade.CustomElements;
using autotrade.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autotrade {
    public partial class MainForm : Form {
        public bool dragging = false;
        public Point dragCursorPoint;
        public Point dragFormPoint;

        public MainForm() {
            InitializeComponent();
            FocusSidePanelToMenuElement(SidePanel, SettingsLinkButton);
        }

        public void AppExitButton_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        public void LeftPanelHideShowButton_Click(object sender, EventArgs e) {
            //1051; 630
            int sizeChange = 115;

            if (this.Width == 1061) {
                LogoImageBox.Visible = false;
                leftHeaderPanel.Width -= sizeChange;
                BotEdge.Left -= sizeChange;
                RightEdge.Left -= sizeChange;
                LeftPanelHideShowButton.Left = 13;
                this.MarketControlTab.Left -= sizeChange;
                SettingsControlTab.Left -= sizeChange;
                appCurtailButton.Left -= sizeChange;
                appExitButton.Left -= sizeChange;
                TradeControlTab.Left -= sizeChange;
                this.Width -= sizeChange;

            } else {
                LogoImageBox.Visible = true;
                leftHeaderPanel.Width += sizeChange;
                BotEdge.Left += sizeChange;
                RightEdge.Left += sizeChange;
                LeftPanelHideShowButton.Left = 132;
                this.MarketControlTab.Left += sizeChange;
                SettingsControlTab.Left += sizeChange;
                appCurtailButton.Left += sizeChange;
                appExitButton.Left += sizeChange;
                TradeControlTab.Left += sizeChange;
                this.Width += sizeChange;
            }
        }

        public void SettingsLinkButton_Click(object sender, EventArgs e) {
            FocusSidePanelToMenuElement(SidePanel, SettingsLinkButton);
            SettingsControlTab.BringToFront();

            RightEdge.BringToFront();
            LeftEdge.BringToFront();
            BotEdge.BringToFront();
        }

        public void SaleLinkButton_Click(object sender, EventArgs e) {
            FocusSidePanelToMenuElement(SidePanel, SaleLinkButton);
            this.MarketControlTab.BringToFront();

            RightEdge.BringToFront();
            LeftEdge.BringToFront();
            BotEdge.BringToFront();
        }

        private void TradeLinkButton_Click(object sender, EventArgs e) {
            FocusSidePanelToMenuElement(SidePanel, TradeLinkButton);
            TradeControlTab.BringToFront();

            RightEdge.BringToFront();
            LeftEdge.BringToFront();
            BotEdge.BringToFront();
        }


        public void Move_MouseDown(object sender, MouseEventArgs e) {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        public void Move_MouseMove(object sender, MouseEventArgs e) {
            if (dragging) {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        public void Move_MouseUp(object sender, MouseEventArgs e) {
            dragging = false;
        }

        public void AppCurtailButton_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
        }

        public void FocusSidePanelToMenuElement(Panel sidePanel, Button button) {
            sidePanel.Height = button.Height;
            sidePanel.Top = button.Top;
        }
    }
}