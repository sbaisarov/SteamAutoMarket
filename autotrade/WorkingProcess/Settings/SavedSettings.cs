using autotrade.CustomElements;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace autotrade.WorkingProcess.Settings {
    class SavedSettings {
        public static string SETTINGS_FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + "settings.ini";
        private static SavedSettings cached = null;

        public int SETTINGS_LOGGER_LEVEL = 0;
        public int SETTINGS_HOURS_TO_BECOME_OLD_CURRENT_PRICE = 1;
        public int SETTINGS_HOURS_TO_BECOME_OLD_AVERAGE_PRICE = 12;
        public int SETTINGS_AVERAGE_PRICE_PARSE_DAYS = 7;

        public string MARKET_INVENTORY_APP_ID = "";
        public string MARKET_INVENTORY_CONTEX_ID = "";

        public string TRADE_INVENTORY_APP_ID = "";
        public string TRADE_INVENTORY_CONTEX_ID = "";
        public string TRADE_CURRENT_PRICE_MINUS_VALUE = "";
        public string TRADE_CURRENT_PRICE_MINUS_PERCENT = "";
        public string TRADE_TOKEN = "";
        public string TRADE_PARTNER_ID = "";

        public string ACTIVE_TRADES_LANGUAGE = "";
        public bool ACTIVE_TRADES_RECIEVED = true;
        public bool ACTIVE_TRADES_SENT = true;
        public bool ACTIVE_TRADES_ACTIVE_ONLY = true;

        public string TRADE_HISTORY_TRADE_ID = "";
        public int TRADE_HISTORY_MAX_TRADES = 10;
        public string TRADE_HISTORY_LANGUAGE = "en_US";
        public bool TRADE_HISTORY_NAVIGATING_BACK = true;
        public bool TRADE_HISTORY_INCLUDE_FAILED = true;
        public bool TRADE_HISTORY_RECIEVED = true;
        public bool TRADE_HISTORY_SENT = true;


        public static void UpdateField(ref string fieldToUpdate, string newValue) {
            if (fieldToUpdate != newValue) {
                fieldToUpdate = newValue;
                UpdateAll();
            }
        }

        public static void UpdateField(ref bool fieldToUpdate, bool newValue) {
            if (fieldToUpdate != newValue) {
                fieldToUpdate = newValue;
                UpdateAll();
            }
        }

        public static void UpdateField(ref int fieldToUpdate, int newValue) {
            if (fieldToUpdate != newValue) {
                fieldToUpdate = newValue;
                UpdateAll();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static SavedSettings Get() {
            if (cached != null) return cached;
            if (!File.Exists(SETTINGS_FILE_PATH)) {
                cached = new SavedSettings();
                UpdateAll();
                return cached;
            }
            cached = JsonConvert.DeserializeObject<SavedSettings>(
                File.ReadAllText(SETTINGS_FILE_PATH));
            return cached;
        }

        public static void RestoreDefault() {
            cached = new SavedSettings();
            SettingsUpdater.IsPending = false;
            File.WriteAllText(SETTINGS_FILE_PATH, JsonConvert.SerializeObject(SavedSettings.Get(), Formatting.Indented));
        }

        private static void UpdateAll() {
            SettingsUpdater.UpdateSettings();
        }
    }

    class SettingsUpdater {
        internal static bool IsPending = false;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateSettings() {
            if (IsPending) return;
            IsPending = true;

            Task.Run(() => {
                Thread.Sleep(5000);
                try {
                    File.WriteAllText(SavedSettings.SETTINGS_FILE_PATH, JsonConvert.SerializeObject(SavedSettings.Get(), Formatting.Indented));
                } finally { };
                IsPending = false;
            });
        }
    }
}
