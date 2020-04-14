namespace SteamAutoMarket.Steam.TradeOffer.Models.Full
{
    using System;

    [Serializable]
    public class FullRgItem
    {
        public RgInventory Asset { get; set; }

        public RgDescription Description { get; set; }

        public FullRgItem CloneAsset()
        {
            return new FullRgItem
            {
                Asset = new RgInventory
                {
                    Amount = this.Asset.Amount,
                    Appid = this.Asset.Appid,
                    Assetid = this.Asset.Assetid,
                    Classid = this.Asset.Classid,
                    Contextid = this.Asset.Contextid,
                    Instanceid = this.Asset.Instanceid
                },
                Description = this.Description
            };
        }
    }
}
