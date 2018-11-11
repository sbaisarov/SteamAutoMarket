namespace SteamAutoMarket.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Core;

    using Steam.TradeOffer.Models.Full;

    using SteamAutoMarket.Annotations;
    using SteamAutoMarket.Repository.Image;
    using SteamAutoMarket.SteamUtils;
    using SteamAutoMarket.SteamUtils.Enums;

    public class MarketSellModel : INotifyPropertyChanged
    {
        private double? averagePrice;

        private double? currentPrice;

        private string image;

        public MarketSellModel(List<FullRgItem> itemsList)
        {
            this.ItemsList = new ObservableCollection<FullRgItem>(itemsList);

            this.ItemModel = itemsList.FirstOrDefault();

            this.Count = itemsList.Sum(i => int.Parse(i.Asset.Amount));

            this.ItemName = this.ItemModel?.Description.MarketName;

            this.Type = SteamUtils.GetClearItemType(this.ItemModel?.Description.Type);

            this.Description = SteamUtils.GetClearDescription(this.ItemModel?.Description);

            this.MarketSellNumericUpDown = new NumericUpDownModel(this.Count);

            this.SellPrice = new PriceModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public double? AveragePrice
        {
            get => this.averagePrice;
            set
            {
                this.averagePrice = value;
                this.OnPropertyChanged();
            }
        }

        public int Count { get; }

        public double? CurrentPrice
        {
            get => this.currentPrice;
            set
            {
                this.currentPrice = value;
                this.OnPropertyChanged();
            }
        }

        public string Description { get; }

        public string Image
        {
            get
            {
                if (this.image != null) return this.image;

                this.image = ImageProvider.GetItemImage(
                    a => this.Image = a,
                    this.ItemModel?.Description?.MarketHashName,
                    this.ItemModel?.Description?.IconUrlLarge ?? this.ItemModel?.Description?.IconUrl);

                return this.image;
            }

            set
            {
                this.image = value;
                this.OnPropertyChanged();
            }
        }

        public FullRgItem ItemModel { get; }

        public string ItemName { get; }

        public ObservableCollection<FullRgItem> ItemsList { get; }

        public NumericUpDownModel MarketSellNumericUpDown { get; }

        public PriceModel SellPrice { get; }

        public string Type { get; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

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