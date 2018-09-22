namespace SteamAutoMarket.CustomElements.Controls.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Newtonsoft.Json;

    using SteamAuth;

    using SteamAutoMarket.CustomElements.Utils;
    using SteamAutoMarket.Steam;
    using SteamAutoMarket.Utils;
    using SteamAutoMarket.WorkingProcess;
    using SteamAutoMarket.WorkingProcess.Settings;

    public partial class GeneralSettingsControl : UserControl
    {
        public GeneralSettingsControl()
        {
            try
            {
                this.InitializeComponent();

                var settings = SavedSettings.Get();

                this.LoggingLevelComboBox.SelectedIndex = settings.SettingsLoggerLevel;
                this.AveragePriceDaysNumericUpDown.Value = settings.SettingsAveragePriceParseDays;
                this.Confirm2FANumericUpDown.Value = settings.Settings2FaItemsToConfirm;

                this.LoadSavedAccount();
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private static string GetPasswordStars(int count)
        {
            var result = string.Empty;
            for (var i = 0; i < count; i++)
            {
                result += "*";
            }

            return result;
        }

        private void LoadSavedAccount()
        {
            if (!File.Exists(SavedSteamAccount.AccountsFilePath))
            {
                return;
            }

            var accounts = SavedSteamAccount.Get();
            foreach (var acc in accounts)
            {
                var row = this.AccountsDataGridView.Rows.Add();
                AccountsDataGridUtils.GetDataGridViewLoginCell(this.AccountsDataGridView, row).Value = acc.Login;
                AccountsDataGridUtils.GetDataGridViewPasswordCell(this.AccountsDataGridView, row).Value =
                    GetPasswordStars(acc.Password.Length);
                AccountsDataGridUtils.GetDataGridViewTruePasswordHiddenCell(this.AccountsDataGridView, row).Value =
                    acc.Password;
                AccountsDataGridUtils.GetDataGridViewSteamApiCell(this.AccountsDataGridView, row).Value = acc.SteamApi;
                AccountsDataGridUtils.GetDataGridViewMafileHiddenCell(this.AccountsDataGridView, row).Value =
                    acc.MaFile;

                Task.Run(
                    () =>
                        {
                            var profileImage = ImageUtils.GetSteamProfileSmallImage(acc.MaFile.Session.SteamID);
                            if (profileImage != null)
                            {
                                AccountsDataGridUtils.GetDataGridViewImageCell(this.AccountsDataGridView, row).Value =
                                    profileImage;
                            }
                        });
            }
        }

        private void BrowseMafilePathClick(object sender, EventArgs e)
        {
            try
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = @"Mobile auth file (*.maFile)|*.mafile";
                    openFileDialog.Multiselect = false;
                    openFileDialog.Title = @"MaFile path selecting";

                    if (openFileDialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    this.MafilePathTextBox.Text = openFileDialog.FileName;
                    Logger.Debug($"{this.MafilePathTextBox.Text} mafile selected");
                }
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void AddNewAccountButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.LoginTextBox.Text) || string.IsNullOrEmpty(this.MafilePathTextBox.Text)
                                                                 || string.IsNullOrEmpty(this.PasswordTextBox.Text)
                                                                 || string.IsNullOrEmpty(this.SteamApiTextBox.Text))
                {
                    MessageBox.Show(
                        @"Some fields are filled - incorrectly",
                        @"Error adding account",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Logger.Error("Error adding account. Some fields are filled - incorrectly");
                    return;
                }

                if (AccountsDataGridUtils.IsAccountAlreadyExist(
                    this.AccountsDataGridView,
                    this.LoginTextBox.Text.Trim()))
                {
                    Logger.Error($"{this.LoginTextBox.Text.Trim()} already exist in accounts list");
                    MessageBox.Show(
                        $@"{this.LoginTextBox.Text.Trim()} already exist in accounts list",
                        @"Error on the account add",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                SteamGuardAccount account;
                try
                {
                    account = JsonConvert.DeserializeObject<SteamGuardAccount>(
                        File.ReadAllText(this.MafilePathTextBox.Text));
                }
                catch (Exception ex)
                {
                    Logger.Error("Error processing MaFile", ex);
                    MessageBox.Show(ex.Message, @"Error processing MaFile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (account == null)
                {
                    Logger.Error("Error processing MaFile");
                    return;
                }

                var row = this.AccountsDataGridView.Rows.Add();

                var login = this.LoginTextBox.Text.Trim();
                AccountsDataGridUtils.GetDataGridViewLoginCell(this.AccountsDataGridView, row).Value = login;
                AccountsDataGridUtils.GetDataGridViewPasswordCell(this.AccountsDataGridView, row).Value =
                    GetPasswordStars(this.PasswordTextBox.Text.Trim().Count());
                AccountsDataGridUtils.GetDataGridViewSteamApiCell(this.AccountsDataGridView, row).Value =
                    this.SteamApiTextBox.Text.Trim();
                AccountsDataGridUtils.GetDataGridViewMafileHiddenCell(this.AccountsDataGridView, row).Value = account;
                AccountsDataGridUtils.GetDataGridViewTruePasswordHiddenCell(this.AccountsDataGridView, row).Value =
                    this.PasswordTextBox.Text.Trim();
                Logger.Debug($"{login} added to accounts list");

                Task.Run(
                    () =>
                        {
                            if (account.Session == null)
                            {
                                return;
                            }

                            var profileImage = ImageUtils.GetSteamProfileSmallImage(account.Session.SteamID);
                            if (profileImage != null)
                            {
                                AccountsDataGridUtils.GetDataGridViewImageCell(this.AccountsDataGridView, row).Value =
                                    profileImage;
                            }
                        });

                this.LoginTextBox.Clear();
                this.PasswordTextBox.Clear();
                this.MafilePathTextBox.Clear();
                this.SteamApiTextBox.Clear();

                this.UpdateAccountsFile();
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void UpdateAccountsFile()
        {
            var accounts = new List<SavedSteamAccount>();
            for (var i = 0; i < this.AccountsDataGridView.RowCount; i++)
            {
                var login = (string)AccountsDataGridUtils.GetDataGridViewLoginCell(this.AccountsDataGridView, i).Value;
                var password = (string)AccountsDataGridUtils
                    .GetDataGridViewTruePasswordHiddenCell(this.AccountsDataGridView, i).Value;
                var steamApi = (string)AccountsDataGridUtils.GetDataGridViewSteamApiCell(this.AccountsDataGridView, i)
                    .Value;
                var mafile = (SteamGuardAccount)AccountsDataGridUtils
                    .GetDataGridViewMafileHiddenCell(this.AccountsDataGridView, i).Value;

                accounts.Add(
                    new SavedSteamAccount { Login = login, Password = password, SteamApi = steamApi, MaFile = mafile });
            }

            SavedSteamAccount.UpdateAll(accounts);
        }

        private void DeleteAccountButtonClick(object sender, EventArgs e)
        {
            try
            {
                var currentCell = this.AccountsDataGridView.CurrentCell;
                if (currentCell == null)
                {
                    Logger.Warning("No accounts selected");
                    return;
                }

                var row = currentCell.RowIndex;
                if (row < 0)
                {
                    return;
                }

                var accName = AccountsDataGridUtils.GetDataGridViewLoginCell(this.AccountsDataGridView, row).Value;

                var confirmResult = MessageBox.Show(
                    $@"Are you sure you want to delete the account - {accName}?",
                    @"Confirm deletion?",
                    MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    this.AccountsDataGridView.Rows.RemoveAt(row);
                    Logger.Debug($"Account {accName} was deleted from accounts list");
                }

                this.UpdateAccountsFile();
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void EditAccountButtonClick(object sender, EventArgs e)
        {
            try
            {
                var currentCell = this.AccountsDataGridView.CurrentCell;
                if (currentCell == null)
                {
                    Logger.Warning("No accounts selected");
                    return;
                }

                var row = currentCell.RowIndex;
                if (row < 0)
                {
                    return;
                }

                this.LoginTextBox.Text = @" " + (string)AccountsDataGridUtils
                                             .GetDataGridViewLoginCell(this.AccountsDataGridView, row).Value;
                this.PasswordTextBox.Text = @" " + (string)AccountsDataGridUtils
                                                .GetDataGridViewTruePasswordHiddenCell(this.AccountsDataGridView, row)
                                                .Value;
                this.SteamApiTextBox.Text = @" " + (string)AccountsDataGridUtils
                                                .GetDataGridViewSteamApiCell(this.AccountsDataGridView, row).Value;
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void LogTextBoxTextChanged(object sender, EventArgs e)
        {
            try
            {
                this.LogTextBox.SelectionStart = this.LogTextBox.Text.Length;
                this.LogTextBox.ScrollToCaret();
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void LoginButtonClick(object sender, EventArgs e)
        {
            try
            {
                this.LoginButton.Enabled = false;
                var currentCell = this.AccountsDataGridView.CurrentCell;
                if (currentCell == null)
                {
                    Logger.Warning("No accounts selected");
                    return;
                }

                var row = currentCell.RowIndex;
                if (row < 0)
                {
                    Logger.Warning("No accounts selected");
                    return;
                }

                var login = (string)AccountsDataGridUtils
                    .GetDataGridViewLoginCell(this.AccountsDataGridView, currentCell.RowIndex).Value;
                var password = (string)AccountsDataGridUtils
                    .GetDataGridViewTruePasswordHiddenCell(this.AccountsDataGridView, currentCell.RowIndex).Value;
                var api = (string)AccountsDataGridUtils
                    .GetDataGridViewSteamApiCell(this.AccountsDataGridView, currentCell.RowIndex).Value;
                var mafile = (SteamGuardAccount)AccountsDataGridUtils
                    .GetDataGridViewMafileHiddenCell(this.AccountsDataGridView, currentCell.RowIndex).Value;
                var image = (Image)AccountsDataGridUtils
                    .GetDataGridViewImageCell(this.AccountsDataGridView, currentCell.RowIndex).Value;

                Logger.Info($"Steam authentication for {login} started");

                Task.Run(
                    () =>
                        {
                            try
                            {
                                CurrentSession.SteamManager = new SteamManager(
                                    login,
                                    password,
                                    mafile,
                                    api,
                                    this.SessionRefreshCheckBox.Checked);
                            }
                            catch (Exception ex)
                            {
                                Logger.Error("Login failed!", ex);
                                Dispatcher.AsMainForm(() => this.LoginButton.Enabled = true);
                                return;
                            }

                            Dispatcher.AsMainForm(
                                () =>
                                    {
                                        CurrentSession.AccountImage = image;

                                        Program.MainForm.MarketControlTab.SaleControl.AuthCurrentAccount();
                                        Program.MainForm.MarketControlTab.RelistControl.AuthCurrentAccount();
                                        Program.MainForm.TradeControlTab.TradeControl.AuthCurrentAccount();
                                        Program.MainForm.TradeControlTab.ReceivedTradeManageControl
                                            .AuthCurrentAccount();
                                        Program.MainForm.TradeControlTab.TradeHistoryControl.AuthCurrentAccount();

                                        Logger.Info($"Steam authentication for {login} successful");
                                        this.LoginButton.Enabled = true;
                                    });
                        });
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
                this.LoginButton.Enabled = true;
            }
        }

        private void LoggingLevelComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedLevel = this.LoggingLevelComboBox.Text;

                LoggerLevel? level = null;
                switch (selectedLevel)
                {
                    case "Debug":
                        level = LoggerLevel.Debug;
                        break;
                    case "Info + Errors":
                        level = LoggerLevel.Info;
                        break;
                    case "Errors":
                        level = LoggerLevel.Error;
                        break;
                    case "None":
                        level = LoggerLevel.None;
                        break;
                }

                if (level == null)
                {
                    return;
                }

                Logger.CurrentLoggerLevel = level.Value;

                SavedSettings.UpdateField(
                    ref SavedSettings.Get().SettingsLoggerLevel,
                    this.LoggingLevelComboBox.SelectedIndex);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void SteamApiLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("https://steamcommunity.com/dev/apikey");
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void AveragePriceDaysNumericUpDownValueChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(
                    ref SavedSettings.Get().SettingsAveragePriceParseDays,
                    (int)this.AveragePriceDaysNumericUpDown.Value);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void Confirm2FaNumericUpDownValueChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(
                    ref SavedSettings.Get().Settings2FaItemsToConfirm,
                    (int)this.Confirm2FANumericUpDown.Value);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void ErrorsOnSellNumericUpDownValueChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(
                    ref SavedSettings.Get().ErrorsOnSellToSkip,
                    (int)this.ErrorsOnSellNumericUpDown.Value);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }

        private void PriceLoadThreadsNumericUpDownValueChanged(object sender, EventArgs e)
        {
            try
            {
                SavedSettings.UpdateField(
                    ref SavedSettings.Get().PriceLoadingThreads,
                    (int)this.PriceLoadThreadsNumericUpDown.Value);
            }
            catch (Exception ex)
            {
                Logger.Critical(ex);
            }
        }
    }
}