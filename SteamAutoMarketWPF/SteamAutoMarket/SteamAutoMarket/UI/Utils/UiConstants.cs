namespace SteamAutoMarket.UI.Utils
{
    using System.Collections.Generic;

    using SteamAutoMarket.UI.Models;

    public class UiConstants
    {
        public const string DoubleToStringFormat = "F";

        public static readonly string FractionalZeroString = 0.ToString(DoubleToStringFormat);

        public static readonly SteamAppId SteamAppId = new SteamAppId(753, "STEAM", 6);

        public static readonly IEnumerable<string> EmptyValueEnumerable = new[] { string.Empty };
    }
}