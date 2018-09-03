using System;
using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class TradeAsset : IEquatable<TradeAsset>
    {
        [JsonProperty("appid")] public long AppId { get; set; }

        [JsonProperty("contextid")] public long ContextId { get; set; }

        [JsonProperty("amount")] public long Amount { get; set; }

        [JsonProperty("assetid")]
        [JsonConverter(typeof(ValueStringConverter))]
        public long AssetId { get; set; }

        [JsonProperty("currencyid")]
        [JsonConverter(typeof(ValueStringConverter))]
        public long CurrencyId { get; set; }

        public bool Equals(TradeAsset other)
        {
            return AppId == other.AppId && ContextId == other.ContextId &&
                   CurrencyId == other.CurrencyId && AssetId == other.AssetId &&
                   Amount == other.Amount;
        }

        public void CreateItemAsset(long appId, long contextId, long assetId, long amount)
        {
            AppId = appId;
            ContextId = contextId;
            AssetId = assetId;
            Amount = amount;
            CurrencyId = 0;
        }

        public void CreateCurrencyAsset(long appId, long contextId, long currencyId, long amount)
        {
            AppId = appId;
            ContextId = contextId;
            CurrencyId = currencyId;
            Amount = amount;
            AssetId = 0;
        }

        public bool ShouldSerializeAssetId()
        {
            return AssetId != 0;
        }

        public bool ShouldSerializeCurrencyId()
        {
            return CurrencyId != 0;
        }

        public class ValueStringConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(value.ToString());
                writer.Flush();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override bool CanConvert(Type objectType)
            {
                return true;
            }
        }
    }
}