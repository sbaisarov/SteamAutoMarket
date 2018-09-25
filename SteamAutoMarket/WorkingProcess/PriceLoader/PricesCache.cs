namespace SteamAutoMarket.WorkingProcess.PriceLoader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Newtonsoft.Json;

    using SteamAutoMarket.Steam.Market.Models;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;

    internal class PricesCache
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
                                                                          {
                                                                              DateFormatHandling =
                                                                                  DateFormatHandling.IsoDateFormat
                                                                          };

        private readonly int hoursToBecomeOld;

        private Dictionary<string, LoadedItemPrice> cache;

        private int counter;

        public PricesCache(string filePath, int hoursToBecomeOld)
        {
            this.CachePricesPath = filePath;
            this.hoursToBecomeOld = hoursToBecomeOld;
        }

        public string CachePricesPath { get; }

        public Dictionary<string, LoadedItemPrice> Get()
        {
            if (this.cache != null)
            {
                return this.cache;
            }

            if (File.Exists(this.CachePricesPath))
            {
                this.cache = JsonConvert.DeserializeObject<Dictionary<string, LoadedItemPrice>>(
                    File.ReadAllText(this.CachePricesPath),
                    JsonSettings);
                this.UpdateAll();
            }
            else
            {
                this.cache = new Dictionary<string, LoadedItemPrice>();
                this.UpdateAll();
            }

            return this.cache;
        }

        public LoadedItemPrice Get(string hashName)
        {
            this.Get().TryGetValue(hashName, out var cached);
            if (cached == null)
            {
                return null;
            }

            if (!this.IsOld(cached))
            {
                return cached;
            }

            this.Uncache(hashName);
            return null;
        }

        public void Cache(string hashName, double price)
        {
            if (price == 0 || double.IsNaN(price))
            {
                return;
            }

            this.Get()[hashName] = new LoadedItemPrice(DateTime.Now, price);
            this.UpdateAll();
        }

        public void ClearOld()
        {
            this.cache = this.cache.Where(pair => IsOld(pair.Value) == false)
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            this.UpdateAll(true);
        }

        public void Clear()
        {
            this.cache.Clear();
            this.UpdateAll(true);
        }

        public void Uncache(string hashName)
        {
            this.Get().Remove(hashName);
            this.counter += 1;
            this.UpdateAll();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void UpdateAll(bool force = false)
        {
            if (++this.counter != 10 && !force)
            {
                return;
            }

            File.WriteAllText(
                this.CachePricesPath,
                JsonConvert.SerializeObject(this.Get(), Formatting.Indented, JsonSettings));
            this.counter = 0;
        }

        public bool IsOld(LoadedItemPrice item)
        {
            return item.ParseTime.AddHours(this.hoursToBecomeOld) < DateTime.Now;
        }
    }
}