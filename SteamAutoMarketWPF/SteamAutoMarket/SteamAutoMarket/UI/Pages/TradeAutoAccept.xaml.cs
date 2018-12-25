namespace SteamAutoMarket.UI.Pages
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using SteamAutoMarket.Properties;

    /// <summary>
    /// Interaction logic for SteamAccountInfo.xaml
    /// </summary>
    public partial class TradeAutoAccept : INotifyPropertyChanged
    {
        public TradeAutoAccept()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}