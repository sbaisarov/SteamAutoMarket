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
            this.logoImageBox = new System.Windows.Forms.PictureBox();
            this.appExitButton = new System.Windows.Forms.Button();
            this.appCurtailButton = new System.Windows.Forms.Button();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.titleLable = new System.Windows.Forms.Label();
            this.saleControl = new autotrade.SaleControl();
            this.settingsControl = new autotrade.CustomElements.SettingsControl();
            this.leftHeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoImageBox)).BeginInit();
            this.headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.leftHeaderPanel.Controls.Add(this.logoImageBox);
            this.leftHeaderPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftHeaderPanel.Location = new System.Drawing.Point(0, 31);
            this.leftHeaderPanel.Name = "leftHeaderPanel";
            this.leftHeaderPanel.Size = new System.Drawing.Size(166, 599);
            this.leftHeaderPanel.TabIndex = 3;
            // 
            // sidePanel
            // 
            this.sidePanel.BackColor = System.Drawing.Color.AliceBlue;
            this.sidePanel.Location = new System.Drawing.Point(0, 172);
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
            this.settingsLinkButton.Location = new System.Drawing.Point(11, 172);
            this.settingsLinkButton.Name = "settingsLinkButton";
            this.settingsLinkButton.Size = new System.Drawing.Size(143, 45);
            this.settingsLinkButton.TabIndex = 1;
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
            this.leftPanelHideShowButton.Location = new System.Drawing.Point(132, 4);
            this.leftPanelHideShowButton.Name = "leftPanelHideShowButton";
            this.leftPanelHideShowButton.Size = new System.Drawing.Size(27, 27);
            this.leftPanelHideShowButton.TabIndex = 4;
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
            this.buyLinkButton.Location = new System.Drawing.Point(11, 294);
            this.buyLinkButton.Name = "buyLinkButton";
            this.buyLinkButton.Size = new System.Drawing.Size(143, 45);
            this.buyLinkButton.TabIndex = 3;
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
            this.saleLinkButton.Location = new System.Drawing.Point(11, 233);
            this.saleLinkButton.Name = "saleLinkButton";
            this.saleLinkButton.Size = new System.Drawing.Size(143, 45);
            this.saleLinkButton.TabIndex = 2;
            this.saleLinkButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saleLinkButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.saleLinkButton.UseVisualStyleBackColor = true;
            this.saleLinkButton.Click += new System.EventHandler(this.SaleLinkButton_Click);
            // 
            // logoImageBox
            // 
            this.logoImageBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("logoImageBox.BackgroundImage")));
            this.logoImageBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.logoImageBox.Location = new System.Drawing.Point(1, 11);
            this.logoImageBox.Name = "logoImageBox";
            this.logoImageBox.Size = new System.Drawing.Size(164, 161);
            this.logoImageBox.TabIndex = 4;
            this.logoImageBox.TabStop = false;
            // 
            // appExitButton
            // 
            this.appExitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.appExitButton.FlatAppearance.BorderSize = 0;
            this.appExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appExitButton.Image = ((System.Drawing.Image)(resources.GetObject("appExitButton.Image")));
            this.appExitButton.Location = new System.Drawing.Point(894, 4);
            this.appExitButton.Name = "appExitButton";
            this.appExitButton.Size = new System.Drawing.Size(23, 23);
            this.appExitButton.TabIndex = 6;
            this.appExitButton.UseVisualStyleBackColor = true;
            this.appExitButton.Click += new System.EventHandler(this.AppExitButton_Click);
            // 
            // appCurtailButton
            // 
            this.appCurtailButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.appCurtailButton.FlatAppearance.BorderSize = 0;
            this.appCurtailButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appCurtailButton.Image = ((System.Drawing.Image)(resources.GetObject("appCurtailButton.Image")));
            this.appCurtailButton.Location = new System.Drawing.Point(859, 4);
            this.appCurtailButton.Name = "appCurtailButton";
            this.appCurtailButton.Size = new System.Drawing.Size(23, 23);
            this.appCurtailButton.TabIndex = 5;
            this.appCurtailButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.appCurtailButton.UseVisualStyleBackColor = true;
            this.appCurtailButton.Click += new System.EventHandler(this.AppCurtailButton_Click);
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.headerPanel.Controls.Add(this.pictureBox1);
            this.headerPanel.Controls.Add(this.titleLable);
            this.headerPanel.Controls.Add(this.appCurtailButton);
            this.headerPanel.Controls.Add(this.appExitButton);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(925, 31);
            this.headerPanel.TabIndex = 2;
            this.headerPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Move_MouseDown);
            this.headerPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Move_MouseMove);
            this.headerPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Move_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(8, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // titleLable
            // 
            this.titleLable.AutoSize = true;
            this.titleLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.titleLable.ForeColor = System.Drawing.Color.White;
            this.titleLable.Location = new System.Drawing.Point(43, 6);
            this.titleLable.Name = "titleLable";
            this.titleLable.Size = new System.Drawing.Size(169, 18);
            this.titleLable.TabIndex = 7;
            this.titleLable.Text = "\"Opskins\" Auto trade ";
            // 
            // saleControl
            // 
            this.saleControl.BackColor = System.Drawing.Color.Silver;
            this.saleControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saleControl.Location = new System.Drawing.Point(166, 31);
            this.saleControl.Name = "saleControl";
            this.saleControl.Size = new System.Drawing.Size(759, 599);
            this.saleControl.TabIndex = 4;
            // 
            // settingsControl
            // 
            this.settingsControl.BackColor = System.Drawing.Color.Silver;
            this.settingsControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.settingsControl.Location = new System.Drawing.Point(166, 31);
            this.settingsControl.Name = "settingsControl";
            this.settingsControl.Size = new System.Drawing.Size(759, 599);
            this.settingsControl.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 630);
            this.Controls.Add(this.leftHeaderPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.settingsControl);
            this.Controls.Add(this.saleControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.leftHeaderPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoImageBox)).EndInit();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.Button appExitButton;
        private System.Windows.Forms.Button appCurtailButton;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLable;
        private System.Windows.Forms.PictureBox pictureBox1;
        private SaleControl saleControl;
    }
}

