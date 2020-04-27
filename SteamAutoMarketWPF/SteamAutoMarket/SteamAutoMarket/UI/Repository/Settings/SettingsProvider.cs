namespace SteamAutoMarket.UI.Repository.Settings
{
    using System;
    using System.IO;

    using Newtonsoft.Json;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.UI.Models;

    public static class SettingsProvider
    {
        private static SettingsModel instance;

        public static SettingsModel GetInstance()
        {
            if (instance != null) return instance;

            if (File.Exists(SettingsUpdated.SettingsPath) == false)
            {
                Logger.Log.Debug("Settings file do not exist. Creating new one");
                instance = CreateNewSettingsInstance();
            }
            else
            {
                try
                {
                    instance = JsonConvert.DeserializeObject<SettingsModel>(
                        File.ReadAllText(SettingsUpdated.SettingsPath),
                        new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace });
                }
                catch (Exception e)
                {
                    Logger.Log.Error(
                        $"Error on {SettingsUpdated.SettingsPath} file load - {e.Message}. Applying default settings");
                    instance = CreateNewSettingsInstance();
                }
            }

            instance.IsSettingsLoaded = true;
            return instance;
        }

        private static SettingsModel CreateNewSettingsInstance()
        {
            var newSettingsInstance = new SettingsModel();

            File.WriteAllText(SettingsUpdated.SettingsPath, JsonConvert.SerializeObject(newSettingsInstance, Formatting.Indented));

            return newSettingsInstance;
        }
    }
}