namespace autotrade
{
    partial class SaleControl
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
            this.twoTabsElement = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.saleObskinsControl1 = new autotrade.SaleObskinsControl();
            this.saleSteamControl2 = new autotrade.SaleSteamControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.twoTabsElement.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // twoTabsElement
            // 
            this.twoTabsElement.Controls.Add(this.tabPage1);
            this.twoTabsElement.Controls.Add(this.tabPage2);
            this.twoTabsElement.Location = new System.Drawing.Point(3, 3);
            this.twoTabsElement.Name = "twoTabsElement";
            this.twoTabsElement.SelectedIndex = 0;
            this.twoTabsElement.Size = new System.Drawing.Size(763, 596);
            this.twoTabsElement.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.saleObskinsControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(803, 616);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "obskins";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // saleObskinsControl1
            // 
            this.saleObskinsControl1.Location = new System.Drawing.Point(0, 2);
            this.saleObskinsControl1.Name = "saleObskinsControl1";
            this.saleObskinsControl1.Size = new System.Drawing.Size(791, 498);
            this.saleObskinsControl1.TabIndex = 0;
            // 
            // saleSteamControl2
            // 
            this.saleSteamControl2.BackColor = System.Drawing.Color.Silver;
            this.saleSteamControl2.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saleSteamControl2.Location = new System.Drawing.Point(0, 0);
            this.saleSteamControl2.Name = "saleSteamControl2";
            this.saleSteamControl2.Size = new System.Drawing.Size(754, 575);
            this.saleSteamControl2.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.saleSteamControl2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(755, 570);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "steam";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // SaleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.twoTabsElement);
            this.Name = "SaleControl";
            this.Size = new System.Drawing.Size(765, 599);
            this.twoTabsElement.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl twoTabsElement;
        private System.Windows.Forms.TabPage tabPage2;
        private SaleSteamControl saleSteamControl1;
        private SaleObskinsControl saleObskinsElement;
        private SaleObskinsControl saleObskinsControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private SaleSteamControl saleSteamControl2;
    }
}
