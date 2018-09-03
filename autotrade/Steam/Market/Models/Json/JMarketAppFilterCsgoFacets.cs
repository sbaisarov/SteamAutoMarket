using Newtonsoft.Json;

namespace autotrade.Steam.Market.Models.Json
{
    public class JMarketAppFilterCsgoFacets
    {
        [JsonProperty("730_ItemSet")] public JMarketAppFilterFacet ItemSet { get; set; }

        [JsonProperty("730_StickerCapsule")] public JMarketAppFilterFacet StickerCapsule { get; set; }
    }
}