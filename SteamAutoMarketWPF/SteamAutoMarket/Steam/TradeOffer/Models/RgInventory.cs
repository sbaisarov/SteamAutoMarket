﻿namespace Steam.TradeOffer.Models
{
    using Newtonsoft.Json;

    public class RgInventory
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("appid")]
        public int Appid { get; set; }

        [JsonProperty("assetid")]
        public string Assetid { get; set; }

        [JsonProperty("classid")]
        public string Classid { get; set; }

        [JsonProperty("contextid")]
        public string Contextid { get; set; }

        [JsonProperty("instanceid")]
        public string Instanceid { get; set; }
    }
}