namespace SteamAutoMarket.WorkingProcess.Settings
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    internal class SavedSettings
    {
        public static string SettingsFilePath = AppDomain.CurrentDomain.BaseDirectory + "settings.ini";
        private static SavedSettings cached;

        public bool ActiveTradesActiveOnly = true;

        public string ActiveTradesLanguage = "";
        public bool ActiveTradesReceived = true;
        public bool ActiveTradesSent = true;

        public string MarketInventoryAppId = "";
        public string MarketInventoryContexId = "";
        public int Settings2FaItemsToConfirm = 10;
        public int SettingsAveragePriceParseDays = 7;
        public int SettingsHoursToBecomeOldAveragePrice = 12;
        public int SettingsHoursToBecomeOldCurrentPrice = 1;

        public int SettingsLoggerLevel = 0;
        public bool TradeHistoryIncludeFailed = true;
        public string TradeHistoryLanguage = "en_US";
        public int TradeHistoryMaxTrades = 10;
        public bool TradeHistoryNavigatingBack = true;
        public bool TradeHistoryReceived = true;
        public bool TradeHistorySent = true;

        public string TradeHistoryTradeId = "";

        public string TRADE_INVENTORY_APP_ID = "";
        public string TRADE_INVENTORY_CONTEX_ID = "";
        public string TRADE_PARTNER_ID = "";
        public string TRADE_TOKEN = "";


        public static void UpdateField(ref string fieldToUpdate, string newValue)
        {
            if (fieldToUpdate != newValue)
            {
                fieldToUpdate = newValue;
                UpdateAll();
            }
        }

        public static void UpdateField(ref bool fieldToUpdate, bool newValue)
        {
            if (fieldToUpdate != newValue)
            {
                fieldToUpdate = newValue;
                UpdateAll();
            }
        }

        public static void UpdateField(ref int fieldToUpdate, int newValue)
        {
            if (fieldToUpdate != newValue)
            {
                fieldToUpdate = newValue;
                UpdateAll();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static SavedSettings Get()
        {
            if (cached != null) return cached;
            if (!File.Exists(SettingsFilePath))
            {
                cached = new SavedSettings();
                UpdateAll();
                return cached;
            }

            cached = JsonConvert.DeserializeObject<SavedSettings>(
                File.ReadAllText(SettingsFilePath));
            return cached;
        }

        public static void RestoreDefault()
        {
            cached = new SavedSettings();
            SettingsUpdater.IsPending = false;
            File.WriteAllText(SettingsFilePath, JsonConvert.SerializeObject(Get(), Formatting.Indented));
        }

        private static void UpdateAll()
        {
            SettingsUpdater.UpdateSettings();
        }
    }

    internal class SettingsUpdater
    {
        internal static bool IsPending;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateSettings()
        {
            if (IsPending) return;
            IsPending = true;

            Task.Run(() =>
            {
                Thread.Sleep(5000);
                File.WriteAllText(SavedSettings.SettingsFilePath,
                    JsonConvert.SerializeObject(SavedSettings.Get(), Formatting.Indented));
                ;
                IsPending = false;
            });
        }
    }
}