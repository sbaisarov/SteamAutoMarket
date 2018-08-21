using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static autotrade.Steam.TradeOffer.Inventory;

namespace autotrade.WorkingProcess.PriceLoader {
    class PricesCache {
        private Dictionary<string, LoadedItemPrice> CACHE;
        private readonly string CACHE_PRICES_PATH;
        private int HOURS_TO_BECOME_OLD;
        private int Counter = 0;
        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };

        public PricesCache(string filePath, int hoursToBecomeOld) {
            CACHE_PRICES_PATH = filePath;
            HOURS_TO_BECOME_OLD = hoursToBecomeOld;
        }

        private Dictionary<string, LoadedItemPrice> Get() {
            if (CACHE == null) {
                if (File.Exists(CACHE_PRICES_PATH)) {
                    CACHE = JsonConvert.DeserializeObject<Dictionary<string, LoadedItemPrice>>(File.ReadAllText(CACHE_PRICES_PATH), _jsonSettings);
                    CACHE = CACHE.Where(x => !IsOld(x.Value)).ToDictionary(x => x.Key, x => x.Value);
                    UpdateAll();
                } else {
                    CACHE = new Dictionary<string, LoadedItemPrice>();
                    UpdateAll();
                }
            }
            return CACHE;
        }

        public LoadedItemPrice Get(RgFullItem item) {
            Get().TryGetValue(item.Description.market_hash_name, out LoadedItemPrice cached);
            if (cached == null) return null;

            if (IsOld(cached)) {
                Uncache(item.Description.market_hash_name);
                return null;
            }
            return cached;
        }

        public void Cache(string hashName, double price) {
            if (price == 0 || double.IsNaN(price)) return;
            Get()[hashName] = new LoadedItemPrice(DateTime.Now, price);
            UpdateAll();
        }

        public void Uncache(string hashName) {
            Get().Remove(hashName);
            Counter += 1;
            UpdateAll();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void UpdateAll()
        {
            Counter += 1;
            if (Counter == 10)
            {
                File.WriteAllText(CACHE_PRICES_PATH, JsonConvert.SerializeObject(Get(), Formatting.Indented, _jsonSettings));
                Counter = 0;
            }
        }

        private bool IsOld(LoadedItemPrice item) {
            return item.ParseTime.AddHours(HOURS_TO_BECOME_OLD) < DateTime.Now;
        }
    }
}
