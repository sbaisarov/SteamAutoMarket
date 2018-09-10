namespace SteamAutoMarket.CustomElements.Elements
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class RichTextBoxWithNoPaint : RichTextBox
    {
        private readonly Color backColorDisabled = Color.Gainsboro;
        private readonly Color foreColorDisabled = SystemColors.ControlText;

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            this.SetStyle(ControlStyles.UserPaint, !this.Enabled);

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            SolidBrush textBrush;

            if (this.Enabled)
            {
                textBrush = new SolidBrush(this.ForeColor);
            }
            else
            {
                var form = this.Parent.FindForm();
                if (form != null)
                {
                    form.BackColor = this.backColorDisabled;
                }

                textBrush = new SolidBrush(this.foreColorDisabled);
                var backBrush = new SolidBrush(this.backColorDisabled);
                e.Graphics.FillRectangle(backBrush, this.ClientRectangle);
            }

            e.Graphics.DrawString(this.Text, this.Font, textBrush, 1.0F, 1.0F);
        }
    }
}