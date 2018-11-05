namespace Steam.Market
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    using Newtonsoft.Json;

    using Steam.Market.Models;

    internal class MarketInfoCache
    {
        public static readonly string CachePricesPath = AppDomain.CurrentDomain.BaseDirectory + "item_ids_cache.ini";

        private static Dictionary<string, MarketItemInfo> cache;

        public static void Cache(int appid, string hashName, MarketItemInfo info)
        {
            Get()[$"{appid}-{hashName}"] = info;
            UpdateAll();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Clear()
        {
            cache.Clear();
            File.WriteAllText(
                CachePricesPath,
                JsonConvert.SerializeObject(new Dictionary<string, MarketItemInfo>(), Formatting.Indented));
        }

        public static Dictionary<string, MarketItemInfo> Get()
        {
            if (cache != null)
            {
                return cache;
            }

            if (File.Exists(CachePricesPath))
            {
                cache = JsonConvert.DeserializeObject<Dictionary<string, MarketItemInfo>>(
                    File.ReadAllText(CachePricesPath));
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
            File.WriteAllText(CachePricesPath, JsonConvert.SerializeObject(Get(), Formatting.Indented));
        }
    }
}