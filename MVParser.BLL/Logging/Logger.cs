using MVParser.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MVParser.BLL.Logging
{
    public class Logger : ILogger
    {
        private ReaderWriterLockSlim lock_ = new ReaderWriterLockSlim();

        public string LogDirectory { get; }

        public Logger()
        {
            LogDirectory = AppDomain.CurrentDomain.BaseDirectory + @"/_logs/" + DateTime.Now.ToString("dd-MM-yy HH-mm-ss") + @"/";

            if (!Directory.Exists(LogDirectory))
                Directory.CreateDirectory(LogDirectory);
        }

        public void Event(string _message)
        {
            lock_.EnterWriteLock();
            try
            {
                using (StreamWriter writetext = new StreamWriter(LogDirectory + "events.txt", append: true))
                {
                    writetext.WriteLine(DateTime.Now + " : " + _message);
                }
            }
            finally
            {
                lock_.ExitWriteLock();
            }

        }

        public void Error(string _message)
        {
            lock_.EnterWriteLock();
            try
            {
                using (StreamWriter writetext = new StreamWriter(LogDirectory + "errors.txt", append: true))
                {
                    writetext.WriteLine(DateTime.Now + " : " + _message);
                }
            }
            finally
            {
                lock_.ExitWriteLock();
            }

        }
    }
}
