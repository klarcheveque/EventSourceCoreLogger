using System;
using System.Diagnostics.Tracing;

namespace EventSourceCoreLogger
{
    public interface IEventSourceRegistrer : IDisposable
    {
        void Registrer(EventSource eventSource);
    }
}