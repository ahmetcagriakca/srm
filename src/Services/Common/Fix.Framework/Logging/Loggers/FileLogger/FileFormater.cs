using Newtonsoft.Json;
using System;
using System.Text;

namespace Fix.Logging.Loggers.FileLogger
{
    public class FileFormater : IFileFormater
    {
        public StringBuilder Format<T>(LogData<T> logData) where T : class
        {
            var builder = new StringBuilder();

            builder
                .Append(DateTime.Now.ToString("HH:mm:ss:fff"))
                .Append(" ")
                .Append(logData.LogLevel)
                .Append(" ")
                .Append(logData.User)
                .Append(" ")
                .Append(logData.CorrlId)
                .AppendLine(logData.Message);



            if (logData.TLogInstance != null)
            {
                var json = JsonConvert.SerializeObject(logData.TLogInstance);
                builder.AppendLine(json);

            }
            return builder;
        }
    }
}
