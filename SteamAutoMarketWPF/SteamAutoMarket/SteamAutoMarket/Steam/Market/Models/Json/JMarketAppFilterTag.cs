namespace SteamAutoMarket.Steam.Market.Models.Json
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class JMarketAppFilterTag
    {
        [JsonProperty("localized_name")]
        public string LocalizedName { get; set; }
    }
}