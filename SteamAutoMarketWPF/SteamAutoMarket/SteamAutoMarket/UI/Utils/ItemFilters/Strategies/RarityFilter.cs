namespace SteamAutoMarket.UI.Utils.ItemFilters.Strategies
{
    using System.Collections.Generic;
    using System.Linq;

    using SteamAutoMarket.UI.Models;

    public class RarityFilter<T> : ISteamItemsFilter<T>
        where T : SteamItemsModel
    {
        public void Process(string filterValue, List<T> items)
        {
            if (string.IsNullOrEmpty(filterValue)) return;

            items.RemoveAll(
                model => model.ItemModel?.Description?.Tags
                             ?.FirstOrDefault(tag => tag.LocalizedCategoryName == "Rarity")?.LocalizedTagName
                         != filterValue);
        }
    }
}