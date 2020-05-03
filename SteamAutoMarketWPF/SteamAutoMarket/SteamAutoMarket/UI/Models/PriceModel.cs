namespace SteamAutoMarket.UI.Models
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.Properties;
    using SteamAutoMarket.UI.Repository.Context;

    [Serializable]
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

        public string StringValue
        {
            get => this.Value?.ToString(CultureInfo.InvariantCulture);
            set
            {
                if (NumberUtils.TryParseDouble(value, out var result))
                {
                    this.Value = result;
                }
                else
                {
                    this.Value = null;
                }
            }
        }

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
                // ReSharper disable once ExplicitCallerInfoArgument
                this.OnPropertyChanged("StringValue");
                UiGlobalVariables.MarketSellPage?.RefreshSelectedItemsInfo();
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