using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autotrade {
    class Logger {
        public static void Debug(string message) {
            message = DateTime.Now.ToShortDateString() + message;
            File.AppendAllText("debug.log", message);
        }

        public static void Error(string message) {
            message = DateTime.Now.ToShortDateString() + message;
            File.AppendAllText("error.log", message);
        }

        public static void Error(string message, Exception e) {
            message = DateTime.Now.ToShortDateString() + message + e.Message + " " + e.StackTrace;
            File.AppendAllText("error.log", message);
        }
    }
}
