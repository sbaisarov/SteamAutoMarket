namespace SteamAutoMarket.Steam.TradeOffer.Models.Full
{
    public class FullRgItem
    {
        public RgInventory Asset { get; set; }

        public RgDescription Description { get; set; }

        public FullRgItem CloneAsset()
        {
            var item = new FullRgItem
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
            return item;
        }
    }
}