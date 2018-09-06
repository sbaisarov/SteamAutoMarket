using System;
using System.Drawing;
using System.Windows.Forms;

namespace SteamAutoMarket.CustomElements.Utils
{
    internal class CommonUtils
    {
        public static void AppendBoldText(RichTextBox textBox, string text)
        {
            textBox.SelectionFont = new Font(textBox.Font, FontStyle.Bold);
            textBox.AppendText(text);
            textBox.SelectionFont = new Font(textBox.Font, FontStyle.Regular);
        }

        public static long GetSecondsFromDateTime(DateTime date)
        {
            return (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static DateTime ResetTimeToDauStart(DateTime date)
        {
            date = date.AddHours(-1 * date.Hour);
            date = date.AddMinutes(-1 * date.Minute);
            date = date.AddSeconds(-1 * date.Second);

            return date;
        }

        public static DateTime ParseSteamUnixDate(int date)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(date).ToLocalTime();
            return dtDateTime;
        }

        public static void ClearGrids(params DataGridView[] grids)
        {
            foreach (var grid in grids) grid.Rows.Clear();
        }
    }
}