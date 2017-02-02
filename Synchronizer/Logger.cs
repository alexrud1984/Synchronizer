using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronizer
{
    class Logger
    {
        private static object sync = new object();
        public static void Write(Exception ex)
        {
            try
            {
                string pathToLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
                if (!Directory.Exists(pathToLog))
                    Directory.CreateDirectory(pathToLog);
                string filename = Path.Combine(pathToLog, string.Format("{0}_{1:dd.MM.yyy}.log",
                AppDomain.CurrentDomain.FriendlyName, DateTime.Now));
                string fullText = string.Format("[{0:dd.MM.yyy HH:mm:ss.fff}] [{1}.{2}()] {3}\r\n",
                DateTime.Now, ex.TargetSite.DeclaringType, ex.TargetSite.Name, ex.Message);
                lock (sync)
                {
                    File.AppendAllText(filename, fullText, Encoding.GetEncoding("Windows-1251"));
                }
            }
            catch
            {
            }
        }

        public static void Write(string msg)
        {
            try
            {
                string pathToLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
                if (!Directory.Exists(pathToLog))
                    Directory.CreateDirectory(pathToLog);
                string filename = Path.Combine(pathToLog, string.Format("{0}_{1:dd.MM.yyy}.log",
                AppDomain.CurrentDomain.FriendlyName, DateTime.Now));
                string fullText = string.Format("[{0:dd.MM.yyy HH:mm:ss.fff}] {1}\r\n",
                DateTime.Now, msg);
                lock (sync)
                {
                    File.AppendAllText(filename, fullText, Encoding.GetEncoding("Windows-1251"));
                }
            }
            catch
            {
            }
        }

        public static void OpenLastLogfile()
        {
            List<FileInfo> logFiles = new List<FileInfo>();
            foreach (var item in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log")))
            {
                logFiles.Add(new FileInfo(item));
            }
            int index=0;
            try
            {
                for (int i = 0; i < logFiles.Count; i++)
                {
                    if (String.Equals(logFiles[i].Extension, ".log") && logFiles[i].LastAccessTime > logFiles[index].LastAccessTime)
                    {
                        index = i;
                    }
                }
                Process.Start("notepad.exe", logFiles[index].FullName);
            }
            catch
            { }

        }
    }
}
