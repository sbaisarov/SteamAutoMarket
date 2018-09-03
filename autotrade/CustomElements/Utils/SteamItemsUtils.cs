using autotrade.Steam.TradeOffer.Models.Full;

namespace autotrade.CustomElements.Utils
{
    internal class SteamItemsUtils
    {
        public static string GetClearType(FullRgItem item)
        {
            return GetClearType(item.Description.Type);
        }

        public static string GetClearType(FullTradeItem item)
        {
            return GetClearType(item.Description.Type);
        }

        public static string GetClearType(FullHistoryTradeItem item)
        {
            return GetClearType(item.Description.Type);
        }

        private static string GetClearType(string type)
        {
            if (type == null) return "[None]";
            if (type.Contains("Sale Foil Trading Card")) return "Sale Foil Trading Card";
            if (type.Contains("Sale Trading Card")) return "Sale Trading Card";
            if (type.Contains("Foil Trading Card")) return "Foil Trading Card";
            if (type.Contains("Trading Card")) return "Trading Card";
            if (type.Contains("Emoticon")) return "Emoticon";
            if (type.Contains("Background")) return "Background";
            if (type.Contains("Sale Item")) return "Sale Item";
            return type;
        }
    }
}