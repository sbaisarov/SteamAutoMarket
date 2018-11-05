namespace Core
{
    using System;

    public class SteamUtils
    {
        public static string GetClearItemType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return "[None]";
            }

            if (type.Contains("Sale Foil Trading Card"))
            {
                return "Sale Foil Trading Card";
            }

            if (type.Contains("Sale Trading Card"))
            {
                return "Sale Trading Card";
            }

            if (type.Contains("Foil Trading Card"))
            {
                return "Foil Trading Card";
            }

            if (type.Contains("Trading Card"))
            {
                return "Trading Card";
            }

            if (type.Contains("Emoticon"))
            {
                return "Emoticon";
            }

            if (type.Contains("Background"))
            {
                return "Background";
            }

            if (type.Contains("Sale Item"))
            {
                return "Sale Item";
            }

            return type;
        }

        public static DateTime ParseSteamUnixDate(int date)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(date).ToLocalTime();
            return dateTime;
        }
    }
}