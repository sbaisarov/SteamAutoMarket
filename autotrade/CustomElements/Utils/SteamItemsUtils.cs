using autotrade.Steam.TradeOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static autotrade.Steam.TradeOffer.Inventory;

namespace autotrade.CustomElements.Utils {
    class SteamItemsUtils {
        public static string GetClearType(RgFullItem item) {
            if (item.Description.type.Contains("Sale Foil Trading Card")) return "Sale Foil Trading Card";
            if (item.Description.type.Contains("Sale Trading Card")) return "Sale Trading Card";
            if (item.Description.type.Contains("Foil Trading Card")) return "Foil Trading Card";
            if (item.Description.type.Contains("Trading Card")) return "Trading Card";
            if (item.Description.type.Contains("Emoticon")) return "Emoticon";
            if (item.Description.type.Contains("Background")) return "Background";
            if (item.Description.type.Contains("Sale Item")) return "Sale Item";
            return item.Description.type;
        }

        public static string GetClearType(TradeFullItem item) {
            if (item.Description.Type == null) return "[None]";
            if (item.Description.Type.Contains("Sale Foil Trading Card")) return "Sale Foil Trading Card";
            if (item.Description.Type.Contains("Sale Trading Card")) return "Sale Trading Card";
            if (item.Description.Type.Contains("Foil Trading Card")) return "Foil Trading Card";
            if (item.Description.Type.Contains("Trading Card")) return "Trading Card";
            if (item.Description.Type.Contains("Emoticon")) return "Emoticon";
            if (item.Description.Type.Contains("Background")) return "Background";
            if (item.Description.Type.Contains("Sale Item")) return "Sale Item";
            return item.Description.Type;
        }
    }
}
