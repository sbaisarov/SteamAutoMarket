using System.Drawing;
using System.Windows.Forms;

namespace autotrade
{
    partial class SaleSteamControl
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
            this.SteamSaleDataGridView = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountToAddColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.AddButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.AddAllButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.titleSelectedItemsLength = new System.Windows.Forms.Label();
            this.ItemsCount = new System.Windows.Forms.Label();
            this.checkInvent = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ItemsToSaleListBox = new System.Windows.Forms.ListBox();
            this.DeleteItemButton = new System.Windows.Forms.Button();
            this.toolStripPaging.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SteamSaleDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(70, 70);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
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
            this.toolStripPaging.Location = new System.Drawing.Point(117, 542);
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
            // SteamSaleDataGridView
            // 
            this.SteamSaleDataGridView.AllowUserToAddRows = false;
            this.SteamSaleDataGridView.AllowUserToDeleteRows = false;
            this.SteamSaleDataGridView.AllowUserToOrderColumns = true;
            this.SteamSaleDataGridView.AllowUserToResizeColumns = false;
            this.SteamSaleDataGridView.AllowUserToResizeRows = false;
            this.SteamSaleDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SteamSaleDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.SteamSaleDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SteamSaleDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.CountColumn,
            this.CountToAddColumn,
            this.AddButtonColumn,
            this.AddAllButtonColumn});
            this.SteamSaleDataGridView.GridColor = System.Drawing.Color.White;
            this.SteamSaleDataGridView.Location = new System.Drawing.Point(3, 3);
            this.SteamSaleDataGridView.MultiSelect = false;
            this.SteamSaleDataGridView.Name = "SteamSaleDataGridView";
            this.SteamSaleDataGridView.RowHeadersVisible = false;
            this.SteamSaleDataGridView.Size = new System.Drawing.Size(444, 535);
            this.SteamSaleDataGridView.TabIndex = 7;
            this.SteamSaleDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SteamSaleDataGridView_CellClick);
            // 
            // NameColumn
            // 
            this.NameColumn.DataPropertyName = "SaleSteamControl";
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ToolTipText = "Item name";
            // 
            // CountColumn
            // 
            this.CountColumn.HeaderText = "Count";
            this.CountColumn.Name = "CountColumn";
            // 
            // CountToAddColumn
            // 
            this.CountToAddColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.CountToAddColumn.HeaderText = "Count to add";
            this.CountToAddColumn.Name = "CountToAddColumn";
            // 
            // AddButtonColumn
            // 
            this.AddButtonColumn.HeaderText = "Add";
            this.AddButtonColumn.Name = "AddButtonColumn";
            this.AddButtonColumn.Text = "Add";
            this.AddButtonColumn.UseColumnTextForButtonValue = true;
            // 
            // AddAllButtonColumn
            // 
            this.AddAllButtonColumn.HeaderText = "Add all";
            this.AddAllButtonColumn.Name = "AddAllButtonColumn";
            this.AddAllButtonColumn.Text = "Add all";
            this.AddAllButtonColumn.UseColumnTextForButtonValue = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.panel1.Location = new System.Drawing.Point(517, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(206, 185);
            this.panel1.TabIndex = 8;
            // 
            // titleSelectedItemsLength
            // 
            this.titleSelectedItemsLength.AutoSize = true;
            this.titleSelectedItemsLength.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.titleSelectedItemsLength.Location = new System.Drawing.Point(495, 297);
            this.titleSelectedItemsLength.Name = "titleSelectedItemsLength";
            this.titleSelectedItemsLength.Size = new System.Drawing.Size(197, 20);
            this.titleSelectedItemsLength.TabIndex = 9;
            this.titleSelectedItemsLength.Text = "Выбранные предметы:";
            // 
            // ItemsCount
            // 
            this.ItemsCount.AutoSize = true;
            this.ItemsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ItemsCount.Location = new System.Drawing.Point(687, 297);
            this.ItemsCount.Name = "ItemsCount";
            this.ItemsCount.Size = new System.Drawing.Size(17, 18);
            this.ItemsCount.TabIndex = 10;
            this.ItemsCount.Text = "0";
            // 
            // checkInvent
            // 
            this.checkInvent.Name = "checkInvent";
            // 
            // ItemsToSaleListBox
            // 
            this.ItemsToSaleListBox.BackColor = System.Drawing.Color.LightGray;
            this.ItemsToSaleListBox.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ItemsToSaleListBox.ItemHeight = 15;
            this.ItemsToSaleListBox.Location = new System.Drawing.Point(498, 321);
            this.ItemsToSaleListBox.Name = "ItemsToSaleListBox";
            this.ItemsToSaleListBox.ScrollAlwaysVisible = true;
            this.ItemsToSaleListBox.Size = new System.Drawing.Size(225, 184);
            this.ItemsToSaleListBox.TabIndex = 11;
            this.ItemsToSaleListBox.SelectedIndexChanged += new System.EventHandler(this.ItemsToSaleListBox_SelectedIndexChanged);
            // 
            // DeleteItemButton
            // 
            this.DeleteItemButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.DeleteItemButton.Enabled = false;
            this.DeleteItemButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteItemButton.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeleteItemButton.Location = new System.Drawing.Point(543, 512);
            this.DeleteItemButton.Name = "DeleteItemButton";
            this.DeleteItemButton.Size = new System.Drawing.Size(132, 27);
            this.DeleteItemButton.TabIndex = 12;
            this.DeleteItemButton.Text = "Удалить предмет";
            this.DeleteItemButton.UseVisualStyleBackColor = false;
            this.DeleteItemButton.Click += new System.EventHandler(this.DeleteItemButton_Click);
            // 
            // SaleSteamControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.DeleteItemButton);
            this.Controls.Add(this.ItemsToSaleListBox);
            this.Controls.Add(this.ItemsCount);
            this.Controls.Add(this.titleSelectedItemsLength);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStripPaging);
            this.Controls.Add(this.SteamSaleDataGridView);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SaleSteamControl";
            this.Size = new System.Drawing.Size(785, 571);
            this.Load += new System.EventHandler(this.SaleControl_Load);
            this.toolStripPaging.ResumeLayout(false);
            this.toolStripPaging.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SteamSaleDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void setSizeListView1(int width, int height)
        {
            // this.listView1.Size = new System.Drawing.Size(width, height);
            this.SteamSaleDataGridView.Size = new System.Drawing.Size(width, height);
        }

        public void setSizeListView2(int width, int height)
        {
        }

        public void setSizeTabControll(int width, int height)
        {

        }

        public void setPoint(int x, int y)
        {
        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private ToolStrip toolStripPaging;
        private ToolStripButton btnBackward;
        private ToolStripButton btnFirst;
        private ToolStripButton button1;
        private ToolStripButton button2;
        private ToolStripButton button3;
        private ToolStripButton button4;
        private ToolStripButton button5;
        private ToolStripButton btnLast;
        private ToolStripButton btnForward;
        private DataGridView SteamSaleDataGridView;
        private Panel panel1;
        private Label titleSelectedItemsLength;
        private Label ItemsCount;
        private DataGridViewCheckBoxColumn checkInvent;
        private DataGridViewTextBoxColumn NameColumn;
        private DataGridViewTextBoxColumn CountColumn;
        private DataGridViewComboBoxColumn CountToAddColumn;
        private DataGridViewButtonColumn AddButtonColumn;
        private DataGridViewButtonColumn AddAllButtonColumn;
        private ListBox ItemsToSaleListBox;
        private Button DeleteItemButton;
    }
}