﻿namespace autotrade
{
    partial class Form1
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.borderTopPanel = new System.Windows.Forms.Panel();
            this.borderLeftPanel = new System.Windows.Forms.Panel();
            this.borderRightPanel = new System.Windows.Forms.Panel();
            this.borderBottomPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.appCurtailButton = new System.Windows.Forms.Button();
            this.appExpandButton = new System.Windows.Forms.Button();
            this.appExitButton = new System.Windows.Forms.Button();
            this.leftHeaderPanel = new System.Windows.Forms.Panel();
            this.leftPanelHideShowButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.leftHeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // borderTopPanel
            // 
            this.borderTopPanel.BackColor = System.Drawing.Color.RoyalBlue;
            this.borderTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.borderTopPanel.Location = new System.Drawing.Point(0, 0);
            this.borderTopPanel.Name = "borderTopPanel";
            this.borderTopPanel.Size = new System.Drawing.Size(912, 1);
            this.borderTopPanel.TabIndex = 0;
            // 
            // borderLeftPanel
            // 
            this.borderLeftPanel.BackColor = System.Drawing.Color.RoyalBlue;
            this.borderLeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.borderLeftPanel.Location = new System.Drawing.Point(0, 1);
            this.borderLeftPanel.Name = "borderLeftPanel";
            this.borderLeftPanel.Size = new System.Drawing.Size(1, 560);
            this.borderLeftPanel.TabIndex = 1;
            // 
            // borderRightPanel
            // 
            this.borderRightPanel.BackColor = System.Drawing.Color.RoyalBlue;
            this.borderRightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.borderRightPanel.Location = new System.Drawing.Point(911, 1);
            this.borderRightPanel.Name = "borderRightPanel";
            this.borderRightPanel.Size = new System.Drawing.Size(1, 560);
            this.borderRightPanel.TabIndex = 1;
            // 
            // borderBottomPanel
            // 
            this.borderBottomPanel.BackColor = System.Drawing.Color.RoyalBlue;
            this.borderBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.borderBottomPanel.Location = new System.Drawing.Point(0, 561);
            this.borderBottomPanel.Name = "borderBottomPanel";
            this.borderBottomPanel.Size = new System.Drawing.Size(912, 1);
            this.borderBottomPanel.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.appCurtailButton);
            this.panel1.Controls.Add(this.appExpandButton);
            this.panel1.Controls.Add(this.appExitButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(910, 31);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(91, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "\"Obskins\" Auto trade ";
            // 
            // appCurtailButton
            // 
            this.appCurtailButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.appCurtailButton.FlatAppearance.BorderSize = 0;
            this.appCurtailButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appCurtailButton.Image = ((System.Drawing.Image)(resources.GetObject("appCurtailButton.Image")));
            this.appCurtailButton.Location = new System.Drawing.Point(790, 9);
            this.appCurtailButton.Name = "appCurtailButton";
            this.appCurtailButton.Size = new System.Drawing.Size(26, 17);
            this.appCurtailButton.TabIndex = 6;
            this.appCurtailButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.appCurtailButton.UseVisualStyleBackColor = true;
            // 
            // appExpandButton
            // 
            this.appExpandButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.appExpandButton.FlatAppearance.BorderSize = 0;
            this.appExpandButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appExpandButton.Image = ((System.Drawing.Image)(resources.GetObject("appExpandButton.Image")));
            this.appExpandButton.Location = new System.Drawing.Point(831, 6);
            this.appExpandButton.Name = "appExpandButton";
            this.appExpandButton.Size = new System.Drawing.Size(25, 20);
            this.appExpandButton.TabIndex = 5;
            this.appExpandButton.UseVisualStyleBackColor = true;
            // 
            // appExitButton
            // 
            this.appExitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.appExitButton.FlatAppearance.BorderSize = 0;
            this.appExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.appExitButton.Image = ((System.Drawing.Image)(resources.GetObject("appExitButton.Image")));
            this.appExitButton.Location = new System.Drawing.Point(874, 4);
            this.appExitButton.Name = "appExitButton";
            this.appExitButton.Size = new System.Drawing.Size(21, 22);
            this.appExitButton.TabIndex = 4;
            this.appExitButton.UseVisualStyleBackColor = true;
            this.appExitButton.Click += new System.EventHandler(this.appExitButton_Click);
            // 
            // leftHeaderPanel
            // 
            this.leftHeaderPanel.BackColor = System.Drawing.Color.RoyalBlue;
            this.leftHeaderPanel.Controls.Add(this.leftPanelHideShowButton);
            this.leftHeaderPanel.Controls.Add(this.button2);
            this.leftHeaderPanel.Controls.Add(this.button1);
            this.leftHeaderPanel.Controls.Add(this.label2);
            this.leftHeaderPanel.Controls.Add(this.pictureBox1);
            this.leftHeaderPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftHeaderPanel.Location = new System.Drawing.Point(1, 32);
            this.leftHeaderPanel.Name = "leftHeaderPanel";
            this.leftHeaderPanel.Size = new System.Drawing.Size(166, 529);
            this.leftHeaderPanel.TabIndex = 3;
            this.leftHeaderPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // leftPanelHideShowButton
            // 
            this.leftPanelHideShowButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.leftPanelHideShowButton.FlatAppearance.BorderSize = 0;
            this.leftPanelHideShowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.leftPanelHideShowButton.Image = ((System.Drawing.Image)(resources.GetObject("leftPanelHideShowButton.Image")));
            this.leftPanelHideShowButton.Location = new System.Drawing.Point(128, 3);
            this.leftPanelHideShowButton.Name = "leftPanelHideShowButton";
            this.leftPanelHideShowButton.Size = new System.Drawing.Size(26, 27);
            this.leftPanelHideShowButton.TabIndex = 8;
            this.leftPanelHideShowButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.leftPanelHideShowButton.UseVisualStyleBackColor = true;
            this.leftPanelHideShowButton.Click += new System.EventHandler(this.leftPanelHideShowButton_Click);
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(-2, 170);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(151, 38);
            this.button2.TabIndex = 9;
            this.button2.Text = "  Покупка";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(0, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 36);
            this.button1.TabIndex = 8;
            this.button1.Text = "  Продажа";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(42, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Obskins";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(50, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 78);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 562);
            this.Controls.Add(this.leftHeaderPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.borderLeftPanel);
            this.Controls.Add(this.borderRightPanel);
            this.Controls.Add(this.borderBottomPanel);
            this.Controls.Add(this.borderTopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.leftHeaderPanel.ResumeLayout(false);
            this.leftHeaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel borderTopPanel;
        private System.Windows.Forms.Panel borderLeftPanel;
        private System.Windows.Forms.Panel borderRightPanel;
        private System.Windows.Forms.Panel borderBottomPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel leftHeaderPanel;
        private System.Windows.Forms.Button appExitButton;
        private System.Windows.Forms.Button appExpandButton;
        private System.Windows.Forms.Button appCurtailButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button leftPanelHideShowButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

