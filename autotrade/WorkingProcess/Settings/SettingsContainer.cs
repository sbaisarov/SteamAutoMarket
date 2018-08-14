using Newtonsoft.Json;
using SteamAuth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using System.Windows.Forms;

namespace autotrade.CustomElements {
    class SettingsContainer {
        public static string ACCOUNTS_FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + "accounts.ini";
        public static string SETTINGS_FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + "settings.ini";
    }
}
