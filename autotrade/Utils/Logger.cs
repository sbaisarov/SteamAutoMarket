using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamAutoMarket.Utils
{
    internal class Logger
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void LogToFile(string s)
        {
            Task.Run(() =>
            {
                try
                {
                    File.AppendAllText("log.log", $@"{s}\n");
                }
                catch
                {
                    Thread.Sleep(1000);
                    LogToFile(s);
                }
            });
        }

        public static void Working(string message)
        {
            message = $"{GetCurrentDate()} [WORKING] - {message}";
            LogToLogBox(message);
        }

        public static void Debug(string message)
        {
            if (_ignoreLogs(LoggerLevel.Debug)) return;

            message = $"{GetCurrentDate()} [DEBUG] - {message}";
            LogToFile(message);
            LogToLogBox(message);
        }

        public static void Info(string message)
        {
            if (_ignoreLogs(LoggerLevel.Info)) return;

            message = $"{GetCurrentDate()} [INFO] - {message}";
            LogToFile(message);
            LogToLogBox(message);
        }


        public static void Warning(string message, Exception e = null)
        {
            if (_ignoreLogs(LoggerLevel.Info)) return;

            message = $"{GetCurrentDate()} [WARN] - {message}";

            LogToLogBox(message);
            LogToFile(message + (e != null ? e.Message + " " + e.StackTrace : ""));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Error(string message, Exception e = null)
        {
            if (_ignoreLogs(LoggerLevel.Error)) return;

            message = $"{GetCurrentDate()} [ERROR] - {message}";
            if (e != null) message += $". {e.Message}";

            File.AppendAllText("error.log", message + @" " + (e != null ? e.Message + " " + e.StackTrace : "") + @"\n");
            LogToLogBox(message);
        }

        public static void Critical(string message, Exception ex)
        {
            message = $"{GetCurrentDate()} [CRITICAL] - {message}. {ex.Message}";
            File.AppendAllText("error.log", $@"{message} {ex.StackTrace}\n");

            MessageBox.Show(message, message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void LogToLogBox(string s)
        {
            if (Program.IsMainThread)
                AppendTextToLogTextBox(s);
            else
                Dispatcher.AsMainForm(() => { AppendTextToLogTextBox(s); });
        }

        private static void AppendTextToLogTextBox(string s)
        {
            var textBox = Program.MainForm.SettingsControlTab.SettingsControl.LogTextBox;
            if (textBox.Lines.Count() > 1000)
            {
                var list = textBox.Lines.ToList();
                list.RemoveRange(0, 500);
                textBox.Lines = list.ToArray();
            }

            textBox.AppendText(s + "\n");
        }

        public static string GetCurrentDate()
        {
            return DateTime.Now.ToString(DateFormat);
        }

        private static bool _ignoreLogs(LoggerLevel invoked)
        {
            switch (invoked)
            {
                case LoggerLevel.Debug: return DebugShouldBeIgnored.Contains(invoked);
                case LoggerLevel.Info: return InfoShouldBeIgnored.Contains(invoked);
                case LoggerLevel.Error: return ErrorShouldBeIgnored.Contains(invoked);
                case LoggerLevel.None: return NoneShouldBeIgnored.Contains(invoked);
                default: return false;
            }
        }

        public const string DateFormat = "HH:mm:ss";

        private static readonly LoggerLevel[] NoneShouldBeIgnored =
            {LoggerLevel.Debug, LoggerLevel.Info, LoggerLevel.Error, LoggerLevel.None};

        private static readonly LoggerLevel[] DebugShouldBeIgnored =
            {LoggerLevel.Info, LoggerLevel.Error, LoggerLevel.None};

        private static readonly LoggerLevel[] InfoShouldBeIgnored = { LoggerLevel.Error, LoggerLevel.None };
        private static readonly LoggerLevel[] ErrorShouldBeIgnored = { LoggerLevel.None };

        public static LoggerLevel LoggerLevel { get; set; } = LoggerLevel.Info;
    }

    internal enum LoggerLevel
    {
        Debug,
        Info,
        Error,
        None
    }
}