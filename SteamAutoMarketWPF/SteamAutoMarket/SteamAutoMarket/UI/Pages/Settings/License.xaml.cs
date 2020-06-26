namespace SteamAutoMarket.UI.Pages.Settings
{
    using System.ComponentModel;
    using System.IO;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows;

    using Newtonsoft.Json.Linq;

    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.Utils.Logger;

    /// <summary>
    /// Interaction logic for License.xaml
    /// </summary>
    public partial class License : INotifyPropertyChanged
    {
        private string extendKey;

        private string licenseDaysLeft;

        private string licenseKey;

        public License()
        {
            this.DataContext = this;
            this.InitializeComponent();
            UiGlobalVariables.License = this;

            this.LicenseKey = File.ReadAllText("license.txt").Trim('\r', '\n', ' ');
            this.LicenseDaysLeft = this.GetLicenseDaysLeft();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string ExtendKey
        {
            get => this.extendKey;
            set
            {
                this.extendKey = value;
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

        public string LicenseKey
        {
            get => this.licenseKey;
            set
            {
                this.licenseKey = value;
                this.OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void ExtendLicenseButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentExtendKey = this.ExtendKey;
                using (var wb = new WebClient())
                {
                    wb.QueryString.Add("code", currentExtendKey);
                    wb.QueryString.Add("key", this.LicenseKey);
                    var response = wb.UploadValues("https://shamanovski.pythonanywhere.com/api/valcode", "POST", wb.QueryString);
                    var responseString = Encoding.UTF8.GetString(response);
                    responseString = "OK";
                    if (responseString.Contains("OK"))
                    {
                        var errorHappens = false;
                        try
                        {
                            this.LicenseDaysLeft = this.GetLicenseDaysLeft();
                        }
                        catch
                        {
                            errorHappens = true;
                            ErrorNotify.CriticalMessageBox(
                                "License was successfully extended. But some error on getting current license status happens. Try to restart application in few minutes");
                        }

                        if (!errorHappens) ErrorNotify.InfoMessageBox("License was successfully extended");
                    }
                }
            }
            catch
            {
                ErrorNotify.CriticalMessageBox("Failed to extend license");
            }
        }

        private string GetLicenseDaysLeft()
        {
            using (var wb = new WebClient())
            {
                var response = wb.UploadString("https://shamanovski.pythonanywhere.com/api/getlicensestatus", this.LicenseKey);
                var responseDeserialized = JObject.Parse(response);
                return responseDeserialized[this.LicenseKey]["subscription_time"].ToString();
            }
        }                          
    }
}