using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autotrade.Utils {
    class Logger {
        public static int LoggerLevel { get; set; } = 0;// 0 info + errors, 1 errors, 2 none
        private const string DATE_FROMAT = "HH:mm:ss"; //dd-MM-yy H:mm:ss

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Info(string message) {
            if (LoggerLevel > 0) return;

            message = $"{GetCurrentDate()} [INFO] - {message}";
            File.AppendAllText("log.log", message);
            LogToLogBox(message);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Warning(string message) {
            if (LoggerLevel > 0) return;

            message = $"{GetCurrentDate()} [WARN] - {message}";
            File.AppendAllText("log.log", message);
            LogToLogBox(message);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Error(string message, Exception e = null) {
            if (LoggerLevel > 1) return;

            message = $"{GetCurrentDate()} [ERROR] - {message}";
            if (e != null) {
                message += $". {e.Message} {e.StackTrace}";
            }

            File.AppendAllText("error.log", message);
            LogToLogBox(message);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Critical(string message, Exception e = null) {
            message = $"{GetCurrentDate()} [CRITICAL] - {message}. {e.Message} {e.StackTrace}";
            File.AppendAllText("error.log", message);

            var confirmResult = MessageBox.Show(message, "Critical unhandled exception",
                                      MessageBoxButtons.OK, MessageBoxIcon.Stop);

            Application.Exit();
        }

        public static void LogToLogBox(string s) {
            if (Program.IsMainThread) {
                AppendTextToLogTextBox(s);
            }
            else {
                Dispatcher.Invoke(Program.MainForm, () => {
                    AppendTextToLogTextBox(s);
                });
            }
        }

        private static void AppendTextToLogTextBox(string s) {
            var textBox = Program.MainForm.SettingsControl.LogTextBox;
            if (textBox.Lines.Count() > 1000) {
                var list = textBox.Lines.ToList();
                list.RemoveRange(0, 500);
                textBox.Lines = list.ToArray();
            }
            textBox.AppendText(s + "\n");
        }

        public static string GetCurrentDate() {
            return DateTime.Now.ToString(DATE_FROMAT);
        }
    }
}
