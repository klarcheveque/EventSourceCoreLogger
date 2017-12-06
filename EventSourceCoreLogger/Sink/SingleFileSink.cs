using System;
using System.Diagnostics.Tracing;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks;


namespace EventSourceCoreLogger.Sink
{
    public sealed class SingleFileSink : IObserver<EventEntry>
    {
        private FlatFileSink _sink;

        public SingleFileSink()
        {
           var  eventTextFormatter = new EventTextFormatter(verbosityThreshold: EventLevel.Informational, dateTimeFormat:"o");
            _sink = new FlatFileSink("someFileName.txt", eventTextFormatter, true);
        }

        public void OnCompleted()
        {
            _sink.OnCompleted();
        }

        public void OnError(Exception error)
        {
            _sink.OnError(error);
        }

        public void OnNext(EventEntry value)
        {
           

            //Console.WriteLine("OnNext");
            if (value != null)
            {

                //System.IO.File.AppendAllText(@"D:\bob.txt", "jaime les chats");
                var message = value.Payload[0];
                Console.WriteLine(message);


                _sink.OnNext(value);
            }
        }

    }
}