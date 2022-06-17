using System;
using System.IO;

namespace Fix.Logging
{
    public static class StaticLogger
    {
        public static DirectoryInfo DirectoryInfo { get; private set; }

        private static DirectoryInfo CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                return Directory.CreateDirectory(path);
            }

            return new DirectoryInfo(path);
        }

        public static void WriteToFile(string text)
        {
            try
            {
                DirectoryInfo = CreateDirectory("C:\\TEST\\CAGRI");
                ////    _readWriteLock.EnterWriteLock();
                string path = Path.Combine(DirectoryInfo.FullName, $"Log_{DateTime.Now.Date.ToString("ddMMyyyy")}.txt");
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine($"{DateTime.Now} {text}");
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
