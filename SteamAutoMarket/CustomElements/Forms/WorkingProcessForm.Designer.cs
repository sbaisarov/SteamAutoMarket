namespace SteamAutoMarket.CustomElements.Forms
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkingProcessForm));
            this.LogTextBox = new System.Windows.Forms.RichTextBox();
            this.StopWorkingProcessButton = new System.Windows.Forms.Button();
            this.RightEdge = new System.Windows.Forms.Panel();
            this.BottomEdge = new System.Windows.Forms.Panel();
            this.LeftEdge = new System.Windows.Forms.Panel();
            this.ScrollCheckBox = new System.Windows.Forms.CheckBox();
            this.AddAllToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // LogTextBox
            // 
            this.LogTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(63)))), ((int)(((byte)(77)))));
            this.LogTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.LogTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LogTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.LogTextBox.Location = new System.Drawing.Point(0, 0);
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.LogTextBox.Size = new System.Drawing.Size(441, 433);
            this.LogTextBox.TabIndex = 0;
            this.LogTextBox.Text = "";
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
            this.StopWorkingProcessButton.Click += new System.EventHandler(this.StopWorkingProcessButtonClick);
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
            // ScrollCheckBox
            // 
            this.ScrollCheckBox.AutoSize = true;
            this.ScrollCheckBox.Checked = true;
            this.ScrollCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ScrollCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(220)))));
            this.ScrollCheckBox.Location = new System.Drawing.Point(384, 462);
            this.ScrollCheckBox.Name = "ScrollCheckBox";
            this.ScrollCheckBox.Size = new System.Drawing.Size(52, 17);
            this.ScrollCheckBox.TabIndex = 24;
            this.ScrollCheckBox.Text = "Scroll";
            this.AddAllToolTip.SetToolTip(this.ScrollCheckBox, "Selection will remain on the cursor if disabled");
            this.ScrollCheckBox.UseVisualStyleBackColor = true;
            // 
            // AddAllToolTip
            // 
            this.AddAllToolTip.AutomaticDelay = 100;
            this.AddAllToolTip.AutoPopDelay = 50000;
            this.AddAllToolTip.InitialDelay = 100;
            this.AddAllToolTip.IsBalloon = true;
            this.AddAllToolTip.ReshowDelay = 20;
            this.AddAllToolTip.UseAnimation = false;
            this.AddAllToolTip.UseFading = false;
            // 
            // WorkingProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(441, 482);
            this.Controls.Add(this.ScrollCheckBox);
            this.Controls.Add(this.LeftEdge);
            this.Controls.Add(this.BottomEdge);
            this.Controls.Add(this.RightEdge);
            this.Controls.Add(this.StopWorkingProcessButton);
            this.Controls.Add(this.LogTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WorkingProcessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Working process";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WorkingProcessFormFormClosing);
            this.Load += new System.EventHandler(this.WorkingProcessFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox LogTextBox;
        private System.Windows.Forms.Button StopWorkingProcessButton;
        private System.Windows.Forms.Panel RightEdge;
        private System.Windows.Forms.Panel BottomEdge;
        private System.Windows.Forms.Panel LeftEdge;
        private System.Windows.Forms.CheckBox ScrollCheckBox;
        private System.Windows.Forms.ToolTip AddAllToolTip;
    }
}