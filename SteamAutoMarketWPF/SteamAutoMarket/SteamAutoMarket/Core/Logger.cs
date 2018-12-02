namespace SteamAutoMarket.Core
{
    using System;
    using System.Reflection;

    using log4net;
    using log4net.Core;

    public class Logger
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static readonly string LogFilePath = AppDomain.CurrentDomain.BaseDirectory + "logs.log";

        public static void UpdateLoggerLevel(Level newLevel)
        {
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = newLevel;
            ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(
                EventArgs.Empty);
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
    }
}