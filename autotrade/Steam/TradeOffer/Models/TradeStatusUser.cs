using System.Collections.Generic;
using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class TradeStatusUser
    {
        public TradeStatusUser()
        {
            Assets = new List<TradeAsset>();
            IsReady = false;
            Currency = new List<TradeAsset>();
        }

        [JsonProperty("assets")] public List<TradeAsset> Assets { get; set; }

        [JsonProperty("currency")] public List<TradeAsset> Currency { get; set; }

        [JsonProperty("ready")] public bool IsReady { get; set; }

        internal bool AddItem(TradeAsset asset)
        {
            if (!Assets.Contains(asset))
            {
                Assets.Add(asset);
                return true;
            }

            return false;
        }

        internal bool AddCurrencyItem(TradeAsset asset)
        {
            if (!Currency.Contains(asset))
            {
                Currency.Add(asset);
                return true;
            }

            return false;
        }

        internal bool RemoveItem(TradeAsset asset)
        {
            return Assets.Contains(asset) && Assets.Remove(asset);
        }

        internal bool RemoveCurrencyItem(TradeAsset asset)
        {
            return Currency.Contains(asset) && Currency.Remove(asset);
        }

        public bool ContainsItem(TradeAsset asset)
        {
            return Assets.Contains(asset);
        }
    }
}