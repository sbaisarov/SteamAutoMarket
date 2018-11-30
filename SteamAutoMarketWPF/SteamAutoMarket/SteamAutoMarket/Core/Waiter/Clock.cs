namespace SteamAutoMarket.Core.Waiter
{
    using System;

    public static class Clock
    {
        /// <summary>
        /// Gets the current date and time values.
        /// </summary>
        public static DateTime Now => DateTime.Now;

        /// <summary>
        /// Gets a value indicating whether the current date and time is before the specified date and time.
        /// </summary>
        /// <param name="otherDateTime">The date and time values to compare the current date and time values to.</param>
        /// <returns><see langword="true"/> if the current date and time is before the specified date and time; otherwise, <see langword="false"/>.</returns>
        public static bool IsNowBefore(DateTime otherDateTime) => DateTime.Now < otherDateTime;

        /// <summary>
        /// Calculates the date and time values after a specific delay.
        /// </summary>
        /// <param name="delay">The delay after to calculate.</param>
        /// <returns>The future date and time values.</returns>
        public static DateTime LaterBy(TimeSpan delay) => DateTime.Now.Add(delay);
    }
}