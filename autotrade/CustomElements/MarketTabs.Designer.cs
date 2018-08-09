namespace autotrade.CustomElements {
    partial class MarketTabs {
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
            this.customTabControl1 = new System.Windows.Forms.CustomTabControl();
            this.BuyPage = new System.Windows.Forms.TabPage();
            this.SellPage = new System.Windows.Forms.TabPage();
            this.saleControl1 = new autotrade.SaleControl();
            this.buyControl1 = new autotrade.CustomElements.BuyControl();
            this.customTabControl1.SuspendLayout();
            this.BuyPage.SuspendLayout();
            this.SellPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // customTabControl1
            // 
            this.customTabControl1.Controls.Add(this.SellPage);
            this.customTabControl1.Controls.Add(this.BuyPage);
            this.customTabControl1.DisplayStyle = System.Windows.Forms.TabStyle.Angled;
            // 
            // 
            // 
            this.customTabControl1.DisplayStyleProvider.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.customTabControl1.DisplayStyleProvider.BorderColorHot = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.customTabControl1.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.customTabControl1.DisplayStyleProvider.CloserColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.customTabControl1.DisplayStyleProvider.CloserColorActive = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.customTabControl1.DisplayStyleProvider.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.customTabControl1.DisplayStyleProvider.FocusTrack = false;
            this.customTabControl1.DisplayStyleProvider.HotTrack = false;
            this.customTabControl1.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.customTabControl1.DisplayStyleProvider.Opacity = 1F;
            this.customTabControl1.DisplayStyleProvider.Overlap = 10;
            this.customTabControl1.DisplayStyleProvider.Padding = new System.Drawing.Point(100, 0);
            this.customTabControl1.DisplayStyleProvider.Radius = 17;
            this.customTabControl1.DisplayStyleProvider.ShowTabCloser = false;
            this.customTabControl1.DisplayStyleProvider.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.customTabControl1.DisplayStyleProvider.TextColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.customTabControl1.DisplayStyleProvider.TextColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.customTabControl1.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.customTabControl1.Location = new System.Drawing.Point(0, 0);
            this.customTabControl1.Name = "customTabControl1";
            this.customTabControl1.SelectedIndex = 0;
            this.customTabControl1.Size = new System.Drawing.Size(895, 637);
            this.customTabControl1.TabIndex = 0;
            // 
            // BuyPage
            // 
            this.BuyPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.BuyPage.Controls.Add(this.buyControl1);
            this.BuyPage.Location = new System.Drawing.Point(4, 31);
            this.BuyPage.Name = "BuyPage";
            this.BuyPage.Padding = new System.Windows.Forms.Padding(3);
            this.BuyPage.Size = new System.Drawing.Size(887, 602);
            this.BuyPage.TabIndex = 0;
            this.BuyPage.Text = "Buy";
            this.BuyPage.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // SellPage
            // 
            this.SellPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.SellPage.Controls.Add(this.saleControl1);
            this.SellPage.Location = new System.Drawing.Point(4, 31);
            this.SellPage.Name = "SellPage";
            this.SellPage.Padding = new System.Windows.Forms.Padding(3);
            this.SellPage.Size = new System.Drawing.Size(887, 602);
            this.SellPage.TabIndex = 1;
            this.SellPage.Text = "Sell";
            // 
            // saleControl1
            // 
            this.saleControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.saleControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saleControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saleControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.saleControl1.Location = new System.Drawing.Point(3, 3);
            this.saleControl1.Name = "saleControl1";
            this.saleControl1.Size = new System.Drawing.Size(881, 596);
            this.saleControl1.TabIndex = 0;
            // 
            // buyControl1
            // 
            this.buyControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.buyControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buyControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.buyControl1.Location = new System.Drawing.Point(3, 3);
            this.buyControl1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buyControl1.Name = "buyControl1";
            this.buyControl1.Size = new System.Drawing.Size(881, 596);
            this.buyControl1.TabIndex = 0;
            // 
            // MarketTabs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.Controls.Add(this.customTabControl1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.Name = "MarketTabs";
            this.Size = new System.Drawing.Size(895, 637);
            this.customTabControl1.ResumeLayout(false);
            this.BuyPage.ResumeLayout(false);
            this.SellPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CustomTabControl customTabControl1;
        private System.Windows.Forms.TabPage BuyPage;
        private System.Windows.Forms.TabPage SellPage;
        private SaleControl saleControl1;
        private BuyControl buyControl1;
    }
}
