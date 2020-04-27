namespace SteamAutoMarket.UI.Utils.ItemFilters.Strategies
{
    using System.Collections.Generic;

    using SteamAutoMarket.UI.Models;

    public class TradabilityFilter<T> : ISteamItemsFilter<T>
        where T : SteamItemsModel
    {
        public void Process(string filterValue, List<T> items)
        {
            if (string.IsNullOrEmpty(filterValue)) return;

            var value = GetFilterBoolValue(filterValue);
            if (value != null)
            {
                items.RemoveAll(g => g.ItemModel.Description.IsTradable != value);
            }
        }

        private static bool? GetFilterBoolValue(string filterValue)
        {
            switch (filterValue)
            {
                case FilterConstants.Tradable:
                    return true;
                case FilterConstants.NotTradable:
                    return false;
                default:
                    return null;
            }
        }
    }
}