using Fix.Searching;
using Nest;
using System;

namespace Fix.Logging.Loggers.Elasticsearch
{

    public abstract class DocType : IDocType
    {
        protected DocType()
        {
            Id = Guid.NewGuid();
        }

        [Ignore]
        public Guid Id { get; set; }


        [Text(Name = "appname")]
        public string AppName { get; set; }

        [Text(Name = "name")]
        public abstract string Name { get; }

        [Text(Name = "level")]
        public string Level { get; set; }

        [Text(Name = "corlId")]
        public string CorrelationId { get; set; }

        [Text(Name = "obj", Index = false)]
        public object LoggingObject { get; set; }

        [Text(Name = "machine", Index = false)]
        public string MachineName { get; set; }

        [Text(Name = "message", Index = false)]
        public string Message { get; set; }

        public bool IsHttpApp { get; set; }

        [Text(Name = "user")]
        public object CurrentUser { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
