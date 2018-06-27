using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SteamAuth;
using Newtonsoft.Json;
using autotrade.Utils;
using autotrade.Steam;
using autotrade.WorkingProcess;

namespace autotrade.CustomElements {
    public partial class SettingsControl : UserControl {


        public SettingsControl() {
            InitializeComponent();
            if (File.Exists(SettingsContainer.ACCOUNTS_FILE_PATH)) {
                var accounts = SavedSteamAccount.Get();
                foreach (var acc in accounts) {
                    int row = AccountsDataGridView.Rows.Add();
                    AccountsDataGridUtils.GetDataGridViewLoginCell(AccountsDataGridView, row).Value = acc.Login;
                    AccountsDataGridUtils.GetDataGridViewPasswordCell(AccountsDataGridView, row).Value = acc.Password;
                    AccountsDataGridUtils.GetDataGridViewOpskinsApiCell(AccountsDataGridView, row).Value = acc.OpskinsApi;
                    AccountsDataGridUtils.GetDataGridViewMafileHidenCell(AccountsDataGridView, row).Value = acc.Mafile;

                    Task.Run(() => {
                        var profileImage = ImageUtils.GetSteamProfileSMallImage(acc.Mafile.Session.SteamID);
                        if (profileImage != null) AccountsDataGridUtils.GetDataGridViewImageCell(AccountsDataGridView, row).Value = profileImage;
                    });
                }
            };
        }

        private void BrowseMafilePath_Click(object sender, EventArgs e) {
            using (var openFileDialog = new OpenFileDialog()) {
                openFileDialog.Filter = "Mobile auth file (*.maFile)|*.mafile";
                openFileDialog.Multiselect = false;
                openFileDialog.Title = "MaFile path selecting";

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    MafilePathTextBox.Text = openFileDialog.FileName;
                    Logger.Info($"{MafilePathTextBox.Text} mafile selected");
                }
            }
        }

