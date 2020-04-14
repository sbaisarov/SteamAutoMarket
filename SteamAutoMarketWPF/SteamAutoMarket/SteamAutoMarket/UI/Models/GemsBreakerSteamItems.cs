namespace SteamAutoMarket.UI.Models
{
    using System.Linq;
    using SteamAutoMarket.Steam;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;

    public class GemsBreakerSteamItems : SteamItemsModel
    {
        private int? gemsCount;

        public GemsBreakerSteamItems(FullRgItem[] itemsList)
            : base(itemsList)
        {
            if (GemsBreakHelper.TryGetGemsCount(itemsList.First(), out var savedCount))
            {
                GemsCount = savedCount;
            }
        }

        public int? GemsCount
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