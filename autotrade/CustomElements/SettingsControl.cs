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
        public SettingsControl() {
            InitializeComponent();
            AccountsDataGridView.Rows.Add(Image.FromFile(@"C:\Users\shatulsky\Downloads\5ad66b40513b4b02a305198717d7853ef018f169.jpg"), "addcheckbox", "testSTRONGpassword1");
            AccountsDataGridView.Rows.Add(Image.FromFile(@"C:\Users\shatulsky\Downloads\5ad66b40513b4b02a305198717d7853ef018f169.jpg"), "IHORIHORIHOR", "IHORIHORIHOR");
            AccountsDataGridView.Rows.Add();
            AccountsDataGridView.Rows.Add();
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
            AccountsDataGridUtils.GetDataGridViewLoginCell(AccountsDataGridView, row).Value = LoginTextBox.Text;
            AccountsDataGridUtils.GetDataGridViewPasswordCell(AccountsDataGridView, row).Value = PasswordTextBox.Text;
            AccountsDataGridUtils.GetDataGridViewMafileHidenCell(AccountsDataGridView, row).Value = account;

            var profileImage = ImageUtils.GetSteamProfileSMallImage(account.Session.SteamID);
            if (profileImage != null) AccountsDataGridUtils.GetDataGridViewImageCell(AccountsDataGridView, row).Value = profileImage;

            LoginTextBox.Clear();
            PasswordTextBox.Clear();
            MafilePathTextBox.Clear();
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
        }

        private void Button2_Click(object sender, EventArgs e) {
            var currentCell = AccountsDataGridView.CurrentCell;
            if (currentCell == null) return;

            int row = currentCell.RowIndex;
            if (row < 0) return;

            Program.MainForm.OpenSaleMenu();
            Program.MainForm.LoadInventory();
        }
    }
}
