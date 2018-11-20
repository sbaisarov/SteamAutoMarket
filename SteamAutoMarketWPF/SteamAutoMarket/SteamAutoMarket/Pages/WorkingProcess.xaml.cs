namespace SteamAutoMarket.Pages
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
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
            this.SizeChanged += this.PlotContainer_OnSizeChanged;

            this.DataContext = UiGlobalVariables.WorkingProcessDataContext;
            this.InitializeComponent();
        }


        public static void OpenTab()
        {
            var target = NavigationHelper.FindFrame("_top", UiGlobalVariables.MainWindow);
            NavigationCommands.GoToPage.Execute("/Pages/WorkingProcess.xaml", target);
        }

        public static void ProcessMethod(Action action, string title)
        {
            UiGlobalVariables.WorkingProcess.ProcessMethodPrivate(action, title);
        }

        private void PlotContainer_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Plot.Width = this.RightColumn.ActualWidth;
            this.Plot.InvalidateVisual();
        }

        private void ProcessMethodPrivate(Action action, string title)
        {
            var wp = UiGlobalVariables.WorkingProcessDataContext;
            if (wp.isAnyWorkingProcessRunning)
            {
                ErrorNotify.InfoMessageBox(
                    "Only one working process can be processed at the same time to avoid temporary Steam ban on requests. Wait for an other working process finish and try again.");

                return;
            }

            var context = wp;

            context.ResetWorkingProcessToDefault();
            context.Title = title;

            context.Timer = Stopwatch.StartNew();
            wp.isAnyWorkingProcessRunning = true;
            try
            {
                wp.cancellationTokenSource = new CancellationTokenSource();
                wp.CancellationToken = wp.cancellationTokenSource.Token;

                wp.workingAction = Task.Run(action, wp.CancellationToken);
                wp.workingAction.ContinueWith(
                    tsk =>
                        {
                            context.AppendLog("Working process finished");
                            wp.isAnyWorkingProcessRunning = false;
                        });
            }
            catch (Exception e)
            {
                Logger.Log.Error("Error on working process", e);
            }
        }

        private void StopButton_OnClick(object sender, EventArgs e)
        {
            var wp = UiGlobalVariables.WorkingProcessDataContext;

            if (wp.workingAction.IsCompleted == false)
            {
                wp.AppendLog("Working process is forcing to stop");
                wp.cancellationTokenSource.Cancel();
                wp.isAnyWorkingProcessRunning = false;
            }
        }
    }
}