namespace SteamAutoMarket.Repository.Context
{
    using System.Collections.Generic;

    using Core;

    using SteamAutoMarket.Models;

    public class UiDefaultValues
    {
        public static readonly List<SteamAppId> AppIdList = ResourceUtils.ToList(
            new SteamAppId(753, "STEAM", 6),
            new SteamAppId(570, "DOTA 2", 2),
            new SteamAppId(730, "CS:GO", 2),
            new SteamAppId(578080, "PUBG", 2),
            new SteamAppId(440, "TF:2", 2),
            new SteamAppId(218620, "PAYDAY 2", 2));
    }
}