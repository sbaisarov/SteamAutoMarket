namespace SteamAutoMarket.Steam
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Threading;
    using Newtonsoft.Json;
    using RestSharp;
    using SteamAutoMarket.Core;
    using SteamAutoMarket.UI.Repository.Settings;

    public static class SetsHelper
    {
        private static readonly Semaphore UpdateFileSemaphore = new Semaphore(1, 1);
        private static readonly string SetsDbFileName;
        private static readonly Dictionary<string, int> Sets;
        private static int _updateFileCounter;

        static SetsHelper()
        {
            try
            {
                SetsDbFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sets_db.json");

                if (File.Exists(SetsDbFileName))
                {
                    Sets = JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(SetsDbFileName));
                }
                else
                {
                    Sets = new Dictionary<string, int>();
                    UpdateFile();
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error("Can not initialize sets database", e);
            }
        }

        public static int GetSetsCount(string gameAppid, WebProxy proxy)
        {
            try
            {
                if (Sets.TryGetValue(gameAppid, out int count))
                {
                    return count;
                }

                count = ParseSetsCount(gameAppid, SettingsProvider.GetInstance().SteamIdToParseSets, proxy);
                Sets.Add(gameAppid, count);

                if (++_updateFileCounter == 3)
                {
                    _updateFileCounter = 0;
                    UpdateFile();
                }

                return count;
            }
            catch (Exception e)
            {
                Logger.Log.Error("Error on sets count parse", e);
                return -1;
            }
        }

        private static void UpdateFile()
        {
            UpdateFileSemaphore.WaitOne();

            File.WriteAllText(SetsDbFileName, JsonConvert.SerializeObject(Sets));

            UpdateFileSemaphore.Release();
        }

        private static int ParseSetsCount(string gameAppid, string steamId, WebProxy proxy)
        {
            var client = new RestClient($"https://steamcommunity.com/profiles/{steamId}/gamecards/{gameAppid}/") { Proxy = proxy };

            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                Logger.Log.Info($"Set cards count can not be obtained for {steamId} user - {response.StatusDescription}");
                throw response.ErrorException ?? new WebException(response.StatusDescription);
            }

            return new Regex("img class=\"gamecard\"").Matches(response.Content).Count;
        }
    }
}
