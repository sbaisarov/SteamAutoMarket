using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

using SteamAutoMarket.Utils;

namespace SteamAutoMarket.CustomElements.Forms
{
    public partial class WorkingProcessForm : Form
    {
        private bool _stopButtonPressed;

        private Thread _workingThread;

        public WorkingProcessForm()
        {
            InitializeComponent();
        }

        public void InitProcess(Action process)
        {
            _activate();
            _workingThread = new Thread(
                () =>
                    {
                        process();
                        _disactivate();
                    });
            _workingThread.Start();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AppendWorkingProcessInfo(string message)
        {
            Dispatcher.AsWorkingProcessForm(
                () =>
                    {
                        this.logTextBox.AppendText($"{Logger.GetCurrentDate()} - {message}\n");
                        this.logTextBox.ScrollToCaret();

                        Logger.Working(message);
                    });
        }

        private void WorkingProcessForm_Load(object sender, EventArgs e)
        {
            AppendWorkingProcessInfo("Working process started.");
        }

        private void _activate()
        {
            Dispatcher.AsWorkingProcessForm(this.Show);
        }

        private void _disactivate()
        {
            Dispatcher.AsMainForm(
                () =>
                    {
                        Close();
                        Program.WorkingProcessForm = new WorkingProcessForm();
                    });
        }

        private void StopWorkingProcessButton_Click(object sender, EventArgs e)
        {
            _stopButtonPressed = true;
            _workingThread.Abort();
            _disactivate();
        }

        private void WorkingProcessForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_stopButtonPressed) StopWorkingProcessButton_Click(sender, e);
        }
    }
}