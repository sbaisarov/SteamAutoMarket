namespace SteamAutoMarket.CustomElements.Tabs
{
    partial class UserControlTab
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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.CurrentUserInfo = new System.Windows.Forms.TabPage();
            this.AccountInfoControl = new SteamAutoMarket.CustomElements.Controls.Account.AccountInfoControl();
            this.TabControl.SuspendLayout();
            this.CurrentUserInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.CurrentUserInfo);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TabControl.ItemSize = new System.Drawing.Size(150, 25);
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Multiline = true;
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(894, 632);
            this.TabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl.TabIndex = 2;
            // 
            // CurrentUserInfo
            // 
            this.CurrentUserInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.CurrentUserInfo.Controls.Add(this.AccountInfoControl);
            this.CurrentUserInfo.Location = new System.Drawing.Point(4, 29);
            this.CurrentUserInfo.Name = "CurrentUserInfo";
            this.CurrentUserInfo.Padding = new System.Windows.Forms.Padding(3);
            this.CurrentUserInfo.Size = new System.Drawing.Size(886, 599);
            this.CurrentUserInfo.TabIndex = 0;
            this.CurrentUserInfo.Text = "Info";
            // 
            // AccountInfoControl
            // 
            this.AccountInfoControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.AccountInfoControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.AccountInfoControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.AccountInfoControl.Location = new System.Drawing.Point(0, 0);
            this.AccountInfoControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AccountInfoControl.Name = "AccountInfoControl";
            this.AccountInfoControl.Size = new System.Drawing.Size(885, 599);
            this.AccountInfoControl.TabIndex = 0;
            // 
            // UserControlTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.Controls.Add(this.TabControl);
            this.Font = new System.Drawing.Font("Impact", 14.25F);
            this.Name = "UserControlTab";
            this.Size = new System.Drawing.Size(894, 632);
            this.TabControl.ResumeLayout(false);
            this.CurrentUserInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage CurrentUserInfo;
        public System.Windows.Forms.TabControl TabControl;
        public Controls.Account.AccountInfoControl AccountInfoControl;
    }
}
