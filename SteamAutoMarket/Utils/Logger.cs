namespace SteamAutoMarket.Utils
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using SteamAutoMarket.WorkingProcess.Settings;

    internal enum LoggerLevel
    {
        Debug,

        Info,

        Error,

        None
    }

    internal static class Logger
    {
        public const string DateFormat = "HH:mm:ss";

        private static readonly LoggerLevel[] NoneShouldBeIgnored =
            {
                LoggerLevel.Debug, LoggerLevel.Info, LoggerLevel.Error, LoggerLevel.None
            };

        private static readonly LoggerLevel[] DebugShouldBeIgnored =
            {
                LoggerLevel.Info, LoggerLevel.Error, LoggerLevel.None
            };

        private static readonly LoggerLevel[] InfoShouldBeIgnored = { LoggerLevel.Error, LoggerLevel.None };

        private static readonly LoggerLevel[] ErrorShouldBeIgnored = { LoggerLevel.None };

        static Logger()
        {
            try
            {
                Directory.CreateDirectory("logs");
                switch (SavedSettings.Get().SettingsLoggerLevel)
                {
                    case 1:
                        LoggerLevel = LoggerLevel.Debug;
                        break;
                    case 2:
                        LoggerLevel = LoggerLevel.Info;
                        break;
                    case 3:
                        LoggerLevel = LoggerLevel.Error;
                        break;
                    case 4:
                        LoggerLevel = LoggerLevel.None;
                        break;

                    default:
                        LoggerLevel = LoggerLevel.Info;
                        break;
                }
            }
            catch
            {
                // ignored
            }
        }

        public static LoggerLevel LoggerLevel { get; set; }

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
                message + @" " + (e != null ? e.Message + " " + e.StackTrace : string.Empty) + @"\n");
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

            File.AppendAllText("logs/error.log", $@"{logMessage} {ex.StackTrace}\n");
            MessageBox.Show(shortMessage, @"Critical exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

            LogCriticalErrorToServer(message, ex);
        }

        public static void LogToLogBox(string s)
        {
            if (Program.IsMainThread)
            {
                AppendTextToLogTextBox(s);
            }
            else
            {
                Dispatcher.AsMainForm(() => { AppendTextToLogTextBox(s); });
            }
        }

        public static string GetCurrentDate()
        {
            return DateTime.Now.ToString(DateFormat);
        }

        private static void LogCriticalErrorToServer(string message, Exception ex)
        {
            // todo Send to server
            message = $"Log: {message} + {GetDetailedExceptionInfo(ex)}";
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

            return message;
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

        private static bool IgnoreLogs(LoggerLevel invoked)
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void LogToFile(string s)
        {
            Task.Run(
                () =>
                    {
                        try
                        {
                            File.AppendAllText($"logs/log {DateTime.Now:dd-MM-yy}.log", $@"{s}\n");
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