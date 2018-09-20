namespace SteamAutoMarket.Utils
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Net;
    
    using SteamAutoMarket.WorkingProcess.Settings;

    internal enum LoggerLevel
    {
        Debug = 0,

        Info = 1,

        Error = 2,

        None = 3
    }

    internal static class Logger
    {
        public const string DateFormat = "HH:mm:ss";

        static Logger()
        {
            try
            {
                Directory.CreateDirectory("logs");
                switch (SavedSettings.Get().SettingsLoggerLevel)
                {
                    case 0:
                        CurrentLoggerLevel = LoggerLevel.Debug;
                        break;
                    case 1:
                        CurrentLoggerLevel = LoggerLevel.Info;
                        break;
                    case 2:
                        CurrentLoggerLevel = LoggerLevel.Error;
                        break;
                    case 3:
                        CurrentLoggerLevel = LoggerLevel.None;
                        break;

                    default:
                        CurrentLoggerLevel = LoggerLevel.Info;
                        break;
                }
            }
            catch
            {
                // ignored
            }
        }

        public static LoggerLevel CurrentLoggerLevel { get; set; }

        public static void Working(string message)
        {
            message = $"{GetCurrentDate()} [WORKING] - {message}";
            LogToLogBox(message);
        }

        public static void Debug(string message)
        {
            if (IgnoreLogs(LoggerLevel.Debug))
            {
                return;
            }

            message = $"{GetCurrentDate()} [DEBUG] - {message}";
            LogToFile(message);
            LogToLogBox(message);
        }

        public static void Info(string message)
        {
            if (IgnoreLogs(LoggerLevel.Info))
            {
                return;
            }

            message = $"{GetCurrentDate()} [INFO] - {message}";
            LogToFile(message);
            LogToLogBox(message);
        }

        public static void Warning(string message, Exception e = null)
        {
            if (IgnoreLogs(LoggerLevel.Info))
            {
                return;
            }

            message = $"{GetCurrentDate()} [WARN] - {message}";

            LogToLogBox(message);
            LogToFile(message + (e != null ? e.Message + " " + e.StackTrace : string.Empty));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Error(string message, Exception e = null)
        {
            if (IgnoreLogs(LoggerLevel.Error))
            {
                return;
            }

            message = $"{GetCurrentDate()} [ERROR] - {message}";
            if (e != null)
            {
                message += $". {e.Message}";
            }

            File.AppendAllText(
                "logs/error.log",
                message + @" " + (e != null ? e.Message + " " + e.StackTrace : string.Empty) + Environment.NewLine);
            LogToLogBox(message);
        }

        public static void Critical(Exception ex)
        {
            Critical(string.Empty, ex);
        }

        public static void Critical(string message, Exception ex)
        {
            var shortMessage = $"{ex.Message}";
            if (message != string.Empty)
            {
                shortMessage = $"{message}. {shortMessage}";
            }

            var logMessage = $"{GetCurrentDate()} [CRITICAL] - {shortMessage}";

            File.AppendAllText("logs/error.log", $@"{logMessage} {ex.StackTrace}" + Environment.NewLine);
            MessageBox.Show(shortMessage, @"Critical exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

            LogCriticalErrorToServer(message, ex);
        }

        public static void LogToLogBox(string s)
        {
            Dispatcher.AsMainForm(() => { AppendTextToLogTextBox(s); });
        }

        public static string GetCurrentDate()
        {
            return DateTime.Now.ToString(DateFormat);
        }

        private static void LogCriticalErrorToServer(string message, Exception ex)
        {
            message = $"Log: {message} + {GetDetailedExceptionInfo(ex)}";
            using (var wb = new WebClient())
            {
                var response = wb.UploadString("https://www.steambiz.store/api/logerror", message);
                Debug("Critical error has been delivered to the server. Response: " + response);
            }
        }

        private static string GetDetailedExceptionInfo(Exception e)
        {
            if (e == null)
            {
                return string.Empty;
            }

            var message = $"Exception: {e.Message} \nSource object: {e.Source} "
                          + $"\nMethod: {e.TargetSite.Name} \nStack trace: {e.StackTrace}";

            if (e.InnerException != null)
            {
                message += $"\nInner exception: {GetDetailedExceptionInfo(e.InnerException)}";
            }

            return message + Environment.NewLine;
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

            textBox.AppendText(s + Environment.NewLine);
        }

        private static bool IgnoreLogs(LoggerLevel invoked)
        {
            return invoked < CurrentLoggerLevel;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void LogToFile(string s)
        {
            Task.Run(
                () =>
                    {
                        try
                        {
                            File.AppendAllText($"logs/log {DateTime.Now:dd-MM-yy}.log", s + Environment.NewLine);
                        }
                        catch
                        {
                            Thread.Sleep(1000);
                            LogToFile(s);
                        }
                    });
        }
    }
}