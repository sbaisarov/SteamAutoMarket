namespace SteamAutoMarket.Pages
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using Core;

    using FirstFloor.ModernUI.Windows.Navigation;

    using SteamAutoMarket.Repository.Context;
    using SteamAutoMarket.Utils.Logger;

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

        public static void OpenTab()
        {
            Application.Current.Dispatcher.Invoke(
                () =>
                    {
                        var target = NavigationHelper.FindFrame("_top", UiGlobalVariables.MainWindow);
                        NavigationCommands.GoToPage.Execute("/Pages/WorkingProcess.xaml", target);
                    });
        }

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

            if (wp.WorkingAction.IsCompleted == false)
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