namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class ApiResponse<T>
    {
        [JsonProperty("response")]
        public T Response { get; set; }
    }
}