namespace SteamAutoMarket.Steam.Market
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Newtonsoft.Json;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.Steam.Market.Models;

    public class MarketInfoCache
    {
        public static readonly string CacheFilePath = AppDomain.CurrentDomain.BaseDirectory + "items_id_cache.ini";

        private static Dictionary<string, MarketItemInfo> cache;

        private static int newValuesCounter;

        public static void Cache(int appid, string hashName, MarketItemInfo info)
        {
            Get()[$"{appid}-{hashName}"] = info;
            UpdateAll();
        }

        public static Dictionary<string, MarketItemInfo> Get()
        {
            if (cache != null)
            {
                return cache;
            }

            if (File.Exists(CacheFilePath))
            {
                try
                {
                    var cacheList = JsonConvert.DeserializeObject<List<MarketInfoCacheModel>>(
                        File.ReadAllText(CacheFilePath));

                    cache = cacheList.ToDictionary(
                        k => k.AppIdHashName,
                        v => new MarketItemInfo { NameId = v.NameId, PublisherFeePercent = v.PublisherFeePercent });
                }
                catch (Exception e)
                {
                    Logger.Log.Error($"Error on {CacheFilePath} processing - {e.Message}", e);
                    cache = new Dictionary<string, MarketItemInfo>();
                }
            }

            if (cache != null)
            {
                return cache;
            }

            cache = new Dictionary<string, MarketItemInfo>();
            UpdateAll();

            return cache;
        }

        public static MarketItemInfo Get(int appid, string hashName)
        {
            Get().TryGetValue($"{appid}-{hashName}", out var cached);
            return cached;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateAll()
        {
            if (++newValuesCounter == 20)
            {
                var cacheList = Get().ToList().Select(e => new MarketInfoCacheModel(e.Key, e.Value));
                File.WriteAllText(CacheFilePath, JsonConvert.SerializeObject(cacheList, Formatting.Indented));
                newValuesCounter = 0;
            }
        }
    }
}