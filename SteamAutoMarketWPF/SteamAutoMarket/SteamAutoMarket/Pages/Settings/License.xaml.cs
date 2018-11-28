using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Windows.Media.Animation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Steam;

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

        private string GetLicenseDaysLeft()
        {
            using (var wb = new WebClient())
            {
                var response = wb.UploadString("https://www.steambiz.store/api/getlicensestatus", this.LicenseKey);
                var responseDeserialized = JObject.Parse(response);
                return responseDeserialized[this.LicenseKey]["subscription_time"].ToString();
            }
        }

        private void ExtendLicenseButton_OnClick(object sender, RoutedEventArgs e)
        {
            var currentExtendKey = this.ExtendKey;
            using (var wb = new WebClient())
            {
                wb.QueryString.Add("code", currentExtendKey);
                wb.QueryString.Add("key", this.LicenseKey);
                var response = wb.UploadValues("https://www.steambiz.store/api/valcode", "POST", wb.QueryString);
                var responseString = Encoding.UTF8.GetString(response);
                if (!responseString.Contains("OK")) throw new Exception("Failed to extend license");
            }
        }
    }
}