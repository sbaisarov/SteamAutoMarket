using System;
using System.Drawing;
using System.Windows.Forms;

namespace autotrade.CustomElements.Elements
{
    internal class RichTextBoxWithNoPaint : RichTextBox
    {
        private readonly Color _backColorDisabled = Color.Gainsboro;
        private readonly Color _foreColorDisabled = SystemColors.ControlText;

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (!Enabled)
                SetStyle(ControlStyles.UserPaint, true);
            else
                SetStyle(ControlStyles.UserPaint, false);

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            SolidBrush textBrush;

            if (Enabled)
            {
                textBrush = new SolidBrush(ForeColor);
            }
            else
            {
                var backColorDisabled = _backColorDisabled;
                if (Parent.FindForm() != null) backColorDisabled = Parent.FindForm().BackColor;
                textBrush = new SolidBrush(_foreColorDisabled);
                var backBrush = new SolidBrush(backColorDisabled);
                e.Graphics.FillRectangle(backBrush, ClientRectangle);
            }

            e.Graphics.DrawString(Text, Font, textBrush, 1.0F, 1.0F);
        }
    }
}