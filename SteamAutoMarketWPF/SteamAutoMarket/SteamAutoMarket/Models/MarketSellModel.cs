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

    public class MarketSellModel : INotifyPropertyChanged
    {
        private double? averagePrice;

        private double? currentPrice;

        public MarketSellModel(List<FullRgItem> itemsList)
        {
            this.ItemsList = new ObservableCollection<FullRgItem>(itemsList);

            this.ItemModel = itemsList.FirstOrDefault();

            this.Count = itemsList.Sum(i => int.Parse(i.Asset.Amount));

            this.ItemName = this.ItemModel?.Description.MarketName;

            this.Type = SteamUtils.GetClearItemType(this.ItemModel?.Description.Type);

            this.Description = "TODO - GENERATE DESCRIPTION" + RandomUtils.RandomString(500);

            this.MarketSellNumericUpDown = new NumericUpDownModel(this.Count);

            this.SellPrice = new PriceModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<FullRgItem> ItemsList { get; }

        public FullRgItem ItemModel { get; }

        public int Count { get; }

        public string ItemName { get; }

        public string Type { get; }

        public string Description { get; }

        public string Image =>
            ImageProvider.GetItemImage(
                this.ItemModel?.Description?.MarketHashName,
                this.ItemModel?.Description?.IconUrlLarge);

        public NumericUpDownModel MarketSellNumericUpDown { get; }

        public double? AveragePrice
        {
            get => this.averagePrice;
            set
            {
                this.averagePrice = value;
                this.ProcessSellPrice();
                this.OnPropertyChanged();
            }
        }

        public double? CurrentPrice
        {
            get => this.currentPrice;
            set
            {
                this.currentPrice = value;
                this.ProcessSellPrice();
                this.OnPropertyChanged();
            }
        }

        public PriceModel SellPrice { get; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void ProcessSellPrice()
        {
            if (this.averagePrice != null || this.currentPrice != null)
            {
                this.SellPrice.Value = 228;
            }
        }
    }
}