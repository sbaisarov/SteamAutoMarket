namespace SteamAutoMarket.Localization
{
    using System;

    using SteamAutoMarket.Localization.Languages;
    using SteamAutoMarket.UI.Repository.Settings;

    public class StringsProvider
    {
        public const string En = "EN";

        public const string Ru = "RU";

        public static readonly IStrings EnglishStrings = new EnglishStrings();

        public static readonly IStrings RussianStrings = new RussianStrings();

        public static IStrings Strings { get; private set; } = GetStringObj();

        public static void UpdateLocal()
        {
            Strings = GetStringObj();
        }

        private static IStrings GetStringObj()
        {
            var local = SettingsProvider.GetInstance().Local;

            switch (local)
            {
                case Ru: return RussianStrings;
                case En: return EnglishStrings;
                default: throw new ArgumentException($"{local} is not supported");
            }
        }
    }
}