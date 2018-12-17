namespace SteamAutoMarket.Steam.TradeOffer.Models.Full
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public class FullTradeItem
    {
        public CEconAsset Asset { get; set; }

        public AssetDescription Description { get; set; }

        public static AssetDescription GetDescription(CEconAsset asset, List<AssetDescription> descriptions)
        {
            var description =
                descriptions.FirstOrDefault(
                    item => asset.InstanceId == item.InstanceId && asset.ClassId == item.ClassId)
                ?? new AssetDescription
                       {
                           MarketHashName = "[Info is missing]",
                           AppId = int.Parse(asset.AppId),
                           Name = "[Info is missing]",
                           Type = "[Info is missing]"
                       };

            return description;
        }

        public static List<FullTradeItem> GetFullItemsList(List<CEconAsset> assets, List<AssetDescription> descriptions)
        {
            var itemsList = new List<FullTradeItem>();
            if (assets == null || descriptions == null) return itemsList;

            foreach (var item in assets)
                itemsList.Add(new FullTradeItem { Asset = item, Description = GetDescription(item, descriptions) });
            return itemsList;
        }
    }
}