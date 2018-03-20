using System.Windows.Forms;

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
            this.components = new System.ComponentModel.Container();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.inventTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.nextButton = new System.Windows.Forms.Button();
            this.inventTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(288, 406);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.Click += new System.EventHandler(this.onclickListItemView1);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(70, 70);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // inventTabControl
            // 
            this.inventTabControl.Controls.Add(this.tabPage1);
            this.inventTabControl.Controls.Add(this.tabPage2);
            this.inventTabControl.Location = new System.Drawing.Point(3, 0);
            this.inventTabControl.Name = "inventTabControl";
            this.inventTabControl.SelectedIndex = 0;
            this.inventTabControl.Size = new System.Drawing.Size(557, 438);
            this.inventTabControl.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.nextButton);
            this.tabPage1.Controls.Add(this.listView2);
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(549, 412);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "steam";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            this.listView2.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.listView2.Location = new System.Drawing.Point(297, 3);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(250, 361);
            this.listView2.TabIndex = 5;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.Click += new System.EventHandler(this.onclickListItemView2);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(549, 412);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "obskins";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(456, 383);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 6;
            this.nextButton.Text = "next";
            this.nextButton.UseVisualStyleBackColor = true;
            // 
            // SaleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.inventTabControl);
            this.Name = "SaleControl";
            this.Size = new System.Drawing.Size(564, 441);
            this.Load += new System.EventHandler(this.SaleControl_Load);
            this.inventTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        
        public void setSizeListView1(int width, int height)
        {
            this.listView1.Size = new System.Drawing.Size(width, height);
        }

        public void setSizeListView2(int width, int height)
        {
            this.listView2.Size = new System.Drawing.Size(width, height);
        }

        public void setSizeTabControll(int width, int height)
        {
            this.inventTabControl.Size = new System.Drawing.Size(width, height);
        }

        public void setPoint(int x, int y)
        {
            this.listView2.Location = new System.Drawing.Point(x, y);
        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl inventTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listView2;
        private Button nextButton;
    }
}