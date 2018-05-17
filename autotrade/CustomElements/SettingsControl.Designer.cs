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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsControl));
            this.AccountsDataGridView = new System.Windows.Forms.DataGridView();
            this.AvatarColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.LoginColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PasswordColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpskinsApiColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.EditAccountButton = new System.Windows.Forms.Button();
            this.AddAllToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.DeleteAccountButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OpskinsApiGroupBox = new System.Windows.Forms.GroupBox();
            this.OpskinsApiTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.AccountsDataGridView)).BeginInit();
            this.AccountGroupBox.SuspendLayout();
            this.MafilePathGroupBox.SuspendLayout();
            this.PasswordGroupBox.SuspendLayout();
            this.LoginGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.OpskinsApiGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AccountsDataGridView
            // 
            this.AccountsDataGridView.AllowUserToAddRows = false;
            this.AccountsDataGridView.AllowUserToDeleteRows = false;
            this.AccountsDataGridView.AllowUserToResizeColumns = false;
            this.AccountsDataGridView.AllowUserToResizeRows = false;
            this.AccountsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AccountsDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AccountsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.AccountsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AccountsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AvatarColumn,
            this.LoginColumn,
            this.PasswordColumn,
            this.OpskinsApiColumn,
            this.AccountObjectHidenColumn});
            this.AccountsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AccountsDataGridView.EnableHeadersVisualStyles = false;
            this.AccountsDataGridView.Location = new System.Drawing.Point(3, 16);
            this.AccountsDataGridView.MultiSelect = false;
            this.AccountsDataGridView.Name = "AccountsDataGridView";
            this.AccountsDataGridView.ReadOnly = true;
            this.AccountsDataGridView.RowHeadersVisible = false;
            this.AccountsDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.AccountsDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.AccountsDataGridView.RowTemplate.Height = 34;
            this.AccountsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AccountsDataGridView.Size = new System.Drawing.Size(395, 481);
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
            this.LoginColumn.HeaderText = "Логин";
            this.LoginColumn.Name = "LoginColumn";
            this.LoginColumn.ReadOnly = true;
            // 
            // PasswordColumn
            // 
            this.PasswordColumn.FillWeight = 119F;
            this.PasswordColumn.HeaderText = "Пароль";
            this.PasswordColumn.Name = "PasswordColumn";
            this.PasswordColumn.ReadOnly = true;
            // 
            // OpskinsApiColumn
            // 
            this.OpskinsApiColumn.FillWeight = 119F;
            this.OpskinsApiColumn.HeaderText = "OPSKINS API";
            this.OpskinsApiColumn.Name = "OpskinsApiColumn";
            this.OpskinsApiColumn.ReadOnly = true;
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
            this.AccountGroupBox.Controls.Add(this.AccountsDataGridView);
            this.AccountGroupBox.Location = new System.Drawing.Point(3, 12);
            this.AccountGroupBox.Name = "AccountGroupBox";
            this.AccountGroupBox.Size = new System.Drawing.Size(401, 500);
            this.AccountGroupBox.TabIndex = 8;
            this.AccountGroupBox.TabStop = false;
            this.AccountGroupBox.Text = "Аккаунты";
            // 
            // AddNewAccountButton
            // 
            this.AddNewAccountButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddNewAccountButton.ForeColor = System.Drawing.Color.Silver;
            this.AddNewAccountButton.Image = ((System.Drawing.Image)(resources.GetObject("AddNewAccountButton.Image")));
            this.AddNewAccountButton.Location = new System.Drawing.Point(290, 45);
            this.AddNewAccountButton.Name = "AddNewAccountButton";
            this.AddNewAccountButton.Size = new System.Drawing.Size(40, 40);
            this.AddNewAccountButton.TabIndex = 5;
            this.AddAllToolTip.SetToolTip(this.AddNewAccountButton, "Добавить аккаунт в список");
            this.AddNewAccountButton.UseVisualStyleBackColor = true;
            this.AddNewAccountButton.Click += new System.EventHandler(this.AddNewAccountButton_Click);
            // 
            // MafilePathGroupBox
            // 
            this.MafilePathGroupBox.Controls.Add(this.BrowseMafilePathButton);
            this.MafilePathGroupBox.Controls.Add(this.MafilePathTextBox);
            this.MafilePathGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MafilePathGroupBox.Location = new System.Drawing.Point(149, 19);
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
            this.MafilePathTextBox.TabIndex = 2;
            // 
            // PasswordGroupBox
            // 
            this.PasswordGroupBox.Controls.Add(this.PasswordTextBox);
            this.PasswordGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PasswordGroupBox.Location = new System.Drawing.Point(6, 67);
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
            this.PasswordTextBox.TabIndex = 3;
            // 
            // LoginGroupBox
            // 
            this.LoginGroupBox.Controls.Add(this.LoginTextBox);
            this.LoginGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoginGroupBox.Location = new System.Drawing.Point(6, 19);
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
            this.LoginTextBox.TabIndex = 1;
            // 
            // EditAccountButton
            // 
            this.EditAccountButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditAccountButton.ForeColor = System.Drawing.Color.Silver;
            this.EditAccountButton.Image = ((System.Drawing.Image)(resources.GetObject("EditAccountButton.Image")));
            this.EditAccountButton.Location = new System.Drawing.Point(9, 110);
            this.EditAccountButton.Name = "EditAccountButton";
            this.EditAccountButton.Size = new System.Drawing.Size(40, 40);
            this.EditAccountButton.TabIndex = 6;
            this.AddAllToolTip.SetToolTip(this.EditAccountButton, "Редактировать выбранный аккаунт");
            this.EditAccountButton.UseVisualStyleBackColor = true;
            this.EditAccountButton.Click += new System.EventHandler(this.EditAccountButton_Click);
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
            this.button2.TabIndex = 0;
            this.AddAllToolTip.SetToolTip(this.button2, "Логин под выбранным аккаунтом");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // DeleteAccountButton
            // 
            this.DeleteAccountButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteAccountButton.ForeColor = System.Drawing.Color.Silver;
            this.DeleteAccountButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteAccountButton.Image")));
            this.DeleteAccountButton.Location = new System.Drawing.Point(55, 110);
            this.DeleteAccountButton.Name = "DeleteAccountButton";
            this.DeleteAccountButton.Size = new System.Drawing.Size(40, 40);
            this.DeleteAccountButton.TabIndex = 12;
            this.AddAllToolTip.SetToolTip(this.DeleteAccountButton, "Удалить выбранный аккаунт из списка");
            this.DeleteAccountButton.UseVisualStyleBackColor = true;
            this.DeleteAccountButton.Click += new System.EventHandler(this.DeleteAccountButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DeleteAccountButton);
            this.groupBox1.Controls.Add(this.EditAccountButton);
            this.groupBox1.Controls.Add(this.AddNewAccountButton);
            this.groupBox1.Controls.Add(this.OpskinsApiGroupBox);
            this.groupBox1.Controls.Add(this.LoginGroupBox);
            this.groupBox1.Controls.Add(this.PasswordGroupBox);
            this.groupBox1.Controls.Add(this.MafilePathGroupBox);
            this.groupBox1.Location = new System.Drawing.Point(410, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 500);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки Opskins";
            // 
            // OpskinsApiGroupBox
            // 
            this.OpskinsApiGroupBox.Controls.Add(this.OpskinsApiTextBox);
            this.OpskinsApiGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpskinsApiGroupBox.Location = new System.Drawing.Point(149, 67);
            this.OpskinsApiGroupBox.Name = "OpskinsApiGroupBox";
            this.OpskinsApiGroupBox.Size = new System.Drawing.Size(137, 37);
            this.OpskinsApiGroupBox.TabIndex = 11;
            this.OpskinsApiGroupBox.TabStop = false;
            this.OpskinsApiGroupBox.Text = "OPSKINS API";
            // 
            // OpskinsApiTextBox
            // 
            this.OpskinsApiTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OpskinsApiTextBox.Location = new System.Drawing.Point(3, 16);
            this.OpskinsApiTextBox.Name = "OpskinsApiTextBox";
            this.OpskinsApiTextBox.Size = new System.Drawing.Size(131, 20);
            this.OpskinsApiTextBox.TabIndex = 4;
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
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
            this.groupBox1.ResumeLayout(false);
            this.OpskinsApiGroupBox.ResumeLayout(false);
            this.OpskinsApiGroupBox.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox OpskinsApiGroupBox;
        private System.Windows.Forms.TextBox OpskinsApiTextBox;
        private System.Windows.Forms.Button DeleteAccountButton;
        private System.Windows.Forms.DataGridViewImageColumn AvatarColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoginColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PasswordColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpskinsApiColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountObjectHidenColumn;
    }
}
