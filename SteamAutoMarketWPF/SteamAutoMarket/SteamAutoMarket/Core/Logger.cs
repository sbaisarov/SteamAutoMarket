using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SteamAutoMarket.Core
{
    public abstract class Logger
    {
        protected abstract void LogMessage(string message, string level);

        public void Info(string message)
        {
            LogMessage(message, "INFO");
        }
        
        public void Warn(string message)
        {
            LogMessage(message, "WARN");
        }
        
        public void Debug(string message)
        {
            LogMessage(message, "DEBUG");
        }
        
        public void Error(string message)
        {
            LogMessage(message, "ERROR");
        }
        
        public void Critical(string message)
        {
            LogMessage(message, "CRITICAL");
        }
    }

    public class FileLogger : Logger
    {
        private static StreamWriter sw = null;
        private static readonly string LogFilePath = AppDomain.CurrentDomain.BaseDirectory + "logs.log";

        public FileLogger() : base()
        {
            sw = new StreamWriter(File.Create(LogFilePath), Encoding.GetEncoding("UTF-8"));
        }

        protected override void LogMessage(string message, string level)
        {
            sw.WriteLine(DateTime.Now + "\t" + level + ": " + message);
            sw.Flush();
        }
    }
}