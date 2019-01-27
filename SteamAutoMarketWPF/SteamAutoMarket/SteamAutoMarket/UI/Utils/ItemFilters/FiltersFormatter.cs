namespace SteamAutoMarket.UI.Utils.ItemFilters
{
    using System.Collections.Generic;
    using System.Linq;

    using SteamAutoMarket.UI.Models;

    public static class FiltersFormatter<T>
        where T : SteamItemsModel
    {
        public static IEnumerable<string> GetRealGameFilters(ICollection<T> items) =>
            AddEmptyValue(items.Select(model => model.Game).ToHashSet().OrderBy(q => q));

        public static IEnumerable<string> GetTypeFilters(ICollection<T> items) =>
            AddEmptyValue(items.Select(model => model.Type).ToHashSet().OrderBy(q => q));

        public static IEnumerable<string> GetRarityFilters(ICollection<T> items) =>
            AddEmptyValue(
                items.Select(
                        model => model.ItemModel?.Description?.Tags
                            ?.FirstOrDefault(tag => tag.LocalizedCategoryName == FilterConstants.Rarity)
                            ?.LocalizedTagName)
                    .ToHashSet().OrderBy(q => q));

        public static IEnumerable<string> GetTradabilityFilters(ICollection<T> items) =>
            AddEmptyValue(
                items.Select(
                    model => (model.ItemModel?.Description.IsTradable == true)
                                 ? FilterConstants.Tradable
                                 : FilterConstants.NotTradable).ToHashSet());

        public static IEnumerable<string> GetMarketableFilters(ICollection<T> items) =>
            AddEmptyValue(
                items.Select(
                    model => (model.ItemModel?.Description.IsMarketable == true)
                                 ? FilterConstants.Marketable
                                 : FilterConstants.NotMarketable).ToHashSet());

        private static IEnumerable<string> AddEmptyValue(IEnumerable<string> values) =>
            UiConstants.EmptyValueEnumerable.Concat(values);
    }
}