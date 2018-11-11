namespace Core.Waiter
{
    using System;

    public class Clock
    {
        /// <summary>
        /// Gets the current date and time values.
        /// </summary>
        public DateTime Now => DateTime.Now;

        /// <summary>
        /// Calculates the date and time values after a specific delay.
        /// </summary>
        /// <param name="delay">The delay after to calculate.</param>
        /// <returns>The future date and time values.</returns>
        public DateTime LaterBy(TimeSpan delay) => DateTime.Now.Add(delay);

        /// <summary>
        /// Gets a value indicating whether the current date and time is before the specified date and time.
        /// </summary>
        /// <param name="otherDateTime">The date and time values to compare the current date and time values to.</param>
        /// <returns><see langword="true"/> if the current date and time is before the specified date and time; otherwise, <see langword="false"/>.</returns>
        public bool IsNowBefore(DateTime otherDateTime) => DateTime.Now < otherDateTime;
    }
}