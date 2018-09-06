using Newtonsoft.Json;

namespace SteamAutoMarket.Steam.TradeOffer.Models
{
    public class ApiResponse<T>
    {
        [JsonProperty("response")] public T Response { get; set; }
    }
}