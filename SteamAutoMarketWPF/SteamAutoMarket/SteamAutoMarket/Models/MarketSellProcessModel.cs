namespace SteamAutoMarket.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using Steam.TradeOffer.Models.Full;

    using SteamAutoMarket.Models.Enums;
    using SteamAutoMarket.SteamIntegration;

    public class MarketSellProcessModel
    {
        public MarketSellProcessModel(MarketSellModel marketSellModel)
        {
            this.ItemName = marketSellModel.ItemName;
            this.ItemModel = marketSellModel.ItemModel;
            this.CurrentPrice = marketSellModel.CurrentPrice;
            this.AveragePrice = marketSellModel.AveragePrice;
            this.SellPrice = marketSellModel.SellPrice.Value;
            this.ItemsList = marketSellModel.ItemsList.ToList().GetRange(
                0,
                marketSellModel.MarketSellNumericUpDown.AmountToSell);
            this.Count = this.ItemsList.Sum(i => int.Parse(i.Asset.Amount));
        }

        public double? AveragePrice { get; set; }

        public int Count { get; set; }

        public double? CurrentPrice { get; set; }

        public FullRgItem ItemModel { get; set; }

        public string ItemName { get; set; }

        public IEnumerable<FullRgItem> ItemsList { get; set; }

        public double? SellPrice { get; set; }

        public void ProcessSellPrice(MarketSellStrategy strategy)
        {
            switch (strategy.SaleType)
            {
                case EMarketSaleType.Recommended:
                    {
                        if (this.CurrentPrice == null || this.AveragePrice == null)
                        {
                            this.SellPrice = null;
                        }
                        else if (this.CurrentPrice > this.AveragePrice)
                        {
                            this.SellPrice = this.CurrentPrice - 0.01;
                        }
                        else
                        {
                            this.SellPrice = this.AveragePrice - 0.01;
                        }

                        break;
                    }

                case EMarketSaleType.LowerThanCurrent:
                    {
                        if (this.CurrentPrice == null)
                        {
                            this.SellPrice = null;
                        }
                        else
                        {
                            this.SellPrice = this.CurrentPrice + strategy.ChangeValue;
                        }

                        break;
                    }

                case EMarketSaleType.LowerThanAverage:
                    {
                        if (this.AveragePrice == null)
                        {
                            this.SellPrice = null;
                        }
                        else
                        {
                            this.SellPrice = this.AveragePrice + strategy.ChangeValue;
                        }

                        break;
                    }

                default:
                    {
                        this.SellPrice = null;
                        return;
                    }
            }
        }
    }
}