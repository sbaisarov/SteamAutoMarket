namespace SteamAutoMarket.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;

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

        private double averageMinutesLeft;

        private double averageSpeed;

        private CancellationTokenSource cancellationTokenSource;

        private double currentSpeed;

        private double minutesLeft;

        private int progressBarMaximum = 1;

        private int progressBarValue;

        private Stopwatch timer;

        private List<double> times = new List<double>();

        private Task workingAction;

        private string workingLogs;

        private WorkingProcessForm()
        {
            this.InitializeComponent();

            this.DataContext = this;

            this.Closing += this.OnWindowClosing;
            this.SizeChanged += this.PlotContainer_OnSizeChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public double AverageMinutesLeft
        {
            get => this.averageMinutesLeft;
            set
            {
                this.averageMinutesLeft = value;
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

        public CancellationToken CancellationToken { get; private set; }

        public ObservableCollection<DataPoint> ChartModel { get; set; } =
            new ObservableCollection<DataPoint> { new DataPoint(0, 0) };

        public double CurrentSpeed
        {
            get => this.currentSpeed;
            set
            {
                this.currentSpeed = value;
                this.OnPropertyChanged();
            }
        }

        public double MinutesLeft
        {
            get => this.minutesLeft;
            set
            {
                this.minutesLeft = value;
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

        public string WorkingLogs
        {
            get => this.workingLogs;
            set
            {
                this.workingLogs = value;
                this.OnPropertyChanged();
            }
        }

        public static WorkingProcessForm NewWorkingProcessWindow(string title)
        {
            var window = new WorkingProcessForm { Title = title };

            return window;
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
            this.times.Add(elapsedSeconds);

            this.ChartModel.AddDispatch(new DataPoint(this.ProgressBarValue, elapsedSeconds));

            var iterationsLeft = this.ProgressBarMaximum - this.ProgressBarValue;
            this.CurrentSpeed = Math.Round(elapsedSeconds, 2);
            this.MinutesLeft = Math.Round(iterationsLeft * elapsedSeconds / 60, 2);

            if (this.ChartModel.Count > 1)
            {
                var averageSeconds = this.times.ToList().Average();
                this.AverageSpeed = Math.Round(averageSeconds, 2);
                this.AverageMinutesLeft = Math.Round(iterationsLeft * averageSeconds / 60);
            }

            if (this.ChartModel.Count > 50)
            {
                var oldChart = this.ChartModel.ToList();
                var newChart = new List<DataPoint>();
                for (var j = 3; j < oldChart.Count; j += 2)
                {
                    var averageX = (oldChart[j].X + oldChart[j - 1].X) / 2;
                    var averageY = (oldChart[j].Y + oldChart[j - 1].Y) / 2;
                    newChart.Add(new DataPoint(averageX, averageY));
                }

                this.ChartModel.ReplaceDispatch(newChart);
            }

            this.timer = Stopwatch.StartNew();
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

        private void PlotContainer_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Plot.Width = this.RightColumn.ActualWidth;
            this.Plot.InvalidateVisual();
        }
    }
}