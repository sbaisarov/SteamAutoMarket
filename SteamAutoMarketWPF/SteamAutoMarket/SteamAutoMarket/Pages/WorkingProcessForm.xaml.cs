namespace SteamAutoMarket.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    using Core;

    using OxyPlot;

    using SteamAutoMarket.Annotations;
    using SteamAutoMarket.Utils.Extension;
    using SteamAutoMarket.Utils.Logger;

    /// <summary>
    /// Interaction logic for WorkingProcessForm.xaml
    /// </summary>
    public partial class WorkingProcessForm : INotifyPropertyChanged
    {
        private static bool isAnyWorkingProcessRunning;

        private Stopwatch timer;

        private Task workingAction;

        private CancellationTokenSource cancellationTokenSource;

        private string workingLogs;

        private double averageSpeed;

        private double currentSpeed;

        private int minutesLeft;

        private int averageMinutesLeft;

        private int progressBarMaximum = 1;

        private int progressBarValue;

        private WorkingProcessForm()
        {
            this.InitializeComponent();

            this.DataContext = this;

            this.Closing += this.OnWindowClosing;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CancellationToken CancellationToken { get; private set; }

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

        public ObservableCollection<DataPoint> ChartModel { get; set; } =
            new ObservableCollection<DataPoint> { new DataPoint(0, 0) };

        public static WorkingProcessForm NewWorkingProcessWindow(string title)
        {
            var window = new WorkingProcessForm { Title = title };

            return window;
        }

        public void ProcessMethod(Action action)
        {
            if (isAnyWorkingProcessRunning)
            {
                ErrorNotify.InfoMessageBox(
                    "Only one working process can be processed at the same time to avoid temporary Steam ban on requests. Wait for an other working process finish and try again.");

                return;
            }

            this.timer = Stopwatch.StartNew();
            this.Show();
            isAnyWorkingProcessRunning = true;
            try
            {
                this.cancellationTokenSource = new CancellationTokenSource();
                this.CancellationToken = this.cancellationTokenSource.Token;

                this.workingAction = Task.Run(action, this.CancellationToken);
                this.workingAction.ContinueWith(
                    tsk =>
                        {
                            this.AppendLog("Working process finished");
                            isAnyWorkingProcessRunning = false;
                        },
                    this.CancellationToken);
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

        public void IncrementProgress()
        {
            if (this.ProgressBarValue < this.ProgressBarMaximum)
            {
                this.ProgressBarValue++;
            }

            this.timer.Stop();
            var elapsedSeconds = this.timer.ElapsedMilliseconds / 1000d;
            this.ChartModel.AddDispatch(new DataPoint(this.ProgressBarValue, elapsedSeconds));

            this.timer = Stopwatch.StartNew();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            this.AppendLog("Working process is forcing to stop");
            this.cancellationTokenSource.Cancel();
            isAnyWorkingProcessRunning = false;
        }
    }
}