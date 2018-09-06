using System.Drawing;
using SteamAutoMarket.Steam;

namespace SteamAutoMarket.WorkingProcess
{
    internal class CurrentSession
    {
        public static SteamManager SteamManager { get; set; }
        public static Image AccountImage { get; set; }
        public static string CurrentInventoryAppId { get; set; }
        public static string CurrentInventoryContextId { get; set; }
    }
}