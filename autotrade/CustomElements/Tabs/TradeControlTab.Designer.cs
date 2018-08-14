namespace autotrade.CustomElements.Tabs {
    partial class TradeControlTab {
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
            this.SendTradeTab = new System.Windows.Forms.TabPage();
            this.TradeControl = new autotrade.CustomElements.TradeSendControl();
            this.RecievedTradesTab = new System.Windows.Forms.TabPage();
            this.recievedTradeManageControl1 = new autotrade.CustomElements.Controls.RecievedTradeManageControl();
            this.TabControl.SuspendLayout();
            this.SendTradeTab.SuspendLayout();
            this.RecievedTradesTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.SendTradeTab);
            this.TabControl.Controls.Add(this.RecievedTradesTab);
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
            // SendTradeTab
            // 
            this.SendTradeTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.SendTradeTab.Controls.Add(this.TradeControl);
            this.SendTradeTab.Location = new System.Drawing.Point(4, 29);
            this.SendTradeTab.Name = "SendTradeTab";
            this.SendTradeTab.Padding = new System.Windows.Forms.Padding(3);
            this.SendTradeTab.Size = new System.Drawing.Size(886, 599);
            this.SendTradeTab.TabIndex = 0;
            this.SendTradeTab.Text = "Send";
            // 
            // TradeControl
            // 
            this.TradeControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.TradeControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TradeControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.TradeControl.Location = new System.Drawing.Point(0, 0);
            this.TradeControl.Name = "TradeControl";
            this.TradeControl.Size = new System.Drawing.Size(885, 599);
            this.TradeControl.TabIndex = 0;
            // 
            // RecievedTradesTab
            // 
            this.RecievedTradesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.RecievedTradesTab.Controls.Add(this.recievedTradeManageControl1);
            this.RecievedTradesTab.Location = new System.Drawing.Point(4, 29);
            this.RecievedTradesTab.Name = "RecievedTradesTab";
            this.RecievedTradesTab.Size = new System.Drawing.Size(886, 599);
            this.RecievedTradesTab.TabIndex = 1;
            this.RecievedTradesTab.Text = "Recieved";
            // 
            // recievedTradeManageControl1
            // 
            this.recievedTradeManageControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.recievedTradeManageControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.recievedTradeManageControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.recievedTradeManageControl1.Location = new System.Drawing.Point(0, 0);
            this.recievedTradeManageControl1.Name = "recievedTradeManageControl1";
            this.recievedTradeManageControl1.Size = new System.Drawing.Size(885, 599);
            this.recievedTradeManageControl1.TabIndex = 0;
            // 
            // TradeControlTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.Controls.Add(this.TabControl);
            this.Name = "TradeControlTab";
            this.Size = new System.Drawing.Size(894, 632);
            this.TabControl.ResumeLayout(false);
            this.SendTradeTab.ResumeLayout(false);
            this.RecievedTradesTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage SendTradeTab;
        public TradeSendControl TradeControl;
        private System.Windows.Forms.TabPage RecievedTradesTab;
        private Controls.RecievedTradeManageControl recievedTradeManageControl1;
    }
}
