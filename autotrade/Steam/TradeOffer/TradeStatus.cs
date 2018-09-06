using System.Collections.Generic;
using Newtonsoft.Json;
using SteamAutoMarket.Steam.TradeOffer.Models;

namespace SteamAutoMarket.Steam.TradeOffer
{
    public class TradeStatus
    {
        public TradeStatus()
        {
            Version = 1;
            MyOfferedItems = new TradeStatusUser();
            TheirOfferedItems = new TradeStatusUser();
        }

        public TradeStatus(IEnumerable<TradeAsset> myItems, IEnumerable<TradeAsset> theirItems)
        {
            Version = 1;
            MyOfferedItems = new TradeStatusUser();
            TheirOfferedItems = new TradeStatusUser();
            foreach (var asset in myItems) MyOfferedItems.AddItem(asset);
            foreach (var asset in theirItems) TheirOfferedItems.AddItem(asset);
        }

        [JsonProperty("newversion")] public bool NewVersion { get; private set; }

        [JsonProperty("version")] private int Version { get; set; }

        [JsonProperty("me")] private TradeStatusUser MyOfferedItems { get; set; }

        [JsonProperty("them")] private TradeStatusUser TheirOfferedItems { get; set; }

        //checks if version needs to be updated
        private bool ShouldUpdate(bool check)
        {
            if (check)
            {
                NewVersion = true;
                Version++;
                return true;
            }

            return false;
        }

        public bool AddMyItem(int appId, long contextId, long assetId, long amount = 1)
        {
            var asset = new TradeAsset();
            asset.CreateItemAsset(appId, contextId, assetId, amount);
            return ShouldUpdate(MyOfferedItems.AddItem(asset));
        }

        public bool AddTheirItem(int appId, long contextId, long assetId, long amount = 1)
        {
            var asset = new TradeAsset();
            asset.CreateItemAsset(appId, contextId, assetId, amount);
            return ShouldUpdate(TheirOfferedItems.AddItem(asset));
        }

        public bool AddMyCurrencyItem(int appId, long contextId, long currencyId, long amount)
        {
            var asset = new TradeAsset();
            asset.CreateCurrencyAsset(appId, contextId, currencyId, amount);
            return ShouldUpdate(MyOfferedItems.AddCurrencyItem(asset));
        }

        public bool AddTheirCurrencyItem(int appId, long contextId, long currencyId, long amount)
        {
            var asset = new TradeAsset();
            asset.CreateCurrencyAsset(appId, contextId, currencyId, amount);
            return ShouldUpdate(TheirOfferedItems.AddCurrencyItem(asset));
        }

        public bool RemoveMyItem(int appId, long contextId, long assetId, long amount = 1)
        {
            var asset = new TradeAsset();
            asset.CreateItemAsset(appId, contextId, assetId, amount);
            return ShouldUpdate(MyOfferedItems.RemoveItem(asset));
        }

        public bool RemoveTheirItem(int appId, long contextId, long assetId, long amount = 1)
        {
            var asset = new TradeAsset();
            asset.CreateItemAsset(appId, contextId, assetId, amount);
            return ShouldUpdate(TheirOfferedItems.RemoveItem(asset));
        }

        public bool RemoveMyCurrencyItem(int appId, long contextId, long currencyId, long amount)
        {
            var asset = new TradeAsset();
            asset.CreateCurrencyAsset(appId, contextId, currencyId, amount);
            return ShouldUpdate(MyOfferedItems.RemoveCurrencyItem(asset));
        }

        public bool RemoveTheirCurrencyItem(int appId, long contextId, long currencyId, long amount)
        {
            var asset = new TradeAsset();
            asset.CreateCurrencyAsset(appId, contextId, currencyId, amount);
            return ShouldUpdate(TheirOfferedItems.RemoveCurrencyItem(asset));
        }

        public bool TryGetMyItem(int appId, long contextId, long assetId, long amount, out TradeAsset asset)
        {
            var tradeAsset = new TradeAsset
            {
                AppId = appId,
                Amount = amount,
                AssetId = assetId,
                ContextId = contextId
            };
            asset = new TradeAsset();
            foreach (var item in MyOfferedItems.Assets)
                if (item.Equals(tradeAsset))
                {
                    asset = item;
                    return true;
                }

            return false;
        }

        public bool TryGetTheirItem(int appId, long contextId, long assetId, long amount, out TradeAsset asset)
        {
            var tradeAsset = new TradeAsset
            {
                AppId = appId,
                Amount = amount,
                AssetId = assetId,
                ContextId = contextId
            };
            asset = new TradeAsset();
            foreach (var item in TheirOfferedItems.Assets)
                if (item.Equals(tradeAsset))
                {
                    asset = item;
                    return true;
                }

            return false;
        }

        public bool TryGetMyCurrencyItem(int appId, long contextId, long currencyId, long amount,
            out TradeAsset asset)
        {
            var tradeAsset = new TradeAsset
            {
                AppId = appId,
                Amount = amount,
                CurrencyId = currencyId,
                ContextId = contextId
            };
            asset = new TradeAsset();
            foreach (var item in MyOfferedItems.Currency)
                if (item.Equals(tradeAsset))
                {
                    asset = item;
                    return true;
                }

            return false;
        }

        public bool TryGetTheirCurrencyItem(int appId, long contextId, long currencyId, long amount,
            out TradeAsset asset)
        {
            var tradeAsset = new TradeAsset
            {
                AppId = appId,
                Amount = amount,
                CurrencyId = currencyId,
                ContextId = contextId
            };
            asset = new TradeAsset();
            foreach (var item in TheirOfferedItems.Currency)
                if (item.Equals(tradeAsset))
                {
                    asset = item;
                    return true;
                }

            return false;
        }

        public List<TradeAsset> GetMyItems()
        {
            return MyOfferedItems.Assets;
        }

        public List<TradeAsset> GetTheirItems()
        {
            return TheirOfferedItems.Assets;
        }
    }
}
