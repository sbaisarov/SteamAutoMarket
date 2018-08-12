namespace autotrade.CustomElements {
    partial class SettingsControlTab {
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
            this.GeneralSettingsTab = new System.Windows.Forms.TabPage();
            this.SettingsControl = new autotrade.CustomElements.GeneralSettingsControl();
            this.TabControl.SuspendLayout();
            this.GeneralSettingsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.GeneralSettingsTab);
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
            // GeneralSettingsTab
            // 
            this.GeneralSettingsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.GeneralSettingsTab.Controls.Add(this.SettingsControl);
            this.GeneralSettingsTab.Location = new System.Drawing.Point(4, 29);
            this.GeneralSettingsTab.Name = "GeneralSettingsTab";
            this.GeneralSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralSettingsTab.Size = new System.Drawing.Size(886, 599);
            this.GeneralSettingsTab.TabIndex = 0;
            this.GeneralSettingsTab.Text = "General";
            // 
            // SettingsControl
            // 
            this.SettingsControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.SettingsControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.SettingsControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SettingsControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(214)))), ((int)(((byte)(201)))));
            this.SettingsControl.Location = new System.Drawing.Point(0, 0);
            this.SettingsControl.Name = "SettingsControl";
            this.SettingsControl.Size = new System.Drawing.Size(885, 599);
            this.SettingsControl.TabIndex = 0;
            // 
            // SettingsControlTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.Controls.Add(this.TabControl);
            this.Name = "SettingsControlTab";
            this.Size = new System.Drawing.Size(894, 632);
            this.TabControl.ResumeLayout(false);
            this.GeneralSettingsTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage GeneralSettingsTab;
        public GeneralSettingsControl SettingsControl;
    }
}
