namespace SteamAutoMarket.Repository.Settings
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Core;

    using Newtonsoft.Json;

    public static class SettingsUpdated
    {
        public static readonly string SettingsPath = AppDomain.CurrentDomain.BaseDirectory + "settings.ini";

        private static bool isUpdatePlanned;

        public static void UpdateSettingsFile()
        {
            if (isUpdatePlanned) return;

            Task.Run(() => PlanSettingsUpdate());
        }

        private static void PlanSettingsUpdate()
        {
            isUpdatePlanned = true;
            Thread.Sleep(TimeSpan.FromSeconds(5));
            UpdateSettings();
            isUpdatePlanned = false;
        }

        private static void UpdateSettings()
        {
            try
            {
                File.WriteAllText(
                    SettingsPath,
                    JsonConvert.SerializeObject(SettingsProvider.GetInstance(), Formatting.Indented));
            }
            catch (Exception e)
            {
                Logger.Log.Error("Error on message file update", e);
                PlanSettingsUpdate();
            }
        }
    }
}