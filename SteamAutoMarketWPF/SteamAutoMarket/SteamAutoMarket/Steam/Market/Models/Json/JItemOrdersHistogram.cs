namespace SteamAutoMarket.Steam.Market.Models.Json
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class JItemOrdersHistogram : JSuccessInt
    {
        [JsonProperty("buy_order_graph")]
        public List<dynamic[]> BuyOrderGraph { get; set; }

        [JsonProperty("highest_buy_order")]
        public int? HighBuyOrder { get; set; }

        [JsonProperty("lowest_sell_order")]
        public int? MinSellPrice { get; set; }

        [JsonProperty("sell_order_graph")]
        public List<dynamic[]> SellOrderGraph { get; set; }
    }
}