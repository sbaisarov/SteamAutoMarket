namespace SteamAutoMarket.Pages
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using Core;

    using OxyPlot;

    using SteamAutoMarket.Annotations;
    using SteamAutoMarket.Utils.Logger;

    /// <summary>
    /// Interaction logic for WorkingProcessForm.xaml
    /// </summary>
    public partial class WorkingProcessForm : INotifyPropertyChanged
    {
        private Task workingAction;

        private string workingLogs;

        private double averageSpeed = 0;

        private double currentSpeed = 0;

        private int minutesLeft = 0;

        private int averageMinutesLeft = 0;

        private int progressBarMaximum;

        private int progressBarValue;

        private WorkingProcessForm()
        {
            this.InitializeComponent();

            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string WorkingLogs
        {
            get => this.workingLogs;
            set
            {
                this.workingLogs = value;
                this.OnPropertyChanged();
            }
        }

        public double AverageSpeed
        {
            get => this.averageSpeed;
            set
            {
                this.averageSpeed = value;
                this.OnPropertyChanged();
            }
        }

        public double CurrentSpeed
        {
            get => this.currentSpeed;
            set
            {
                this.currentSpeed = value;
                this.OnPropertyChanged();
            }
        }

        public int MinutesLeft
        {
            get => this.minutesLeft;
            set
            {
                this.minutesLeft = value;
                this.OnPropertyChanged();
            }
        }

        public int AverageMinutesLeft
        {
            get => this.averageMinutesLeft;
            set
            {
                this.averageMinutesLeft = value;
                this.OnPropertyChanged();
            }
        }

        public int ProgressBarMaximum
        {
            get => this.progressBarMaximum;
            set
            {
                this.progressBarMaximum = value;
                this.OnPropertyChanged();
            }
        }

        public int ProgressBarValue
        {
            get => this.progressBarValue;
            set
            {
                this.progressBarValue = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<DataPoint> ChartModel { get; set; } = new ObservableCollection<DataPoint>();

        public static WorkingProcessForm NewWorkingProcessWindow(string title, int maximumProgressBarValue)
        {
            var window = new WorkingProcessForm { Title = title, ProgressBarMaximum = maximumProgressBarValue };

            return window;
        }

        public void ProcessMethod(Action action)
        {
            this.Show();
            try
            {
                this.workingAction = Task.Run(action);
                this.workingAction.ContinueWith(tsk => this.AppendLog("Working process finished"));
            }
            catch (Exception e)
            {
                Logger.Log.Error("Error on working process", e);
            }
        }

        public void AppendLog(string message)
        {
            this.WorkingLogs += $"{UiLogAppender.GetCurrentDate()} - {message}{Environment.NewLine}";
            Logger.Log.Info(message);
        }

        public void IncrementProgress() => this.ProgressBarValue++;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}