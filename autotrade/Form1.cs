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
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public Form1() {
            InitializeComponent();
            FocusSidePanelToMenuElement(sidePanel, settingsLinkButton);
        }

        private void AppExitButton_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void LeftPanelHideShowButton_Click(object sender, EventArgs e) {
            //913; 632
            if (this.Height == 632) {
                leftHeaderPanel.Width = 45;
                leftPanelHideShowButton.Left = 10;


                ////buy controll size
                //buyControl1.Width = 865;
                //buyControl1.Left = 10;

                ////sale controll size
                //saleControl1.Width = 865;
                //saleControl1.Left = 10;

                this.Width = 400;
            }
            else {
                leftHeaderPanel.Width = 166;
                leftPanelHideShowButton.Left = 128;
                //buy controll size
                //buyControl1.Width = 742;
                //buyControl1.Left = 168;
                //sale controll size
                //saleControl1.Width = 742;
                //saleControl1.Left = 168;
            }
        }

        private void SettingsLinkButton_Click(object sender, EventArgs e) {
            OpenSettingsMenu();
        }

        private void SaleLinkButton_Click(object sender, EventArgs e) {
            OpenSaleMenu();
        }

        private void BuyLinkButton_Click(object sender, EventArgs e) {
            FocusSidePanelToMenuElement(sidePanel, buyLinkButton);
            //buyControl1.BringToFront();
        }

        
        private void Move_MouseDown(object sender, MouseEventArgs e) {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Move_MouseMove(object sender, MouseEventArgs e) {
            if (dragging) {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void Move_MouseUp(object sender, MouseEventArgs e) {
            dragging = false;
        }

        private void AppExpandButton_Click(object sender, EventArgs e) {
            if (this.WindowState != FormWindowState.Maximized) {
                this.WindowState = FormWindowState.Maximized;
            }
            else {
                this.WindowState = FormWindowState.Normal;
            }

        }

        private void AppCurtailButton_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FocusSidePanelToMenuElement(Panel sidePanel, Button button) {
            sidePanel.Height = button.Height;
            sidePanel.Top = button.Top;
        }

        public void OpenSettingsMenu() {
            FocusSidePanelToMenuElement(sidePanel, settingsLinkButton);
            settingsControl.BringToFront();
            settingsControl.Enabled = true;
            saleControl.Enabled = false;
        }

        public void OpenSaleMenu() {
            FocusSidePanelToMenuElement(sidePanel, saleLinkButton);
            saleControl.BringToFront();
            saleControl.Enabled = true;
            settingsControl.Enabled = false;
        }

        public void LoadInventory() {
            saleControl.LoadInventory();
        }
    }
}