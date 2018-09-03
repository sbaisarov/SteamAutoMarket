namespace autotrade.CustomElements.Forms
{
    partial class WorkingProcessForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkingProcessForm));
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.StopWorkingProcessButton = new System.Windows.Forms.Button();
            this.RightEdge = new System.Windows.Forms.Panel();
            this.BottomEdge = new System.Windows.Forms.Panel();
            this.LeftEdge = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.logTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.logTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.logTextBox.Location = new System.Drawing.Point(0, 0);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.logTextBox.Size = new System.Drawing.Size(441, 433);
            this.logTextBox.TabIndex = 0;
            this.logTextBox.Text = "";
            // 
            // StopWorkingProcessButton
            // 
            this.StopWorkingProcessButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(218)))), ((int)(((byte)(106)))));
            this.StopWorkingProcessButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StopWorkingProcessButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StopWorkingProcessButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(30)))), ((int)(((byte)(34)))));
            this.StopWorkingProcessButton.Location = new System.Drawing.Point(133, 439);
            this.StopWorkingProcessButton.Name = "StopWorkingProcessButton";
            this.StopWorkingProcessButton.Size = new System.Drawing.Size(174, 35);
            this.StopWorkingProcessButton.TabIndex = 15;
            this.StopWorkingProcessButton.Text = "Stop working process";
            this.StopWorkingProcessButton.UseVisualStyleBackColor = false;
            this.StopWorkingProcessButton.Click += new System.EventHandler(this.StopWorkingProcessButton_Click);
            // 
            // RightEdge
            // 
            this.RightEdge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.RightEdge.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightEdge.Location = new System.Drawing.Point(439, 433);
            this.RightEdge.Name = "RightEdge";
            this.RightEdge.Size = new System.Drawing.Size(2, 49);
            this.RightEdge.TabIndex = 21;
            // 
            // BottomEdge
            // 
            this.BottomEdge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.BottomEdge.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomEdge.Location = new System.Drawing.Point(0, 480);
            this.BottomEdge.Name = "BottomEdge";
            this.BottomEdge.Size = new System.Drawing.Size(439, 2);
            this.BottomEdge.TabIndex = 22;
            // 
            // LeftEdge
            // 
            this.LeftEdge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.LeftEdge.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftEdge.Location = new System.Drawing.Point(0, 433);
            this.LeftEdge.Name = "LeftEdge";
            this.LeftEdge.Size = new System.Drawing.Size(2, 47);
            this.LeftEdge.TabIndex = 23;
            // 
            // WorkingProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(441, 482);
            this.Controls.Add(this.LeftEdge);
            this.Controls.Add(this.BottomEdge);
            this.Controls.Add(this.RightEdge);
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
        private System.Windows.Forms.Panel RightEdge;
        private System.Windows.Forms.Panel BottomEdge;
        private System.Windows.Forms.Panel LeftEdge;
    }
}