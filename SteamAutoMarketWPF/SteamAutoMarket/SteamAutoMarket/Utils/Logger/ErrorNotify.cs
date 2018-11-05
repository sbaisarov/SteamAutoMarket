namespace SteamAutoMarket.Utils.Logger
{
    using System;
    using System.Windows;

    public static class ErrorNotify
    {
        public static void CriticalMessageBox(Exception e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void CriticalMessageBox(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}