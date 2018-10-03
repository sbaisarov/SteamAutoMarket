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
                    Amount = Asset.Amount,
                    Appid = Asset.Appid,
                    Assetid = Asset.Assetid,
                    Classid = Asset.Classid,
                    Contextid = Asset.Contextid,
                    Instanceid = Asset.Instanceid
                },
                Description = Description
            };
            return item;
        }
    }
}