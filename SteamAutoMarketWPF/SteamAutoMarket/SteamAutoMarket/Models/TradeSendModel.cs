namespace SteamAutoMarket.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Core;

    using global::Steam.TradeOffer.Models.Full;

    using SteamAutoMarket.Annotations;
    using SteamAutoMarket.Repository.Image;

    public class TradeSendModel : INotifyPropertyChanged
    {
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

        public NumericUpDownModel NumericUpDown { get; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}