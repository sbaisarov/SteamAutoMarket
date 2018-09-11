namespace SteamAutoMarket.Steam.Market.Models.Json
{
    public class JMarketAppFilter<T> : JSuccess
    {
        public T Facets { get; set; }
    }
}