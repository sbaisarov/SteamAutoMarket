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
        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.IsoDateFormat };

        public PricesCash(string filePath) {
            CACHE_PRICES_PATH = filePath;
        }

        private Dictionary<string, LoadedItemPrice> Get() {
            if (CACHE == null) {
                if (File.Exists(CACHE_PRICES_PATH)) {
                    CACHE = JsonConvert.DeserializeObject<Dictionary<string, LoadedItemPrice>>(File.ReadAllText(CACHE_PRICES_PATH), _jsonSettings);
                } else {
                    CACHE = new Dictionary<string, LoadedItemPrice>();
                    UpdateAll();
                }
            }
            return CACHE;
        }

        public LoadedItemPrice Get(RgFullItem item) {
            //todo some login with time
            Get().TryGetValue(item.Description.market_hash_name, out LoadedItemPrice cached);
            return cached;
        }

        public void Cache(string hashName, double price) {
            Get()[hashName] = new LoadedItemPrice(DateTime.Now, price);
            UpdateAll();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateAll() {
            File.WriteAllText(CACHE_PRICES_PATH, JsonConvert.SerializeObject(Get(), Formatting.Indented, _jsonSettings));
        }
    }
}
