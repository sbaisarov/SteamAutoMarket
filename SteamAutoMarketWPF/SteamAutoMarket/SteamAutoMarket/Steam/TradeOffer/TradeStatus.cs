namespace SteamAutoMarket.Steam.TradeOffer
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using SteamAutoMarket.Steam.TradeOffer.Models;

    public class TradeStatus
    {
        public TradeStatus()
        {
            this.Version = 1;
            this.MyOfferedItems = new TradeStatusUser();
            this.TheirOfferedItems = new TradeStatusUser();
        }

        public TradeStatus(IEnumerable<TradeAsset> myItems, IEnumerable<TradeAsset> theirItems)
        {
            this.Version = 1;
            this.MyOfferedItems = new TradeStatusUser();
            this.TheirOfferedItems = new TradeStatusUser();
            foreach (var asset in myItems) this.MyOfferedItems.AddItem(asset);
            foreach (var asset in theirItems) this.TheirOfferedItems.AddItem(asset);
        }

        [JsonProperty("newversion")]
        public bool NewVersion { get; private set; }

        [JsonProperty("me")]
        private TradeStatusUser MyOfferedItems { get; set; }

        [JsonProperty("them")]
        private TradeStatusUser TheirOfferedItems { get; set; }

        [JsonProperty("version")]
        private int Version { get; set; }

        public bool AddMyCurrencyItem(int appId, string contextId, long currencyId, long amount)
        {
            var asset = new TradeAsset();
            asset.CreateCurrencyAsset(appId, contextId, currencyId, amount);
            return this.ShouldUpdate(this.MyOfferedItems.AddCurrencyItem(asset));
        }

        public bool AddMyItem(int appId, string contextId, string assetId, long amount = 1)
        {
            var asset = new TradeAsset();
            asset.CreateItemAsset(appId, contextId, assetId, amount);
            return this.ShouldUpdate(this.MyOfferedItems.AddItem(asset));
        }

        public bool AddTheirCurrencyItem(int appId, string contextId, long currencyId, long amount)
        {
            var asset = new TradeAsset();
            asset.CreateCurrencyAsset(appId, contextId, currencyId, amount);
            return this.ShouldUpdate(this.TheirOfferedItems.AddCurrencyItem(asset));
        }

        public bool AddTheirItem(int appId, string contextId, string assetId, long amount = 1)
        {
            var asset = new TradeAsset();
            asset.CreateItemAsset(appId, contextId, assetId, amount);
            return this.ShouldUpdate(this.TheirOfferedItems.AddItem(asset));
        }

        public List<TradeAsset> GetMyItems()
        {
            return this.MyOfferedItems.Assets;
        }

        public List<TradeAsset> GetTheirItems()
        {
            return this.TheirOfferedItems.Assets;
        }

        public bool RemoveMyCurrencyItem(int appId, string contextId, long currencyId, long amount)
        {
            var asset = new TradeAsset();
            asset.CreateCurrencyAsset(appId, contextId, currencyId, amount);
            return this.ShouldUpdate(this.MyOfferedItems.RemoveCurrencyItem(asset));
        }

        public bool RemoveMyItem(int appId, string contextId, string assetId, long amount = 1)
        {
            var asset = new TradeAsset();
            asset.CreateItemAsset(appId, contextId, assetId, amount);
            return this.ShouldUpdate(this.MyOfferedItems.RemoveItem(asset));
        }

        public bool RemoveTheirCurrencyItem(int appId, string contextId, long currencyId, long amount)
        {
            var asset = new TradeAsset();
            asset.CreateCurrencyAsset(appId, contextId, currencyId, amount);
            return this.ShouldUpdate(this.TheirOfferedItems.RemoveCurrencyItem(asset));
        }

        public bool RemoveTheirItem(int appId, string contextId, string assetId, long amount = 1)
        {
            var asset = new TradeAsset();
            asset.CreateItemAsset(appId, contextId, assetId, amount);
            return this.ShouldUpdate(this.TheirOfferedItems.RemoveItem(asset));
        }

        public bool TryGetMyCurrencyItem(
            int appId,
            string contextId,
            long currencyId,
            long amount,
            out TradeAsset asset)
        {
            var tradeAsset = new TradeAsset
                                 {
                                     AppId = appId, Amount = amount, CurrencyId = currencyId, ContextId = contextId
                                 };
            asset = new TradeAsset();
            foreach (var item in this.MyOfferedItems.Currency)
                if (item.Equals(tradeAsset))
                {
                    asset = item;
                    return true;
                }

            return false;
        }

        public bool TryGetMyItem(int appId, string contextId, string assetId, long amount, out TradeAsset asset)
        {
            var tradeAsset = new TradeAsset
                                 {
                                     AppId = appId, Amount = amount, AssetId = assetId, ContextId = contextId
                                 };
            asset = new TradeAsset();
            foreach (var item in this.MyOfferedItems.Assets)
                if (item.Equals(tradeAsset))
                {
                    asset = item;
                    return true;
                }

            return false;
        }

        public bool TryGetTheirCurrencyItem(
            int appId,
            string contextId,
            long currencyId,
            long amount,
            out TradeAsset asset)
        {
            var tradeAsset = new TradeAsset
                                 {
                                     AppId = appId, Amount = amount, CurrencyId = currencyId, ContextId = contextId
                                 };
            asset = new TradeAsset();
            foreach (var item in this.TheirOfferedItems.Currency)
                if (item.Equals(tradeAsset))
                {
                    asset = item;
                    return true;
                }

            return false;
        }

        public bool TryGetTheirItem(int appId, string contextId, string assetId, long amount, out TradeAsset asset)
        {
            var tradeAsset = new TradeAsset
                                 {
                                     AppId = appId, Amount = amount, AssetId = assetId, ContextId = contextId
                                 };
            asset = new TradeAsset();
            foreach (var item in this.TheirOfferedItems.Assets)
                if (item.Equals(tradeAsset))
                {
                    asset = item;
                    return true;
                }

            return false;
        }

        // checks if version needs to be updated
        private bool ShouldUpdate(bool check)
        {
            if (check)
            {
                this.NewVersion = true;
                this.Version++;
                return true;
            }

            return false;
        }
    }
}