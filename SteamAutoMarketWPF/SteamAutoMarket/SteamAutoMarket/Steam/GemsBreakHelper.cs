namespace SteamAutoMarket.Steam
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Threading;
    using Newtonsoft.Json;
    using SteamAutoMarket.Core;
    using SteamAutoMarket.Steam.Auth;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;

    public static class GemsBreakHelper
    {
        private static readonly Semaphore UpdateFileSemaphore = new Semaphore(1, 1);
        private static readonly string GemsDbFileName;
        private static readonly Dictionary<string, int> Gems;
        private static int _updateFileCounter;

        static GemsBreakHelper()
        {
            try
            {
                GemsDbFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gems_db.json");

                if (File.Exists(GemsDbFileName))
                {
                    Gems = JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText(GemsDbFileName));
                }
                else
                {
                    Gems = new Dictionary<string, int>();
                    UpdateFile();
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error("Can not initialize Gems database", e);
            }
        }

        public static void UpdateFile()
        {
            UpdateFileSemaphore.WaitOne();

            File.WriteAllText(GemsDbFileName, JsonConvert.SerializeObject(Gems));

            UpdateFileSemaphore.Release();
        }

        public static bool TryGetGemsCount(FullRgItem item, out int gemsCount)
        {
            return Gems.TryGetValue(item.Description.MarketHashName, out gemsCount);
        }

        public static int GetGemsCount(FullRgItem item, CookieContainer steamCookies, WebProxy proxy = null)
        {
            var ownerTag = item.Description.OwnerActions?.FirstOrDefault(t => t.Name.Equals("Turn into Gems...", StringComparison.InvariantCultureIgnoreCase));

            int gemsCount = 0;

            if (ownerTag == null)
            {
                Gems.Add(item.Description.MarketHashName, gemsCount);
                return gemsCount;
            }

            if (TryGetGemsCount(item, out gemsCount))
            {
                return gemsCount;
            }
            
            //javascript:GetGooValue( '%contextid%', '%assetid%', 603770, 3, 1 )
            var regex = Regex.Match(ownerTag.Link, "javascript:GetGooValue\\( '%contextid%', '%assetid%', (\\d+), (\\d+), (\\d+) \\)");

            var appId = regex.Groups[1].Value;
            var itemType = regex.Groups[2].Value;
            var border = regex.Groups[3].Value;

            var response = SteamWeb.Request(
                $"https://steamcommunity.com/auction/ajaxgetgoovalueforitemtype/?appid={appId}&item_type={itemType}&border_color={border}",
                "GET",
                data: null,
                cookies: steamCookies,
                proxy: proxy);

            var json = JsonConvert.DeserializeObject<GooResponse>(response);

            if (json.Success != 1)
            {
                throw new WebException($"Success status is {json.Success}");
            }

            gemsCount = int.Parse(json.GooValue);

            Gems.Add(item.Description.MarketHashName, gemsCount);
            if (++_updateFileCounter == 5)
            {
                _updateFileCounter = 0;
                UpdateFile();
            }

            return gemsCount;
        }

        public static void BreakOnGems(string sessionId, string appId, string assetId, string contextId, int expectedGemsValue, ulong steamId, CookieContainer cookies, WebProxy proxy)
        {
            var response = SteamWeb.Request(
                $"https://steamcommunity.com/profiles/{steamId}/ajaxgrindintogoo/",
                "POST",
                new NameValueCollection
                {
                    { "sessionid", sessionId },
                    { "appid", appId },
                    { "assetid", assetId },
                    { "contextid", contextId },
                    { "goo_value_expected", expectedGemsValue.ToString() },
                },
                cookies,
                referer: $"https://steamcommunity.com/profiles/{steamId}/inventory/",
                proxy: proxy);

            if (response == null)
            {
                throw new WebException("Steam response is empty");
            }

            var json = JsonConvert.DeserializeObject<BreakGemsResponse>(response);
            if (json.Success != 1)
            {
                throw new WebException($"Response success is {json.Success}");
            }
        }
    }
}
