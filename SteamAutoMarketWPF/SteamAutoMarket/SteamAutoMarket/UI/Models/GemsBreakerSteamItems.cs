namespace SteamAutoMarket.UI.Models
{
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;

    public class GemsBreakerSteamItems : SteamItemsModel
    {
        private int gemsCount;

        public GemsBreakerSteamItems(FullRgItem[] itemsList)
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