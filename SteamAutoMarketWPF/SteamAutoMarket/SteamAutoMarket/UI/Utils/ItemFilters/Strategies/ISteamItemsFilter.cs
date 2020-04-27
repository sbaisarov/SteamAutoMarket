namespace SteamAutoMarket.UI.Utils.ItemFilters.Strategies
{
    using System.Collections.Generic;

    using SteamAutoMarket.UI.Models;

    public interface ISteamItemsFilter<T>
        where T : SteamItemsModel
    {
        void Process(string filterValue, List<T> items);
    }
}