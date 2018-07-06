using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autotrade.Utils {
    class Dispatcher {
        public delegate void AsyncAction();

        public delegate void DispatcherInvoker(Form1 form, AsyncAction a);

        public static void Invoke(Form1 form, AsyncAction action) {
            if (!form.InvokeRequired) {
                action();
            } else {
                form.Invoke((DispatcherInvoker)Invoke, form, action);
            }
        }
    }
}
