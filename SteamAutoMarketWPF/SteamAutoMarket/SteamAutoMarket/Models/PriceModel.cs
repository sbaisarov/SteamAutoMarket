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
                if (value != null)
                {
                    this.price = Math.Round(value.Value, 2);
                    if (this.price < 0) this.price = 0;
                }
                else
                {
                    this.price = null;
                }

                this.OnPropertyChanged();
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (obj is PriceModel otherObj)
            {
                if (!this.Value.HasValue && !otherObj.Value.HasValue)
                    return 0;

                if (this.Value.HasValue && !otherObj.Value.HasValue)
                    return 1;

                if (!this.Value.HasValue)
                    return -1;

                return this.Value.Value.CompareTo(otherObj.Value);
            }

            return -1;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}