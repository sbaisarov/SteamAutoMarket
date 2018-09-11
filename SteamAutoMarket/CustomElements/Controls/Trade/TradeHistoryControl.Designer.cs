using SteamAutoMarket.CustomElements.Elements;

namespace SteamAutoMarket.CustomElements.Controls.Trade
{
    using System.Windows.Forms;

    partial class TradeHistoryControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle47 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle48 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle50 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle49 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CurrentTradesGroupBox = new System.Windows.Forms.GroupBox();
            this.CurrentTradesGridView = new System.Windows.Forms.DataGridView();
            this.TradeofferIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SenderAccountColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.TradeSteteColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MyItemsGroupBox = new System.Windows.Forms.GroupBox();
            this.MyItemsGridView = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HidenMarketHashNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDescriptionGroupBox = new System.Windows.Forms.GroupBox();
            this.ItemDescriptionTextBox = new SteamAutoMarket.CustomElements.Elements.RichTextBoxWithNoPaint();
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
            this.TradeIdComboBox = new System.Windows.Forms.ComboBox();
            this.LanguageComboBox = new System.Windows.Forms.ComboBox();
            this.TimeLable = new System.Windows.Forms.Label();
            this.MaxTradesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.SentOffersCheckBox = new System.Windows.Forms.CheckBox();
            this.ReceivedOffersCheckBox = new System.Windows.Forms.CheckBox();
            this.IncludeFailedCheckBox = new System.Windows.Forms.CheckBox();
            this.NavigatingBackCheckBox = new System.Windows.Forms.CheckBox();
            this.TradeIdLabel = new System.Windows.Forms.Label();
            this.MaxTradesLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LoadTradesButton = new System.Windows.Forms.Button();
            this.AccountNameLable = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SplitterPanel = new System.Windows.Forms.Panel();
            this.SteamDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.CurrentTradesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentTradesGridView)).BeginInit();
            this.MyItemsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MyItemsGridView)).BeginInit();
            this.ItemDescriptionGroupBox.SuspendLayout();
            this.ExtraTradeInfoGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExtraTradeInfoGridView)).BeginInit();
            this.HisItemsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HisItemsGridView)).BeginInit();
            this.TradeSettingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxTradesNumericUpDown)).BeginInit();
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
            dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle41.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle41.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle41.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle41.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle41.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CurrentTradesGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle41;
            this.CurrentTradesGridView.ColumnHeadersHeight = 30;
            this.CurrentTradesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.CurrentTradesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TradeofferIdColumn,
            this.SenderAccountColumn,
            this.TradeSteteColumn});
            dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle42.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle42.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle42.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle42.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CurrentTradesGridView.DefaultCellStyle = dataGridViewCellStyle42;
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
            this.CurrentTradesGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CurrentTradesGridViewCellContentClick);
            this.CurrentTradesGridView.SelectionChanged += new System.EventHandler(this.CurrentTradesGridViewSelectionChanged);
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
            dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle43.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle43.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle43.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle43.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle43.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MyItemsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle43;
            this.MyItemsGridView.ColumnHeadersHeight = 30;
            this.MyItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.MyItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.CountColumn,
            this.ItemTypeColumn,
            this.HidenMarketHashNameColumn});
            dataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle45.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle45.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle45.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle45.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MyItemsGridView.DefaultCellStyle = dataGridViewCellStyle45;
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
            this.MyItemsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemsGridViewCellContentClick);
            this.MyItemsGridView.SelectionChanged += new System.EventHandler(this.ItemsGridViewSelectionChanged);
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
            dataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CountColumn.DefaultCellStyle = dataGridViewCellStyle44;
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
            dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle46.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle46.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle46.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle46.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle46.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ExtraTradeInfoGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle46;
            this.ExtraTradeInfoGridView.ColumnHeadersHeight = 30;
            this.ExtraTradeInfoGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ExtraTradeInfoGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParameterColumn,
            this.ValueColumn});
            dataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle47.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle47.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle47.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle47.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle47.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ExtraTradeInfoGridView.DefaultCellStyle = dataGridViewCellStyle47;
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
            dataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle48.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle48.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle48.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle48.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle48.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.HisItemsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle48;
            this.HisItemsGridView.ColumnHeadersHeight = 30;
            this.HisItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.HisItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.HidenHisMarketHashNameColumn});
            dataGridViewCellStyle50.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle50.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle50.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle50.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle50.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle50.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.HisItemsGridView.DefaultCellStyle = dataGridViewCellStyle50;
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
            this.HisItemsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemsGridViewCellContentClick);
            this.HisItemsGridView.SelectionChanged += new System.EventHandler(this.ItemsGridViewSelectionChanged);
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
            dataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle49;
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
            this.TradeSettingsGroupBox.Controls.Add(this.SteamDateTimePicker);
            this.TradeSettingsGroupBox.Controls.Add(this.TradeIdComboBox);
            this.TradeSettingsGroupBox.Controls.Add(this.LanguageComboBox);
            this.TradeSettingsGroupBox.Controls.Add(this.TimeLable);
            this.TradeSettingsGroupBox.Controls.Add(this.MaxTradesNumericUpDown);
            this.TradeSettingsGroupBox.Controls.Add(this.SentOffersCheckBox);
            this.TradeSettingsGroupBox.Controls.Add(this.ReceivedOffersCheckBox);
            this.TradeSettingsGroupBox.Controls.Add(this.IncludeFailedCheckBox);
            this.TradeSettingsGroupBox.Controls.Add(this.NavigatingBackCheckBox);
            this.TradeSettingsGroupBox.Controls.Add(this.TradeIdLabel);
            this.TradeSettingsGroupBox.Controls.Add(this.MaxTradesLabel);
            this.TradeSettingsGroupBox.Controls.Add(this.label1);
            this.TradeSettingsGroupBox.Controls.Add(this.LoadTradesButton);
            this.TradeSettingsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.TradeSettingsGroupBox.Location = new System.Drawing.Point(603, 350);
            this.TradeSettingsGroupBox.Name = "TradeSettingsGroupBox";
            this.TradeSettingsGroupBox.Size = new System.Drawing.Size(279, 241);
            this.TradeSettingsGroupBox.TabIndex = 31;
            this.TradeSettingsGroupBox.TabStop = false;
            this.TradeSettingsGroupBox.Text = "Trade loading settings";
            // 
            // TradeIdComboBox
            // 
            this.TradeIdComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.TradeIdComboBox.DropDownHeight = 150;
            this.TradeIdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TradeIdComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TradeIdComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.TradeIdComboBox.FormattingEnabled = true;
            this.TradeIdComboBox.IntegralHeight = false;
            this.TradeIdComboBox.Location = new System.Drawing.Point(121, 47);
            this.TradeIdComboBox.Name = "TradeIdComboBox";
            this.TradeIdComboBox.Size = new System.Drawing.Size(149, 21);
            this.TradeIdComboBox.TabIndex = 44;
            this.TradeIdComboBox.TextChanged += new System.EventHandler(this.TradeIdComboBoxTextChanged);
            // 
            // LanguageComboBox
            // 
            this.LanguageComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.LanguageComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LanguageComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.LanguageComboBox.FormattingEnabled = true;
            this.LanguageComboBox.Items.AddRange(new object[] {
            "en_US",
            "de_DE",
            "zh_CN",
            "ko_KR"});
            this.LanguageComboBox.Location = new System.Drawing.Point(121, 97);
            this.LanguageComboBox.Name = "LanguageComboBox";
            this.LanguageComboBox.Size = new System.Drawing.Size(69, 21);
            this.LanguageComboBox.TabIndex = 33;
            this.LanguageComboBox.Text = "en_US";
            this.LanguageComboBox.TextChanged += new System.EventHandler(this.LanguageComboBoxTextChanged);
            // 
            // TimeLable
            // 
            this.TimeLable.AutoSize = true;
            this.TimeLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.TimeLable.Location = new System.Drawing.Point(7, 23);
            this.TimeLable.Name = "TimeLable";
            this.TimeLable.Size = new System.Drawing.Size(92, 16);
            this.TimeLable.TabIndex = 36;
            this.TimeLable.Text = "Start after time";
            this.toolTip1.SetToolTip(this.TimeLable, "The time of the last trade shown on the previous page of results. Ignored if \'Sta" +
        "rt after trade id\' is present. Doesnt work with navigating back");
            // 
            // MaxTradesNumericUpDown
            // 
            this.MaxTradesNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.MaxTradesNumericUpDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.MaxTradesNumericUpDown.Location = new System.Drawing.Point(121, 73);
            this.MaxTradesNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxTradesNumericUpDown.Name = "MaxTradesNumericUpDown";
            this.MaxTradesNumericUpDown.Size = new System.Drawing.Size(69, 20);
            this.MaxTradesNumericUpDown.TabIndex = 34;
            this.MaxTradesNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxTradesNumericUpDown.ValueChanged += new System.EventHandler(this.MaxTradesNumericUpDownValueChanged);
            // 
            // SentOffersCheckBox
            // 
            this.SentOffersCheckBox.AutoSize = true;
            this.SentOffersCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SentOffersCheckBox.Location = new System.Drawing.Point(9, 179);
            this.SentOffersCheckBox.Name = "SentOffersCheckBox";
            this.SentOffersCheckBox.Size = new System.Drawing.Size(90, 20);
            this.SentOffersCheckBox.TabIndex = 26;
            this.SentOffersCheckBox.Text = "Sent offers";
            this.toolTip1.SetToolTip(this.SentOffersCheckBox, "Filter only sent offers (result amount will be lower then \'Max trades\' parameter)" +
        "");
            this.SentOffersCheckBox.UseVisualStyleBackColor = true;
            this.SentOffersCheckBox.CheckStateChanged += new System.EventHandler(this.SentOffersCheckBoxCheckStateChanged);
            // 
            // ReceivedOffersCheckBox
            // 
            this.ReceivedOffersCheckBox.AutoSize = true;
            this.ReceivedOffersCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ReceivedOffersCheckBox.Location = new System.Drawing.Point(9, 204);
            this.ReceivedOffersCheckBox.Name = "ReceivedOffersCheckBox";
            this.ReceivedOffersCheckBox.Size = new System.Drawing.Size(122, 20);
            this.ReceivedOffersCheckBox.TabIndex = 27;
            this.ReceivedOffersCheckBox.Text = "Received offers";
            this.toolTip1.SetToolTip(this.ReceivedOffersCheckBox, "Filter only Received offers (result amount will be lower then \'Max trades\' parame" +
        "ter)");
            this.ReceivedOffersCheckBox.UseVisualStyleBackColor = true;
            this.ReceivedOffersCheckBox.CheckedChanged += new System.EventHandler(this.ReceivedOffersCheckBoxCheckedChanged);
            // 
            // IncludeFailedCheckBox
            // 
            this.IncludeFailedCheckBox.AutoSize = true;
            this.IncludeFailedCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IncludeFailedCheckBox.Location = new System.Drawing.Point(9, 154);
            this.IncludeFailedCheckBox.Name = "IncludeFailedCheckBox";
            this.IncludeFailedCheckBox.Size = new System.Drawing.Size(106, 20);
            this.IncludeFailedCheckBox.TabIndex = 43;
            this.IncludeFailedCheckBox.Text = "Include failed";
            this.toolTip1.SetToolTip(this.IncludeFailedCheckBox, "If set, trades in status Failed, RollbackFailed, RollbackAbandoned, and EscrowRol" +
        "lback will be included");
            this.IncludeFailedCheckBox.UseVisualStyleBackColor = true;
            this.IncludeFailedCheckBox.CheckedChanged += new System.EventHandler(this.IncludeFailedCheckBoxCheckedChanged);
            // 
            // NavigatingBackCheckBox
            // 
            this.NavigatingBackCheckBox.AutoSize = true;
            this.NavigatingBackCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NavigatingBackCheckBox.Location = new System.Drawing.Point(9, 129);
            this.NavigatingBackCheckBox.Name = "NavigatingBackCheckBox";
            this.NavigatingBackCheckBox.Size = new System.Drawing.Size(128, 20);
            this.NavigatingBackCheckBox.TabIndex = 42;
            this.NavigatingBackCheckBox.Text = "Navigating back ";
            this.toolTip1.SetToolTip(this.NavigatingBackCheckBox, "Return the previous \'Max trades\' trades before the start time and ID");
            this.NavigatingBackCheckBox.UseVisualStyleBackColor = true;
            this.NavigatingBackCheckBox.CheckedChanged += new System.EventHandler(this.NavigatingBackCheckBoxCheckedChanged);
            // 
            // TradeIdLabel
            // 
            this.TradeIdLabel.AutoSize = true;
            this.TradeIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.TradeIdLabel.Location = new System.Drawing.Point(6, 48);
            this.TradeIdLabel.Name = "TradeIdLabel";
            this.TradeIdLabel.Size = new System.Drawing.Size(112, 16);
            this.TradeIdLabel.TabIndex = 37;
            this.TradeIdLabel.Text = "Start after trade id";
            this.toolTip1.SetToolTip(this.TradeIdLabel, "The tradeid shown on the previous page of results, or the ID of the first trade i" +
        "f navigating back");
            // 
            // MaxTradesLabel
            // 
            this.MaxTradesLabel.AutoSize = true;
            this.MaxTradesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.MaxTradesLabel.Location = new System.Drawing.Point(6, 73);
            this.MaxTradesLabel.Name = "MaxTradesLabel";
            this.MaxTradesLabel.Size = new System.Drawing.Size(74, 16);
            this.MaxTradesLabel.TabIndex = 35;
            this.MaxTradesLabel.Text = "Max trades";
            this.toolTip1.SetToolTip(this.MaxTradesLabel, "The number of trades to return information for (limit 100)");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label1.Location = new System.Drawing.Point(6, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 32;
            this.label1.Text = "Language";
            this.toolTip1.SetToolTip(this.label1, "The language to use when loading item display data");
            // 
            // LoadTradesButton
            // 
            this.LoadTradesButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoadTradesButton.FlatAppearance.BorderSize = 0;
            this.LoadTradesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadTradesButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.LoadTradesButton.Image = global::SteamAutoMarket.Properties.Resources.Download;
            this.LoadTradesButton.Location = new System.Drawing.Point(197, 154);
            this.LoadTradesButton.Name = "LoadTradesButton";
            this.LoadTradesButton.Size = new System.Drawing.Size(76, 81);
            this.LoadTradesButton.TabIndex = 25;
            this.toolTip1.SetToolTip(this.LoadTradesButton, "Load trades");
            this.LoadTradesButton.UseVisualStyleBackColor = false;
            this.LoadTradesButton.Click += new System.EventHandler(this.LoadInventoryButtonClick);
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
            this.toolTip1.AutomaticDelay = 100;
            this.toolTip1.AutoPopDelay = 50000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 20;
            this.toolTip1.UseAnimation = false;
            this.toolTip1.UseFading = false;
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
            // SteamDateTimePicker
            // 
            this.SteamDateTimePicker.CustomFormat = "dd/MM/yyyy";
            this.SteamDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SteamDateTimePicker.Location = new System.Drawing.Point(121, 19);
            this.SteamDateTimePicker.Name = "SteamDateTimePicker";
            this.SteamDateTimePicker.Size = new System.Drawing.Size(149, 20);
            this.SteamDateTimePicker.TabIndex = 45;
            // 
            // TradeHistoryControl
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
            this.Controls.Add(this.CurrentTradesGroupBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.Name = "TradeHistoryControl";
            this.Size = new System.Drawing.Size(885, 599);
            this.CurrentTradesGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CurrentTradesGridView)).EndInit();
            this.MyItemsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MyItemsGridView)).EndInit();
            this.ItemDescriptionGroupBox.ResumeLayout(false);
            this.ExtraTradeInfoGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ExtraTradeInfoGridView)).EndInit();
            this.HisItemsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HisItemsGridView)).EndInit();
            this.TradeSettingsGroupBox.ResumeLayout(false);
            this.TradeSettingsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxTradesNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox CurrentTradesGroupBox;
        private System.Windows.Forms.DataGridView CurrentTradesGridView;
        private System.Windows.Forms.GroupBox MyItemsGroupBox;
        private System.Windows.Forms.DataGridView MyItemsGridView;
        private System.Windows.Forms.GroupBox ItemDescriptionGroupBox;
        private RichTextBoxWithNoPaint ItemDescriptionTextBox;
        private System.Windows.Forms.Label ItemNameLable;
        private System.Windows.Forms.Panel ItemImageBox;
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
        private System.Windows.Forms.Label MaxTradesLabel;
        private System.Windows.Forms.NumericUpDown MaxTradesNumericUpDown;
        private System.Windows.Forms.ComboBox LanguageComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label TradeIdLabel;
        private System.Windows.Forms.Label TimeLable;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox IncludeFailedCheckBox;
        private System.Windows.Forms.CheckBox NavigatingBackCheckBox;
        private System.Windows.Forms.ComboBox TradeIdComboBox;
        private DateTimePicker SteamDateTimePicker;
    }
}
