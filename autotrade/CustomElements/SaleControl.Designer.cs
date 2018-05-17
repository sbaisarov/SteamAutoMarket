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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaleControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.AllSteamItemsGridView = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.CurrentPriceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ManualPriceRadioButton = new System.Windows.Forms.RadioButton();
            this.HalfAutoPriceRadioButton = new System.Windows.Forms.RadioButton();
            this.RecomendedPriceRadioButton = new System.Windows.Forms.RadioButton();
            this.SellGroupBox = new System.Windows.Forms.GroupBox();
            this.StartOpsSellButton = new System.Windows.Forms.Button();
            this.RefreshPanel = new System.Windows.Forms.Panel();
            this.AddAllPanel = new System.Windows.Forms.Panel();
            this.SteamPanel = new System.Windows.Forms.Panel();
            this.OpskinsPanel = new System.Windows.Forms.Panel();
            this.SplitterPanel = new System.Windows.Forms.Panel();
            this.AddAllToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.AllSteamItemsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsToSaleGridView)).BeginInit();
            this.ItemDescriptionGroupBox.SuspendLayout();
            this.ItemsToSaleGroupBox.SuspendLayout();
            this.AllSteamItemsGroupBox.SuspendLayout();
            this.PriceSettingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPriceNumericUpDown)).BeginInit();
            this.SellGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AllSteamItemsGridView
            // 
            this.AllSteamItemsGridView.AllowUserToAddRows = false;
            this.AllSteamItemsGridView.AllowUserToDeleteRows = false;
            this.AllSteamItemsGridView.AllowUserToResizeColumns = false;
            this.AllSteamItemsGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Menu;
            this.AllSteamItemsGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.AllSteamItemsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AllSteamItemsGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.AllSteamItemsGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AllSteamItemsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.AllSteamItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AllSteamItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.CountColumn,
            this.CountToAddColumn,
            this.AddButtonColumn,
            this.AddAllButtonColumn,
            this.HidenItemsListColumn,
            this.HidenItemImageColumn,
            this.HidenItemMarketHashName});
            this.AllSteamItemsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllSteamItemsGridView.GridColor = System.Drawing.Color.White;
            this.AllSteamItemsGridView.Location = new System.Drawing.Point(3, 16);
            this.AllSteamItemsGridView.MultiSelect = false;
            this.AllSteamItemsGridView.Name = "AllSteamItemsGridView";
            this.AllSteamItemsGridView.RowHeadersVisible = false;
            this.AllSteamItemsGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.AllSteamItemsGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AllSteamItemsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AllSteamItemsGridView.Size = new System.Drawing.Size(479, 295);
            this.AllSteamItemsGridView.TabIndex = 7;
            this.AllSteamItemsGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SteamSaleDataGridView_CellClick);
            this.AllSteamItemsGridView.CurrentCellChanged += new System.EventHandler(this.AllSteamItemsGridView_CurrentCellChanged);
            this.AllSteamItemsGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.SteamSaleDataGridView_EditingControlShowing);
            // 
            // NameColumn
            // 
            this.NameColumn.DataPropertyName = "SaleSteamControl";
            this.NameColumn.FillWeight = 85.5619F;
            this.NameColumn.HeaderText = "Название";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.ToolTipText = "Item name";
            // 
            // CountColumn
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CountColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.CountColumn.FillWeight = 35F;
            this.CountColumn.HeaderText = "Общее кол-во";
            this.CountColumn.Name = "CountColumn";
            this.CountColumn.ReadOnly = true;
            // 
            // CountToAddColumn
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.CountToAddColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.CountToAddColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.CountToAddColumn.FillWeight = 35F;
            this.CountToAddColumn.HeaderText = "Кол-во";
            this.CountToAddColumn.MaxDropDownItems = 10;
            this.CountToAddColumn.Name = "CountToAddColumn";
            this.CountToAddColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // AddButtonColumn
            // 
            this.AddButtonColumn.FillWeight = 35F;
            this.AddButtonColumn.HeaderText = "Добавить";
            this.AddButtonColumn.Name = "AddButtonColumn";
            this.AddButtonColumn.Text = "Add";
            this.AddButtonColumn.UseColumnTextForButtonValue = true;
            // 
            // AddAllButtonColumn
            // 
            this.AddAllButtonColumn.FillWeight = 35F;
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
            this.ItemImageBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ItemImageBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ItemImageBox.BackgroundImage")));
            this.ItemImageBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ItemImageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ItemImageBox.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.ItemImageBox.Location = new System.Drawing.Point(3, 17);
            this.ItemImageBox.Name = "ItemImageBox";
            this.ItemImageBox.Size = new System.Drawing.Size(120, 120);
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
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Menu;
            this.ItemsToSaleGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.ItemsToSaleGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemsToSaleGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.ItemsToSaleGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemsToSaleGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AddedToSaleListItemName,
            this.AddedToSaleListItemsCount,
            this.ItemToSalePriceColumn,
            this.HidenMarketHashNameColumn,
            this.AddedToSaleListHidenItemsList});
            this.ItemsToSaleGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.ItemsToSaleGridView.GridColor = System.Drawing.Color.White;
            this.ItemsToSaleGridView.Location = new System.Drawing.Point(3, 16);
            this.ItemsToSaleGridView.MultiSelect = false;
            this.ItemsToSaleGridView.Name = "ItemsToSaleGridView";
            this.ItemsToSaleGridView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ItemsToSaleGridView.RowHeadersVisible = false;
            this.ItemsToSaleGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ItemsToSaleGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ItemsToSaleGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ItemsToSaleGridView.Size = new System.Drawing.Size(473, 215);
            this.ItemsToSaleGridView.TabIndex = 13;
            this.ItemsToSaleGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemsToSaleGridView_CellClick);
            this.ItemsToSaleGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemsToSaleGridView_CellEndEdit);
            this.ItemsToSaleGridView.CurrentCellChanged += new System.EventHandler(this.ItemsToSaleGridView_CurrentCellChanged);
            // 
            // AddedToSaleListItemName
            // 
            this.AddedToSaleListItemName.FillWeight = 163.7636F;
            this.AddedToSaleListItemName.HeaderText = "Название";
            this.AddedToSaleListItemName.Name = "AddedToSaleListItemName";
            this.AddedToSaleListItemName.ReadOnly = true;
            // 
            // AddedToSaleListItemsCount
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AddedToSaleListItemsCount.DefaultCellStyle = dataGridViewCellStyle7;
            this.AddedToSaleListItemsCount.FillWeight = 50F;
            this.AddedToSaleListItemsCount.HeaderText = "Кол-во к продаже";
            this.AddedToSaleListItemsCount.Name = "AddedToSaleListItemsCount";
            this.AddedToSaleListItemsCount.ReadOnly = true;
            // 
            // ItemToSalePriceColumn
            // 
            this.ItemToSalePriceColumn.FillWeight = 50F;
            this.ItemToSalePriceColumn.HeaderText = "Цена за 1";
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
            // ItemDescriptionGroupBox
            // 
            this.ItemDescriptionGroupBox.Controls.Add(this.ItemNameLable);
            this.ItemDescriptionGroupBox.Controls.Add(this.ItemDescriptionTextBox);
            this.ItemDescriptionGroupBox.Controls.Add(this.ItemImageBox);
            this.ItemDescriptionGroupBox.Location = new System.Drawing.Point(497, 54);
            this.ItemDescriptionGroupBox.Name = "ItemDescriptionGroupBox";
            this.ItemDescriptionGroupBox.Size = new System.Drawing.Size(259, 263);
            this.ItemDescriptionGroupBox.TabIndex = 14;
            this.ItemDescriptionGroupBox.TabStop = false;
            this.ItemDescriptionGroupBox.Text = "Описание предмета";
            // 
            // ItemNameLable
            // 
            this.ItemNameLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ItemNameLable.Location = new System.Drawing.Point(129, 17);
            this.ItemNameLable.Name = "ItemNameLable";
            this.ItemNameLable.Size = new System.Drawing.Size(121, 120);
            this.ItemNameLable.TabIndex = 10;
            // 
            // ItemDescriptionTextBox
            // 
            this.ItemDescriptionTextBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ItemDescriptionTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.ItemDescriptionTextBox.DetectUrls = false;
            this.ItemDescriptionTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ItemDescriptionTextBox.Location = new System.Drawing.Point(3, 143);
            this.ItemDescriptionTextBox.Name = "ItemDescriptionTextBox";
            this.ItemDescriptionTextBox.ReadOnly = true;
            this.ItemDescriptionTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.ItemDescriptionTextBox.Size = new System.Drawing.Size(253, 117);
            this.ItemDescriptionTextBox.TabIndex = 9;
            this.ItemDescriptionTextBox.Text = "";
            // 
            // ItemsToSaleGroupBox
            // 
            this.ItemsToSaleGroupBox.Controls.Add(this.ItemsToSaleGridView);
            this.ItemsToSaleGroupBox.Controls.Add(this.DeleteSelectedItemButton);
            this.ItemsToSaleGroupBox.Controls.Add(this.DeleteUntradableSteamButton);
            this.ItemsToSaleGroupBox.Controls.Add(this.DeleteUnmarketableSteamButton);
            this.ItemsToSaleGroupBox.Location = new System.Drawing.Point(9, 323);
            this.ItemsToSaleGroupBox.Name = "ItemsToSaleGroupBox";
            this.ItemsToSaleGroupBox.Size = new System.Drawing.Size(479, 273);
            this.ItemsToSaleGroupBox.TabIndex = 15;
            this.ItemsToSaleGroupBox.TabStop = false;
            this.ItemsToSaleGroupBox.Text = "Предметы к продаже";
            // 
            // DeleteSelectedItemButton
            // 
            this.DeleteSelectedItemButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteSelectedItemButton.ForeColor = System.Drawing.Color.Silver;
            this.DeleteSelectedItemButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteSelectedItemButton.Image")));
            this.DeleteSelectedItemButton.Location = new System.Drawing.Point(432, 230);
            this.DeleteSelectedItemButton.Name = "DeleteSelectedItemButton";
            this.DeleteSelectedItemButton.Size = new System.Drawing.Size(40, 40);
            this.DeleteSelectedItemButton.TabIndex = 22;
            this.AddAllToolTip.SetToolTip(this.DeleteSelectedItemButton, "Удалить выбранный предмет из списка к продаже");
            this.DeleteSelectedItemButton.UseVisualStyleBackColor = true;
            this.DeleteSelectedItemButton.Click += new System.EventHandler(this.DeleteAccountButton_Click);
            // 
            // DeleteUntradableSteamButton
            // 
            this.DeleteUntradableSteamButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteUntradableSteamButton.ForeColor = System.Drawing.Color.Silver;
            this.DeleteUntradableSteamButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteUntradableSteamButton.Image")));
            this.DeleteUntradableSteamButton.Location = new System.Drawing.Point(330, 230);
            this.DeleteUntradableSteamButton.Name = "DeleteUntradableSteamButton";
            this.DeleteUntradableSteamButton.Size = new System.Drawing.Size(40, 40);
            this.DeleteUntradableSteamButton.TabIndex = 23;
            this.AddAllToolTip.SetToolTip(this.DeleteUntradableSteamButton, "Удалить из списка к продаже вещи которые нельзя передать на OPSKINS");
            this.DeleteUntradableSteamButton.UseVisualStyleBackColor = true;
            this.DeleteUntradableSteamButton.Click += new System.EventHandler(this.DeleteUntradableSteamButton_Click);
            // 
            // DeleteUnmarketableSteamButton
            // 
            this.DeleteUnmarketableSteamButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteUnmarketableSteamButton.ForeColor = System.Drawing.Color.Silver;
            this.DeleteUnmarketableSteamButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteUnmarketableSteamButton.Image")));
            this.DeleteUnmarketableSteamButton.Location = new System.Drawing.Point(381, 230);
            this.DeleteUnmarketableSteamButton.Name = "DeleteUnmarketableSteamButton";
            this.DeleteUnmarketableSteamButton.Size = new System.Drawing.Size(40, 40);
            this.DeleteUnmarketableSteamButton.TabIndex = 24;
            this.AddAllToolTip.SetToolTip(this.DeleteUnmarketableSteamButton, "Удалить из списка к продаже вещи которые нельзя продать на ТП");
            this.DeleteUnmarketableSteamButton.UseVisualStyleBackColor = true;
            this.DeleteUnmarketableSteamButton.Click += new System.EventHandler(this.DeleteUnmarketableSteamButton_Click);
            // 
            // StartSteamSellButton
            // 
            this.StartSteamSellButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.StartSteamSellButton.Enabled = false;
            this.StartSteamSellButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StartSteamSellButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StartSteamSellButton.Location = new System.Drawing.Point(50, 21);
            this.StartSteamSellButton.Name = "StartSteamSellButton";
            this.StartSteamSellButton.Size = new System.Drawing.Size(161, 33);
            this.StartSteamSellButton.TabIndex = 14;
            this.StartSteamSellButton.Text = "Продать на ТП";
            this.StartSteamSellButton.UseVisualStyleBackColor = false;
            // 
            // AllSteamItemsGroupBox
            // 
            this.AllSteamItemsGroupBox.Controls.Add(this.AllSteamItemsGridView);
            this.AllSteamItemsGroupBox.Location = new System.Drawing.Point(6, 3);
            this.AllSteamItemsGroupBox.Name = "AllSteamItemsGroupBox";
            this.AllSteamItemsGroupBox.Size = new System.Drawing.Size(485, 314);
            this.AllSteamItemsGroupBox.TabIndex = 16;
            this.AllSteamItemsGroupBox.TabStop = false;
            this.AllSteamItemsGroupBox.Text = "Все предметы";
            // 
            // PriceSettingsGroupBox
            // 
            this.PriceSettingsGroupBox.Controls.Add(this.CurrentPriceNumericUpDown);
            this.PriceSettingsGroupBox.Controls.Add(this.ManualPriceRadioButton);
            this.PriceSettingsGroupBox.Controls.Add(this.HalfAutoPriceRadioButton);
            this.PriceSettingsGroupBox.Controls.Add(this.RecomendedPriceRadioButton);
            this.PriceSettingsGroupBox.Location = new System.Drawing.Point(497, 323);
            this.PriceSettingsGroupBox.Name = "PriceSettingsGroupBox";
            this.PriceSettingsGroupBox.Size = new System.Drawing.Size(259, 156);
            this.PriceSettingsGroupBox.TabIndex = 17;
            this.PriceSettingsGroupBox.TabStop = false;
            this.PriceSettingsGroupBox.Text = "Настройка формирования цены";
            // 
            // CurrentPriceNumericUpDown
            // 
            this.CurrentPriceNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrentPriceNumericUpDown.DecimalPlaces = 2;
            this.CurrentPriceNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentPriceNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.CurrentPriceNumericUpDown.Location = new System.Drawing.Point(141, 86);
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
            this.CurrentPriceNumericUpDown.Size = new System.Drawing.Size(90, 22);
            this.CurrentPriceNumericUpDown.TabIndex = 3;
            this.CurrentPriceNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CurrentPriceNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147352576});
            // 
            // ManualPriceRadioButton
            // 
            this.ManualPriceRadioButton.AutoSize = true;
            this.ManualPriceRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManualPriceRadioButton.Location = new System.Drawing.Point(16, 55);
            this.ManualPriceRadioButton.Name = "ManualPriceRadioButton";
            this.ManualPriceRadioButton.Size = new System.Drawing.Size(85, 20);
            this.ManualPriceRadioButton.TabIndex = 2;
            this.ManualPriceRadioButton.Text = "Вручную";
            this.ManualPriceRadioButton.UseVisualStyleBackColor = true;
            // 
            // HalfAutoPriceRadioButton
            // 
            this.HalfAutoPriceRadioButton.AutoSize = true;
            this.HalfAutoPriceRadioButton.Checked = true;
            this.HalfAutoPriceRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HalfAutoPriceRadioButton.Location = new System.Drawing.Point(16, 86);
            this.HalfAutoPriceRadioButton.Name = "HalfAutoPriceRadioButton";
            this.HalfAutoPriceRadioButton.Size = new System.Drawing.Size(117, 20);
            this.HalfAutoPriceRadioButton.TabIndex = 1;
            this.HalfAutoPriceRadioButton.TabStop = true;
            this.HalfAutoPriceRadioButton.Text = "Текущая цена";
            this.HalfAutoPriceRadioButton.UseVisualStyleBackColor = true;
            this.HalfAutoPriceRadioButton.CheckedChanged += new System.EventHandler(this.HalfAutoPriceRadioButton_CheckedChanged);
            // 
            // RecomendedPriceRadioButton
            // 
            this.RecomendedPriceRadioButton.AutoSize = true;
            this.RecomendedPriceRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecomendedPriceRadioButton.Location = new System.Drawing.Point(16, 24);
            this.RecomendedPriceRadioButton.Name = "RecomendedPriceRadioButton";
            this.RecomendedPriceRadioButton.Size = new System.Drawing.Size(166, 20);
            this.RecomendedPriceRadioButton.TabIndex = 0;
            this.RecomendedPriceRadioButton.Text = "Рекомендуемая цена";
            this.RecomendedPriceRadioButton.UseVisualStyleBackColor = true;
            // 
            // SellGroupBox
            // 
            this.SellGroupBox.Controls.Add(this.StartOpsSellButton);
            this.SellGroupBox.Controls.Add(this.StartSteamSellButton);
            this.SellGroupBox.Location = new System.Drawing.Point(497, 482);
            this.SellGroupBox.Name = "SellGroupBox";
            this.SellGroupBox.Size = new System.Drawing.Size(259, 114);
            this.SellGroupBox.TabIndex = 18;
            this.SellGroupBox.TabStop = false;
            this.SellGroupBox.Text = "Продажа";
            // 
            // StartOpsSellButton
            // 
            this.StartOpsSellButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.StartOpsSellButton.Enabled = false;
            this.StartOpsSellButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StartOpsSellButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StartOpsSellButton.Location = new System.Drawing.Point(50, 60);
            this.StartOpsSellButton.Name = "StartOpsSellButton";
            this.StartOpsSellButton.Size = new System.Drawing.Size(161, 35);
            this.StartOpsSellButton.TabIndex = 15;
            this.StartOpsSellButton.Text = "Продать на Opskins";
            this.StartOpsSellButton.UseVisualStyleBackColor = false;
            // 
            // RefreshPanel
            // 
            this.RefreshPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RefreshPanel.BackgroundImage")));
            this.RefreshPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.RefreshPanel.Cursor = System.Windows.Forms.Cursors.Help;
            this.RefreshPanel.Location = new System.Drawing.Point(546, 10);
            this.RefreshPanel.Name = "RefreshPanel";
            this.RefreshPanel.Size = new System.Drawing.Size(38, 38);
            this.RefreshPanel.TabIndex = 19;
            this.AddAllToolTip.SetToolTip(this.RefreshPanel, "Перезагрузить инвентарь");
            this.RefreshPanel.Click += new System.EventHandler(this.RefreshPanel_Click);
            this.RefreshPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RefreshPanel_MouseDown);
            this.RefreshPanel.MouseEnter += new System.EventHandler(this.RefreshPanel_MouseEnter);
            this.RefreshPanel.MouseLeave += new System.EventHandler(this.RefreshPanel_MouseLeave);
            this.RefreshPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RefreshPanel_MouseUp);
            // 
            // AddAllPanel
            // 
            this.AddAllPanel.BackColor = System.Drawing.Color.Silver;
            this.AddAllPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AddAllPanel.BackgroundImage")));
            this.AddAllPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.AddAllPanel.Cursor = System.Windows.Forms.Cursors.Help;
            this.AddAllPanel.Location = new System.Drawing.Point(497, 10);
            this.AddAllPanel.Name = "AddAllPanel";
            this.AddAllPanel.Size = new System.Drawing.Size(38, 38);
            this.AddAllPanel.TabIndex = 20;
            this.AddAllToolTip.SetToolTip(this.AddAllPanel, "Добавить все предметы на продажу");
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
            this.SteamPanel.Cursor = System.Windows.Forms.Cursors.Help;
            this.SteamPanel.Location = new System.Drawing.Point(666, 10);
            this.SteamPanel.Name = "SteamPanel";
            this.SteamPanel.Size = new System.Drawing.Size(38, 38);
            this.SteamPanel.TabIndex = 20;
            this.AddAllToolTip.SetToolTip(this.SteamPanel, "Открыть страницу предмета на торговой площадке STEAM");
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
            this.OpskinsPanel.Cursor = System.Windows.Forms.Cursors.Help;
            this.OpskinsPanel.Location = new System.Drawing.Point(715, 10);
            this.OpskinsPanel.Name = "OpskinsPanel";
            this.OpskinsPanel.Size = new System.Drawing.Size(38, 38);
            this.OpskinsPanel.TabIndex = 21;
            this.AddAllToolTip.SetToolTip(this.OpskinsPanel, "Открыть страницу предмета на торговой площадке OPSKINS");
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
            this.SplitterPanel.Location = new System.Drawing.Point(592, 10);
            this.SplitterPanel.Name = "SplitterPanel";
            this.SplitterPanel.Size = new System.Drawing.Size(66, 38);
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
            // SaleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
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
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SaleControl";
            this.Size = new System.Drawing.Size(759, 599);
            this.Load += new System.EventHandler(this.SaleControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AllSteamItemsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsToSaleGridView)).EndInit();
            this.ItemDescriptionGroupBox.ResumeLayout(false);
            this.ItemsToSaleGroupBox.ResumeLayout(false);
            this.AllSteamItemsGroupBox.ResumeLayout(false);
            this.PriceSettingsGroupBox.ResumeLayout(false);
            this.PriceSettingsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPriceNumericUpDown)).EndInit();
            this.SellGroupBox.ResumeLayout(false);
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
        private DataGridViewTextBoxColumn NameColumn;
        private DataGridViewTextBoxColumn CountColumn;
        private DataGridViewComboBoxColumn CountToAddColumn;
        private DataGridViewButtonColumn AddButtonColumn;
        private DataGridViewButtonColumn AddAllButtonColumn;
        private DataGridViewTextBoxColumn HidenItemsListColumn;
        private DataGridViewTextBoxColumn HidenItemImageColumn;
        private DataGridViewTextBoxColumn HidenItemMarketHashName;
        private GroupBox PriceSettingsGroupBox;
        private RadioButton ManualPriceRadioButton;
        private RadioButton HalfAutoPriceRadioButton;
        private RadioButton RecomendedPriceRadioButton;
        private GroupBox SellGroupBox;
        private Button StartOpsSellButton;
        private Panel RefreshPanel;
        private Panel AddAllPanel;
        private Panel SteamPanel;
        private Panel OpskinsPanel;
        private Panel SplitterPanel;
        private ToolTip AddAllToolTip;
        private Button DeleteSelectedItemButton;
        private NumericUpDown CurrentPriceNumericUpDown;
        private DataGridViewTextBoxColumn AddedToSaleListItemName;
        private DataGridViewTextBoxColumn AddedToSaleListItemsCount;
        private DataGridViewTextBoxColumn ItemToSalePriceColumn;
        private DataGridViewTextBoxColumn HidenMarketHashNameColumn;
        private DataGridViewTextBoxColumn AddedToSaleListHidenItemsList;
        private Button DeleteUntradableSteamButton;
        private Button DeleteUnmarketableSteamButton;
    }
}