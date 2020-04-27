namespace SteamAutoMarket.UI.SteamIntegration.InventoryLoad
{
    using System.Collections.Generic;

    using SteamAutoMarket.Steam.TradeOffer;
    using SteamAutoMarket.Steam.TradeOffer.Models;
    using SteamAutoMarket.UI.Models;

    public interface IInventoryLoadStrategy<T> where T : SteamItemsModel
    {
        void ProcessInventoryPage(
            ICollection<T> processedItems,
            InventoryRootModel inventoryPage,
            Inventory inventory);
    }
}