using System.Windows.Forms;

namespace SteamAutoMarket.CustomElements.Utils
{
    internal class AccountsDataGridUtils
    {
        public static bool IsAccountAreadyExist(DataGridView accountsDataGridView, string login)
        {
            for (var i = 0; i < accountsDataGridView.RowCount; i++)
                if ((string) GetDataGridViewLoginCell(accountsDataGridView, i).Value == login)
                    return true;
            return false;
        }

        public static DataGridViewImageCell GetDataGridViewImageCell(DataGridView accountsDataGridView, int row)
        {
            return (DataGridViewImageCell) accountsDataGridView.Rows[row].Cells[0];
        }

        public static DataGridViewTextBoxCell GetDataGridViewLoginCell(DataGridView accountsDataGridView, int row)
        {
            return (DataGridViewTextBoxCell) accountsDataGridView.Rows[row].Cells[1];
        }

        public static DataGridViewTextBoxCell GetDataGridViewPasswordCell(DataGridView accountsDataGridView, int row)
        {
            return (DataGridViewTextBoxCell) accountsDataGridView.Rows[row].Cells[2];
        }

        public static DataGridViewTextBoxCell GetDataGridViewSteamApiCell(DataGridView accountsDataGridView, int row)
        {
            return (DataGridViewTextBoxCell) accountsDataGridView.Rows[row].Cells[3];
        }

        public static DataGridViewTextBoxCell GetDataGridViewMafileHidenCell(DataGridView accountsDataGridView, int row)
        {
            return (DataGridViewTextBoxCell) accountsDataGridView.Rows[row].Cells[4];
        }

        public static DataGridViewTextBoxCell GetDataGridViewTruePasswordHidenCell(DataGridView accountsDataGridView,
            int row)
        {
            return (DataGridViewTextBoxCell) accountsDataGridView.Rows[row].Cells[5];
        }
    }
}