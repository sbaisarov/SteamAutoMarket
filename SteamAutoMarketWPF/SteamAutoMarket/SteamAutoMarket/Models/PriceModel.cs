namespace SteamAutoMarket.Models
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using SteamAutoMarket.Annotations;

    public class PriceModel : INotifyPropertyChanged, IComparable
    {
        private double? price;

        public PriceModel(double? price)
        {
            this.price = price;
        }

        public PriceModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public double? Value
        {
            get => this.price;
            set
            {
                this.price = value;
                this.OnPropertyChanged();
            }
        }

        public int CompareTo(object obj) => this.price?.CompareTo(obj) ?? 0;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}