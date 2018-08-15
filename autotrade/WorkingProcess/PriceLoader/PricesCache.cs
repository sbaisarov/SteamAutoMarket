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
    class PricesCash {
        private Dictionary<string, LoadedItemPrice> CACHE;
        private readonly string CACHE_PRICES_PATH;
        private int HOURS_TO_BECOME_OLD;
        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };

        public PricesCash(string filePath, int hoursToBecomeOld) {
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
            if (price == 0 || price == Double.NaN) return;
            Get()[hashName] = new LoadedItemPrice(DateTime.Now, price);
            UpdateAll();
        }

        public void Uncache(string hashName) {
            Get().Remove(hashName);
            UpdateAll();
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateAll() {
            File.WriteAllText(CACHE_PRICES_PATH, JsonConvert.SerializeObject(Get(), Formatting.Indented, _jsonSettings));
        }

        private bool IsOld(LoadedItemPrice item) {
            return item.ParseTime.AddHours(HOURS_TO_BECOME_OLD) < DateTime.Now;
        }
    }
}
