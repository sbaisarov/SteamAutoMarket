namespace SteamAutoMarket.Pages.Settings
{
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows;

    /// <summary>
    /// Interaction logic for License.xaml
    /// </summary>
    public partial class License : INotifyPropertyChanged
    {
        private string licenseKey;

        private string licenseDaysLeft;

        private string extendKey;

        public License()
        {
            this.DataContext = this;
            this.InitializeComponent();
            this.LicenseKey = File.ReadAllText("license.txt").Trim('\r', '\n', ' ');
            this.LicenseDaysLeft = GetLicenseDaysLeft();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string LicenseKey
        {
            get => this.licenseKey;
            set
            {
                this.licenseKey = value;
                this.OnPropertyChanged();
            }
        }

        public string LicenseDaysLeft
        {
            get => this.licenseDaysLeft;
            set
            {
                this.licenseDaysLeft = value;
                this.OnPropertyChanged();
            }
        }

        public string ExtendKey
        {
            get => this.extendKey;
            set
            {
                this.extendKey = value;
                this.OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private static string GetLicenseDaysLeft()
        {
            // todo
            return "77";
        }

        private void ExtendLicenseButton_OnClick(object sender, RoutedEventArgs e)
        {
            var currentExtendKey = this.ExtendKey;

            // todo
        }
    }
}