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
        public static LoggerLevel LOGGER_LEVEL { get; set; } = LoggerLevel.INFO;
        public const string DATE_FROMAT = "HH:mm:ss"; //dd-MM-yy H:mm:ss

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Working(string message) {
            message = $"{GetCurrentDate()} [WORKING] - {message}";
            File.AppendAllText("log.log", message + "\n");
            LogToLogBox(message);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Debug(string message) {
            if (_ignoreLogs(LoggerLevel.DEBUG)) return;

            message = $"{GetCurrentDate()} [DEBUG] - {message}";
            File.AppendAllText("log.log", message + "\n");
            LogToLogBox(message);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Info(string message) {
            if (_ignoreLogs(LoggerLevel.INFO)) return;

            message = $"{GetCurrentDate()} [INFO] - {message}";
            File.AppendAllText("log.log", message + "\n");
            LogToLogBox(message);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Warning(string message, Exception e = null) {
            if (_ignoreLogs(LoggerLevel.INFO)) return;

            message = $"{GetCurrentDate()} [WARN] - {message} " + ((e != null) ? e.Message + " " + e.StackTrace : "");
            File.AppendAllText("log.log", message + "\n");
            LogToLogBox(message);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Error(string message, Exception e = null) {
            if (_ignoreLogs(LoggerLevel.ERROR)) return;

            message = $"{GetCurrentDate()} [ERROR] - {message}";
            if (e != null) {
                message += $". {e.Message}";
            }

            File.AppendAllText("error.log", message + " " + ((e != null) ? e.Message + " " + e.StackTrace : "") + "\n");
            LogToLogBox(message);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Critical(string message, Exception e) {
            message = $"{GetCurrentDate()} [CRITICAL] - {message}. {e.Message}";
            File.AppendAllText("error.log", $"{message} {e.StackTrace}\n");

            var confirmResult = MessageBox.Show(message, "Critical unhandled exception",
                                      MessageBoxButtons.OK, MessageBoxIcon.Stop);

            Application.Exit();
        }

        public static void LogToLogBox(string s) {
            if (Program.IsMainThread) {
                AppendTextToLogTextBox(s);
            } else {
                Dispatcher.Invoke(Program.MainForm, () => {
                    AppendTextToLogTextBox(s);
                });
            }
        }

        private static void AppendTextToLogTextBox(string s) {
            var textBox = Program.MainForm.SettingsControlTab.SettingsControl.LogTextBox;
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

        private static bool _ignoreLogs(LoggerLevel invoked) {
            switch (invoked) {
                case LoggerLevel.DEBUG: return DEBUG_SHOULD_BE_IGNORED.Contains(invoked);
                case LoggerLevel.INFO: return INFO_SHOULD_BE_IGNORED.Contains(invoked);
                case LoggerLevel.ERROR: return ERROR_SHOULD_BE_IGNORED.Contains(invoked);
                case LoggerLevel.NONE: return NONE_SHOULD_BE_IGNORED.Contains(invoked);
                default: return false;
            }
        }

        private static LoggerLevel[] NONE_SHOULD_BE_IGNORED = { LoggerLevel.DEBUG, LoggerLevel.INFO, LoggerLevel.ERROR, LoggerLevel.NONE };
        private static LoggerLevel[] DEBUG_SHOULD_BE_IGNORED = { LoggerLevel.INFO, LoggerLevel.ERROR, LoggerLevel.NONE };
        private static LoggerLevel[] INFO_SHOULD_BE_IGNORED = { LoggerLevel.ERROR, LoggerLevel.NONE };
        private static LoggerLevel[] ERROR_SHOULD_BE_IGNORED = { LoggerLevel.NONE };


    }

    enum LoggerLevel {
        DEBUG,
        INFO,
        ERROR,
        NONE
    }
}
