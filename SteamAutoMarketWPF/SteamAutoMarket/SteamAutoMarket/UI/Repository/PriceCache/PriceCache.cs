namespace SteamAutoMarket.UI.Repository.PriceCache
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    using Newtonsoft.Json;

    using SteamAutoMarket.Core;

    [Obfuscation(Exclude = true)]
    public class PriceCache
    {
        private readonly int hoursToBecomeOld;

        private Dictionary<string, CachedPriceModel> cache;

        private int fileUpdateCounter;

        public PriceCache(string filePath, int hoursToBecomeOld)
        {
            this.FilePath = filePath;
            this.hoursToBecomeOld = hoursToBecomeOld;
        }

        public string FilePath { get; }

        public void Cache(string hashName, double price)
        {
            if (price == 0 || double.IsNaN(price))
            {
                return;
            }

            this.Get()[hashName] = new CachedPriceModel(DateTime.Now, price);
            this.UpdateAll();
        }

        public void Clear()
        {
            this.cache.Clear();
            this.UpdateAll(true);
        }

        public void ClearOld()
        {
            this.cache = this.cache.Where(pair => this.IsOld(pair.Value) == false)
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            this.UpdateAll(true);
        }

        public Dictionary<string, CachedPriceModel> Get()
        {
            if (this.cache != null)
            {
                return this.cache;
            }

            if (File.Exists(this.FilePath))
            {
                try
                {
                    var cacheList = JsonConvert.DeserializeObject<List<PriceCacheModel>>(
                        File.ReadAllText(this.FilePath));

                    this.cache = cacheList.ToDictionary(
                        k => k.MarketHashName,
                        v => new CachedPriceModel(v.ParseTime, v.Price));

                    this.UpdateAll();
                }
                catch (Exception ex)
                {
                    Logger.Log.Error($"Error on {this.FilePath} processing - {ex.Message}", ex);
                    this.cache = new Dictionary<string, CachedPriceModel>();
                }
            }
            else
            {
                this.cache = new Dictionary<string, CachedPriceModel>();
                this.UpdateAll();
            }

            return this.cache;
        }

        public CachedPriceModel Get(string hashName)
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

        public void Uncache(string hashName)
        {
            this.Get().Remove(hashName);
            this.UpdateAll();
        }

        private bool IsOld(CachedPriceModel item) => item.ParseTime.AddHours(this.hoursToBecomeOld) < DateTime.Now;

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void UpdateAll(bool force = false)
        {
            try
            {
                if (++this.fileUpdateCounter > 20 && !force)
                {
                    return;
                }

                this.fileUpdateCounter = 0;
                var cacheList = this.cache.ToList().Select(
                    e => new PriceCacheModel
                             {
                                 MarketHashName = e.Key, Price = e.Value.Price, ParseTime = e.Value.ParseTime
                             });

                File.WriteAllText(this.FilePath, JsonConvert.SerializeObject(cacheList, Formatting.Indented));
            }
            catch (Exception e)
            {
                Logger.Log.Error("Error on price cache file save", e);
            }
        }
    }
}