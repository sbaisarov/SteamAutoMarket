namespace SteamAutoMarket.SteamIntegration
{
    using SteamAutoMarket.Models.Enums;

    public class MarketSellStrategy
    {
        public double ChangeValue { get; set; }

        public EMarketSaleType SaleType { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((MarketSellStrategy)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)this.SaleType * 397) ^ this.ChangeValue.GetHashCode();
            }
        }

        protected bool Equals(MarketSellStrategy other)
        {
            return this.SaleType == other.SaleType && this.ChangeValue.Equals(other.ChangeValue);
        }
    }
}