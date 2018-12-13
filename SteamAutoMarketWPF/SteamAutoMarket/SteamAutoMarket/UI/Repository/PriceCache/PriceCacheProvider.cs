namespace SteamAutoMarket.UI.Repository.PriceCache
{
    using System;

    public static class PriceCacheProvider
    {
        public static PriceCache GetAveragePriceCache(
            string currency,
            int daysForAveragePriceScrap,
            int hourToBecomeOld) =>
            new PriceCache(GetAverageCacheFilePath(currency, daysForAveragePriceScrap), hourToBecomeOld);

        public static PriceCache GetCurrentPriceCache(string currency, int hourToBecomeOld) =>
            new PriceCache(GetCurrentCacheFilePath(currency), hourToBecomeOld);

        private static string GetAverageCacheFilePath(string currency, int days) =>
            $"{AppDomain.CurrentDomain.BaseDirectory}average_prices_cache_{currency}_{days}_days.ini";

        private static string GetCurrentCacheFilePath(string currency) =>
            $"{AppDomain.CurrentDomain.BaseDirectory}current_prices_cache_{currency}.ini";
    }
}