namespace Core.Waiter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    public class Waiter
    {
        public class DefaultWait<T>
        {
            private readonly T input;

            private readonly Clock clock;

            private readonly List<Type> ignoredExceptions = new List<Type>();

            /// <summary>
            /// Initializes a new instance of the <see cref="DefaultWait&lt;T&gt;"/> class.
            /// </summary>
            /// <param name="input">The input value to pass to the evaluated conditions.</param>
            public DefaultWait(T input)
                : this(input, new Clock())
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="DefaultWait&lt;T&gt;"/> class.
            /// </summary>
            /// <param name="input">The input value to pass to the evaluated conditions.</param>
            /// <param name="clock">The clock to use when measuring the timeout.</param>
            public DefaultWait(T input, Clock clock)
            {
                if (input == null)
                {
                    throw new ArgumentNullException(nameof(input), "input cannot be null");
                }

                this.input = input;
                this.clock = clock ?? throw new ArgumentNullException(nameof(clock), "clock cannot be null");
            }

            /// <summary>
            /// Gets or sets how long to wait for the evaluated condition to be true. The default timeout is 500 milliseconds.
            /// </summary>
            public TimeSpan Timeout { get; set; } = DefaultSleepTimeout;

            /// <summary>
            /// Gets or sets how often the condition should be evaluated. The default timeout is 500 milliseconds.
            /// </summary>
            public TimeSpan PollingInterval { get; set; } = DefaultSleepTimeout;

            /// <summary>
            /// Gets or sets the message to be displayed when time expires.
            /// </summary>
            public string Message { get; set; } = string.Empty;

            private static TimeSpan DefaultSleepTimeout => TimeSpan.FromMilliseconds(500);

            /// <summary>
            /// Configures this instance to ignore specific types of exceptions while waiting for a condition.
            /// Any exceptions not whitelisted will be allowed to propagate, terminating the wait.
            /// </summary>
            /// <param name="exceptionTypes">The types of exceptions to ignore.</param>
            public void IgnoreExceptionTypes(params Type[] exceptionTypes)
            {
                if (exceptionTypes == null)
                {
                    throw new ArgumentNullException(nameof(exceptionTypes), "exceptionTypes cannot be null");
                }

                if (exceptionTypes.Any(exceptionType => !typeof(Exception).IsAssignableFrom(exceptionType)))
                {
                    throw new ArgumentException(
                        "All types to be ignored must derive from System.Exception",
                        nameof(exceptionTypes));
                }

                this.ignoredExceptions.AddRange(exceptionTypes);
            }

            /// <summary>
            /// Repeatedly applies this instance's input value to the given function until one of the following
            /// occurs:
            /// <para>
            /// <list type="bullet">
            /// <item>the function returns neither null nor false</item>
            /// <item>the function throws an exception that is not in the list of ignored exception types</item>
            /// <item>the timeout expires</item>
            /// </list>
            /// </para>
            /// </summary>
            /// <typeparam name="TResult">The delegate's expected return type.</typeparam>
            /// <param name="condition">A delegate taking an object of type T as its parameter, and returning a TResult.</param>
            /// <param name="failOnTimeOver">If true - will fail with exception on time ends</param>
            /// <returns>The delegate's return value.</returns>
            public TResult Until<TResult>(Func<T, TResult> condition, bool failOnTimeOver = false)
            {
                if (condition == null)
                {
                    throw new ArgumentNullException(nameof(condition), "condition cannot be null");
                }

                var resultType = typeof(TResult);
                if ((resultType.IsValueType && resultType != typeof(bool))
                    || !typeof(object).IsAssignableFrom(resultType))
                {
                    throw new ArgumentException(
                        "Can only wait on an object or boolean response, tried to use type: " + resultType,
                        nameof(condition));
                }

                Exception lastException = null;
                var endTime = this.clock.LaterBy(this.Timeout);
                while (true)
                {
                    try
                    {
                        var result = condition(this.input);
                        if (resultType == typeof(bool))
                        {
                            var boolResult = result as bool?;
                            if (boolResult.HasValue && boolResult.Value)
                            {
                                return result;
                            }
                        }
                        else
                        {
                            if (result != null)
                            {
                                return result;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!this.IsIgnoredException(ex))
                        {
                            throw;
                        }

                        lastException = ex;
                    }

                    // Check the timeout after evaluating the function to ensure conditions
                    // with a zero timeout can succeed.
                    if (failOnTimeOver && !this.clock.IsNowBefore(endTime))
                    {
                        var timeoutMessage = string.Format(
                            CultureInfo.InvariantCulture,
                            "Timed out after {0} seconds",
                            this.Timeout.TotalSeconds);
                        if (!string.IsNullOrEmpty(this.Message))
                        {
                            timeoutMessage += ": " + this.Message;
                        }

                        this.ThrowTimeoutException(timeoutMessage, lastException);
                    }

                    Thread.Sleep(this.PollingInterval);
                }
            }

            /// <summary>
            /// Throws a <see cref="TimeoutException"/> with the given message.
            /// </summary>
            /// <param name="exceptionMessage">The message of the exception.</param>
            /// <param name="lastException">The last exception thrown by the condition.</param>
            /// <remarks>This method may be overridden to throw an exception that is
            /// idiomatic for a particular test infrastructure.</remarks>
            protected virtual void ThrowTimeoutException(string exceptionMessage, Exception lastException)
            {
                throw new TimeoutException(exceptionMessage, lastException);
            }

            private bool IsIgnoredException(Exception exception)
            {
                return this.ignoredExceptions.Any(type => type.IsInstanceOfType(exception));
            }
        }
    }
}