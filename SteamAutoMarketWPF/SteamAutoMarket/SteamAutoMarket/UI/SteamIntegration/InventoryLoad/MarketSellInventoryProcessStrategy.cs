namespace SteamAutoMarket.UI.SteamIntegration.InventoryLoad
{
    using System.Collections.Generic;
    using System.Linq;

    using SteamAutoMarket.Steam.TradeOffer;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Utils.Extension;

    public class MarketSellInventoryProcessStrategy : IInventoryLoadStrategy<MarketSellModel>
    {
        public static readonly IInventoryLoadStrategy<MarketSellModel> MarketSellStrategy =
            new MarketSellInventoryProcessStrategy();

        private MarketSellInventoryProcessStrategy()
        {
        }

        public void ProcessInventoryPage(
            ICollection<MarketSellModel> processedItems,
            InventoryRootModel inventoryPage,
            Inventory inventory)
        {
            var items = inventory.ProcessInventoryPage(inventoryPage).ToArray();

            items = inventory.FilterInventory(items, true, false);

            var groupedItems = items.GroupBy(i => new { i.Description.MarketHashName, i.Asset.Amount }).ToArray();

            foreach (var group in groupedItems)
            {
                var existModel = processedItems.FirstOrDefault(
                    item => item.ItemModel.Description.MarketHashName == group.Key.MarketHashName
                            && item.ItemModel.Asset.Amount == group.Key.Amount);

                if (existModel != null)
                {
                    foreach (var groupItem in group.ToArray())
                    {
                        existModel.ItemsList.Add(groupItem);
                    }

                    existModel.RefreshCount();
                }
                else
                {
                    processedItems.AddDispatch(new MarketSellModel(group.ToArray()));
                }
            }
        }
    }
}