using System.Windows.Forms;

namespace autotrade.CustomElements {
    partial class TradeControl {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TradeControl));
            this.AllSteamItemsToTradeGridView = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountToAddColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.AddButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.HidenItemsListColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HidenItemImageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HidenItemMarketHashName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemImageBox = new System.Windows.Forms.Panel();
            this.checkInvent = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ItemsToTradeGridView = new System.Windows.Forms.DataGridView();
            this.AddedToSaleListItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddedToSaleListItemsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemToSalePriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HidenMarketHashNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddedToSaleListHidenItemsList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AveragePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDescriptionGroupBox = new System.Windows.Forms.GroupBox();
            this.ItemDescriptionTextBox = new autotrade.CustomElements.RichTextBoxWithNoPaint();
            this.ItemNameLable = new System.Windows.Forms.Label();
            this.ItemsToSaleGroupBox = new System.Windows.Forms.GroupBox();
            this.DeleteSelectedItemButton = new System.Windows.Forms.Button();
            this.AllSteamItemsGroupBox = new System.Windows.Forms.GroupBox();
            this.TradeGroupBox = new System.Windows.Forms.GroupBox();
            this.TradeTokenTextBox = new System.Windows.Forms.TextBox();
            this.TradeTokenLable = new System.Windows.Forms.Label();
            this.TradeParthenIdTextBox = new System.Windows.Forms.TextBox();
            this.PartnerIdLable = new System.Windows.Forms.Label();
            this.TradeSendGroupBox = new System.Windows.Forms.GroupBox();
            this.SendTradeButton = new System.Windows.Forms.Button();
            this.SplitterPanel = new System.Windows.Forms.Panel();
            this.AddAllToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.LoadInventoryButton = new System.Windows.Forms.Button();
            this.OpenGameInventoryPageButton = new System.Windows.Forms.Button();
            this.OpenMarketPageButtonClick = new System.Windows.Forms.Button();
            this.RefreshInventoryButton = new System.Windows.Forms.Button();
            this.AddAllButton = new System.Windows.Forms.Button();
            this.InventoryGroupBox = new System.Windows.Forms.GroupBox();
            this.InventoryContextIdLabel = new System.Windows.Forms.Label();
            this.InventoryContextIdComboBox = new System.Windows.Forms.ComboBox();
            this.InventoryAppIdLabel = new System.Windows.Forms.Label();
            this.InventoryAppIdComboBox = new System.Windows.Forms.ComboBox();
            this.AccountNameLable = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AllSteamItemsToTradeGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsToTradeGridView)).BeginInit();
            this.ItemDescriptionGroupBox.SuspendLayout();
            this.ItemsToSaleGroupBox.SuspendLayout();
            this.AllSteamItemsGroupBox.SuspendLayout();
            this.TradeGroupBox.SuspendLayout();
            this.TradeSendGroupBox.SuspendLayout();
            this.InventoryGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AllSteamItemsToTradeGridView
            // 
            this.AllSteamItemsToTradeGridView.AllowUserToAddRows = false;
            this.AllSteamItemsToTradeGridView.AllowUserToDeleteRows = false;
            this.AllSteamItemsToTradeGridView.AllowUserToResizeColumns = false;
            this.AllSteamItemsToTradeGridView.AllowUserToResizeRows = false;
            this.AllSteamItemsToTradeGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AllSteamItemsToTradeGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.AllSteamItemsToTradeGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.AllSteamItemsToTradeGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AllSteamItemsToTradeGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.AllSteamItemsToTradeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AllSteamItemsToTradeGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.CountColumn,
            this.ItemTypeColumn,
            this.CountToAddColumn,
            this.AddButtonColumn,
            this.HidenItemsListColumn,
            this.HidenItemImageColumn,
            this.HidenItemMarketHashName});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.AllSteamItemsToTradeGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.AllSteamItemsToTradeGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllSteamItemsToTradeGridView.EnableHeadersVisualStyles = false;
            this.AllSteamItemsToTradeGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.AllSteamItemsToTradeGridView.Location = new System.Drawing.Point(3, 16);
            this.AllSteamItemsToTradeGridView.Name = "AllSteamItemsToTradeGridView";
            this.AllSteamItemsToTradeGridView.RowHeadersVisible = false;
            this.AllSteamItemsToTradeGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.AllSteamItemsToTradeGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AllSteamItemsToTradeGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AllSteamItemsToTradeGridView.Size = new System.Drawing.Size(560, 295);
            this.AllSteamItemsToTradeGridView.TabIndex = 7;
            this.AllSteamItemsToTradeGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SteamSaleDataGridView_CellClick);
            this.AllSteamItemsToTradeGridView.CurrentCellChanged += new System.EventHandler(this.AllSteamItemsGridView_CurrentCellChanged);
            this.AllSteamItemsToTradeGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.SteamSaleDataGridView_EditingControlShowing);
            // 
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.NameColumn.DataPropertyName = "SaleSteamControl";
            this.NameColumn.FillWeight = 85.5619F;
            this.NameColumn.Frozen = true;
            this.NameColumn.HeaderText = "Item name";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.ToolTipText = "Item name";
            this.NameColumn.Width = 181;
            // 
            // CountColumn
            // 
            this.CountColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CountColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.CountColumn.FillWeight = 35F;
            this.CountColumn.Frozen = true;
            this.CountColumn.HeaderText = "#";
            this.CountColumn.Name = "CountColumn";
            this.CountColumn.ReadOnly = true;
            this.CountColumn.Width = 35;
            // 
            // ItemTypeColumn
            // 
            this.ItemTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ItemTypeColumn.Frozen = true;
            this.ItemTypeColumn.HeaderText = "Type";
            this.ItemTypeColumn.Name = "ItemTypeColumn";
            this.ItemTypeColumn.ReadOnly = true;
            // 
            // CountToAddColumn
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.CountToAddColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.CountToAddColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.CountToAddColumn.FillWeight = 35F;
            this.CountToAddColumn.HeaderText = "Amount for trade";
            this.CountToAddColumn.MaxDropDownItems = 10;
            this.CountToAddColumn.Name = "CountToAddColumn";
            this.CountToAddColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // AddButtonColumn
            // 
            this.AddButtonColumn.FillWeight = 35F;
            this.AddButtonColumn.HeaderText = "Add";
            this.AddButtonColumn.Name = "AddButtonColumn";
            this.AddButtonColumn.Text = "Add";
            this.AddButtonColumn.UseColumnTextForButtonValue = true;
            // 
            // HidenItemsListColumn
            // 
            this.HidenItemsListColumn.HeaderText = "HidenItemsList";
            this.HidenItemsListColumn.Name = "HidenItemsListColumn";
            this.HidenItemsListColumn.Visible = false;
            // 
            // HidenItemImageColumn
            // 
            this.HidenItemImageColumn.HeaderText = "HidenItemImage";
            this.HidenItemImageColumn.Name = "HidenItemImageColumn";
            this.HidenItemImageColumn.Visible = false;
            // 
            // HidenItemMarketHashName
            // 
            this.HidenItemMarketHashName.HeaderText = "HidenItemMarketHashNameColumn";
            this.HidenItemMarketHashName.Name = "HidenItemMarketHashName";
            this.HidenItemMarketHashName.Visible = false;
            // 
            // ItemImageBox
            // 
            this.ItemImageBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.ItemImageBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ItemImageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ItemImageBox.Location = new System.Drawing.Point(3, 15);
            this.ItemImageBox.Name = "ItemImageBox";
            this.ItemImageBox.Size = new System.Drawing.Size(139, 120);
            this.ItemImageBox.TabIndex = 8;
            // 
            // checkInvent
            // 
            this.checkInvent.Name = "checkInvent";
            // 
            // ItemsToTradeGridView
            // 
            this.ItemsToTradeGridView.AllowUserToAddRows = false;
            this.ItemsToTradeGridView.AllowUserToDeleteRows = false;
            this.ItemsToTradeGridView.AllowUserToResizeColumns = false;
            this.ItemsToTradeGridView.AllowUserToResizeRows = false;
            this.ItemsToTradeGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ItemsToTradeGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.ItemsToTradeGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemsToTradeGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.ItemsToTradeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemsToTradeGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AddedToSaleListItemName,
            this.AddedToSaleListItemsCount,
            this.ItemToSalePriceColumn,
            this.HidenMarketHashNameColumn,
            this.AddedToSaleListHidenItemsList,
            this.AveragePrice});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemsToTradeGridView.DefaultCellStyle = dataGridViewCellStyle7;
            this.ItemsToTradeGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.ItemsToTradeGridView.EnableHeadersVisualStyles = false;
            this.ItemsToTradeGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.ItemsToTradeGridView.Location = new System.Drawing.Point(3, 16);
            this.ItemsToTradeGridView.MultiSelect = false;
            this.ItemsToTradeGridView.Name = "ItemsToTradeGridView";
            this.ItemsToTradeGridView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ItemsToTradeGridView.RowHeadersVisible = false;
            this.ItemsToTradeGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ItemsToTradeGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ItemsToTradeGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ItemsToTradeGridView.Size = new System.Drawing.Size(553, 201);
            this.ItemsToTradeGridView.TabIndex = 13;
            this.ItemsToTradeGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemsToTradeGridView_CellClick);
            this.ItemsToTradeGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemsToSaleGridView_CellEndEdit);
            this.ItemsToTradeGridView.CurrentCellChanged += new System.EventHandler(this.ItemsToSaleGridView_CurrentCellChanged);
            // 
            // AddedToSaleListItemName
            // 
            this.AddedToSaleListItemName.FillWeight = 152.3472F;
            this.AddedToSaleListItemName.HeaderText = "Item name";
            this.AddedToSaleListItemName.Name = "AddedToSaleListItemName";
            this.AddedToSaleListItemName.ReadOnly = true;
            // 
            // AddedToSaleListItemsCount
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AddedToSaleListItemsCount.DefaultCellStyle = dataGridViewCellStyle6;
            this.AddedToSaleListItemsCount.FillWeight = 35F;
            this.AddedToSaleListItemsCount.HeaderText = "Amount for sale";
            this.AddedToSaleListItemsCount.Name = "AddedToSaleListItemsCount";
            this.AddedToSaleListItemsCount.ReadOnly = true;
            // 
            // ItemToSalePriceColumn
            // 
            this.ItemToSalePriceColumn.FillWeight = 35F;
            this.ItemToSalePriceColumn.HeaderText = "Unit price";
            this.ItemToSalePriceColumn.Name = "ItemToSalePriceColumn";
            this.ItemToSalePriceColumn.Visible = false;
            // 
            // HidenMarketHashNameColumn
            // 
            this.HidenMarketHashNameColumn.HeaderText = "HidenMarketHashName";
            this.HidenMarketHashNameColumn.Name = "HidenMarketHashNameColumn";
            this.HidenMarketHashNameColumn.ReadOnly = true;
            this.HidenMarketHashNameColumn.Visible = false;
            // 
            // AddedToSaleListHidenItemsList
            // 
            this.AddedToSaleListHidenItemsList.HeaderText = "HidenItemsList";
            this.AddedToSaleListHidenItemsList.Name = "AddedToSaleListHidenItemsList";
            this.AddedToSaleListHidenItemsList.ReadOnly = true;
            this.AddedToSaleListHidenItemsList.Visible = false;
            // 
            // AveragePrice
            // 
            this.AveragePrice.FillWeight = 35F;
            this.AveragePrice.HeaderText = "Average price";
            this.AveragePrice.Name = "AveragePrice";
            this.AveragePrice.ReadOnly = true;
            this.AveragePrice.Visible = false;
            // 
            // ItemDescriptionGroupBox
            // 
            this.ItemDescriptionGroupBox.Controls.Add(this.ItemDescriptionTextBox);
            this.ItemDescriptionGroupBox.Controls.Add(this.ItemNameLable);
            this.ItemDescriptionGroupBox.Controls.Add(this.ItemImageBox);
            this.ItemDescriptionGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.ItemDescriptionGroupBox.Location = new System.Drawing.Point(579, 3);
            this.ItemDescriptionGroupBox.Name = "ItemDescriptionGroupBox";
            this.ItemDescriptionGroupBox.Size = new System.Drawing.Size(296, 224);
            this.ItemDescriptionGroupBox.TabIndex = 14;
            this.ItemDescriptionGroupBox.TabStop = false;
            this.ItemDescriptionGroupBox.Text = "Item description";
            // 
            // ItemDescriptionTextBox
            // 
            this.ItemDescriptionTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.ItemDescriptionTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ItemDescriptionTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.ItemDescriptionTextBox.Location = new System.Drawing.Point(3, 141);
            this.ItemDescriptionTextBox.Name = "ItemDescriptionTextBox";
            this.ItemDescriptionTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.ItemDescriptionTextBox.Size = new System.Drawing.Size(290, 80);
            this.ItemDescriptionTextBox.TabIndex = 12;
            this.ItemDescriptionTextBox.Text = "";
            // 
            // ItemNameLable
            // 
            this.ItemNameLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ItemNameLable.Location = new System.Drawing.Point(150, 15);
            this.ItemNameLable.Name = "ItemNameLable";
            this.ItemNameLable.Size = new System.Drawing.Size(139, 120);
            this.ItemNameLable.TabIndex = 10;
            // 
            // ItemsToSaleGroupBox
            // 
            this.ItemsToSaleGroupBox.Controls.Add(this.ItemsToTradeGridView);
            this.ItemsToSaleGroupBox.Controls.Add(this.DeleteSelectedItemButton);
            this.ItemsToSaleGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.ItemsToSaleGroupBox.Location = new System.Drawing.Point(10, 323);
            this.ItemsToSaleGroupBox.Name = "ItemsToSaleGroupBox";
            this.ItemsToSaleGroupBox.Size = new System.Drawing.Size(559, 267);
            this.ItemsToSaleGroupBox.TabIndex = 15;
            this.ItemsToSaleGroupBox.TabStop = false;
            this.ItemsToSaleGroupBox.Text = "Items for sale";
            // 
            // DeleteSelectedItemButton
            // 
            this.DeleteSelectedItemButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DeleteSelectedItemButton.BackgroundImage")));
            this.DeleteSelectedItemButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.DeleteSelectedItemButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteSelectedItemButton.FlatAppearance.BorderSize = 0;
            this.DeleteSelectedItemButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteSelectedItemButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.DeleteSelectedItemButton.Location = new System.Drawing.Point(504, 221);
            this.DeleteSelectedItemButton.Name = "DeleteSelectedItemButton";
            this.DeleteSelectedItemButton.Size = new System.Drawing.Size(47, 40);
            this.DeleteSelectedItemButton.TabIndex = 22;
            this.AddAllToolTip.SetToolTip(this.DeleteSelectedItemButton, "Remove selected item from to trade list");
            this.DeleteSelectedItemButton.UseVisualStyleBackColor = true;
            this.DeleteSelectedItemButton.Click += new System.EventHandler(this.DeleteAccountButton_Click);
            // 
            // AllSteamItemsGroupBox
            // 
            this.AllSteamItemsGroupBox.Controls.Add(this.AllSteamItemsToTradeGridView);
            this.AllSteamItemsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.AllSteamItemsGroupBox.Location = new System.Drawing.Point(7, 3);
            this.AllSteamItemsGroupBox.Name = "AllSteamItemsGroupBox";
            this.AllSteamItemsGroupBox.Size = new System.Drawing.Size(566, 314);
            this.AllSteamItemsGroupBox.TabIndex = 16;
            this.AllSteamItemsGroupBox.TabStop = false;
            this.AllSteamItemsGroupBox.Text = "All items";
            // 
            // TradeGroupBox
            // 
            this.TradeGroupBox.Controls.Add(this.TradeTokenTextBox);
            this.TradeGroupBox.Controls.Add(this.TradeTokenLable);
            this.TradeGroupBox.Controls.Add(this.TradeParthenIdTextBox);
            this.TradeGroupBox.Controls.Add(this.PartnerIdLable);
            this.TradeGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.TradeGroupBox.Location = new System.Drawing.Point(580, 385);
            this.TradeGroupBox.Name = "TradeGroupBox";
            this.TradeGroupBox.Size = new System.Drawing.Size(296, 107);
            this.TradeGroupBox.TabIndex = 18;
            this.TradeGroupBox.TabStop = false;
            this.TradeGroupBox.Text = "Trade setting ";
            // 
            // TradeTokenTextBox
            // 
            this.TradeTokenTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.TradeTokenTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.TradeTokenTextBox.Location = new System.Drawing.Point(154, 66);
            this.TradeTokenTextBox.Name = "TradeTokenTextBox";
            this.TradeTokenTextBox.Size = new System.Drawing.Size(123, 20);
            this.TradeTokenTextBox.TabIndex = 29;
            this.TradeTokenTextBox.TextChanged += new System.EventHandler(this.TradeTokenTextBox_TextChanged);
            // 
            // TradeTokenLable
            // 
            this.TradeTokenLable.AutoSize = true;
            this.TradeTokenLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.TradeTokenLable.Location = new System.Drawing.Point(12, 67);
            this.TradeTokenLable.Name = "TradeTokenLable";
            this.TradeTokenLable.Size = new System.Drawing.Size(118, 16);
            this.TradeTokenLable.TabIndex = 28;
            this.TradeTokenLable.Text = "Target trade token";
            // 
            // TradeParthenIdTextBox
            // 
            this.TradeParthenIdTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.TradeParthenIdTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.TradeParthenIdTextBox.Location = new System.Drawing.Point(154, 31);
            this.TradeParthenIdTextBox.Name = "TradeParthenIdTextBox";
            this.TradeParthenIdTextBox.Size = new System.Drawing.Size(123, 20);
            this.TradeParthenIdTextBox.TabIndex = 27;
            this.TradeParthenIdTextBox.TextChanged += new System.EventHandler(this.TradeParthenIdTextBox_TextChanged);
            // 
            // PartnerIdLable
            // 
            this.PartnerIdLable.AutoSize = true;
            this.PartnerIdLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.PartnerIdLable.Location = new System.Drawing.Point(12, 32);
            this.PartnerIdLable.Name = "PartnerIdLable";
            this.PartnerIdLable.Size = new System.Drawing.Size(107, 16);
            this.PartnerIdLable.TabIndex = 26;
            this.PartnerIdLable.Text = "Target partner id";
            // 
            // TradeSendGroupBox
            // 
            this.TradeSendGroupBox.Controls.Add(this.SendTradeButton);
            this.TradeSendGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.TradeSendGroupBox.Location = new System.Drawing.Point(580, 498);
            this.TradeSendGroupBox.Name = "TradeSendGroupBox";
            this.TradeSendGroupBox.Size = new System.Drawing.Size(296, 92);
            this.TradeSendGroupBox.TabIndex = 19;
            this.TradeSendGroupBox.TabStop = false;
            this.TradeSendGroupBox.Text = "Trade";
            // 
            // SendTradeButton
            // 
            this.SendTradeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.SendTradeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SendTradeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SendTradeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.SendTradeButton.Location = new System.Drawing.Point(54, 33);
            this.SendTradeButton.Name = "SendTradeButton";
            this.SendTradeButton.Size = new System.Drawing.Size(203, 35);
            this.SendTradeButton.TabIndex = 14;
            this.SendTradeButton.Text = "Send trade offer";
            this.SendTradeButton.UseVisualStyleBackColor = false;
            this.SendTradeButton.Click += new System.EventHandler(this.SendTradeButton_Click);
            // 
            // SplitterPanel
            // 
            this.SplitterPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SplitterPanel.BackgroundImage")));
            this.SplitterPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.SplitterPanel.Location = new System.Drawing.Point(692, 237);
            this.SplitterPanel.Name = "SplitterPanel";
            this.SplitterPanel.Size = new System.Drawing.Size(77, 36);
            this.SplitterPanel.TabIndex = 20;
            // 
            // AddAllToolTip
            // 
            this.AddAllToolTip.AutomaticDelay = 100;
            this.AddAllToolTip.AutoPopDelay = 50000;
            this.AddAllToolTip.InitialDelay = 100;
            this.AddAllToolTip.IsBalloon = true;
            this.AddAllToolTip.ReshowDelay = 20;
            this.AddAllToolTip.UseAnimation = false;
            this.AddAllToolTip.UseFading = false;
            // 
            // LoadInventoryButton
            // 
            this.LoadInventoryButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.LoadInventoryButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoadInventoryButton.FlatAppearance.BorderSize = 0;
            this.LoadInventoryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadInventoryButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.LoadInventoryButton.Image = ((System.Drawing.Image)(resources.GetObject("LoadInventoryButton.Image")));
            this.LoadInventoryButton.Location = new System.Drawing.Point(206, 9);
            this.LoadInventoryButton.Name = "LoadInventoryButton";
            this.LoadInventoryButton.Size = new System.Drawing.Size(76, 65);
            this.LoadInventoryButton.TabIndex = 25;
            this.AddAllToolTip.SetToolTip(this.LoadInventoryButton, "Load selected inventory");
            this.LoadInventoryButton.UseVisualStyleBackColor = true;
            this.LoadInventoryButton.Click += new System.EventHandler(this.LoadInventoryButton_Click);
            // 
            // OpenGameInventoryPageButton
            // 
            this.OpenGameInventoryPageButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OpenGameInventoryPageButton.FlatAppearance.BorderSize = 0;
            this.OpenGameInventoryPageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenGameInventoryPageButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.OpenGameInventoryPageButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenGameInventoryPageButton.Image")));
            this.OpenGameInventoryPageButton.Location = new System.Drawing.Point(828, 242);
            this.OpenGameInventoryPageButton.Name = "OpenGameInventoryPageButton";
            this.OpenGameInventoryPageButton.Size = new System.Drawing.Size(47, 40);
            this.OpenGameInventoryPageButton.TabIndex = 30;
            this.AddAllToolTip.SetToolTip(this.OpenGameInventoryPageButton, "Open selected inventory");
            this.OpenGameInventoryPageButton.UseVisualStyleBackColor = true;
            this.OpenGameInventoryPageButton.Click += new System.EventHandler(this.OpenGameInventoryPageButton_Click);
            // 
            // OpenMarketPageButtonClick
            // 
            this.OpenMarketPageButtonClick.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OpenMarketPageButtonClick.FlatAppearance.BorderSize = 0;
            this.OpenMarketPageButtonClick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenMarketPageButtonClick.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.OpenMarketPageButtonClick.Image = ((System.Drawing.Image)(resources.GetObject("OpenMarketPageButtonClick.Image")));
            this.OpenMarketPageButtonClick.Location = new System.Drawing.Point(778, 242);
            this.OpenMarketPageButtonClick.Name = "OpenMarketPageButtonClick";
            this.OpenMarketPageButtonClick.Size = new System.Drawing.Size(47, 40);
            this.OpenMarketPageButtonClick.TabIndex = 29;
            this.AddAllToolTip.SetToolTip(this.OpenMarketPageButtonClick, "Find selected item on Steam Market");
            this.OpenMarketPageButtonClick.UseVisualStyleBackColor = true;
            this.OpenMarketPageButtonClick.Click += new System.EventHandler(this.OpenMarketPageButtonClick_Click);
            // 
            // RefreshInventoryButton
            // 
            this.RefreshInventoryButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RefreshInventoryButton.FlatAppearance.BorderSize = 0;
            this.RefreshInventoryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RefreshInventoryButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.RefreshInventoryButton.Image = ((System.Drawing.Image)(resources.GetObject("RefreshInventoryButton.Image")));
            this.RefreshInventoryButton.Location = new System.Drawing.Point(635, 240);
            this.RefreshInventoryButton.Name = "RefreshInventoryButton";
            this.RefreshInventoryButton.Size = new System.Drawing.Size(47, 40);
            this.RefreshInventoryButton.TabIndex = 27;
            this.AddAllToolTip.SetToolTip(this.RefreshInventoryButton, "Force prices reload");
            this.RefreshInventoryButton.UseVisualStyleBackColor = true;
            this.RefreshInventoryButton.Click += new System.EventHandler(this.RefreshInventoryButton_Click);
            // 
            // AddAllButton
            // 
            this.AddAllButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddAllButton.FlatAppearance.BorderSize = 0;
            this.AddAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddAllButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.AddAllButton.Image = ((System.Drawing.Image)(resources.GetObject("AddAllButton.Image")));
            this.AddAllButton.Location = new System.Drawing.Point(582, 240);
            this.AddAllButton.Name = "AddAllButton";
            this.AddAllButton.Size = new System.Drawing.Size(47, 40);
            this.AddAllButton.TabIndex = 28;
            this.AddAllToolTip.SetToolTip(this.AddAllButton, "Add all selected items to trade list");
            this.AddAllButton.UseVisualStyleBackColor = true;
            this.AddAllButton.Click += new System.EventHandler(this.AddAllButton_Click);
            // 
            // InventoryGroupBox
            // 
            this.InventoryGroupBox.Controls.Add(this.LoadInventoryButton);
            this.InventoryGroupBox.Controls.Add(this.InventoryContextIdLabel);
            this.InventoryGroupBox.Controls.Add(this.InventoryContextIdComboBox);
            this.InventoryGroupBox.Controls.Add(this.InventoryAppIdLabel);
            this.InventoryGroupBox.Controls.Add(this.InventoryAppIdComboBox);
            this.InventoryGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.InventoryGroupBox.Location = new System.Drawing.Point(580, 296);
            this.InventoryGroupBox.Name = "InventoryGroupBox";
            this.InventoryGroupBox.Size = new System.Drawing.Size(296, 79);
            this.InventoryGroupBox.TabIndex = 18;
            this.InventoryGroupBox.TabStop = false;
            this.InventoryGroupBox.Text = "Inventory settings";
            // 
            // InventoryContextIdLabel
            // 
            this.InventoryContextIdLabel.AutoSize = true;
            this.InventoryContextIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.InventoryContextIdLabel.Location = new System.Drawing.Point(8, 49);
            this.InventoryContextIdLabel.Name = "InventoryContextIdLabel";
            this.InventoryContextIdLabel.Size = new System.Drawing.Size(66, 16);
            this.InventoryContextIdLabel.TabIndex = 3;
            this.InventoryContextIdLabel.Text = "Context id";
            // 
            // InventoryContextIdComboBox
            // 
            this.InventoryContextIdComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.InventoryContextIdComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InventoryContextIdComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.InventoryContextIdComboBox.FormattingEnabled = true;
            this.InventoryContextIdComboBox.Location = new System.Drawing.Point(92, 46);
            this.InventoryContextIdComboBox.Name = "InventoryContextIdComboBox";
            this.InventoryContextIdComboBox.Size = new System.Drawing.Size(97, 21);
            this.InventoryContextIdComboBox.TabIndex = 2;
            this.InventoryContextIdComboBox.TextChanged += new System.EventHandler(this.InventoryContextIdComboBox_TextChanged);
            // 
            // InventoryAppIdLabel
            // 
            this.InventoryAppIdLabel.AutoSize = true;
            this.InventoryAppIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.InventoryAppIdLabel.Location = new System.Drawing.Point(8, 22);
            this.InventoryAppIdLabel.Name = "InventoryAppIdLabel";
            this.InventoryAppIdLabel.Size = new System.Drawing.Size(47, 16);
            this.InventoryAppIdLabel.TabIndex = 1;
            this.InventoryAppIdLabel.Text = "App id";
            // 
            // InventoryAppIdComboBox
            // 
            this.InventoryAppIdComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.InventoryAppIdComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InventoryAppIdComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.InventoryAppIdComboBox.FormattingEnabled = true;
            this.InventoryAppIdComboBox.Items.AddRange(new object[] {
            "STEAM",
            "CS:GO",
            "PUBG",
            "TF"});
            this.InventoryAppIdComboBox.Location = new System.Drawing.Point(92, 19);
            this.InventoryAppIdComboBox.Name = "InventoryAppIdComboBox";
            this.InventoryAppIdComboBox.Size = new System.Drawing.Size(97, 21);
            this.InventoryAppIdComboBox.TabIndex = 0;
            this.InventoryAppIdComboBox.TextChanged += new System.EventHandler(this.InventoryAppIdComboBox_TextChanged);
            // 
            // AccountNameLable
            // 
            this.AccountNameLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AccountNameLable.Location = new System.Drawing.Point(660, 275);
            this.AccountNameLable.Name = "AccountNameLable";
            this.AccountNameLable.Size = new System.Drawing.Size(140, 18);
            this.AccountNameLable.TabIndex = 11;
            this.AccountNameLable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TradeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.Controls.Add(this.OpenGameInventoryPageButton);
            this.Controls.Add(this.InventoryGroupBox);
            this.Controls.Add(this.OpenMarketPageButtonClick);
            this.Controls.Add(this.SplitterPanel);
            this.Controls.Add(this.RefreshInventoryButton);
            this.Controls.Add(this.AddAllButton);
            this.Controls.Add(this.AllSteamItemsGroupBox);
            this.Controls.Add(this.ItemDescriptionGroupBox);
            this.Controls.Add(this.ItemsToSaleGroupBox);
            this.Controls.Add(this.AccountNameLable);
            this.Controls.Add(this.TradeGroupBox);
            this.Controls.Add(this.TradeSendGroupBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.Name = "TradeControl";
            this.Size = new System.Drawing.Size(885, 599);
            this.Load += new System.EventHandler(this.SaleControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AllSteamItemsToTradeGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsToTradeGridView)).EndInit();
            this.ItemDescriptionGroupBox.ResumeLayout(false);
            this.ItemsToSaleGroupBox.ResumeLayout(false);
            this.AllSteamItemsGroupBox.ResumeLayout(false);
            this.TradeGroupBox.ResumeLayout(false);
            this.TradeGroupBox.PerformLayout();
            this.TradeSendGroupBox.ResumeLayout(false);
            this.InventoryGroupBox.ResumeLayout(false);
            this.InventoryGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        public void setSizeListView1(int width, int height) {
            // this.listView1.Size = new System.Drawing.Size(width, height);
            this.AllSteamItemsToTradeGridView.Size = new System.Drawing.Size(width, height);
        }

        public void setSizeListView2(int width, int height) {
        }

        public void setSizeTabControll(int width, int height) {

        }

        public void setPoint(int x, int y) {
        }

        #endregion
        private DataGridView AllSteamItemsToTradeGridView;
        private Panel ItemImageBox;
        private DataGridViewCheckBoxColumn checkInvent;
        private GroupBox ItemDescriptionGroupBox;
        private Label ItemNameLable;
        private GroupBox ItemsToSaleGroupBox;
        private GroupBox AllSteamItemsGroupBox;
        private Panel SplitterPanel;
        private ToolTip AddAllToolTip;
        private Button DeleteSelectedItemButton;
        private GroupBox InventoryGroupBox;
        private Label AccountNameLable;
        private Button LoadInventoryButton;
        private Label InventoryContextIdLabel;
        private ComboBox InventoryContextIdComboBox;
        private Label InventoryAppIdLabel;
        private ComboBox InventoryAppIdComboBox;
        private GroupBox TradeGroupBox;
        private TextBox TradeTokenTextBox;
        private Label TradeTokenLable;
        private TextBox TradeParthenIdTextBox;
        private Label PartnerIdLable;
        private GroupBox TradeSendGroupBox;
        private Button SendTradeButton;
        private DataGridViewTextBoxColumn AddedToSaleListItemName;
        private DataGridViewTextBoxColumn AddedToSaleListItemsCount;
        private DataGridViewTextBoxColumn ItemToSalePriceColumn;
        private DataGridViewTextBoxColumn HidenMarketHashNameColumn;
        private DataGridViewTextBoxColumn AddedToSaleListHidenItemsList;
        private DataGridViewTextBoxColumn AveragePrice;
        private Button OpenGameInventoryPageButton;
        private Button OpenMarketPageButtonClick;
        private Button RefreshInventoryButton;
        private Button AddAllButton;
        public DataGridView ItemsToTradeGridView;
        private RichTextBoxWithNoPaint ItemDescriptionTextBox;
        private DataGridViewTextBoxColumn NameColumn;
        private DataGridViewTextBoxColumn CountColumn;
        private DataGridViewTextBoxColumn ItemTypeColumn;
        private DataGridViewComboBoxColumn CountToAddColumn;
        private DataGridViewButtonColumn AddButtonColumn;
        private DataGridViewTextBoxColumn HidenItemsListColumn;
        private DataGridViewTextBoxColumn HidenItemImageColumn;
        private DataGridViewTextBoxColumn HidenItemMarketHashName;
    }
}