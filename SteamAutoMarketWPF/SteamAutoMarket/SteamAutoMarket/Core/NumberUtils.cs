namespace SteamAutoMarket.Core
{
    using System.Text.RegularExpressions;
    using System.Threading;

    public static class NumberUtils
    {
        public static readonly string DoubleDelimiter =
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        public static readonly Regex DoubleRegex = new Regex($"([0-9])+(\\{DoubleDelimiter}[0-9]+)?");

        public static bool TryParseDouble(string s, out double result)
        {
            if (s.Contains(",") && DoubleDelimiter != ",")
            {
                s = s.Replace(",", DoubleDelimiter);
            }

            if (s.Contains(".") && DoubleDelimiter != ".")
            {
                s = s.Replace(".", DoubleDelimiter);
            }

            if (DoubleRegex.IsMatch(s))
            {
                s = DoubleRegex.Match(s).Value;
            }

            return double.TryParse(s, out result);
        }
    }
}