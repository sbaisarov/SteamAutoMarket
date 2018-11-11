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

    public class TradeSendModel : INotifyPropertyChanged
    {
        private string image;

        public TradeSendModel(List<FullRgItem> itemsList)
        {
            this.ItemsList = new ObservableCollection<FullRgItem>(itemsList);

            this.ItemModel = itemsList.FirstOrDefault();

            this.Count = itemsList.Sum(i => int.Parse(i.Asset.Amount));

            this.ItemName = this.ItemModel?.Description.MarketName;

            this.Type = SteamUtils.GetClearItemType(this.ItemModel?.Description.Type);

            this.Description = "TODO - GENERATE DESCRIPTION" + RandomUtils.RandomString(500);

            this.NumericUpDown = new NumericUpDownModel(this.Count);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Count { get; }

        public string Description { get; }

        public string Image
        {
            get =>
                this.image ?? ImageProvider.GetItemImage(
                    a => this.Image = a,
                    this.ItemModel?.Description?.MarketHashName,
                    this.ItemModel?.Description?.IconUrlLarge);

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

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}