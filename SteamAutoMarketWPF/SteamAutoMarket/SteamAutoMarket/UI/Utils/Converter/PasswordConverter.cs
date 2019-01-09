namespace SteamAutoMarket.UI.Utils.Converter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class PasswordConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => FormatEncodedString((string)value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;

        private static string FormatEncodedString(string password) => new string('*', password.Length);
    }
}