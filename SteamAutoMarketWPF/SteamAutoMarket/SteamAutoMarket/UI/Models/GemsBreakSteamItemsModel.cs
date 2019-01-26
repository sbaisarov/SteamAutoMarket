namespace SteamAutoMarket.UI.Models
{
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;

    public class GemsBreakSteamItemsModel : SteamItemsModel
    {
        private int gemsCount;

        public GemsBreakSteamItemsModel(FullRgItem[] itemsList)
            : base(itemsList)
        {
        }

        public int GemsCount
        {
            get => this.gemsCount;
            set
            {
                this.gemsCount = value;
                this.OnPropertyChanged();
            }
        }
    }
}