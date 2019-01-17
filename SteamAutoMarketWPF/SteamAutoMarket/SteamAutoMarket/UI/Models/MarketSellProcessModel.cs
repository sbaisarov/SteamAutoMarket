namespace SteamAutoMarket.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.UI.Models.Enums;
    using SteamAutoMarket.UI.SteamIntegration;

    [Serializable]
    public class MarketSellProcessModel
    {
        public MarketSellProcessModel(MarketSellModel marketSellModel)
        {
            this.ItemName = marketSellModel.ItemName;
            this.ItemModel = marketSellModel.ItemModel;
            this.CurrentPrice = marketSellModel.CurrentPrice;
            this.AveragePrice = marketSellModel.AveragePrice;
            this.SellPrice = marketSellModel.SellPrice.Value;
            this.ItemsList = marketSellModel.ItemsList.ToList().GetRange(0, marketSellModel.NumericUpDown.AmountToSell);
            this.Count = this.ItemsList.Sum(i => int.Parse(i.Asset.Amount));
        }

        public MarketSellProcessModel(FullRgItem[] fullRgItems, PriceModel sellPrice)
        {
            this.ItemName = fullRgItems[0].Description.Name;
            this.ItemModel = fullRgItems[0];
            this.ItemsList = fullRgItems;
            this.Count = this.ItemsList.Sum(i => int.Parse(i.Asset.Amount));
            this.SellPrice = sellPrice.Value;
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
                            this.SellPrice = this.AveragePrice;
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