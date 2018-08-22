using Market.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace autotrade.Steam.Market {
    class MarketInfoCache {
        private static Dictionary<string, MarketItemInfo> CACHE;
        private static readonly string CACHE_PRICES_PATH = "item_ids_cache.ini";

        public static Dictionary<string, MarketItemInfo> Get() {
            if (CACHE == null) {
                if (File.Exists(CACHE_PRICES_PATH)) {
                    CACHE = JsonConvert.DeserializeObject<Dictionary<string, MarketItemInfo>>(File.ReadAllText(CACHE_PRICES_PATH));
                } else {
                    CACHE = new Dictionary<string, MarketItemInfo>();
                    UpdateAll();
                }
            }
            return CACHE;
        }

        public static MarketItemInfo Get(int appid, string hashName) {
            Get().TryGetValue($"{appid}-{hashName}", out MarketItemInfo cached);
            return cached;
        }

        public static void Cache(int appid, string hashName, MarketItemInfo info) {
            Get()[$"{appid}-{hashName}"] = info;
            UpdateAll();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateAll() {
            File.WriteAllText(CACHE_PRICES_PATH, JsonConvert.SerializeObject(Get(), Formatting.Indented));
        }
    }
}

