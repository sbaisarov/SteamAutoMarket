using autotrade.CustomElements;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace autotrade.WorkingProcess.Settings {
    class SavedSettings {
        private static SavedSettings cached = null;

        public int LOGGER_LEVEL { get; set; } = 0;

        public string MARKET_INVENTORY_APP_ID { get; set; } = "";
        public string MARKET_INVENTORY_CONTEX_ID { get; set; } = "";
        public string MARKET_CURRENT_PRICE_MINUS_VALUE { get; set; } = "";
        public string MARKET_CURRENT_PRICE_MINUS_PERCENT { get; set; } = "";

        public string TRADE_INVENTORY_APP_ID { get; set; } = "";
        public string TRADE_INVENTORY_CONTEX_ID { get; set; } = "";
        public string TRADE_CURRENT_PRICE_MINUS_VALUE { get; set; } = "";
        public string TRADE_CURRENT_PRICE_MINUS_PERCENT { get; set; } = "";
        public string TRADE_TOKEN { get; set; } = "";
        public string TRADE_PARTNER_ID { get; set; } = "";

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static SavedSettings Get() {
            if (cached != null) return cached;
            if (!File.Exists(SettingsContainer.ACCOUNTS_FILE_PATH)) {
                cached = new SavedSettings();
                UpdateAll();
                return cached;
            }
            cached = JsonConvert.DeserializeObject<SavedSettings>(
                File.ReadAllText(SettingsContainer.SETTINGS_FILE_PATH));
            return cached;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateAll() {
            try {
                File.WriteAllText(SettingsContainer.SETTINGS_FILE_PATH, JsonConvert.SerializeObject(cached, Formatting.Indented));
            } finally { };
        }
    }
}
