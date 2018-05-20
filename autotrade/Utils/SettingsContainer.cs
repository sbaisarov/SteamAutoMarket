using autotrade.Utils;
using Newtonsoft.Json;
using SteamAuth;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace autotrade.CustomElements {
    class SettingsContainer {
        public const string ACCOUNTS_FILE_PATH = "accounts.ini";
        public const string SETTINGS_FILE_PATH = "settings.ini";
    }

    class SavedSettings {
        private static SavedSettings cached = null;

        public int LOGGER_LEVEL { get; set; } = 0;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static SavedSettings Get() {
            if (cached != null) return cached;
            if (!File.Exists(SettingsContainer.SETTINGS_FILE_PATH)) {
                cached = new SavedSettings();
                UpdateAll(cached);
                return cached;
            }
            return JsonConvert.DeserializeObject<SavedSettings>(
                File.ReadAllText(SettingsContainer.SETTINGS_FILE_PATH));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateAll(SavedSettings settings) {
            cached = settings;
            File.WriteAllText(SettingsContainer.SETTINGS_FILE_PATH, JsonConvert.SerializeObject(settings));
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
            return JsonConvert.DeserializeObject<List<SavedSteamAccount>>(
                File.ReadAllText(SettingsContainer.ACCOUNTS_FILE_PATH));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateAll(List<SavedSteamAccount> accounts) {
            cached = accounts;
            File.WriteAllText(SettingsContainer.ACCOUNTS_FILE_PATH, JsonConvert.SerializeObject(accounts));
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
    }
}
