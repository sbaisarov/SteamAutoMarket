namespace SteamAutoMarket.Core
{
    using System;
    using System.Reflection;

    using log4net;
    using log4net.Appender;
    using log4net.Core;
    using log4net.Layout;
    using log4net.Repository.Hierarchy;

    public class Logger
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly string LogFilePath = AppDomain.CurrentDomain.BaseDirectory + "logs.log";

        public static RollingFileAppender NewFileAppender()
        {
            var patternLayout = new PatternLayout { ConversionPattern = "%date [%thread] %-5level - %message%newline" };
            patternLayout.ActivateOptions();

            var appender = new RollingFileAppender
                               {
                                   AppendToFile = true,
                                   File = @"Logs\log.log",
                                   Layout = patternLayout,
                                   MaxSizeRollBackups = 100,
                                   MaximumFileSize = "10MB",
                                   RollingStyle = RollingFileAppender.RollingMode.Size,
                                   StaticLogFileName = true
                               };

            appender.ActivateOptions();

            return appender;
        }

        public static void Setup(params IAppender[] appenders)
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();

            foreach (var appender in appenders)
            {
                hierarchy.Root.AddAppender(appender);
            }

            var memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;
        }

        public static void UpdateLoggerLevel(Level newLevel)
        {
            ((Hierarchy)LogManager.GetRepository()).Root.Level = newLevel;
            ((Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
        }

        public static void UpdateLoggerLevel(string newLevel)
        {
            switch (newLevel)
            {
                case "DEBUG":
                    UpdateLoggerLevel(Level.Debug);
                    return;

                case "INFO":
                    UpdateLoggerLevel(Level.Info);
                    return;

                case "ERROR":
                    UpdateLoggerLevel(Level.Error);
                    return;

                case "NONE":
                    UpdateLoggerLevel(Level.Fatal);
                    return;

                default:
                    Log.Error($"{newLevel} logger value can not be handled.");
                    return;
            }
        }

        public static Level CurrentLogLevel => ((Hierarchy)LogManager.GetRepository()).Root.Level;
    }
}