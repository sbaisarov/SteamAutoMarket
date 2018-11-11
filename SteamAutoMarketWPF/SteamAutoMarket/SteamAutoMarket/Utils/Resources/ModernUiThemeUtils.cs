namespace SteamAutoMarket.Utils.Resources
{
    using System;
    using System.Linq;
    using System.Windows.Media;

    using FirstFloor.ModernUI.Presentation;

    using SteamAutoMarket.Repository.Context;

    public static class ModernUiThemeUtils
    {
        public static Color GetColor(string colorName)
        {
            if (colorName == null)
            {
                return UiDefaultValues.Colors["cyan"];
            }

            return UiDefaultValues.Colors.TryGetValue(colorName, out var color)
                       ? color
                       : UiDefaultValues.Colors["cyan"];
        }

        public static string GetColorName(Color color) =>
            UiDefaultValues.Colors.FirstOrDefault(c => c.Value == color).Key;

        public static Uri GetTheme(string themeName) =>
            UiDefaultValues.Themes.FirstOrDefault(theme => theme.DisplayName == themeName)?.Source
            ?? AppearanceManager.DarkThemeSource;

        public static string GetThemeName(Uri theme) =>
            UiDefaultValues.Themes.FirstOrDefault(t => t.Source == theme)?.DisplayName;
    }
}