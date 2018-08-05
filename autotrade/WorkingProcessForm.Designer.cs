namespace autotrade {
    partial class WorkingProcessForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkingProcessForm));
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.StopWorkingProcessButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logTextBox
            // 
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.logTextBox.Location = new System.Drawing.Point(0, 0);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.logTextBox.Size = new System.Drawing.Size(395, 433);
            this.logTextBox.TabIndex = 0;
            this.logTextBox.Text = "";
            // 
            // StopWorkingProcessButton
            // 
            this.StopWorkingProcessButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.StopWorkingProcessButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StopWorkingProcessButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StopWorkingProcessButton.Location = new System.Drawing.Point(118, 439);
            this.StopWorkingProcessButton.Name = "StopWorkingProcessButton";
            this.StopWorkingProcessButton.Size = new System.Drawing.Size(174, 35);
            this.StopWorkingProcessButton.TabIndex = 15;
            this.StopWorkingProcessButton.Text = "Stop working process";
            this.StopWorkingProcessButton.UseVisualStyleBackColor = false;
            this.StopWorkingProcessButton.Click += new System.EventHandler(this.StopWorkingProcessButton_Click);
            // 
            // WorkingProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(395, 482);
            this.Controls.Add(this.StopWorkingProcessButton);
            this.Controls.Add(this.logTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorkingProcessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Working process";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WorkingProcessForm_FormClosing);
            this.Load += new System.EventHandler(this.WorkingProcessForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.Button StopWorkingProcessButton;
    }
}