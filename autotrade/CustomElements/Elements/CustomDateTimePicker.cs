using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using autotrade.Utils;

namespace autotrade.CustomElements.Elements
{
    internal class CustomDateTimePicker : DateTimePicker
    {
        public CustomDateTimePicker()
        {
            SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(FormComponents.SIMPLE_BACK_COLOR), ClientRectangle);

            var pen = new Pen(SystemColors.MenuBar, 2)
            {
                Alignment = PenAlignment.Center
            };
            e.Graphics.DrawRectangle(pen, ClientRectangle);

            e.Graphics.DrawString(Value.ToString(CustomFormat), Font, new SolidBrush(FormComponents.SIMPLE_TEXT_COLOR),
                2, 4);

            ComboBoxRenderer.DrawDropDownButton(e.Graphics,
                new Rectangle(
                    new Point(ClientRectangle.X + ClientRectangle.Width - 20, ClientRectangle.Y),
                    new Size(20, 20)),
                ComboBoxState.Normal);
        }
    }
}