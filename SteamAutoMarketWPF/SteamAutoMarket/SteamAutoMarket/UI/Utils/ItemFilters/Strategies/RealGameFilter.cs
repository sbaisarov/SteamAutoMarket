namespace SteamAutoMarket.UI.Utils.ItemFilters.Strategies
{
    using System.Collections.Generic;

    using SteamAutoMarket.UI.Models;

    public class RealGameFilter<T> : ISteamItemsFilter<T>
        where T : SteamItemsModel
    {
        public void Process(string filterValue, List<T> items)
        {
            if (string.IsNullOrEmpty(filterValue)) return;

            items.RemoveAll(g => g.Game != filterValue);
        }
    }
}