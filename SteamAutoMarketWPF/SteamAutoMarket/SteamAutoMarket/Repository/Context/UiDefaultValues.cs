namespace SteamAutoMarket.Repository.Context
{
    using System.Collections.Generic;
    using System.Windows.Media;

    using Core;

    using FirstFloor.ModernUI.Presentation;

    using SteamAutoMarket.Models;

    public class UiDefaultValues
    {
        public static readonly SteamAppId[] AppIdList = ResourceUtils.ToArray(
            new SteamAppId(753, "STEAM", 6),
            new SteamAppId(570, "DOTA 2", 2),
            new SteamAppId(730, "CS:GO", 2),
            new SteamAppId(578080, "PUBG", 2),
            new SteamAppId(440, "TF:2", 2),
            new SteamAppId(218620, "PAYDAY 2", 2));

        public static readonly LinkCollection Themes = new LinkCollection
                                                           {
                                                               new Link
                                                                   {
                                                                       DisplayName = "dark",
                                                                       Source = AppearanceManager.DarkThemeSource
                                                                   },
                                                               new Link
                                                                   {
                                                                       DisplayName = "light",
                                                                       Source = AppearanceManager.LightThemeSource
                                                                   }
                                                           };

        public static readonly Dictionary<string, Color> Colors = new Dictionary<string, Color>
                                                                      {
                                                                          { "lime", Color.FromRgb(0xa4, 0xc4, 0x00) },
                                                                          { "green", Color.FromRgb(0x60, 0xa9, 0x17) },
                                                                          {
                                                                              "emerald", Color.FromRgb(0x00, 0x8a, 0x00)
                                                                          },
                                                                          { "teal", Color.FromRgb(0x00, 0xab, 0xa9) },
                                                                          { "cyan", Color.FromRgb(0x1b, 0xa1, 0xe2) },
                                                                          { "cobalt", Color.FromRgb(0x00, 0x50, 0xef) },
                                                                          { "indigo", Color.FromRgb(0x6a, 0x00, 0xff) },
                                                                          { "violet", Color.FromRgb(0xaa, 0x00, 0xff) },
                                                                          { "pink", Color.FromRgb(0xf4, 0x72, 0xd0) },
                                                                          {
                                                                              "magenta", Color.FromRgb(0xd8, 0x00, 0x73)
                                                                          },
                                                                          {
                                                                              "crimson", Color.FromRgb(0xa2, 0x00, 0x25)
                                                                          },
                                                                          { "red", Color.FromRgb(0xe5, 0x14, 0x00) },
                                                                          { "orange", Color.FromRgb(0xfa, 0x68, 0x00) },
                                                                          { "amber", Color.FromRgb(0xf0, 0xa3, 0x0a) },
                                                                          { "yellow", Color.FromRgb(0xe3, 0xc8, 0x00) },
                                                                          { "brown", Color.FromRgb(0x82, 0x5a, 0x2c) },
                                                                          { "olive", Color.FromRgb(0x6d, 0x87, 0x64) },
                                                                          { "steel", Color.FromRgb(0x64, 0x76, 0x87) },
                                                                          { "mauve", Color.FromRgb(0x76, 0x60, 0x8a) },
                                                                          { "taupe", Color.FromRgb(0x87, 0x79, 0x4e) }
                                                                      };
    }
}