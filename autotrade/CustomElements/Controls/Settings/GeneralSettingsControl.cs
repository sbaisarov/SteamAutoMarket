using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using autotrade.CustomElements.Utils;
using autotrade.Steam;
using autotrade.Utils;
using autotrade.WorkingProcess;
using autotrade.WorkingProcess.Settings;
using Newtonsoft.Json;
using SteamAuth;

namespace autotrade.CustomElements.Controls.Settings
{
    public partial class GeneralSettingsControl : UserControl
    {
        public GeneralSettingsControl()
        {
            InitializeComponent();

            var settings = SavedSettings.Get();

            LoggingLevelComboBox.SelectedIndex = settings.SETTINGS_LOGGER_LEVEL;
            AveragePriceDaysNumericUpDown.Value = settings.SETTINGS_AVERAGE_PRICE_PARSE_DAYS;
            Confirm2FANumericUpDown.Value = settings.SETTINGS_2FA_ITEMS_TO_CONFIRM;

            if (File.Exists(SavedSteamAccount.ACCOUNTS_FILE_PATH))
            {
                var accounts = SavedSteamAccount.Get();
                foreach (var acc in accounts)
                {
                    var row = AccountsDataGridView.Rows.Add();
                    AccountsDataGridUtils.GetDataGridViewLoginCell(AccountsDataGridView, row).Value = acc.Login;
                    AccountsDataGridUtils.GetDataGridViewPasswordCell(AccountsDataGridView, row).Value =
                        GetPasswordStars(acc.Password.Count());
                    AccountsDataGridUtils.GetDataGridViewTruePasswordHidenCell(AccountsDataGridView, row).Value =
                        acc.Password;
                    AccountsDataGridUtils.GetDataGridViewSteamApiCell(AccountsDataGridView, row).Value = acc.OpskinsApi;
                    AccountsDataGridUtils.GetDataGridViewMafileHidenCell(AccountsDataGridView, row).Value = acc.Mafile;

                    Task.Run(() =>
                    {
                        var profileImage = ImageUtils.GetSteamProfileSmallImage(acc.Mafile.Session.SteamID);
                        if (profileImage != null)
                            AccountsDataGridUtils.GetDataGridViewImageCell(AccountsDataGridView, row).Value =
                                profileImage;
                    });
                }
            }

            ;
        }

