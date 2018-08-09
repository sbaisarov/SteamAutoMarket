using autotrade.Utils;
using autotrade.WorkingProcess;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using static autotrade.Steam.TradeOffer.Inventory;

namespace autotrade {
    public partial class InventoryLoadingForm : Form {
        private Thread workingThread;
        private int totalItemsCount;
        private int currentPage;
        private int totalPages;
        private List<RgFullItem> items;
        bool stopButtonPressed = false;
        
        public void InitProcess() {
            this.Text = $"{CurrentSession.InventoryAppId}-{CurrentSession.InventoryContextId} inventory loading";
            _activate();
            workingThread = new Thread(() => {
                LoadCurrentInventory();
            });
            workingThread.Start();
        }

        public void LoadCurrentInventory() {
            items = CurrentSession.SteamManager.LoadInventory(CurrentSession.SteamManager.Guard.Session.SteamID.ToString(), CurrentSession.InventoryAppId, CurrentSession.InventoryContextId, true);
        }

        public void SetTotalItemsCount(int count) {
            this.totalItemsCount = count;
            this.totalPages = (int)Math.Ceiling((double)count / 5000);

            Dispatcher.Invoke(Program.InventoryLoadingForm, () => {
                TotalItemsLable.Text = $"Total items count - {count}";
                ProgressBar.Maximum = totalPages;
            });
        }

        public void TrackLoadedPage() {
            Dispatcher.Invoke(Program.InventoryLoadingForm, () => {
                PageLable.Text = $"Page {++currentPage} of {totalPages} loaded";
                ProgressBar.Value = currentPage;
            });
        }

        public List<RgFullItem> GetLoadedItems() {
            if (items == null) {
                Thread.Sleep(300);
                return GetLoadedItems();
            }
            return items;
        }

        public InventoryLoadingForm() {
            InitializeComponent();
        }

        private void _activate() {
            Dispatcher.Invoke(Program.MainForm, () => {
                this.Show();
                Program.MainForm.Enabled = false;
            });
        }

        public void Disactivate() {
            Dispatcher.Invoke(Program.MainForm, () => {
                Program.MainForm.Enabled = true;
                this.Close();
                Program.InventoryLoadingForm = new InventoryLoadingForm();
            });
        }

        private void StopWorkingProcessButton_Click(object sender, EventArgs e) {
            stopButtonPressed = true;
            Dispatcher.Invoke(Program.InventoryLoadingForm, () => {
                Logger.Debug($"Inventory {CurrentSession.InventoryAppId}-{CurrentSession.InventoryContextId} loading process aborted");
                items = new List<RgFullItem>();
                workingThread.Abort();
                Disactivate();
            });
        }

        private void InventoryLoadingForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (!stopButtonPressed) {
                StopWorkingProcessButton_Click(sender, e);
            }
        }
    }
}
