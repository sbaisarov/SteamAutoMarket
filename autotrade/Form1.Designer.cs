namespace autotrade
{
    partial class Form1
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.leftHeaderPanel = new System.Windows.Forms.Panel();
            this.sidePanel = new System.Windows.Forms.Panel();
            this.settingsLinkButton = new System.Windows.Forms.Button();
            this.leftPanelHideShowButton = new System.Windows.Forms.Button();
            this.buyLinkButton = new System.Windows.Forms.Button();
            this.saleLinkButton = new System.Windows.Forms.Button();
            this.menuTitleLable = new System.Windows.Forms.Label();
            this.logoImageBox = new System.Windows.Forms.PictureBox();
            this.appExitButton = new System.Windows.Forms.Button();
            this.appExpandButton = new System.Windows.Forms.Button();
            this.appCurtailButton = new System.Windows.Forms.Button();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.titleLable = new System.Windows.Forms.Label();
            this.settingsControl = new autotrade.CustomElements.SettingsControl();
            this.saleControl = new autotrade.SaleControl();
            this.leftHeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoImageBox)).BeginInit();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftHeaderPanel
            // 
            this.leftHeaderPanel.BackColor = System.Drawing.Color.RoyalBlue;
            this.leftHeaderPanel.Controls.Add(this.sidePanel);
            this.leftHeaderPanel.Controls.Add(this.settingsLinkButton);
            this.leftHeaderPanel.Controls.Add(this.leftPanelHideShowButton);
            this.leftHeaderPanel.Controls.Add(this.buyLinkButton);
            this.leftHeaderPanel.Controls.Add(this.saleLinkButton);
            this.leftHeaderPanel.Controls.Add(this.menuTitleLable);
            this.leftHeaderPanel.Controls.Add(this.logoImageBox);
            this.leftHeaderPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftHeaderPanel.Location = new System.Drawing.Point(0, 31);
            this.leftHeaderPanel.Name = "leftHeaderPanel";
            this.leftHeaderPanel.Size = new System.Drawing.Size(166, 601);
            this.leftHeaderPanel.TabIndex = 3;
            // 
            // sidePanel
            // 
            this.sidePanel.BackColor = System.Drawing.Color.AliceBlue;
            this.sidePanel.Location = new System.Drawing.Point(0, 132);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Size = new System.Drawing.Size(10, 45);
            this.sidePanel.TabIndex = 4;
            // 
            // settingsLinkButton
            // 
            this.settingsLinkButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.settingsLinkButton.FlatAppearance.BorderSize = 0;
            this.settingsLinkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsLinkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settingsLinkButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.settingsLinkButton.Image = ((System.Drawing.Image)(resources.GetObject("settingsLinkButton.Image")));
            this.settingsLinkButton.Location = new System.Drawing.Point(11, 132);
            this.settingsLinkButton.Name = "settingsLinkButton";
            this.settingsLinkButton.Size = new System.Drawing.Size(143, 45);
            this.settingsLinkButton.TabIndex = 10;
            this.settingsLinkButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.settingsLinkButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.settingsLinkButton.UseVisualStyleBackColor = true;
            this.settingsLinkButton.Click += new System.EventHandler(this.SettingsLinkButton_Click);
            // 
            // leftPanelHideShowButton
            // 
            this.leftPanelHideShowButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.leftPanelHideShowButton.FlatAppearance.BorderSize = 0;
            this.leftPanelHideShowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.leftPanelHideShowButton.Image = ((System.Drawing.Image)(resources.GetObject("leftPanelHideShowButton.Image")));
            this.leftPanelHideShowButton.Location = new System.Drawing.Point(128, 3);
            this.leftPanelHideShowButton.Name = "leftPanelHideShowButton";
            this.leftPanelHideShowButton.Size = new System.Drawing.Size(26, 27);
            this.leftPanelHideShowButton.TabIndex = 8;
            this.leftPanelHideShowButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.leftPanelHideShowButton.UseVisualStyleBackColor = true;
            this.leftPanelHideShowButton.Click += new System.EventHandler(this.LeftPanelHideShowButton_Click);
            // 
            // buyLinkButton
            // 
            this.buyLinkButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buyLinkButton.FlatAppearance.BorderSize = 0;
            this.buyLinkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buyLinkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buyLinkButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buyLinkButton.Image = ((System.Drawing.Image)(resources.GetObject("buyLinkButton.Image")));
            this.buyLinkButton.Location = new System.Drawing.Point(14, 254);
            this.buyLinkButton.Name = "buyLinkButton";
            this.buyLinkButton.Size = new System.Drawing.Size(143, 45);
            this.buyLinkButton.TabIndex = 9;
            this.buyLinkButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buyLinkButton.UseVisualStyleBackColor = true;
            this.buyLinkButton.Click += new System.EventHandler(this.BuyLinkButton_Click);
            // 
            // saleLinkButton
            // 
            this.saleLinkButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saleLinkButton.FlatAppearance.BorderSize = 0;
            this.saleLinkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saleLinkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saleLinkButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.saleLinkButton.Image = ((System.Drawing.Image)(resources.GetObject("saleLinkButton.Image")));
            this.saleLinkButton.Location = new System.Drawing.Point(11, 193);
            this.saleLinkButton.Name = "saleLinkButton";
            this.saleLinkButton.Size = new System.Drawing.Size(143, 45);
            this.saleLinkButton.TabIndex = 8;
            this.saleLinkButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saleLinkButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.saleLinkButton.UseVisualStyleBackColor = true;
            this.saleLinkButton.Click += new System.EventHandler(this.SaleLinkButton_Click);
            // 
            // menuTitleLable
            // 
            this.menuTitleLable.AutoSize = true;
            this.menuTitleLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuTitleLable.ForeColor = System.Drawing.Color.White;
            this.menuTitleLable.Location = new System.Drawing.Point(42, 87);
            this.menuTitleLable.Name = "menuTitleLable";
            this.menuTitleLable.Size = new System.Drawing.Size(70, 18);
            this.menuTitleLable.TabIndex = 8;
            this.menuTitleLable.Text = "Opskins";
            // 
            // logoImageBox
            // 
            this.logoImageBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("logoImageBox.BackgroundImage")));
            this.logoImageBox.Location = new System.Drawing.Point(50, 6);
            this.logoImageBox.Name = "logoImageBox";
            this.logoImageBox.Size = new System.Drawing.Size(62, 78);
            this.logoImageBox.TabIndex = 4;
            this.logoImageBox.TabStop = false;
            // 
            // appExitButton
            // 
            this.appExitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.appExitButton.FlatAppearance.BorderSize = 0;
            this.appExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appExitButton.Image = ((System.Drawing.Image)(resources.GetObject("appExitButton.Image")));
            this.appExitButton.Location = new System.Drawing.Point(881, 3);
            this.appExitButton.Name = "appExitButton";
            this.appExitButton.Size = new System.Drawing.Size(21, 22);
            this.appExitButton.TabIndex = 4;
            this.appExitButton.UseVisualStyleBackColor = true;
            this.appExitButton.Click += new System.EventHandler(this.AppExitButton_Click);
            // 
            // appExpandButton
            // 
            this.appExpandButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.appExpandButton.FlatAppearance.BorderSize = 0;
            this.appExpandButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appExpandButton.Image = ((System.Drawing.Image)(resources.GetObject("appExpandButton.Image")));
            this.appExpandButton.Location = new System.Drawing.Point(838, 5);
            this.appExpandButton.Name = "appExpandButton";
            this.appExpandButton.Size = new System.Drawing.Size(25, 20);
            this.appExpandButton.TabIndex = 5;
            this.appExpandButton.UseVisualStyleBackColor = true;
            this.appExpandButton.Click += new System.EventHandler(this.AppExpandButton_Click);
            // 
            // appCurtailButton
            // 
            this.appCurtailButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.appCurtailButton.FlatAppearance.BorderSize = 0;
            this.appCurtailButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appCurtailButton.Image = ((System.Drawing.Image)(resources.GetObject("appCurtailButton.Image")));
            this.appCurtailButton.Location = new System.Drawing.Point(797, 8);
            this.appCurtailButton.Name = "appCurtailButton";
            this.appCurtailButton.Size = new System.Drawing.Size(26, 17);
            this.appCurtailButton.TabIndex = 6;
            this.appCurtailButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.appCurtailButton.UseVisualStyleBackColor = true;
            this.appCurtailButton.Click += new System.EventHandler(this.AppCurtailButton_Click);
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.headerPanel.Controls.Add(this.titleLable);
            this.headerPanel.Controls.Add(this.appCurtailButton);
            this.headerPanel.Controls.Add(this.appExpandButton);
            this.headerPanel.Controls.Add(this.appExitButton);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(929, 31);
            this.headerPanel.TabIndex = 2;
            this.headerPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Move_MouseDown);
            this.headerPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Move_MouseMove);
            this.headerPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Move_MouseUp);
            // 
            // titleLable
            // 
            this.titleLable.AutoSize = true;
            this.titleLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.titleLable.ForeColor = System.Drawing.Color.White;
            this.titleLable.Location = new System.Drawing.Point(11, 6);
            this.titleLable.Name = "titleLable";
            this.titleLable.Size = new System.Drawing.Size(169, 18);
            this.titleLable.TabIndex = 7;
            this.titleLable.Text = "\"Opskins\" Auto trade ";
            // 
            // settingsControl
            // 
            this.settingsControl.BackColor = System.Drawing.Color.Silver;
            this.settingsControl.Location = new System.Drawing.Point(167, 32);
            this.settingsControl.Name = "settingsControl";
            this.settingsControl.Size = new System.Drawing.Size(761, 598);
            this.settingsControl.TabIndex = 4;
            // 
            // saleControl
            // 
            this.saleControl.BackColor = System.Drawing.Color.Silver;
            this.saleControl.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saleControl.Location = new System.Drawing.Point(167, 32);
            this.saleControl.Name = "saleControl";
            this.saleControl.Size = new System.Drawing.Size(759, 599);
            this.saleControl.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 632);
            this.Controls.Add(this.settingsControl);
            this.Controls.Add(this.leftHeaderPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.saleControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.leftHeaderPanel.ResumeLayout(false);
            this.leftHeaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoImageBox)).EndInit();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel leftHeaderPanel;
        private System.Windows.Forms.PictureBox logoImageBox;
        private System.Windows.Forms.Button buyLinkButton;
        private System.Windows.Forms.Button saleLinkButton;
        private System.Windows.Forms.Button leftPanelHideShowButton;
        private System.Windows.Forms.Panel sidePanel;
        private autotrade.SaleControl saleSteamControl1;
        private System.Windows.Forms.Button settingsLinkButton;
        private CustomElements.SettingsControl settingsControl;
        private System.Windows.Forms.Label menuTitleLable;
        private System.Windows.Forms.Button appExitButton;
        private System.Windows.Forms.Button appExpandButton;
        private System.Windows.Forms.Button appCurtailButton;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLable;
        private SaleControl saleControl;
    }
}

