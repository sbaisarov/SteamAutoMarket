using autotrade.Utils;

namespace autotrade {
    partial class MainForm {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.leftHeaderPanel = new System.Windows.Forms.Panel();
            this.LeftEdge = new System.Windows.Forms.Panel();
            this.SidePanel = new System.Windows.Forms.Panel();
            this.SettingsLinkButton = new System.Windows.Forms.Button();
            this.LeftPanelHideShowButton = new System.Windows.Forms.Button();
            this.SaleLinkButton = new System.Windows.Forms.Button();
            this.LogoImageBox = new System.Windows.Forms.PictureBox();
            this.TradeLinkButton = new System.Windows.Forms.Button();
            this.BotEdge = new System.Windows.Forms.Panel();
            this.BottomEdge1 = new System.Windows.Forms.Panel();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.ProgramLogo = new System.Windows.Forms.PictureBox();
            this.titleLable = new System.Windows.Forms.Label();
            this.appCurtailButton = new System.Windows.Forms.Button();
            this.appExitButton = new System.Windows.Forms.Button();
            this.RightEdge = new System.Windows.Forms.Panel();
            this.MarketControlTab = new autotrade.CustomElements.Tabs.MarketControlTab();
            this.SettingsControlTab = new autotrade.CustomElements.SettingsControlTab();
            this.TradeControlTab = new autotrade.CustomElements.Tabs.TradeControlTab();
            this.leftHeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoImageBox)).BeginInit();
            this.HeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgramLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // leftHeaderPanel
            // 
            this.leftHeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.leftHeaderPanel.Controls.Add(this.LeftEdge);
            this.leftHeaderPanel.Controls.Add(this.SidePanel);
            this.leftHeaderPanel.Controls.Add(this.SettingsLinkButton);
            this.leftHeaderPanel.Controls.Add(this.LeftPanelHideShowButton);
            this.leftHeaderPanel.Controls.Add(this.SaleLinkButton);
            this.leftHeaderPanel.Controls.Add(this.LogoImageBox);
            this.leftHeaderPanel.Controls.Add(this.TradeLinkButton);
            this.leftHeaderPanel.Location = new System.Drawing.Point(0, 31);
            this.leftHeaderPanel.Name = "leftHeaderPanel";
            this.leftHeaderPanel.Size = new System.Drawing.Size(166, 696);
            this.leftHeaderPanel.TabIndex = 3;
            // 
            // LeftEdge
            // 
            this.LeftEdge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.LeftEdge.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftEdge.Location = new System.Drawing.Point(0, 0);
            this.LeftEdge.Name = "LeftEdge";
            this.LeftEdge.Size = new System.Drawing.Size(2, 696);
            this.LeftEdge.TabIndex = 5;
            // 
            // SidePanel
            // 
            this.SidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.SidePanel.Location = new System.Drawing.Point(0, 172);
            this.SidePanel.Name = "SidePanel";
            this.SidePanel.Size = new System.Drawing.Size(12, 45);
            this.SidePanel.TabIndex = 4;
            // 
            // SettingsLinkButton
            // 
            this.SettingsLinkButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SettingsLinkButton.FlatAppearance.BorderSize = 0;
            this.SettingsLinkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsLinkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SettingsLinkButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.SettingsLinkButton.Image = global::autotrade.Properties.Resources.MenuSettings;
            this.SettingsLinkButton.Location = new System.Drawing.Point(3, 172);
            this.SettingsLinkButton.Name = "SettingsLinkButton";
            this.SettingsLinkButton.Size = new System.Drawing.Size(166, 45);
            this.SettingsLinkButton.TabIndex = 1;
            this.SettingsLinkButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SettingsLinkButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SettingsLinkButton.UseVisualStyleBackColor = true;
            this.SettingsLinkButton.Click += new System.EventHandler(this.SettingsLinkButton_Click);
            // 
            // LeftPanelHideShowButton
            // 
            this.LeftPanelHideShowButton.BackgroundImage = global::autotrade.Properties.Resources.HideMenuButton;
            this.LeftPanelHideShowButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.LeftPanelHideShowButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LeftPanelHideShowButton.FlatAppearance.BorderSize = 0;
            this.LeftPanelHideShowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LeftPanelHideShowButton.Location = new System.Drawing.Point(134, 4);
            this.LeftPanelHideShowButton.Name = "LeftPanelHideShowButton";
            this.LeftPanelHideShowButton.Size = new System.Drawing.Size(27, 27);
            this.LeftPanelHideShowButton.TabIndex = 4;
            this.LeftPanelHideShowButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.LeftPanelHideShowButton.UseVisualStyleBackColor = true;
            this.LeftPanelHideShowButton.Click += new System.EventHandler(this.LeftPanelHideShowButton_Click);
            // 
            // SaleLinkButton
            // 
            this.SaleLinkButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaleLinkButton.FlatAppearance.BorderSize = 0;
            this.SaleLinkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaleLinkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaleLinkButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.SaleLinkButton.Image = global::autotrade.Properties.Resources.MenuMarket;
            this.SaleLinkButton.Location = new System.Drawing.Point(0, 233);
            this.SaleLinkButton.Name = "SaleLinkButton";
            this.SaleLinkButton.Size = new System.Drawing.Size(166, 45);
            this.SaleLinkButton.TabIndex = 2;
            this.SaleLinkButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaleLinkButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SaleLinkButton.UseVisualStyleBackColor = true;
            this.SaleLinkButton.Click += new System.EventHandler(this.SaleLinkButton_Click);
            // 
            // LogoImageBox
            // 
            this.LogoImageBox.BackgroundImage = global::autotrade.Properties.Resources.MainLogo;
            this.LogoImageBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.LogoImageBox.ErrorImage = null;
            this.LogoImageBox.Location = new System.Drawing.Point(1, 7);
            this.LogoImageBox.Name = "LogoImageBox";
            this.LogoImageBox.Size = new System.Drawing.Size(164, 161);
            this.LogoImageBox.TabIndex = 4;
            this.LogoImageBox.TabStop = false;
            // 
            // TradeLinkButton
            // 
            this.TradeLinkButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TradeLinkButton.FlatAppearance.BorderSize = 0;
            this.TradeLinkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TradeLinkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TradeLinkButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.TradeLinkButton.Image = global::autotrade.Properties.Resources.MenuTrade;
            this.TradeLinkButton.Location = new System.Drawing.Point(0, 294);
            this.TradeLinkButton.Name = "TradeLinkButton";
            this.TradeLinkButton.Size = new System.Drawing.Size(166, 45);
            this.TradeLinkButton.TabIndex = 7;
            this.TradeLinkButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TradeLinkButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.TradeLinkButton.UseVisualStyleBackColor = true;
            this.TradeLinkButton.Click += new System.EventHandler(this.TradeLinkButton_Click);
            // 
            // BotEdge
            // 
            this.BotEdge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.BotEdge.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BotEdge.Location = new System.Drawing.Point(0, 670);
            this.BotEdge.Name = "BotEdge";
            this.BotEdge.Size = new System.Drawing.Size(1061, 2);
            this.BotEdge.TabIndex = 7;
            // 
            // BottomEdge1
            // 
            this.BottomEdge1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BottomEdge1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomEdge1.Location = new System.Drawing.Point(0, 668);
            this.BottomEdge1.Name = "BottomEdge1";
            this.BottomEdge1.Size = new System.Drawing.Size(1061, 2);
            this.BottomEdge1.TabIndex = 6;
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.HeaderPanel.Controls.Add(this.ProgramLogo);
            this.HeaderPanel.Controls.Add(this.titleLable);
            this.HeaderPanel.Controls.Add(this.appCurtailButton);
            this.HeaderPanel.Controls.Add(this.appExitButton);
            this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(1061, 31);
            this.HeaderPanel.TabIndex = 2;
            this.HeaderPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Move_MouseDown);
            this.HeaderPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Move_MouseMove);
            this.HeaderPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Move_MouseUp);
            // 
            // ProgramLogo
            // 
            this.ProgramLogo.BackgroundImage = global::autotrade.Properties.Resources.SmallLogo;
            this.ProgramLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ProgramLogo.ErrorImage = null;
            this.ProgramLogo.InitialImage = null;
            this.ProgramLogo.Location = new System.Drawing.Point(8, 2);
            this.ProgramLogo.Name = "ProgramLogo";
            this.ProgramLogo.Size = new System.Drawing.Size(28, 24);
            this.ProgramLogo.TabIndex = 11;
            this.ProgramLogo.TabStop = false;
            this.ProgramLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Move_MouseDown);
            this.ProgramLogo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Move_MouseMove);
            this.ProgramLogo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Move_MouseUp);
            // 
            // titleLable
            // 
            this.titleLable.AutoSize = true;
            this.titleLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.titleLable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.titleLable.Location = new System.Drawing.Point(38, 8);
            this.titleLable.Name = "titleLable";
            this.titleLable.Size = new System.Drawing.Size(154, 18);
            this.titleLable.TabIndex = 7;
            this.titleLable.Text = "Steam-Auto-Market";
            this.titleLable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Move_MouseDown);
            this.titleLable.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Move_MouseMove);
            this.titleLable.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Move_MouseUp);
            // 
            // appCurtailButton
            // 
            this.appCurtailButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.appCurtailButton.FlatAppearance.BorderSize = 0;
            this.appCurtailButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appCurtailButton.Image = global::autotrade.Properties.Resources.MinimizeButton;
            this.appCurtailButton.Location = new System.Drawing.Point(999, 3);
            this.appCurtailButton.Name = "appCurtailButton";
            this.appCurtailButton.Size = new System.Drawing.Size(27, 23);
            this.appCurtailButton.TabIndex = 5;
            this.appCurtailButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.appCurtailButton.UseVisualStyleBackColor = true;
            this.appCurtailButton.Click += new System.EventHandler(this.AppCurtailButton_Click);
            // 
            // appExitButton
            // 
            this.appExitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.appExitButton.FlatAppearance.BorderSize = 0;
            this.appExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appExitButton.Image = global::autotrade.Properties.Resources.CloseButton;
            this.appExitButton.Location = new System.Drawing.Point(1029, 3);
            this.appExitButton.Name = "appExitButton";
            this.appExitButton.Size = new System.Drawing.Size(27, 23);
            this.appExitButton.TabIndex = 6;
            this.appExitButton.UseVisualStyleBackColor = true;
            this.appExitButton.Click += new System.EventHandler(this.AppExitButton_Click);
            // 
            // RightEdge
            // 
            this.RightEdge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.RightEdge.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightEdge.Location = new System.Drawing.Point(1058, 31);
            this.RightEdge.Name = "RightEdge";
            this.RightEdge.Size = new System.Drawing.Size(3, 637);
            this.RightEdge.TabIndex = 6;
            // 
            // MarketControlTab
            // 
            this.MarketControlTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.MarketControlTab.Location = new System.Drawing.Point(165, 36);
            this.MarketControlTab.Name = "MarketControlTab";
            this.MarketControlTab.Size = new System.Drawing.Size(894, 632);
            this.MarketControlTab.TabIndex = 8;
            // 
            // SettingsControlTab
            // 
            this.SettingsControlTab.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.SettingsControlTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.SettingsControlTab.CausesValidation = false;
            this.SettingsControlTab.Location = new System.Drawing.Point(165, 36);
            this.SettingsControlTab.Margin = new System.Windows.Forms.Padding(0);
            this.SettingsControlTab.Name = "SettingsControlTab";
            this.SettingsControlTab.Size = new System.Drawing.Size(894, 632);
            this.SettingsControlTab.TabIndex = 8;
            // 
            // tradeControlTab1
            // 
            this.TradeControlTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.TradeControlTab.Location = new System.Drawing.Point(165, 36);
            this.TradeControlTab.Name = "tradeControlTab1";
            this.TradeControlTab.Size = new System.Drawing.Size(894, 632);
            this.TradeControlTab.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.ClientSize = new System.Drawing.Size(1061, 672);
            this.Controls.Add(this.RightEdge);
            this.Controls.Add(this.SettingsControlTab);
            this.Controls.Add(this.BottomEdge1);
            this.Controls.Add(this.BotEdge);
            this.Controls.Add(this.leftHeaderPanel);
            this.Controls.Add(this.HeaderPanel);
            this.Controls.Add(this.MarketControlTab);
            this.Controls.Add(this.TradeControlTab);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Steam-Auto-Market";
            this.leftHeaderPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogoImageBox)).EndInit();
            this.HeaderPanel.ResumeLayout(false);
            this.HeaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgramLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel leftHeaderPanel;
        private System.Windows.Forms.PictureBox LogoImageBox;
        private System.Windows.Forms.Button SaleLinkButton;
        private System.Windows.Forms.Button LeftPanelHideShowButton;
        private System.Windows.Forms.Panel SidePanel;
        private System.Windows.Forms.Button SettingsLinkButton;
        private System.Windows.Forms.Button appExitButton;
        private System.Windows.Forms.Button appCurtailButton;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Label titleLable;
        private System.Windows.Forms.PictureBox ProgramLogo;
        private System.Windows.Forms.Panel LeftEdge;
        private System.Windows.Forms.Panel BottomEdge1;
        private System.Windows.Forms.Panel RightEdge;
        private System.Windows.Forms.Panel BotEdge;
        private System.Windows.Forms.Button TradeLinkButton;

        public CustomElements.SettingsControlTab SettingsControlTab;
        public CustomElements.Tabs.MarketControlTab MarketControlTab;
        public CustomElements.Tabs.TradeControlTab TradeControlTab;
    }
}