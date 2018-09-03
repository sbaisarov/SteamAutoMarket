using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using autotrade.Steam.Market.Models;
using Newtonsoft.Json;

namespace autotrade.Steam.Market
{
    internal class MarketInfoCache
    {
        public static readonly string CACHE_PRICES_PATH = AppDomain.CurrentDomain.BaseDirectory + "item_ids_cache.ini";
        private static Dictionary<string, MarketItemInfo> CACHE;

        public static Dictionary<string, MarketItemInfo> Get()
        {
            if (CACHE == null)
            {
                if (File.Exists(CACHE_PRICES_PATH))
                {
                    CACHE = JsonConvert.DeserializeObject<Dictionary<string, MarketItemInfo>>(
                        File.ReadAllText(CACHE_PRICES_PATH));
                }
                else
                {
                    CACHE = new Dictionary<string, MarketItemInfo>();
                    UpdateAll();
                }
            }

            return CACHE;
        }

        public static MarketItemInfo Get(int appid, string hashName)
        {
            Get().TryGetValue($"{appid}-{hashName}", out var cached);
            return cached;
        }

        public static void Cache(int appid, string hashName, MarketItemInfo info)
        {
            Get()[$"{appid}-{hashName}"] = info;
            UpdateAll();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateAll()
        {
            File.WriteAllText(CACHE_PRICES_PATH, JsonConvert.SerializeObject(Get(), Formatting.Indented));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Clear()
        {
            CACHE.Clear();
            File.WriteAllText(CACHE_PRICES_PATH,
                JsonConvert.SerializeObject(new Dictionary<string, MarketItemInfo>(), Formatting.Indented));
        }
    }
}