namespace SteamAutoMarket.UI.Repository.Context
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using OxyPlot;
    using SteamAutoMarket.Core;
    using SteamAutoMarket.Properties;
    using SteamAutoMarket.UI.Pages;
    using SteamAutoMarket.UI.Repository.Settings;
    using SteamAutoMarket.UI.SteamIntegration;
    using SteamAutoMarket.UI.Utils.Extension;
    using SteamAutoMarket.UI.Utils.Logger;

    [Obfuscation(Exclude = true)]
    public class WorkingProcessDataContext : INotifyPropertyChanged
    {
        private readonly List<double> times = new List<double>();

        private double averageMinutesLeft;

        private double averageSpeed;

        private double currentSpeed;

        private double minutesLeft;

        private int progressBarMaximum = 1;

        private int progressBarValue;

        private string workingLogs;

        public WorkingProcessDataContext(string title, UiSteamManager manager)
        {
            this.Title = title;
            this.SteamManager = manager;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static ObservableCollection<string> GlobalWorkingProcessesList { get; set; }

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

        public CancellationToken CancellationToken { get; set; }

        public CancellationTokenSource CancellationTokenSource { get; set; }

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

        public UiSteamManager SteamManager { get; set; }

        public Stopwatch Timer { get; set; }

        public string Title { get; }

        public Task WorkingAction { get; set; }

        public string WorkingLogs
        {
            get => this.workingLogs;
            set
            {
                this.workingLogs = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<string> WorkingProcessesList
        {
            get => GlobalWorkingProcessesList;
            set
            {
                GlobalWorkingProcessesList = value;
                this.OnPropertyChanged();
            }
        }

        public void AppendLog(string message)
        {
            this.WorkingLogs += $"{UiLogAppender.GetCurrentDate()} - {message}{Environment.NewLine}";
            Logger.Log.Info(message);

            if (this.WorkingLogs.Length <= 100000) return;

            try
            {
                var lines = this.WorkingLogs.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                var start = lines.Length / 2;
                var end = lines.Length - start;
                this.WorkingLogs = string.Join(Environment.NewLine, lines.ToList().GetRange(start, end));
            }
            catch (Exception ex)
            {
                Logger.Log.Error($"Error on working process logs cutback - {ex.Message}", ex);
            }
        }

        public void ClearChart()
        {
            this.ChartModel.ClearDispatch();
            this.ChartModel.AddDispatch(new DataPoint(0, 0));
        }

        public void IncrementProgress()
        {
            if (this.ProgressBarValue < this.ProgressBarMaximum)
            {
                this.ProgressBarValue++;
            }
            UiGlobalVariables.WorkingProcessTab.UpdateStartBarProgress();

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
            this.ClearChart();
            this.times?.Clear();
        }

        public void StartWorkingProcess(Action action)
        {
            if (this.WorkingAction?.Status == TaskStatus.Running)
            {
                return;
            }

            WorkingProcess.OpenTab();

            UiGlobalVariables.LastInvokedWorkingProcessDataContext = this;
            if (UiGlobalVariables.WorkingProcessTab != null)
            {
                UiGlobalVariables.WorkingProcessTab.ChangeDataContext(this);
            }

            Task.Run(
                () =>
                {
                    try
                    {
                        this.ResetWorkingProcessToDefault();
                        this.AppendLog($"{this.Title} working process started");

                        this.CancellationTokenSource = new CancellationTokenSource();
                        this.CancellationToken = this.CancellationTokenSource.Token;

                        this.Timer = Stopwatch.StartNew();

                        this.WorkingAction = Task.Run(
                            () =>
                            {
                                try
                                {
                                    action();
                                }
                                catch (Exception e)
                                {
                                    Logger.Log.Error($"Error on working process - {e.Message}", e);
                                }

                                this.AppendLog("Working process finished");
                            });
                    }
                    catch (Exception e)
                    {
                        Logger.Log.Error($"Error on working process - {e.Message}", e);
                    }
                });
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void OptimizeChart()
        {
            if (this.ChartModel.Count <= SettingsProvider.GetInstance().WorkingChartMaxCount) return;

            var oldChart = this.ChartModel.ToList();
            var chartCenter = oldChart.Capacity / 2;
            var newChart = oldChart.GetRange(chartCenter, oldChart.Count - chartCenter);

            this.ChartModel.ReplaceDispatch(newChart);
        }

        private bool Equals(WorkingProcessDataContext other)
        {
            return Title == other.Title && Equals(WorkingAction, other.WorkingAction);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((WorkingProcessDataContext)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Title != null ? Title.GetHashCode() : 0) * 397) ^ (WorkingAction != null ? WorkingAction.GetHashCode() : 0);
            }
        }
    }
}
