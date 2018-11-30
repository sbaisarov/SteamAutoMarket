namespace SteamAutoMarket.UI.Models
{
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.UI.Models.Enums;
    using SteamAutoMarket.UI.SteamIntegration;

    public class MarketSellModel : SteamItemsModel
    {
        private double? averagePrice;

        private double? currentPrice;

        public MarketSellModel(FullRgItem[] itemsList)
            : base(itemsList)
        {
            this.SellPrice = new PriceModel();
        }

        public double? AveragePrice
        {
            get => this.averagePrice;
            set
            {
                this.averagePrice = value;
                this.OnPropertyChanged();
            }
        }

        public double? CurrentPrice
        {
            get => this.currentPrice;
            set
            {
                this.currentPrice = value;
                this.OnPropertyChanged();
            }
        }

        public PriceModel SellPrice { get; }

        public void CleanItemPrices()
        {
            this.CurrentPrice = null;
            this.AveragePrice = null;
            this.SellPrice.Value = null;
        }

        public void ProcessSellPrice(MarketSellStrategy strategy)
        {
            switch (strategy.SaleType)
            {
                case EMarketSaleType.Recommended:
                    {
                        if (this.CurrentPrice == null || this.AveragePrice == null)
                        {
                            this.SellPrice.Value = null;
                        }
                        else if (this.CurrentPrice > this.AveragePrice)
                        {
                            this.SellPrice.Value = this.CurrentPrice - 0.01;
                        }
                        else
                        {
                            this.SellPrice.Value = this.AveragePrice - 0.01;
                        }

                        break;
                    }

                case EMarketSaleType.LowerThanCurrent:
                    {
                        if (this.CurrentPrice == null)
                        {
                            this.SellPrice.Value = null;
                        }
                        else
                        {
                            this.SellPrice.Value = this.CurrentPrice + strategy.ChangeValue;
                        }

                        break;
                    }

                case EMarketSaleType.LowerThanAverage:
                    {
                        if (this.AveragePrice == null)
                        {
                            this.SellPrice.Value = null;
                        }
                        else
                        {
                            this.SellPrice.Value = this.AveragePrice + strategy.ChangeValue;
                        }

                        break;
                    }

                default:
                    {
                        this.SellPrice.Value = null;
                        return;
                    }
            }
        }
    }
}