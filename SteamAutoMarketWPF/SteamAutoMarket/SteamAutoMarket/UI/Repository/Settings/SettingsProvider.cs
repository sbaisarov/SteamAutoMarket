namespace SteamAutoMarket.UI.Repository.Settings
{
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
                UiGlobalVariables.FileLogger.Debug("Settings file do not exist. Creating new one");
                instance = new SettingsModel();

                File.WriteAllText(
                    SettingsUpdated.SettingsPath,
                    JsonConvert.SerializeObject(instance, Formatting.Indented));
            }
            else
            {
                instance = JsonConvert.DeserializeObject<SettingsModel>(
                    File.ReadAllText(SettingsUpdated.SettingsPath),
                    new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace });
            }

            instance.IsSettingsLoaded = true;
            return instance;
        }
    }
}