namespace SteamAutoMarket.UI.Repository.Settings
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using SteamAutoMarket.Core;

    public static class SettingsUpdated
    {
        public static readonly string SettingsPath = AppDomain.CurrentDomain.BaseDirectory + "settings.ini";

        private static bool isUpdatePlanned;

        public static void UpdateSettingsFile()
        {
            if (isUpdatePlanned)
            {
                Logger.Log.Debug("Settings file update is already planed");
                return;
            }

            Task.Run(() => PlanSettingsUpdate());
        }

        public static void ForceSettingsUpdate()
        {
            UpdateSettings();
        }

        private static void PlanSettingsUpdate()
        {
            Logger.Log.Debug("Settings file update is planed in 5 seconds");
            isUpdatePlanned = true;
            Thread.Sleep(TimeSpan.FromSeconds(5));
            UpdateSettings();
            isUpdatePlanned = false;
            Logger.Log.Debug("Settings file was updated");
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
                Logger.Log.Error($"Error on settings file update - {e.Message}", e);
                PlanSettingsUpdate();
            }
        }
    }
}