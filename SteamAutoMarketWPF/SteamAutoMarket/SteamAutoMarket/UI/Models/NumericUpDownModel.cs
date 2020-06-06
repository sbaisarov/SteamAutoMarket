namespace SteamAutoMarket.UI.Models
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using SteamAutoMarket.Properties;

    [Serializable]
    public class NumericUpDownModel : INotifyPropertyChanged
    {
        private int amountToSell;

        private int maxAllowedCount;

        public NumericUpDownModel(int maxAllowedCount)
        {
            this.MaxAllowedCount = maxAllowedCount;
            this.amountToSell = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int AmountToSell
        {
            get => this.amountToSell;
            set
            {
                if (value > MaxAllowedCount)
                {
                    return;
                }

                this.amountToSell = value;
                this.OnPropertyChanged();
            }
        }

        public int MaxAllowedCount
        {
            get => this.maxAllowedCount;
            set
            {
                if (this.maxAllowedCount == value) return;
                this.maxAllowedCount = value;
                this.OnPropertyChanged();
            }
        }

        public void SetToMaximum()
        {
            this.AmountToSell = this.MaxAllowedCount;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}