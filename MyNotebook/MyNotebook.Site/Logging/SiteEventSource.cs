using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotebook.Site.Logging
{
    internal class SiteEventSource : EventSource
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

        private static readonly Lazy<SiteEventSource> Instance =
        new Lazy<SiteEventSource>(() => new SiteEventSource());

        private SiteEventSource() { }

        public static SiteEventSource Log { get { return Instance.Value; } }

        [Event(1, Message = "Application Failure: {0}",
        Level = EventLevel.Critical, Keywords = Keywords.Diagnostic)]
        public void Failure(string message)
        {
            this.WriteEvent(1, message);
        }
    }
}
