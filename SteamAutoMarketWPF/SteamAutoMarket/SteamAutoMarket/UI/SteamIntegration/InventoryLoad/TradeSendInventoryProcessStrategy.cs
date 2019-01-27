namespace SteamAutoMarket.UI.SteamIntegration.InventoryLoad
{
    using System.Collections.Generic;
    using System.Linq;

    using SteamAutoMarket.Steam.TradeOffer;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Utils.Extension;

    public class TradeSendInventoryProcessStrategy : IInventoryLoadStrategy<SteamItemsModel>
    {
        public static readonly IInventoryLoadStrategy<SteamItemsModel> TradeSendStrategy = new TradeSendInventoryProcessStrategy();

        private TradeSendInventoryProcessStrategy()
        {
        }

        public void ProcessInventoryPage(ICollection<SteamItemsModel> processedItems, InventoryRootModel inventoryPage, Inventory inventory)
        {
            var items = inventory.ProcessInventoryPage(inventoryPage).ToArray();

            items = inventory.FilterInventory(items, false, true);

            var groupedItems = items.GroupBy(x => new { x.Description.MarketHashName, x.Description.IsTradable })
                .ToArray();

            foreach (var group in groupedItems)
            {
                var existModel = processedItems.FirstOrDefault(
                    item => item.ItemModel.Description.MarketHashName == group.Key.MarketHashName
                            && item.ItemModel.Description.IsTradable == group.Key.IsTradable);

                if (existModel != null)
                {
                    existModel.ItemsList.AddRangeDispatch(group);
                    existModel.RefreshCount();
                }
                else
                {
                    processedItems.AddDispatch(new SteamItemsModel(group.ToArray()));
                }
            }
        }
    }
}