using autotrade.CustomElements;
using Newtonsoft.Json;
using SteamAuth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace autotrade.WorkingProcess.Settings {
    class SavedSteamAccount {
        public static string ACCOUNTS_FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + "accounts.ini";

        private static List<SavedSteamAccount> cached = null;

        public string Login { get; set; }
        public string Password { get; set; }
        public string OpskinsApi { get; set; }
        public SteamGuardAccount Mafile { get; set; }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static List<SavedSteamAccount> Get() {
            if (cached != null) return cached;
            if (!File.Exists(ACCOUNTS_FILE_PATH)) {
                cached = new List<SavedSteamAccount>();
                UpdateAll(cached);
                return cached;
            }
            cached = JsonConvert.DeserializeObject<List<SavedSteamAccount>>(
                File.ReadAllText(ACCOUNTS_FILE_PATH));
            return cached;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateAll(List<SavedSteamAccount> accounts) {
            cached = accounts;
            File.WriteAllText(ACCOUNTS_FILE_PATH, JsonConvert.SerializeObject(accounts, Formatting.Indented));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateByLogin(String login, SavedSteamAccount account) {
            var allAccounts = Get();
            int foundAccount = allAccounts.FindIndex(all => all.Login.Equals(account.Login));

            if (foundAccount == -1) {
                allAccounts.Add(account);
            } else {
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
