using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using SteamAutoMarket.Steam.Market.Models;

namespace SteamAutoMarket.Steam.Market
{
    internal class MarketInfoCache
    {
        public static readonly string CachePricesPath = AppDomain.CurrentDomain.BaseDirectory + "item_ids_cache.ini";
        private static Dictionary<string, MarketItemInfo> _cache;

        public static Dictionary<string, MarketItemInfo> Get()
        {
            if (_cache != null) return _cache;

            if (File.Exists(CachePricesPath))
            {
                _cache = JsonConvert.DeserializeObject<Dictionary<string, MarketItemInfo>>(
                    File.ReadAllText(CachePricesPath));
            }
            else
            {
                _cache = new Dictionary<string, MarketItemInfo>();
                UpdateAll();
            }

            return _cache;
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
            File.WriteAllText(CachePricesPath, JsonConvert.SerializeObject(Get(), Formatting.Indented));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Clear()
        {
            _cache.Clear();
            File.WriteAllText(CachePricesPath,
                JsonConvert.SerializeObject(new Dictionary<string, MarketItemInfo>(), Formatting.Indented));
        }
    }
}