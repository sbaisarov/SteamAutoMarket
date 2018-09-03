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
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
            {DateFormatHandling = DateFormatHandling.IsoDateFormat};

        private readonly int _hoursToBecomeOld;
        private Dictionary<string, LoadedItemPrice> _cache;
        private int _counter;

        public PricesCache(string filePath, int hoursToBecomeOld)
        {
            CachePricesPath = filePath;
            _hoursToBecomeOld = hoursToBecomeOld;
        }

        public string CachePricesPath { get; }

        public Dictionary<string, LoadedItemPrice> Get()
        {
            if (_cache == null)
            {
                if (File.Exists(CachePricesPath))
                {
                    _cache = JsonConvert.DeserializeObject<Dictionary<string, LoadedItemPrice>>(
                        File.ReadAllText(CachePricesPath), JsonSettings);
                    UpdateAll();
                }
                else
                {
                    _cache = new Dictionary<string, LoadedItemPrice>();
                    UpdateAll();
                }
            }

            return _cache;
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
            _cache = _cache
                .Where(pair => IsOld(pair.Value) == false)
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            UpdateAll(true);
        }

        public void Clear()
        {
            _cache.Clear();
            UpdateAll(true);
        }

        public void Uncache(string hashName)
        {
            Get().Remove(hashName);
            _counter += 1;
            UpdateAll();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void UpdateAll(bool force = false)
        {
            if (++_counter == 10 || force)
            {
                File.WriteAllText(CachePricesPath,
                    JsonConvert.SerializeObject(Get(), Formatting.Indented, JsonSettings));
                _counter = 0;
            }
        }

        public bool IsOld(LoadedItemPrice item)
        {
            return item.ParseTime.AddHours(_hoursToBecomeOld) < DateTime.Now;
        }
    }
}