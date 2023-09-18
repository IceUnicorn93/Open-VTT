using System;
using System.IO;

namespace OpenVTT.Logging
{
    internal class Logger
    {
        static string appPath;
        static DateTime startupTime;

        public Logger(string applicationPath)
        {
            appPath = applicationPath;
            startupTime = DateTime.Now;

            //Clean old Log-Files
            var files = Directory.GetFiles(applicationPath, "Log - *.txt", SearchOption.AllDirectories);
            foreach (var file in files) { File.Delete(file); }
        }

        public static void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(appPath)) return;
            if (default(DateTime) == startupTime) return;

            File.AppendAllText(Path.Combine(appPath, $"Log - {startupTime:yyyy-MM-dd - HH.mm.ss}.txt"), $"{DateTime.Now:HH:mm:ss} {message}{Environment.NewLine}");
        }
    }
}
