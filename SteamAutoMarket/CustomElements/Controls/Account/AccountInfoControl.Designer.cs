namespace SteamAutoMarket.CustomElements.Controls.Account
{
    partial class AccountInfoControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.AvatarImageBox = new System.Windows.Forms.Panel();
            this.ActionsGroupBox = new System.Windows.Forms.GroupBox();
            this.OpenInBrowserButton = new System.Windows.Forms.Button();
            this.Copy2FAButton = new System.Windows.Forms.Button();
            this.LoginLable = new System.Windows.Forms.Label();
            this.InfoGridView = new System.Windows.Forms.DataGridView();
            this.ParameterColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InfoGroupBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ActionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InfoGridView)).BeginInit();
            this.InfoGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AvatarImageBox
            // 
            this.AvatarImageBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.AvatarImageBox.BackgroundImage = global::SteamAutoMarket.Properties.Resources.NoAvatar;
            this.AvatarImageBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.AvatarImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AvatarImageBox.Location = new System.Drawing.Point(79, 19);
            this.AvatarImageBox.Name = "AvatarImageBox";
            this.AvatarImageBox.Size = new System.Drawing.Size(185, 185);
            this.AvatarImageBox.TabIndex = 9;
            // 
            // ActionsGroupBox
            // 
            this.ActionsGroupBox.Controls.Add(this.button2);
            this.ActionsGroupBox.Controls.Add(this.button1);
            this.ActionsGroupBox.Controls.Add(this.OpenInBrowserButton);
            this.ActionsGroupBox.Controls.Add(this.Copy2FAButton);
            this.ActionsGroupBox.Controls.Add(this.LoginLable);
            this.ActionsGroupBox.Controls.Add(this.AvatarImageBox);
            this.ActionsGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.ActionsGroupBox.Location = new System.Drawing.Point(5, 3);
            this.ActionsGroupBox.Name = "ActionsGroupBox";
            this.ActionsGroupBox.Size = new System.Drawing.Size(345, 593);
            this.ActionsGroupBox.TabIndex = 10;
            this.ActionsGroupBox.TabStop = false;
            this.ActionsGroupBox.Text = "Actions";
            // 
            // OpenInBrowserButton
            // 
            this.OpenInBrowserButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.OpenInBrowserButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OpenInBrowserButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OpenInBrowserButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.OpenInBrowserButton.Location = new System.Drawing.Point(71, 329);
            this.OpenInBrowserButton.Name = "OpenInBrowserButton";
            this.OpenInBrowserButton.Size = new System.Drawing.Size(203, 45);
            this.OpenInBrowserButton.TabIndex = 33;
            this.OpenInBrowserButton.Text = "Open steam profile in browser";
            this.OpenInBrowserButton.UseVisualStyleBackColor = false;
            // 
            // Copy2FAButton
            // 
            this.Copy2FAButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.Copy2FAButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Copy2FAButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Copy2FAButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.Copy2FAButton.Location = new System.Drawing.Point(71, 262);
            this.Copy2FAButton.Name = "Copy2FAButton";
            this.Copy2FAButton.Size = new System.Drawing.Size(203, 45);
            this.Copy2FAButton.TabIndex = 32;
            this.Copy2FAButton.Text = "Copy 2FA code to clipboard";
            this.Copy2FAButton.UseVisualStyleBackColor = false;
            // 
            // LoginLable
            // 
            this.LoginLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoginLable.Location = new System.Drawing.Point(6, 207);
            this.LoginLable.Name = "LoginLable";
            this.LoginLable.Size = new System.Drawing.Size(333, 42);
            this.LoginLable.TabIndex = 10;
            this.LoginLable.Text = "Not logged in";
            this.LoginLable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InfoGridView
            // 
            this.InfoGridView.AllowUserToAddRows = false;
            this.InfoGridView.AllowUserToDeleteRows = false;
            this.InfoGridView.AllowUserToResizeRows = false;
            this.InfoGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.InfoGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.InfoGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.InfoGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InfoGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.InfoGridView.ColumnHeadersHeight = 30;
            this.InfoGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.InfoGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParameterColumn,
            this.ValueColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.InfoGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.InfoGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoGridView.EnableHeadersVisualStyles = false;
            this.InfoGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.InfoGridView.Location = new System.Drawing.Point(3, 16);
            this.InfoGridView.Name = "InfoGridView";
            this.InfoGridView.RowHeadersVisible = false;
            this.InfoGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.InfoGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InfoGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.InfoGridView.Size = new System.Drawing.Size(520, 574);
            this.InfoGridView.TabIndex = 33;
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
            // InfoGroupBox
            // 
            this.InfoGroupBox.Controls.Add(this.InfoGridView);
            this.InfoGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.InfoGroupBox.Location = new System.Drawing.Point(356, 6);
            this.InfoGroupBox.Name = "InfoGroupBox";
            this.InfoGroupBox.Size = new System.Drawing.Size(526, 593);
            this.InfoGroupBox.TabIndex = 34;
            this.InfoGroupBox.TabStop = false;
            this.InfoGroupBox.Text = "Info";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.button1.Location = new System.Drawing.Point(71, 380);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(203, 45);
            this.button1.TabIndex = 34;
            this.button1.Text = "Confirm All Market transactions";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.button2.Location = new System.Drawing.Point(71, 431);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(203, 45);
            this.button2.TabIndex = 35;
            this.button2.Text = "Decline All Market Transactions";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // AccountInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.Controls.Add(this.InfoGroupBox);
            this.Controls.Add(this.ActionsGroupBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.Name = "AccountInfoControl";
            this.Size = new System.Drawing.Size(885, 599);
            this.ActionsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InfoGridView)).EndInit();
            this.InfoGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel AvatarImageBox;
        private System.Windows.Forms.GroupBox ActionsGroupBox;
        private System.Windows.Forms.Label LoginLable;
        private System.Windows.Forms.Button Copy2FAButton;
        private System.Windows.Forms.DataGridView InfoGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParameterColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueColumn;
        private System.Windows.Forms.GroupBox InfoGroupBox;
        private System.Windows.Forms.Button OpenInBrowserButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}
