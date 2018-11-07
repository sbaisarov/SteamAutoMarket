namespace SteamAutoMarket.Utils.Logger
{
    using System;

    using log4net.Appender;
    using log4net.Core;

    using SteamAutoMarket.Pages;

    public class UiLogAppender : AppenderSkeleton
    {
        private const string DateFormat = "HH:mm:ss";

        public static string GetCurrentDate() => DateTime.Now.ToString(DateFormat);

        protected override void Append(LoggingEvent e)
        {
            LogsWindow.GlobalLogs += $"{GetCurrentDate()} ({e.Level}) - {e.RenderedMessage}{Environment.NewLine}";
        }
    }
}