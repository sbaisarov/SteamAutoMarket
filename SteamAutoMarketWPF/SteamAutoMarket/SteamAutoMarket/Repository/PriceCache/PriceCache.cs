namespace SteamAutoMarket.Repository.PriceCache
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Newtonsoft.Json;

    using SteamAutoMarket.Models;

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

        public Dictionary<string, CachedPriceModel> Get()
        {
            if (this.cache != null)
            {
                return this.cache;
            }

            if (File.Exists(this.FilePath))
            {
                this.cache = JsonConvert.DeserializeObject<Dictionary<string, CachedPriceModel>>(
                    File.ReadAllText(this.FilePath));

                this.UpdateAll();
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

        public void Cache(string hashName, double price)
        {
            if (price == 0 || double.IsNaN(price))
            {
                return;
            }

            this.Get()[hashName] = new CachedPriceModel(DateTime.Now, price);
            this.UpdateAll();
        }

        public void ClearOld()
        {
            this.cache = this.cache.Where(pair => this.IsOld(pair.Value) == false)
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
            this.fileUpdateCounter += 1;
            this.UpdateAll();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void UpdateAll(bool force = false)
        {
            if (++this.fileUpdateCounter != 10 && !force)
            {
                return;
            }

            File.WriteAllText(this.FilePath, JsonConvert.SerializeObject(this.Get(), Formatting.Indented));
            this.fileUpdateCounter = 0;
        }

        private bool IsOld(CachedPriceModel item) => item.ParseTime.AddHours(this.hoursToBecomeOld) < DateTime.Now;
    }
}