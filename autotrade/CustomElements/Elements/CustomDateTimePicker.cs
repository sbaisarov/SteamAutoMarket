using autotrade.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace autotrade.CustomElements.Elements {
    class CustomDateTimePicker : DateTimePicker {

        protected override void OnPaint(PaintEventArgs e) {
            e.Graphics.FillRectangle(new SolidBrush(FormComponents.SIMPLE_BACK_COLOR), this.ClientRectangle);

            Pen pen = new Pen(SystemColors.MenuBar, 2)
            {
                Alignment = PenAlignment.Center
            };
            e.Graphics.DrawRectangle(pen, this.ClientRectangle);

            e.Graphics.DrawString(this.Value.ToString(this.CustomFormat), this.Font, new SolidBrush(FormComponents.SIMPLE_TEXT_COLOR), 2, 4);

            ComboBoxRenderer.DrawDropDownButton(e.Graphics,
                new Rectangle(
                    new Point(this.ClientRectangle.X + this.ClientRectangle.Width - 20, this.ClientRectangle.Y),
                    new Size(20, 20)),
                ComboBoxState.Normal);
        }

        public CustomDateTimePicker() {
            this.SetStyle(ControlStyles.UserPaint, true);
        }
    }
}
