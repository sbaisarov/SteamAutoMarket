using System;
using System.Linq;
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
            ActivateForm();
            _workingThread = new Thread(
                () =>
                    {
                        process();
                        DeactivateForm();
                    });
            _workingThread.Start();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AppendWorkingProcessInfo(string message)
        {
            Dispatcher.AsWorkingProcessForm(
                () =>
                    {
                        logTextBox.AppendText($"{Logger.GetCurrentDate()} - {message}\n");

                        if (ScrollCheckBox.Checked)
                        {
                            logTextBox.ScrollToCaret();
                        }

                        Logger.Working(message);

                        ClearLogBox();
                    });
        }

        private void ClearLogBox()
        {
            if (logTextBox.Lines.Length <= 300)
            {
                return;
            }

            var realCount = logTextBox.Lines.Length;
            logTextBox.Lines = logTextBox.Lines.ToList().GetRange(100, realCount - 101).ToArray();
        }

        private void WorkingProcessForm_Load(object sender, EventArgs e)
        {
            AppendWorkingProcessInfo("Working process started.");
        }

        private void ActivateForm()
        {
            Dispatcher.AsMainForm(Show);
        }

        private void DeactivateForm()
        {
            Dispatcher.AsMainForm(() =>
                    {
                        Close();
                        Program.WorkingProcessForm = new WorkingProcessForm();
                    });
        }

        private void StopWorkingProcessButton_Click(object sender, EventArgs e)
        {
            _stopButtonPressed = true;
            Dispatcher.AsWorkingProcessForm(() =>
            {
                _workingThread.Abort();
                DeactivateForm();
            });
        }

        private void WorkingProcessForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_stopButtonPressed) StopWorkingProcessButton_Click(sender, e);
        }
    }
}