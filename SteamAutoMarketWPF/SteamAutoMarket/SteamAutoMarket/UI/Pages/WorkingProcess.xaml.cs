namespace SteamAutoMarket.UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    using SteamAutoMarket.Core;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.Utils;
    using SteamAutoMarket.UI.Utils.Logger;

    /// <summary>
    /// Interaction logic for WorkingProcessForm.xaml
    /// </summary>
    [Obfuscation(Exclude = true)]
    public partial class WorkingProcess
    {
        public WorkingProcess()
        {
            UiGlobalVariables.WorkingProcessTab = this;
            this.InitializeComponent();
            this.DataContext = UiGlobalVariables.LastInvokedWorkingProcessDataContext;
            this.RefreshWorkingProcessesList();
        }

        public static void OpenTab()
        {
            AppUtils.OpenTab("UI/Pages/WorkingProcess.xaml");
        }

        public void ChangeDataContext(WorkingProcessDataContext wp)
        {
            Application.Current.Dispatcher.Invoke(
                () =>
                    {
                        this.RefreshWorkingProcessesList();
                        this.CurrentProcessComboBox.SelectedValue = wp.Title;
                        this.DataContext = wp;
                    },
                DispatcherPriority.Send);
        }

        public void RefreshWorkingProcessesList()
        {
            Application.Current.Dispatcher.Invoke(
                () =>
                    {
                        var wp = this.GetContext();
                        wp.WorkingProcessesList =
                            new ObservableCollection<string>(WorkingProcessProvider.GetAllProcessesNames());

                        if ((string)this.CurrentProcessComboBox.SelectedValue != wp.Title)
                        {
                            this.CurrentProcessComboBox.SelectedValue = wp.Title;
                        }
                    },
                DispatcherPriority.Send);
        }

        private WorkingProcessDataContext GetContext() => (WorkingProcessDataContext)this.DataContext;

        private void RemoveCurrentOnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.RefreshWorkingProcessesList();

                if (((IEnumerable<string>)this.CurrentProcessComboBox.ItemsSource)?.Count() <= 1)
                {
                    ErrorNotify.CriticalMessageBox(
                        "You can remove processes from list only in case when the list will have at least 1 more process!");

                    return;
                }

                if (this.GetContext().WorkingAction?.IsCompleted == false)
                {
                    ErrorNotify.CriticalMessageBox("You can not remove this processes!");
                    return;
                }

                WorkingProcessProvider.RemoveWorkingProcessFromList((string)this.CurrentProcessComboBox.SelectedValue);

                var newValue = WorkingProcessProvider.GetAllProcessesNames().FirstOrDefault()
                               ?? WorkingProcessProvider.EmptyWorkingProcess.Title;

                this.CurrentProcessComboBox.SelectedValue = newValue;

                this.RefreshWorkingProcessesList();
            }
            catch (Exception ex)
            {
                ErrorNotify.CriticalMessageBox(
                    "Some error occured. Try to switch over working processes and retry removing");
                Logger.Log.Error("Error on working process remove", ex);
                this.RefreshWorkingProcessesList();
            }
        }

        private void StopButton_OnClick(object sender, EventArgs e)
        {
            var wp = (WorkingProcessDataContext)this.DataContext;
            if (wp.WorkingAction?.IsCompleted == false && wp.CancellationToken.IsCancellationRequested == false)
            {
                wp.AppendLog("Working process is forcing to stop");
                wp.CancellationTokenSource.Cancel();
            }
        }

        private void WorkingProcessOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.DataContext = WorkingProcessProvider.GetInstance((string)this.CurrentProcessComboBox.SelectedValue);
        }

        private void WorkingProcessTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var currentContext = (WorkingProcessDataContext)this.DataContext;
            if (currentContext != null && currentContext.ScrollLogsToEnd)
            {
                this.WorkingProcessTextBox.ScrollToEnd();
            }
        }
    }
}