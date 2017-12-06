using System;
using System.Diagnostics.Tracing;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;

namespace EventSourceCoreLogger.Sink
{
    public static class SingleFileSinkExtensions
    {
        public static SinkSubscription<SingleFileSink> LogToFile(
            this IObservable<EventEntry> eventStream)
        {
            var sink = new SingleFileSink();
            var subscription = eventStream.Subscribe(sink);
            return new SinkSubscription<SingleFileSink>(subscription, sink);
        }
    }
}