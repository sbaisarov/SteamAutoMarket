using autotrade.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autotrade {
    public partial class WorkingProcessForm : Form {
        private Thread workingThread;
        private bool stopButtonPressed = false;

        public void InitProcess(Action process) {
            _activate();
            workingThread = new Thread(() => {
                process();
                _disactivate();
            });
            workingThread.Start();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AppendWorkingProcessInfo(string message) {
            Dispatcher.Invoke(Program.WorkingProcessForm, () => {
                this.logTextBox.AppendText($"{Logger.GetCurrentDate()} - {message}\n");
                Logger.Working(message);
            });
        }

        public WorkingProcessForm() {
            InitializeComponent();
        }

        private void WorkingProcessForm_Load(object sender, EventArgs e) {
            AppendWorkingProcessInfo("Working process started.");
        }

        private void _activate() {
            this.Show();
            Program.MainForm.Enabled = false;
        }

        private void _disactivate() {
            Dispatcher.Invoke(Program.WorkingProcessForm, () => {
                Program.MainForm.Enabled = true;
                this.Close();
                Program.WorkingProcessForm = new WorkingProcessForm();
            });
        }

        private void StopWorkingProcessButton_Click(object sender, EventArgs e) {
            stopButtonPressed = true;
            workingThread.Abort();
            _disactivate();
        }

        private void WorkingProcessForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (!stopButtonPressed) {
                StopWorkingProcessButton_Click(sender, e);
            }
        }
    }
}
