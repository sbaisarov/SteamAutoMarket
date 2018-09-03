using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using autotrade.Steam.TradeOffer.Models.Full;
using Newtonsoft.Json;

namespace autotrade.WorkingProcess.PriceLoader
{
    internal class PricesCache
    {
        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
            {DateFormatHandling = DateFormatHandling.IsoDateFormat};

        private readonly int HOURS_TO_BECOME_OLD;
        private Dictionary<string, LoadedItemPrice> CACHE;
        private int Counter;

        public PricesCache(string filePath, int hoursToBecomeOld)
        {
            CACHE_PRICES_PATH = filePath;
            HOURS_TO_BECOME_OLD = hoursToBecomeOld;
        }

        public string CACHE_PRICES_PATH { get; }

        public Dictionary<string, LoadedItemPrice> Get()
        {
            if (CACHE == null)
            {
                if (File.Exists(CACHE_PRICES_PATH))
                {
                    CACHE = JsonConvert.DeserializeObject<Dictionary<string, LoadedItemPrice>>(
                        File.ReadAllText(CACHE_PRICES_PATH), _jsonSettings);
                    UpdateAll();
                }
                else
                {
                    CACHE = new Dictionary<string, LoadedItemPrice>();
                    UpdateAll();
                }
            }

            return CACHE;
        }

        public LoadedItemPrice Get(FullRgItem item)
        {
            Get().TryGetValue(item.Description.MarketHashName, out var cached);
            if (cached == null) return null;

            if (IsOld(cached))
            {
                Uncache(item.Description.MarketHashName);
                return null;
            }

            return cached;
        }

        public void Cache(string hashName, double price)
        {
            if (price == 0 || double.IsNaN(price)) return;
            Get()[hashName] = new LoadedItemPrice(DateTime.Now, price);
            UpdateAll();
        }

        public void ClearOld()
        {
            CACHE = CACHE
                .Where(pair => IsOld(pair.Value) == false)
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            UpdateAll(true);
        }

        public void Clear()
        {
            CACHE.Clear();
            UpdateAll(true);
        }

        public void Uncache(string hashName)
        {
            Get().Remove(hashName);
            Counter += 1;
            UpdateAll();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void UpdateAll(bool force = false)
        {
            if (++Counter == 10 || force)
            {
                File.WriteAllText(CACHE_PRICES_PATH,
                    JsonConvert.SerializeObject(Get(), Formatting.Indented, _jsonSettings));
                Counter = 0;
            }
        }

        public bool IsOld(LoadedItemPrice item)
        {
            return item.ParseTime.AddHours(HOURS_TO_BECOME_OLD) < DateTime.Now;
        }
    }
}