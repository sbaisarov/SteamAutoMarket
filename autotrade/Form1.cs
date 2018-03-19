using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autotrade
{
    public partial class Form1 : Form
    {
        ApiService services = new ApiService();


        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public Form1()
        {
            InitializeComponent();
            sidePanel.Height = saleLinkButton.Height;
            sidePanel.Top = saleLinkButton.Top;
        }

        private void appExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void leftPanelHideShowButton_Click(object sender, EventArgs e)
        {
            if(leftHeaderPanel.Height == 529 & leftHeaderPanel.Width == 166)
            {
                leftHeaderPanel.Height = 529;
                leftHeaderPanel.Width = 45;
                leftPanelHideShowButton.Left = 10;
                buyControl1.Width = 865;
                buyControl1.Left = 47;
                saleControl1.Width = 865;
                saleControl1.Left = 47;
            }
            else
            {
                leftHeaderPanel.Height = 529;
                leftHeaderPanel.Width = 166;
                leftPanelHideShowButton.Left = 128;
                buyControl1.Width = 742;
                buyControl1.Left = 168;
                saleControl1.Width = 742;
                saleControl1.Left = 168;
            }



        }

        private void saleLinkButton_Click(object sender, EventArgs e)
        {
            sidePanel.Height = saleLinkButton.Height;
            sidePanel.Top = saleLinkButton.Top;
            saleControl1.BringToFront();

        }

        private void buyLinkButton_Click(object sender, EventArgs e)
        {
            sidePanel.Height = buyLinkButton.Height;
            sidePanel.Top = buyLinkButton.Top;
            buyControl1.BringToFront();
        }

        //enabled move work space application
        private void move_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void move_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void move_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        
        
    }
}
