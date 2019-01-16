namespace SteamAutoMarket.UI.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using SteamAutoMarket.Properties;
    using SteamAutoMarket.Steam;
    using SteamAutoMarket.Steam.TradeOffer.Models.Full;
    using SteamAutoMarket.UI.Repository.Image;

    [Serializable]
    public class SteamItemsModel : INotifyPropertyChanged
    {
        private double? averagePrice;

        private int count;

        private double? currentPrice;

        private string image;

        public SteamItemsModel(FullRgItem[] itemsList)
        {
            this.ItemsList = new ObservableCollection<FullRgItem>(itemsList);

            this.ItemModel = itemsList.FirstOrDefault();

            this.Count = itemsList.Sum(i => int.Parse(i.Asset.Amount));

            this.ItemName = this.ItemModel?.Description.MarketName;

            this.Game = this.ItemModel?.Description?.Tags?.FirstOrDefault(tag => tag.Category == "Game")
                ?.LocalizedTagName;

            this.Type = SteamUtils.GetClearItemType(this.ItemModel?.Description?.Type);

            this.Description = SteamUtils.GetClearDescription(this.ItemModel?.Description);

            this.NumericUpDown = new NumericUpDownModel(this.Count);
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

        public int Count
        {
            get => this.count;
            private set
            {
                if (this.count == value) return;
                this.count = value;
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

        public string Description { get; }

        public string Game { get; }

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

        public NumericUpDownModel NumericUpDown { get; }

        public string Type { get; }

        public virtual void CleanItemPrices()
        {
            this.CurrentPrice = null;
            this.AveragePrice = null;
        }

        public void RefreshCount()
        {
            this.Count = this.ItemsList.Sum(i => int.Parse(i.Asset.Amount));
            this.NumericUpDown.MaxAllowedCount = this.Count;
            if (this.NumericUpDown.AmountToSell > this.Count)
            {
                this.NumericUpDown.AmountToSell = this.Count;
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}