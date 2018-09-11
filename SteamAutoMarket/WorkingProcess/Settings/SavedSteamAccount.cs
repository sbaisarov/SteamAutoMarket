namespace SteamAutoMarket.WorkingProcess.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    using Newtonsoft.Json;

    using SteamAuth;

    internal class SavedSteamAccount
    {
        public static string AccountsFilePath = AppDomain.CurrentDomain.BaseDirectory + "accounts.ini";

        private static List<SavedSteamAccount> _cached;

        public string Login { get; set; }
        public string Password { get; set; }
        public string SteamApi { get; set; }
        public SteamGuardAccount MaFile { get; set; }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static List<SavedSteamAccount> Get()
        {
            if (_cached != null) return _cached;
            if (!File.Exists(AccountsFilePath))
            {
                _cached = new List<SavedSteamAccount>();
                UpdateAll(_cached);
                return _cached;
            }

            _cached = JsonConvert.DeserializeObject<List<SavedSteamAccount>>(
                File.ReadAllText(AccountsFilePath));
            return _cached;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateAll(List<SavedSteamAccount> accounts)
        {
            _cached = accounts;
            File.WriteAllText(AccountsFilePath, JsonConvert.SerializeObject(accounts, Formatting.Indented));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateByLogin(string login, SavedSteamAccount account)
        {
            var allAccounts = Get();
            var foundAccount = allAccounts.FindIndex(all => all.Login.Equals(account.Login));

            if (foundAccount == -1)
                allAccounts.Add(account);
            else
                allAccounts[foundAccount] = account;

            UpdateAll(allAccounts);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateByLogin(string login, SteamGuardAccount account)
        {
            var allAccounts = Get();
            var foundAccount = allAccounts.FindIndex(all => all.Login.Equals(login));

            if (foundAccount == -1)
                return;
            allAccounts[foundAccount].MaFile = account;

            UpdateAll(allAccounts);
        }
    }
}