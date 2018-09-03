using autotrade.CustomElements.Controls.Trade;

namespace autotrade.CustomElements.Tabs
{
    partial class TradeControlTab
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.TabControl = new System.Windows.Forms.TabControl();
            this.SendTradeTab = new System.Windows.Forms.TabPage();
            this.TradeControl = new TradeSendControl();
            this.ActiveTradesTab = new System.Windows.Forms.TabPage();
            this.RecievedTradeManageControl = new RecievedTradeManageControl();
            this.HistoryTradesTab = new System.Windows.Forms.TabPage();
            this.TradeHistoryControl = new TradeHistoryControl();
            this.TabControl.SuspendLayout();
            this.SendTradeTab.SuspendLayout();
            this.ActiveTradesTab.SuspendLayout();
            this.HistoryTradesTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.SendTradeTab);
            this.TabControl.Controls.Add(this.ActiveTradesTab);
            this.TabControl.Controls.Add(this.HistoryTradesTab);
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
            // ActiveTradesTab
            // 
            this.ActiveTradesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.ActiveTradesTab.Controls.Add(this.RecievedTradeManageControl);
            this.ActiveTradesTab.Location = new System.Drawing.Point(4, 29);
            this.ActiveTradesTab.Name = "ActiveTradesTab";
            this.ActiveTradesTab.Size = new System.Drawing.Size(886, 599);
            this.ActiveTradesTab.TabIndex = 1;
            this.ActiveTradesTab.Text = "Active";
            this.ActiveTradesTab.UseVisualStyleBackColor = true;
            // 
            // RecievedTradeManageControl
            // 
            this.RecievedTradeManageControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.RecievedTradeManageControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RecievedTradeManageControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.RecievedTradeManageControl.Location = new System.Drawing.Point(0, 0);
            this.RecievedTradeManageControl.Name = "RecievedTradeManageControl";
            this.RecievedTradeManageControl.Size = new System.Drawing.Size(885, 599);
            this.RecievedTradeManageControl.TabIndex = 0;
            // 
            // HistoryTradesTab
            // 
            this.HistoryTradesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.HistoryTradesTab.Controls.Add(this.TradeHistoryControl);
            this.HistoryTradesTab.Location = new System.Drawing.Point(4, 29);
            this.HistoryTradesTab.Name = "HistoryTradesTab";
            this.HistoryTradesTab.Size = new System.Drawing.Size(886, 599);
            this.HistoryTradesTab.TabIndex = 2;
            this.HistoryTradesTab.Text = "History";
            // 
            // tradeHistoryControl1
            // 
            this.TradeHistoryControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.TradeHistoryControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TradeHistoryControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.TradeHistoryControl.Location = new System.Drawing.Point(0, 0);
            this.TradeHistoryControl.Name = "tradeHistoryControl1";
            this.TradeHistoryControl.Size = new System.Drawing.Size(885, 599);
            this.TradeHistoryControl.TabIndex = 0;
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
            this.ActiveTradesTab.ResumeLayout(false);
            this.HistoryTradesTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage SendTradeTab;
        private System.Windows.Forms.TabPage ActiveTradesTab;
        private System.Windows.Forms.TabPage HistoryTradesTab;
        public TradeSendControl TradeControl;
        public RecievedTradeManageControl RecievedTradeManageControl;
        public TradeHistoryControl TradeHistoryControl;
    }
}
