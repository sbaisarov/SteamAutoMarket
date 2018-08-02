using System.Drawing;
using System.Windows.Forms;

namespace autotrade {
    partial class SaleControl {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaleControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle48 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle47 = new System.Windows.Forms.DataGridViewCellStyle();
            this.AllSteamItemsGridView = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountToAddColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.AddButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.AddAllButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.HidenItemsListColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HidenItemImageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HidenItemMarketHashName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemImageBox = new System.Windows.Forms.Panel();
            this.checkInvent = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ItemsToSaleGridView = new System.Windows.Forms.DataGridView();
            this.AddedToSaleListItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddedToSaleListItemsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemToSalePriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HidenMarketHashNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddedToSaleListHidenItemsList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AveragePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDescriptionGroupBox = new System.Windows.Forms.GroupBox();
            this.ItemNameLable = new System.Windows.Forms.Label();
            this.ItemDescriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.ItemsToSaleGroupBox = new System.Windows.Forms.GroupBox();
            this.DeleteSelectedItemButton = new System.Windows.Forms.Button();
            this.DeleteUntradableSteamButton = new System.Windows.Forms.Button();
            this.DeleteUnmarketableSteamButton = new System.Windows.Forms.Button();
            this.StartSteamSellButton = new System.Windows.Forms.Button();
            this.AllSteamItemsGroupBox = new System.Windows.Forms.GroupBox();
            this.PriceSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.CurrentPricePercentNumericUpDown = new autotrade.CustomElements.CustomNumericUpDown();
            this.CurrentPriceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ManualPriceRadioButton = new System.Windows.Forms.RadioButton();
            this.HalfAutoPriceRadioButton = new System.Windows.Forms.RadioButton();
            this.RecomendedPriceRadioButton = new System.Windows.Forms.RadioButton();
            this.TradeGroupBox = new System.Windows.Forms.GroupBox();
            this.TradeTokenTextBox = new System.Windows.Forms.TextBox();
            this.TradeTokenLable = new System.Windows.Forms.Label();
            this.TradeParthenIdTextBox = new System.Windows.Forms.TextBox();
            this.PartnerIdLable = new System.Windows.Forms.Label();
            this.SellGroupBox = new System.Windows.Forms.GroupBox();
            this.TradeSendGroupBox = new System.Windows.Forms.GroupBox();
            this.SendTradeButton = new System.Windows.Forms.Button();
            this.RefreshPanel = new System.Windows.Forms.Panel();
            this.AddAllPanel = new System.Windows.Forms.Panel();
            this.SteamPanel = new System.Windows.Forms.Panel();
            this.OpskinsPanel = new System.Windows.Forms.Panel();
            this.SplitterPanel = new System.Windows.Forms.Panel();
            this.AddAllToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.LoadInventoryButton = new System.Windows.Forms.Button();
            this.InventoryGroupBox = new System.Windows.Forms.GroupBox();
            this.InventoryContextIdLabel = new System.Windows.Forms.Label();
            this.InventoryContextIdComboBox = new System.Windows.Forms.ComboBox();
            this.InventoryAppIdLabel = new System.Windows.Forms.Label();
            this.InventoryAppIdComboBox = new System.Windows.Forms.ComboBox();
            this.AccountNameLable = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AllSteamItemsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsToSaleGridView)).BeginInit();
            this.ItemDescriptionGroupBox.SuspendLayout();
            this.ItemsToSaleGroupBox.SuspendLayout();
            this.AllSteamItemsGroupBox.SuspendLayout();
            this.PriceSettingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPricePercentNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPriceNumericUpDown)).BeginInit();
            this.TradeGroupBox.SuspendLayout();
            this.SellGroupBox.SuspendLayout();
            this.TradeSendGroupBox.SuspendLayout();
            this.InventoryGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AllSteamItemsGridView
            // 
            this.AllSteamItemsGridView.AllowUserToAddRows = false;
            this.AllSteamItemsGridView.AllowUserToDeleteRows = false;
            this.AllSteamItemsGridView.AllowUserToResizeColumns = false;
            this.AllSteamItemsGridView.AllowUserToResizeRows = false;
            this.AllSteamItemsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AllSteamItemsGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.AllSteamItemsGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.AllSteamItemsGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle41.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle41.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle41.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle41.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle41.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AllSteamItemsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle41;
            this.AllSteamItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AllSteamItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.CountColumn,
            this.ItemTypeColumn,
            this.CountToAddColumn,
            this.AddButtonColumn,
            this.AddAllButtonColumn,
            this.HidenItemsListColumn,
            this.HidenItemImageColumn,
            this.HidenItemMarketHashName});
            dataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle44.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle44.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle44.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle44.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle44.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.AllSteamItemsGridView.DefaultCellStyle = dataGridViewCellStyle44;
            this.AllSteamItemsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllSteamItemsGridView.EnableHeadersVisualStyles = false;
            this.AllSteamItemsGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.AllSteamItemsGridView.Location = new System.Drawing.Point(3, 16);
            this.AllSteamItemsGridView.Name = "AllSteamItemsGridView";
            this.AllSteamItemsGridView.RowHeadersVisible = false;
            this.AllSteamItemsGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.AllSteamItemsGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AllSteamItemsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AllSteamItemsGridView.Size = new System.Drawing.Size(560, 295);
            this.AllSteamItemsGridView.TabIndex = 7;
            this.AllSteamItemsGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SteamSaleDataGridView_CellClick);
            this.AllSteamItemsGridView.CurrentCellChanged += new System.EventHandler(this.AllSteamItemsGridView_CurrentCellChanged);
            this.AllSteamItemsGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.SteamSaleDataGridView_EditingControlShowing);
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
            dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CountColumn.DefaultCellStyle = dataGridViewCellStyle42;
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
            dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.CountToAddColumn.DefaultCellStyle = dataGridViewCellStyle43;
            this.CountToAddColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.CountToAddColumn.FillWeight = 35F;
            this.CountToAddColumn.HeaderText = "Amount for sale";
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
            // AddAllButtonColumn
            // 
            this.AddAllButtonColumn.FillWeight = 35F;
            this.AddAllButtonColumn.HeaderText = "Add all";
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
            this.ItemImageBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ItemImageBox.BackgroundImage")));
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
            // ItemsToSaleGridView
            // 
            this.ItemsToSaleGridView.AllowUserToAddRows = false;
            this.ItemsToSaleGridView.AllowUserToDeleteRows = false;
            this.ItemsToSaleGridView.AllowUserToResizeColumns = false;
            this.ItemsToSaleGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle45.BackColor = System.Drawing.SystemColors.Menu;
            this.ItemsToSaleGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle45;
            this.ItemsToSaleGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ItemsToSaleGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.ItemsToSaleGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle46.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle46.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle46.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle46.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle46.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemsToSaleGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle46;
            this.ItemsToSaleGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemsToSaleGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AddedToSaleListItemName,
            this.AddedToSaleListItemsCount,
            this.ItemToSalePriceColumn,
            this.HidenMarketHashNameColumn,
            this.AddedToSaleListHidenItemsList,
            this.AveragePrice});
            dataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle48.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle48.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle48.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle48.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemsToSaleGridView.DefaultCellStyle = dataGridViewCellStyle48;
            this.ItemsToSaleGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.ItemsToSaleGridView.EnableHeadersVisualStyles = false;
            this.ItemsToSaleGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.ItemsToSaleGridView.Location = new System.Drawing.Point(3, 16);
            this.ItemsToSaleGridView.MultiSelect = false;
            this.ItemsToSaleGridView.Name = "ItemsToSaleGridView";
            this.ItemsToSaleGridView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ItemsToSaleGridView.RowHeadersVisible = false;
            this.ItemsToSaleGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ItemsToSaleGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ItemsToSaleGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ItemsToSaleGridView.Size = new System.Drawing.Size(553, 201);
            this.ItemsToSaleGridView.TabIndex = 13;
            this.ItemsToSaleGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemsToSaleGridView_CellClick);
            this.ItemsToSaleGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemsToSaleGridView_CellEndEdit);
            this.ItemsToSaleGridView.CurrentCellChanged += new System.EventHandler(this.ItemsToSaleGridView_CurrentCellChanged);
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
            dataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AddedToSaleListItemsCount.DefaultCellStyle = dataGridViewCellStyle47;
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
            // 
            // ItemDescriptionGroupBox
            // 
            this.ItemDescriptionGroupBox.Controls.Add(this.ItemNameLable);
            this.ItemDescriptionGroupBox.Controls.Add(this.ItemDescriptionTextBox);
            this.ItemDescriptionGroupBox.Controls.Add(this.ItemImageBox);
            this.ItemDescriptionGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.ItemDescriptionGroupBox.Location = new System.Drawing.Point(579, 3);
            this.ItemDescriptionGroupBox.Name = "ItemDescriptionGroupBox";
            this.ItemDescriptionGroupBox.Size = new System.Drawing.Size(296, 224);
            this.ItemDescriptionGroupBox.TabIndex = 14;
            this.ItemDescriptionGroupBox.TabStop = false;
            this.ItemDescriptionGroupBox.Text = "Item description";
            // 
            // ItemNameLable
            // 
            this.ItemNameLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ItemNameLable.Location = new System.Drawing.Point(150, 15);
            this.ItemNameLable.Name = "ItemNameLable";
            this.ItemNameLable.Size = new System.Drawing.Size(139, 120);
            this.ItemNameLable.TabIndex = 10;
            // 
            // ItemDescriptionTextBox
            // 
            this.ItemDescriptionTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.ItemDescriptionTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.ItemDescriptionTextBox.DetectUrls = false;
            this.ItemDescriptionTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ItemDescriptionTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.ItemDescriptionTextBox.Location = new System.Drawing.Point(3, 141);
            this.ItemDescriptionTextBox.Name = "ItemDescriptionTextBox";
            this.ItemDescriptionTextBox.ReadOnly = true;
            this.ItemDescriptionTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.ItemDescriptionTextBox.Size = new System.Drawing.Size(290, 80);
            this.ItemDescriptionTextBox.TabIndex = 9;
            this.ItemDescriptionTextBox.Text = "";
            // 
            // ItemsToSaleGroupBox
            // 
            this.ItemsToSaleGroupBox.Controls.Add(this.ItemsToSaleGridView);
            this.ItemsToSaleGroupBox.Controls.Add(this.DeleteSelectedItemButton);
            this.ItemsToSaleGroupBox.Controls.Add(this.DeleteUntradableSteamButton);
            this.ItemsToSaleGroupBox.Controls.Add(this.DeleteUnmarketableSteamButton);
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
            this.DeleteSelectedItemButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteSelectedItemButton.FlatAppearance.BorderSize = 0;
            this.DeleteSelectedItemButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteSelectedItemButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.DeleteSelectedItemButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteSelectedItemButton.Image")));
            this.DeleteSelectedItemButton.Location = new System.Drawing.Point(504, 221);
            this.DeleteSelectedItemButton.Name = "DeleteSelectedItemButton";
            this.DeleteSelectedItemButton.Size = new System.Drawing.Size(47, 40);
            this.DeleteSelectedItemButton.TabIndex = 22;
            this.AddAllToolTip.SetToolTip(this.DeleteSelectedItemButton, "Remove selected item from sale list");
            this.DeleteSelectedItemButton.UseVisualStyleBackColor = true;
            this.DeleteSelectedItemButton.Click += new System.EventHandler(this.DeleteAccountButton_Click);
            // 
            // DeleteUntradableSteamButton
            // 
            this.DeleteUntradableSteamButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteUntradableSteamButton.FlatAppearance.BorderSize = 0;
            this.DeleteUntradableSteamButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteUntradableSteamButton.ForeColor = System.Drawing.Color.Silver;
            this.DeleteUntradableSteamButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteUntradableSteamButton.Image")));
            this.DeleteUntradableSteamButton.Location = new System.Drawing.Point(385, 221);
            this.DeleteUntradableSteamButton.Name = "DeleteUntradableSteamButton";
            this.DeleteUntradableSteamButton.Size = new System.Drawing.Size(47, 40);
            this.DeleteUntradableSteamButton.TabIndex = 23;
            this.AddAllToolTip.SetToolTip(this.DeleteUntradableSteamButton, "Remove from list items, that can not be traded");
            this.DeleteUntradableSteamButton.UseVisualStyleBackColor = true;
            this.DeleteUntradableSteamButton.Click += new System.EventHandler(this.DeleteUntradableSteamButton_Click);
            // 
            // DeleteUnmarketableSteamButton
            // 
            this.DeleteUnmarketableSteamButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteUnmarketableSteamButton.FlatAppearance.BorderSize = 0;
            this.DeleteUnmarketableSteamButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteUnmarketableSteamButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.DeleteUnmarketableSteamButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteUnmarketableSteamButton.Image")));
            this.DeleteUnmarketableSteamButton.Location = new System.Drawing.Point(446, 221);
            this.DeleteUnmarketableSteamButton.Name = "DeleteUnmarketableSteamButton";
            this.DeleteUnmarketableSteamButton.Size = new System.Drawing.Size(47, 40);
            this.DeleteUnmarketableSteamButton.TabIndex = 24;
            this.AddAllToolTip.SetToolTip(this.DeleteUnmarketableSteamButton, "Remove from list items, that can not be sold on STEAM Market");
            this.DeleteUnmarketableSteamButton.UseVisualStyleBackColor = true;
            this.DeleteUnmarketableSteamButton.Click += new System.EventHandler(this.DeleteUnmarketableSteamButton_Click);
            // 
            // StartSteamSellButton
            // 
            this.StartSteamSellButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.StartSteamSellButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StartSteamSellButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StartSteamSellButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.StartSteamSellButton.Location = new System.Drawing.Point(54, 33);
            this.StartSteamSellButton.Name = "StartSteamSellButton";
            this.StartSteamSellButton.Size = new System.Drawing.Size(203, 35);
            this.StartSteamSellButton.TabIndex = 14;
            this.StartSteamSellButton.Text = "Sell on STEAM Market";
            this.StartSteamSellButton.UseVisualStyleBackColor = false;
            this.StartSteamSellButton.Click += new System.EventHandler(this.StartSteamSellButton_Click);
            // 
            // AllSteamItemsGroupBox
            // 
            this.AllSteamItemsGroupBox.Controls.Add(this.AllSteamItemsGridView);
            this.AllSteamItemsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.AllSteamItemsGroupBox.Location = new System.Drawing.Point(7, 3);
            this.AllSteamItemsGroupBox.Name = "AllSteamItemsGroupBox";
            this.AllSteamItemsGroupBox.Size = new System.Drawing.Size(566, 314);
            this.AllSteamItemsGroupBox.TabIndex = 16;
            this.AllSteamItemsGroupBox.TabStop = false;
            this.AllSteamItemsGroupBox.Text = "All items";
            // 
            // PriceSettingsGroupBox
            // 
            this.PriceSettingsGroupBox.Controls.Add(this.CurrentPricePercentNumericUpDown);
            this.PriceSettingsGroupBox.Controls.Add(this.CurrentPriceNumericUpDown);
            this.PriceSettingsGroupBox.Controls.Add(this.ManualPriceRadioButton);
            this.PriceSettingsGroupBox.Controls.Add(this.HalfAutoPriceRadioButton);
            this.PriceSettingsGroupBox.Controls.Add(this.RecomendedPriceRadioButton);
            this.PriceSettingsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.PriceSettingsGroupBox.Location = new System.Drawing.Point(580, 384);
            this.PriceSettingsGroupBox.Name = "PriceSettingsGroupBox";
            this.PriceSettingsGroupBox.Size = new System.Drawing.Size(296, 107);
            this.PriceSettingsGroupBox.TabIndex = 17;
            this.PriceSettingsGroupBox.TabStop = false;
            this.PriceSettingsGroupBox.Text = "The price formation setting ";
            // 
            // CurrentPricePercentNumericUpDown
            // 
            this.CurrentPricePercentNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.CurrentPricePercentNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrentPricePercentNumericUpDown.DecimalPlaces = 2;
            this.CurrentPricePercentNumericUpDown.Enabled = false;
            this.CurrentPricePercentNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CurrentPricePercentNumericUpDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.CurrentPricePercentNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.CurrentPricePercentNumericUpDown.Location = new System.Drawing.Point(213, 70);
            this.CurrentPricePercentNumericUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.CurrentPricePercentNumericUpDown.Minimum = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147483648});
            this.CurrentPricePercentNumericUpDown.Name = "CurrentPricePercentNumericUpDown";
            this.CurrentPricePercentNumericUpDown.Size = new System.Drawing.Size(76, 22);
            this.CurrentPricePercentNumericUpDown.TabIndex = 4;
            this.CurrentPricePercentNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AddAllToolTip.SetToolTip(this.CurrentPricePercentNumericUpDown, "Only one value type can be chosen");
            this.CurrentPricePercentNumericUpDown.TrailingSign = "%";
            this.CurrentPricePercentNumericUpDown.ValueChanged += new System.EventHandler(this.CurrentPricePercentNumericUpDown_ValueChanged);
            // 
            // CurrentPriceNumericUpDown
            // 
            this.CurrentPriceNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.CurrentPriceNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrentPriceNumericUpDown.DecimalPlaces = 2;
            this.CurrentPriceNumericUpDown.Enabled = false;
            this.CurrentPriceNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CurrentPriceNumericUpDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.CurrentPriceNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.CurrentPriceNumericUpDown.Location = new System.Drawing.Point(131, 70);
            this.CurrentPriceNumericUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.CurrentPriceNumericUpDown.Minimum = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147483648});
            this.CurrentPriceNumericUpDown.Name = "CurrentPriceNumericUpDown";
            this.CurrentPriceNumericUpDown.Size = new System.Drawing.Size(76, 22);
            this.CurrentPriceNumericUpDown.TabIndex = 3;
            this.CurrentPriceNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AddAllToolTip.SetToolTip(this.CurrentPriceNumericUpDown, "Only one value type can be chosen");
            this.CurrentPriceNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147352576});
            this.CurrentPriceNumericUpDown.ValueChanged += new System.EventHandler(this.CurrentPriceNumericUpDown_ValueChanged);
            // 
            // ManualPriceRadioButton
            // 
            this.ManualPriceRadioButton.AutoSize = true;
            this.ManualPriceRadioButton.Checked = true;
            this.ManualPriceRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManualPriceRadioButton.Location = new System.Drawing.Point(12, 44);
            this.ManualPriceRadioButton.Name = "ManualPriceRadioButton";
            this.ManualPriceRadioButton.Size = new System.Drawing.Size(80, 20);
            this.ManualPriceRadioButton.TabIndex = 2;
            this.ManualPriceRadioButton.TabStop = true;
            this.ManualPriceRadioButton.Text = "Manually";
            this.ManualPriceRadioButton.UseVisualStyleBackColor = true;
            // 
            // HalfAutoPriceRadioButton
            // 
            this.HalfAutoPriceRadioButton.AutoSize = true;
            this.HalfAutoPriceRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HalfAutoPriceRadioButton.Location = new System.Drawing.Point(12, 70);
            this.HalfAutoPriceRadioButton.Name = "HalfAutoPriceRadioButton";
            this.HalfAutoPriceRadioButton.Size = new System.Drawing.Size(101, 20);
            this.HalfAutoPriceRadioButton.TabIndex = 1;
            this.HalfAutoPriceRadioButton.Text = "Current price";
            this.AddAllToolTip.SetToolTip(this.HalfAutoPriceRadioButton, "The value to add to the calculated price (minimum and maximum are respected)");
            this.HalfAutoPriceRadioButton.UseVisualStyleBackColor = true;
            this.HalfAutoPriceRadioButton.CheckedChanged += new System.EventHandler(this.HalfAutoPriceRadioButton_CheckedChanged);
            // 
            // RecomendedPriceRadioButton
            // 
            this.RecomendedPriceRadioButton.AutoSize = true;
            this.RecomendedPriceRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecomendedPriceRadioButton.Location = new System.Drawing.Point(12, 19);
            this.RecomendedPriceRadioButton.Name = "RecomendedPriceRadioButton";
            this.RecomendedPriceRadioButton.Size = new System.Drawing.Size(153, 20);
            this.RecomendedPriceRadioButton.TabIndex = 0;
            this.RecomendedPriceRadioButton.Text = "Recommended price";
            this.RecomendedPriceRadioButton.UseVisualStyleBackColor = true;
            // 
            // TradeGroupBox
            // 
            this.TradeGroupBox.Controls.Add(this.TradeTokenTextBox);
            this.TradeGroupBox.Controls.Add(this.TradeTokenLable);
            this.TradeGroupBox.Controls.Add(this.TradeParthenIdTextBox);
            this.TradeGroupBox.Controls.Add(this.PartnerIdLable);
            this.TradeGroupBox.Location = new System.Drawing.Point(580, 385);
            this.TradeGroupBox.Name = "TradeGroupBox";
            this.TradeGroupBox.Size = new System.Drawing.Size(296, 107);
            this.TradeGroupBox.TabIndex = 18;
            this.TradeGroupBox.TabStop = false;
            this.TradeGroupBox.Text = "Trade setting ";
            // 
            // TradeTokenTextBox
            // 
            this.TradeTokenTextBox.Location = new System.Drawing.Point(154, 66);
            this.TradeTokenTextBox.Name = "TradeTokenTextBox";
            this.TradeTokenTextBox.Size = new System.Drawing.Size(123, 20);
            this.TradeTokenTextBox.TabIndex = 29;
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
            this.TradeParthenIdTextBox.Location = new System.Drawing.Point(154, 31);
            this.TradeParthenIdTextBox.Name = "TradeParthenIdTextBox";
            this.TradeParthenIdTextBox.Size = new System.Drawing.Size(123, 20);
            this.TradeParthenIdTextBox.TabIndex = 27;
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
            // SellGroupBox
            // 
            this.SellGroupBox.Controls.Add(this.StartSteamSellButton);
            this.SellGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.SellGroupBox.Location = new System.Drawing.Point(580, 498);
            this.SellGroupBox.Name = "SellGroupBox";
            this.SellGroupBox.Size = new System.Drawing.Size(296, 92);
            this.SellGroupBox.TabIndex = 18;
            this.SellGroupBox.TabStop = false;
            this.SellGroupBox.Text = "Sell";
            // 
            // TradeSendGroupBox
            // 
            this.TradeSendGroupBox.Controls.Add(this.SendTradeButton);
            this.TradeSendGroupBox.Location = new System.Drawing.Point(580, 498);
            this.TradeSendGroupBox.Name = "TradeSendGroupBox";
            this.TradeSendGroupBox.Size = new System.Drawing.Size(296, 92);
            this.TradeSendGroupBox.TabIndex = 19;
            this.TradeSendGroupBox.TabStop = false;
            this.TradeSendGroupBox.Text = "Sell";
            // 
            // SendTradeButton
            // 
            this.SendTradeButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.SendTradeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SendTradeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SendTradeButton.Location = new System.Drawing.Point(54, 33);
            this.SendTradeButton.Name = "SendTradeButton";
            this.SendTradeButton.Size = new System.Drawing.Size(203, 35);
            this.SendTradeButton.TabIndex = 14;
            this.SendTradeButton.Text = "Send trade offer";
            this.SendTradeButton.UseVisualStyleBackColor = false;
            this.SendTradeButton.Click += new System.EventHandler(this.SendTradeButton_Click);
            // 
            // RefreshPanel
            // 
            this.RefreshPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RefreshPanel.BackgroundImage")));
            this.RefreshPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.RefreshPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RefreshPanel.Location = new System.Drawing.Point(632, 242);
            this.RefreshPanel.Name = "RefreshPanel";
            this.RefreshPanel.Size = new System.Drawing.Size(44, 38);
            this.RefreshPanel.TabIndex = 19;
            this.AddAllToolTip.SetToolTip(this.RefreshPanel, "Reload current inventory");
            this.RefreshPanel.Click += new System.EventHandler(this.RefreshPanel_Click);
            this.RefreshPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RefreshPanel_MouseDown);
            this.RefreshPanel.MouseEnter += new System.EventHandler(this.RefreshPanel_MouseEnter);
            this.RefreshPanel.MouseLeave += new System.EventHandler(this.RefreshPanel_MouseLeave);
            this.RefreshPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RefreshPanel_MouseUp);
            // 
            // AddAllPanel
            // 
            this.AddAllPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AddAllPanel.BackgroundImage")));
            this.AddAllPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.AddAllPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddAllPanel.Location = new System.Drawing.Point(579, 242);
            this.AddAllPanel.Name = "AddAllPanel";
            this.AddAllPanel.Size = new System.Drawing.Size(44, 38);
            this.AddAllPanel.TabIndex = 20;
            this.AddAllToolTip.SetToolTip(this.AddAllPanel, "Add selected items for sale");
            this.AddAllPanel.Click += new System.EventHandler(this.AddAllPanel_Click);
            this.AddAllPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AddAllPanel_MouseDown);
            this.AddAllPanel.MouseEnter += new System.EventHandler(this.AddAllPanel_MouseEnter);
            this.AddAllPanel.MouseLeave += new System.EventHandler(this.AddAllPanel_MouseLeave);
            this.AddAllPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AddAllPanel_MouseUp);
            // 
            // SteamPanel
            // 
            this.SteamPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SteamPanel.BackgroundImage")));
            this.SteamPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.SteamPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SteamPanel.Location = new System.Drawing.Point(777, 242);
            this.SteamPanel.Name = "SteamPanel";
            this.SteamPanel.Size = new System.Drawing.Size(44, 38);
            this.SteamPanel.TabIndex = 20;
            this.AddAllToolTip.SetToolTip(this.SteamPanel, "Find selected item on STEAM Market");
            this.SteamPanel.Click += new System.EventHandler(this.SteamPanel_Click);
            this.SteamPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SteamPanel_MouseDown);
            this.SteamPanel.MouseEnter += new System.EventHandler(this.SteamPanel_MouseEnter);
            this.SteamPanel.MouseLeave += new System.EventHandler(this.SteamPanel_MouseLeave);
            this.SteamPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SteamPanel_MouseUp);
            // 
            // OpskinsPanel
            // 
            this.OpskinsPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OpskinsPanel.BackgroundImage")));
            this.OpskinsPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.OpskinsPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OpskinsPanel.Location = new System.Drawing.Point(831, 242);
            this.OpskinsPanel.Name = "OpskinsPanel";
            this.OpskinsPanel.Size = new System.Drawing.Size(44, 38);
            this.OpskinsPanel.TabIndex = 21;
            this.AddAllToolTip.SetToolTip(this.OpskinsPanel, "Find selected item on TODO!");
            this.OpskinsPanel.Click += new System.EventHandler(this.OpskinsPanel_Click);
            this.OpskinsPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OpskinsPanel_MouseDown);
            this.OpskinsPanel.MouseEnter += new System.EventHandler(this.OpskinsPanel_MouseEnter);
            this.OpskinsPanel.MouseLeave += new System.EventHandler(this.OpskinsPanel_MouseLeave);
            this.OpskinsPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OpskinsPanel_MouseUp);
            // 
            // SplitterPanel
            // 
            this.SplitterPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SplitterPanel.BackgroundImage")));
            this.SplitterPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.SplitterPanel.Location = new System.Drawing.Point(688, 237);
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
            this.InventoryAppIdComboBox.SelectedIndexChanged += new System.EventHandler(this.InventoryAppIdComboBox_SelectedIndexChanged);
            // 
            // AccountNameLable
            // 
            this.AccountNameLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AccountNameLable.Location = new System.Drawing.Point(659, 275);
            this.AccountNameLable.Name = "AccountNameLable";
            this.AccountNameLable.Size = new System.Drawing.Size(140, 18);
            this.AccountNameLable.TabIndex = 11;
            this.AccountNameLable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SaleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.Controls.Add(this.InventoryGroupBox);
            this.Controls.Add(this.SplitterPanel);
            this.Controls.Add(this.OpskinsPanel);
            this.Controls.Add(this.SteamPanel);
            this.Controls.Add(this.AddAllPanel);
            this.Controls.Add(this.RefreshPanel);
            this.Controls.Add(this.SellGroupBox);
            this.Controls.Add(this.PriceSettingsGroupBox);
            this.Controls.Add(this.AllSteamItemsGroupBox);
            this.Controls.Add(this.ItemDescriptionGroupBox);
            this.Controls.Add(this.ItemsToSaleGroupBox);
            this.Controls.Add(this.TradeGroupBox);
            this.Controls.Add(this.TradeSendGroupBox);
            this.Controls.Add(this.AccountNameLable);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.Name = "SaleControl";
            this.Size = new System.Drawing.Size(885, 599);
            this.Load += new System.EventHandler(this.SaleControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AllSteamItemsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsToSaleGridView)).EndInit();
            this.ItemDescriptionGroupBox.ResumeLayout(false);
            this.ItemsToSaleGroupBox.ResumeLayout(false);
            this.AllSteamItemsGroupBox.ResumeLayout(false);
            this.PriceSettingsGroupBox.ResumeLayout(false);
            this.PriceSettingsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPricePercentNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPriceNumericUpDown)).EndInit();
            this.TradeGroupBox.ResumeLayout(false);
            this.TradeGroupBox.PerformLayout();
            this.SellGroupBox.ResumeLayout(false);
            this.TradeSendGroupBox.ResumeLayout(false);
            this.InventoryGroupBox.ResumeLayout(false);
            this.InventoryGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        public void setSizeListView1(int width, int height) {
            // this.listView1.Size = new System.Drawing.Size(width, height);
            this.AllSteamItemsGridView.Size = new System.Drawing.Size(width, height);
        }

        public void setSizeListView2(int width, int height) {
        }

        public void setSizeTabControll(int width, int height) {

        }

        public void setPoint(int x, int y) {
        }

        #endregion
        private DataGridView AllSteamItemsGridView;
        private Panel ItemImageBox;
        private DataGridViewCheckBoxColumn checkInvent;
        private DataGridView ItemsToSaleGridView;
        private GroupBox ItemDescriptionGroupBox;
        private RichTextBox ItemDescriptionTextBox;
        private Label ItemNameLable;
        private GroupBox ItemsToSaleGroupBox;
        private Button StartSteamSellButton;
        private GroupBox AllSteamItemsGroupBox;
        private GroupBox PriceSettingsGroupBox;
        private RadioButton ManualPriceRadioButton;
        private RadioButton HalfAutoPriceRadioButton;
        private RadioButton RecomendedPriceRadioButton;
        private GroupBox SellGroupBox;
        private Panel RefreshPanel;
        private Panel AddAllPanel;
        private Panel SteamPanel;
        private Panel OpskinsPanel;
        private Panel SplitterPanel;
        private ToolTip AddAllToolTip;
        private Button DeleteSelectedItemButton;
        private NumericUpDown CurrentPriceNumericUpDown;
        private Button DeleteUntradableSteamButton;
        private Button DeleteUnmarketableSteamButton;
        private GroupBox InventoryGroupBox;
        private Label AccountNameLable;
        private Button LoadInventoryButton;
        private Label InventoryContextIdLabel;
        private ComboBox InventoryContextIdComboBox;
        private Label InventoryAppIdLabel;
        private ComboBox InventoryAppIdComboBox;
        private DataGridViewTextBoxColumn NameColumn;
        private DataGridViewTextBoxColumn CountColumn;
        private DataGridViewTextBoxColumn ItemTypeColumn;
        private DataGridViewComboBoxColumn CountToAddColumn;
        private DataGridViewButtonColumn AddButtonColumn;
        private DataGridViewButtonColumn AddAllButtonColumn;
        private DataGridViewTextBoxColumn HidenItemsListColumn;
        private DataGridViewTextBoxColumn HidenItemImageColumn;
        private DataGridViewTextBoxColumn HidenItemMarketHashName;
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
        private CustomElements.CustomNumericUpDown CurrentPricePercentNumericUpDown;
    }
}