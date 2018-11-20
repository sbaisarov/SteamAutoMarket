namespace SteamAutoMarket.Utils.Logger
{
    using System;
    using System.Windows;

    using Core;

    using FirstFloor.ModernUI.Windows.Controls;

    public static class ErrorNotify
    {
        public static void CriticalMessageBox(Exception e)
        {
            Logger.Log.Error(string.Empty, e);
           ModernDialog.ShowMessage(e.Message, "Error", MessageBoxButton.OK);
        }

        public static void CriticalMessageBox(string message)
        {
            Logger.Log.Error(message);
            ModernDialog.ShowMessage(message, "Error", MessageBoxButton.OK);
        }

        public static void CriticalMessageBox(string message, Exception e)
        {
            Logger.Log.Error(message, e);
            ModernDialog.ShowMessage($"{message} - {e.Message}", "Error", MessageBoxButton.OK);
        }

        public static void InfoMessageBox(string message)
        {
            ModernDialog.ShowMessage(message, "Info", MessageBoxButton.OK);
        }
    }
}