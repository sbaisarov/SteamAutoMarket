namespace SteamAutoMarket.Models
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Core;

    using Steam.TradeOffer.Models.Full;

    using SteamAutoMarket.Annotations;
    using SteamAutoMarket.Repository.Image;

    public class SteamItemsModel : INotifyPropertyChanged
    {
        private int count;

        private string image;

        public SteamItemsModel(FullRgItem[] itemsList)
        {
            this.ItemsList = new ObservableCollection<FullRgItem>(itemsList);

            this.ItemModel = itemsList.FirstOrDefault();

            this.Count = itemsList.Sum(i => int.Parse(i.Asset.Amount));

            this.ItemName = this.ItemModel?.Description.MarketName;

            this.Type = SteamUtils.GetClearItemType(this.ItemModel?.Description.Type);

            this.Description = SteamUtils.GetClearDescription(this.ItemModel?.Description);

            this.NumericUpDown = new NumericUpDownModel(this.Count);
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        public NumericUpDownModel NumericUpDown { get; }

        public string Type { get; }

        public void RefreshCount()
        {
            this.Count = this.ItemsList.Sum(i => int.Parse(i.Asset.Amount));
            this.NumericUpDown.MaxAllowedCount = this.Count;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}