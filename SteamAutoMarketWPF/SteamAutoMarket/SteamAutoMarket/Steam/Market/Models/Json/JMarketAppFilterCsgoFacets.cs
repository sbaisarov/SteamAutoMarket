namespace SteamAutoMarket.Steam.Market.Models.Json
{
    using System;

    using Newtonsoft.Json;

    [Serializable]
    public class JMarketAppFilterCsgoFacets
    {
        [JsonProperty("730_ItemSet")]
        public JMarketAppFilterFacet ItemSet { get; set; }

        [JsonProperty("730_StickerCapsule")]
        public JMarketAppFilterFacet StickerCapsule { get; set; }
    }
}