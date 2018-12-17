namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    [Serializable]
    public class TradeStatusUser
    {
        public TradeStatusUser()
        {
            this.Assets = new List<TradeAsset>();
            this.IsReady = false;
            this.Currency = new List<TradeAsset>();
        }

        [JsonProperty("assets")]
        public List<TradeAsset> Assets { get; set; }

        [JsonProperty("currency")]
        public List<TradeAsset> Currency { get; set; }

        [JsonProperty("ready")]
        public bool IsReady { get; set; }

        public bool ContainsItem(TradeAsset asset)
        {
            return this.Assets.Contains(asset);
        }

        internal bool AddCurrencyItem(TradeAsset asset)
        {
            if (!this.Currency.Contains(asset))
            {
                this.Currency.Add(asset);
                return true;
            }

            return false;
        }

        internal bool AddItem(TradeAsset asset)
        {
            if (!this.Assets.Contains(asset))
            {
                this.Assets.Add(asset);
                return true;
            }

            return false;
        }

        internal bool RemoveCurrencyItem(TradeAsset asset)
        {
            return this.Currency.Contains(asset) && this.Currency.Remove(asset);
        }

        internal bool RemoveItem(TradeAsset asset)
        {
            return this.Assets.Contains(asset) && this.Assets.Remove(asset);
        }
    }
}