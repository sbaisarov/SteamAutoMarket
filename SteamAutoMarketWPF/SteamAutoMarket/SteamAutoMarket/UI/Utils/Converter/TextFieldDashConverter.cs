namespace SteamAutoMarket.UI.Utils.Converter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class TextFieldDashConverter : IValueConverter
    {
        private const string DashPart = ": ";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value + DashPart;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}