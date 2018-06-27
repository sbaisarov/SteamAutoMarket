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
    public partial class Form1 : Form {
        public bool dragging = false;
        public Point dragCursorPoint;
        public Point dragFormPoint;

        public Form1() {
            InitializeComponent();
            FocusSidePanelToMenuElement(sidePanel, settingsLinkButton);
        }

        public void AppExitButton_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        public void LeftPanelHideShowButton_Click(object sender, EventArgs e) {
            //925; 630
            int sizeChange = 115;

            if (this.Width == 925) {
                leftHeaderPanel.Width -= sizeChange;
                LeftEdge2.Left -= sizeChange;
                BotEdge.Left -= sizeChange;
                RightEdge.Left -= sizeChange;
                leftPanelHideShowButton.Left = 13;
                SaleControl.Left -= sizeChange;
                SettingsControl.Left -= sizeChange;
                BuyControl.Left -= sizeChange;
                appCurtailButton.Left -= sizeChange;
                appExitButton.Left -= sizeChange;
                this.Width -= sizeChange;
                logoImageBox.Visible = false;
            } else {
                leftHeaderPanel.Width += sizeChange;
                LeftEdge2.Left += sizeChange;
                BotEdge.Left += sizeChange;
                RightEdge.Left += sizeChange;
                leftPanelHideShowButton.Left = 132;
                SaleControl.Left += sizeChange;
                SettingsControl.Left += sizeChange;
                BuyControl.Left += sizeChange;
                appCurtailButton.Left += sizeChange;
                appExitButton.Left += sizeChange;
                this.Width += sizeChange;
                logoImageBox.Visible = true;
            }
        }

        public void SettingsLinkButton_Click(object sender, EventArgs e) {
            FocusSidePanelToMenuElement(sidePanel, settingsLinkButton);
            SettingsControl.Visible = true;
            SaleControl.Visible = false;
            BuyControl.Visible = false;
        }

        public void SaleLinkButton_Click(object sender, EventArgs e) {
            FocusSidePanelToMenuElement(sidePanel, saleLinkButton);
            SaleControl.Visible = true;
            SettingsControl.Visible = false;
            BuyControl.Visible = false;
        }

        public void BuyLinkButton_Click(object sender, EventArgs e) {
            FocusSidePanelToMenuElement(sidePanel, buyLinkButton);
            BuyControl.Visible = true;
            SettingsControl.Visible = false;
            SaleControl.Visible = false;
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

        public void LoadInventory() {
            SaleControl.LoadInventory();
        }

        public void Form1_Load(object sender, EventArgs e) {
            SavedSettings settings = SavedSettings.Get();
            this.SettingsControl.LoggingLevelComboBox.SelectedIndex = settings.LOGGER_LEVEL;
        }
    }
}