namespace SteamAutoMarket.WorkingProcess
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class SellTimeTracker
    {
        private readonly Stopwatch stopwatch = new Stopwatch();
        private readonly List<double> savedOneItemSecondsValues = new List<double>();
        private readonly double oneItemTimeDivider;

        public SellTimeTracker(int itemsPerIterationCount)
        {
            this.oneItemTimeDivider = 0.001 / itemsPerIterationCount;
            stopwatch.Start();
        }

        public void TrackTime(int itemsLeft)
        {
            this.stopwatch.Stop();

            this.ProcessCurrentStatus(itemsLeft);
            this.ProcessAverageStatus(itemsLeft);

            this.stopwatch.Restart();
        }

        private void ProcessAverageStatus(int itemsLeft)
        {
            var oneItemSeconds = this.savedOneItemSecondsValues.Count > 0 ? this.savedOneItemSecondsValues.Average() : 0d;

            Program.WorkingProcessForm.AppendWorkingProcessInfo(
                $"[TIME INFO] Average sell speed - {Math.Round(oneItemSeconds, 2)} sec/item.");

            var timeLeft = TimeSpan.FromSeconds(oneItemSeconds * itemsLeft).Duration();
            Program.WorkingProcessForm.AppendWorkingProcessInfo(
                $"[TIME INFO] Average time left - {this.GetClearTimeLeft(timeLeft)}");
        }

        private void ProcessCurrentStatus(int itemsLeft)
        {
            var oneItemSeconds = this.stopwatch.ElapsedMilliseconds * this.oneItemTimeDivider;
            this.savedOneItemSecondsValues.Add(oneItemSeconds);

            Program.WorkingProcessForm.AppendWorkingProcessInfo(
                $"[TIME INFO] Current sell speed - {Math.Round(oneItemSeconds, 2)} sec/item.");
        }

        private string GetClearTimeLeft(TimeSpan timeSpan)
        {
            var text = string.Empty;
            text += (timeSpan.TotalHours > 1) ? $"{timeSpan.TotalHours}" : null;
            text += (timeSpan.TotalMinutes > 1) ? $"{timeSpan.Minutes} minutes " : null;
            text += (timeSpan.TotalSeconds > 1) ? $"{timeSpan.Seconds} seconds " : null;

            return text;
        }
    }
}