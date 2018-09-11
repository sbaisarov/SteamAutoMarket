namespace SteamAutoMarket.CustomElements.Utils
{
    using System.Windows.Forms;

    internal class AccountsDataGridUtils
    {
        public static bool IsAccountAlreadyExist(DataGridView accountsDataGridView, string login)
        {
            for (var i = 0; i < accountsDataGridView.RowCount; i++)
            {
                if ((string)GetDataGridViewLoginCell(accountsDataGridView, i).Value == login)
                {
                    return true;
                }
            }

            return false;
        }

        public static DataGridViewImageCell GetDataGridViewImageCell(DataGridView accountsDataGridView, int row)
        {
            return (DataGridViewImageCell)accountsDataGridView.Rows[row].Cells[0];
        }

        public static DataGridViewTextBoxCell GetDataGridViewLoginCell(DataGridView accountsDataGridView, int row)
        {
            return (DataGridViewTextBoxCell)accountsDataGridView.Rows[row].Cells[1];
        }

        public static DataGridViewTextBoxCell GetDataGridViewPasswordCell(DataGridView accountsDataGridView, int row)
        {
            return (DataGridViewTextBoxCell)accountsDataGridView.Rows[row].Cells[2];
        }

        public static DataGridViewTextBoxCell GetDataGridViewSteamApiCell(DataGridView accountsDataGridView, int row)
        {
            return (DataGridViewTextBoxCell)accountsDataGridView.Rows[row].Cells[3];
        }

        public static DataGridViewTextBoxCell GetDataGridViewMafileHiddenCell(
            DataGridView accountsDataGridView,
            int row)
        {
            return (DataGridViewTextBoxCell)accountsDataGridView.Rows[row].Cells[4];
        }

        public static DataGridViewTextBoxCell GetDataGridViewTruePasswordHiddenCell(
            DataGridView accountsDataGridView,
            int row)
        {
            return (DataGridViewTextBoxCell)accountsDataGridView.Rows[row].Cells[5];
        }
    }
}