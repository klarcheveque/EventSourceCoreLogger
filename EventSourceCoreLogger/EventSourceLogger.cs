using System;
using System.Diagnostics.Tracing;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging.Abstractions.Internal;

namespace EventSourceCoreLogger
{
    [EventSource(Name = "EventSourceLogger")]
    public class EventSourceLogger :EventSource, ILogger
    {
        private static readonly Lazy<EventSourceLogger> eventSourceLogger =
            new Lazy<EventSourceLogger>(() => new EventSourceLogger());

        public static EventSourceLogger Instance => eventSourceLogger.Value;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            List<Exception> exceptions = null;
            try
            {
                var message = formatter(state, exception);
                WriteEvent(logLevel.GetHashCode(), message, exception);
            }
            catch (Exception ex)
            {
                if (exceptions == null)
                {
                    exceptions = new List<Exception>();
                }

                exceptions.Add(ex);
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