namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class TradeAsset : IEquatable<TradeAsset>
    {
        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("appid")]
        public long AppId { get; set; }

        [JsonProperty("assetid")]
        [JsonConverter(typeof(ValueStringConverter))]
        public string AssetId { get; set; }

        [JsonProperty("contextid")]
        public string ContextId { get; set; }

        [JsonProperty("currencyid")]
        [JsonConverter(typeof(ValueStringConverter))]
        public long CurrencyId { get; set; }

        public void CreateCurrencyAsset(long appId, string contextId, long currencyId, long amount)
        {
            this.AppId = appId;
            this.ContextId = contextId;
            this.CurrencyId = currencyId;
            this.Amount = amount;
            this.AssetId = "0";
        }

        public void CreateItemAsset(long appId, string contextId, string assetId, long amount)
        {
            this.AppId = appId;
            this.ContextId = contextId;
            this.AssetId = assetId;
            this.Amount = amount;
            this.CurrencyId = 0;
        }

        public bool Equals(TradeAsset other)
        {

            return other != null && (this.AppId == other.AppId && this.ContextId == other.ContextId && this.CurrencyId == other.CurrencyId
                                     && this.AssetId == other.AssetId && this.Amount == other.Amount);
        }

        public bool ShouldSerializeAssetId()
        {
            return this.AssetId != "0";
        }

        public bool ShouldSerializeCurrencyId()
        {
            return this.CurrencyId != 0;
        }

        public class ValueStringConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(
                JsonReader reader,
                Type objectType,
                object existingValue,
                JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(value.ToString());
                writer.Flush();
            }
        }
    }
}