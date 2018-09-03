using Newtonsoft.Json;

namespace autotrade.Steam.Market.Models.Json
{
    public class JCreateBuyOrder : JSuccessInt
    {
        [JsonProperty("buy_orderid")] public long? BuyOrderId { get; set; }

        [JsonProperty("message")] public string Message { get; set; }
    }
}