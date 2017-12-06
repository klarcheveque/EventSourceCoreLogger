using System;
using System.Diagnostics.Tracing;
using EventSourceCoreLogger.Sink;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;

namespace EventSourceCoreLogger.Registration
{
    public class SingleFileRegistration : IEventSourceRegistrer
    {
        private static readonly ObservableEventListener _listener = new ObservableEventListener();
        private EventSource _eventSource;

        public void Registrer(EventSource eventSource)
        {
            _eventSource = eventSource;
            _listener.EnableEvents(eventSource, EventLevel.Informational, Keywords.All);
            _listener.LogToFile();
        }

        public void Dispose()
        {
            _listener.DisableEvents(_eventSource);
            _listener.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}