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
            this.borderTopPanel = new System.Windows.Forms.Panel();
            this.borderLeftPanel = new System.Windows.Forms.Panel();
            this.borderRightPanel = new System.Windows.Forms.Panel();
            this.borderBottomPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.appCurtailButton = new System.Windows.Forms.Button();
            this.appExpandButton = new System.Windows.Forms.Button();
            this.appExitButton = new System.Windows.Forms.Button();
            this.leftHeaderPanel = new System.Windows.Forms.Panel();
            this.leftPanelHideShowButton = new System.Windows.Forms.Button();
            this.buyLinkButton = new System.Windows.Forms.Button();
            this.saleLinkButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.sidePanel = new System.Windows.Forms.Panel();
            this.saleControl1 = new autotrade.SaleControl();
            this.buyControl1 = new autotrade.BuyControl();
            this.panel1.SuspendLayout();
            this.leftHeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // borderTopPanel
            // 
            this.borderTopPanel.BackColor = System.Drawing.Color.RoyalBlue;
            this.borderTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.borderTopPanel.Location = new System.Drawing.Point(0, 0);
            this.borderTopPanel.Name = "borderTopPanel";
            this.borderTopPanel.Size = new System.Drawing.Size(962, 1);
            this.borderTopPanel.TabIndex = 0;
            // 
            // borderLeftPanel
            // 
            this.borderLeftPanel.BackColor = System.Drawing.Color.RoyalBlue;
            this.borderLeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.borderLeftPanel.Location = new System.Drawing.Point(0, 1);
            this.borderLeftPanel.Name = "borderLeftPanel";
            this.borderLeftPanel.Size = new System.Drawing.Size(1, 631);
            this.borderLeftPanel.TabIndex = 1;
            // 
            // borderRightPanel
            // 
            this.borderRightPanel.BackColor = System.Drawing.Color.RoyalBlue;
            this.borderRightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.borderRightPanel.Location = new System.Drawing.Point(961, 1);
            this.borderRightPanel.Name = "borderRightPanel";
            this.borderRightPanel.Size = new System.Drawing.Size(1, 631);
            this.borderRightPanel.TabIndex = 1;
            // 
            // borderBottomPanel
            // 
            this.borderBottomPanel.BackColor = System.Drawing.Color.RoyalBlue;
            this.borderBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.borderBottomPanel.Location = new System.Drawing.Point(0, 632);
            this.borderBottomPanel.Name = "borderBottomPanel";
            this.borderBottomPanel.Size = new System.Drawing.Size(962, 1);
            this.borderBottomPanel.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.appCurtailButton);
            this.panel1.Controls.Add(this.appExpandButton);
            this.panel1.Controls.Add(this.appExitButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 31);
            this.panel1.TabIndex = 2;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.move_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.move_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.move_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "\"Opskins\" Auto trade ";
            // 
            // appCurtailButton
            // 
            this.appCurtailButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.appCurtailButton.FlatAppearance.BorderSize = 0;
            this.appCurtailButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appCurtailButton.Image = ((System.Drawing.Image)(resources.GetObject("appCurtailButton.Image")));
            this.appCurtailButton.Location = new System.Drawing.Point(844, 9);
            this.appCurtailButton.Name = "appCurtailButton";
            this.appCurtailButton.Size = new System.Drawing.Size(26, 17);
            this.appCurtailButton.TabIndex = 6;
            this.appCurtailButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.appCurtailButton.UseVisualStyleBackColor = true;
            this.appCurtailButton.Click += new System.EventHandler(this.appCurtailButton_Click);
            // 
            // appExpandButton
            // 
            this.appExpandButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.appExpandButton.FlatAppearance.BorderSize = 0;
            this.appExpandButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appExpandButton.Image = ((System.Drawing.Image)(resources.GetObject("appExpandButton.Image")));
            this.appExpandButton.Location = new System.Drawing.Point(885, 6);
            this.appExpandButton.Name = "appExpandButton";
            this.appExpandButton.Size = new System.Drawing.Size(25, 20);
            this.appExpandButton.TabIndex = 5;
            this.appExpandButton.UseVisualStyleBackColor = true;
            this.appExpandButton.Click += new System.EventHandler(this.appExpandButton_Click);
            // 
            // appExitButton
            // 
            this.appExitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.appExitButton.FlatAppearance.BorderSize = 0;
            this.appExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appExitButton.Image = ((System.Drawing.Image)(resources.GetObject("appExitButton.Image")));
            this.appExitButton.Location = new System.Drawing.Point(928, 4);
            this.appExitButton.Name = "appExitButton";
            this.appExitButton.Size = new System.Drawing.Size(21, 22);
            this.appExitButton.TabIndex = 4;
            this.appExitButton.UseVisualStyleBackColor = true;
            this.appExitButton.Click += new System.EventHandler(this.appExitButton_Click);
            // 
            // leftHeaderPanel
            // 
            this.leftHeaderPanel.BackColor = System.Drawing.Color.RoyalBlue;
            this.leftHeaderPanel.Controls.Add(this.leftPanelHideShowButton);
            this.leftHeaderPanel.Controls.Add(this.buyLinkButton);
            this.leftHeaderPanel.Controls.Add(this.saleLinkButton);
            this.leftHeaderPanel.Controls.Add(this.label2);
            this.leftHeaderPanel.Controls.Add(this.pictureBox1);
            this.leftHeaderPanel.Controls.Add(this.sidePanel);
            this.leftHeaderPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftHeaderPanel.Location = new System.Drawing.Point(1, 32);
            this.leftHeaderPanel.Name = "leftHeaderPanel";
            this.leftHeaderPanel.Size = new System.Drawing.Size(166, 600);
            this.leftHeaderPanel.TabIndex = 3;
            this.leftHeaderPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
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
            this.leftPanelHideShowButton.Click += new System.EventHandler(this.leftPanelHideShowButton_Click);
            // 
            // buyLinkButton
            // 
            this.buyLinkButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buyLinkButton.FlatAppearance.BorderSize = 0;
            this.buyLinkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buyLinkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buyLinkButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buyLinkButton.Image = ((System.Drawing.Image)(resources.GetObject("buyLinkButton.Image")));
            this.buyLinkButton.Location = new System.Drawing.Point(11, 170);
            this.buyLinkButton.Name = "buyLinkButton";
            this.buyLinkButton.Size = new System.Drawing.Size(119, 36);
            this.buyLinkButton.TabIndex = 9;
            this.buyLinkButton.Text = "  Покупка";
            this.buyLinkButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buyLinkButton.UseVisualStyleBackColor = true;
            this.buyLinkButton.Click += new System.EventHandler(this.buyLinkButton_Click);
            // 
            // saleLinkButton
            // 
            this.saleLinkButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saleLinkButton.FlatAppearance.BorderSize = 0;
            this.saleLinkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saleLinkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saleLinkButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.saleLinkButton.Image = ((System.Drawing.Image)(resources.GetObject("saleLinkButton.Image")));
            this.saleLinkButton.Location = new System.Drawing.Point(11, 128);
            this.saleLinkButton.Name = "saleLinkButton";
            this.saleLinkButton.Size = new System.Drawing.Size(127, 36);
            this.saleLinkButton.TabIndex = 8;
            this.saleLinkButton.Text = "  Продажа";
            this.saleLinkButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saleLinkButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.saleLinkButton.UseVisualStyleBackColor = true;
            this.saleLinkButton.Click += new System.EventHandler(this.saleLinkButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(42, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Opskins";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(50, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 78);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // sidePanel
            // 
            this.sidePanel.BackColor = System.Drawing.Color.AliceBlue;
            this.sidePanel.Location = new System.Drawing.Point(-2, 128);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Size = new System.Drawing.Size(10, 36);
            this.sidePanel.TabIndex = 4;
            // 
            // saleControl1
            // 
            this.saleControl1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.saleControl1.Location = new System.Drawing.Point(162, 32);
            this.saleControl1.Name = "saleControl1";
            this.saleControl1.Size = new System.Drawing.Size(799, 599);
            this.saleControl1.TabIndex = 5;
            // 
            // buyControl1
            // 
            this.buyControl1.BackColor = System.Drawing.Color.Gold;
            this.buyControl1.Location = new System.Drawing.Point(162, 31);
            this.buyControl1.Name = "buyControl1";
            this.buyControl1.Size = new System.Drawing.Size(799, 530);
            this.buyControl1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 633);
            this.Controls.Add(this.leftHeaderPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.borderLeftPanel);
            this.Controls.Add(this.borderRightPanel);
            this.Controls.Add(this.borderBottomPanel);
            this.Controls.Add(this.borderTopPanel);
            this.Controls.Add(this.saleControl1);
            this.Controls.Add(this.buyControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.leftHeaderPanel.ResumeLayout(false);
            this.leftHeaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel borderTopPanel;
        private System.Windows.Forms.Panel borderLeftPanel;
        private System.Windows.Forms.Panel borderRightPanel;
        private System.Windows.Forms.Panel borderBottomPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel leftHeaderPanel;
        private System.Windows.Forms.Button appExitButton;
        private System.Windows.Forms.Button appExpandButton;
        private System.Windows.Forms.Button appCurtailButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buyLinkButton;
        private System.Windows.Forms.Button saleLinkButton;
        private System.Windows.Forms.Button leftPanelHideShowButton;
        private System.Windows.Forms.Panel sidePanel;
        private BuyControl buyControl1;
        private SaleControl saleControl1;
    }
}

