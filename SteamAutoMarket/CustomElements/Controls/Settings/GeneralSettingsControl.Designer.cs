namespace SteamAutoMarket.CustomElements.Controls.Settings
{
    partial class GeneralSettingsControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralSettingsControl));
            this.AccountsDataGridView = new System.Windows.Forms.DataGridView();
            this.AvatarColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.LoginColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PasswordColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SteamApiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountObjectHidenColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TruePasswordColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountGroupBox = new System.Windows.Forms.GroupBox();
            this.MafilePathGroupBox = new System.Windows.Forms.GroupBox();
            this.BrowseMafilePathButton = new System.Windows.Forms.Button();
            this.MafilePathTextBox = new System.Windows.Forms.TextBox();
            this.PasswordGroupBox = new System.Windows.Forms.GroupBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.LoginGroupBox = new System.Windows.Forms.GroupBox();
            this.LoginTextBox = new System.Windows.Forms.TextBox();
            this.AddAllToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.LoggingLevelGroupBox = new System.Windows.Forms.GroupBox();
            this.LoggingLevelComboBox = new System.Windows.Forms.ComboBox();
            this.SteamApiLinkLable = new System.Windows.Forms.LinkLabel();
            this.AvrPriceGroupBox = new System.Windows.Forms.GroupBox();
            this.AveragePriceDaysNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.DeleteAccountButton = new System.Windows.Forms.Button();
            this.EditAccountButton = new System.Windows.Forms.Button();
            this.AddNewAccountButton = new System.Windows.Forms.Button();
            this.LoginButton = new System.Windows.Forms.Button();
            this.Confirm2FACountGroupBox = new System.Windows.Forms.GroupBox();
            this.Confirm2FANumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SteamApiGroupBox = new System.Windows.Forms.GroupBox();
            this.SteamApiTextBox = new System.Windows.Forms.TextBox();
            this.LogGroupBox = new System.Windows.Forms.GroupBox();
            this.LogTextBox = new System.Windows.Forms.RichTextBox();
            this.SettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.SessionRefreshCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.AccountsDataGridView)).BeginInit();
            this.AccountGroupBox.SuspendLayout();
            this.MafilePathGroupBox.SuspendLayout();
            this.PasswordGroupBox.SuspendLayout();
            this.LoginGroupBox.SuspendLayout();
            this.LoggingLevelGroupBox.SuspendLayout();
            this.AvrPriceGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AveragePriceDaysNumericUpDown)).BeginInit();
            this.Confirm2FACountGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Confirm2FANumericUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SteamApiGroupBox.SuspendLayout();
            this.LogGroupBox.SuspendLayout();
            this.SettingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AccountsDataGridView
            // 
            this.AccountsDataGridView.AllowUserToAddRows = false;
            this.AccountsDataGridView.AllowUserToDeleteRows = false;
            this.AccountsDataGridView.AllowUserToResizeColumns = false;
            this.AccountsDataGridView.AllowUserToResizeRows = false;
            this.AccountsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AccountsDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.AccountsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AccountsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.AccountsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AccountsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AvatarColumn,
            this.LoginColumn,
            this.PasswordColumn,
            this.SteamApiColumn,
            this.AccountObjectHidenColumn,
            this.TruePasswordColumn});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.AccountsDataGridView.DefaultCellStyle = dataGridViewCellStyle8;
            this.AccountsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AccountsDataGridView.EnableHeadersVisualStyles = false;
            this.AccountsDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.AccountsDataGridView.Location = new System.Drawing.Point(3, 16);
            this.AccountsDataGridView.MultiSelect = false;
            this.AccountsDataGridView.Name = "AccountsDataGridView";
            this.AccountsDataGridView.ReadOnly = true;
            this.AccountsDataGridView.RowHeadersVisible = false;
            this.AccountsDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.AccountsDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.AccountsDataGridView.RowTemplate.Height = 32;
            this.AccountsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AccountsDataGridView.Size = new System.Drawing.Size(426, 331);
            this.AccountsDataGridView.TabIndex = 7;
            // 
            // AvatarColumn
            // 
            this.AvatarColumn.FillWeight = 0.2428631F;
            this.AvatarColumn.HeaderText = "";
            this.AvatarColumn.Image = ((System.Drawing.Image)(resources.GetObject("AvatarColumn.Image")));
            this.AvatarColumn.MinimumWidth = 32;
            this.AvatarColumn.Name = "AvatarColumn";
            this.AvatarColumn.ReadOnly = true;
            this.AvatarColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // LoginColumn
            // 
            this.LoginColumn.FillWeight = 119F;
            this.LoginColumn.HeaderText = "Login";
            this.LoginColumn.Name = "LoginColumn";
            this.LoginColumn.ReadOnly = true;
            this.LoginColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PasswordColumn
            // 
            this.PasswordColumn.FillWeight = 119F;
            this.PasswordColumn.HeaderText = "Password";
            this.PasswordColumn.Name = "PasswordColumn";
            this.PasswordColumn.ReadOnly = true;
            this.PasswordColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SteamApiColumn
            // 
            this.SteamApiColumn.FillWeight = 119F;
            this.SteamApiColumn.HeaderText = "STEAM API";
            this.SteamApiColumn.Name = "SteamApiColumn";
            this.SteamApiColumn.ReadOnly = true;
            this.SteamApiColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AccountObjectHidenColumn
            // 
            this.AccountObjectHidenColumn.FillWeight = 2F;
            this.AccountObjectHidenColumn.HeaderText = "AccountObjectHidenColumn";
            this.AccountObjectHidenColumn.MinimumWidth = 2;
            this.AccountObjectHidenColumn.Name = "AccountObjectHidenColumn";
            this.AccountObjectHidenColumn.ReadOnly = true;
            this.AccountObjectHidenColumn.Visible = false;
            // 
            // TruePasswordColumn
            // 
            this.TruePasswordColumn.HeaderText = "TruePassword";
            this.TruePasswordColumn.Name = "TruePasswordColumn";
            this.TruePasswordColumn.ReadOnly = true;
            this.TruePasswordColumn.Visible = false;
            // 
            // AccountGroupBox
            // 
            this.AccountGroupBox.Controls.Add(this.AccountsDataGridView);
            this.AccountGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.AccountGroupBox.Location = new System.Drawing.Point(7, 3);
            this.AccountGroupBox.Name = "AccountGroupBox";
            this.AccountGroupBox.Size = new System.Drawing.Size(432, 350);
            this.AccountGroupBox.TabIndex = 8;
            this.AccountGroupBox.TabStop = false;
            this.AccountGroupBox.Text = "Steam Accounts";
            // 
            // MafilePathGroupBox
            // 
            this.MafilePathGroupBox.Controls.Add(this.BrowseMafilePathButton);
            this.MafilePathGroupBox.Controls.Add(this.MafilePathTextBox);
            this.MafilePathGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MafilePathGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.MafilePathGroupBox.Location = new System.Drawing.Point(183, 19);
            this.MafilePathGroupBox.Name = "MafilePathGroupBox";
            this.MafilePathGroupBox.Size = new System.Drawing.Size(169, 39);
            this.MafilePathGroupBox.TabIndex = 10;
            this.MafilePathGroupBox.TabStop = false;
            this.MafilePathGroupBox.Text = "Mafile path";
            // 
            // BrowseMafilePathButton
            // 
            this.BrowseMafilePathButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BrowseMafilePathButton.FlatAppearance.BorderSize = 0;
            this.BrowseMafilePathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BrowseMafilePathButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.BrowseMafilePathButton.Image = global::SteamAutoMarket.Properties.Resources.BrowseFolders;
            this.BrowseMafilePathButton.Location = new System.Drawing.Point(135, 11);
            this.BrowseMafilePathButton.Name = "BrowseMafilePathButton";
            this.BrowseMafilePathButton.Size = new System.Drawing.Size(29, 25);
            this.BrowseMafilePathButton.TabIndex = 11;
            this.AddAllToolTip.SetToolTip(this.BrowseMafilePathButton, "Mafile path selecting dialog");
            this.BrowseMafilePathButton.UseVisualStyleBackColor = true;
            this.BrowseMafilePathButton.Click += new System.EventHandler(this.BrowseMafilePath_Click);
            // 
            // MafilePathTextBox
            // 
            this.MafilePathTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.MafilePathTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.MafilePathTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.MafilePathTextBox.Location = new System.Drawing.Point(3, 16);
            this.MafilePathTextBox.Name = "MafilePathTextBox";
            this.MafilePathTextBox.Size = new System.Drawing.Size(130, 20);
            this.MafilePathTextBox.TabIndex = 2;
            // 
            // PasswordGroupBox
            // 
            this.PasswordGroupBox.Controls.Add(this.PasswordTextBox);
            this.PasswordGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PasswordGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.PasswordGroupBox.Location = new System.Drawing.Point(7, 69);
            this.PasswordGroupBox.Name = "PasswordGroupBox";
            this.PasswordGroupBox.Size = new System.Drawing.Size(169, 39);
            this.PasswordGroupBox.TabIndex = 10;
            this.PasswordGroupBox.TabStop = false;
            this.PasswordGroupBox.Text = "Password";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.PasswordTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PasswordTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.PasswordTextBox.Location = new System.Drawing.Point(3, 16);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(163, 20);
            this.PasswordTextBox.TabIndex = 3;
            // 
            // LoginGroupBox
            // 
            this.LoginGroupBox.Controls.Add(this.LoginTextBox);
            this.LoginGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoginGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.LoginGroupBox.Location = new System.Drawing.Point(7, 19);
            this.LoginGroupBox.Name = "LoginGroupBox";
            this.LoginGroupBox.Size = new System.Drawing.Size(169, 39);
            this.LoginGroupBox.TabIndex = 9;
            this.LoginGroupBox.TabStop = false;
            this.LoginGroupBox.Text = "Login";
            // 
            // LoginTextBox
            // 
            this.LoginTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.LoginTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoginTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.LoginTextBox.Location = new System.Drawing.Point(3, 16);
            this.LoginTextBox.Name = "LoginTextBox";
            this.LoginTextBox.Size = new System.Drawing.Size(163, 20);
            this.LoginTextBox.TabIndex = 1;
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
            // LoggingLevelGroupBox
            // 
            this.LoggingLevelGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.LoggingLevelGroupBox.Controls.Add(this.LoggingLevelComboBox);
            this.LoggingLevelGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoggingLevelGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.LoggingLevelGroupBox.Location = new System.Drawing.Point(148, 19);
            this.LoggingLevelGroupBox.Name = "LoggingLevelGroupBox";
            this.LoggingLevelGroupBox.Size = new System.Drawing.Size(133, 40);
            this.LoggingLevelGroupBox.TabIndex = 10;
            this.LoggingLevelGroupBox.TabStop = false;
            this.LoggingLevelGroupBox.Text = "Logging detailing";
            this.AddAllToolTip.SetToolTip(this.LoggingLevelGroupBox, "With large amounts of work, disabling logs will speed up the program");
            // 
            // LoggingLevelComboBox
            // 
            this.LoggingLevelComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.LoggingLevelComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoggingLevelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LoggingLevelComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoggingLevelComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.LoggingLevelComboBox.FormattingEnabled = true;
            this.LoggingLevelComboBox.Items.AddRange(new object[] {
            "Debug",
            "Info + Errors",
            "Errors",
            "None"});
            this.LoggingLevelComboBox.Location = new System.Drawing.Point(3, 16);
            this.LoggingLevelComboBox.Name = "LoggingLevelComboBox";
            this.LoggingLevelComboBox.Size = new System.Drawing.Size(127, 21);
            this.LoggingLevelComboBox.TabIndex = 0;
            this.AddAllToolTip.SetToolTip(this.LoggingLevelComboBox, "With large amounts of work, disabling logs will speed up the program");
            this.LoggingLevelComboBox.SelectedIndexChanged += new System.EventHandler(this.LoggingLevelComboBox_SelectedIndexChanged);
            // 
            // SteamApiLinkLable
            // 
            this.SteamApiLinkLable.ActiveLinkColor = System.Drawing.Color.Aqua;
            this.SteamApiLinkLable.AutoSize = true;
            this.SteamApiLinkLable.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.SteamApiLinkLable.Location = new System.Drawing.Point(68, 0);
            this.SteamApiLinkLable.Name = "SteamApiLinkLable";
            this.SteamApiLinkLable.Size = new System.Drawing.Size(39, 13);
            this.SteamApiLinkLable.TabIndex = 16;
            this.SteamApiLinkLable.TabStop = true;
            this.SteamApiLinkLable.Text = "(Link)";
            this.AddAllToolTip.SetToolTip(this.SteamApiLinkLable, "Open Steam API page on browser");
            this.SteamApiLinkLable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SteamApiLinkLable_LinkClicked);
            // 
            // AvrPriceGroupBox
            // 
            this.AvrPriceGroupBox.Controls.Add(this.AveragePriceDaysNumericUpDown);
            this.AvrPriceGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AvrPriceGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.AvrPriceGroupBox.Location = new System.Drawing.Point(7, 18);
            this.AvrPriceGroupBox.Name = "AvrPriceGroupBox";
            this.AvrPriceGroupBox.Size = new System.Drawing.Size(133, 40);
            this.AvrPriceGroupBox.TabIndex = 16;
            this.AvrPriceGroupBox.TabStop = false;
            this.AvrPriceGroupBox.Text = "Avr price (days)";
            this.AddAllToolTip.SetToolTip(this.AvrPriceGroupBox, "Count of days from today on which average price will be calculated");
            // 
            // AveragePriceDaysNumericUpDown
            // 
            this.AveragePriceDaysNumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.AveragePriceDaysNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AveragePriceDaysNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AveragePriceDaysNumericUpDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.AveragePriceDaysNumericUpDown.Location = new System.Drawing.Point(3, 16);
            this.AveragePriceDaysNumericUpDown.Name = "AveragePriceDaysNumericUpDown";
            this.AveragePriceDaysNumericUpDown.Size = new System.Drawing.Size(127, 20);
            this.AveragePriceDaysNumericUpDown.TabIndex = 0;
            this.AveragePriceDaysNumericUpDown.ValueChanged += new System.EventHandler(this.AveragePriceDaysNumericUpDown_ValueChanged);
            // 
            // DeleteAccountButton
            // 
            this.DeleteAccountButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteAccountButton.FlatAppearance.BorderSize = 0;
            this.DeleteAccountButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteAccountButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.DeleteAccountButton.Image = global::SteamAutoMarket.Properties.Resources.CancelButton;
            this.DeleteAccountButton.Location = new System.Drawing.Point(66, 116);
            this.DeleteAccountButton.Name = "DeleteAccountButton";
            this.DeleteAccountButton.Size = new System.Drawing.Size(47, 40);
            this.DeleteAccountButton.TabIndex = 99;
            this.AddAllToolTip.SetToolTip(this.DeleteAccountButton, "Remove the selected account from the list");
            this.DeleteAccountButton.UseVisualStyleBackColor = true;
            this.DeleteAccountButton.Click += new System.EventHandler(this.DeleteAccountButton_Click);
            // 
            // EditAccountButton
            // 
            this.EditAccountButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EditAccountButton.FlatAppearance.BorderSize = 0;
            this.EditAccountButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditAccountButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.EditAccountButton.Image = global::SteamAutoMarket.Properties.Resources.EditButton;
            this.EditAccountButton.Location = new System.Drawing.Point(10, 116);
            this.EditAccountButton.Name = "EditAccountButton";
            this.EditAccountButton.Size = new System.Drawing.Size(47, 40);
            this.EditAccountButton.TabIndex = 99;
            this.AddAllToolTip.SetToolTip(this.EditAccountButton, "Edit selected account");
            this.EditAccountButton.UseVisualStyleBackColor = true;
            this.EditAccountButton.Click += new System.EventHandler(this.EditAccountButton_Click);
            // 
            // AddNewAccountButton
            // 
            this.AddNewAccountButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddNewAccountButton.FlatAppearance.BorderSize = 0;
            this.AddNewAccountButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddNewAccountButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.AddNewAccountButton.Image = global::SteamAutoMarket.Properties.Resources.AddButton;
            this.AddNewAccountButton.Location = new System.Drawing.Point(366, 49);
            this.AddNewAccountButton.Name = "AddNewAccountButton";
            this.AddNewAccountButton.Size = new System.Drawing.Size(47, 40);
            this.AddNewAccountButton.TabIndex = 99;
            this.AddAllToolTip.SetToolTip(this.AddNewAccountButton, "Add account to the list");
            this.AddNewAccountButton.UseVisualStyleBackColor = true;
            this.AddNewAccountButton.Click += new System.EventHandler(this.AddNewAccountButton_Click);
            // 
            // LoginButton
            // 
            this.LoginButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoginButton.FlatAppearance.BorderSize = 0;
            this.LoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoginButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.LoginButton.Image = global::SteamAutoMarket.Properties.Resources.LoginButton;
            this.LoginButton.Location = new System.Drawing.Point(-1, 533);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(885, 66);
            this.LoginButton.TabIndex = 5;
            this.AddAllToolTip.SetToolTip(this.LoginButton, "Login using selected account");
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // Confirm2FACountGroupBox
            // 
            this.Confirm2FACountGroupBox.Controls.Add(this.Confirm2FANumericUpDown);
            this.Confirm2FACountGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Confirm2FACountGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.Confirm2FACountGroupBox.Location = new System.Drawing.Point(289, 19);
            this.Confirm2FACountGroupBox.Name = "Confirm2FACountGroupBox";
            this.Confirm2FACountGroupBox.Size = new System.Drawing.Size(133, 40);
            this.Confirm2FACountGroupBox.TabIndex = 17;
            this.Confirm2FACountGroupBox.TabStop = false;
            this.Confirm2FACountGroupBox.Text = "Confirm 2FA (count)";
            this.AddAllToolTip.SetToolTip(this.Confirm2FACountGroupBox, "Count of days from today on which average price will be calculated");
            // 
            // Confirm2FANumericUpDown
            // 
            this.Confirm2FANumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.Confirm2FANumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Confirm2FANumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Confirm2FANumericUpDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.Confirm2FANumericUpDown.Location = new System.Drawing.Point(3, 16);
            this.Confirm2FANumericUpDown.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.Confirm2FANumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Confirm2FANumericUpDown.Name = "Confirm2FANumericUpDown";
            this.Confirm2FANumericUpDown.Size = new System.Drawing.Size(127, 20);
            this.Confirm2FANumericUpDown.TabIndex = 0;
            this.AddAllToolTip.SetToolTip(this.Confirm2FANumericUpDown, "The number of 2FA requests after which all the requests will be confirmed");
            this.Confirm2FANumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Confirm2FANumericUpDown.ValueChanged += new System.EventHandler(this.Confirm2FANumericUpDown_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DeleteAccountButton);
            this.groupBox1.Controls.Add(this.EditAccountButton);
            this.groupBox1.Controls.Add(this.AddNewAccountButton);
            this.groupBox1.Controls.Add(this.SteamApiGroupBox);
            this.groupBox1.Controls.Add(this.LoginGroupBox);
            this.groupBox1.Controls.Add(this.PasswordGroupBox);
            this.groupBox1.Controls.Add(this.MafilePathGroupBox);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.groupBox1.Location = new System.Drawing.Point(9, 359);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(428, 174);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Accounts Settings";
            // 
            // SteamApiGroupBox
            // 
            this.SteamApiGroupBox.Controls.Add(this.SteamApiLinkLable);
            this.SteamApiGroupBox.Controls.Add(this.SteamApiTextBox);
            this.SteamApiGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SteamApiGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.SteamApiGroupBox.Location = new System.Drawing.Point(183, 69);
            this.SteamApiGroupBox.Name = "SteamApiGroupBox";
            this.SteamApiGroupBox.Size = new System.Drawing.Size(169, 39);
            this.SteamApiGroupBox.TabIndex = 11;
            this.SteamApiGroupBox.TabStop = false;
            this.SteamApiGroupBox.Text = "Steam API        ";
            // 
            // SteamApiTextBox
            // 
            this.SteamApiTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.SteamApiTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SteamApiTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.SteamApiTextBox.Location = new System.Drawing.Point(3, 16);
            this.SteamApiTextBox.Name = "SteamApiTextBox";
            this.SteamApiTextBox.Size = new System.Drawing.Size(163, 20);
            this.SteamApiTextBox.TabIndex = 4;
            // 
            // LogGroupBox
            // 
            this.LogGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.LogGroupBox.Controls.Add(this.LogTextBox);
            this.LogGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.LogGroupBox.Location = new System.Drawing.Point(444, 3);
            this.LogGroupBox.Name = "LogGroupBox";
            this.LogGroupBox.Size = new System.Drawing.Size(432, 350);
            this.LogGroupBox.TabIndex = 9;
            this.LogGroupBox.TabStop = false;
            this.LogGroupBox.Text = "Logging";
            // 
            // LogTextBox
            // 
            this.LogTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.LogTextBox.DetectUrls = false;
            this.LogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LogTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.LogTextBox.Location = new System.Drawing.Point(3, 16);
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.LogTextBox.Size = new System.Drawing.Size(426, 331);
            this.LogTextBox.TabIndex = 0;
            this.LogTextBox.Text = "";
            this.LogTextBox.TextChanged += new System.EventHandler(this.LogTextBox_TextChanged);
            // 
            // SettingsGroupBox
            // 
            this.SettingsGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.SettingsGroupBox.Controls.Add(this.Confirm2FACountGroupBox);
            this.SettingsGroupBox.Controls.Add(this.AvrPriceGroupBox);
            this.SettingsGroupBox.Controls.Add(this.UpdateButton);
            this.SettingsGroupBox.Controls.Add(this.LoggingLevelGroupBox);
            this.SettingsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.SettingsGroupBox.Location = new System.Drawing.Point(444, 359);
            this.SettingsGroupBox.Name = "SettingsGroupBox";
            this.SettingsGroupBox.Size = new System.Drawing.Size(428, 174);
            this.SettingsGroupBox.TabIndex = 13;
            this.SettingsGroupBox.TabStop = false;
            this.SettingsGroupBox.Text = "Settings";
            // 
            // UpdateButton
            // 
            this.UpdateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.UpdateButton.Enabled = false;
            this.UpdateButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UpdateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UpdateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.UpdateButton.Location = new System.Drawing.Point(112, 130);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(203, 33);
            this.UpdateButton.TabIndex = 15;
            this.UpdateButton.Text = "Check for updates";
            this.UpdateButton.UseVisualStyleBackColor = false;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.FillWeight = 0.2428631F;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn1.Image")));
            this.dataGridViewImageColumn1.MinimumWidth = 32;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn1.Width = 32;
            // 
            // SessionRefreshCheckBox
            // 
            this.SessionRefreshCheckBox.AutoSize = true;
            this.SessionRefreshCheckBox.Location = new System.Drawing.Point(740, 582);
            this.SessionRefreshCheckBox.Name = "SessionRefreshCheckBox";
            this.SessionRefreshCheckBox.Size = new System.Drawing.Size(147, 17);
            this.SessionRefreshCheckBox.TabIndex = 18;
            this.SessionRefreshCheckBox.Text = "Force session refresh";
            this.SessionRefreshCheckBox.UseVisualStyleBackColor = true;
            // 
            // GeneralSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Controls.Add(this.SessionRefreshCheckBox);
            this.Controls.Add(this.SettingsGroupBox);
            this.Controls.Add(this.LogGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.AccountGroupBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.Name = "GeneralSettingsControl";
            this.Size = new System.Drawing.Size(885, 599);
            ((System.ComponentModel.ISupportInitialize)(this.AccountsDataGridView)).EndInit();
            this.AccountGroupBox.ResumeLayout(false);
            this.MafilePathGroupBox.ResumeLayout(false);
            this.MafilePathGroupBox.PerformLayout();
            this.PasswordGroupBox.ResumeLayout(false);
            this.PasswordGroupBox.PerformLayout();
            this.LoginGroupBox.ResumeLayout(false);
            this.LoginGroupBox.PerformLayout();
            this.LoggingLevelGroupBox.ResumeLayout(false);
            this.AvrPriceGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AveragePriceDaysNumericUpDown)).EndInit();
            this.Confirm2FACountGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Confirm2FANumericUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.SteamApiGroupBox.ResumeLayout(false);
            this.SteamApiGroupBox.PerformLayout();
            this.LogGroupBox.ResumeLayout(false);
            this.SettingsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView AccountsDataGridView;
        private System.Windows.Forms.GroupBox AccountGroupBox;
        private System.Windows.Forms.Button EditAccountButton;
        private System.Windows.Forms.GroupBox MafilePathGroupBox;
        private System.Windows.Forms.Button BrowseMafilePathButton;
        private System.Windows.Forms.TextBox MafilePathTextBox;
        private System.Windows.Forms.GroupBox PasswordGroupBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.GroupBox LoginGroupBox;
        private System.Windows.Forms.TextBox LoginTextBox;
        private System.Windows.Forms.ToolTip AddAllToolTip;
        private System.Windows.Forms.Button AddNewAccountButton;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.GroupBox SteamApiGroupBox;
        private System.Windows.Forms.TextBox SteamApiTextBox;
        private System.Windows.Forms.Button DeleteAccountButton;
        private System.Windows.Forms.GroupBox LogGroupBox;
        public System.Windows.Forms.RichTextBox LogTextBox;
        private System.Windows.Forms.GroupBox SettingsGroupBox;
        private System.Windows.Forms.GroupBox LoggingLevelGroupBox;
        private System.Windows.Forms.Button UpdateButton;
        public System.Windows.Forms.ComboBox LoggingLevelComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewImageColumn AvatarColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoginColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PasswordColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountObjectHidenColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TruePasswordColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SteamApiColumn;
        private System.Windows.Forms.LinkLabel SteamApiLinkLable;
        private System.Windows.Forms.GroupBox AvrPriceGroupBox;
        private System.Windows.Forms.NumericUpDown AveragePriceDaysNumericUpDown;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.GroupBox Confirm2FACountGroupBox;
        private System.Windows.Forms.NumericUpDown Confirm2FANumericUpDown;
        private System.Windows.Forms.CheckBox SessionRefreshCheckBox;
    }
}
