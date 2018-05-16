namespace autotrade.CustomElements {
    partial class SettingsControl {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsControl));
            this.AccountsDataGridView = new System.Windows.Forms.DataGridView();
            this.AvatarColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.LoginColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PasswordColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountObjectHidenColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountGroupBox = new System.Windows.Forms.GroupBox();
            this.AddNewAccountButton = new System.Windows.Forms.Button();
            this.MafilePathGroupBox = new System.Windows.Forms.GroupBox();
            this.BrowseMafilePathButton = new System.Windows.Forms.Button();
            this.MafilePathTextBox = new System.Windows.Forms.TextBox();
            this.PasswordGroupBox = new System.Windows.Forms.GroupBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.LoginGroupBox = new System.Windows.Forms.GroupBox();
            this.LoginTextBox = new System.Windows.Forms.TextBox();
            this.DeleteAccountButton = new System.Windows.Forms.Button();
            this.AddAllToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.AccountsDataGridView)).BeginInit();
            this.AccountGroupBox.SuspendLayout();
            this.MafilePathGroupBox.SuspendLayout();
            this.PasswordGroupBox.SuspendLayout();
            this.LoginGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AccountsDataGridView
            // 
            this.AccountsDataGridView.AllowUserToAddRows = false;
            this.AccountsDataGridView.AllowUserToDeleteRows = false;
            this.AccountsDataGridView.AllowUserToResizeColumns = false;
            this.AccountsDataGridView.AllowUserToResizeRows = false;
            this.AccountsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AccountsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AccountsDataGridView.ColumnHeadersVisible = false;
            this.AccountsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AvatarColumn,
            this.LoginColumn,
            this.PasswordColumn,
            this.AccountObjectHidenColumn});
            this.AccountsDataGridView.Location = new System.Drawing.Point(6, 19);
            this.AccountsDataGridView.MultiSelect = false;
            this.AccountsDataGridView.Name = "AccountsDataGridView";
            this.AccountsDataGridView.ReadOnly = true;
            this.AccountsDataGridView.RowHeadersVisible = false;
            this.AccountsDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.AccountsDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.AccountsDataGridView.RowTemplate.Height = 34;
            this.AccountsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AccountsDataGridView.Size = new System.Drawing.Size(290, 355);
            this.AccountsDataGridView.TabIndex = 7;
            // 
            // AvatarColumn
            // 
            this.AvatarColumn.FillWeight = 0.6443831F;
            this.AvatarColumn.HeaderText = "AvatarColumn";
            this.AvatarColumn.Image = ((System.Drawing.Image)(resources.GetObject("AvatarColumn.Image")));
            this.AvatarColumn.MinimumWidth = 32;
            this.AvatarColumn.Name = "AvatarColumn";
            this.AvatarColumn.ReadOnly = true;
            this.AvatarColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // LoginColumn
            // 
            this.LoginColumn.FillWeight = 258F;
            this.LoginColumn.HeaderText = "LoginColumn";
            this.LoginColumn.Name = "LoginColumn";
            this.LoginColumn.ReadOnly = true;
            // 
            // PasswordColumn
            // 
            this.PasswordColumn.FillWeight = 258F;
            this.PasswordColumn.HeaderText = "PasswordColumn";
            this.PasswordColumn.Name = "PasswordColumn";
            this.PasswordColumn.ReadOnly = true;
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
            // AccountGroupBox
            // 
            this.AccountGroupBox.Controls.Add(this.AddNewAccountButton);
            this.AccountGroupBox.Controls.Add(this.MafilePathGroupBox);
            this.AccountGroupBox.Controls.Add(this.PasswordGroupBox);
            this.AccountGroupBox.Controls.Add(this.LoginGroupBox);
            this.AccountGroupBox.Controls.Add(this.DeleteAccountButton);
            this.AccountGroupBox.Controls.Add(this.AccountsDataGridView);
            this.AccountGroupBox.Location = new System.Drawing.Point(19, 12);
            this.AccountGroupBox.Name = "AccountGroupBox";
            this.AccountGroupBox.Size = new System.Drawing.Size(302, 488);
            this.AccountGroupBox.TabIndex = 8;
            this.AccountGroupBox.TabStop = false;
            this.AccountGroupBox.Text = "Аккаунты";
            // 
            // AddNewAccountButton
            // 
            this.AddNewAccountButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddNewAccountButton.ForeColor = System.Drawing.Color.Silver;
            this.AddNewAccountButton.Image = ((System.Drawing.Image)(resources.GetObject("AddNewAccountButton.Image")));
            this.AddNewAccountButton.Location = new System.Drawing.Point(176, 437);
            this.AddNewAccountButton.Name = "AddNewAccountButton";
            this.AddNewAccountButton.Size = new System.Drawing.Size(40, 40);
            this.AddNewAccountButton.TabIndex = 3;
            this.AddAllToolTip.SetToolTip(this.AddNewAccountButton, "Добавить аккаунт в список");
            this.AddNewAccountButton.UseVisualStyleBackColor = true;
            this.AddNewAccountButton.Click += new System.EventHandler(this.AddNewAccountButton_Click);
            // 
            // MafilePathGroupBox
            // 
            this.MafilePathGroupBox.Controls.Add(this.BrowseMafilePathButton);
            this.MafilePathGroupBox.Controls.Add(this.MafilePathTextBox);
            this.MafilePathGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MafilePathGroupBox.Location = new System.Drawing.Point(159, 389);
            this.MafilePathGroupBox.Name = "MafilePathGroupBox";
            this.MafilePathGroupBox.Size = new System.Drawing.Size(137, 37);
            this.MafilePathGroupBox.TabIndex = 10;
            this.MafilePathGroupBox.TabStop = false;
            this.MafilePathGroupBox.Text = "Путь к мафайлу";
            // 
            // BrowseMafilePathButton
            // 
            this.BrowseMafilePathButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BrowseMafilePathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BrowseMafilePathButton.ForeColor = System.Drawing.Color.Silver;
            this.BrowseMafilePathButton.Image = ((System.Drawing.Image)(resources.GetObject("BrowseMafilePathButton.Image")));
            this.BrowseMafilePathButton.Location = new System.Drawing.Point(107, 9);
            this.BrowseMafilePathButton.Name = "BrowseMafilePathButton";
            this.BrowseMafilePathButton.Size = new System.Drawing.Size(25, 25);
            this.BrowseMafilePathButton.TabIndex = 11;
            this.AddAllToolTip.SetToolTip(this.BrowseMafilePathButton, "Выбрать пусть к мафайлу");
            this.BrowseMafilePathButton.UseVisualStyleBackColor = true;
            this.BrowseMafilePathButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // MafilePathTextBox
            // 
            this.MafilePathTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.MafilePathTextBox.Location = new System.Drawing.Point(3, 16);
            this.MafilePathTextBox.Name = "MafilePathTextBox";
            this.MafilePathTextBox.Size = new System.Drawing.Size(101, 20);
            this.MafilePathTextBox.TabIndex = 1;
            // 
            // PasswordGroupBox
            // 
            this.PasswordGroupBox.Controls.Add(this.PasswordTextBox);
            this.PasswordGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PasswordGroupBox.Location = new System.Drawing.Point(7, 437);
            this.PasswordGroupBox.Name = "PasswordGroupBox";
            this.PasswordGroupBox.Size = new System.Drawing.Size(137, 37);
            this.PasswordGroupBox.TabIndex = 10;
            this.PasswordGroupBox.TabStop = false;
            this.PasswordGroupBox.Text = "Пароль";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PasswordTextBox.Location = new System.Drawing.Point(3, 16);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(131, 20);
            this.PasswordTextBox.TabIndex = 2;
            // 
            // LoginGroupBox
            // 
            this.LoginGroupBox.Controls.Add(this.LoginTextBox);
            this.LoginGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoginGroupBox.Location = new System.Drawing.Point(7, 389);
            this.LoginGroupBox.Name = "LoginGroupBox";
            this.LoginGroupBox.Size = new System.Drawing.Size(137, 37);
            this.LoginGroupBox.TabIndex = 9;
            this.LoginGroupBox.TabStop = false;
            this.LoginGroupBox.Text = "Логин";
            // 
            // LoginTextBox
            // 
            this.LoginTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoginTextBox.Location = new System.Drawing.Point(3, 16);
            this.LoginTextBox.Name = "LoginTextBox";
            this.LoginTextBox.Size = new System.Drawing.Size(131, 20);
            this.LoginTextBox.TabIndex = 0;
            // 
            // DeleteAccountButton
            // 
            this.DeleteAccountButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteAccountButton.ForeColor = System.Drawing.Color.Silver;
            this.DeleteAccountButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteAccountButton.Image")));
            this.DeleteAccountButton.Location = new System.Drawing.Point(234, 437);
            this.DeleteAccountButton.Name = "DeleteAccountButton";
            this.DeleteAccountButton.Size = new System.Drawing.Size(40, 40);
            this.DeleteAccountButton.TabIndex = 9;
            this.AddAllToolTip.SetToolTip(this.DeleteAccountButton, "Удалить выбранный аккаунт из списка");
            this.DeleteAccountButton.UseVisualStyleBackColor = true;
            this.DeleteAccountButton.Click += new System.EventHandler(this.DeleteAccountButton_Click);
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
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.Silver;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(287, 518);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(183, 66);
            this.button2.TabIndex = 4;
            this.AddAllToolTip.SetToolTip(this.button2, "Логин под выбранным аккаунтом");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(343, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 374);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки Opskins";
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.AccountGroupBox);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(759, 599);
            ((System.ComponentModel.ISupportInitialize)(this.AccountsDataGridView)).EndInit();
            this.AccountGroupBox.ResumeLayout(false);
            this.MafilePathGroupBox.ResumeLayout(false);
            this.MafilePathGroupBox.PerformLayout();
            this.PasswordGroupBox.ResumeLayout(false);
            this.PasswordGroupBox.PerformLayout();
            this.LoginGroupBox.ResumeLayout(false);
            this.LoginGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView AccountsDataGridView;
        private System.Windows.Forms.GroupBox AccountGroupBox;
        private System.Windows.Forms.Button DeleteAccountButton;
        private System.Windows.Forms.GroupBox MafilePathGroupBox;
        private System.Windows.Forms.Button BrowseMafilePathButton;
        private System.Windows.Forms.TextBox MafilePathTextBox;
        private System.Windows.Forms.GroupBox PasswordGroupBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.GroupBox LoginGroupBox;
        private System.Windows.Forms.TextBox LoginTextBox;
        private System.Windows.Forms.ToolTip AddAllToolTip;
        private System.Windows.Forms.Button AddNewAccountButton;
        private System.Windows.Forms.DataGridViewImageColumn AvatarColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoginColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PasswordColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountObjectHidenColumn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
