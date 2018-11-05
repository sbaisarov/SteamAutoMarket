namespace Steam.TradeOffer.Models
{
    using Newtonsoft.Json;

    public class ApiResponse<T>
    {
        [JsonProperty("response")]
        public T Response { get; set; }
    }
}