namespace SteamAutoMarket.Localization
{
    using System;

    using SteamAutoMarket.Localization.Languages;
    using SteamAutoMarket.UI.Repository.Settings;

    public class StringsProvider
    {
        public static IStrings Strings { get; } = GetStringObj();

        private static IStrings GetStringObj()
        {
            var local = SettingsProvider.GetInstance().Local;

            switch (local)
            {
                case "EN": return new EnglishStrings();
                case "RU": return new RussianStrings();
                default: throw new ArgumentException($"{local} is not supported");
            }
        }
    }
}