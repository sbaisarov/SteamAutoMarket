namespace SteamAutoMarket.UI.Utils.Logger
{
    using System;
    using System.Linq;

    using log4net.Appender;
    using log4net.Core;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.UI.Pages;

    public class UiLogAppender : AppenderSkeleton
    {
        private const string DateFormat = "HH:mm:ss";

        public static string GetCurrentDate() => DateTime.Now.ToString(DateFormat);

        protected override void Append(LoggingEvent e)
        {
            if (e.Level == Level.Debug)
            {
                return;
            }

            LogsWindow.GlobalLogs += $"{GetCurrentDate()} ({e.Level}) - {e.RenderedMessage}{Environment.NewLine}";

            if (LogsWindow.GlobalLogs.Length <= 100000) return;

            try
            {
                var lines = LogsWindow.GlobalLogs.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                var start = lines.Length / 2;
                var end = lines.Length - start;
                LogsWindow.GlobalLogs = string.Join(Environment.NewLine, lines.ToList().GetRange(start, end));
            }
            catch (Exception ex)
            {
                Logger.Log.Error($"Error on global logs cutback - {ex.Message}", ex);
            }
        }
    }
}