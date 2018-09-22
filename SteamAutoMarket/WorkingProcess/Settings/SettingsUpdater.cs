namespace SteamAutoMarket.WorkingProcess.Settings
{
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    internal class SettingsUpdater
    {
        internal static bool IsPending;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateSettings()
        {
            if (IsPending) return;
            IsPending = true;

            Task.Run(
                () =>
                    {
                        Thread.Sleep(5000);
                        File.WriteAllText(
                            SavedSettings.SettingsFilePath,
                            JsonConvert.SerializeObject(SavedSettings.Get(), Formatting.Indented));
                        
                        IsPending = false;
                    });
        }
    }
}