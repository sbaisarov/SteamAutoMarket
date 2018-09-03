using System;
using System.Collections.Generic;
using System.Linq;
using autotrade.Steam.TradeOffer.Models;
using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer
{
        public class TradeAssetsConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var assetList = ((Dictionary<TradeAsset, TradeAsset>) value).Select(x => x.Value).ToList();
                serializer.Serialize(writer, assetList);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                JsonSerializer serializer)
            {
                var assets = serializer.Deserialize<List<TradeAsset>>(reader);
                return assets.ToDictionary(x => x, x => x);
            }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Dictionary<TradeAsset, TradeAsset>) ||
                       objectType == typeof(List<TradeAsset>);
            }
        }
}