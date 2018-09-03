using System.Windows.Forms;
using autotrade.CustomElements.Elements;

namespace autotrade.CustomElements.Controls.Market
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle49 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle47 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle48 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle50 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle54 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle51 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle52 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle53 = new System.Windows.Forms.DataGridViewCellStyle();
            this.AllSteamItemsGridView = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentPriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AveragePriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountToAddColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.AddButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.HidenItemsListColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HidenItemImageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HidenItemMarketHashName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemImageBox = new System.Windows.Forms.Panel();
            this.checkInvent = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ItemDescriptionGroupBox = new System.Windows.Forms.GroupBox();
            this.ItemNameLable = new System.Windows.Forms.Label();
            this.ItemsToSaleGroupBox = new System.Windows.Forms.GroupBox();
            this.ItemsForSaleUpdateOneButtonClick = new System.Windows.Forms.Button();
            this.ForcePricesReloadButton = new System.Windows.Forms.Button();
            this.ItemsToSaleGridView = new System.Windows.Forms.DataGridView();
            this.DeleteSelectedItemButton = new System.Windows.Forms.Button();
            this.AllSteamItemsGroupBox = new System.Windows.Forms.GroupBox();
            this.SplitterPanel = new System.Windows.Forms.Panel();
            this.AddAllToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.LoadInventoryButton = new System.Windows.Forms.Button();
            this.CurrentMinusPriceRadioButton = new System.Windows.Forms.RadioButton();
            this.CurrentPriceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.AddAllButton = new System.Windows.Forms.Button();
            this.RefreshPricesButton = new System.Windows.Forms.Button();
            this.OpenMarketPageButtonClick = new System.Windows.Forms.Button();
            this.AllItemsUpdateOneButtonClick = new System.Windows.Forms.Button();
            this.AveragePriceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.AveregeMinusPriceRadioButton = new System.Windows.Forms.RadioButton();
            this.ManualPriceRadioButton = new System.Windows.Forms.RadioButton();
            this.RecomendedPriceRadioButton = new System.Windows.Forms.RadioButton();
            this.InventoryGroupBox = new System.Windows.Forms.GroupBox();
            this.InventoryContextIdLabel = new System.Windows.Forms.Label();
            this.InventoryContextIdComboBox = new System.Windows.Forms.ComboBox();
            this.InventoryAppIdLabel = new System.Windows.Forms.Label();
            this.InventoryAppIdComboBox = new System.Windows.Forms.ComboBox();
            this.AccountNameLable = new System.Windows.Forms.Label();
            this.StartSteamSellButton = new System.Windows.Forms.Button();
            this.SellGroupBox = new System.Windows.Forms.GroupBox();
            this.PriceSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.AddedToSaleListItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddedToSaleListItemsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemToSalePriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AveragePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HidenMarketHashNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddedToSaleListHidenItemsList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDescriptionTextBox = new RichTextBoxWithNoPaint();
            this.AveragePricePercentNumericUpDown = new CustomNumericUpDown();
            this.CurrentPricePercentNumericUpDown = new CustomNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.AllSteamItemsGridView)).BeginInit();
            this.ItemDescriptionGroupBox.SuspendLayout();
            this.ItemsToSaleGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsToSaleGridView)).BeginInit();
            this.AllSteamItemsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPriceNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AveragePriceNumericUpDown)).BeginInit();
            this.InventoryGroupBox.SuspendLayout();
            this.SellGroupBox.SuspendLayout();
            this.PriceSettingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AveragePricePercentNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPricePercentNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // AllSteamItemsGridView
            // 
            this.AllSteamItemsGridView.AllowUserToAddRows = false;
            this.AllSteamItemsGridView.AllowUserToDeleteRows = false;
            this.AllSteamItemsGridView.AllowUserToResizeRows = false;
            this.AllSteamItemsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AllSteamItemsGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.AllSteamItemsGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.AllSteamItemsGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle46.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle46.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle46.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle46.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle46.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AllSteamItemsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle46;
            this.AllSteamItemsGridView.ColumnHeadersHeight = 30;
            this.AllSteamItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.AllSteamItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.CountColumn,
            this.ItemTypeColumn,
            this.CurrentPriceColumn,
            this.AveragePriceColumn,
            this.CountToAddColumn,
            this.AddButtonColumn,
            this.HidenItemsListColumn,
            this.HidenItemImageColumn,
            this.HidenItemMarketHashName});
            dataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle49.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle49.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle49.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle49.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle49.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.AllSteamItemsGridView.DefaultCellStyle = dataGridViewCellStyle49;
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
            this.NameColumn.FillWeight = 200F;
            this.NameColumn.Frozen = true;
            this.NameColumn.HeaderText = "Item name";
            this.NameColumn.MinimumWidth = 200;
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.NameColumn.Width = 200;
            // 
            // CountColumn
            // 
            this.CountColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CountColumn.DefaultCellStyle = dataGridViewCellStyle47;
            this.CountColumn.FillWeight = 40F;
            this.CountColumn.HeaderText = "#";
            this.CountColumn.Name = "CountColumn";
            this.CountColumn.ReadOnly = true;
            this.CountColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CountColumn.Width = 40;
            // 
            // ItemTypeColumn
            // 
            this.ItemTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ItemTypeColumn.FillWeight = 120F;
            this.ItemTypeColumn.HeaderText = "Type";
            this.ItemTypeColumn.Name = "ItemTypeColumn";
            this.ItemTypeColumn.ReadOnly = true;
            this.ItemTypeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemTypeColumn.Width = 120;
            // 
            // CurrentPriceColumn
            // 
            this.CurrentPriceColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CurrentPriceColumn.FillWeight = 20F;
            this.CurrentPriceColumn.HeaderText = "Cur";
            this.CurrentPriceColumn.MinimumWidth = 20;
            this.CurrentPriceColumn.Name = "CurrentPriceColumn";
            this.CurrentPriceColumn.ReadOnly = true;
            this.CurrentPriceColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CurrentPriceColumn.ToolTipText = "Current price";
            this.CurrentPriceColumn.Width = 45;
            // 
            // AveragePriceColumn
            // 
            this.AveragePriceColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AveragePriceColumn.FillWeight = 20F;
            this.AveragePriceColumn.HeaderText = "Avr";
            this.AveragePriceColumn.MinimumWidth = 20;
            this.AveragePriceColumn.Name = "AveragePriceColumn";
            this.AveragePriceColumn.ReadOnly = true;
            this.AveragePriceColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.AveragePriceColumn.ToolTipText = "Average price";
            this.AveragePriceColumn.Width = 45;
            // 
            // CountToAddColumn
            // 
            dataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.CountToAddColumn.DefaultCellStyle = dataGridViewCellStyle48;
            this.CountToAddColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.CountToAddColumn.FillWeight = 20F;
            this.CountToAddColumn.HeaderText = "Amount to add";
            this.CountToAddColumn.MaxDropDownItems = 10;
            this.CountToAddColumn.MinimumWidth = 20;
            this.CountToAddColumn.Name = "CountToAddColumn";
            this.CountToAddColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // AddButtonColumn
            // 
            this.AddButtonColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AddButtonColumn.FillWeight = 60F;
            this.AddButtonColumn.HeaderText = "Add";
            this.AddButtonColumn.Name = "AddButtonColumn";
            this.AddButtonColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.AddButtonColumn.Text = "▼";
            this.AddButtonColumn.UseColumnTextForButtonValue = true;
            // 
            // HidenItemsListColumn
            // 
            this.HidenItemsListColumn.HeaderText = "HidenItemsList";
            this.HidenItemsListColumn.Name = "HidenItemsListColumn";
            this.HidenItemsListColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.HidenItemsListColumn.Visible = false;
            // 
            // HidenItemImageColumn
            // 
            this.HidenItemImageColumn.HeaderText = "HidenItemImage";
            this.HidenItemImageColumn.Name = "HidenItemImageColumn";
            this.HidenItemImageColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.HidenItemImageColumn.Visible = false;
            // 
            // HidenItemMarketHashName
            // 
            this.HidenItemMarketHashName.HeaderText = "HidenItemMarketHashNameColumn";
            this.HidenItemMarketHashName.Name = "HidenItemMarketHashName";
            this.HidenItemMarketHashName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.HidenItemMarketHashName.Visible = false;
            // 
            // ItemImageBox
            // 
            this.ItemImageBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.ItemImageBox.BackgroundImage = global::autotrade.Properties.Resources.DefaultItem;
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
            // ItemDescriptionGroupBox
            // 
            this.ItemDescriptionGroupBox.Controls.Add(this.ItemDescriptionTextBox);
            this.ItemDescriptionGroupBox.Controls.Add(this.ItemNameLable);
            this.ItemDescriptionGroupBox.Controls.Add(this.ItemImageBox);
            this.ItemDescriptionGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.ItemDescriptionGroupBox.Location = new System.Drawing.Point(579, 66);
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
            // ItemsToSaleGroupBox
            // 
            this.ItemsToSaleGroupBox.Controls.Add(this.ForcePricesReloadButton);
            this.ItemsToSaleGroupBox.Controls.Add(this.ItemsToSaleGridView);
            this.ItemsToSaleGroupBox.Controls.Add(this.DeleteSelectedItemButton);
            this.ItemsToSaleGroupBox.Controls.Add(this.ItemsForSaleUpdateOneButtonClick);
            this.ItemsToSaleGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.ItemsToSaleGroupBox.Location = new System.Drawing.Point(7, 323);
            this.ItemsToSaleGroupBox.Name = "ItemsToSaleGroupBox";
            this.ItemsToSaleGroupBox.Size = new System.Drawing.Size(564, 267);
            this.ItemsToSaleGroupBox.TabIndex = 15;
            this.ItemsToSaleGroupBox.TabStop = false;
            this.ItemsToSaleGroupBox.Text = "Items for sale";
            // 
            // ItemsForSaleUpdateOneButtonClick
            // 
            this.ItemsForSaleUpdateOneButtonClick.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ItemsForSaleUpdateOneButtonClick.FlatAppearance.BorderSize = 0;
            this.ItemsForSaleUpdateOneButtonClick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ItemsForSaleUpdateOneButtonClick.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.ItemsForSaleUpdateOneButtonClick.Image = global::autotrade.Properties.Resources.update_1;
            this.ItemsForSaleUpdateOneButtonClick.Location = new System.Drawing.Point(451, 221);
            this.ItemsForSaleUpdateOneButtonClick.Name = "ItemsForSaleUpdateOneButtonClick";
            this.ItemsForSaleUpdateOneButtonClick.Size = new System.Drawing.Size(47, 40);
            this.ItemsForSaleUpdateOneButtonClick.TabIndex = 24;
            this.AddAllToolTip.SetToolTip(this.ItemsForSaleUpdateOneButtonClick, "Force selected items price reload (cached prices will be ignored)");
            this.ItemsForSaleUpdateOneButtonClick.UseVisualStyleBackColor = true;
            this.ItemsForSaleUpdateOneButtonClick.Click += new System.EventHandler(this.ItemsForSaleUpdateOneButtonClick_Click);
            // 
            // ForcePricesReloadButton
            // 
            this.ForcePricesReloadButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ForcePricesReloadButton.FlatAppearance.BorderSize = 0;
            this.ForcePricesReloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ForcePricesReloadButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.ForcePricesReloadButton.Image = global::autotrade.Properties.Resources.RefreshButton;
            this.ForcePricesReloadButton.Location = new System.Drawing.Point(398, 221);
            this.ForcePricesReloadButton.Name = "ForcePricesReloadButton";
            this.ForcePricesReloadButton.Size = new System.Drawing.Size(47, 40);
            this.ForcePricesReloadButton.TabIndex = 23;
            this.AddAllToolTip.SetToolTip(this.ForcePricesReloadButton, "Force prices reload of items for sale");
            this.ForcePricesReloadButton.UseVisualStyleBackColor = true;
            this.ForcePricesReloadButton.Click += new System.EventHandler(this.ForcePricesReloadButton_Click);
            // 
            // ItemsToSaleGridView
            // 
            this.ItemsToSaleGridView.AllowUserToAddRows = false;
            this.ItemsToSaleGridView.AllowUserToDeleteRows = false;
            this.ItemsToSaleGridView.AllowUserToResizeRows = false;
            this.ItemsToSaleGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ItemsToSaleGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.ItemsToSaleGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle50.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle50.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle50.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle50.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle50.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle50.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemsToSaleGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle50;
            this.ItemsToSaleGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemsToSaleGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AddedToSaleListItemName,
            this.AddedToSaleListItemsCount,
            this.ItemToSalePriceColumn,
            this.AveragePrice,
            this.HidenMarketHashNameColumn,
            this.AddedToSaleListHidenItemsList});
            dataGridViewCellStyle54.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle54.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle54.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle54.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle54.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle54.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemsToSaleGridView.DefaultCellStyle = dataGridViewCellStyle54;
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
            this.ItemsToSaleGridView.Size = new System.Drawing.Size(558, 201);
            this.ItemsToSaleGridView.TabIndex = 13;
            this.ItemsToSaleGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemsToSaleGridView_CellClick);
            this.ItemsToSaleGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemsToSaleGridView_CellEndEdit);
            this.ItemsToSaleGridView.CurrentCellChanged += new System.EventHandler(this.ItemsToSaleGridView_CurrentCellChanged);
            // 
            // DeleteSelectedItemButton
            // 
            this.DeleteSelectedItemButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteSelectedItemButton.FlatAppearance.BorderSize = 0;
            this.DeleteSelectedItemButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteSelectedItemButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.DeleteSelectedItemButton.Image = global::autotrade.Properties.Resources.CancelButton;
            this.DeleteSelectedItemButton.Location = new System.Drawing.Point(504, 221);
            this.DeleteSelectedItemButton.Name = "DeleteSelectedItemButton";
            this.DeleteSelectedItemButton.Size = new System.Drawing.Size(47, 40);
            this.DeleteSelectedItemButton.TabIndex = 22;
            this.AddAllToolTip.SetToolTip(this.DeleteSelectedItemButton, "Remove selected item from to sale list");
            this.DeleteSelectedItemButton.UseVisualStyleBackColor = true;
            this.DeleteSelectedItemButton.Click += new System.EventHandler(this.DeleteAccountButton_Click);
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
            // SplitterPanel
            // 
            this.SplitterPanel.BackgroundImage = global::autotrade.Properties.Resources.NotLogined;
            this.SplitterPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.SplitterPanel.Location = new System.Drawing.Point(690, 11);
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
            this.LoadInventoryButton.Image = global::autotrade.Properties.Resources.Download;
            this.LoadInventoryButton.Location = new System.Drawing.Point(206, 9);
            this.LoadInventoryButton.Name = "LoadInventoryButton";
            this.LoadInventoryButton.Size = new System.Drawing.Size(76, 65);
            this.LoadInventoryButton.TabIndex = 25;
            this.AddAllToolTip.SetToolTip(this.LoadInventoryButton, "Load selected inventory");
            this.LoadInventoryButton.UseVisualStyleBackColor = true;
            this.LoadInventoryButton.Click += new System.EventHandler(this.LoadInventoryButton_Click);
            // 
            // CurrentMinusPriceRadioButton
            // 
            this.CurrentMinusPriceRadioButton.AutoSize = true;
            this.CurrentMinusPriceRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentMinusPriceRadioButton.Location = new System.Drawing.Point(12, 79);
            this.CurrentMinusPriceRadioButton.Name = "CurrentMinusPriceRadioButton";
            this.CurrentMinusPriceRadioButton.Size = new System.Drawing.Size(101, 20);
            this.CurrentMinusPriceRadioButton.TabIndex = 1;
            this.CurrentMinusPriceRadioButton.Text = "Current price";
            this.AddAllToolTip.SetToolTip(this.CurrentMinusPriceRadioButton, "The value to add to the current calculated price (minimum and maximum are respect" +
        "ed)");
            this.CurrentMinusPriceRadioButton.UseVisualStyleBackColor = true;
            this.CurrentMinusPriceRadioButton.CheckedChanged += new System.EventHandler(this.HalfAutoPriceRadioButton_CheckedChanged);
            // 
            // CurrentPriceNumericUpDown
            // 
            this.CurrentPriceNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.CurrentPriceNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrentPriceNumericUpDown.DecimalPlaces = 2;
            this.CurrentPriceNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CurrentPriceNumericUpDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.CurrentPriceNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.CurrentPriceNumericUpDown.Location = new System.Drawing.Point(130, 79);
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
            this.AddAllToolTip.SetToolTip(this.CurrentPriceNumericUpDown, "Only one value type can be used");
            this.CurrentPriceNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147352576});
            this.CurrentPriceNumericUpDown.Visible = false;
            this.CurrentPriceNumericUpDown.ValueChanged += new System.EventHandler(this.CurrentPriceNumericUpDown_ValueChanged);
            // 
            // AddAllButton
            // 
            this.AddAllButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.AddAllButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddAllButton.FlatAppearance.BorderSize = 0;
            this.AddAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddAllButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.AddAllButton.Image = global::autotrade.Properties.Resources.AddAllButton;
            this.AddAllButton.Location = new System.Drawing.Point(582, 10);
            this.AddAllButton.Name = "AddAllButton";
            this.AddAllButton.Size = new System.Drawing.Size(47, 40);
            this.AddAllButton.TabIndex = 24;
            this.AddAllToolTip.SetToolTip(this.AddAllButton, "Add all selected items to sale list");
            this.AddAllButton.UseVisualStyleBackColor = false;
            this.AddAllButton.Click += new System.EventHandler(this.AddAllButton_Click);
            // 
            // RefreshPricesButton
            // 
            this.RefreshPricesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.RefreshPricesButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RefreshPricesButton.FlatAppearance.BorderSize = 0;
            this.RefreshPricesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RefreshPricesButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.RefreshPricesButton.Image = global::autotrade.Properties.Resources.RefreshButton;
            this.RefreshPricesButton.Location = new System.Drawing.Point(775, 12);
            this.RefreshPricesButton.Name = "RefreshPricesButton";
            this.RefreshPricesButton.Size = new System.Drawing.Size(47, 40);
            this.RefreshPricesButton.TabIndex = 24;
            this.AddAllToolTip.SetToolTip(this.RefreshPricesButton, "Force all prices reload (cached prices will be ignored)");
            this.RefreshPricesButton.UseVisualStyleBackColor = false;
            this.RefreshPricesButton.Click += new System.EventHandler(this.RefreshPricesButton_Click);
            // 
            // OpenMarketPageButtonClick
            // 
            this.OpenMarketPageButtonClick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.OpenMarketPageButtonClick.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OpenMarketPageButtonClick.FlatAppearance.BorderSize = 0;
            this.OpenMarketPageButtonClick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenMarketPageButtonClick.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.OpenMarketPageButtonClick.Image = global::autotrade.Properties.Resources.SteamButton;
            this.OpenMarketPageButtonClick.Location = new System.Drawing.Point(828, 12);
            this.OpenMarketPageButtonClick.Name = "OpenMarketPageButtonClick";
            this.OpenMarketPageButtonClick.Size = new System.Drawing.Size(47, 40);
            this.OpenMarketPageButtonClick.TabIndex = 25;
            this.AddAllToolTip.SetToolTip(this.OpenMarketPageButtonClick, "Find selected item on Steam Market");
            this.OpenMarketPageButtonClick.UseVisualStyleBackColor = false;
            this.OpenMarketPageButtonClick.Click += new System.EventHandler(this.OpenMarketPageButtonClick_Click);
            // 
            // AllItemsUpdateOneButtonClick
            // 
            this.AllItemsUpdateOneButtonClick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.AllItemsUpdateOneButtonClick.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AllItemsUpdateOneButtonClick.FlatAppearance.BorderSize = 0;
            this.AllItemsUpdateOneButtonClick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AllItemsUpdateOneButtonClick.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.AllItemsUpdateOneButtonClick.Image = global::autotrade.Properties.Resources.update_1;
            this.AllItemsUpdateOneButtonClick.Location = new System.Drawing.Point(635, 10);
            this.AllItemsUpdateOneButtonClick.Name = "AllItemsUpdateOneButtonClick";
            this.AllItemsUpdateOneButtonClick.Size = new System.Drawing.Size(47, 40);
            this.AllItemsUpdateOneButtonClick.TabIndex = 26;
            this.AddAllToolTip.SetToolTip(this.AllItemsUpdateOneButtonClick, "Force selected items price reload (cached prices will be ignored)");
            this.AllItemsUpdateOneButtonClick.UseVisualStyleBackColor = false;
            this.AllItemsUpdateOneButtonClick.Click += new System.EventHandler(this.AllItemsUpdateOneButtonClick_Click);
            // 
            // AveragePriceNumericUpDown
            // 
            this.AveragePriceNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.AveragePriceNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AveragePriceNumericUpDown.DecimalPlaces = 2;
            this.AveragePriceNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AveragePriceNumericUpDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.AveragePriceNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.AveragePriceNumericUpDown.Location = new System.Drawing.Point(130, 57);
            this.AveragePriceNumericUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.AveragePriceNumericUpDown.Minimum = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147483648});
            this.AveragePriceNumericUpDown.Name = "AveragePriceNumericUpDown";
            this.AveragePriceNumericUpDown.Size = new System.Drawing.Size(76, 22);
            this.AveragePriceNumericUpDown.TabIndex = 6;
            this.AveragePriceNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AddAllToolTip.SetToolTip(this.AveragePriceNumericUpDown, "Only one value type can be used");
            this.AveragePriceNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147352576});
            this.AveragePriceNumericUpDown.Visible = false;
            this.AveragePriceNumericUpDown.ValueChanged += new System.EventHandler(this.AveragePriceNumericUpDown_ValueChanged);
            // 
            // AveregeMinusPriceRadioButton
            // 
            this.AveregeMinusPriceRadioButton.AutoSize = true;
            this.AveregeMinusPriceRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AveregeMinusPriceRadioButton.Location = new System.Drawing.Point(11, 57);
            this.AveregeMinusPriceRadioButton.Name = "AveregeMinusPriceRadioButton";
            this.AveregeMinusPriceRadioButton.Size = new System.Drawing.Size(111, 20);
            this.AveregeMinusPriceRadioButton.TabIndex = 5;
            this.AveregeMinusPriceRadioButton.Text = "Average price";
            this.AddAllToolTip.SetToolTip(this.AveregeMinusPriceRadioButton, "The value to add to the average calculated price (minimum and maximum are respect" +
        "ed)");
            this.AveregeMinusPriceRadioButton.UseVisualStyleBackColor = true;
            this.AveregeMinusPriceRadioButton.CheckedChanged += new System.EventHandler(this.AveregeMinusPriceRadioButton_CheckedChanged);
            // 
            // ManualPriceRadioButton
            // 
            this.ManualPriceRadioButton.AutoSize = true;
            this.ManualPriceRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManualPriceRadioButton.Location = new System.Drawing.Point(12, 35);
            this.ManualPriceRadioButton.Name = "ManualPriceRadioButton";
            this.ManualPriceRadioButton.Size = new System.Drawing.Size(80, 20);
            this.ManualPriceRadioButton.TabIndex = 2;
            this.ManualPriceRadioButton.Text = "Manually";
            this.AddAllToolTip.SetToolTip(this.ManualPriceRadioButton, "Allow manual price change");
            this.ManualPriceRadioButton.UseVisualStyleBackColor = true;
            // 
            // RecomendedPriceRadioButton
            // 
            this.RecomendedPriceRadioButton.AutoSize = true;
            this.RecomendedPriceRadioButton.Checked = true;
            this.RecomendedPriceRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecomendedPriceRadioButton.Location = new System.Drawing.Point(12, 13);
            this.RecomendedPriceRadioButton.Name = "RecomendedPriceRadioButton";
            this.RecomendedPriceRadioButton.Size = new System.Drawing.Size(153, 20);
            this.RecomendedPriceRadioButton.TabIndex = 0;
            this.RecomendedPriceRadioButton.TabStop = true;
            this.RecomendedPriceRadioButton.Text = "Recommended price";
            this.AddAllToolTip.SetToolTip(this.RecomendedPriceRadioButton, "The largest of the maximum and current prices");
            this.RecomendedPriceRadioButton.UseVisualStyleBackColor = true;
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
            this.InventoryGroupBox.Text = "Inventory loading settings";
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
            this.InventoryAppIdComboBox.TextChanged += new System.EventHandler(this.InventoryAppIdComboBox_SelectedIndexChanged);
            // 
            // AccountNameLable
            // 
            this.AccountNameLable.BackColor = System.Drawing.Color.Transparent;
            this.AccountNameLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AccountNameLable.Location = new System.Drawing.Point(658, 48);
            this.AccountNameLable.Name = "AccountNameLable";
            this.AccountNameLable.Size = new System.Drawing.Size(140, 18);
            this.AccountNameLable.TabIndex = 11;
            this.AccountNameLable.Text = "Not logged in";
            this.AccountNameLable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StartSteamSellButton
            // 
            this.StartSteamSellButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.StartSteamSellButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StartSteamSellButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StartSteamSellButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.StartSteamSellButton.Location = new System.Drawing.Point(50, 28);
            this.StartSteamSellButton.Name = "StartSteamSellButton";
            this.StartSteamSellButton.Size = new System.Drawing.Size(203, 35);
            this.StartSteamSellButton.TabIndex = 14;
            this.StartSteamSellButton.Text = "Sell on STEAM Market";
            this.StartSteamSellButton.UseVisualStyleBackColor = false;
            this.StartSteamSellButton.Click += new System.EventHandler(this.StartSteamSellButton_Click);
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
            // PriceSettingsGroupBox
            // 
            this.PriceSettingsGroupBox.Controls.Add(this.AveragePricePercentNumericUpDown);
            this.PriceSettingsGroupBox.Controls.Add(this.AveragePriceNumericUpDown);
            this.PriceSettingsGroupBox.Controls.Add(this.AveregeMinusPriceRadioButton);
            this.PriceSettingsGroupBox.Controls.Add(this.CurrentPricePercentNumericUpDown);
            this.PriceSettingsGroupBox.Controls.Add(this.CurrentPriceNumericUpDown);
            this.PriceSettingsGroupBox.Controls.Add(this.ManualPriceRadioButton);
            this.PriceSettingsGroupBox.Controls.Add(this.CurrentMinusPriceRadioButton);
            this.PriceSettingsGroupBox.Controls.Add(this.RecomendedPriceRadioButton);
            this.PriceSettingsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.PriceSettingsGroupBox.Location = new System.Drawing.Point(580, 385);
            this.PriceSettingsGroupBox.Name = "PriceSettingsGroupBox";
            this.PriceSettingsGroupBox.Size = new System.Drawing.Size(296, 107);
            this.PriceSettingsGroupBox.TabIndex = 17;
            this.PriceSettingsGroupBox.TabStop = false;
            this.PriceSettingsGroupBox.Text = "The price formation setting ";
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
            dataGridViewCellStyle51.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AddedToSaleListItemsCount.DefaultCellStyle = dataGridViewCellStyle51;
            this.AddedToSaleListItemsCount.FillWeight = 35F;
            this.AddedToSaleListItemsCount.HeaderText = "Amount for sale";
            this.AddedToSaleListItemsCount.Name = "AddedToSaleListItemsCount";
            this.AddedToSaleListItemsCount.ReadOnly = true;
            // 
            // ItemToSalePriceColumn
            // 
            dataGridViewCellStyle52.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ItemToSalePriceColumn.DefaultCellStyle = dataGridViewCellStyle52;
            this.ItemToSalePriceColumn.FillWeight = 35F;
            this.ItemToSalePriceColumn.HeaderText = "Current price";
            this.ItemToSalePriceColumn.Name = "ItemToSalePriceColumn";
            // 
            // AveragePrice
            // 
            dataGridViewCellStyle53.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AveragePrice.DefaultCellStyle = dataGridViewCellStyle53;
            this.AveragePrice.FillWeight = 35F;
            this.AveragePrice.HeaderText = "Average price";
            this.AveragePrice.Name = "AveragePrice";
            this.AveragePrice.ReadOnly = true;
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
            // ItemDescriptionTextBox
            // 
            this.ItemDescriptionTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.ItemDescriptionTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ItemDescriptionTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.ItemDescriptionTextBox.Location = new System.Drawing.Point(3, 141);
            this.ItemDescriptionTextBox.Name = "ItemDescriptionTextBox";
            this.ItemDescriptionTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.ItemDescriptionTextBox.Size = new System.Drawing.Size(290, 80);
            this.ItemDescriptionTextBox.TabIndex = 11;
            this.ItemDescriptionTextBox.Text = "";
            // 
            // AveragePricePercentNumericUpDown
            // 
            this.AveragePricePercentNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.AveragePricePercentNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AveragePricePercentNumericUpDown.DecimalPlaces = 2;
            this.AveragePricePercentNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AveragePricePercentNumericUpDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.AveragePricePercentNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.AveragePricePercentNumericUpDown.Location = new System.Drawing.Point(212, 57);
            this.AveragePricePercentNumericUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.AveragePricePercentNumericUpDown.Minimum = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147483648});
            this.AveragePricePercentNumericUpDown.Name = "AveragePricePercentNumericUpDown";
            this.AveragePricePercentNumericUpDown.Size = new System.Drawing.Size(76, 22);
            this.AveragePricePercentNumericUpDown.TabIndex = 7;
            this.AveragePricePercentNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AddAllToolTip.SetToolTip(this.AveragePricePercentNumericUpDown, "Only one value type can be used");
            this.AveragePricePercentNumericUpDown.TrailingSign = "%";
            this.AveragePricePercentNumericUpDown.Visible = false;
            this.AveragePricePercentNumericUpDown.ValueChanged += new System.EventHandler(this.AveragePricePercentNumericUpDown_ValueChanged);
            // 
            // CurrentPricePercentNumericUpDown
            // 
            this.CurrentPricePercentNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.CurrentPricePercentNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrentPricePercentNumericUpDown.DecimalPlaces = 2;
            this.CurrentPricePercentNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CurrentPricePercentNumericUpDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.CurrentPricePercentNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.CurrentPricePercentNumericUpDown.Location = new System.Drawing.Point(212, 79);
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
            this.AddAllToolTip.SetToolTip(this.CurrentPricePercentNumericUpDown, "Only one value type can be used");
            this.CurrentPricePercentNumericUpDown.TrailingSign = "%";
            this.CurrentPricePercentNumericUpDown.Visible = false;
            this.CurrentPricePercentNumericUpDown.ValueChanged += new System.EventHandler(this.CurrentPricePercentNumericUpDown_ValueChanged);
            // 
            // SaleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.Controls.Add(this.OpenMarketPageButtonClick);
            this.Controls.Add(this.ItemDescriptionGroupBox);
            this.Controls.Add(this.RefreshPricesButton);
            this.Controls.Add(this.AllItemsUpdateOneButtonClick);
            this.Controls.Add(this.AddAllButton);
            this.Controls.Add(this.InventoryGroupBox);
            this.Controls.Add(this.SplitterPanel);
            this.Controls.Add(this.AllSteamItemsGroupBox);
            this.Controls.Add(this.ItemsToSaleGroupBox);
            this.Controls.Add(this.SellGroupBox);
            this.Controls.Add(this.PriceSettingsGroupBox);
            this.Controls.Add(this.AccountNameLable);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.Name = "SaleControl";
            this.Size = new System.Drawing.Size(885, 599);
            this.Load += new System.EventHandler(this.SaleControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AllSteamItemsGridView)).EndInit();
            this.ItemDescriptionGroupBox.ResumeLayout(false);
            this.ItemsToSaleGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ItemsToSaleGridView)).EndInit();
            this.AllSteamItemsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPriceNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AveragePriceNumericUpDown)).EndInit();
            this.InventoryGroupBox.ResumeLayout(false);
            this.InventoryGroupBox.PerformLayout();
            this.SellGroupBox.ResumeLayout(false);
            this.PriceSettingsGroupBox.ResumeLayout(false);
            this.PriceSettingsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AveragePricePercentNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPricePercentNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        public void setSizeListView1(int width, int height)
        {
            // this.listView1.Size = new System.Drawing.Size(width, height);
            this.AllSteamItemsGridView.Size = new System.Drawing.Size(width, height);
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
        private DataGridView AllSteamItemsGridView;
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
        private Button StartSteamSellButton;
        private GroupBox SellGroupBox;
        private RadioButton RecomendedPriceRadioButton;
        private RadioButton CurrentMinusPriceRadioButton;
        private RadioButton ManualPriceRadioButton;
        private NumericUpDown CurrentPriceNumericUpDown;
        private CustomNumericUpDown CurrentPricePercentNumericUpDown;
        private GroupBox PriceSettingsGroupBox;
        private DataGridView ItemsToSaleGridView;
        private Button ForcePricesReloadButton;
        private Button AddAllButton;
        private Button RefreshPricesButton;
        private Button OpenMarketPageButtonClick;
        private Button AllItemsUpdateOneButtonClick;
        private RichTextBoxWithNoPaint ItemDescriptionTextBox;
        private CustomNumericUpDown AveragePricePercentNumericUpDown;
        private NumericUpDown AveragePriceNumericUpDown;
        private RadioButton AveregeMinusPriceRadioButton;
        private DataGridViewTextBoxColumn NameColumn;
        private DataGridViewTextBoxColumn CountColumn;
        private DataGridViewTextBoxColumn ItemTypeColumn;
        private DataGridViewTextBoxColumn CurrentPriceColumn;
        private DataGridViewTextBoxColumn AveragePriceColumn;
        private DataGridViewComboBoxColumn CountToAddColumn;
        private DataGridViewButtonColumn AddButtonColumn;
        private DataGridViewTextBoxColumn HidenItemsListColumn;
        private DataGridViewTextBoxColumn HidenItemImageColumn;
        private DataGridViewTextBoxColumn HidenItemMarketHashName;
        private Button ItemsForSaleUpdateOneButtonClick;
        private DataGridViewTextBoxColumn AddedToSaleListItemName;
        private DataGridViewTextBoxColumn AddedToSaleListItemsCount;
        private DataGridViewTextBoxColumn ItemToSalePriceColumn;
        private DataGridViewTextBoxColumn AveragePrice;
        private DataGridViewTextBoxColumn HidenMarketHashNameColumn;
        private DataGridViewTextBoxColumn AddedToSaleListHidenItemsList;
    }
}