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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.saleSteamControl1 = new autotrade.SaleSteamControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.saleObskinsControl = new autotrade.SaleObskinsControl();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(799, 527);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.saleSteamControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(791, 501);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "steam";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // saleSteamControl1
            // 
            this.saleSteamControl1.BackColor = System.Drawing.Color.Silver;
            this.saleSteamControl1.Location = new System.Drawing.Point(3, 3);
            this.saleSteamControl1.Name = "saleSteamControl1";
            this.saleSteamControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.saleObskinsControl);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(791, 501);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "obskins";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // saleObskinsControl
            // 
            this.saleObskinsControl.Location = new System.Drawing.Point(0, 0);
            this.saleObskinsControl.Name = "saleObskinsControl";
            this.saleObskinsControl.Size = new System.Drawing.Size(785, 495);
            this.saleObskinsControl.TabIndex = 0;
            // 
            // SaleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.tabControl1);
            this.Name = "SaleControl";
            this.Size = new System.Drawing.Size(799, 530);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private SaleSteamControl saleSteamControl1;
        private SaleObskinsControl saleObskinsControl;
    }
}
