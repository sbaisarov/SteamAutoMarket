namespace SteamAutoMarket.UI.SteamIntegration.InventoryLoad
{
    using System.Collections.Generic;
    using System.Linq;

    using SteamAutoMarket.Steam.TradeOffer;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Utils.Extension;

    public class GemsBreakerInventoryProcessStrategy : IInventoryLoadStrategy<GemsBreakerSteamItems>
    {
        public static readonly IInventoryLoadStrategy<GemsBreakerSteamItems> GemsBreakerStrategy =
            new GemsBreakerInventoryProcessStrategy();

        private GemsBreakerInventoryProcessStrategy()
        {
        }

        public void ProcessInventoryPage(
            ICollection<GemsBreakerSteamItems> processedItems,
            InventoryRootModel inventoryPage,
            Inventory inventory)
        {
            var items = inventory.ProcessInventoryPage(inventoryPage).ToArray();

            var groupedItems = items.GroupBy(
                x => new
                         {
                             x.Description.MarketHashName,
                             x.Description.IsTradable,
                             x.Description.IsMarketable,
                             x.Asset.Amount
                         }).ToArray();

            foreach (var group in groupedItems)
            {
                var existModel = processedItems.FirstOrDefault(
                    item => item.ItemModel.Description.MarketHashName == group.Key.MarketHashName
                            && item.ItemModel.Description.IsTradable == group.Key.IsTradable
                            && item.ItemModel.Asset.Amount == group.Key.Amount);

                if (existModel != null)
                {
                    existModel.ItemsList.AddRangeDispatch(group);
                    existModel.RefreshCount();
                }
                else
                {
                    processedItems.AddDispatch(new GemsBreakerSteamItems(group.ToArray()));
                }
            }
        }
    }
}