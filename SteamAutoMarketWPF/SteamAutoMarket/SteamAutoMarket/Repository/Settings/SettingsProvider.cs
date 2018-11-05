namespace SteamAutoMarket.Repository.Settings
{
    using System;
    using System.IO;

    using Newtonsoft.Json;

    public static class SettingsProvider
    {
        private static readonly string SettingsPath = AppDomain.CurrentDomain.BaseDirectory + "settings.ini";

        private static SettingsModel instance;

        public static SettingsModel GetInstance()
        {
            if (instance != null) return instance;

            if (File.Exists(SettingsPath) == false)
            {
                instance = new SettingsModel();

                File.WriteAllText(SettingsPath, JsonConvert.SerializeObject(instance, Formatting.Indented));
            }
            else
            {
                instance = JsonConvert.DeserializeObject<SettingsModel>(
                    File.ReadAllText(SettingsPath),
                    new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace });
            }

            return instance;
        }
    }
}