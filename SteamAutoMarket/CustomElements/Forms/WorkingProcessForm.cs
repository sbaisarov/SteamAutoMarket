namespace SteamAutoMarket.CustomElements.Forms
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    using SteamAutoMarket.Utils;

    public partial class WorkingProcessForm : Form
    {
        private bool stopButtonPressed;

        private Thread workingThread;

        public WorkingProcessForm()
        {
            this.InitializeComponent();
        }

        public static void InitProcess(Action process)
        {
            Dispatcher.AsMainForm(() => { Program.WorkingProcessForm = new WorkingProcessForm(); });
            Program.WorkingProcessForm.StartProcess(process);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AppendWorkingProcessInfo(string message)
        {
            try
            {
                Dispatcher.AsWorkingProcessForm(
                    () =>
                        {
                            this.LogTextBox.AppendText($"{Logger.GetCurrentDate()} - {message}\n");
                            this.ClearLogBox();

                            if (this.ScrollCheckBox.Checked)
                            {
                                this.LogTextBox.ScrollToCaret();
                            }

                            Logger.Working(message);
                        });
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }

        private void ClearLogBox()
        {
            if (this.LogTextBox.Lines.Length <= 1000)
            {
                return;
            }

            var list = this.LogTextBox.Lines.ToList();
            list.RemoveRange(0, 500);
            this.LogTextBox.Lines = list.ToArray();
        }

        private void WorkingProcessFormLoad(object sender, EventArgs e)
        {
            this.AppendWorkingProcessInfo("Working process started.");
        }

        private void DeactivateForm()
        {
            Dispatcher.AsWorkingProcessForm(this.Close);
        }

        private void StopWorkingProcessButtonClick(object sender, EventArgs e)
        {
            this.stopButtonPressed = true;
            Dispatcher.AsMainForm(
                () =>
                    {
                        this.workingThread.Abort();
                        this.DeactivateForm();
                    });
        }

        private void WorkingProcessFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.stopButtonPressed)
            {
                this.StopWorkingProcessButtonClick(sender, e);
            }
        }

        private void StartProcess(Action process)
        {
            Dispatcher.AsMainForm(
                () =>
                    {
                        this.Show();
                        this.workingThread = new Thread(
                            () =>
                                {
                                    process();
                                    this.DeactivateForm();
                                });
                        this.workingThread.Start();
                    });
        }
    }
}