        private void AddNewAccountButton_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(LoginTextBox.Text) || string.IsNullOrEmpty(MafilePathTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Text)) {
                MessageBox.Show("Some fields are filled - incorrectly", "Error adding account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error adding account. Some fields are filled - incorrectly");
                return;
            }
            if (AccountsDataGridUtils.IsAccountAreadyExist(AccountsDataGridView, LoginTextBox.Text.Trim())) {
                Logger.Error($"{LoginTextBox.Text.Trim()} already exist in accounts list");
                MessageBox.Show($"{LoginTextBox.Text.Trim()} already exist in accounts list", "Error on the account add", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SteamGuardAccount account = null;
            try {
                account = JsonConvert.DeserializeObject<SteamGuardAccount>(File.ReadAllText(MafilePathTextBox.Text));
            } catch (Exception ex) {
                Logger.Error("Error processing MaFile", ex);
                MessageBox.Show(ex.Message, "Error processing MaFile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (account == null) {
                Logger.Error("Error processing MaFile");
                return;
            }

            int row = AccountsDataGridView.Rows.Add();

            var login = LoginTextBox.Text.Trim();
            AccountsDataGridUtils.GetDataGridViewLoginCell(AccountsDataGridView, row).Value = login;
            AccountsDataGridUtils.GetDataGridViewPasswordCell(AccountsDataGridView, row).Value = PasswordTextBox.Text.Trim();
            AccountsDataGridUtils.GetDataGridViewOpskinsApiCell(AccountsDataGridView, row).Value = OpskinsApiTextBox.Text.Trim();
            AccountsDataGridUtils.GetDataGridViewMafileHidenCell(AccountsDataGridView, row).Value = account;
            Logger.Info($"{login} added to accounts list");

            Task.Run(() => {
                if (account.Session != null) {
                    var profileImage = ImageUtils.GetSteamProfileSMallImage(account.Session.SteamID);
                    if (profileImage != null) AccountsDataGridUtils.GetDataGridViewImageCell(AccountsDataGridView, row).Value = profileImage;
                }
            });

            LoginTextBox.Clear();
            PasswordTextBox.Clear();
            MafilePathTextBox.Clear();
            OpskinsApiTextBox.Clear();

            UpdateAccountsFile();
        }


        private void UpdateAccountsFile() {
            var accounts = new List<SavedSteamAccount>();
            for (int i = 0; i < AccountsDataGridView.RowCount; i++) {
                var login = (String)AccountsDataGridUtils.GetDataGridViewLoginCell(AccountsDataGridView, i).Value;
                var password = (String)AccountsDataGridUtils.GetDataGridViewPasswordCell(AccountsDataGridView, i).Value;
                var opskinsApi = (String)AccountsDataGridUtils.GetDataGridViewOpskinsApiCell(AccountsDataGridView, i).Value;
                var mafile = (SteamGuardAccount)AccountsDataGridUtils.GetDataGridViewMafileHidenCell(AccountsDataGridView, i).Value;

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

        private void DeleteAccountButton_Click(object sender, EventArgs e) {
            var currentCell = AccountsDataGridView.CurrentCell;
            if (currentCell == null) {
                Logger.Warning("No accounts selected");
                return;
            }

            int row = currentCell.RowIndex;
            if (row < 0) return;

            var accName = AccountsDataGridUtils.GetDataGridViewLoginCell(AccountsDataGridView, row).Value;

            var confirmResult = MessageBox.Show($"Are you sure you want to delete the account - {accName}?",
                                      "Confirm deletion?",
                                      MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes) {
                AccountsDataGridView.Rows.RemoveAt(row);
                Logger.Info($"Account {accName} was deleted from accounts list");
            }

            UpdateAccountsFile();
        }

        private void EditAccountButton_Click(object sender, EventArgs e) {
            var currentCell = AccountsDataGridView.CurrentCell;
            if (currentCell == null) {
                Logger.Warning("No accounts selected");
                return;
            }

            int row = currentCell.RowIndex;
            if (row < 0) return;

            LoginTextBox.Text = " " + (String)AccountsDataGridUtils.GetDataGridViewLoginCell(AccountsDataGridView, row).Value;
            PasswordTextBox.Text = " " + (String)AccountsDataGridUtils.GetDataGridViewPasswordCell(AccountsDataGridView, row).Value;
            OpskinsApiTextBox.Text = " " + (String)AccountsDataGridUtils.GetDataGridViewOpskinsApiCell(AccountsDataGridView, row).Value;
        }

        private void LogTextBox_TextChanged(object sender, EventArgs e) {
            LogTextBox.SelectionStart = LogTextBox.Text.Length;
            LogTextBox.ScrollToCaret();
        }

        private void LoginButton_Click(object sender, EventArgs e) {
            var currentCell = AccountsDataGridView.CurrentCell;
            if (currentCell == null) {
                Logger.Warning("No accounts selected");
                return;
            }

            int row = currentCell.RowIndex;
            if (row < 0) return;

            var login = (string)AccountsDataGridUtils.GetDataGridViewLoginCell(AccountsDataGridView, currentCell.RowIndex).Value;
            var password = (string)AccountsDataGridUtils.GetDataGridViewPasswordCell(AccountsDataGridView, currentCell.RowIndex).Value;
            var mafile = (SteamGuardAccount)AccountsDataGridUtils.GetDataGridViewMafileHidenCell(AccountsDataGridView, currentCell.RowIndex).Value;
            var image = (Image)AccountsDataGridUtils.GetDataGridViewImageCell(AccountsDataGridView, currentCell.RowIndex).Value;

            Dispatcher.Invoke(Program.MainForm, () => {
                Utils.Logger.Info("Steam authentication started");
                this.Enabled = false;
                CurrentSession.CurrentUser = new SteamManager(login, password, mafile);
                this.Enabled = true;
                Utils.Logger.Info("Steam authentication successful");
                CurrentSession.AccountImage = image;
                Program.MainForm.SaleLinkButton_Click(null, null);
                Program.MainForm.SaleControl.AuthCurrentAccount();
            });
        }

        private void LoggingLevelComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            Logger.LoggerLevel = LoggingLevelComboBox.SelectedIndex;

            var settings = SavedSettings.Get();
            settings.LOGGER_LEVEL = LoggingLevelComboBox.SelectedIndex;
            SavedSettings.UpdateAll(settings);
        }
    }
}
