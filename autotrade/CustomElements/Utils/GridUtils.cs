using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autotrade.CustomElements.Utils {
    class GridUtils {
        public static void ClearGrids(params DataGridView[] grids) {
            foreach (var grid in grids) {
                grid.Rows.Clear();
            }
        }

        public static DateTime ParseSteamUnixDate(int date) {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(date).ToLocalTime();
            return dtDateTime;
        }
    }
}
