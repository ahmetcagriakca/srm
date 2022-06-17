using System;

namespace Fix.Logging
{
    public class LogData
    {
        public LogLevel LogLevel { get; set; }
        public string Message { get; set; }
        public string AppName { get; set; }
        public DateTime UtcTimestamp { get; set; }
        public virtual string MachineName { get; set; }
        public string CorrlId { get; set; }
        public string User { get; set; }
    }


    public class LogData<TLog> : LogData where TLog : class
    {
        public TLog TLogInstance { get; set; }
    }
    //public class LogData<TLog> where TLog : class
    //{
    //    public LogLevel LogLevel { get; set; }
    //    public string Message { get; set; }
    //    public string AppName { get; set; }
    //    public DateTime UtcTimestamp { get; set; }
    //    public TLog TLogInstance { get; set; }
    //    public virtual string MachineName { get; set; }
    //    public string CorrlId { get; set; }
    //    public string User { get; set; }
    //}

    //public class LogData : Dictionary<string, string>
    //{
    //    public LogData()
    //    {

    //    }

    //}

    public static class LogDictionary
    {
        public static string USER = "User";
        public static string LEVEL = "Level";
        public static string MESSAGE = "Message";
        public static string APPLICATION = "AppName";
        public static string MACHINA_NAME = "MachineName";
        public static string CORRELATION_ID = "CorrlId";
    }
}
