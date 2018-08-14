namespace autotrade.CustomElements.Tabs {
    partial class MarketControlTab {
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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.MarketSellTab = new System.Windows.Forms.TabPage();
            this.SaleControl = new autotrade.SaleControl();
            this.MarketBuyTab = new System.Windows.Forms.TabPage();
            this.BuyControl = new autotrade.CustomElements.MarketBuyControl();
            this.TabControl.SuspendLayout();
            this.MarketSellTab.SuspendLayout();
            this.MarketBuyTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.MarketSellTab);
            this.TabControl.Controls.Add(this.MarketBuyTab);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TabControl.ItemSize = new System.Drawing.Size(150, 25);
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Multiline = true;
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(894, 632);
            this.TabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl.TabIndex = 1;
            // 
            // MarketSellTab
            // 
            this.MarketSellTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.MarketSellTab.Controls.Add(this.SaleControl);
            this.MarketSellTab.Location = new System.Drawing.Point(4, 29);
            this.MarketSellTab.Name = "MarketSellTab";
            this.MarketSellTab.Padding = new System.Windows.Forms.Padding(3);
            this.MarketSellTab.Size = new System.Drawing.Size(886, 599);
            this.MarketSellTab.TabIndex = 0;
            this.MarketSellTab.Text = "Sell";
            // 
            // SaleControl
            // 
            this.SaleControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.SaleControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaleControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.SaleControl.Location = new System.Drawing.Point(0, 0);
            this.SaleControl.Name = "SaleControl";
            this.SaleControl.Size = new System.Drawing.Size(885, 599);
            this.SaleControl.TabIndex = 0;
            // 
            // MarketBuyTab
            // 
            this.MarketBuyTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.MarketBuyTab.Controls.Add(this.BuyControl);
            this.MarketBuyTab.Location = new System.Drawing.Point(4, 29);
            this.MarketBuyTab.Name = "MarketBuyTab";
            this.MarketBuyTab.Padding = new System.Windows.Forms.Padding(3);
            this.MarketBuyTab.Size = new System.Drawing.Size(886, 599);
            this.MarketBuyTab.TabIndex = 1;
            this.MarketBuyTab.Text = "Buy";
            // 
            // BuyControl
            // 
            this.BuyControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.BuyControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.BuyControl.Location = new System.Drawing.Point(0, 0);
            this.BuyControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BuyControl.Name = "BuyControl";
            this.BuyControl.Size = new System.Drawing.Size(885, 599);
            this.BuyControl.TabIndex = 0;
            // 
            // MarketControlTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.Controls.Add(this.TabControl);
            this.Name = "MarketControlTab";
            this.Size = new System.Drawing.Size(894, 632);
            this.TabControl.ResumeLayout(false);
            this.MarketSellTab.ResumeLayout(false);
            this.MarketBuyTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage MarketSellTab;
        private System.Windows.Forms.TabPage MarketBuyTab;
        public SaleControl SaleControl;
        public MarketBuyControl BuyControl;
    }
}
