using System.Collections.Generic;
using Newtonsoft.Json;

namespace Market.Models.Json
{
    public class JPriceHistory : JSuccess
    {
        [JsonProperty("prices")]
        public dynamic Prices { get; set; }
    }
}