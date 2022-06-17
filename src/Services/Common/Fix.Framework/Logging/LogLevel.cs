using Fix.Logging.Policies;
using System;

namespace Fix.Logging
{
    public enum LogLevel
    {
        Fatal,
        Error,
        Warn,
        Info,
        Debug,
        Trace
    }

    public static class LogLevelExtensions
    {
        public static Type PolicyType(this LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Fatal:
                    return typeof(FatalPolicy);
                case LogLevel.Error:
                    return typeof(ErrorPolicy);
                case LogLevel.Warn:
                    return typeof(WarnPolicy);
                case LogLevel.Info:
                    return typeof(InfoPolicy);
                case LogLevel.Debug:
                    return typeof(DebugPolicy);
                case LogLevel.Trace:
                    return typeof(TracePolicy);
                default:
                    throw new Exception();
            }
        }
    }
}
