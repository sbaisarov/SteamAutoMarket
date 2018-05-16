using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autotrade.CustomElements {
    class MainFormUtils {
        public static void MoveSidePandelToButton(Panel panel, Button button) {
            panel.Height = button.Height;
            panel.Top = button.Top;
        }
    }
}
