namespace SteamAutoMarket.Pages
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Forms;

    using SteamAutoMarket.Annotations;
    using SteamAutoMarket.Models;
    using SteamAutoMarket.Repository.Context;
    using SteamAutoMarket.Repository.Image;
    using SteamAutoMarket.Repository.Settings;
    using SteamAutoMarket.Utils.Logger;

    using MessageBox = System.Windows.MessageBox;

    /// <summary>
    /// Interaction logic for SteamAccountLogin.xaml
    /// </summary>
    public partial class SteamAccountLogin : INotifyPropertyChanged
    {
        #region Private variables

        private string newAccountLogin;

        private string newAccountPassword;

        private string mafilesPath = SettingsProvider.GetInstance().MafilesPath;

        private SettingsSteamAccount currentLoggedAccount;

        private ObservableCollection<SettingsSteamAccount> steamAccountList =
            new ObservableCollection<SettingsSteamAccount>();

        private SettingsSteamAccount selectSteamAccount;

        #endregion

        public SteamAccountLogin()
        {
            this.InitializeComponent();
            UiGlobalVariables.SteamAccountLogin = this;
            this.DataContext = this;

            this.SteamAccountList.Add(
                new SettingsSteamAccount
                    {
                        Login = "login1",
                        Password = "password1",
                        SteamApi = "ap1i",
                        SteamId = 123,
                        TradeToken = "tok1en"
                    });

            this.SteamAccountList.Add(
                new SettingsSteamAccount
                    {
                        Login = "log2in",
                        Password = "pas2sword",
                        SteamApi = "ap2i",
                        SteamId = 345,
                        TradeToken = "to2ken"
                    });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string NewAccountLogin
        {
            get => this.newAccountLogin;
            set
            {
                this.newAccountLogin = value;
                this.OnPropertyChanged();
            }
        }

        public string NewAccountPassword
        {
            get => this.newAccountPassword;
            set
            {
                this.newAccountPassword = value;
                this.OnPropertyChanged();
            }
        }

        public string MafilesPath
        {
            get => this.mafilesPath;
            set
            {
                this.mafilesPath = value;
                this.OnPropertyChanged();
            }
        }

        public SettingsSteamAccount CurrentLoggedAccount
        {
            get => this.currentLoggedAccount;
            set
            {
                this.currentLoggedAccount = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<SettingsSteamAccount> SteamAccountList
        {
            get => this.steamAccountList;
            set
            {
                this.steamAccountList = value;
                this.OnPropertyChanged();
            }
        }

        public SettingsSteamAccount SelectSteamAccount
        {
            get => this.selectSteamAccount;
            set
            {
                this.selectSteamAccount = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void BrowseFolder(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select mafiles folder";
                dlg.SelectedPath = this.MafilesPath;
                dlg.ShowNewFolderButton = true;
                var result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.MafilesPath = dlg.SelectedPath;
                }
            }
        }

        private void LoginSteamAccountButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.selectSteamAccount == null)
            {
                ErrorNotify.CriticalMessageBox("No account selected!");
                return;
            }

            this.CurrentLoggedAccount = this.SelectSteamAccount;
            UiGlobalVariables.MainWindow.Account.DisplayName = this.CurrentLoggedAccount.Login;
        }

        private void AddNewAccountButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.SteamAccountList.FirstOrDefault(
                        a => a.Login.Equals(this.NewAccountLogin, StringComparison.InvariantCultureIgnoreCase)) != null)
                {
                    ErrorNotify.CriticalMessageBox($"Account {this.NewAccountLogin} is already in accounts list.");
                    return;
                }

                var newAccount = new SettingsSteamAccount(
                    this.NewAccountLogin,
                    this.NewAccountPassword,
                    $"{this.MafilesPath}\\{this.NewAccountLogin?.ToLower()}.maFile");

                this.SteamAccountList.Add(newAccount);

                this.SelectSteamAccount = newAccount;

                this.RefreshSteamAccountFields(newAccount);
            }
            catch (Exception ex)
            {
                ErrorNotify.CriticalMessageBox(ex);
            }
        }

        private void RemoveSelectedAccountButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.selectSteamAccount == null)
            {
                ErrorNotify.CriticalMessageBox("No account selected!");
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to remove '{this.SelectSteamAccount.Login}' account?",
                "Clarification",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this.SteamAccountList.Remove(this.SelectSteamAccount);
            }
        }

        private void RefreshSteamAccountFields(SettingsSteamAccount account)
        {
            Task.Run(
                () =>
                    {
                        account.Avatar =
                            ImageProvider.GetSmallSteamProfileImage(this.SelectSteamAccount.SteamId.ToString());
                    });
        }
    }
}