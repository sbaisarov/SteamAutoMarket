namespace SteamAutoMarket.UI.Pages
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Controls;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.Utils;
    using SteamAutoMarket.UI.Utils.Logger;

    /// <summary>
    /// Interaction logic for WorkingProcessForm.xaml
    /// </summary>
    public partial class WorkingProcess
    {
        public WorkingProcess()
        {
            this.DataContext = UiGlobalVariables.WorkingProcessDataContext;
            this.InitializeComponent();
        }

        public static void OpenTab() => AppUtils.OpenTab("UI/Pages/WorkingProcess.xaml");

        public static void ProcessMethod(Action action, string title)
        {
            var wp = UiGlobalVariables.WorkingProcessDataContext;

            try
            {
                if (wp.WorkingAction?.IsCompleted == false)
                {
                    ErrorNotify.InfoMessageBox(
                        $"Only one working process can be processed at the same time to avoid temporary Steam ban on requests. Wait for '{wp.Title}' working process finish and try again.");
                    return;
                }

                OpenTab();

                wp.ResetWorkingProcessToDefault();
                wp.Title = title;

                wp.Timer = Stopwatch.StartNew();

                wp.CancellationTokenSource = new CancellationTokenSource();
                wp.CancellationToken = wp.CancellationTokenSource.Token;

                wp.WorkingAction = Task.Run(action, wp.CancellationToken);
                wp.WorkingAction.ContinueWith(tsk => { wp.AppendLog("Working process finished"); });
            }
            catch (Exception e)
            {
                Logger.Log.Error("Error on working process", e);
            }
        }

        private void StopButton_OnClick(object sender, EventArgs e)
        {
            var wp = UiGlobalVariables.WorkingProcessDataContext;

            if (wp.WorkingAction?.IsCompleted == false)
            {
                wp.AppendLog("Working process is forcing to stop");
                wp.CancellationTokenSource.Cancel();
            }
        }

        private void WorkingProcessTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (UiGlobalVariables.WorkingProcessDataContext.ScrollLogsToEnd)
            {
                this.WorkingProcessTextBox.ScrollToEnd();
            }
        }
    }
}