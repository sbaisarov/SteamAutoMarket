using Newtonsoft.Json;
using SteamAuth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace autotrade.CustomElements {
    class SettingsContainer {
        public static string ACCOUNTS_FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + "accounts.ini";
        public static string SETTINGS_FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + "settings.ini";
    }

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
            if (!File.Exists(SettingsContainer.SETTINGS_FILE_PATH)) {
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
            File.WriteAllText(SettingsContainer.SETTINGS_FILE_PATH, JsonConvert.SerializeObject(cached, Formatting.Indented));
        }
    }

    class SavedSteamAccount {
        private static List<SavedSteamAccount> cached = null;

        public string Login { get; set; }
        public string Password { get; set; }
        public string OpskinsApi { get; set; }
        public SteamGuardAccount Mafile { get; set; }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static List<SavedSteamAccount> Get() {
            if (cached != null) return cached;
            if (!File.Exists(SettingsContainer.ACCOUNTS_FILE_PATH)) {
                cached = new List<SavedSteamAccount>();
                UpdateAll(cached);
                return cached;
            }
            cached = JsonConvert.DeserializeObject<List<SavedSteamAccount>>(
                File.ReadAllText(SettingsContainer.ACCOUNTS_FILE_PATH));
            return cached;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateAll(List<SavedSteamAccount> accounts) {
            cached = accounts;
            File.WriteAllText(SettingsContainer.ACCOUNTS_FILE_PATH, JsonConvert.SerializeObject(accounts, Formatting.Indented));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateByLogin(String login, SavedSteamAccount account) {
            var allAccounts = Get();
            int foundAccount = allAccounts.FindIndex(all => all.Login.Equals(account.Login));

            if (foundAccount == -1) {
                allAccounts.Add(account);
            }
            else {
                allAccounts[foundAccount] = account;
            }

            UpdateAll(allAccounts);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateByLogin(String login, SteamGuardAccount account) {
            var allAccounts = Get();
            int foundAccount = allAccounts.FindIndex(all => all.Login.Equals(login));

            if (foundAccount == -1) {
                return;
            } else {
                allAccounts[foundAccount].Mafile = account;
            }

            UpdateAll(allAccounts);
        }
    }
}
