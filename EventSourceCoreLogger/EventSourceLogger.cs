using System;
using System.Diagnostics.Tracing;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.Extensions.Logging.Abstractions.Internal;

namespace EventSourceCoreLogger
{
    [EventSource(Name = "EventSourceLogger")]
    public class EventSourceLogger : EventSource, ILogger
    {
        private static readonly Lazy<EventSourceLogger> Instance =
            new Lazy<EventSourceLogger>(() => new EventSourceLogger());

        public static EventSourceLogger Logger => Instance.Value;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            List<Exception> exceptions = null;
            try
            {

                var message = formatter(state, exception);
                WriteEvent(1, message);
            }
            catch (Exception ex)
            {
                exceptions = new List<Exception> { ex };
            }

            if (exceptions != null && exceptions.Count > 0)
            {
                throw new AggregateException(
                    message: "An error occurred while writing to logger(s).", innerExceptions: exceptions);
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //TODO: Change LogLevel.Information to state driven by configuration
            return logLevel != LogLevel.None && logLevel >= LogLevel.Information;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return NullScope.Instance;
        }
    }
}