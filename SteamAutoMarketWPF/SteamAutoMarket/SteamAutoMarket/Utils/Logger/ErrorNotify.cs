namespace SteamAutoMarket.Utils.Logger
{
    using System;
    using System.Windows;

    using Core;

    public static class ErrorNotify
    {
        public static void CriticalMessageBox(Exception e)
        {
            Logger.Log.Error(string.Empty, e);
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void CriticalMessageBox(string message)
        {
            Logger.Log.Error(message);
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void CriticalMessageBox(string message, Exception e)
        {
            Logger.Log.Error(message, e);
            MessageBox.Show($"{message} - {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void InfoMessageBox(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}