namespace SteamAutoMarket.Repository.Settings
{
    using System.IO;

    using Newtonsoft.Json;

    using SteamAutoMarket.Models;

    public static class SettingsProvider
    {
        private static SettingsModel instance;

        public static SettingsModel GetInstance()
        {
            if (instance != null) return instance;

            if (File.Exists(SettingsUpdated.SettingsPath) == false)
            {
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