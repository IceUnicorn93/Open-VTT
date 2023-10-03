using System;
using System.Collections.Generic;
using System.IO;

namespace OpenVTT.Logging
{
    internal class Logger
    {
        static string appPath;
        static DateTime startupTime;

        static List<string> lines = new List<string>();

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

            lines.Add($"{DateTime.Now:HH:mm:ss} {message}{Environment.NewLine}");

            if(lines.Count == 10)
            {
                var messageDump = string.Join(" ", lines.ToArray());
                File.AppendAllText(Path.Combine(appPath, $"Log - {startupTime:yyyy-MM-dd - HH.mm.ss}.txt"), messageDump);

                lines.Clear();
            }
        }
    }
}
