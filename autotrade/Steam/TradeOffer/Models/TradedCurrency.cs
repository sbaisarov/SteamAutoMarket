using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class TradedCurrency
    {
        [JsonProperty("appid")] public int Appid { get; set; }

        [JsonProperty("contextid")] public string ContextId { get; set; }

        [JsonProperty("currencyid")] public string CurrenyId { get; set; }

        [JsonProperty("classid")] public string ClassId { get; set; }

        [JsonProperty("new_currencyid")] public string NewCurrencyId { get; set; }

        [JsonProperty("new_contextid")] public string NewContextId { get; set; }

        [JsonProperty("rollback_new_currencyid")]
        public string RollbackNewCurrencyId { get; set; }

        [JsonProperty("rollback_new_contextid")]
        public string RollbackNewContextId { get; set; }

        [JsonProperty("amount")] public string Amount { get; set; }
    }
}