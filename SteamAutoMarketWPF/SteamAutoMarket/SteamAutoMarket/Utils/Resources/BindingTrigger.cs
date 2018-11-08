namespace SteamAutoMarket.Utils.Resources
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;

    public class BindingTrigger : INotifyPropertyChanged
    {
        public BindingTrigger()
        {
            this.Binding = new Binding { Source = this, Path = new PropertyPath(nameof(this.Value)) };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Binding Binding { get; }

        public object Value { get; set; }

        public void Refresh() => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Value)));
    }
}