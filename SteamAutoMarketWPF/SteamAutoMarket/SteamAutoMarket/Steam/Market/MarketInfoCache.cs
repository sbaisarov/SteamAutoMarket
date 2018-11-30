namespace SteamAutoMarket.Steam.Market
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    using Newtonsoft.Json;

    using SteamAutoMarket.Steam.Market.Models;

    public class MarketInfoCache
    {
        public static readonly string CacheFilePath = AppDomain.CurrentDomain.BaseDirectory + "items_id_cache.ini";

        private static Dictionary<string, MarketItemInfo> cache;

        private static int newValuesCounter;

        public static void Cache(int appid, string hashName, MarketItemInfo info)
        {
            Get()[$"{appid}-{hashName}"] = info;
            newValuesCounter++;
            UpdateAll();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Clear()
        {
            cache.Clear();
            File.WriteAllText(
                CacheFilePath,
                JsonConvert.SerializeObject(new Dictionary<string, MarketItemInfo>(), Formatting.Indented));
        }

        public static Dictionary<string, MarketItemInfo> Get()
        {
            if (cache != null)
            {
                return cache;
            }

            if (File.Exists(CacheFilePath))
            {
                cache = JsonConvert.DeserializeObject<Dictionary<string, MarketItemInfo>>(
                    File.ReadAllText(CacheFilePath));
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
            if (newValuesCounter == 10)
            {
                File.WriteAllText(CacheFilePath, JsonConvert.SerializeObject(Get(), Formatting.Indented));
                newValuesCounter = 0;
            }
        }
    }
}