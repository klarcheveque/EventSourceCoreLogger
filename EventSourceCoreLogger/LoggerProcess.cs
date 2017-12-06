using System.Diagnostics.Tracing;
using EventSourceCoreLogger.Registration;

namespace EventSourceCoreLogger
{
    public class LoggerProcess
    {
        private static EventSource _eventSource;
        private static IEventSourceRegistrer _eventSourceRegistrer;

        public static void Start(EventSource eventSource)
        {
            _eventSource = eventSource;

            _eventSourceRegistrer = new SingleFileRegistration();
            _eventSourceRegistrer.Registrer(_eventSource);
        }

        public static void Stop()
        {
            _eventSourceRegistrer.Dispose();

            _eventSource.Dispose();

        }
    }
}