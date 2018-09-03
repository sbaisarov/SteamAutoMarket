using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class ItemAttribute
    {
        [JsonProperty("defindex")] public ushort DefIndex { get; set; }

        [JsonProperty("value")] public string Value { get; set; }

        [JsonProperty("float_value")] public float FloatValue { get; set; }

        [JsonProperty("account_info")] public AccountInfo AccountInfo { get; set; }
    }
}