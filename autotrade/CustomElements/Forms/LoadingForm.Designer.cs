namespace autotrade {
    partial class LoadingForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.StopWorkingProcessButton = new System.Windows.Forms.Button();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.TotalItemsLable = new System.Windows.Forms.Label();
            this.PageLable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // StopWorkingProcessButton
            // 
            this.StopWorkingProcessButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.StopWorkingProcessButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StopWorkingProcessButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StopWorkingProcessButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.StopWorkingProcessButton.Location = new System.Drawing.Point(109, 97);
            this.StopWorkingProcessButton.Name = "StopWorkingProcessButton";
            this.StopWorkingProcessButton.Size = new System.Drawing.Size(174, 35);
            this.StopWorkingProcessButton.TabIndex = 15;
            this.StopWorkingProcessButton.Text = "Stop working process";
            this.StopWorkingProcessButton.UseVisualStyleBackColor = false;
            this.StopWorkingProcessButton.Click += new System.EventHandler(this.StopWorkingProcessButton_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.ProgressBar.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.ProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.ProgressBar.Location = new System.Drawing.Point(15, 60);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(362, 31);
            this.ProgressBar.TabIndex = 16;
            // 
            // TotalItemsLable
            // 
            this.TotalItemsLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TotalItemsLable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.TotalItemsLable.Location = new System.Drawing.Point(0, 9);
            this.TotalItemsLable.Name = "TotalItemsLable";
            this.TotalItemsLable.Size = new System.Drawing.Size(395, 18);
            this.TotalItemsLable.TabIndex = 17;
            this.TotalItemsLable.Text = "Total items count - 0";
            this.TotalItemsLable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PageLable
            // 
            this.PageLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PageLable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.PageLable.Location = new System.Drawing.Point(3, 33);
            this.PageLable.Name = "PageLable";
            this.PageLable.Size = new System.Drawing.Size(392, 18);
            this.PageLable.TabIndex = 18;
            this.PageLable.Text = "Page 0 of 0 loaded";
            this.PageLable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(395, 140);
            this.Controls.Add(this.PageLable);
            this.Controls.Add(this.TotalItemsLable);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.StopWorkingProcessButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loading";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InventoryLoadingForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button StopWorkingProcessButton;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label TotalItemsLable;
        private System.Windows.Forms.Label PageLable;
    }
}