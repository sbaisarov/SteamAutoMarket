using Newtonsoft.Json;

namespace autotrade.Steam.TradeOffer.Models
{
    public class ApiResponse<T>
    {
        [JsonProperty("response")] public T Response { get; set; }
    }
}