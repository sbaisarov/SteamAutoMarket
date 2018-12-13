namespace SteamAutoMarket.UI.Models
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using SteamAutoMarket.Properties;

    public class CheckedModel : INotifyPropertyChanged
    {
        private bool checkBoxChecked;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool CheckBoxChecked
        {
            get => this.checkBoxChecked;
            set
            {
                this.checkBoxChecked = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}