        private void BrowseMafilePath_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Mobile auth file (*.maFile)|*.mafile";
                openFileDialog.Multiselect = false;
                openFileDialog.Title = "MaFile path selecting";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MafilePathTextBox.Text = openFileDialog.FileName;
                    Logger.Debug($"{MafilePathTextBox.Text} mafile selected");
                }
            }
        }

        private string GetPasswordStars(int Count)
        {
            var result = "";
            for (var i = 0; i < Count; i++) result += "*";
            return result;
        }

        private void AddNewAccountButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text) || string.IsNullOrEmpty(MafilePathTextBox.Text) ||
                string.IsNullOrEmpty(PasswordTextBox.Text) || string.IsNullOrEmpty(SteamApiTextBox.Text))
            {
                MessageBox.Show("Some fields are filled - incorrectly", "Error adding account", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Logger.Error("Error adding account. Some fields are filled - incorrectly");
                return;
            }

            if (AccountsDataGridUtils.IsAccountAreadyExist(AccountsDataGridView, LoginTextBox.Text.Trim()))
            {
                Logger.Error($"{LoginTextBox.Text.Trim()} already exist in accounts list");
                MessageBox.Show($"{LoginTextBox.Text.Trim()} already exist in accounts list",
                    "Error on the account add", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SteamGuardAccount account = null;
            try
            {
                account = JsonConvert.DeserializeObject<SteamGuardAccount>(File.ReadAllText(MafilePathTextBox.Text));
            }
            catch (Exception ex)
            {
                Logger.Error("Error processing MaFile", ex);
                MessageBox.Show(ex.Message, "Error processing MaFile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (account == null)
            {
                Logger.Error("Error processing MaFile");
                return;
            }

            var row = AccountsDataGridView.Rows.Add();

            var login = LoginTextBox.Text.Trim();
            AccountsDataGridUtils.GetDataGridViewLoginCell(AccountsDataGridView, row).Value = login;
            AccountsDataGridUtils.GetDataGridViewPasswordCell(AccountsDataGridView, row).Value =
                GetPasswordStars(PasswordTextBox.Text.Trim().Count());
            AccountsDataGridUtils.GetDataGridViewSteamApiCell(AccountsDataGridView, row).Value =
                SteamApiTextBox.Text.Trim();
            AccountsDataGridUtils.GetDataGridViewMafileHidenCell(AccountsDataGridView, row).Value = account;
            AccountsDataGridUtils.GetDataGridViewTruePasswordHidenCell(AccountsDataGridView, row).Value =
                PasswordTextBox.Text.Trim();
            Logger.Debug($"{login} added to accounts list");

            Task.Run(() =>
            {
                if (account.Session != null)
                {
                    var profileImage = ImageUtils.GetSteamProfileSmallImage(account.Session.SteamID);
                    if (profileImage != null)
                        AccountsDataGridUtils.GetDataGridViewImageCell(AccountsDataGridView, row).Value = profileImage;
                }
            });

            LoginTextBox.Clear();
            PasswordTextBox.Clear();
            MafilePathTextBox.Clear();
            SteamApiTextBox.Clear();

            UpdateAccountsFile();
        }


        private void UpdateAccountsFile()
        {
            var accounts = new List<SavedSteamAccount>();
            for (var i = 0; i < AccountsDataGridView.RowCount; i++)
            {
                var login = (string) AccountsDataGridUtils.GetDataGridViewLoginCell(AccountsDataGridView, i).Value;
                var password = (string) AccountsDataGridUtils
                    .GetDataGridViewTruePasswordHidenCell(AccountsDataGridView, i).Value;
                var opskinsApi =
                    (string) AccountsDataGridUtils.GetDataGridViewSteamApiCell(AccountsDataGridView, i).Value;
                var mafile = (SteamGuardAccount) AccountsDataGridUtils
                    .GetDataGridViewMafileHidenCell(AccountsDataGridView, i).Value;

                accounts.Add(new SavedSteamAccount
                {
                    Login = login,
                    Password = password,
                    OpskinsApi = opskinsApi,
                    Mafile = mafile
                });
            }

            SavedSteamAccount.UpdateAll(accounts);
        }

        private void DeleteAccountButton_Click(object sender, EventArgs e)
        {
            var currentCell = AccountsDataGridView.CurrentCell;
            if (currentCell == null)
            {
                Logger.Warning("No accounts selected");
                return;
            }

            var row = currentCell.RowIndex;
            if (row < 0) return;

            var accName = AccountsDataGridUtils.GetDataGridViewLoginCell(AccountsDataGridView, row).Value;

            var confirmResult = MessageBox.Show($"Are you sure you want to delete the account - {accName}?",
                "Confirm deletion?",
                MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                AccountsDataGridView.Rows.RemoveAt(row);
                Logger.Debug($"Account {accName} was deleted from accounts list");
            }

            UpdateAccountsFile();
        }

        private void EditAccountButton_Click(object sender, EventArgs e)
        {
            var currentCell = AccountsDataGridView.CurrentCell;
            if (currentCell == null)
            {
                Logger.Warning("No accounts selected");
                return;
            }

            var row = currentCell.RowIndex;
            if (row < 0) return;

            LoginTextBox.Text =
                " " + (string) AccountsDataGridUtils.GetDataGridViewLoginCell(AccountsDataGridView, row).Value;
            PasswordTextBox.Text = " " + (string) AccountsDataGridUtils
                                       .GetDataGridViewTruePasswordHidenCell(AccountsDataGridView, row).Value;
            SteamApiTextBox.Text =
                " " + (string) AccountsDataGridUtils.GetDataGridViewSteamApiCell(AccountsDataGridView, row).Value;
        }

        private void LogTextBox_TextChanged(object sender, EventArgs e)
        {
            LogTextBox.SelectionStart = LogTextBox.Text.Length;
            LogTextBox.ScrollToCaret();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            LoginButton.Enabled = false;
            var currentCell = AccountsDataGridView.CurrentCell;
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

            var login = (string) AccountsDataGridUtils
                .GetDataGridViewLoginCell(AccountsDataGridView, currentCell.RowIndex).Value;
            var password = (string) AccountsDataGridUtils
                .GetDataGridViewTruePasswordHidenCell(AccountsDataGridView, currentCell.RowIndex).Value;
            var api = (string) AccountsDataGridUtils
                .GetDataGridViewSteamApiCell(AccountsDataGridView, currentCell.RowIndex).Value;
            var mafile = (SteamGuardAccount) AccountsDataGridUtils
                .GetDataGridViewMafileHidenCell(AccountsDataGridView, currentCell.RowIndex).Value;
            var image = (Image) AccountsDataGridUtils
                .GetDataGridViewImageCell(AccountsDataGridView, currentCell.RowIndex).Value;

            Logger.Info($"Steam authentication for {login} started");

            Task.Run(() =>
            {
                try
                {
                    CurrentSession.SteamManager = new SteamManager(login, password, mafile, api);
                }
                catch (Exception ex)
                {
                    Logger.Error("Login failed!", ex);
                    LoginButton.Enabled = true;
                    return;
                }

                Dispatcher.AsMainForm(() =>
                {
                    CurrentSession.AccountImage = image;

                    Program.MainForm.MarketControlTab.SaleControl.AuthCurrentAccount();
                    Program.MainForm.MarketControlTab.RelistControl.AuthCurrentAccount();
                    Program.MainForm.TradeControlTab.TradeControl.AuthCurrentAccount();
                    Program.MainForm.TradeControlTab.RecievedTradeManageControl.AuthCurrentAccount();
                    Program.MainForm.TradeControlTab.TradeHistoryControl.AuthCurrentAccount();

                    Logger.Info($"Steam authentication for {login} successful");
                    LoginButton.Enabled = true;
                });
            });
        }

        private void LoggingLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedLevel = LoggingLevelComboBox.SelectedValue;

            var level = LoggerLevel.INFO;
            switch (selectedLevel)
            {
                case "Debug":
                    level = LoggerLevel.DEBUG;
                    break;
                case "Info + Errors":
                    level = LoggerLevel.INFO;
                    break;
                case "Errors":
                    level = LoggerLevel.ERROR;
                    break;
                case "None":
                    level = LoggerLevel.NONE;
                    break;
            }

            Logger.LOGGER_LEVEL = level;

            SavedSettings.UpdateField(ref SavedSettings.Get().SETTINGS_LOGGER_LEVEL,
                LoggingLevelComboBox.SelectedIndex);
        }

        private void SteamApiLinkLable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://steamcommunity.com/dev/apikey");
        }

        private void AveragePriceDaysNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().SETTINGS_AVERAGE_PRICE_PARSE_DAYS,
                (int) AveragePriceDaysNumericUpDown.Value);
        }

        private void Confirm2FANumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            SavedSettings.UpdateField(ref SavedSettings.Get().SETTINGS_2FA_ITEMS_TO_CONFIRM,
                (int) Confirm2FANumericUpDown.Value);
        }
    }
}