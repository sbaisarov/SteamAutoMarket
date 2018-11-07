namespace SteamAutoMarket.Utils.Logger
{
    using System;

    using log4net.Appender;
    using log4net.Core;

    using SteamAutoMarket.Pages;
    using SteamAutoMarket.Repository.Context;

    public class UiLogAppender : AppenderSkeleton
    {
        private const string DateFormat = "HH:mm:ss";

        protected override void Append(LoggingEvent e)
        {
            LogsWindow.GlobalLogs += $"{GetCurrentDate()} ({e.Level}) - {e.RenderedMessage}{Environment.NewLine}";
        }

        private static string GetCurrentDate() => DateTime.Now.ToString(DateFormat);
    }
}