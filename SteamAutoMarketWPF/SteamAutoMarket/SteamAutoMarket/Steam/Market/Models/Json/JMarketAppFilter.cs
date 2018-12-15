namespace SteamAutoMarket.Steam.Market.Models.Json
{
    using System;

    [Serializable]
    public class JMarketAppFilter<T> : JSuccess
    {
        public T Facets { get; set; }
    }
}