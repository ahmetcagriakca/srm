using Fix.Logging.Config;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Fix.Logging.Loggers.FileLogger
{
    public class TextFileLogger : BaseLogger
    {
        private readonly IFileFormater fileFormater;
        private readonly LoggingConfig loggingConfig;

        private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();
        public string FileNameWithoutSuffix { get; private set; }

        public DirectoryInfo DirectoryInfo { get; private set; }

        public TextFileLogger(
            ILogDataFactory logDataFactory,
            IFileFormater fileFormater,
            LoggingConfig loggingConfig) : base(logDataFactory)
        {
            this.fileFormater = fileFormater ?? throw new ArgumentNullException(nameof(fileFormater));
            this.loggingConfig = loggingConfig ?? throw new ArgumentNullException(nameof(loggingConfig));
            InitSettings();
        }

        private void InitSettings()
        {
            DirectoryInfo = CreateDirectory(loggingConfig.FileLoggerConfig.Directory);
            FileNameWithoutSuffix = GetFileNameWithoutSuffix();
        }

        public override string AliasName => "FileLogger";

        protected override void LogInternal<T>(LogData<T> logData)
        {
            LogInternalAsync(logData).Wait();
        }

        protected override async Task LogInternalAsync<T>(LogData<T> logData)
        {
            var text = fileFormater.Format(logData);
            await WriteToFile(text.ToString(), GetFileName());
        }

        private DirectoryInfo CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                return Directory.CreateDirectory(path);
            }

            return new DirectoryInfo(path);
        }
        private string GetFileNameWithoutSuffix()
        {
            return Path.Combine(DirectoryInfo.FullName, "Log_{0}.txt");
        }
        private string GetFileName()
        {
            return string.Format(FileNameWithoutSuffix, DateTime.Now.ToString("yyyyMMddHH"));
        }

        public async Task WriteToFile(string text, string path)
        {
            try
            {
                //    _readWriteLock.EnterWriteLock();
                using (StreamWriter sw = File.AppendText(path))
                {
                    await sw.WriteLineAsync(text);
                    sw.Close();
                }
            }
            finally
            {
                //  _readWriteLock.ExitWriteLock();
            }
        }
    }
}
