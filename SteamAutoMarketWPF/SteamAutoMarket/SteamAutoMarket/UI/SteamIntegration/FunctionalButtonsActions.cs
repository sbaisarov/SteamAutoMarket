namespace SteamAutoMarket.UI.SteamIntegration
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using SteamAutoMarket.UI.Models;
    using SteamAutoMarket.UI.Utils.Logger;

    public static class FunctionalButtonsActions
    {
        public static void AddPlusOneAmountToAllItems(IEnumerable<SteamItemsModel> items)
        {
            foreach (var t in items)
            {
                t.NumericUpDown.AmountToSell++;
            }
        }

        public static void OpenOnSteamMarket(SteamItemsModel item)
        {
            try
            {
                if (item == null)
                {
                    return;
                }

                Process.Start(
                    "https://" + $"steamcommunity.com/market/listings/{item.ItemModel.Asset.Appid}/"
                               + item.ItemModel.Description.MarketHashName);
            }
            catch (Exception ex)
            {
                ErrorNotify.CriticalMessageBox(ex);
            }
        }
    }
}