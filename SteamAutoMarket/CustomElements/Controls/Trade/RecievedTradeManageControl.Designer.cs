using SteamAutoMarket.CustomElements.Elements;

namespace SteamAutoMarket.CustomElements.Controls.Trade
{
    partial class ReceivedTradeManageControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CurrentTradesGroupBox = new System.Windows.Forms.GroupBox();
            this.CurrentTradesGridView = new System.Windows.Forms.DataGridView();
            this.TradeofferIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SenderAccountColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.TradeSteteColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellGroupBox = new System.Windows.Forms.GroupBox();
            this.DeclineTradeButton = new System.Windows.Forms.Button();
            this.AcceptTradeButton = new System.Windows.Forms.Button();
            this.MyItemsGroupBox = new System.Windows.Forms.GroupBox();
            this.MyItemsGridView = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HidenMarketHashNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDescriptionGroupBox = new System.Windows.Forms.GroupBox();
            this.ItemDescriptionTextBox = new RichTextBoxWithNoPaint();
            this.ItemNameLable = new System.Windows.Forms.Label();
            this.ItemImageBox = new System.Windows.Forms.Panel();
            this.ExtraTradeInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.ExtraTradeInfoGridView = new System.Windows.Forms.DataGridView();
            this.ParameterColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HisItemsGroupBox = new System.Windows.Forms.GroupBox();
            this.HisItemsGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HidenHisMarketHashNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TradeSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.ActiveOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.DescriptionLanguageComboBox = new System.Windows.Forms.ComboBox();
            this.PartnerIdLable = new System.Windows.Forms.Label();
            this.ReceivedOffersCheckBox = new System.Windows.Forms.CheckBox();
            this.SentOffersCheckBox = new System.Windows.Forms.CheckBox();
            this.LoadTradesButton = new System.Windows.Forms.Button();
            this.SplitterPanel = new System.Windows.Forms.Panel();
            this.AccountNameLable = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.CurrentTradesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentTradesGridView)).BeginInit();
            this.SellGroupBox.SuspendLayout();
            this.MyItemsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MyItemsGridView)).BeginInit();
            this.ItemDescriptionGroupBox.SuspendLayout();
            this.ExtraTradeInfoGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExtraTradeInfoGridView)).BeginInit();
            this.HisItemsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HisItemsGridView)).BeginInit();
            this.TradeSettingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CurrentTradesGroupBox
            // 
            this.CurrentTradesGroupBox.Controls.Add(this.CurrentTradesGridView);
            this.CurrentTradesGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.CurrentTradesGroupBox.Location = new System.Drawing.Point(7, 3);
            this.CurrentTradesGroupBox.Name = "CurrentTradesGroupBox";
            this.CurrentTradesGroupBox.Size = new System.Drawing.Size(289, 290);
            this.CurrentTradesGroupBox.TabIndex = 17;
            this.CurrentTradesGroupBox.TabStop = false;
            this.CurrentTradesGroupBox.Text = "Current trades";
            // 
            // CurrentTradesGridView
            // 
            this.CurrentTradesGridView.AllowUserToAddRows = false;
            this.CurrentTradesGridView.AllowUserToDeleteRows = false;
            this.CurrentTradesGridView.AllowUserToResizeRows = false;
            this.CurrentTradesGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CurrentTradesGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.CurrentTradesGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.CurrentTradesGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CurrentTradesGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.CurrentTradesGridView.ColumnHeadersHeight = 30;
            this.CurrentTradesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.CurrentTradesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TradeofferIdColumn,
            this.SenderAccountColumn,
            this.TradeSteteColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CurrentTradesGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.CurrentTradesGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CurrentTradesGridView.EnableHeadersVisualStyles = false;
            this.CurrentTradesGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.CurrentTradesGridView.Location = new System.Drawing.Point(3, 16);
            this.CurrentTradesGridView.Name = "CurrentTradesGridView";
            this.CurrentTradesGridView.RowHeadersVisible = false;
            this.CurrentTradesGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.CurrentTradesGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CurrentTradesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CurrentTradesGridView.Size = new System.Drawing.Size(283, 271);
            this.CurrentTradesGridView.TabIndex = 7;
            this.CurrentTradesGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CurrentTradesGridView_CellContentClick);
            this.CurrentTradesGridView.SelectionChanged += new System.EventHandler(this.CurrentTradesGridView_SelectionChanged);
            // 
            // TradeofferIdColumn
            // 
            this.TradeofferIdColumn.FillWeight = 80F;
            this.TradeofferIdColumn.HeaderText = "Id";
            this.TradeofferIdColumn.Name = "TradeofferIdColumn";
            // 
            // SenderAccountColumn
            // 
            this.SenderAccountColumn.FillWeight = 152.2696F;
            this.SenderAccountColumn.HeaderText = "Sender";
            this.SenderAccountColumn.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.SenderAccountColumn.Name = "SenderAccountColumn";
            this.SenderAccountColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SenderAccountColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // TradeSteteColumn
            // 
            this.TradeSteteColumn.FillWeight = 72F;
            this.TradeSteteColumn.HeaderText = "State";
            this.TradeSteteColumn.Name = "TradeSteteColumn";
            // 
            // SellGroupBox
            // 
            this.SellGroupBox.Controls.Add(this.DeclineTradeButton);
            this.SellGroupBox.Controls.Add(this.AcceptTradeButton);
            this.SellGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.SellGroupBox.Location = new System.Drawing.Point(603, 489);
            this.SellGroupBox.Name = "SellGroupBox";
            this.SellGroupBox.Size = new System.Drawing.Size(279, 101);
            this.SellGroupBox.TabIndex = 19;
            this.SellGroupBox.TabStop = false;
            this.SellGroupBox.Text = "Sell";
            // 
            // DeclineTradeButton
            // 
            this.DeclineTradeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.DeclineTradeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeclineTradeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeclineTradeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.DeclineTradeButton.Location = new System.Drawing.Point(45, 57);
            this.DeclineTradeButton.Name = "DeclineTradeButton";
            this.DeclineTradeButton.Size = new System.Drawing.Size(203, 35);
            this.DeclineTradeButton.TabIndex = 17;
            this.DeclineTradeButton.Text = "DECLINE selected trade";
            this.DeclineTradeButton.UseVisualStyleBackColor = false;
            this.DeclineTradeButton.Click += new System.EventHandler(this.DeclineTradeButton_Click);
            // 
            // AcceptTradeButton
            // 
            this.AcceptTradeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.AcceptTradeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AcceptTradeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AcceptTradeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.AcceptTradeButton.Location = new System.Drawing.Point(45, 13);
            this.AcceptTradeButton.Name = "AcceptTradeButton";
            this.AcceptTradeButton.Size = new System.Drawing.Size(203, 35);
            this.AcceptTradeButton.TabIndex = 16;
            this.AcceptTradeButton.Text = "ACCEPT selected trade";
            this.AcceptTradeButton.UseVisualStyleBackColor = false;
            this.AcceptTradeButton.Click += new System.EventHandler(this.AcceptTradeButton_Click);
            // 
            // MyItemsGroupBox
            // 
            this.MyItemsGroupBox.Controls.Add(this.MyItemsGridView);
            this.MyItemsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.MyItemsGroupBox.Location = new System.Drawing.Point(303, 3);
            this.MyItemsGroupBox.Name = "MyItemsGroupBox";
            this.MyItemsGroupBox.Size = new System.Drawing.Size(294, 290);
            this.MyItemsGroupBox.TabIndex = 20;
            this.MyItemsGroupBox.TabStop = false;
            this.MyItemsGroupBox.Text = "My items";
            // 
            // MyItemsGridView
            // 
            this.MyItemsGridView.AllowUserToAddRows = false;
            this.MyItemsGridView.AllowUserToDeleteRows = false;
            this.MyItemsGridView.AllowUserToResizeRows = false;
            this.MyItemsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.MyItemsGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.MyItemsGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.MyItemsGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MyItemsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.MyItemsGridView.ColumnHeadersHeight = 30;
            this.MyItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.MyItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.CountColumn,
            this.ItemTypeColumn,
            this.HidenMarketHashNameColumn});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MyItemsGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.MyItemsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyItemsGridView.EnableHeadersVisualStyles = false;
            this.MyItemsGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.MyItemsGridView.Location = new System.Drawing.Point(3, 16);
            this.MyItemsGridView.Name = "MyItemsGridView";
            this.MyItemsGridView.RowHeadersVisible = false;
            this.MyItemsGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.MyItemsGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MyItemsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MyItemsGridView.Size = new System.Drawing.Size(288, 271);
            this.MyItemsGridView.TabIndex = 7;
            this.MyItemsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemsGridView_CellContentClick);
            this.MyItemsGridView.SelectionChanged += new System.EventHandler(this.ItemsGridView_SelectionChanged);
            // 
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.NameColumn.FillWeight = 150F;
            this.NameColumn.Frozen = true;
            this.NameColumn.HeaderText = "Item name";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.Width = 150;
            // 
            // CountColumn
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CountColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.CountColumn.FillWeight = 40F;
            this.CountColumn.HeaderText = "#";
            this.CountColumn.MinimumWidth = 40;
            this.CountColumn.Name = "CountColumn";
            this.CountColumn.ReadOnly = true;
            // 
            // ItemTypeColumn
            // 
            this.ItemTypeColumn.FillWeight = 150F;
            this.ItemTypeColumn.HeaderText = "Type";
            this.ItemTypeColumn.Name = "ItemTypeColumn";
            this.ItemTypeColumn.ReadOnly = true;
            // 
            // HidenMarketHashNameColumn
            // 
            this.HidenMarketHashNameColumn.HeaderText = "HidenMarketHashNameColumn";
            this.HidenMarketHashNameColumn.Name = "HidenMarketHashNameColumn";
            this.HidenMarketHashNameColumn.Visible = false;
            // 
            // ItemDescriptionGroupBox
            // 
            this.ItemDescriptionGroupBox.Controls.Add(this.ItemDescriptionTextBox);
            this.ItemDescriptionGroupBox.Controls.Add(this.ItemNameLable);
            this.ItemDescriptionGroupBox.Controls.Add(this.ItemImageBox);
            this.ItemDescriptionGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.ItemDescriptionGroupBox.Location = new System.Drawing.Point(603, 58);
            this.ItemDescriptionGroupBox.Name = "ItemDescriptionGroupBox";
            this.ItemDescriptionGroupBox.Size = new System.Drawing.Size(279, 286);
            this.ItemDescriptionGroupBox.TabIndex = 15;
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
            this.ItemDescriptionTextBox.Size = new System.Drawing.Size(273, 142);
            this.ItemDescriptionTextBox.TabIndex = 11;
            this.ItemDescriptionTextBox.Text = "";
            // 
            // ItemNameLable
            // 
            this.ItemNameLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ItemNameLable.Location = new System.Drawing.Point(150, 15);
            this.ItemNameLable.Name = "ItemNameLable";
            this.ItemNameLable.Size = new System.Drawing.Size(120, 120);
            this.ItemNameLable.TabIndex = 10;
            // 
            // ItemImageBox
            // 
            this.ItemImageBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.ItemImageBox.BackgroundImage = global::SteamAutoMarket.Properties.Resources.DefaultItem;
            this.ItemImageBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ItemImageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ItemImageBox.Location = new System.Drawing.Point(3, 15);
            this.ItemImageBox.Name = "ItemImageBox";
            this.ItemImageBox.Size = new System.Drawing.Size(139, 120);
            this.ItemImageBox.TabIndex = 8;
            // 
            // ExtraTradeInfoGroupBox
            // 
            this.ExtraTradeInfoGroupBox.Controls.Add(this.ExtraTradeInfoGridView);
            this.ExtraTradeInfoGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.ExtraTradeInfoGroupBox.Location = new System.Drawing.Point(7, 300);
            this.ExtraTradeInfoGroupBox.Name = "ExtraTradeInfoGroupBox";
            this.ExtraTradeInfoGroupBox.Size = new System.Drawing.Size(286, 290);
            this.ExtraTradeInfoGroupBox.TabIndex = 21;
            this.ExtraTradeInfoGroupBox.TabStop = false;
            this.ExtraTradeInfoGroupBox.Text = "Extra trade info";
            // 
            // ExtraTradeInfoGridView
            // 
            this.ExtraTradeInfoGridView.AllowUserToAddRows = false;
            this.ExtraTradeInfoGridView.AllowUserToDeleteRows = false;
            this.ExtraTradeInfoGridView.AllowUserToResizeRows = false;
            this.ExtraTradeInfoGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ExtraTradeInfoGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.ExtraTradeInfoGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.ExtraTradeInfoGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ExtraTradeInfoGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.ExtraTradeInfoGridView.ColumnHeadersHeight = 30;
            this.ExtraTradeInfoGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ExtraTradeInfoGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParameterColumn,
            this.ValueColumn});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ExtraTradeInfoGridView.DefaultCellStyle = dataGridViewCellStyle7;
            this.ExtraTradeInfoGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExtraTradeInfoGridView.EnableHeadersVisualStyles = false;
            this.ExtraTradeInfoGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.ExtraTradeInfoGridView.Location = new System.Drawing.Point(3, 16);
            this.ExtraTradeInfoGridView.Name = "ExtraTradeInfoGridView";
            this.ExtraTradeInfoGridView.RowHeadersVisible = false;
            this.ExtraTradeInfoGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ExtraTradeInfoGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ExtraTradeInfoGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ExtraTradeInfoGridView.Size = new System.Drawing.Size(280, 271);
            this.ExtraTradeInfoGridView.TabIndex = 7;
            // 
            // ParameterColumn
            // 
            this.ParameterColumn.HeaderText = "Parameter";
            this.ParameterColumn.Name = "ParameterColumn";
            this.ParameterColumn.ReadOnly = true;
            // 
            // ValueColumn
            // 
            this.ValueColumn.FillWeight = 149.2386F;
            this.ValueColumn.HeaderText = "Value";
            this.ValueColumn.Name = "ValueColumn";
            this.ValueColumn.ReadOnly = true;
            // 
            // HisItemsGroupBox
            // 
            this.HisItemsGroupBox.Controls.Add(this.HisItemsGridView);
            this.HisItemsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.HisItemsGroupBox.Location = new System.Drawing.Point(303, 296);
            this.HisItemsGroupBox.Name = "HisItemsGroupBox";
            this.HisItemsGroupBox.Size = new System.Drawing.Size(294, 295);
            this.HisItemsGroupBox.TabIndex = 21;
            this.HisItemsGroupBox.TabStop = false;
            this.HisItemsGroupBox.Text = "His items";
            // 
            // HisItemsGridView
            // 
            this.HisItemsGridView.AllowUserToAddRows = false;
            this.HisItemsGridView.AllowUserToDeleteRows = false;
            this.HisItemsGridView.AllowUserToResizeRows = false;
            this.HisItemsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.HisItemsGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.HisItemsGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.HisItemsGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.HisItemsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.HisItemsGridView.ColumnHeadersHeight = 30;
            this.HisItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.HisItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.HidenHisMarketHashNameColumn});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.HisItemsGridView.DefaultCellStyle = dataGridViewCellStyle10;
            this.HisItemsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HisItemsGridView.EnableHeadersVisualStyles = false;
            this.HisItemsGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.HisItemsGridView.Location = new System.Drawing.Point(3, 16);
            this.HisItemsGridView.Name = "HisItemsGridView";
            this.HisItemsGridView.RowHeadersVisible = false;
            this.HisItemsGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.HisItemsGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HisItemsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.HisItemsGridView.Size = new System.Drawing.Size(288, 276);
            this.HisItemsGridView.TabIndex = 7;
            this.HisItemsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemsGridView_CellContentClick);
            this.HisItemsGridView.SelectionChanged += new System.EventHandler(this.ItemsGridView_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.FillWeight = 150F;
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Item name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn2.FillWeight = 40F;
            this.dataGridViewTextBoxColumn2.HeaderText = "#";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 40;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 150F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Type";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // HidenHisMarketHashNameColumn
            // 
            this.HidenHisMarketHashNameColumn.HeaderText = "HidenHisMarketHashNameColumn";
            this.HidenHisMarketHashNameColumn.Name = "HidenHisMarketHashNameColumn";
            this.HidenHisMarketHashNameColumn.Visible = false;
            // 
            // TradeSettingsGroupBox
            // 
            this.TradeSettingsGroupBox.Controls.Add(this.ActiveOnlyCheckBox);
            this.TradeSettingsGroupBox.Controls.Add(this.DescriptionLanguageComboBox);
            this.TradeSettingsGroupBox.Controls.Add(this.PartnerIdLable);
            this.TradeSettingsGroupBox.Controls.Add(this.ReceivedOffersCheckBox);
            this.TradeSettingsGroupBox.Controls.Add(this.SentOffersCheckBox);
            this.TradeSettingsGroupBox.Controls.Add(this.LoadTradesButton);
            this.TradeSettingsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.TradeSettingsGroupBox.Location = new System.Drawing.Point(603, 350);
            this.TradeSettingsGroupBox.Name = "TradeSettingsGroupBox";
            this.TradeSettingsGroupBox.Size = new System.Drawing.Size(279, 133);
            this.TradeSettingsGroupBox.TabIndex = 31;
            this.TradeSettingsGroupBox.TabStop = false;
            this.TradeSettingsGroupBox.Text = "Trade loading settings";
            // 
            // ActiveOnlyCheckBox
            // 
            this.ActiveOnlyCheckBox.AutoSize = true;
            this.ActiveOnlyCheckBox.Checked = true;
            this.ActiveOnlyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ActiveOnlyCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ActiveOnlyCheckBox.Location = new System.Drawing.Point(13, 59);
            this.ActiveOnlyCheckBox.Name = "ActiveOnlyCheckBox";
            this.ActiveOnlyCheckBox.Size = new System.Drawing.Size(91, 20);
            this.ActiveOnlyCheckBox.TabIndex = 32;
            this.ActiveOnlyCheckBox.Text = "ActiveOnly";
            this.toolTip1.SetToolTip(this.ActiveOnlyCheckBox, "Return only trade offers in an active state (offers that haven\'t been accepted ye" +
        "t)");
            this.ActiveOnlyCheckBox.UseVisualStyleBackColor = true;
            this.ActiveOnlyCheckBox.CheckedChanged += new System.EventHandler(this.ActiveOnlyCheckBox_CheckedChanged);
            // 
            // DescriptionLanguageComboBox
            // 
            this.DescriptionLanguageComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.DescriptionLanguageComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DescriptionLanguageComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.DescriptionLanguageComboBox.FormattingEnabled = true;
            this.DescriptionLanguageComboBox.Items.AddRange(new object[] {
            "en_US",
            "de_DE",
            "zh_CN",
            "ko_KR"});
            this.DescriptionLanguageComboBox.Location = new System.Drawing.Point(85, 95);
            this.DescriptionLanguageComboBox.Name = "DescriptionLanguageComboBox";
            this.DescriptionLanguageComboBox.Size = new System.Drawing.Size(68, 21);
            this.DescriptionLanguageComboBox.TabIndex = 31;
            this.DescriptionLanguageComboBox.Text = "en_US";
            this.DescriptionLanguageComboBox.TextChanged += new System.EventHandler(this.DescriptionLanguageComboBox_TextChanged);
            // 
            // PartnerIdLable
            // 
            this.PartnerIdLable.AutoSize = true;
            this.PartnerIdLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.PartnerIdLable.Location = new System.Drawing.Point(10, 96);
            this.PartnerIdLable.Name = "PartnerIdLable";
            this.PartnerIdLable.Size = new System.Drawing.Size(69, 16);
            this.PartnerIdLable.TabIndex = 28;
            this.PartnerIdLable.Text = "Language";
            this.toolTip1.SetToolTip(this.PartnerIdLable, "The language to use when loading item display data");
            // 
            // ReceivedOffersCheckBox
            // 
            this.ReceivedOffersCheckBox.AutoSize = true;
            this.ReceivedOffersCheckBox.Checked = true;
            this.ReceivedOffersCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ReceivedOffersCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ReceivedOffersCheckBox.Location = new System.Drawing.Point(13, 38);
            this.ReceivedOffersCheckBox.Name = "ReceivedOffersCheckBox";
            this.ReceivedOffersCheckBox.Size = new System.Drawing.Size(122, 20);
            this.ReceivedOffersCheckBox.TabIndex = 27;
            this.ReceivedOffersCheckBox.Text = "Received offers";
            this.toolTip1.SetToolTip(this.ReceivedOffersCheckBox, "Return the list of offers you\'ve received from other people");
            this.ReceivedOffersCheckBox.UseVisualStyleBackColor = true;
            this.ReceivedOffersCheckBox.CheckedChanged += new System.EventHandler(this.ReceivedOffersCheckBox_CheckedChanged);
            // 
            // SentOffersCheckBox
            // 
            this.SentOffersCheckBox.AutoSize = true;
            this.SentOffersCheckBox.Checked = true;
            this.SentOffersCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SentOffersCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SentOffersCheckBox.Location = new System.Drawing.Point(14, 18);
            this.SentOffersCheckBox.Name = "SentOffersCheckBox";
            this.SentOffersCheckBox.Size = new System.Drawing.Size(90, 20);
            this.SentOffersCheckBox.TabIndex = 26;
            this.SentOffersCheckBox.Text = "Sent offers";
            this.toolTip1.SetToolTip(this.SentOffersCheckBox, "Return the list of offers you\'ve sent to other people");
            this.SentOffersCheckBox.UseVisualStyleBackColor = true;
            this.SentOffersCheckBox.CheckStateChanged += new System.EventHandler(this.SentOffersCheckBox_CheckStateChanged);
            // 
            // LoadTradesButton
            // 
            this.LoadTradesButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoadTradesButton.FlatAppearance.BorderSize = 0;
            this.LoadTradesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadTradesButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.LoadTradesButton.Image = global::SteamAutoMarket.Properties.Resources.Download;
            this.LoadTradesButton.Location = new System.Drawing.Point(194, 26);
            this.LoadTradesButton.Name = "LoadTradesButton";
            this.LoadTradesButton.Size = new System.Drawing.Size(76, 81);
            this.LoadTradesButton.TabIndex = 25;
            this.toolTip1.SetToolTip(this.LoadTradesButton, "Load trades");
            this.LoadTradesButton.UseVisualStyleBackColor = false;
            this.LoadTradesButton.Click += new System.EventHandler(this.LoadInventoryButton_Click);
            // 
            // SplitterPanel
            // 
            this.SplitterPanel.BackgroundImage = global::SteamAutoMarket.Properties.Resources.NotLogined;
            this.SplitterPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.SplitterPanel.Location = new System.Drawing.Point(706, 3);
            this.SplitterPanel.Name = "SplitterPanel";
            this.SplitterPanel.Size = new System.Drawing.Size(77, 36);
            this.SplitterPanel.TabIndex = 33;
            // 
            // AccountNameLable
            // 
            this.AccountNameLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AccountNameLable.Location = new System.Drawing.Point(674, 39);
            this.AccountNameLable.Name = "AccountNameLable";
            this.AccountNameLable.Size = new System.Drawing.Size(140, 18);
            this.AccountNameLable.TabIndex = 32;
            this.AccountNameLable.Text = "Not logged in";
            this.AccountNameLable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 50000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.UseAnimation = false;
            this.toolTip1.UseFading = false;
            // 
            // ReceivedTradeManageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.Controls.Add(this.SplitterPanel);
            this.Controls.Add(this.AccountNameLable);
            this.Controls.Add(this.TradeSettingsGroupBox);
            this.Controls.Add(this.HisItemsGroupBox);
            this.Controls.Add(this.ItemDescriptionGroupBox);
            this.Controls.Add(this.ExtraTradeInfoGroupBox);
            this.Controls.Add(this.MyItemsGroupBox);
            this.Controls.Add(this.SellGroupBox);
            this.Controls.Add(this.CurrentTradesGroupBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.Name = "ReceivedTradeManageControl";
            this.Size = new System.Drawing.Size(885, 599);
            this.CurrentTradesGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CurrentTradesGridView)).EndInit();
            this.SellGroupBox.ResumeLayout(false);
            this.MyItemsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MyItemsGridView)).EndInit();
            this.ItemDescriptionGroupBox.ResumeLayout(false);
            this.ExtraTradeInfoGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ExtraTradeInfoGridView)).EndInit();
            this.HisItemsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HisItemsGridView)).EndInit();
            this.TradeSettingsGroupBox.ResumeLayout(false);
            this.TradeSettingsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox CurrentTradesGroupBox;
        private System.Windows.Forms.DataGridView CurrentTradesGridView;
        private System.Windows.Forms.GroupBox SellGroupBox;
        private System.Windows.Forms.GroupBox MyItemsGroupBox;
        private System.Windows.Forms.DataGridView MyItemsGridView;
        private System.Windows.Forms.GroupBox ItemDescriptionGroupBox;
        private RichTextBoxWithNoPaint ItemDescriptionTextBox;
        private System.Windows.Forms.Label ItemNameLable;
        private System.Windows.Forms.Panel ItemImageBox;
        private System.Windows.Forms.Button DeclineTradeButton;
        private System.Windows.Forms.Button AcceptTradeButton;
        private System.Windows.Forms.GroupBox ExtraTradeInfoGroupBox;
        private System.Windows.Forms.DataGridView ExtraTradeInfoGridView;
        private System.Windows.Forms.GroupBox HisItemsGroupBox;
        private System.Windows.Forms.DataGridView HisItemsGridView;
        private System.Windows.Forms.GroupBox TradeSettingsGroupBox;
        private System.Windows.Forms.Button LoadTradesButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn HidenMarketHashNameColumn;
        private System.Windows.Forms.CheckBox ReceivedOffersCheckBox;
        private System.Windows.Forms.CheckBox SentOffersCheckBox;
        private System.Windows.Forms.Label PartnerIdLable;
        private System.Windows.Forms.ComboBox DescriptionLanguageComboBox;
        private System.Windows.Forms.CheckBox ActiveOnlyCheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn HidenHisMarketHashNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TradeofferIdColumn;
        private System.Windows.Forms.DataGridViewLinkColumn SenderAccountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TradeSteteColumn;
        private System.Windows.Forms.Panel SplitterPanel;
        private System.Windows.Forms.Label AccountNameLable;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParameterColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueColumn;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

