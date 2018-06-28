using autotrade.Steam;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autotrade.WorkingProcess {
    class CurrentSession {
        public static SteamManager SteamManager { get; set; }
        public static Image AccountImage { get; set; }
        public static string InventoryAppId { get; set; }
        public static string InventoryContextId { get; set; }
    }
}
