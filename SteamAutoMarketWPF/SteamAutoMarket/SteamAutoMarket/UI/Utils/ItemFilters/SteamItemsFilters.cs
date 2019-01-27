namespace SteamAutoMarket.UI.Utils.ItemFilters
{
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Utils.ItemFilters.Strategies;

    public static class SteamItemsFilters<T>
        where T : SteamItemsModel
    {
        public static readonly ISteamItemsFilter<T> RealGameFilter = new RealGameFilter<T>();

        public static readonly ISteamItemsFilter<T> TypeFilter = new TypeFilter<T>();

        public static readonly ISteamItemsFilter<T> RarityFilter = new RarityFilter<T>();

        public static readonly ISteamItemsFilter<T> MarketableFilter = new MarketableFilter<T>();

        public static readonly ISteamItemsFilter<T> TradabilityFilter = new TradabilityFilter<T>();
    }
}