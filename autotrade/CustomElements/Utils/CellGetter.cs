namespace SteamAutoMarket.CustomElements.Utils
{
    using System.Windows.Forms;

    public static class CellGetter
    {
        public static T GetCellValue<T>(DataGridViewCell cell)
        {
            if (cell == null)
            {
                return default(T);
            }

            var cellValue = cell.Value;
            if (cellValue == null)
            {
                return default(T);
            }

            if (cellValue is T variable)
            {
                return variable;
            }

            return default(T);
        }
    }
}