namespace SteamAutoMarket.Pages
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Forms;

    using Core;

    using Steam;

    using SteamAutoMarket.Annotations;
    using SteamAutoMarket.Models;
    using SteamAutoMarket.Repository.Context;
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

        private SettingsSteamAccount selectSteamAccount;

        private ObservableCollection<SettingsSteamAccount> steamAccountList =
            new ObservableCollection<SettingsSteamAccount>(SettingsProvider.GetInstance().SteamAccounts);

        #endregion

        public SteamAccountLogin()
        {
            this.InitializeComponent();
            UiGlobalVariables.SteamAccountLogin = this;
            this.DataContext = this;
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
                SettingsProvider.GetInstance().MafilesPath = value;
            }
        }

        public ObservableCollection<SettingsSteamAccount> SteamAccountList
        {
            get => this.steamAccountList;
            set
            {
                this.steamAccountList = value;
                this.OnPropertyChanged();
                SettingsProvider.GetInstance().SteamAccounts = value.ToList();
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

        public bool ForceSessionRefresh { get; set; }

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

            try
            {
                UiGlobalVariables.SteamManager = new SteamManager(
                    this.SelectSteamAccount.Login,
                    this.SelectSteamAccount.Password,
                    this.SelectSteamAccount.Mafile,
                    this.SelectSteamAccount.SteamApi,
                    this.ForceSessionRefresh);

                UiGlobalVariables.MainWindow.Account.DisplayName = this.SelectSteamAccount.Login;
            }
            catch
            {
                ErrorNotify.CriticalMessageBox("Failed to log in. Please check credentials provided");
            }
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
                SettingsProvider.GetInstance().SteamAccounts = this.SteamAccountList.ToList();

                this.SelectSteamAccount = newAccount;

                newAccount.DownloadAvatarAsync();
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error on new account add", ex);
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
    }
}