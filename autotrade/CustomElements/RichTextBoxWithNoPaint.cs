using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autotrade.CustomElements {
    class RichTextBoxWithNoPaint : RichTextBox {
        private Color _backColorDisabled = Color.Gainsboro;
        private Color _foreColorDisabled = SystemColors.ControlText;

        protected override void OnEnabledChanged(EventArgs e) {
            base.OnEnabledChanged(e);
            if (!(this.Enabled)) {
                this.SetStyle(ControlStyles.UserPaint, true);
            } else {
                this.SetStyle(ControlStyles.UserPaint, false);
            }

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            SolidBrush textBrush;

            if (this.Enabled) {
                textBrush = new SolidBrush(this.ForeColor);
            } else {
                Color backColorDisabled = this._backColorDisabled;
                if (this.Parent.FindForm() != null) {
                    backColorDisabled = this.Parent.FindForm().BackColor;
                }
                textBrush = new SolidBrush(this._foreColorDisabled);
                SolidBrush backBrush = new SolidBrush(backColorDisabled);
                e.Graphics.FillRectangle(backBrush, ClientRectangle);
            }

            e.Graphics.DrawString(this.Text, this.Font, textBrush, 1.0F, 1.0F);
        }
    }
}
