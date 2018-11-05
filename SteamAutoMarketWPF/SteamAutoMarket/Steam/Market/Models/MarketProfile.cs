namespace Steam.Market.Models
{
    public class MarketProfile
    {
        public MarketProfile()
        {
            this.WalletInfo = new WalletInfo();
        }

        public string AvatarUrl { get; set; }

        public long Id { get; set; }

        public string Login { get; set; }

        public WalletInfo WalletInfo { get; set; }
    }
}