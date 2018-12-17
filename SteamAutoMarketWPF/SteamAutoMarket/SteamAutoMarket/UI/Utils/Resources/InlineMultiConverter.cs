namespace SteamAutoMarket.UI.Utils.Resources
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class InlineMultiConverter : IMultiValueConverter
    {
        public InlineMultiConverter(ConvertDelegate convert, ConvertBackDelegate convertBack = null)
        {
            this._convert = convert ?? throw new ArgumentNullException(nameof(convert));
            this._convertBack = convertBack;
        }

        public delegate object[] ConvertBackDelegate(
            object value,
            Type[] targetTypes,
            object parameter,
            CultureInfo culture);

        public delegate object ConvertDelegate(object[] values, Type targetType, object parameter, CultureInfo culture);

        private ConvertDelegate _convert { get; }

        private ConvertBackDelegate _convertBack { get; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) =>
            this._convert(values, targetType, parameter, culture);

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
            (this._convertBack != null)
                ? this._convertBack(value, targetTypes, parameter, culture)
                : throw new NotImplementedException();
    }
}