namespace SteamAutoMarket.CustomElements.Forms
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    using SteamAutoMarket.Utils;

    public partial class WorkingProcessForm : Form
    {
        private static Thread workingThread;

        private bool invokedFromStopButton;

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
                            this.ClearLogBox();

                            if (this.ScrollCheckBox.Checked)
                            {
                                this.LogTextBox.AppendText($"{Logger.GetCurrentDate()} - {message}\n");
                            }
                            else
                            {
                                this.AppendWithoutScroll($"{Logger.GetCurrentDate()} - {message}\n");
                            }

                            Logger.Working(message);
                        });
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LockWindowUpdate(IntPtr handle);

        private void AppendWithoutScroll(string text)
        {
            LockWindowUpdate(this.Handle);
            var pos = this.LogTextBox.SelectionStart;
            var len = this.LogTextBox.SelectionLength;
            this.LogTextBox.AppendText(text);
            this.LogTextBox.SelectionStart = pos;
            this.LogTextBox.SelectionLength = len;
            LockWindowUpdate(IntPtr.Zero);
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
            this.invokedFromStopButton = true;
            Dispatcher.AsMainForm(
                () =>
                    {
                        workingThread.Abort();
                        this.DeactivateForm();
                    });
        }

        private void WorkingProcessFormFormClosing(object sender, FormClosingEventArgs e)
        {
            // if invoked from [X] button
            if (this.invokedFromStopButton == false)
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
                        workingThread = new Thread(
                            () =>
                                {
                                    process();
                                    this.DeactivateForm();
                                });
                        workingThread.Start();
                    });
        }
    }
}