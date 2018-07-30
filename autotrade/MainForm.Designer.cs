namespace autotrade
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.leftHeaderPanel = new System.Windows.Forms.Panel();
            this.LeftEdge2 = new System.Windows.Forms.Panel();
            this.BottomEdge1 = new System.Windows.Forms.Panel();
            this.LeftEdge = new System.Windows.Forms.Panel();
            this.sidePanel = new System.Windows.Forms.Panel();
            this.settingsLinkButton = new System.Windows.Forms.Button();
            this.leftPanelHideShowButton = new System.Windows.Forms.Button();
            this.buyLinkButton = new System.Windows.Forms.Button();
            this.saleLinkButton = new System.Windows.Forms.Button();
            this.logoImageBox = new System.Windows.Forms.PictureBox();
            this.TradeLinkButton = new System.Windows.Forms.Button();
            this.appExitButton = new System.Windows.Forms.Button();
            this.appCurtailButton = new System.Windows.Forms.Button();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.titleLable = new System.Windows.Forms.Label();
            this.RightEdge = new System.Windows.Forms.Panel();
            this.BotEdge = new System.Windows.Forms.Panel();
            this.SettingsControl = new autotrade.CustomElements.SettingsControl();
            this.SaleControl = new autotrade.SaleControl();
            this.BuyControl = new autotrade.CustomElements.BuyControl();
            this.leftHeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoImageBox)).BeginInit();
            this.headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // leftHeaderPanel
            // 
            this.leftHeaderPanel.BackColor = System.Drawing.Color.RoyalBlue;
            this.leftHeaderPanel.Controls.Add(this.LeftEdge2);
            this.leftHeaderPanel.Controls.Add(this.BottomEdge1);
            this.leftHeaderPanel.Controls.Add(this.LeftEdge);
            this.leftHeaderPanel.Controls.Add(this.sidePanel);
            this.leftHeaderPanel.Controls.Add(this.settingsLinkButton);
            this.leftHeaderPanel.Controls.Add(this.leftPanelHideShowButton);
            this.leftHeaderPanel.Controls.Add(this.buyLinkButton);
            this.leftHeaderPanel.Controls.Add(this.saleLinkButton);
            this.leftHeaderPanel.Controls.Add(this.logoImageBox);
            this.leftHeaderPanel.Controls.Add(this.TradeLinkButton);
            this.leftHeaderPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftHeaderPanel.Location = new System.Drawing.Point(0, 31);
            this.leftHeaderPanel.Name = "leftHeaderPanel";
            this.leftHeaderPanel.Size = new System.Drawing.Size(166, 599);
            this.leftHeaderPanel.TabIndex = 3;
            // 
            // LeftEdge2
            // 
            this.LeftEdge2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.LeftEdge2.Location = new System.Drawing.Point(164, 0);
            this.LeftEdge2.Name = "LeftEdge2";
            this.LeftEdge2.Size = new System.Drawing.Size(2, 599);
            this.LeftEdge2.TabIndex = 6;
            // 
            // BottomEdge1
            // 
            this.BottomEdge1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BottomEdge1.Location = new System.Drawing.Point(1, 597);
            this.BottomEdge1.Name = "BottomEdge1";
            this.BottomEdge1.Size = new System.Drawing.Size(166, 2);
            this.BottomEdge1.TabIndex = 6;
            // 
            // LeftEdge
            // 
            this.LeftEdge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.LeftEdge.Location = new System.Drawing.Point(0, 0);
            this.LeftEdge.Name = "LeftEdge";
            this.LeftEdge.Size = new System.Drawing.Size(2, 599);
            this.LeftEdge.TabIndex = 5;
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
            this.settingsLinkButton.Location = new System.Drawing.Point(0, 172);
            this.settingsLinkButton.Name = "settingsLinkButton";
            this.settingsLinkButton.Size = new System.Drawing.Size(166, 45);
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
            this.buyLinkButton.Location = new System.Drawing.Point(0, 294);
            this.buyLinkButton.Name = "buyLinkButton";
            this.buyLinkButton.Size = new System.Drawing.Size(166, 45);
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
            this.saleLinkButton.Location = new System.Drawing.Point(0, 233);
            this.saleLinkButton.Name = "saleLinkButton";
            this.saleLinkButton.Size = new System.Drawing.Size(166, 45);
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
            // TradeLinkButton
            // 
            this.TradeLinkButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TradeLinkButton.FlatAppearance.BorderSize = 0;
            this.TradeLinkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TradeLinkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TradeLinkButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.TradeLinkButton.Image = ((System.Drawing.Image)(resources.GetObject("TradeLinkButton.Image")));
            this.TradeLinkButton.Location = new System.Drawing.Point(0, 357);
            this.TradeLinkButton.Name = "TradeLinkButton";
            this.TradeLinkButton.Size = new System.Drawing.Size(166, 45);
            this.TradeLinkButton.TabIndex = 7;
            this.TradeLinkButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TradeLinkButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.TradeLinkButton.UseVisualStyleBackColor = true;
            this.TradeLinkButton.Click += new System.EventHandler(this.TradeLinkButton_Click);
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
            // RightEdge
            // 
            this.RightEdge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.RightEdge.Location = new System.Drawing.Point(923, 31);
            this.RightEdge.Name = "RightEdge";
            this.RightEdge.Size = new System.Drawing.Size(2, 599);
            this.RightEdge.TabIndex = 6;
            // 
            // BotEdge
            // 
            this.BotEdge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BotEdge.Location = new System.Drawing.Point(166, 628);
            this.BotEdge.Name = "BotEdge";
            this.BotEdge.Size = new System.Drawing.Size(759, 2);
            this.BotEdge.TabIndex = 7;
            // 
            // SettingsControl
            // 
            this.SettingsControl.BackColor = System.Drawing.Color.Silver;
            this.SettingsControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.SettingsControl.Location = new System.Drawing.Point(166, 31);
            this.SettingsControl.Name = "SettingsControl";
            this.SettingsControl.Size = new System.Drawing.Size(759, 599);
            this.SettingsControl.TabIndex = 0;
            this.SettingsControl.Load += new System.EventHandler(this.SettingsControl_Load);
            // 
            // SaleControl
            // 
            this.SaleControl.BackColor = System.Drawing.Color.Silver;
            this.SaleControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaleControl.Location = new System.Drawing.Point(166, 31);
            this.SaleControl.Name = "SaleControl";
            this.SaleControl.Size = new System.Drawing.Size(759, 599);
            this.SaleControl.TabIndex = 4;
            // 
            // BuyControl
            // 
            this.BuyControl.BackColor = System.Drawing.Color.Silver;
            this.BuyControl.Location = new System.Drawing.Point(166, 31);
            this.BuyControl.Name = "BuyControl";
            this.BuyControl.Size = new System.Drawing.Size(759, 599);
            this.BuyControl.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 630);
            this.Controls.Add(this.BotEdge);
            this.Controls.Add(this.RightEdge);
            this.Controls.Add(this.leftHeaderPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.SettingsControl);
            this.Controls.Add(this.SaleControl);
            this.Controls.Add(this.BuyControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.Button settingsLinkButton;
        private System.Windows.Forms.Button appExitButton;
        private System.Windows.Forms.Button appCurtailButton;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLable;
        private System.Windows.Forms.PictureBox pictureBox1;
        public CustomElements.SettingsControl SettingsControl;
        private System.Windows.Forms.Panel LeftEdge;
        private System.Windows.Forms.Panel BottomEdge1;
        private System.Windows.Forms.Panel LeftEdge2;
        private System.Windows.Forms.Panel RightEdge;
        private System.Windows.Forms.Panel BotEdge;
        private CustomElements.BuyControl BuyControl;
        public SaleControl SaleControl;
        private System.Windows.Forms.Button TradeLinkButton;
    }
}

