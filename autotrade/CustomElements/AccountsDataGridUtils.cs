using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autotrade.CustomElements {
    class AccountsDataGridUtils {
        public static DataGridViewImageCell GetDataGridViewImageCell(DataGridView accountsDataGridView, int row) {
            return (DataGridViewImageCell)accountsDataGridView.Rows[row].Cells[0];
        }

        public static DataGridViewTextBoxCell GetDataGridViewLoginCell(DataGridView accountsDataGridView, int row) {
            return (DataGridViewTextBoxCell)accountsDataGridView.Rows[row].Cells[1];
        }

        public static DataGridViewTextBoxCell GetDataGridViewPasswordCell(DataGridView accountsDataGridView, int row) {
            return (DataGridViewTextBoxCell)accountsDataGridView.Rows[row].Cells[2];
        }

        public static DataGridViewTextBoxCell GetDataGridViewOpskinsApiCell(DataGridView accountsDataGridView, int row) {
            return (DataGridViewTextBoxCell)accountsDataGridView.Rows[row].Cells[3];
        }

        public static DataGridViewTextBoxCell GetDataGridViewMafileHidenCell(DataGridView accountsDataGridView, int row) {
            return (DataGridViewTextBoxCell)accountsDataGridView.Rows[row].Cells[4];
        }

    }
}
