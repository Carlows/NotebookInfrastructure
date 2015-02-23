using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotebook.Domain.Logging
{
    public class DomainEventSource : EventSource
    {
        public class Keywords
        {
            public const EventKeywords Data = (EventKeywords)1;
            public const EventKeywords Diagnostic = (EventKeywords)2;
            public const EventKeywords Perf = (EventKeywords)4;
        }

        public class Tasks
        {
            public const EventTask Data = (EventTask)1;
        }

        private static readonly Lazy<DomainEventSource> Instance =
        new Lazy<DomainEventSource>(() => new DomainEventSource());

        private DomainEventSource() { }

        public static DomainEventSource Log { get { return Instance.Value; } }

        [Event(1, Message = "Application Failure: {0}",
        Level = EventLevel.Critical, Keywords = Keywords.Diagnostic)]
        public void Failure(string message)
        {
            this.WriteEvent(1, message);
        }

        [Event(2, Message = "Database Update Error: {0}",
        Level = EventLevel.Error, Keywords = Keywords.Diagnostic)]
        public void DbUpdateError(string message)
        {
            this.WriteEvent(2, message);
        }

        [Event(3, Message = "Database Validation Error: {0}",
        Level = EventLevel.Warning, Keywords = Keywords.Diagnostic)]
        public void DbValidationError(string message)
        {
            this.WriteEvent(3, message);
        }
    }
}
