using SteamAuth;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autotrade.CustomElements {
    class SettingsContainer {
    }

    class SavedSteamAccount {
        public string Login { get; set; }
        public string Password { get; set; }
        public string OpskinsApi { get; set; }
        public SteamGuardAccount Mafile { get; set; }
    }
}
