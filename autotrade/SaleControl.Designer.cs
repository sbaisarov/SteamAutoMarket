using System.Windows.Forms;

namespace autotrade
{
    partial class SaleControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.inventTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStripPaging = new System.Windows.Forms.ToolStrip();
            this.btnBackward = new System.Windows.Forms.ToolStripButton();
            this.btnFirst = new System.Windows.Forms.ToolStripButton();
            this.button1 = new System.Windows.Forms.ToolStripButton();
            this.button2 = new System.Windows.Forms.ToolStripButton();
            this.button3 = new System.Windows.Forms.ToolStripButton();
            this.button4 = new System.Windows.Forms.ToolStripButton();
            this.button5 = new System.Windows.Forms.ToolStripButton();
            this.btnLast = new System.Windows.Forms.ToolStripButton();
            this.btnForward = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.listView2 = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.inventTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStripPaging.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(70, 70);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // inventTabControl
            // 
            this.inventTabControl.Controls.Add(this.tabPage1);
            this.inventTabControl.Controls.Add(this.tabPage2);
            this.inventTabControl.Location = new System.Drawing.Point(3, 0);
            this.inventTabControl.Name = "inventTabControl";
            this.inventTabControl.SelectedIndex = 0;
            this.inventTabControl.Size = new System.Drawing.Size(557, 438);
            this.inventTabControl.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.toolStripPaging);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.listView2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(549, 412);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "steam";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Location = new System.Drawing.Point(319, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(201, 158);
            this.panel1.TabIndex = 8;
            // 
            // toolStripPaging
            // 
            this.toolStripPaging.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.toolStripPaging.BackColor = System.Drawing.Color.RoyalBlue;
            this.toolStripPaging.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripPaging.CanOverflow = false;
            this.toolStripPaging.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripPaging.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripPaging.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBackward,
            this.btnFirst,
            this.button1,
            this.button2,
            this.button3,
            this.button4,
            this.button5,
            this.btnLast,
            this.btnForward});
            this.toolStripPaging.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripPaging.Location = new System.Drawing.Point(22, 381);
            this.toolStripPaging.Name = "toolStripPaging";
            this.toolStripPaging.ShowItemToolTips = false;
            this.toolStripPaging.Size = new System.Drawing.Size(218, 25);
            this.toolStripPaging.TabIndex = 0;
            this.toolStripPaging.Text = "toolStrip1";
            // 
            // btnBackward
            // 
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(23, 22);
            this.btnBackward.Text = "<";
            this.btnBackward.Click += new System.EventHandler(this.ToolStripButtonClick);
            // 
            // btnFirst
            // 
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(27, 22);
            this.btnFirst.Text = "<<";
            this.btnFirst.Click += new System.EventHandler(this.ToolStripButtonClick);
            // 
            // button1
            // 
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 22);
            this.button1.Text = "1";
            this.button1.Click += new System.EventHandler(this.ToolStripButtonClick);
            // 
            // button2
            // 
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(23, 22);
            this.button2.Text = "2";
            this.button2.Click += new System.EventHandler(this.ToolStripButtonClick);
            // 
            // button3
            // 
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(23, 22);
            this.button3.Text = "3";
            this.button3.Click += new System.EventHandler(this.ToolStripButtonClick);
            // 
            // button4
            // 
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(23, 22);
            this.button4.Text = "4";
            this.button4.Click += new System.EventHandler(this.ToolStripButtonClick);
            // 
            // button5
            // 
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(23, 22);
            this.button5.Text = "5";
            this.button5.Click += new System.EventHandler(this.ToolStripButtonClick);
            // 
            // btnLast
            // 
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(27, 22);
            this.btnLast.Text = ">>";
            this.btnLast.Click += new System.EventHandler(this.ToolStripButtonClick);
            // 
            // btnForward
            // 
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(23, 22);
            this.btnForward.Text = ">";
            this.btnForward.Click += new System.EventHandler(this.ToolStripButtonClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(288, 375);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mouseHoverDataGridView1);
            this.dataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mouseKeyDataGrid);
            // 
            // listView2
            // 
            this.listView2.BackColor = System.Drawing.Color.White;
            this.listView2.Location = new System.Drawing.Point(297, 3);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(250, 375);
            this.listView2.TabIndex = 5;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(549, 412);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "obskins";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // SaleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.inventTabControl);
            this.Name = "SaleControl";
            this.Size = new System.Drawing.Size(564, 441);
            this.Load += new System.EventHandler(this.SaleControl_Load);
            this.inventTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.toolStripPaging.ResumeLayout(false);
            this.toolStripPaging.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }
        
        public void setSizeListView1(int width, int height)
        {
           // this.listView1.Size = new System.Drawing.Size(width, height);
           this.dataGridView1.Size = new System.Drawing.Size(width, height);
        }

        public void setSizeListView2(int width, int height)
        {
            this.listView2.Size = new System.Drawing.Size(width, height);
        }

        public void setSizeTabControll(int width, int height)
        {
            this.inventTabControl.Size = new System.Drawing.Size(width, height);
        }

        public void setPoint(int x, int y)
        {
            this.listView2.Location = new System.Drawing.Point(x, y);
        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl inventTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listView2;
        private DataGridView dataGridView1;
        private ToolStripButton button1;
        private ToolStripButton btnLast;
        private ToolStripButton btnFirst;
        private ToolStripButton btnBackward;
        private ToolStripButton button5;
        private ToolStripButton button4;
        private ToolStripButton button3;
        private ToolStripButton button2;
        private ToolStripButton btnForward;
        private ToolStrip toolStripPaging;
        private Panel panel1;
    }
}