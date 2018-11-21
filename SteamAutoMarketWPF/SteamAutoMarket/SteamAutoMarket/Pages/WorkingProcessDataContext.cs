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
    using SteamAutoMarket.Repository.Context;
    using SteamAutoMarket.Repository.Settings;
    using SteamAutoMarket.Utils.Extension;
    using SteamAutoMarket.Utils.Logger;

    public class WorkingProcessDataContext : INotifyPropertyChanged
    {
        public CancellationTokenSource cancellationTokenSource;

        public Task workingAction;

        public CancellationToken CancellationToken { get; set; }

        private readonly List<double> times = new List<double>();

        private double averageMinutesLeft;

        private double averageSpeed;

        private double currentSpeed;

        private double minutesLeft;

        private int progressBarMaximum = 1;

        private int progressBarValue;

        private string title = "Working process";

        private string workingLogs;

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

        public string ButtonTitle => $"Stop {this.Title}";

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

        public bool ScrollLogsToEnd
        {
            get => SettingsProvider.GetInstance().ScrollLogsToEnd;
            set
            {
                SettingsProvider.GetInstance().ScrollLogsToEnd = value;
                this.OnPropertyChanged();
            }
        }

        public Stopwatch Timer { get; set; }

        public string Title
        {
            get => this.title;
            set
            {
                if (value == this.title) return;
                this.title = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.ButtonTitle));
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

            this.Timer.Stop();

            var elapsedSeconds = this.Timer.ElapsedMilliseconds / 1000d;
            this.times.Add(elapsedSeconds);

            this.ChartModel.AddDispatch(new DataPoint(this.ProgressBarValue, elapsedSeconds));

            var iterationsLeft = this.ProgressBarMaximum - this.ProgressBarValue;
            this.CurrentSpeed = Math.Round(elapsedSeconds, 2);
            this.MinutesLeft = Math.Round(iterationsLeft * elapsedSeconds / 60, 2);

            if (this.ChartModel.Count > 1)
            {
                var averageSeconds = this.times.ToArray().Average();
                this.AverageSpeed = Math.Round(averageSeconds, 2);
                this.AverageMinutesLeft = Math.Round(iterationsLeft * averageSeconds / 60, 2);
            }

            this.OptimizeChart();

            this.Timer = Stopwatch.StartNew();
        }

        public void ResetWorkingProcessToDefault()
        {
            this.WorkingLogs = string.Empty;
            this.ProgressBarValue = 0;
            this.ProgressBarMaximum = 1;
            this.AverageSpeed = 0;
            this.CurrentSpeed = 0;
            this.MinutesLeft = 0;
            this.AverageMinutesLeft = 0;
            this.ChartModel.ClearDispatch();
            this.ChartModel.AddDispatch(new DataPoint(0, 0));
            this.Title = "Working process";
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void OptimizeChart()
        {
            if (this.ChartModel.Count <= SettingsProvider.GetInstance().WorkingChartMaxCount) return;

            var oldChart = this.ChartModel.ToArray();
            var newChart = new List<DataPoint>();
            for (var j = 2; j < oldChart.Length; j += 2)
            {
                var averageX = (oldChart[j].X + oldChart[j - 1].X) / 2;
                var averageY = (oldChart[j].Y + oldChart[j - 1].Y) / 2;
                newChart.Add(new DataPoint(averageX, averageY));
            }

            this.ChartModel.ReplaceDispatch(newChart);
        }
    }
}