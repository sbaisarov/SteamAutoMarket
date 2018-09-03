using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using autotrade.Utils;

namespace autotrade.CustomElements.Forms
{
    public partial class WorkingProcessForm : Form
    {
        private bool stopButtonPressed;
        private Thread workingThread;

        public WorkingProcessForm()
        {
            InitializeComponent();
        }

        public void InitProcess(Action process)
        {
            _activate();
            workingThread = new Thread(() =>
            {
                process();
                _disactivate();
            });
            workingThread.Start();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AppendWorkingProcessInfo(string message)
        {
            Dispatcher.AsWorkingProcessForm(() =>
            {
                logTextBox.AppendText($"{Logger.GetCurrentDate()} - {message}\n");
                logTextBox.ScrollToCaret();

                Logger.Working(message);
            });
        }

        private void WorkingProcessForm_Load(object sender, EventArgs e)
        {
            AppendWorkingProcessInfo("Working process started.");
        }

        private void _activate()
        {
            Show();
        }

        private void _disactivate()
        {
            Dispatcher.AsMainForm(() =>
            {
                Close();
                Program.WorkingProcessForm = new WorkingProcessForm();
            });
        }

        private void StopWorkingProcessButton_Click(object sender, EventArgs e)
        {
            stopButtonPressed = true;
            workingThread.Abort();
            _disactivate();
        }

        private void WorkingProcessForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!stopButtonPressed) StopWorkingProcessButton_Click(sender, e);
        }
    }
}