namespace SteamAutoMarket.Steam.Market.Models.Json
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class JCreateBuyOrder : JSuccessInt
    {
        [JsonProperty("buy_orderid")]
        public long? BuyOrderId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}