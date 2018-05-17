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

namespace autotrade.CustomElements {
    public partial class SettingsControl : UserControl {
        private string accountsFilePath = "accounts.ini";

        public SettingsControl() {
            InitializeComponent();
            if (File.Exists(accountsFilePath)) {
                var accounts = JsonConvert.DeserializeObject<List<SavedSteamAccount>>(File.ReadAllText(accountsFilePath));
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

        private void Button1_Click(object sender, EventArgs e) {
            using (var openFileDialog = new OpenFileDialog()) {
                openFileDialog.Filter = "Mobile auth file (*.maFile)|*.mafile";
                openFileDialog.Multiselect = false;
                openFileDialog.Title = "Выбор мафайла";

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    MafilePathTextBox.Text = openFileDialog.FileName;
                }
            }
        }

        private void AddNewAccountButton_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(LoginTextBox.Text) || string.IsNullOrEmpty(MafilePathTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Text)) {
                MessageBox.Show("Поля заполнены - некорректно", "Ошибка при добавлении аккаунта", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SteamGuardAccount account = null;
            try {
                account = JsonConvert.DeserializeObject<SteamGuardAccount>(File.ReadAllText(MafilePathTextBox.Text));
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Ошибка при обработке мафайла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int row = AccountsDataGridView.Rows.Add();
            AccountsDataGridUtils.GetDataGridViewLoginCell(AccountsDataGridView, row).Value = LoginTextBox.Text.Trim();
            AccountsDataGridUtils.GetDataGridViewPasswordCell(AccountsDataGridView, row).Value = PasswordTextBox.Text.Trim();
            AccountsDataGridUtils.GetDataGridViewOpskinsApiCell(AccountsDataGridView, row).Value = OpskinsApiTextBox.Text.Trim();
            AccountsDataGridUtils.GetDataGridViewMafileHidenCell(AccountsDataGridView, row).Value = account;

            Task.Run(() => {
                var profileImage = ImageUtils.GetSteamProfileSMallImage(account.Session.SteamID);
                if (profileImage != null) AccountsDataGridUtils.GetDataGridViewImageCell(AccountsDataGridView, row).Value = profileImage;
            });

            LoginTextBox.Clear();
            PasswordTextBox.Clear();
            MafilePathTextBox.Clear();
            OpskinsApiTextBox.Clear();

            UpdateAccountsFile();
        }


        private void Button2_Click(object sender, EventArgs e) {
            var currentCell = AccountsDataGridView.CurrentCell;
            if (currentCell == null) return;

            int row = currentCell.RowIndex;
            if (row < 0) return;

            Program.MainForm.OpenSaleMenu();
            Program.MainForm.LoadInventory();
        }

        private void UpdateAccountsFile() {
            var accounts = new List<SavedSteamAccount>();
            for (int i = 0; i < AccountsDataGridView.RowCount; i++) {
                var login = (String)AccountsDataGridUtils.GetDataGridViewLoginCell(AccountsDataGridView, i).Value;
                var password = (String)AccountsDataGridUtils.GetDataGridViewPasswordCell(AccountsDataGridView, i).Value;
                var opskinsApi = (String)AccountsDataGridUtils.GetDataGridViewOpskinsApiCell(AccountsDataGridView, i).Value;
                var mafile = (SteamGuardAccount)AccountsDataGridUtils.GetDataGridViewMafileHidenCell(AccountsDataGridView, i).Value;

                accounts.Add(new SavedSteamAccount {
                    Login = login,
                    Password = password,
                    OpskinsApi = opskinsApi,
                    Mafile = mafile
                });
            }
            File.WriteAllText(accountsFilePath, JsonConvert.SerializeObject(accounts));
        }

        private void DeleteAccountButton_Click(object sender, EventArgs e) {
            var currentCell = AccountsDataGridView.CurrentCell;
            if (currentCell == null) return;

            int row = currentCell.RowIndex;
            if (row < 0) return;

            var accName = AccountsDataGridUtils.GetDataGridViewLoginCell(AccountsDataGridView, row).Value;

            var confirmResult = MessageBox.Show($"Вы уверены что хотите удалить аккаунт {accName}?",
                                      "Подтвердить удаление?",
                                      MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes) {
                AccountsDataGridView.Rows.RemoveAt(row);
            }

            UpdateAccountsFile();
        }

        private void EditAccountButton_Click(object sender, EventArgs e) {
            var currentCell = AccountsDataGridView.CurrentCell;
            if (currentCell == null) return;

            int row = currentCell.RowIndex;
            if (row < 0) return;

            LoginTextBox.Text = " " + (String)AccountsDataGridUtils.GetDataGridViewLoginCell(AccountsDataGridView, row).Value;
            PasswordTextBox.Text = " " + (String)AccountsDataGridUtils.GetDataGridViewPasswordCell(AccountsDataGridView, row).Value;
            OpskinsApiTextBox.Text = " " + (String)AccountsDataGridUtils.GetDataGridViewOpskinsApiCell(AccountsDataGridView, row).Value;
        }
    }
}
