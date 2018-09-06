namespace SteamAutoMarket.Steam.Market.Models
{
    public class MarketProfile
    {
        public MarketProfile()
        {
            WalletInfo = new WalletInfo();
        }

        public long Id { get; set; }
        public string Login { get; set; }
        public string AvatarUrl { get; set; }

        public WalletInfo WalletInfo { get; set; }
    }
}