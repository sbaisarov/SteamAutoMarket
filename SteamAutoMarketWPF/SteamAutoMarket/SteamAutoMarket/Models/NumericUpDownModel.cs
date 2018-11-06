namespace SteamAutoMarket.Models
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using SteamAutoMarket.Annotations;

    public class NumericUpDownModel : INotifyPropertyChanged
    {
        private int amountToSell;

        public NumericUpDownModel(int maxAllowedCount)
        {
            this.MaxAllowedCount = maxAllowedCount;
            this.amountToSell = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int MaxAllowedCount { get; }

        public int AmountToSell
        {
            get => this.amountToSell;
            set
            {
                this.amountToSell = value;
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