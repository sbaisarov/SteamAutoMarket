namespace SteamAutoMarket.UI.Utils.Logger
{
    using System;
    using System.Windows;

    using FirstFloor.ModernUI.Windows.Controls;

    using SteamAutoMarket.Core;

    public static class ErrorNotify
    {
        public static void CriticalMessageBox(Exception e)
        {
            Logger.Log.Error(string.Empty, e);
            ShowMessageBox(e.Message, "Error", MessageBoxButton.OK);
        }

        public static void CriticalMessageBox(string message)
        {
            Logger.Log.Error(message);
            ShowMessageBox(message, "Error", MessageBoxButton.OK);
        }

        public static void CriticalMessageBox(string message, Exception e)
        {
            Logger.Log.Error(message, e);
            ShowMessageBox($"{message} - {e.Message}", "Error", MessageBoxButton.OK);
        }

        public static void InfoMessageBox(string message)
        {
            Logger.Log.Info(message);
            ShowMessageBox(message, "Info", MessageBoxButton.OK);
        }

        private static void ShowMessageBox(string message, string title, MessageBoxButton button)
        {
            Application.Current.Dispatcher.Invoke(() => ModernDialog.ShowMessage(message, title, button));
        }
    }
}