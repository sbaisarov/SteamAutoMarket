using System.Drawing;
using System.Windows.Forms;

namespace autotrade {
    partial class SaleSteamControl {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaleSteamControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SteamSaleDataGridView = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountToAddColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.AddButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.AddAllButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.HidenItemsListColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemImageBox = new System.Windows.Forms.Panel();
            this.titleSelectedItemsLength = new System.Windows.Forms.Label();
            this.ItemsCount = new System.Windows.Forms.Label();
            this.checkInvent = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DeleteItemButton = new System.Windows.Forms.Button();
            this.ItemsToSaleGrid = new System.Windows.Forms.DataGridView();
            this.AddedToSaleListItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddedToSaleListItemsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddedToSaleListHidenItemsList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.SteamSaleDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsToSaleGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(70, 70);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // SteamSaleDataGridView
            // 
            this.SteamSaleDataGridView.AllowUserToAddRows = false;
            this.SteamSaleDataGridView.AllowUserToDeleteRows = false;
            this.SteamSaleDataGridView.AllowUserToResizeColumns = false;
            this.SteamSaleDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Menu;
            this.SteamSaleDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.SteamSaleDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SteamSaleDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.SteamSaleDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SteamSaleDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.SteamSaleDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SteamSaleDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.CountColumn,
            this.CountToAddColumn,
            this.AddButtonColumn,
            this.AddAllButtonColumn,
            this.HidenItemsListColumn});
            this.SteamSaleDataGridView.GridColor = System.Drawing.Color.White;
            this.SteamSaleDataGridView.Location = new System.Drawing.Point(3, 3);
            this.SteamSaleDataGridView.MultiSelect = false;
            this.SteamSaleDataGridView.Name = "SteamSaleDataGridView";
            this.SteamSaleDataGridView.RowHeadersVisible = false;
            this.SteamSaleDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SteamSaleDataGridView.Size = new System.Drawing.Size(448, 565);
            this.SteamSaleDataGridView.TabIndex = 7;
            this.SteamSaleDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SteamSaleDataGridView_CellClick);
            this.SteamSaleDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.SteamSaleDataGridView_EditingControlShowing);
            // 
            // NameColumn
            // 
            this.NameColumn.DataPropertyName = "SaleSteamControl";
            this.NameColumn.FillWeight = 85.5619F;
            this.NameColumn.HeaderText = "Название предмета";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.ToolTipText = "Item name";
            // 
            // CountColumn
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CountColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.CountColumn.FillWeight = 35F;
            this.CountColumn.HeaderText = "Общее количество";
            this.CountColumn.Name = "CountColumn";
            this.CountColumn.ReadOnly = true;
            // 
            // CountToAddColumn
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.CountToAddColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.CountToAddColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.CountToAddColumn.FillWeight = 35F;
            this.CountToAddColumn.HeaderText = "Количество";
            this.CountToAddColumn.MaxDropDownItems = 10;
            this.CountToAddColumn.Name = "CountToAddColumn";
            this.CountToAddColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // AddButtonColumn
            // 
            this.AddButtonColumn.FillWeight = 30F;
            this.AddButtonColumn.HeaderText = "Добавить";
            this.AddButtonColumn.Name = "AddButtonColumn";
            this.AddButtonColumn.Text = "Add";
            this.AddButtonColumn.UseColumnTextForButtonValue = true;
            // 
            // AddAllButtonColumn
            // 
            this.AddAllButtonColumn.FillWeight = 30F;
            this.AddAllButtonColumn.HeaderText = "Добавить все";
            this.AddAllButtonColumn.Name = "AddAllButtonColumn";
            this.AddAllButtonColumn.Text = "Add all";
            this.AddAllButtonColumn.UseColumnTextForButtonValue = true;
            // 
            // HidenItemsListColumn
            // 
            this.HidenItemsListColumn.HeaderText = "HidenItemsList";
            this.HidenItemsListColumn.Name = "HidenItemsListColumn";
            this.HidenItemsListColumn.Visible = false;
            // 
            // panel1
            // 
            this.ItemImageBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ItemImageBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.ItemImageBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ItemImageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ItemImageBox.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.ItemImageBox.Location = new System.Drawing.Point(476, 17);
            this.ItemImageBox.Name = "panel1";
            this.ItemImageBox.Size = new System.Drawing.Size(120, 120);
            this.ItemImageBox.TabIndex = 8;
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
            // DeleteItemButton
            // 
            this.DeleteItemButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.DeleteItemButton.Enabled = false;
            this.DeleteItemButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteItemButton.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeleteItemButton.Location = new System.Drawing.Point(537, 511);
            this.DeleteItemButton.Name = "DeleteItemButton";
            this.DeleteItemButton.Size = new System.Drawing.Size(132, 27);
            this.DeleteItemButton.TabIndex = 12;
            this.DeleteItemButton.Text = "Удалить предмет";
            this.DeleteItemButton.UseVisualStyleBackColor = false;
            this.DeleteItemButton.Click += new System.EventHandler(this.DeleteItemButton_Click);
            this.DeleteItemButton.MouseEnter += new System.EventHandler(this.DeleteItemButton_MouseEnter);
            this.DeleteItemButton.MouseLeave += new System.EventHandler(this.DeleteItemButton_MouseLeave);
            // 
            // ItemsToSaleGrid
            // 
            this.ItemsToSaleGrid.AllowUserToAddRows = false;
            this.ItemsToSaleGrid.AllowUserToDeleteRows = false;
            this.ItemsToSaleGrid.AllowUserToResizeColumns = false;
            this.ItemsToSaleGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Menu;
            this.ItemsToSaleGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.ItemsToSaleGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemsToSaleGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.ItemsToSaleGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemsToSaleGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AddedToSaleListItemName,
            this.AddedToSaleListItemsCount,
            this.AddedToSaleListHidenItemsList});
            this.ItemsToSaleGrid.GridColor = System.Drawing.Color.White;
            this.ItemsToSaleGrid.Location = new System.Drawing.Point(476, 320);
            this.ItemsToSaleGrid.MultiSelect = false;
            this.ItemsToSaleGrid.Name = "ItemsToSaleGrid";
            this.ItemsToSaleGrid.ReadOnly = true;
            this.ItemsToSaleGrid.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ItemsToSaleGrid.RowHeadersVisible = false;
            this.ItemsToSaleGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ItemsToSaleGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ItemsToSaleGrid.Size = new System.Drawing.Size(247, 185);
            this.ItemsToSaleGrid.TabIndex = 13;
            this.ItemsToSaleGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.ItemsToSaleGrid_RowsAdded);
            this.ItemsToSaleGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.ItemsToSaleGrid_RowsRemoved);
            // 
            // AddedToSaleListItemName
            // 
            this.AddedToSaleListItemName.FillWeight = 163.7636F;
            this.AddedToSaleListItemName.HeaderText = "Name";
            this.AddedToSaleListItemName.Name = "AddedToSaleListItemName";
            this.AddedToSaleListItemName.ReadOnly = true;
            // 
            // AddedToSaleListItemsCount
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AddedToSaleListItemsCount.DefaultCellStyle = dataGridViewCellStyle7;
            this.AddedToSaleListItemsCount.FillWeight = 51.31258F;
            this.AddedToSaleListItemsCount.HeaderText = "Count";
            this.AddedToSaleListItemsCount.Name = "AddedToSaleListItemsCount";
            this.AddedToSaleListItemsCount.ReadOnly = true;
            // 
            // AddedToSaleListHidenItemsList
            // 
            this.AddedToSaleListHidenItemsList.HeaderText = "HidenItemsList";
            this.AddedToSaleListHidenItemsList.Name = "AddedToSaleListHidenItemsList";
            this.AddedToSaleListHidenItemsList.ReadOnly = true;
            this.AddedToSaleListHidenItemsList.Visible = false;
            // 
            // SaleSteamControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.ItemsToSaleGrid);
            this.Controls.Add(this.DeleteItemButton);
            this.Controls.Add(this.ItemsCount);
            this.Controls.Add(this.titleSelectedItemsLength);
            this.Controls.Add(this.ItemImageBox);
            this.Controls.Add(this.SteamSaleDataGridView);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SaleSteamControl";
            this.Size = new System.Drawing.Size(785, 571);
            this.Load += new System.EventHandler(this.SaleControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SteamSaleDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsToSaleGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void setSizeListView1(int width, int height) {
            // this.listView1.Size = new System.Drawing.Size(width, height);
            this.SteamSaleDataGridView.Size = new System.Drawing.Size(width, height);
        }

        public void setSizeListView2(int width, int height) {
        }

        public void setSizeTabControll(int width, int height) {

        }

        public void setPoint(int x, int y) {
        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private DataGridView SteamSaleDataGridView;
        private Panel ItemImageBox;
        private Label titleSelectedItemsLength;
        private Label ItemsCount;
        private DataGridViewCheckBoxColumn checkInvent;
        private Button DeleteItemButton;
        private DataGridView ItemsToSaleGrid;
        private DataGridViewTextBoxColumn AddedToSaleListItemName;
        private DataGridViewTextBoxColumn AddedToSaleListItemsCount;
        private DataGridViewTextBoxColumn AddedToSaleListHidenItemsList;
        private DataGridViewTextBoxColumn NameColumn;
        private DataGridViewTextBoxColumn CountColumn;
        private DataGridViewComboBoxColumn CountToAddColumn;
        private DataGridViewButtonColumn AddButtonColumn;
        private DataGridViewButtonColumn AddAllButtonColumn;
        private DataGridViewTextBoxColumn HidenItemsListColumn;
    }
}