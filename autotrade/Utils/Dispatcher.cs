using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autotrade.Utils {
    class Dispatcher {
        public delegate void AsyncAction();

        public delegate void DispatcherInvoker(Form form, AsyncAction a);

        public static void Invoke(Form form, AsyncAction action) {
            if (!form.InvokeRequired) {
                action();
            } else {
                form.Invoke((DispatcherInvoker)Invoke, form, action);
            }
        }
    }
}
